using System.Linq;
using System.Web.Mvc;
using RecipeSite.Domain.Abstract;
using RecipeSite.Domain.Entities;

namespace RecipeSite.WebUI.Controllers
{
    public class AdminController : Controller
    {
        private IRecipeRepository repository;

        public AdminController(IRecipeRepository repo)
        {
            repository = repo;
        }

        public ViewResult Index()
        {
            return View(repository.Recipe);
        }

        public ViewResult Edit(int recipeID)
        {
            Recipe recipe = repository.Recipe.FirstOrDefault(r => r.RecipeID == recipeID);
            return View(recipe);
        }
    }
}