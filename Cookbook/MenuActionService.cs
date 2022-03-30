using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cookbook
{
    public class MenuActionService
    {
        List<MenuAction> menuActions;

        public MenuActionService()
        {
            menuActions = new List<MenuAction>();
        }

        public void AddMenuView(int id, string name, string menuType)
        {
            MenuAction menuAction = new MenuAction() { Id = id, Name = name, MenuType = menuType};
            menuActions.Add(menuAction);
        }

        public void DrawMenuViewByMenuType(string menuType)
        {
            Console.WriteLine();

            foreach (var menu in menuActions)
            {
                if (menu.MenuType == menuType)
                {
                    Console.WriteLine($"{menu.Id}. {menu.Name}");
                }
            }
        }
    }
}
