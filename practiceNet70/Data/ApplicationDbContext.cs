using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using practiceNet70.Models;

namespace practiceNet70.Data;

public class ApplicationDbContext : IdentityDbContext<User, IdentityRole, string>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<practiceNet70.Models.Movie> Movie { get; set; } = default!;
    public DbSet<practiceNet70.Models.User> User { get; set; } = default!;

}

