namespace Lamba.Identity.Application.Common.Accessors
{
    public interface ICurrentUserAccessor
    {
        Guid? GetId();
        string? GetUsername();
        string? GetRole();
    }
}
