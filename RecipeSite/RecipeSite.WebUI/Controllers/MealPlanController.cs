using System.Linq;
using System.Web.Mvc;
using RecipeSite.Domain.Abstract;
using RecipeSite.Domain.Entities;
using RecipeSite.WebUI.Models;

namespace RecipeSite.WebUI.Controllers
{
    public class MealPlanController : Controller
    {
        private IRecipeRepository repository;

        public MealPlanController(IRecipeRepository repo)
        {
            repository = repo;
        }

        public ViewResult Index(MealPlan mealplan, string returnUrl)
        {
            return View(new MealPlanIndexViewModel {ReturnUrl = returnUrl, MealPlan = mealplan});
        }

        public RedirectToRouteResult AddToMealPlan(MealPlan mealplan, int recipeID, string returnUrl)
        {
            Recipe recipe = repository.Recipe.FirstOrDefault(r => r.RecipeID == recipeID);

            if (recipe != null)
            {
                mealplan.AddRecipe(recipe, 1);
            }
            return RedirectToAction("Index", new {returnUrl});
        }

        public RedirectToRouteResult RemoveFromMealPlan(MealPlan mealplan, int recipeID, string returnUrl)
        {
            Recipe recipe = repository.Recipe.FirstOrDefault(r => r.RecipeID == recipeID);

            if (recipe != null)
            {
                mealplan.RemoveRecipe(recipe);
            }
            return RedirectToAction("Index", new {returnUrl});
        }

        private MealPlan GetMealPlan()
        {
            MealPlan mealplan = (MealPlan) Session["MealPlan"];
            if (mealplan == null)
            {
                mealplan = new MealPlan();
                Session["MealPlan"] = mealplan;
            }
            return mealplan;
        }

        public PartialViewResult Summary(MealPlan mealplan)
        {
            return PartialView(mealplan);
        }
    }
}