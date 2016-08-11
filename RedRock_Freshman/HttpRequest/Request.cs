using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RedRock_Freshman.HttpRequest
{
    public class Request
    {
        public static async Task<string> YuanChuang_Request()
        {
            HttpClient httpclient = new HttpClient();
            HttpResponseMessage response = new HttpResponseMessage();
            List<KeyValuePair<string, string>> param = new List<KeyValuePair<string, string>>();
            string result = "";
            try
            {
                param.Add(new KeyValuePair<string, string>("page", "0"));
                param.Add(new KeyValuePair<string, string>("size", "9"));
                response = await httpclient.PostAsync(Resource.Api.yuanchuang_api, new FormUrlEncodedContent(param));
                result = await response.Content.ReadAsStringAsync();
                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
