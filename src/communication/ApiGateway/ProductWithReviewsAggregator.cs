using Newtonsoft.Json.Linq;
using Ocelot.Middleware;
using Ocelot.Multiplexer;

public class ProductWithReviewsAggregator : IDefinedAggregator
{
    public async Task<DownstreamResponse> Aggregate(List<HttpContext> responses)
    {
        var productResponse = responses[0].Items.DownstreamResponse();
        var reviewResponse = responses[1].Items.DownstreamResponse();

        var productContent = await productResponse.Content.ReadAsStringAsync();
        var reviewContent = await reviewResponse.Content.ReadAsStringAsync();

        var productJson = JObject.Parse(productContent);
        var reviewsJson = JArray.Parse(reviewContent);

        var combined = new JObject
        {
            ["product"] = productJson,
            ["reviews"] = reviewsJson
        };

        return new DownstreamResponse(
            new StringContent(combined.ToString(), System.Text.Encoding.UTF8, "application/json"),
            System.Net.HttpStatusCode.OK,
            new List<KeyValuePair<string, IEnumerable<string>>>(),
            "OK"
        );
    }
}
