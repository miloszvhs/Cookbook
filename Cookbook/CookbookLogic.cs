using Cookbook.App.Abstract;
using Cookbook.App.Concrete;
using Cookbook.App.Managers;
using Cookbook.Domain.Concrete;
using Cookbook.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cookbook
{
    public class CookbookLogic
    {
        public MenuActionService MenuActionService { get; set; }
        public RecipeManager RecipeManager { get; set; }
        public IngredientManager IngredientManager { get; set; }
        public RecipeService recipeService { get; set; }

        public CookbookLogic()
        {
            MenuActionService = new MenuActionService();
            recipeService = new RecipeService();
            RecipeManager = new RecipeManager(MenuActionService, recipeService);
            IngredientManager = new IngredientManager(MenuActionService, recipeService);
        }

        /// <summary>
        /// This function is running the whole class
        /// </summary>
        public void Run()
        {
            bool isRunning = true;

            while (isRunning)
            {
                RecipeManager.ShowRecipes();

                MenuActionService.DrawMenuViewByMenuType("MainMenu");

                var operation = Console.ReadKey();

                switch (operation.KeyChar)
                {
                    case '1':
                        var recipeId = RecipeManager.AddRecipe();
                        IngredientManager.AddIngredients(recipeId);
                        break;
                    case '2':
                        RecipeManager.ShowSelectedRecipeById();
                        break;
                    case '3':
                        RecipeManager.RemoveRecipe();
                        break;
                    case '4':
                        int? id = RecipeManager.EditRecipe();
                        EditRecipe(id);
                        break;
                    case '5':
                        isRunning = GoodbyeMessage();
                        break;
                    default:
                        break;
                }
            }
        }

        private void EditRecipe(int? recipeId)
        {
            if (recipeId.HasValue)
            {
                Console.Clear();

                bool isRunning = true;

                while (isRunning)
                {
                    RecipeManager.ShowSelectedRecipeById(recipeId);

                    MenuActionService.DrawMenuViewByMenuType("EditMenu");

                    var operation = Console.ReadKey();

                    switch (operation.KeyChar)
                    {
                        case '1':
                            IngredientManager.AddIngredients(recipeId);
                            break;
                        case '2':
                            IngredientManager.RemoveAndEditAllIngredients(recipeId);
                            break;
                        case '3':
                            var ingredientId = IngredientManager.GetIngredientId(recipeId);
                            IngredientManager.RemoveIngredient(recipeId, ingredientId);
                            break;
                        case '4':
                            ingredientId = IngredientManager.GetIngredientId(recipeId);
                            IngredientManager.EditSpecifiedIngredient(recipeId, ingredientId);
                            break;
                        case '5':
                            RecipeManager.ChangeDescription(recipeId);
                            break;
                        case '6':
                            isRunning = false;
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        /*private void ShowRecipe()
        {
            bool isRunning = true;

            while (isRunning)
            {
                RecipeManager.ShowRecipes();

                var operation = Console.ReadKey();

                switch (operation.KeyChar)
                {
                    case '1':
                        RecipeManager.ShowSelectedRecipeById();
                        break;
                    case '2':
                        RecipeManager.RemoveRecipe();
                        break;
                    case '3':
                        isRunning = false;
                        break;
                    default:
                        break;
                }
            }

        }*/
        /*        private Recipe ChangeIngredientsAndDescription(Recipe recipe)
                {
                    Console.WriteLine();
                    recipe = ChangeIngredients(recipe);
                    recipe = ChangeDescription(recipe);

                    return recipe;
                }*/

        private bool GoodbyeMessage()
        {
            Console.WriteLine("\nSee you later!");
            return false;
        }
    }
}
