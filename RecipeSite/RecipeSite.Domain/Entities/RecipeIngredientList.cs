using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeSite.Domain.Entities
{
    public class RecipeIngredientList
    {
        [Key]
        public int RecipeID { get; set; }
        public int RecipeIngredientID { get; set; }
        public int RecipeMeasurementID { get; set; }
        public int RecipeQuantityID { get; set; }
    }
}
