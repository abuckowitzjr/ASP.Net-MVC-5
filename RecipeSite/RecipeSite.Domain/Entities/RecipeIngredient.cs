using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeSite.Domain.Entities
{
    public class RecipeIngredient
    {
        [Key]
        public int RecipeIngredientID { get; set; }
        public int IngredientCategoryID { get; set; }
        public string IngredientName { get; set; }
        public string IngredientNamePlural { get; set; }
    }
}
