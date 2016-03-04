using System.Web.Mvc;
using RecipeSite.Domain.Entities;

namespace RecipeSite.WebUI.Infrastructure.Binders
{
    public class MealPlanModelBinder : IModelBinder
    {
        private const string sessionKey = "MealPlan";

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            // get the MealPlan from the session

            MealPlan mealplan = null;

            if (controllerContext.HttpContext.Session != null)
            {
                mealplan = (MealPlan) controllerContext.HttpContext.Session[sessionKey];
            }

            // create the MealPlan if there wasn't one in the session data
            if (mealplan == null)
            {
                mealplan = new MealPlan();
                if (controllerContext.HttpContext.Session != null)
                {
                    controllerContext.HttpContext.Session[sessionKey] = mealplan;
                }
            }

            // return the mealplan
            return mealplan;
        }
    }
}