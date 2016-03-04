using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RecipeSite.Domain.Abstract;
using RecipeSite.Domain.Entities;
using RecipeSite.WebUI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace RecipeSite.UnitTests
{
    [TestClass]
    public class AdminTests
    {
        [TestMethod]
        public void Index_Contains_All_Recipes()
        {
            // Arrange - create the mock repository
            Mock<IRecipeRepository> mock = new Mock<IRecipeRepository>();
            mock.Setup(m => m.Recipe).Returns(new Recipe[]
            {
                new Recipe {RecipeID = 1, RecipeName = "R1"},
                new Recipe {RecipeID = 2, RecipeName = "R2"},
                new Recipe {RecipeID = 3, RecipeName = "R3"},
            });

            // Arrange - create a controller
            AdminController target = new AdminController(mock.Object);

            // Action
            Recipe[] result = ((IEnumerable<Recipe>) target.Index().ViewData.Model).ToArray();

            // Assert
            Assert.AreEqual(result.Length, 3);
            Assert.AreEqual("R1", result[0].RecipeName);
            Assert.AreEqual("R2", result[1].RecipeName);
            Assert.AreEqual("R3", result[2].RecipeName);  
        }

        [TestMethod]
        public void Can_Edit_Recipe()
        {
            // Arrange - create the mock repository
            Mock<IRecipeRepository> mock = new Mock<IRecipeRepository>();
            mock.Setup(m => m.Recipe).Returns(new Recipe[]
            {
                new Recipe {RecipeID = 1, RecipeName = "R1"},
                new Recipe {RecipeID = 2, RecipeName = "R2"},
                new Recipe {RecipeID = 3, RecipeName = "R3"},
            });

            // Arrange - create a controller
            AdminController target = new AdminController(mock.Object);

            // Act
            Recipe r1 = target.Edit(1).ViewData.Model as Recipe;
            Recipe r2 = target.Edit(2).ViewData.Model as Recipe;
            Recipe r3 = target.Edit(3).ViewData.Model as Recipe;

            // Assert
            Assert.AreEqual( 1, r1.RecipeID);
            Assert.AreEqual( 2, r2.RecipeID);
            Assert.AreEqual( 3, r3.RecipeID);
        }

        [TestMethod]
        public void Cannot_Edit_Nonexistent_Recipe()
        {
            // Arrange - create the mock repository
            Mock<IRecipeRepository> mock = new Mock<IRecipeRepository>();
            mock.Setup(m => m.Recipe).Returns(new Recipe[]
            {
                new Recipe {RecipeID = 1, RecipeName = "R1"},
                new Recipe {RecipeID = 2, RecipeName = "R2"},
                new Recipe {RecipeID = 3, RecipeName = "R3"},
            });

            // Arrange - create a controller
            AdminController target = new AdminController(mock.Object);

            // Act
            Recipe result = (Recipe) target.Edit(4).ViewData.Model;

            // Assert
            Assert.IsNull(result);
        }
    }
}
