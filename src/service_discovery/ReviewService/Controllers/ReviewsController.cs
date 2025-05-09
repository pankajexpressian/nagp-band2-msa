using Microsoft.AspNetCore.Mvc;

namespace ReviewService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewsController : ControllerBase
    {

        [HttpGet("products/{productId}")]
        public ActionResult<Review> GetProductReviews(int productId)
        {
            var productReviews = reviews.Where(r => r.ProductId == productId).ToList();
            return productReviews != null ? Ok(productReviews) : NotFound();
        }


        public static List<Review> reviews = new List<Review>() {
            new Review()
            {
                Id = 1,
                ProductId = 1,
                Comment="OnePlus 3T is a falgship."
            },new Review()
            {
                Id = 2,
                ProductId = 1,
                Comment="OnePlus 3T is value for money."
            },
            new Review()
            {
                Id = 3,
                ProductId = 2,
                Comment="Samsung is a camera killer."
            },new Review()
            {
                Id = 4,
                ProductId = 2,
                Comment="Samsung jindabad tha, hai aur rahega.."
            }
        };


    }

    public class Review
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Comment { get; set; }
    }
}
