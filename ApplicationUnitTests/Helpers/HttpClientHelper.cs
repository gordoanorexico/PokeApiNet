using Moq.Protected;
using Moq;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net;

namespace ApplicationUnitTests.Helpers;

public class HttpClientHelper
{
    public static Mock<HttpMessageHandler> GetResults<T>(T response, HttpStatusCode statusCode)
    {
        var mockResponse = new HttpResponseMessage()
        {
            Content = new StringContent(JsonConvert.SerializeObject(response)),
            StatusCode = statusCode
        };

        mockResponse.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        var mockHandler = new Mock<HttpMessageHandler>();

        mockHandler.Protected().Setup<Task<HttpResponseMessage>>
            (
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            ).ReturnsAsync(mockResponse);

        return mockHandler;
    }
}
