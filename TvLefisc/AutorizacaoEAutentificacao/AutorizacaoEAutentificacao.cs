using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace TvLefisc.AutorizacaoEAutentificacao
{
    public class ApplicationUser : IdentityUser
    {
    }
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public IEnumerable<object> Categorias { get; set; }
    }
}
