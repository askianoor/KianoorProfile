using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Askianoor.Models.Main;

namespace Askianoor.Models
{
    public class ApplicationContext : IdentityDbContext
    {
        public ApplicationContext(DbContextOptions options) : base(options)
        {

        }

        #region User & Dashboard Settings & Main Models

        public DbSet<DashboardSetting> DashboardSettings { get; set; }

        public DbSet<AppUser> AppUsers { get; set; }

        public DbSet<Navbar> Navbars { get; set; }

        public DbSet<SocialNetwork> SocialNetworks { get; set; }

        public DbSet<Education> Educations { get; set; }

        public DbSet<Experience> Experiences { get; set; }

        #endregion

        #region Portfolios

        public DbSet<Portfolio> Portfolios { get; set; }

        public DbSet<PortfolioCategory> PortfolioCategories { get; set; }

        public DbSet<LikePortfolio> LikePortfolios { get; set; }

        public DbSet<Askianoor.Models.Main.Skill> Skill { get; set; }


        #endregion

    }
}
