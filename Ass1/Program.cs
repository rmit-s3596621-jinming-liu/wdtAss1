using System;

namespace Ass1
{
    
    public static class BuildingDemo
    {


        public static void Main(string[] args)
        {
            DataWarehouse.LoadAccount();
            var result = DataWarehouse.Accounts.Count;
            Console.WriteLine(result);
        }

    }
    }

