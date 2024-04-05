
class ResponseBuilder
{
    private ResponseHTTP? response;

    public void build(int statusCode, Dictionary<string, string> headers, Object body)
    {
        this.response = new ResponseHTTP();
        this.response.StatusCode = statusCode;
        this.response.Headers = headers;
        this.response.Body = body;
    }

    public ResponseHTTP? GetResponse()
    {
        return this.response;
    }
}
