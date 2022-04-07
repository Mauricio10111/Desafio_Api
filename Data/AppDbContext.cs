using System;
using Desafio_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Desafio_Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<DiagonalSumModel> DiagonalSumModel {get; set;}
        public DbSet<VeryBigSumModel> VeryBigSumModels {get; set;}

        internal void Get()
        {
            throw new NotImplementedException();
        }
    }
}