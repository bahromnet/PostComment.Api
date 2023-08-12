namespace Domain.Api.Token;

public class UserRefreshToken
{
    public Guid Id { get; set; }
    public string RefreshToken { get; set; }
    public string UserName { get; set; }
    public DateTime ExpiresTime { get; set; }
}
