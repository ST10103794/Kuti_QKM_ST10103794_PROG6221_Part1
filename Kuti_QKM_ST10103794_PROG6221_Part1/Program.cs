using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace Kuti_QKM_ST10103794_PROG6221_Part1
{
    class Program
    {
        static void Main(string[] args)
        {
            // necessary variables declarations

            // instance of the 'Recipe' class
            Recipe recipe = new Recipe();

            int choice = 0;
            int ingredients=0;
            int steps = 0;
            string title = "";
            string input;

            Console.WriteLine("~~~~~~~~~~~~~~WELCOME TO THE RECIPE APP~~~~~~~~~~~~~~");
            Console.WriteLine("\nTo start, you will need to create a recipe");

            // method of 'Recipe' class to make a recipe
            recipe.makeRecipe();

            //program body
            while (true)
            {
                // menu
                Console.WriteLine("\nChoose from the menu below:\n" +
                "1. View recipes\n" +
                "2. Create recipe\n" +
                "99. Quit");

                // below block verifies that user input is an int
                input = Console.ReadLine();
                while (!Int32.TryParse(input, out choice))
                {
                    Console.WriteLine("\nWrong input, it should be a number (int). \nEnter again:");
                    input = Console.ReadLine();
                }

                switch (choice)
                {
                    case 1:
                        Console.WriteLine("\nHere are all the recorded recipes:\n");
                        recipe.listRecipes();

                        // other menu
                        Console.WriteLine("\n1. Choose a recipe \n2. Go back");

                        // below block verifies that user input is an int
                        input = Console.ReadLine();
                        while (!Int32.TryParse(input, out choice))
                        {
                            Console.WriteLine("\nWrong input, it should be a number (int). \nEnter again:");
                            input = Console.ReadLine();
                        }

                        // second program body
                        while (choice != 2 && choice != 99)
                        {

                            // in case of invalid input
                            while (choice != 1)
                            {
                                Console.WriteLine("\nInvalid option !!! \n1. Choose a recipe \n2. Go back");

                                // below block verifies that user input is an int
                                input = Console.ReadLine();
                                while (!Int32.TryParse(input, out choice))
                                {
                                    Console.WriteLine("\nWrong input, it should be a number (int). \nEnter again:");
                                    input = Console.ReadLine();
                                }
                            }
                            
                            
                            while (choice == 1)
                            {
                                // stores the name of desired recipe
                                string recipeName;

                                    Console.WriteLine("\nWrite the title of the recipe:");
                                    recipeName = Console.ReadLine().ToLower();

                                    recipeName = recipe.containsRecipe(recipeName);

                                // keeps the program about one recipe, unless loop is exited
                                while (recipeName != "stop")
                                    {

                                    // other menu
                                    Console.WriteLine("\nChoose a scale for the recipe:" +
                                    "\n1. half(0.5)" +
                                    "\n2. One (1)" +
                                    "\n3. Double (2)" +
                                    "\n4. Triple (3)" +
                                    "\n99. Return");

                                        // below block verifies that user input is an int
                                        input = Console.ReadLine();
                                        while (!Int32.TryParse(input, out choice))
                                        {
                                            Console.WriteLine("\nWrong input, it should be a number (int). \nEnter again:");
                                            input = Console.ReadLine();
                                        }

                                        // below ajusts the measurements of the ingredients in the menu
                                        switch (choice)
                                        {
                                            case 1:
                                                recipe.displayRecipe(recipeName, 0.5);
                                                break;
                                            case 2:
                                                recipe.displayRecipe(recipeName, 1);
                                                break;
                                            case 3:
                                                recipe.displayRecipe(recipeName, 2);
                                                break;
                                            case 4:
                                                recipe.displayRecipe(recipeName, 3);
                                                break;
                                            case 99:
                                                recipeName = "stop"; choice = 99; break;
                                                default:
                                                Console.WriteLine("\nYou have not selected a valid option!!!");
                                                break;
                                        }
                                    }
                            }
                        }
                        break;

                    case 2:
                        Console.WriteLine("\n=========New recipe=========");
                        recipe.makeRecipe();
                        break;

                    case 99:
                        return;

                    default:
                        Console.WriteLine("\nYou have not selected a valid option!!!");
                        break;
                }
            }
        }

    }
}