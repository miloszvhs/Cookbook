using Cookbook.App.Abstract;
using Cookbook.App.Concrete;
using Cookbook.App.Helpers;
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
        private IService<Recipe> _recipeService;

        public IngredientManager(MenuActionService actionService, IService<Recipe> recipeService)
        {
            _menuActionService = actionService;
            _ingredientService = new IngredientService();
            _recipeService = recipeService;
        }

        public void AddIngredients(int? id)
        {
            var recipe = GetRecipeById(id);

            TextAlert.Show(Types.InsertNumberOfIngredients);

            int input = Validation.ValidateInt();

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
                    TextAlert.Show(Types.IncorrectId, 1);
                }
            }
        }

        public int? GetIngredientId(int? recipeId)
        {
            TextAlert.Show(Types.InsertId);

            int id = Validation.ValidateInt();

            Validation.ValidateConsoleClearException();

            return id;
        }

        public void EditSpecifiedIngredient(int? recipeId, int? ingredientId)
        {
            var item = GetRecipeById(recipeId)
                .Ingredients.FirstOrDefault(x => x.Id == ingredientId);

            if (item is not null)
            {
                TextAlert.Show(Types.InsertNameOfIngredient);
                item.Name = Console.ReadLine();

                TextAlert.Show(Types.InsertUnitName);
                item.Unit = Console.ReadLine();

                TextAlert.Show(Types.InsertUnitAmount);
                item.AmountOfUnit = Validation.ValidateInt();
            }
            else
            {
                TextAlert.Show(Types.IncorrectId);
            }
        }

        public void RemoveAndEditAllIngredients(int? id)
        {
            if (id is not null)
            {
                int recipeId = (int)id;
                RemoveAllIngredients(recipeId);
                AddIngredients(recipeId);
            }
        }

        public void AddIngredient(Recipe recipe)
        {
            TextAlert.Show(Types.InsertNameOfIngredient);
            string name = Console.ReadLine();

            TextAlert.Show(Types.InsertUnitName);
            string unit = Console.ReadLine();

            TextAlert.Show(Types.InsertUnitAmount);
            int amount = Validation.ValidateInt();

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

            if (recipe is not null)
            {
                recipe.Ingredients.Clear();
            }
        }

        private Recipe GetRecipeById(int? id)
        {
            return _recipeService.Items.FirstOrDefault(x => x.Id == id);
        }
    }
}
