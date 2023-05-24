using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;
using System.Security.Cryptography.X509Certificates;
using VK_API.Resources;

namespace VK_API.Form_Objects
{
    public class ProfilePage : Form
    {
        private string postByIdStr = "//div[@id='post802756743_{0}']//div[contains(@class, 'wall_post_text')]";
        private string authorOfPostByPostIdStr = "//div[@id='post802756743_{0}']//a[@class='author']";
        private string commentTextByIdStr = "//div[@id='post802756743_{0}']//div[contains(@class, 'wall_reply_text')]";
        private string likeButtonByPostIdStr = "//div[@id='post802756743_{0}']//span[contains(@class,'like')]";
        private string postContentByPostIdStr = "//div[@id='post802756743_{0}']//div[@class='post_content']";
        public ProfilePage() : base(By.ClassName("ProfileHeader"), "ProfilePage") { }

        public string GetPostTextById(int postId)
        {
            string postByIdLocatorStr = String.Format(postByIdStr, postId.ToString());
            ITextBox postText = ElementFactory.GetTextBox(By.XPath(postByIdLocatorStr), "postText");
            return postText.Text;
        }
        public bool IsPostAuthorCorrect(int postId)
        {
            string authorLinkByPostIdLocatorStr = String.Format(authorOfPostByPostIdStr, postId.ToString());
            ILink authorLink = ElementFactory.GetLink(By.XPath(authorLinkByPostIdLocatorStr), "authorPostLink");
            return authorLink.GetAttribute("href").Contains(TestData.accountId);
        }

        public string GetCommentTextById(int commentId)
        {
            string commentByIdLocatorStr = String.Format(commentTextByIdStr, commentId.ToString());
            ITextBox commentText = ElementFactory.GetTextBox(By.XPath(commentByIdLocatorStr), "commentText");
            return commentText.Text;
        }

        public bool IsCommentAuthorCorrect(int comntId)
        {
            string authorLinkByCommentIdLocatorStr = String.Format(authorOfPostByPostIdStr, comntId.ToString());
            ILink authorLink = ElementFactory.GetLink(By.XPath(authorLinkByCommentIdLocatorStr), "authorCommentLink");
            return authorLink.GetAttribute("href").Contains(TestData.accountId);
        }

        public void likePostById(int postId)
        {
            string likeButtonLocatorStr = String.Format(likeButtonByPostIdStr, postId);
            IButton likeButton = ElementFactory.GetButton(By.XPath(likeButtonLocatorStr), "likeButton");
            likeButton.Click();
        }

        public bool IsPostDoesNotExist(int postId)
        {
            string postByIdLocatorStr = String.Format(postContentByPostIdStr, postId);
            ILabel postToCheck = ElementFactory.GetLabel(By.XPath(postByIdLocatorStr), "postToCheck");

            return postToCheck.State.WaitForNotDisplayed();
        }
    }
}
