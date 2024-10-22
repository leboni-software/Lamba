namespace Lamba.Domain.Abstract
{
    public interface IHasDeletedAt
    {
        public DateTime? DeletedAt { get; set; }
    }
}
