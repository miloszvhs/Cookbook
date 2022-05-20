using Cookbook.App.Abstract;
using Cookbook.App.Concrete;
using Cookbook.App.Managers;
using Cookbook.App.Helpers;
using Cookbook.Domain.Concrete;
using Cookbook.Domain.Entity;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Cookbook.Tests
{
    public class RecipeTest
    {
        [Fact]
        public void AddRecipeAndGetProperResult()
        {
            //Arrange
            Recipe recipe = new Recipe() { Id = 5 };
            IService<Recipe> recipeService = new RecipeService();

            //Act
            int result = recipeService.AddItem(recipe);
            int lastId = recipeService.GetLastId();

            //Assert
            result.Should().Be(5);
            recipeService.Items.Should().HaveCount(1);
            recipe.Id.Should().Be(lastId);

            //Clean
            recipeService.RemoveItem(recipe);
        }
        
        [Fact]
        public void RemoveRecipeByIdAndReturnProperResult()
        {
            //Arrange
            Recipe recipe = new Recipe() { Id = 3 };
            IService<Recipe> recipeService = new RecipeService();
            RecipeManager recipeManager = new RecipeManager(new MenuActionService(), recipeService);
            recipeService.AddItem(recipe);

            //Act
            bool positiveResult = recipeManager.RemoveRecipeById(3);
            bool negativeResult = recipeManager.RemoveRecipeById(5);
            //Assert
            positiveResult.Should().BeTrue();
            negativeResult.Should().BeFalse();
            recipeService.Items.Should().BeEmpty();
        }

        [Fact]
        public void CheckIfThereAreAnyRecipesInService()
        {
            //Arrange
            Recipe recipe = new Recipe() { Id = 1};
            IService<Recipe> recipeService = new RecipeService();
            RecipeManager recipeManager = new RecipeManager(new MenuActionService(), recipeService);
            recipeService.AddItem(recipe);

            //Act
            bool result = recipeManager.AreThereAnyRecipes();

            //Assert
            result.Should().Be(true);
            recipeService.GetAllItems().Should().HaveCount(1);

            //Clean
            recipeService.RemoveItem(recipe);
        }

        [Fact]
        public void CheckIfRecipeWithIdExist()
        {
            //Arrange
            Recipe recipe = new Recipe() { Id = 6};
            IService<Recipe> recipeService = new RecipeService();
            RecipeManager recipeManager = new RecipeManager(new MenuActionService(), recipeService);
            recipeService.AddItem(recipe);

            //Act
            int positiveResult = recipeManager.CheckIfRecipeExistAndGetId(6);
            int negativeResult = recipeManager.CheckIfRecipeExistAndGetId(5);

            //Assert
            positiveResult.Should().Be(6);
            negativeResult.Should().Be(-1);

            //Clean
            recipeService.RemoveItem(recipe);
        }

        [Fact]
        public void CheckIfShowRecipesShowsText()
        {
            //Arrange
            Recipe recipeOne = new Recipe() { Id = 6, Name = "Test" };
            Recipe recipeTwo = new Recipe() { Id = 3, Name = "TestTest" };
            string[] input = new string[] { "1. Test ID:6", "2. TestTest ID:3" };
            IService<Recipe> recipeService = new RecipeService();
            RecipeManager recipeManager = new RecipeManager(new MenuActionService(), recipeService);
            recipeService.AddItem(recipeOne);
            recipeService.AddItem(recipeTwo);

            //Act
            string[] result = recipeManager.ShowRecipes();

            //Assert
            result.Should().NotBeEquivalentTo(new string[] {"test", "test"});
            result.Should().BeEquivalentTo(input);
        }

        [Fact]
        public void CheckIfDescriptionWasChangedProperly()
        {
            //Arrange
            Recipe recipe = new Recipe() { Id = 2, Description = "Test"};
            IService<Recipe> recipeService = new RecipeService();
            RecipeManager recipeManager = new RecipeManager(new MenuActionService(), recipeService);
            recipeService.AddItem(recipe);

            //Act
            string result = recipeManager.ChangeDescription(recipe.Id, "testt");

            //Assert
            result.Should().Be("testt");
            result.Should().NotBe("empty");
            recipeManager.GetItemById(recipe.Id).Description.Should().Be(result);
        }
    }
}
