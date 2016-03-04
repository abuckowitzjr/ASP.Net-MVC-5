using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeSite.Domain.Entities
{
    public class IngredientCategory
    {
        [Key]
        public int IngredientCategoryID { get; set; }
        public string IngredientCategoryName { get; set; }
        public string IngredientCategoryType { get; set; }
    }
}
