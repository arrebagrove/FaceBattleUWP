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
        private static async Task<string> MakeAuthCode()
        {
            var uid = LocalSettingHelper.GetValue("uid");
            var userName = LocalSettingHelper.GetValue("username");
            var authCode = LocalSettingHelper.GetValue("authcode");

            var time = await GetServerTimeStampAsync(CTSFactory.MakeCTS().Token);
            var md5 = MD5.GetMd5String(uid + authCode + time);
            var code = md5.Substring(9, 16) + time;
            return code;
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
            var keyValue = new List<KeyValuePair<string, string>>();
            keyValue.Add(new KeyValuePair<string, string>("uid", uid));
            keyValue.Add(new KeyValuePair<string, string>("type", type));
            keyValue.Add(new KeyValuePair<string, string>("token", await MakeAuthCode()));
            data.Add(new FormUrlEncodedContent(keyValue));
            data.Add(new ByteArrayContent(photoBytes));

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
