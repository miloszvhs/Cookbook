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
    public class IngredientManager
    {
        private readonly MenuActionService _menuActionService;
        private IngredientService _ingredientService;
        private RecipeService _recipeService;

        public IngredientManager(MenuActionService actionService, RecipeService recipeService)
        {
            _menuActionService = actionService;
            _ingredientService = new IngredientService();
            _recipeService = recipeService;
        }

        public void AddIngredients(int? id)
        {
            var recipe = GetRecipeById(id);

            Console.WriteLine("How many ingredients do you want to add?");

            int input;

            int.TryParse(Console.ReadLine(), out input);

            for (int i = 0; i < input; i++)
            {
                AddIngredient(recipe);
            }
        }

        public void RemoveIngredient(int? recipeId, int? ingredientId)
        {
            if (recipeId != null && ingredientId != null)
            {
                var recipe = GetRecipeById(recipeId);
                var ingredient = recipe.Ingredients.FirstOrDefault(x => x.Id == ingredientId);
                if (ingredient != null)
                {
                    recipe.Ingredients.Remove(ingredient);
                }
                else
                {
                    Console.WriteLine($"Ingredient with the ID:{ingredientId} doesnt exist.");
                }
            }
        }

        public int? GetIngredientId(int? recipeId)
        {
            Console.Write("\nInsert your ingredient ID: ");

            int id;
            int.TryParse(Console.ReadLine(), out id);
            Console.Clear();

            return id;
        }

        public void EditSpecifiedIngredient(int? recipeId, int? ingredientId)
        {
            var item = GetRecipeById(recipeId)
                .Ingredients.FirstOrDefault(x => x.Id == ingredientId);

            if (item != null)
            {
                Console.Write($"Insert ingredient name: ");
                item.Name = Console.ReadLine();

                Console.Write("Insert amount unit: ");
                item.Unit = Console.ReadLine();

                Console.Write("Insert ingredient amount: ");
                int ingredientAmount;
                int.TryParse(Console.ReadLine(), out ingredientAmount);
                item.Amount = ingredientAmount;
            }
            else
            {
                Console.WriteLine($"Ingredient {ingredientId} doesnt exist.");
            }
        }

        public void RemoveAndEditAllIngredients(int? id)
        {
            if (id != null)
            {
                int recipeId = (int)id;
                RemoveAllIngredients(recipeId);
                AddIngredients(recipeId);
            }
        }

        private void AddIngredient(Recipe recipe)
        {
            Console.WriteLine("Insert the name of your ingredient: ");
            string name = Console.ReadLine();

            Console.WriteLine("Insert unit of your ingredient: ");
            string unit = Console.ReadLine();

            Console.WriteLine("Insert amount of your ingredient: ");
            int amount;
            int.TryParse(Console.ReadLine(), out amount);

            int id = _ingredientService.GetLastId() + 1;
            int recipeId = recipe.Id;

            Ingredient ingredient = new Ingredient(id, name, amount, unit);

            _ingredientService.AddItem(ingredient);

            recipe.Ingredients.Add(ingredient);

            Console.WriteLine();
        }

        private void RemoveAllIngredients(int id)
        {
            var recipe = _recipeService.Items.FirstOrDefault(x => x.Id == id);
            if (recipe != null)
            {
                recipe.Ingredients.Clear();
            }
        }

        private Recipe GetRecipeById(int id)
        {
            var recipe = _recipeService.Items.FirstOrDefault(x => x.Id == id);
            return recipe;
        }

        private Recipe GetRecipeById(int? id)
        {
            var recipe = _recipeService.Items.FirstOrDefault(x => x.Id == id);
            return recipe;
        }
    }
}
