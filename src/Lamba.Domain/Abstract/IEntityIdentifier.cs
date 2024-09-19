namespace Lamba.Domain.Abstract
{
    public interface IEntityIdentifier<TKey> where TKey : struct
    {
        public TKey Id { get; set; }
    }
}
