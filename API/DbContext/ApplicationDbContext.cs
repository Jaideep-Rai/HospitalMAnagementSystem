using API.UpdateUserDb;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace API.Dbcontext
{
    public class ApplicationDbContext : IdentityDbContext<UserField>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(GetConnectionString());
        }
        IConfiguration _configuration;
        public ApplicationDbContext(IConfiguration iconfiguration)
        {
            _configuration = iconfiguration;
        }
        public string GetConnectionString()
        {
            //=======DbConnection For JWT=========//
            string connectionstring = _configuration.GetSection("ConnectionStrings").GetSection("DefaultConnectionLocal").Value;
            return connectionstring;
        }
    }
}
