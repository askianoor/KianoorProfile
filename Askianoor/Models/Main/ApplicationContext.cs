using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Askianoor.Models
{
    public class ApplicationContext : IdentityDbContext
    {
        public ApplicationContext(DbContextOptions options) : base(options)
        {

        }

        #region User & Dashboard Settings

        public DbSet<DashboardSetting> DashboardSettings { get; set; }

        public DbSet<AppUser> AppUsers { get; set; }

        #endregion

        #region Portfolios

        public DbSet<Portfolio> Portfolios { get; set; }

        public DbSet<PortfolioCategory> PortfolioCategories { get; set; }

        public DbSet<Like> Likes { get; set; }

        #endregion


    }
}
