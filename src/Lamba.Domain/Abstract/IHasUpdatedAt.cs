namespace Lamba.Domain.Abstract
{
    internal interface IHasUpdatedAt
    {
        public DateTime? UpdatedAt { get; set; }
    }
}
