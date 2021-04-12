using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System; 
using System.Net.Http;
 

namespace ShopBridgeApp.AppCommon
{
    public class APIHandler
    {

        private string strURL = "http://localhost:8021/api/";

        public string messageDetail { get; set; } = string.Empty;
        public HttpClient Client { get; set; }

        public APIHandler()
        {
            Client = new HttpClient();
        }

        private readonly AppIdentitySettings _appIdentitySettings;
        public APIHandler( IOptions<AppIdentitySettings> appIdentitySettingsAccessor)
        {
            //_appIdentitySettings = appIdentitySettingsAccessor.Value; 
            //Client.BaseAddress = new Uri(_appIdentitySettings.APISettings.Url); 
        }
     
       
        public HttpResponseMessage GetResponse(string url)
        {
            return Client.GetAsync(strURL+url).Result;
        }
        public HttpResponseMessage PutResponse(string url, object model)
        {
            return Client.PutAsJsonAsync(strURL + url, model).Result;
        }
        public HttpResponseMessage PostResponse(string url, object model)
        {
            return Client.PostAsJsonAsync(strURL + url, model).Result;

        }
        public HttpResponseMessage DeleteResponse(string url)
        {
            return Client.DeleteAsync(strURL + url).Result;
        }
    }
}
