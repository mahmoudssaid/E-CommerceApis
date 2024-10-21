namespace Domain.Entities.Basket
{
    public abstract class BaseEntity<TKey>
    {
        public TKey Id { get; set; }
    }
}
