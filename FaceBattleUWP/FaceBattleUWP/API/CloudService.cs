using JP.API;
using JP.Utils.Data.Json;
using JP.Utils.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Data.Json;

namespace FaceBattleUWP.API
{
    public static class CloudService
    {
        public static async Task<CommonRespMsg> LoginAsync(string username, string pwd,CancellationToken token)
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
            catch(Exception)
            {
                result.ErrorCode = 0;
            }
        }
    }
}
