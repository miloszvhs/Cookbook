using Cookbook.App.Abstract;
using Cookbook.App.Concrete;
using Cookbook.App.Managers;
using Cookbook.Domain.Concrete;
using Cookbook.Domain.Entity;
using FluentAssertions; //fajna biblioteka, która zamienia nam asserty
using Moq;
using System;
using Xunit;

namespace Cookbook.Tests
{
    //Arrange - przygotowujemy dane do testowania
    //Act - wykonaj dzia³anie logiczne, które ma zostaæ przetestowane
    //Assert
    public class IngredientTest
    {
        [Fact] //adnotacja, która wskazuje, ¿e ta metoda jest metod¹ testow¹
        public void CheckIfRecipeWasReturnedProperly()
        {
            Recipe recipe = new Recipe() { Id = 1, Name = "Apple", Description = "test" };
            var mock = new Mock<IService<Recipe>>(); //symulacja serwisu
            mock.Setup(s => s.GetItemById(1)).Returns(recipe);

            var manager = new RecipeManager(new MenuActionService(), mock.Object);

            //Act
            var returnedRecipe = manager.GetItemById(recipe.Id);

            //Assert
            returnedRecipe.Should().BeOfType(typeof(Recipe));
            returnedRecipe.Should().BeSameAs(recipe);
            returnedRecipe.Should().NotBeNull();
        }

        [Fact]
        public void CanDeleteIngredientWithProperIdAndReturnsCorrectResult()
        {
            //Arange
            Ingredient ingredient = new Ingredient(3, "test", 100, "test");
            Recipe recipe = new Recipe() { Id = 5 };
            IService<Recipe> recipeService = new RecipeService();
            recipeService.AddItem(recipe);

            var manager = new IngredientManager(new MenuActionService(), recipeService);

            //Act
            manager.RemoveIngredient(5, 3);

            //Assert
            recipeService.GetItemById(recipe.Id).Ingredients.Should().BeEmpty();

            //Clean
            recipeService.RemoveItem(recipe);
        }

        [Fact]
        public void CheckIfIngredientWasAddedProperly()
        {
            //Arange
            Ingredient ingredient = new Ingredient(1, "test", 100, "gram");
            IService<Ingredient> ingredientService = new IngredientService();

            //Act
            int ingredientId = ingredientService.AddItem(ingredient);
            int lastId = ingredientService.GetLastId();

            //Assert
            ingredientId.Should().Be(1);
            ingredientService.Items.Should().HaveCount(1);
            ingredient.Id.Should().Be(lastId);

            //Clear
            ingredientService.RemoveItem(ingredient);
        }
    }
}
