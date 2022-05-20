using Spectre.Console;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cookbook.App.Helpers
{
    public static class TextAlert
    {
        private static Dictionary<Types, string> _strings = new Dictionary<Types, string>()
        {
            { Types.InsertId, "Please insert the ID: "},
            { Types.IncorrectValue, "Incorrect value!"},
            { Types.InsertNameOfIngredient, "Insert the name of ingredient: " },
            { Types.InsertUnitName, "Insert the name of unit: " },
            { Types.InsertUnitAmount, "Insert unit amount: " },
            { Types.InsertNameOfRecipe, "Insert the name of recipe: " },
            { Types.IncorrectId, "Incorrect ID!" },
            { Types.InsertDescription, "Insert description: " },
            { Types.EmptyListOfRecipes, "Empty list of recipes!" },
            { Types.EmptyListOfIngredients, "Empty list of ingredients!" },
            { Types.InsertNumberOfIngredients, "Insert number of ingredients you want to add: " }
        };

        /// <summary>
        /// Show a text by type.
        /// </summary>
        /// <param name="type">Select type of communicate</param>
        /// <param name="isAlert">0, if it's not an alert, 1 if it is alert, 2 if want to draw with lines. 0 is default</param>
        public static void Show(Types type, int isAlert = 0)
        {
            if (isAlert == 0)
            {
                ConsoleText.ShowWithLine(_strings[type], "green3");
            }
            else if (isAlert == 1)
            {
                AnsiConsole.Markup($"[red bold]{_strings[type]}[/]\n");
            }

        }
    }

    public enum Types : int
    {
        InsertId = 1,
        IncorrectValue = 2,
        InsertNameOfIngredient = 3,
        InsertUnitName = 4,
        InsertUnitAmount = 5,
        IncorrectId = 6,
        EmptyListOfRecipes = 7,
        EmptyListOfIngredients = 8,
        InsertNameOfRecipe = 9,
        InsertDescription = 10,
        InsertNumberOfIngredients = 11,

    }
}