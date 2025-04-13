using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ProductService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        [HttpGet]
        public ActionResult<Product> GetProducts()
        {
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
            var maxId=products.Max(p => p.Id)+1;
            product.Id = maxId;
            products.Add(product);
            return Created();
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
