using System.Net;
using System.Web;


    public class CORSModule : IHttpModule
    {
        public void Dispose() { }

        public void Init(HttpApplication context)
        {
            context.PreSendRequestHeaders += delegate
                                                 {
                                                     //if sending an ajax request using the with credentials flag the Access-Control-Allow-Origin header needs to be the value of the origin. 
                                                     //The wildcard value is not permitted. So instead of specifying a different value for each environment this module creates the header with the origin value. 
                                                     //For more information google CORS.
                                                     string origin = context.Request.Headers["Origin"];
                                                     if (origin != null)
                                                     {
                                                         context.Response.Headers.Add("Access-Control-Allow-Origin", origin);
                                                     }

                                                     if (context.Request.HttpMethod == "OPTIONS")
                                                     {
                                                         
                                                         var response = context.Response;
                                                         response.StatusCode = (int)HttpStatusCode.OK;
                                                     }
                                                 };
        }
    }
