using System.Collections.Generic;
using System.Web.Mvc;
using RecipeSite.Domain.Abstract;
using System.Linq;

namespace RecipeSite.WebUI.Controllers
{
    public class NavController : Controller
    {
        private IRecipeRepository repository;

        public NavController(IRecipeRepository repo)
        {
            repository = repo;
        }
        
        public PartialViewResult Menu(string category = null)
        {
            ViewBag.SelectedCategory = category;
            IEnumerable<string> categories = repository.Recipe.Select(x => x.RecipeCategory).Distinct().OrderBy(x => x);
            return PartialView(categories);
        }
    }
}