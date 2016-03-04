using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipeSite.Domain.Entities;

namespace RecipeSite.Domain.Abstract
{
    public interface IRecipeQuantityRepository
    {
        IEnumerable<RecipeQuantity> RecipeQuantity { get; } 
    }
}
