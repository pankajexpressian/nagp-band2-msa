using System.Net.Http;
using System.Reflection;
using MassTransit;
using MessageBusContracts;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using static System.Net.WebRequestMethods;

namespace ProductService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly HttpClient _apiGatewayHttpClient;
        private readonly IPublishEndpoint _publishEndpoint;

        public ProductsController(IHttpClientFactory httpClientFactory, IPublishEndpoint publishEndpoint)
        {
            _apiGatewayHttpClient = httpClientFactory.CreateClient("APIGatewayClient");
            _publishEndpoint = publishEndpoint;
        }

        [HttpGet]
        public ActionResult<Product> GetProducts()
        {
            Console.WriteLine("==========PRODUCT SERVICE INSTANCE 1============");
            return Ok(products);
        }

        [HttpGet("{id}")]
        public ActionResult<Product> GetProductById(int id)
        {
            var product = products.Where(p => p.Id == id).FirstOrDefault();
            return product == null ? NotFound() : Ok(product);
        }

        [HttpPost]
        public ActionResult<Product> CreateProduct(Product product)
        {
            var maxId = products.Max(p => p.Id) + 1;
            product.Id = maxId;
            products.Add(product);
            return Created();
        }

        [HttpPost("{id}/review")]   //api/products/1/review or gateway/products/1/review
        public async Task<string> CreateProductReview(int id, [FromBody] ProductReviewDto productReviewDto)
        {
            
            //http://localhost:2000/gateway/reviews/products/{id};
            // "/gateway/reviews/products/{everything}"
            var response = await _apiGatewayHttpClient.PostAsJsonAsync($"reviews/products/{id}", productReviewDto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        [HttpPost("{id}/add-review")]
        public async Task<IActionResult> AddReview(int id, [FromBody] ProductReviewDto productReviewDto)
        {
            var reviewAddedEvent = new ReviewAddedEvent
            {
                ProductId = productReviewDto.ProductId,
                Comment = productReviewDto.Comment
            };

            await _publishEndpoint.Publish(reviewAddedEvent);
            return Ok("Event published");
        }


        public static List<Product> products = new List<Product>() {
            new Product(){
            Id = 2,
            Name="Samsung S23"
            },
            new Product()
            {
                Id = 1,
                Name="OnePlus 3T"
            }
        };

    }




    public class Product()
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

}
