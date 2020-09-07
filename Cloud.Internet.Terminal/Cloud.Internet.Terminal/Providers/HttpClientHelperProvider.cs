namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Providers
{
    public class HttpClientHelperProvider : SAPRESSC.Core.Api.Connect.Providers.Facade.IHttpClientHelperProviderFacade
    {
        //public const string CLOUD_SERVER_BASE_URI = "http://localhost:5000";
        //public const string CLOUD_SERVER_BASE_URI = "http://tu.tvcamaguey.icrt.cu";
        //public const string CLOUD_SERVER_BASE_URI = "http://192.168.200.202";
        public const string CLOUD_SERVER_BASE_URI = "http://localhost";

        private static HttpClientHelperProvider instance;

        public static HttpClientHelperProvider Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new HttpClientHelperProvider();
                }

                return instance;
            }
        }

        public HttpClientHelperProvider()
        {
            HttpClientHelper = new SAPRESSC.Core.Api.Connect.Helpers.HttpClientHelper(string.Format("{0}/api/", CLOUD_SERVER_BASE_URI), () => AuthorizeHeader, null);
        }

        public SAPRESSC.Core.Api.Connect.Helpers.HttpClientHelper HttpClientHelper { get; private set; }

        public System.Collections.Generic.KeyValuePair<string, string> AuthorizeHeader
        {
            get
            {
                if (!Managers.SessionManager.Instance.IsAuthenticated)
                {
                    return new System.Collections.Generic.KeyValuePair<string, string>();
                }
                else
                {
                    return new System.Collections.Generic.KeyValuePair<string, string>(Managers.SessionManager.Instance.AuthenticationType, Managers.SessionManager.Instance.AuthenticationToken);
                }
            }
        }
    }
}
