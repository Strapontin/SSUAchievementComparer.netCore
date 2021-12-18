using Microsoft.EntityFrameworkCore;
using SSUAchievementComparer.Core;
using SSUAchievementComparer.Core.Entities.DB;
using System;
using System.Collections.Generic;
using System.Text;

namespace SSUAchievementComparer.Data
{
    public class SSUAchievementComparerDbContext : DbContext
    {
        public SSUAchievementComparerDbContext(DbContextOptions<SSUAchievementComparerDbContext> options) : base(options)
        {

        }

        public DbSet<GameDetailsDb> GameDetailsDb { get; set; }
        public DbSet<PlayerDetailsDb> PlayerDetailsDb { get; set; }
    }
}
