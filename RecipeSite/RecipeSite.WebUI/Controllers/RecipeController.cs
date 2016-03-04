using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RecipeSite.Domain.Abstract;
using RecipeSite.Domain.Entities;
using RecipeSite.WebUI.Models;

namespace RecipeSite.WebUI.Controllers
{
    public class RecipeController : Controller
    {
        private IRecipeRepository repository;
        public int PageSize = 4;

        public RecipeController(IRecipeRepository recipeRepository)
        {
            this.repository = recipeRepository;
        }

        public ViewResult List(string category, int page = 1)
        {
            RecipeListViewModel ViewModel = new RecipeListViewModel
            {
                Recipe = repository.Recipe.Where(r => category == null || r.RecipeCategory == category).OrderBy(p => p.RecipeID).Skip((page - 1)*PageSize).Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = PageSize,
                    ItemsPerPage = PageSize,
                    TotalItems = category == null ?
                        repository.Recipe.Count() : repository.Recipe.Where(e => e.RecipeCategory == category).Count()
                },
                CurrentCategory = category
            };
            return View(ViewModel);
        }
    }
}