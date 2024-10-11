﻿using GymInnowise.GymService.Persistence.Data.Configuration;
using GymInnowise.GymService.Persistence.Models.Entities;
using GymInnowise.Shared.Gym.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;

namespace GymInnowise.GymService.Persistence.Data
{
    public class GymServiceDbContext : DbContext
    {
        public DbSet<GymEntity> Gyms { get; set; }
        public DbSet<GymEventEntity> GymEvents { get; set; }

        public GymServiceDbContext(DbContextOptions<GymServiceDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var gymConfigurator = new GymEntityTypeConfiguration();
            var gymEventConfigurator = new GymEventEntityTypeConfiguration();
            gymConfigurator.Configure(modelBuilder.Entity<GymEntity>());
            gymEventConfigurator.Configure(modelBuilder.Entity<GymEventEntity>());
        }
    }
}