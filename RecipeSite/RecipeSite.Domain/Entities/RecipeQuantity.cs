using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeSite.Domain.Entities
{
    public class RecipeQuantity
    {
        [Key]
        public int RecipeQuantityID { get; set; }
        public double RecipeQuantityAmount { get; set; }
        public string RecipeQuantityFraction { get; set; }
    }
}
