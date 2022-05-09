using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using FilmsActors.Models;

namespace FilmsActors.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<FilmsActors.Models.Film> Film { get; set; }
        public DbSet<FilmsActors.Models.Actor> Actor { get; set; }
        public DbSet<FilmsActors.Models.FilmsActorsMod> FilmsActorsMod { get; set; }
    }
}
