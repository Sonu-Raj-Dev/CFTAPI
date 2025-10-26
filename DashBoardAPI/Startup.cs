namespace DashBoardAPI
{
    public class Startup
    {
        #region Properties
        //headers to add
        private static List<KeyValuePair<string, string>> InclusionHeaderList = new List<KeyValuePair<string, string>>() {
                    new KeyValuePair<string, string>("Content-Security-Policy", "default-src https: 'unsafe-inline' 'unsafe-eval'; script-src https: 'unsafe-inline' 'unsafe-eval'; style-src https: 'unsafe-inline'; img-src https: data:; font-src https:; object-src 'none'; frame-src 'none'; upgrade-insecure-requests;"),
                    new KeyValuePair<string, string>("X-Content-Type-Options", "nosniff"),
                    new KeyValuePair<string, string>("X-Xss-Protection", "1; mode=block"),
                    new KeyValuePair<string, string>("X-Frame-Options", "SAMEORIGIN"),
                    new KeyValuePair<string, string>("Strict-Transport-Security", "max-age=31536000;"),
                    new KeyValuePair<string, string>("X-Content-Type", "nosniff"),
                    new KeyValuePair<string, string>("Access-Control-Allow-Headers", "SAMEORIGIN"),
                    new KeyValuePair<string, string>("Access-Control-Allow-Methods", "GET,POST"),
                    new KeyValuePair<string, string>("Feature-Policy", "accelerometer 'none'; camera 'none'; microphone 'none'"),
                    new KeyValuePair<string, string>("Referrer-Policy", "no-referrer, strict-origin-when-cross-origin"),
                    new KeyValuePair<string, string>("Cross-Origin-Opener-Policy", "same-origin"),
                    new KeyValuePair<string, string>("Cross-Origin-Resource-Policy", "same-origin"),
                    new KeyValuePair<string, string>("Cache-Control", "no-cache, must-revalidate, no-store"),
                    new KeyValuePair<string, string>("Access-Control-Allow-Credentials", "true")
                };

        //headers to remove
        public static List<string> ExclusionHeaderList = new List<string>() {
                    "X-Powered-By",
                    "Server"
                };

        //CORS - allowed domains
        private static string[] AllowedDomains = new string[] {
                    "http://localhost:3000",
                    "http://localhost:8080",
                    "http://localhost:6178",
                    "https://localhost:6178",
                    "https://CFTManagement.somee.com/",
                    "https://CFTManagement.somee.com",
                    "https://CFTManagement.somee.com/api"         
                };

        //CORS - allowed methods
        public static string[] AllowedMethods = new string[] { "POST", "GET" };
        #endregion

        #region Methods
        public static List<KeyValuePair<string, string>> GetInclusiveHeaders(HttpContext context)
        {
            if (!InclusionHeaderList.Any(ihl => ihl.Key.ToLower().Equals("Access-Control-Allow-Origin".ToLower())))
            {
                InclusionHeaderList.Add(new KeyValuePair<string, string>("Access-Control-Allow-Origin", Environment.GetEnvironmentVariable("WebOrigin")));
            }
            return InclusionHeaderList;
        }

        public static string[] GetAllowedDomains()
        {
            return AllowedDomains.Select(ad => ad.ToLower()).ToArray();
        }

        public static string GetBaseUrl(HttpContext context)
        {
            //string baseUrl = $"{context.Request.Scheme}://{context.Request.Host.Value}{context.Request.PathBase.Value}".ToLower();
            //[BUG] hardcoding https bcoz Request.Scheme returns http even if https is enabled.
            string baseUrl = $"https://{context.Request.Host.Value}{context.Request.PathBase.Value}".ToLower();
            return baseUrl;
        }

        public static Stream GetStreamFromString(string str)
        {
            var newStream = new MemoryStream();
            var sw = new StreamWriter(newStream);
            sw.Write(str);
            sw.Flush();
            return newStream;
        }

        #endregion

    }

}
