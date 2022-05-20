using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cookbook.App.Helpers
{
    public static class ConsoleText
    {
        private static Rule _rule = new Rule();
        private static Table _table = new Table();
        public static void ShowTitle()
        {
            _rule = new Rule("[bold]Cookbook[/]");
            _rule.Alignment = Justify.Left;
            AnsiConsole.Write(_rule);
        }

        public static void ShowWithLine(string txt, string color = "red")
        {
            Console.WriteLine();
            _rule = new Rule($"[{color}]{txt}[/]");
            _rule.Alignment = Justify.Left;
            AnsiConsole.Write(_rule);
        }
    }
}
