using RecipeSite.Domain.Entities;

namespace RecipeSite.WebUI.Models
{
    public class MealPlanIndexViewModel
    {
        public MealPlan MealPlan { get; set; }
        public string ReturnUrl { get; set; }
    }
}