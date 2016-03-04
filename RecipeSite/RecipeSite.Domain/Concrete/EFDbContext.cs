using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipeSite.Domain.Entities;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Runtime.Remoting;

namespace RecipeSite.Domain.Concrete
{
    public class EFDbContext : DbContext
    {
        public EFDbContext() : base("EFDbContext")
        {
            
        }

        public DbSet<Customer> Customer { get; set; }
        public DbSet<IngredientCategory> IngredientCategory { get; set; }
        public DbSet<Recipe> Recipe { get; set; }
        public DbSet<RecipeIngredient> RecipeIngredient { get; set; }
        public DbSet<RecipeIngredientList> RecipeIngredientList { get; set; }
        public DbSet<RecipeMeasurement> RecipeMeasurement { get; set; }
        public DbSet<RecipeQuantity> RecipeQuantity { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
