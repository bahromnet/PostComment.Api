namespace Application.Api.Common.Interfaces
{
    public interface ICurrentUserService
    {
        public string UserName { get; }
        public Guid UserId { get; }
    }
}
