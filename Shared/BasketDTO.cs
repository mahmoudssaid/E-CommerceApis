namespace Shared
{
    public record BasketDTO
    {
        public string Id { get; init; } // => pK 
        public IEnumerable<BasketItemDTO> Items { get; init; }
    }
}
