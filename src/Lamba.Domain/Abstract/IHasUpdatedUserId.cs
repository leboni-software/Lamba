namespace Lamba.Domain.Abstract
{
    public interface IHasUpdatedUserId<TKey>
    {
        public TKey UpdatedUserId { get; set; }
    }
}
