using Allure.Commons;
using Aquality.Selenium.Browsers;
using Aquality.Selenium.Core.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using RestSharp;
using System;
using System.Linq;
using VK_API.APIUtils;
using VK_API.Form_Objects;
using VK_API.Resources;
using VK_API.Supporting_Classes;

namespace VK_API
{
    [AllureNUnit]
    public class Tests : BaseTest
    {
        [Test]
        [AllureTag("Regression")]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureIssue("ISSUE-1")]
        [AllureTms("TMS-12")]
        [AllureOwner("User")]
        [AllureSuite("PassedSuite")]
        [AllureSubSuite("NoAssert")]
        public void TestVK()
        {
            Logger.Instance.Info("Start step 1 and 2");

            LoginPage loginPage = new LoginPage();
            loginPage.SendLogin(TestData.login);
            loginPage.ClickSubmitButton();
            EnterPasswordPage enterPasswordPage = new EnterPasswordPage();
            enterPasswordPage.SendPassword(TestData.password);
            enterPasswordPage.ClickContinueButton();

            Logger.Instance.Info("Start step 3");

            FeedPage feedPage = new FeedPage();
            feedPage.State.WaitForDisplayed();
            feedPage.ClickProfileButton();

            Logger.Instance.Info("Start step 4");

            ProfilePage profilePage = new ProfilePage();
            profilePage.State.WaitForDisplayed();
            string messageStr = RandomUtils.CreateRandomString();
            RestResponse postsResponse = VkAPIUtils.SendPost(messageStr);
            

            Logger.Instance.Info("Start step 5");

            string answerFromVkStr = postsResponse.Content;
            JObject answerFromVkJObject = JObject.Parse(answerFromVkStr);
            int createdPostId = (int)answerFromVkJObject.GetValue("response")["post_id"];
            string textFromCreatedPost = profilePage.GetPostTextById(createdPostId);
            bool isPostAuthorCorrect = profilePage.IsPostAuthorCorrect(createdPostId);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(messageStr, textFromCreatedPost, "Unexpected text from created post");
                Assert.True(isPostAuthorCorrect);
            });

            Logger.Instance.Info("Start step 6");

            string messageToEditStr = RandomUtils.CreateRandomString();
            string answerFromEditPostStr =  VkAPIUtils.EditPost(createdPostId, messageToEditStr,
                VkAPIUtils.saveResultOnServer(
                    VkAPIUtils.uploadPhotoToServer(
                        VkAPIUtils.getWallUploadServer()))).Content;

            Logger.Instance.Info("Start step 7");

            JObject answerFromEditPostJObject = JObject.Parse(answerFromEditPostStr);
            int editPostId = (int)answerFromEditPostJObject.GetValue("response")["post_id"];
            string textFromEditPost = profilePage.GetPostTextById(editPostId);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(messageToEditStr, textFromEditPost, "Unexpected text from edit post");
            });

            Logger.Instance.Info("Start step 8");

            string messageToCreateCommentStr = RandomUtils.CreateRandomString();
            string answerFromCreateCommentStr = VkAPIUtils.CreateCommentToPostById(editPostId, messageToCreateCommentStr).Content;

            Logger.Instance.Info("Start step 9");

            browser.Refresh();
            JObject answerFromCreateCommentJObject = JObject.Parse(answerFromCreateCommentStr);
            int commentId = (int)answerFromCreateCommentJObject.GetValue("response")["comment_id"];
            string textFromCreaterComment = profilePage.GetCommentTextById(commentId);
            bool isCommentAuthorCorrect = profilePage.IsCommentAuthorCorrect(commentId);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(messageToCreateCommentStr, textFromCreaterComment, "Unexpected text from created comment");
                Assert.True(isCommentAuthorCorrect);
            });

            Logger.Instance.Info("Start step 10");
            profilePage.likePostById(editPostId);

            Logger.Instance.Info("Start step 11");

            JArray userSLikedPostJArray = VkAPIUtils.getUserLikedPostById(editPostId);
            List<string> userSLikedPostArr = userSLikedPostJArray.ToObject<List<string>>();
            bool isUserLikedPost = userSLikedPostArr.Contains(TestData.accountId);
            Assert.True(isUserLikedPost);

            Logger.Instance.Info("Start step 12");
            VkAPIUtils.DeletePostById(editPostId);

            Logger.Instance.Info("Start step 12");
            Assert.True(profilePage.IsPostDoesNotExist(editPostId));
        }
    }
}