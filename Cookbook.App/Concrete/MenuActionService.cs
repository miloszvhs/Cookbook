using Cookbook.App.Common;
using Cookbook.Domain.Common;
using Cookbook.Domain.Entity;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cookbook.Domain.Concrete
{
    public class MenuActionService : BaseService<MenuAction>
    {
        public MenuActionService()
        {
            InitializeMenu();
        }

        public List<MenuAction> DrawMenuViewByMenuType(string menuType)
        {
            Console.WriteLine();
            List<MenuAction> menuActions = new List<MenuAction>();

            foreach (var menu in Items)
            {
                if (menu.MenuType == menuType)
                {
                    AnsiConsole.Markup($"[bold cornflowerblue]{menu.Id}.[/] [dim steelblue1]{menu.Name}[/]\n");
                    menuActions.Add(menu);
                }
            }
            return menuActions;
        }

        private void InitializeMenu()
        {
            AddItem(new MenuAction(1, "Add recipe", "MainMenu"));
            AddItem(new MenuAction(2, "Show recipe", "MainMenu"));
            AddItem(new MenuAction(3, "Remove recipe", "MainMenu"));
            AddItem(new MenuAction(4, "Edit recipe", "MainMenu"));
            AddItem(new MenuAction(5, "Leave", "MainMenu"));

            AddItem(new MenuAction(1, "Add ingredients", "EditMenu"));
            AddItem(new MenuAction(2, "Remove and edit all ingredients", "EditMenu"));
            AddItem(new MenuAction(3, "Remove ingredient", "EditMenu"));
            AddItem(new MenuAction(4, "Edit specified ingredient", "EditMenu"));
            AddItem(new MenuAction(5, "Change description", "EditMenu"));
            AddItem(new MenuAction(6, "Leave", "EditMenu"));
        }
    }
}
