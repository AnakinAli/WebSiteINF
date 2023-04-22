using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebSiteINF.Models;

namespace WebSiteINF.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<WebSiteINF.Models.Ad> Ad { get; set; } = default!;
        public DbSet<WebSiteINF.Models.AboutUs> AboutUs { get; set; } = default!;
        public DbSet<WebSiteINF.Models.Service> Service { get; set; } = default!;
        public DbSet<WebSiteINF.Models.SubjectArea> SubjectArea { get; set; } = default!;
    }
}