using System.Collections.Generic;
using RecipeSite.Domain.Entities;

namespace RecipeSite.WebUI.Models
{
    public class RecipeListViewModel
    {
        public IEnumerable<Recipe> Recipe { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string CurrentCategory { get; set; }
    }
}