using System.Net;

namespace docs_garage_api.Modal
{
    public class Response
    {
        public HttpStatusCode status { get; set; }
        public string message { get; set; }
        public object data { get; set; }
    }
}
