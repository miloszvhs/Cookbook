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
        public Cookbook Cookbook { get; set; }

        public CookbookLogic()
        {
            MenuActionService = Initialize();
            Cookbook = new Cookbook();
        }

        /// <summary>
        /// This function is running the whole class
        /// </summary>
        public void Run()
        {
            bool isRunning = true;

            while (isRunning)
            {
                MenuActionService.DrawMenuViewByMenuType("MainMenu");

                var operation = Console.ReadKey();

                switch (operation.KeyChar)
                {
                    case '1':
                        AddRecipe();
                        break;
                    case '2':
                        ShowRecipes();
                        break;
                    case '3':
                        isRunning = GoodbyeMessage();
                        break;
                    default:
                        break;
                }
            }
        }

        private bool GoodbyeMessage()
        {
            Console.WriteLine("See you later!");
            return false;
        }

        public void AddRecipe()
        {
            Console.WriteLine();

            Recipe recipeItem = new Recipe();

            Console.Write("What is your recipe name? ");
            string name = Console.ReadLine();

            while (Cookbook.Recipes.Select(x => x.Name).Contains(name))
            {
                Console.Write("There is a recipe like this. Please change the name of your new recipe: ");
                name = Console.ReadLine();
            }

            recipeItem.Name = name;

            Console.Write("How many ingredients do you want? ");

            int ingredientsNumber;
            int.TryParse(Console.ReadLine(), out ingredientsNumber);

            for (int i = 1; i <= ingredientsNumber; i++)
            {
                Console.Write($"Ingredient nr. {i}\nInsert ingredient name: ");
                string ingredientName = Console.ReadLine();

                Console.Write("Insert ingredient amount: ");
                int ingredientAmount;
                int.TryParse(Console.ReadLine(), out ingredientAmount);

                Console.Write("Insert amount unit: ");
                string ingredientUnit = Console.ReadLine();

                Ingredient ingredient = new Ingredient()
                {
                    Name = ingredientName,
                    Amount = ingredientAmount,
                    Unit = ingredientUnit
                };


                recipeItem.Ingredients.Add(ingredient);
            }

            Console.WriteLine("Insert your recipe description: ");

            string description = Console.ReadLine();

            recipeItem.Description = description;

            Cookbook.Recipes.Add(recipeItem);
        }

        public void RemoveRecipeByName()
        {
            Console.WriteLine();
            Console.Write("Insert name of the recipe you want to delete: ");
            string name = Console.ReadLine();

            if (Cookbook.Recipes.Count != 0)
            {
                foreach (var recipe in Cookbook.Recipes)
                {
                    if (recipe.Name == name)
                    {
                        Cookbook.Recipes.Remove(recipe);
                    }
                }
            } else
            {
                Console.WriteLine("Your recipes are empty. Try add some and then delete them.");
            }
        }

        public void ShowSelectedRecipeByName()
        {
            Console.WriteLine();
            Console.Write("Insert name of the recipe: ");
            string userInput = Console.ReadLine();

            Console.WriteLine();

            foreach (var recipe in Cookbook.Recipes)
            {
                if (recipe.Name == userInput)
                {
                    foreach (var (ingredient, index) in recipe.Ingredients.Select((x, i) => (x, i)))
                    {
                        Console.WriteLine($"{index + 1}. {ingredient.Name} - {ingredient.Amount} : {ingredient.Unit}");
                    }

                    Console.WriteLine(recipe.Description);
                    break;
                }
            }
        }

        public void ShowRecipes()
        {
            Console.WriteLine();

            if (Cookbook.Recipes != null)
            {
                foreach (var (recipe, index) in Cookbook.Recipes.Select((x, i) => (x, i)))
                {
                    Console.WriteLine($"{index + 1}. {recipe.Name}");
                }
            }

            bool isRunning = true;

            while (isRunning)
            {
                MenuActionService.DrawMenuViewByMenuType("EditMenu");

                var operation = Console.ReadKey();

                switch (operation.KeyChar)
                {
                    case '1':
                        ShowSelectedRecipeByName();
                        break;
                    case '2':
                        RemoveRecipeByName();
                        break;
                    case '3':
                        EditRecipe();
                        break;
                    case '4':
                        isRunning = false;
                        break;
                    default:
                        break;
                }
            }
        }

        public void EditRecipe()
        {

        }

        private MenuActionService Initialize()
        {
            MenuActionService menu = new MenuActionService();

            menu.AddMenuView(1, "Add recipe", "MainMenu");
            menu.AddMenuView(2, "Show recipes", "MainMenu");
            menu.AddMenuView(3, "Leave", "MainMenu");

            menu.AddMenuView(1, "Select recipe", "EditMenu");
            menu.AddMenuView(2, "Remove recipe", "EditMenu");
            menu.AddMenuView(3, "Edit recipe", "EditMenu");
            menu.AddMenuView(4, "Leave", "EditMenu");

            return menu;
        }
    }
}
