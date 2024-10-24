namespace Lamba.Domain.Abstract
{
    public interface IHasCreatedUserId<TKey>
    {
        public TKey CreatedUserId { get; set; }
    }
}
