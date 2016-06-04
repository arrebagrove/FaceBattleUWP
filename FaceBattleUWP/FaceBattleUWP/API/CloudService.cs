using JP.API;
using JP.Utils.Data;
using JP.Utils.Data.Json;
using JP.Utils.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Data.Json;

namespace FaceBattleUWP.API
{
    public static class CloudService
    {
        private static async Task<string> MakeAuthToken()
        {
            var uid = LocalSettingHelper.GetValue("uid");
            var userName = LocalSettingHelper.GetValue("username");
            var authCode = LocalSettingHelper.GetValue("authcode");

            var timeResult = await GetServerTimeStampAsync(CTSFactory.MakeCTS().Token);
            var time = timeResult.JsonSrc;

            var md5 = MD5.GetMd5String(uid + authCode + time);
            var token = md5.Substring(8, 16) + time;
            return token;
        }

        public static async Task<CommonRespMsg> GetServerTimeStampAsync(CancellationToken token)
        {
            return await HttpRequestSender.SendGetRequestAsync(URLs.GET_TIME, token);
        }

        public static async Task<CommonRespMsg> LoginAsync(string username, string pwd, CancellationToken token)
        {
            var param = new List<KeyValuePair<string, string>>();
            param.Add(new KeyValuePair<string, string>("username", username));
            param.Add(new KeyValuePair<string, string>("password", pwd));

            return await HttpRequestSender.SendPostRequestAsync(URLs.LOGIN, param, token);
        }

        public static async Task<CommonRespMsg> RegisterAsync(string username, string pwd, CancellationToken token)
        {
            var param = new List<KeyValuePair<string, string>>();
            param.Add(new KeyValuePair<string, string>("username", username));
            param.Add(new KeyValuePair<string, string>("password", pwd));

            return await HttpRequestSender.SendPostRequestAsync(URLs.REGISTER, param, token);
        }

        public static async Task<HttpResponseMessage> UploadImageAsync(string uid, byte[] photoBytes, string type, CancellationToken token)
        {
            var client = new HttpClient();

            var request = new HttpRequestMessage();
            request.RequestUri = new Uri(URLs.CREATE_BATTLE);
            request.Method = HttpMethod.Post;

            var data = new MultipartFormDataContent();
            data.Add(new StringContent(uid), "uid");
            data.Add(new StringContent(type), "type");
            data.Add(new StringContent(await MakeAuthToken()), "token");
            data.Add(new ByteArrayContent(photoBytes), "photo","photo.jpg");

            request.Content = data;

            return await client.SendAsync(request, token);  
        }

        public static void CheckAPIResult(this CommonRespMsg result)
        {
            try
            {
                var jsonObj = JsonObject.Parse(result.JsonSrc);
                var code = JsonParser.GetNumberFromJsonObj(jsonObj, "code");
                result.ErrorCode = (int)code;
                var msg = JsonParser.GetStringFromJsonObj(jsonObj, "msg");
                result.ErrorMsg = msg;
            }
            catch (Exception)
            {
                result.ErrorCode = 0;
            }
        }
    }
}
