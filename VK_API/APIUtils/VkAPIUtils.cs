using Aquality.Selenium.Core.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Data.Common;
using System.Net;
using System.Text;
using VK_API.Resources;
using static System.Net.Mime.MediaTypeNames;
using static System.Net.WebRequestMethods;

namespace VK_API.APIUtils
{
    public static class VkAPIUtils
    {
        private static RestClient client = new RestClient(Config.urlToAPIRequest);

        public static RestResponse SendPost(string postMessage)
        {
            RestRequest sendPostRequest = new RestRequest(Config.urlToAPIRequest + TestData.requestCreatePost, Method.Post);
            sendPostRequest.AddParameter(Config.owner_id, TestData.accountId, ParameterType.QueryString);
            sendPostRequest.AddParameter(Config.access_token, TestData.accessToken, ParameterType.QueryString);
            sendPostRequest.AddParameter(Config.message, postMessage, ParameterType.QueryString);
            sendPostRequest.AddParameter(Config.v, Config.v_value, ParameterType.QueryString);
            return client.ExecutePost(sendPostRequest);
        }

        public static string getWallUploadServer()
        {
            RestRequest getUploadServerRequest = new RestRequest(Config.urlToAPIRequest + TestData.requesGetUploadServer);
            getUploadServerRequest.AddParameter(Config.owner_id, TestData.accountId, ParameterType.QueryString);
            getUploadServerRequest.AddParameter(Config.access_token, TestData.accessToken, ParameterType.QueryString);
            getUploadServerRequest.AddParameter(Config.v, Config.v_value, ParameterType.QueryString);
            JObject answerAboutUploadServer = JObject.Parse(client.ExecuteGet(getUploadServerRequest).Content);
            string requesUploadPhotoToServerStr = answerAboutUploadServer["response"]["upload_url"].ToString();

            return requesUploadPhotoToServerStr;
        }

        public static JObject uploadPhotoToServer(string requesUploadPhotoToServerStr)
        {
            string photoPath = AppDomain.CurrentDomain.BaseDirectory + Paths.pictureToSendPath;
            var c = new WebClient();
            var r2 = Encoding.UTF8.GetString(c.UploadFile(requesUploadPhotoToServerStr, "POST", photoPath));
            JObject uploadinPicToSomewhereJOject = JsonConvert.DeserializeObject(r2) as JObject;
            c.Dispose();

            return uploadinPicToSomewhereJOject;
        }

        public static JObject saveResultOnServer(JObject uploadinPicToSomewhereJOject)
        {
            RestRequest requestDoSomethingWithPhoto = new RestRequest(Config.urlToAPIRequest + TestData.saveWallPhoto, Method.Post);
            requestDoSomethingWithPhoto.AddParameter(Config.access_token, TestData.accessToken, ParameterType.QueryString);
            requestDoSomethingWithPhoto.AddParameter(Config.server, uploadinPicToSomewhereJOject["server"], ParameterType.QueryString);
            requestDoSomethingWithPhoto.AddParameter(Config.photo, uploadinPicToSomewhereJOject["photo"], ParameterType.QueryString);
            requestDoSomethingWithPhoto.AddParameter(Config.hash, uploadinPicToSomewhereJOject["hash"], ParameterType.QueryString);
            requestDoSomethingWithPhoto.AddParameter(Config.v, Config.v_value, ParameterType.QueryString);

            string saveOnServerResultStr = client.ExecutePost(requestDoSomethingWithPhoto).Content;
            JObject saveOnServerResultJOject = JObject.Parse(saveOnServerResultStr);

            return saveOnServerResultJOject;
        }

        public static RestResponse EditPost(int postId, string message, JObject saveOnServerResultJOject)
        {
            RestRequest requesEditWallPost = new RestRequest(Config.urlToAPIRequest + TestData.editWallPost, Method.Post);
            string photoId = "photo" + saveOnServerResultJOject["response"][0]["owner_id"] + "_" + saveOnServerResultJOject["response"][0]["id"];
            requesEditWallPost.AddParameter(Config.access_token, TestData.accessToken, ParameterType.QueryString);
            requesEditWallPost.AddParameter(Config.owner_id, saveOnServerResultJOject["response"][0]["owner_id"], ParameterType.QueryString);
            requesEditWallPost.AddParameter(Config.post_id, postId.ToString(), ParameterType.QueryString);
            requesEditWallPost.AddParameter(Config.message, message, ParameterType.QueryString);
            requesEditWallPost.AddParameter(Config.attachments, photoId, ParameterType.QueryString);
            requesEditWallPost.AddParameter(Config.v, Config.v_value, ParameterType.QueryString);

            return client.ExecutePost(requesEditWallPost);

        }

        public static RestResponse CreateCommentToPostById(int postId, string message)
        {
            RestRequest requestCreateComment = new RestRequest(Config.urlToAPIRequest + TestData.createWallPostComment);
            requestCreateComment.AddParameter(Config.access_token, TestData.accessToken, ParameterType.QueryString);
            requestCreateComment.AddParameter(Config.owner_id, TestData.accountId, ParameterType.QueryString);
            requestCreateComment.AddParameter(Config.post_id, postId, ParameterType.QueryString);
            requestCreateComment.AddParameter(Config.message, message, ParameterType.QueryString);
            requestCreateComment.AddParameter(Config.v, Config.v_value, ParameterType.QueryString);

            return client.ExecutePost(requestCreateComment);
        }

        public static JArray getUserLikedPostById(int postId)
        {
            RestRequest requestGetLikes = new RestRequest(Config.urlToAPIRequest + TestData.getLikes);
            requestGetLikes.AddParameter(Config.access_token, TestData.accessToken, ParameterType.QueryString);
            requestGetLikes.AddParameter(Config.owner_id, TestData.accountId, ParameterType.QueryString);
            requestGetLikes.AddParameter(Config.type, "post", ParameterType.QueryString);
            requestGetLikes.AddParameter(Config.item_id, postId, ParameterType.QueryString);
            requestGetLikes.AddParameter(Config.v, Config.v_value, ParameterType.QueryString);

            JObject likesInfoJObject = JObject.Parse(client.ExecuteGet(requestGetLikes).Content);
            JArray usersLLikedList = (JArray)likesInfoJObject["response"]["items"];

            return usersLLikedList;
        }

        public static RestResponse DeletePostById(int postId)
        {
            RestRequest requesDeletePost = new RestRequest(Config.urlToAPIRequest + TestData.deletePost);
            requesDeletePost.AddParameter(Config.access_token, TestData.accessToken, ParameterType.QueryString);
            requesDeletePost.AddParameter(Config.owner_id, TestData.accountId, ParameterType.QueryString);
            requesDeletePost.AddParameter(Config.post_id, postId, ParameterType.QueryString);
            requesDeletePost.AddParameter(Config.v, Config.v_value, ParameterType.QueryString);

            return client.Execute(requesDeletePost);
        }
    }
}