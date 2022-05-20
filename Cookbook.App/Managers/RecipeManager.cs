using Cookbook.App.Abstract;
using Cookbook.App.Concrete;
using Cookbook.App.Helpers;
using Cookbook.Domain.Concrete;
using Cookbook.Domain.Entity;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cookbook.App.Managers
{
    public class RecipeManager
    {
        private readonly MenuActionService _menuActionService;
        private IService<Recipe> _recipeService;

        public RecipeManager(MenuActionService actionService, IService<Recipe> recipeService)
        {
            _menuActionService = actionService;
            _recipeService = recipeService;
        }

        public int CheckIfRecipeExistAndGetId(int id = 0)
        {
            if (AreThereAnyRecipes())
            {
                if (id == 0)
                {
                    TextAlert.Show(Types.InsertId);

                    id = Validation.ValidateInt();
                }

                var recipe = _recipeService.Items.FirstOrDefault(x => x.Id == id);

                if (recipe is not null)
                {
                    return id;
                }

                //Needed to paste that here because my tests went wrong
                //cuz of IOexception of Console.Clear() method.
                Validation.ValidateConsoleClearException();

                TextAlert.Show(Types.IncorrectId, 1);

                return -1;
            }

            Validation.ValidateConsoleClearException();

            TextAlert.Show(Types.EmptyListOfRecipes, 1);

            return -1;
        }

        public int AddRecipe()
        {
            TextAlert.Show(Types.InsertNameOfRecipe);
            string name = Console.ReadLine();

            TextAlert.Show(Types.InsertDescription);
            string description = Console.ReadLine();

            int id = _recipeService.GetLastId() + 1;

            Recipe recipe = new Recipe(id, name, description);

            int recipeId = _recipeService.AddItem(recipe);

            return recipeId;
        }

        public bool RemoveRecipeById(int id = 0)
        {
            Console.WriteLine();

            if (AreThereAnyRecipes())
            {
                if (id == 0)
                {
                    TextAlert.Show(Types.InsertId);

                    id = Validation.ValidateInt();
                }

                var item = _recipeService.GetItemById(id);

                if (item is not null)
                {
                    Validation.ValidateConsoleClearException();

                    return _recipeService.RemoveItem(item);
                }
                else
                {
                    Validation.ValidateConsoleClearException();
                    TextAlert.Show(Types.IncorrectId, 1);
                    return false;
                }
            }
            Validation.ValidateConsoleClearException();
            TextAlert.Show(Types.EmptyListOfRecipes, 1);

            return false;
        }

        public void ShowRecipeById(int? id = null)
        {
            if (AreThereAnyRecipes())
            {
                if (!id.HasValue)
                {
                    TextAlert.Show(Types.InsertId);
                    int recipeId = Validation.ValidateInt();
                    Console.Clear();
                    ShowRecipe(recipeId);
                }
                else
                {
                    ShowRecipe((int)id);
                }
            }
            else
            {
                Validation.ValidateConsoleClearException();
                TextAlert.Show(Types.EmptyListOfRecipes, 1);
            }
        }

        public string[] ShowRecipes()
        {
            List<string> textOutput = new List<string>();

            var table = new Table();

            table.AddColumn("Number");
            table.AddColumn(new TableColumn("Name").RightAligned());
            table.AddColumn(new TableColumn("ID").RightAligned());

            if (_recipeService.Items.Any())
            {
                foreach (var (item, index) in _recipeService.Items.Select((x, i) => (x, i)))
                {
                    table.AddRow($"{index + 1}", $"{item.Name}", $"{item.Id}");
                    textOutput.Add($"{index + 1}. {item.Name} ID:{item.Id}");
                }
            }
            AnsiConsole.Write(table);

            return textOutput.ToArray();
        }

        public string ChangeDescription(int? id, string newDescription = "empty")
        {
            var recipe = GetRecipeById(id);

            if (newDescription is "empty")
            {
                TextAlert.Show(Types.InsertDescription);
                newDescription = Console.ReadLine();
            }

            recipe.Description = newDescription;

            Validation.ValidateConsoleClearException();

            return newDescription;
        }

        public bool AreThereAnyRecipes()
        {
            return _recipeService.GetAllItems().Any();
        }

        private Recipe GetRecipeById(int? id)
        {
            return _recipeService.GetItemById(id);
        }

        private void ShowRecipe(int id)
        {
            var recipe = _recipeService.GetItemById(id);

            if (recipe is not null)
            {
                var rule = new Rule(recipe.Name);
                rule.Alignment = Justify.Left;
                AnsiConsole.Write(rule);

                var table = new Table();

                table.AddColumn("Number");
                table.AddColumn("Name");
                table.AddColumn(new TableColumn("AmountOfUnit").RightAligned());
                table.AddColumn(new TableColumn("Unit").RightAligned());
                table.AddColumn(new TableColumn("ID").RightAligned());

                
                foreach (var (ingredient, index) in recipe.Ingredients.Select((x, i) => (x, i)))
                {
                    table.AddRow($"{index + 1}", $"{ingredient.Name}", $"{ingredient.AmountOfUnit}", $"{ingredient.Unit}", $"{ingredient.Id}");
                }
                AnsiConsole.Write(table);

                table = new Table();
                table.AddColumn("Description");
                table.AddRow(recipe.Description);
                table.Width(50);
                AnsiConsole.Write(table);
            }
            else
            {
                TextAlert.Show(Types.IncorrectId, 1);
            }
        }

        public Recipe GetItemById(int id)
        {
            var recipe = _recipeService.GetItemById(id);
            return recipe;
        }
    }
}
