using Aquality.Selenium.Core.Configurations;
using Aquality.Selenium.Core.Utilities;

namespace VK_API.Resources
{
    public class TestData
    {
        public static ISettingsFile testDataArr = new JsonSettingsFile("Test_Data.json");

        public static string login = testDataArr.GetValue<string>("login");
        public static string password = testDataArr.GetValue<string>("password");
        public static string requestCreatePost = testDataArr.GetValue<string>("requestCreatePost");
        public static string requesGetUploadServer = testDataArr.GetValue<string>("requesGetUploadServer");
        public static string saveWallPhoto = testDataArr.GetValue<string>("saveWallPhoto");
        public static string editWallPost = testDataArr.GetValue<string>("editWallPost");
        public static string createWallPostComment = testDataArr.GetValue<string>("createWallPostComment");
        public static string getLikes = testDataArr.GetValue<string>("getLikes");
        public static string deletePost = testDataArr.GetValue<string>("deletePost");
        public static string accessToken = testDataArr.GetValue<string>("accessToken");
        public static string accountId = testDataArr.GetValue<string>("accountId");
        public static string firstPartPostId = testDataArr.GetValue<string>("firstPartPostId");
    }
}


