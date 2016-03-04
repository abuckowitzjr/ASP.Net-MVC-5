using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace RecipeSite.Domain.Entities
{
    public class Recipe
    {
        [HiddenInput(DisplayValue = false)]
        [Key]
        public int RecipeID { get; set; }
        public int CustomerID { get; set; }

        public string RecipeName { get; set; }
        public string RecipeCategory { get; set; }

        [DataType(DataType.MultilineText)]
        public string RecipeInstructions { get; set; }
    }
}
