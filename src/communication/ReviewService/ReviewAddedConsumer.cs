using MassTransit;
using MessageBusContracts;
using ReviewService.Controllers;

public class ReviewAddedConsumer : IConsumer<ReviewAddedEvent>
{
    public Task Consume(ConsumeContext<ReviewAddedEvent> context)
    {
        var data = context.Message;
        Console.WriteLine($"=========[ReviewService] Review for Product {data.ProductId}: {data.Comment}========");

        var review = new Review()
        {
            ProductId = data.ProductId,
            Comment = data.Comment
        };

        ReviewsController.AddReview(review);
        return Task.CompletedTask;
    }
}
