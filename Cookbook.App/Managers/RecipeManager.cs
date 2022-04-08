using Cookbook.App.Abstract;
using Cookbook.App.Concrete;
using Cookbook.Domain.Concrete;
using Cookbook.Domain.Entity;
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
        private RecipeService _recipeService;

        public RecipeManager(MenuActionService actionService, RecipeService recipeService)
        {
            _menuActionService = actionService;
            _recipeService = recipeService;
        }

        public int? EditRecipe()
        {
            if (_recipeService.Items.Any())
            {
                Console.WriteLine();
                Console.Write("Pick recipe id you want to edit: ");

                int recipeId;

                int.TryParse(Console.ReadLine(), out recipeId);

                var recipe = _recipeService.Items.FirstOrDefault(x => x.Id == recipeId);

                if (recipe != null)
                {
                    return recipeId;
                }
                else
                {
                    Console.WriteLine($"Recipe with ID {recipeId} doesnt exist.");
                    return null;
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Your recipes are empty. Add some and then edit them.");
                return null;
            }
        }

        public int AddRecipe()
        {
            Console.WriteLine("\nInsert your recipe name: ");
            string name = Console.ReadLine();

            Console.WriteLine("Insert your description of the recipe: ");
            string description = Console.ReadLine();

            int id = _recipeService.GetLastId() + 1;

            Recipe recipe = new Recipe() { Id = id, Name = name, Description = description };

            int recipeId = _recipeService.AddItem(recipe);

            return recipeId;
        }

        public void RemoveRecipe()
        {
            Console.WriteLine();

            if (AreThereAnyRecipes())
            {
                Console.Write("Insert the id of the recipe you want to delete: ");

                int recipeId;
                int.TryParse(Console.ReadLine(), out recipeId);

                var item = _recipeService.Items.FirstOrDefault(x => x.Id == recipeId);

                if (item != null)
                {
                    _recipeService.RemoveItem(item);
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine($"{recipeId} doesnt exist.");
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Your recipes are empty. Try to add some and then delete them.");
            }
        }

        public void ShowSelectedRecipeById(int? id = null)
        {
            if (AreThereAnyRecipes())
            {
                if (!id.HasValue)
                {
                    Console.Write("\nInsert your recipe ID:");
                    int recipeId;
                    int.TryParse(Console.ReadLine(), out recipeId);
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
                Console.Clear();
                Console.WriteLine("Your recipes are empty. Try to add some.");
            }
        }

        public void ShowRecipes()
        {
            if (_recipeService.Items.Any())
            {
                foreach (var (item, index) in _recipeService.Items.Select((x, i) => (x, i)))
                {
                    Console.WriteLine($"{index + 1}. {item.Name} ID:{item.Id}");
                }
            }
        }

        public void ChangeDescription(int? id)
        {
            var recipe = GetRecipeById(id);

            Console.WriteLine("\nInsert your new description: ");

            recipe.Description = Console.ReadLine();
        }

        public bool AreThereAnyRecipes()
        {
            return _recipeService.GetAllItems().Any();
        }

        private Recipe GetRecipeById(int id)
        {
            return _recipeService.GetItemById(id);
        }

        private Recipe GetRecipeById(int? id)
        {
            return _recipeService.GetItemById(id);
        }

        private void ShowRecipe(int id)
        {
            var recipe = _recipeService.GetItemById(id);

            if (recipe != null)
            {
                Console.WriteLine(recipe.Name + ":");
                foreach (var (ingredient, index) in recipe.Ingredients.Select((x, i) => (x, i)))
                {
                    Console.WriteLine($"{index + 1}. {ingredient.Name}\t(amount)- {ingredient.Amount}\t(unit): {ingredient.Unit}\t| ID:{ingredient.Id}");
                }
                Console.WriteLine("\nDescription: ");
                Console.WriteLine(recipe.Description + "\n");
            }
            else
            {
                Console.WriteLine($"{id} doesnt exist.");
            }
        }
    }
}
