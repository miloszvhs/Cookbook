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
            MenuActionService = InitializeMenus();
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
            Console.WriteLine("\nSee you later!");
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

            Recipe recipe= new Recipe();

            Console.Write("What is your recipe name? ");
            string name = Console.ReadLine();

            name = CheckIfRecipeExistInList(name);

            recipe.Name = name;

            recipe = AddIngredients(recipe);

            Console.WriteLine("Insert your recipe description: ");

            string description = Console.ReadLine();

            recipe.Description = description;

            Cookbook.Recipes.Add(recipe);
        }

        private Recipe AddIngredients(Recipe recipe)
        {
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

                recipe.Ingredients.Add(ingredient);
            }
            return recipe; 
        }

        /// <summary>
        /// Remove recipe by name of the recipe shown in the list
        /// </summary>
        public void RemoveRecipeByName()
        {
            Console.WriteLine();

            if (Cookbook.Recipes.Count != 0)
            {
                Console.Write("Insert name of the recipe you want to delete: ");
                string name = Console.ReadLine();

                var item = Cookbook.Recipes.FirstOrDefault(x => x.Name == name);

                if (item != null)
                {
                    Cookbook.Recipes.Remove(item);
                } 
                else
                {
                    Console.Clear();
                    Console.WriteLine($"{name} doesnt exist.");
                }
            } else
            {
                Console.Clear();
                Console.WriteLine("Your recipes are empty. Try add some and then delete them.");
            }
        }

        private void ShowRecipe(string name)
        {
            var recipe = Cookbook.Recipes.FirstOrDefault(x => x.Name == name);
            
            if (recipe != null)
            {
                Console.WriteLine(recipe.Name + ":");
                foreach (var (ingredient, index) in recipe.Ingredients.Select((x, i) => (x, i)))
                {
                    Console.WriteLine($"{index + 1}. {ingredient.Name}\t- {ingredient.Amount}\t: {ingredient.Unit}");
                }
                Console.WriteLine("\nDescription: ");
                Console.WriteLine(recipe.Description + "\n");
            }
            else
            {
                Console.WriteLine($"{name} doesnt exist.");
            }
        }

        /// <summary>
        /// Show details of recipe by name of the recipe shown in the list
        /// </summary>
        public void ShowSelectedRecipeByName()
        {
            if (Cookbook.Recipes.Count != 0)
            {
                Console.WriteLine();
                Console.Write("Insert name of the recipe: ");
                string userInput = Console.ReadLine();

                Console.WriteLine();
                Console.Clear();

                ShowRecipe(userInput);
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Recipes are empty. Add some and then select one of them.");
            }
            
        }

        /// <summary>
        /// Show recipes list
        /// </summary>
        public void ShowRecipes()
        {
            bool isRunning = true;

            Console.Clear();

            while (isRunning)
            {

                if (Cookbook.Recipes != null)
                {
                    Console.WriteLine($"Recipes: ");

                    foreach (var (recipe, index) in Cookbook.Recipes.Select((x, i) => (x, i)))
                    {
                        Console.WriteLine($"{index + 1}. {recipe.Name}");
                    }
                }

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
                        Console.Clear();
                        isRunning = false;
                        break;
                    default:
                        break;
                }
            }
        }

        public void EditRecipe()
        {
            if (Cookbook.Recipes.Count != 0)
            {
                bool isRunning = true;

                Console.WriteLine();
                Console.Write("Pick recipe you want to edit: ");
                string recipeName = Console.ReadLine();

                var recipe = Cookbook.Recipes.FirstOrDefault(x => x.Name == recipeName);

                if (recipe != null && isRunning)
                {
                    ShowRecipe(recipeName);

                    Console.WriteLine("What do you want to edit?");
                    MenuActionService.DrawMenuViewByMenuType("EditRecipeMenu");

                    var operation = Console.ReadKey();

                    switch (operation.KeyChar)
                    {
                        case '1':
                            recipe = ChangeIngredients(recipe);
                            //ingredients
                            break;
                        case '2':
                            recipe = ChangeDescription(recipe);
                            //description
                            break;
                        case '3':
                            //both
                            recipe = ChangeIngredientsAndDescription(recipe);
                            break;
                        case '4':
                            Console.Clear();
                            isRunning = false;
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
            else
            {
                Console.Clear();
                Console.WriteLine("Your recipes are empty. Add some and then edit them.");
            }
        }

        private Recipe ChangeIngredients(Recipe recipe)
        {
            bool isRunning = true;

            while (isRunning)
            {
                Console.WriteLine("\nWhat do you want to do?");

                MenuActionService.DrawMenuViewByMenuType("IngredientsEdit");

                var operation = Console.ReadKey();

                switch (operation.KeyChar)
                {
                    case '1':
                        recipe = EditSpecifiedIngredient(recipe);
                        break;
                    case '2':
                        recipe = EditAllIngredients(recipe);
                        break;
                    case '3':
                        isRunning = false;
                        break;
                    default:
                        break;
                }
            }
            return recipe;
        }
        
        private Recipe EditSpecifiedIngredient(Recipe recipe)
        {
            Console.Write("\nChoose ingredient by the name that you want to change: ");
            string name = Console.ReadLine();

            var item = recipe.Ingredients.Find(x => x.Name == name);

            if (item != null)
            {
                Console.Write($"Insert ingredient name: ");
                item.Name = Console.ReadLine();

                Console.Write("Insert ingredient amount: ");
                int ingredientAmount;
                int.TryParse(Console.ReadLine(), out ingredientAmount);
                item.Amount = ingredientAmount;

                Console.Write("Insert amount unit: ");
                item.Unit = Console.ReadLine();
            }
            else
            {
                Console.WriteLine($"Ingredient {name} doesnt exist.");
            }

            return recipe;
        }

        private Recipe EditAllIngredients(Recipe recipe)
        {
            Console.WriteLine();
            recipe = RemoveAllIngredients(recipe);
            recipe = AddIngredients(recipe);

            return recipe;
        }

        private Recipe RemoveAllIngredients(Recipe recipe)
        {
            recipe.Ingredients.Clear();

            return recipe;
        }

        private Recipe ChangeDescription(Recipe recipe)
        {
            Console.WriteLine("\nInsert your new description: ");

            string description = Console.ReadLine();

            recipe.Description = description;

            return recipe;
        }

        private Recipe ChangeIngredientsAndDescription(Recipe recipe)
        {
            Console.WriteLine();
            recipe = ChangeIngredients(recipe);
            recipe = ChangeDescription(recipe);

            return recipe;
        }

        private MenuActionService InitializeMenus()
        {
            MenuActionService menu = new MenuActionService();

            menu.AddMenuView(1, "Add recipe", "MainMenu");
            menu.AddMenuView(2, "Show recipes", "MainMenu");
            menu.AddMenuView(3, "Leave", "MainMenu");

            menu.AddMenuView(1, "Show recipe", "EditMenu");
            menu.AddMenuView(2, "Remove recipe", "EditMenu");
            menu.AddMenuView(3, "Edit recipe", "EditMenu");
            menu.AddMenuView(4, "Leave", "EditMenu");

            menu.AddMenuView(1, "Ingredients", "EditRecipeMenu");
            menu.AddMenuView(2, "Description", "EditRecipeMenu");
            menu.AddMenuView(3, "Both", "EditRecipeMenu");
            menu.AddMenuView(4, "Leave", "EditRecipeMenu");

            menu.AddMenuView(1, "Edit specified ingredient", "IngredientsEdit");
            menu.AddMenuView(2, "Remove all ingredients and add the new ones", "IngredientsEdit");
            menu.AddMenuView(3, "Leave", "IngredientsEdit");

            return menu;
        }
    }
}
