namespace Domain.Entities
{
    public class CustomerBasket
    {
        public string Id { get; set; } // => pK 
        public IEnumerable<BasketItem> Items { get; set; }
    }
}
