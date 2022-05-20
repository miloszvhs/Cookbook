using Cookbook.App.Abstract;
using Cookbook.App.Concrete;
using Cookbook.App.Helpers;
using Cookbook.App.Managers;
using Cookbook.Domain.Concrete;
using Cookbook.Domain.Entity;
using Spectre.Console;
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

            recipeService.LoadFromXML();

            while (true)
            {
                ConsoleText.ShowTitle();

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
                        RecipeManager.ShowRecipeById();
                        break;
                    case '3':
                        bool isRemoved = RecipeManager.RemoveRecipeById();
                        break;
                    case '4':
                        int id = RecipeManager.CheckIfRecipeExistAndGetId();
                        EditRecipe(id);
                        break;
                    case '5':
                        recipeService.SaveToXML();
                        GoodbyeMessage();
                        return;
                    default:
                        Validation.ValidateConsoleClearException();
                        break;
                }
            }
        }

        private void EditRecipe(int recipeId)
        {
            if (recipeId != -1)
            {
                Validation.ValidateConsoleClearException();

                while (true)
                {
                    ConsoleText.ShowTitle();

                    RecipeManager.ShowRecipeById(recipeId);

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
                            Validation.ValidateConsoleClearException();
                            return;
                        default:
                            Validation.ValidateConsoleClearException();
                            break;
                    }
                }
            }
        }

        private void GoodbyeMessage()
        {
            Console.WriteLine("\nSee you later!");
        }
    }
}
