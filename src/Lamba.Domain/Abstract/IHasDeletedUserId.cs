namespace Lamba.Domain.Abstract
{
    public interface IHasDeletedUserId<TKey>
    {
        public TKey DeletedUserId { get; set; }
    }
}
