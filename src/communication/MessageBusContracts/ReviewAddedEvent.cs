namespace MessageBusContracts
{
    public class ReviewAddedEvent
    {
        public int ProductId { get; set; }
        public string Comment { get; set; }
    }
}
