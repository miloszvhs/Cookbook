using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cookbook.App.Helpers
{
    public static class Validation
    {
        public static double ValidateDouble()
        {
            double result;

            while (!double.TryParse(Console.ReadLine(), out result) || result <= 0)
            {
                TextAlert.Show(Types.IncorrectValue);
            }
            return result;
        }

        public static int ValidateInt()
        {
            int result;

            while (!int.TryParse(Console.ReadLine(), out result) || result <= 0)
            {
                TextAlert.Show(Types.IncorrectValue);
            }
            return result;
        }

        //I did it cuz sometime my tests went wrong because of no console window which Console.Clear() demands
        public static void ValidateConsoleClearException()
        {
            try
            {
                Console.Clear();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
