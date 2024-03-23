using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ArtsyApp.Models;

namespace ArtsyApp.Data
{
    public class ArtsyAppContext : DbContext
    {
        public ArtsyAppContext (DbContextOptions<ArtsyAppContext> options)
            : base(options)
        {
        }

        public DbSet<ArtsyApp.Models.ArtWorksModel> ArtWorksModel { get; set; } = default!;
    }
}
