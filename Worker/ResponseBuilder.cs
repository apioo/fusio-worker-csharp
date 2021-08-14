
using System.Collections.Generic;
using System.Text.Json;

namespace FusioWorker
{
    public class ResponseBuilder
    {
        public Response Build(int statusCode, Dictionary<string, string> headers, object body)
        {
            Response response = new Response
            {
                StatusCode = statusCode,
                Headers = headers,
                Body = JsonSerializer.Serialize(body)
            };

            return response;
        }
    }
}
