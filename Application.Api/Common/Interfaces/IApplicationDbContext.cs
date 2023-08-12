using Domain.Api.Entities;
using Domain.Api.IdentityEntity;
using Microsoft.EntityFrameworkCore;

namespace Application.Api.Common.Interfaces;

public interface IApplicationDbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Permission> Permissions { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
