namespace Lamba.Domain.Abstract
{
    internal interface IHasDeletedAt
    {
        public DateTime? DeletedAt { get; set; }
    }
}
