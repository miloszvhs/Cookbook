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

        private string CheckIfRecipeExistInList(string name)
        {
            while (Cookbook.Recipes.Select(x => x.Name).Contains(name))
            {
                Console.Write("There is a recipe like this. Please change the name of your new recipe: ");
                name = Console.ReadLine();
            }
            return name;
        }

        /// <summary>
        /// Add recipe to the list
        /// </summary>
        public void AddRecipe()
        {
            Console.WriteLine();

            Recipe recipeItem = new Recipe();

            Console.Write("What is your recipe name? ");
            string name = Console.ReadLine();

            name = CheckIfRecipeExistInList(name);

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

        /// <summary>
        /// Remove recipe by name of the recipe shown in the list
        /// </summary>
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

        private void ShowRecipe(string name)
        {
            foreach (var recipe in Cookbook.Recipes)
            {
                if (recipe.Name == name)
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

        /// <summary>
        /// Show details of recipe by name of the recipe shown in the list
        /// </summary>
        public void ShowSelectedRecipeByName()
        {
            Console.WriteLine();
            Console.Write("Insert name of the recipe: ");
            string userInput = Console.ReadLine();

            Console.WriteLine();

            ShowRecipe(userInput);
        }

        /// <summary>
        /// Show recipes list
        /// </summary>
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

        //TODO dokonczyc editRecipe
        public void EditRecipe()
        {
            Console.Write("Pick recipe you want to edit: ");
            string recipeName = Console.ReadLine();

            if (Cookbook.Recipes.Select(x => x.Name).Contains(recipeName))
            {
                ShowRecipe(recipeName);

                Console.WriteLine("What do you want to edit?");
                MenuActionService.DrawMenuViewByMenuType("EditMenu");

                var operation = Console.ReadKey();

                switch (operation.KeyChar)
                {
                    case '1':
                        break;
                    case '2':
                        break;
                    case '3':
                        break;
                    default:
                        break;
                }
            }
            else
            {
                Console.WriteLine($"{recipeName} doesnt exist.");
            }
            
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

            menu.AddMenuView(1, "Ingredients", "EditMenu");
            menu.AddMenuView(2, "Description", "EditMenu");
            menu.AddMenuView(3, "Both", "EditMenu");

            return menu;
        }
    }
}
