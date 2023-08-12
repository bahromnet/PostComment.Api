namespace Domain.Api.Common;

public abstract class BaseAuditibleEntity : BaseEntity
{
    public DateTime Created { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime LastUpdated { get; set; }
    public string? LastUpdatedBy { get; set; }
}
