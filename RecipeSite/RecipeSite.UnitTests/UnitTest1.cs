using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Security.Cryptography;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ninject.Planning;
using RecipeSite.Domain.Abstract;
using RecipeSite.Domain.Entities;
using RecipeSite.WebUI.Controllers;
using RecipeSite.WebUI.Models;
using RecipeSite.WebUI.HtmlHelpers;

namespace RecipeSite.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Can_Paginate()
        {
            // Arrange
            Mock<IRecipeRepository> mock = new Mock<IRecipeRepository>();
            mock.Setup(m => m.Recipe).Returns(new Recipe[]
            {
                new Recipe {RecipeID = 1, RecipeName = "Test1", RecipeInstructions = "Test1"},
                new Recipe {RecipeID = 2, RecipeName = "Test2", RecipeInstructions = "Test2"},
                new Recipe {RecipeID = 3, RecipeName = "Test3", RecipeInstructions = "Test3"},
                new Recipe {RecipeID = 4, RecipeName = "Test4", RecipeInstructions = "Test4"},
                new Recipe {RecipeID = 5, RecipeName = "Test5", RecipeInstructions = "Test5"}
            });

            //create controller and make the page size 3 items
            RecipeController controller = new RecipeController(mock.Object);
            controller.PageSize = 3;

            // Act
            RecipeListViewModel result = (RecipeListViewModel) controller.List(null, 2).Model;

            // Assert
            Recipe[] recipeArray = result.Recipe.ToArray();
            Assert.IsTrue(recipeArray.Length == 2);
            Assert.AreEqual(recipeArray[0].RecipeName, "Test4");
            Assert.AreEqual(recipeArray[1].RecipeName, "Test5");
        }

        [TestMethod]
        public void Can_Generate_Page_Links()
        {
            // Arrange - define an HTML helper - we need to do this in order to apply the extension method
            HtmlHelper myHelper = null;

            // Arrange - create PageInfo data
            PagingInfo pagingInfo = new PagingInfo
            {
                CurrentPage = 2,
                TotalItems = 28,
                ItemsPerPage = 10
            };

            // Arrange - set up the delegate using a lambda expression
            Func<int, string> pageUrlDelegate = i => "Page" + i;

            // Act
            MvcHtmlString result = myHelper.PageLinks(pagingInfo, pageUrlDelegate);

            // Assert
            Assert.AreEqual(@"<a class=""btn btn-default"" href=""Page1"">1</a>"
                + @"<a class=""btn btn-default btn-primary selected"" href=""Page2"">2</a>"
                + @"<a class=""btn btn-default"" href=""Page3"">3</a>", result.ToString());
        }

        [TestMethod]
        public void Can_Send_Pagination_View_Model()
        {
            // Arrange
            Mock<IRecipeRepository> mock = new Mock<IRecipeRepository>();
            mock.Setup(m => m.Recipe).Returns(new Recipe[]
            {
                new Recipe {RecipeID = 1, RecipeName = "R1"},
                new Recipe {RecipeID = 2, RecipeName = "R2"},
                new Recipe {RecipeID = 3, RecipeName = "R3"},
                new Recipe {RecipeID = 4, RecipeName = "R4"},
                new Recipe {RecipeID = 5, RecipeName = "R5"}
            });

            // Arrange
            RecipeController controller = new RecipeController(mock.Object);
            controller.PageSize = 3;

            // Act
            RecipeListViewModel result = (RecipeListViewModel) controller.List(null, 2).Model;

            // Assert
            PagingInfo pageInfo = result.PagingInfo;
            Assert.AreNotEqual(pageInfo.CurrentPage, 2);
            Assert.AreEqual(pageInfo.ItemsPerPage, 3);
            Assert.AreEqual(pageInfo.TotalItems, 5);
            Assert.AreEqual(pageInfo.TotalPages, 2);
        }

        [TestMethod]
        public void Can_Filter_Recipes()
        {
            // Arrange - create the mock repository
            Mock<IRecipeRepository> mock = new Mock<IRecipeRepository>();
            mock.Setup(m => m.Recipe).Returns(new Recipe[]
            {
                new Recipe {RecipeID = 1, RecipeName = "R1", RecipeCategory = "Cat1"},
                new Recipe {RecipeID = 2, RecipeName = "R2", RecipeCategory = "Cat2"},
                new Recipe {RecipeID = 3, RecipeName = "R3", RecipeCategory = "Cat1"},
                new Recipe {RecipeID = 4, RecipeName = "R4", RecipeCategory = "Cat2"},
                new Recipe {RecipeID = 5, RecipeName = "R5", RecipeCategory = "Cat3"},
            });

            // Arrange - create a controller and make the page size 3 items
            RecipeController controller = new RecipeController(mock.Object);
            controller.PageSize = 3;

            // Action
            Recipe[] result = ((RecipeListViewModel) controller.List("Cat2", 1).Model).Recipe.ToArray();

            // Assert
            Assert.AreEqual(result.Length, 2);
            Assert.IsTrue(result[0].RecipeName == "R2" && result[0].RecipeCategory == "Cat2");
            Assert.IsTrue(result[1].RecipeName == "R4" && result[1].RecipeCategory == "Cat2");
        }

        [TestMethod]
        public void Can_Create_Categories()
        {
            // Arrange - create the mock repository
            Mock<IRecipeRepository> mock = new Mock<IRecipeRepository>();
            mock.Setup(m => m.Recipe).Returns(new Recipe[]
            {
                new Recipe {RecipeID = 1, RecipeName = "R1", RecipeCategory = "Apples"},
                new Recipe {RecipeID = 2, RecipeName = "R2", RecipeCategory = "Apples"},
                new Recipe {RecipeID = 3, RecipeName = "R3", RecipeCategory = "Plums"},
                new Recipe {RecipeID = 4, RecipeName = "R4", RecipeCategory = "Oranges"},
            });

            // Arrange - create the controller
            NavController target = new NavController(mock.Object);

            // Act - get the set of categories
            string[] result = ((IEnumerable<string>) target.Menu().Model).ToArray();

            // Assert
            Assert.AreEqual(result.Length, 3);
            Assert.AreEqual(result[0], "Apples");
            Assert.AreEqual(result[1], "Oranges");
            Assert.AreEqual(result[2], "Plums");
        }

        [TestMethod]
        public void Indicates_Selected_Category()
        {
            // Arrange - create the mock repository
            Mock<IRecipeRepository> mock = new Mock<IRecipeRepository>();
            mock.Setup(m => m.Recipe).Returns(new Recipe[]
            {
                new Recipe {RecipeID = 1, RecipeName = "R1", RecipeCategory = "Apples"},
                new Recipe {RecipeID = 4, RecipeName = "R2", RecipeCategory = "Oranges"},
            });

            // Arrange - create the controller
            NavController target = new NavController(mock.Object);

            // Arrange - define the category as selected
            string categoryToSelect = "Apples";

            // Action
            string result = target.Menu(categoryToSelect).ViewBag.SelectedCategory;

            // Assert
            Assert.AreEqual(categoryToSelect, result);
        }

        [TestMethod]
        public void Generate_Category_Specific_Recipe_Count()
        {
            // Arrange - create the mock repository
            Mock<IRecipeRepository> mock = new Mock<IRecipeRepository>();
            mock.Setup(m => m.Recipe).Returns(new Recipe[]
            {
                new Recipe {RecipeID = 1, RecipeName = "R1", RecipeCategory = "Cat1"},
                new Recipe {RecipeID = 2, RecipeName = "R2", RecipeCategory = "Cat2"},
                new Recipe {RecipeID = 3, RecipeName = "R3", RecipeCategory = "Cat1"},
                new Recipe {RecipeID = 4, RecipeName = "R4", RecipeCategory = "Cat2"},
                new Recipe {RecipeID = 5, RecipeName = "R5", RecipeCategory = "Cat3"},
            });

            // Arrange - create the controller and make the page size 3 items
            RecipeController target = new RecipeController(mock.Object);
            target.PageSize = 3;

            // Action - test the recipe counts for different categories
            int res1 = ((RecipeListViewModel) target.List("Cat1").Model).PagingInfo.TotalItems;
            int res2 = ((RecipeListViewModel)target.List("Cat2").Model).PagingInfo.TotalItems;
            int res3 = ((RecipeListViewModel)target.List("Cat3").Model).PagingInfo.TotalItems;
            int resAll = ((RecipeListViewModel)target.List(null).Model).PagingInfo.TotalItems;

            // Assert
            Assert.AreEqual(res1, 2);
            Assert.AreEqual(res2, 2);
            Assert.AreEqual(res3, 1);
            Assert.AreEqual(resAll, 5);
        }

        [TestMethod]
        public void Can_Add_New_Lines()
        {
            // Arrange - create some test recipes
            Recipe r1 = new Recipe {RecipeID = 1, RecipeName = "R1"};
            Recipe r2 = new Recipe { RecipeID = 2, RecipeName = "R2" };

            // Arrange - create a new mealplan
            MealPlan target = new MealPlan();

            // Act
            target.AddRecipe(r1, 1);
            target.AddRecipe(r2, 1);
            MealPlanLine[] results = target.Lines.ToArray();

            // Assert
            Assert.AreEqual(results.Length, 2);
            Assert.AreEqual(results[0].Recipe, r1);
            Assert.AreEqual(results[1].Recipe, r2);
        }

        [TestMethod]
        public void Can_Remove_Line()
        {
            // Arrange - create some test recipes
            Recipe r1 = new Recipe { RecipeID = 1, RecipeName = "R1" };
            Recipe r2 = new Recipe { RecipeID = 2, RecipeName = "R2" };
            Recipe r3 = new Recipe { RecipeID = 3, RecipeName = "R3" };

            // Arrange - create a new mealplan
            MealPlan target = new MealPlan();

            // Arrange - add some recipes to the mealplan
            target.AddRecipe(r1, 1);
            target.AddRecipe(r2, 1);
            target.AddRecipe(r3, 1);

            // Act
            target.RemoveRecipe(r2);

            // Assert
            Assert.AreEqual(target.Lines.Where(c => c.Recipe == r2).Count(), 0);
            Assert.AreEqual(target.Lines.Count(), 2);
        }

        [TestMethod]
        public void Can_Clear_Contents()
        {
            // Arrange - create some test recipes
            Recipe r1 = new Recipe { RecipeID = 1, RecipeName = "R1" };
            Recipe r2 = new Recipe { RecipeID = 2, RecipeName = "R2" };

            // Arrange - create a new mealplan
            MealPlan target = new MealPlan();

            // Arrange - add some recipes
            target.AddRecipe(r1, 1);
            target.AddRecipe(r2, 1);

            // Act - reset the mealplan
            target.Clear();

            // Assert
            Assert.AreEqual(target.Lines.Count(), 0);
        }

        [TestMethod]
        public void Can_Add_To_MealPlan()
        {
            // Arrange - create the mock repository
            Mock<IRecipeRepository> mock = new Mock<IRecipeRepository>();
            mock.Setup(m => m.Recipe).Returns(new Recipe[]
            {
                new Recipe {RecipeID = 1, RecipeName = "R1", RecipeCategory = "Apples"},
            }.AsQueryable());

            // Arrange - create a MealPlan
            MealPlan mealplan = new MealPlan();

            // Arrange - create the controller
            MealPlanController target = new MealPlanController(mock.Object);

            // Action - add a recipe to the mealplan
            target.AddToMealPlan(mealplan, 1, null);

            // Assert
            Assert.AreEqual(mealplan.Lines.Count(), 1);
            Assert.AreEqual(mealplan.Lines.ToArray()[0].Recipe.RecipeID, 1);
        }

        [TestMethod]
        public void Adding_Product_To_MealPlan_Goes_To_MealPlan_Screen()
        {
            // Arrange - create the mock repository
            Mock<IRecipeRepository> mock = new Mock<IRecipeRepository>();
            mock.Setup(m => m.Recipe).Returns(new Recipe[]
            {
                new Recipe {RecipeID = 1, RecipeName = "R1", RecipeCategory = "Apples"},
            }.AsQueryable());

            // Arrange - create a MealPlan
            MealPlan mealplan = new MealPlan();

            // Arrange - create the controller
            MealPlanController target = new MealPlanController(mock.Object);

            // Action - add a recipe to the mealplan
            RedirectToRouteResult result = target.AddToMealPlan(mealplan, 2, "myUrl");

            // Assert
            Assert.AreEqual(result.RouteValues["action"], "Index");
            Assert.AreEqual(result.RouteValues["returnUrl"], "myUrl");
        }

        [TestMethod]
        public void Can_View_MealPlan_Contents()
        {
            // Arrange - create a MealPlan
            MealPlan mealplan = new MealPlan();

            // Arrange - create the controller
            MealPlanController target = new MealPlanController(null);

            // Act - call the Index action method
            MealPlanIndexViewModel result = (MealPlanIndexViewModel) target.Index(mealplan, "myUrl").ViewData.Model;

            // Assert
            Assert.AreSame(result.MealPlan, mealplan);
            Assert.AreEqual(result.ReturnUrl, "myUrl");
        }
    }
}
