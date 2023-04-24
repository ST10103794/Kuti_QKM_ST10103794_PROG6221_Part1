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
            int choice = 0;
            int ingredients=0;
            int steps = 0;
            string title = "";

            Console.WriteLine("~~~~~~~~~~~~~~WELCOME TO THE RECIPE APP~~~~~~~~~~~~~~");
            Console.WriteLine("\nTo start, you will need to create a recipe\n");

            // record data for class constructor
            Console.WriteLine("\nWrite a title for your recipe (e.g. How to cook pasta):");
            title = Console.ReadLine();

            Console.WriteLine("\nHow many ingredients do you require:");
            ingredients = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("\nHow many steps will be followed:");
            steps = Convert.ToInt32(Console.ReadLine());

            // creating an object of the 'recipe' class
            Recipe cook = new Recipe(title, ingredients + 1, steps + 1); //might need to add '+1' to the int n double

            makeRecipe(ingredients, steps);

            //program body
            while (true)
            {
                Console.WriteLine("Choose from the menu below:\n" +
                "1. View recipe\n" +
                "2. Create recipe   (!!!DELETE CURRENT RECIPE!!!)\n" +
                "99. Quit");
                choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        Console.WriteLine("\nChoose a scale for the recipe:" +
                                "\n1. half(0.5)" +
                                "\n2. One (1)" +
                                "\n3. Double (2)" +
                                "\n4. Triple (3)");
                        choice = Convert.ToInt32(Console.ReadLine());

                        switch (choice)
                        {
                            case 1:
                                readRecipe(0.5);
                                break;
                            case 2:
                                readRecipe(1);
                                break;
                            case 3:
                                readRecipe(2);
                                break;
                            case 4:
                                readRecipe(3);
                                break;
                            default:
                                Console.WriteLine("You have not selected a valid option!!!");
                                break;
                        }

                        //readRecipe();
                        break;

                    case 2:
                        Console.WriteLine("\n=========New recipe=========");


                        newRecipe();
                        break;

                    case 99:
                        return;

                    default:
                        Console.WriteLine("You have not selected a valid option!!!");
                        break;
                }
            }



            void makeRecipe(int ingredientCount, int stepsCount)
            {
                Console.WriteLine("\nFill in the ingredients details:");

                for(int i=0; i<ingredientCount; i++)
                {
                    Console.WriteLine("Ingredient #" + (i + 1)+":");
                    cook.setIngredients(i);
                    Console.WriteLine();
                }

                Console.WriteLine("\nFill in the cooking steps:");
                for(int i=0; i<stepsCount;i++)
                {
                    Console.WriteLine("Step #"+(i + 1)+":");
                    cook.setSteps(i);
                    Console.WriteLine() ;
                }

                Console.WriteLine();
            }

            void newRecipe()
            {
                // record data for class constructor
                Console.WriteLine("\nWrite a title for your recipe (e.g. How to cook pasta):");
                title = Console.ReadLine();

                Console.WriteLine("\nHow many ingredients do you require:");
                ingredients = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("\nHow many steps will be followed:");
                steps = Convert.ToInt32(Console.ReadLine());

                // creating an object of the 'recipe' class
                Recipe cook = new Recipe(title, ingredients+1, steps+1); //might need to add '+1' to the int n double

                Console.WriteLine("\nFill in the ingredients details:");

                for (int i = 0; i < ingredients; i++)
                {
                    Console.WriteLine("Ingredient #" + (i + 1) + ":");
                    cook.setIngredients(i);
                    Console.WriteLine();
                }

                Console.WriteLine("\nFill in the cooking steps:");
                for (int i = 0; i < steps; i++)
                {
                    Console.WriteLine("Step #" + (i + 1) + ":");
                    cook.setSteps(i);
                    Console.WriteLine();
                }

                Console.WriteLine();
            }

            void readRecipe(double x)
            {
                Console.WriteLine("\n"/*Recipe: "*/+ cook.Title +
                "\nIngredients:");

                for (int i = 0; i <= cook.ingredientCount - 1; i++)//might change '-1' to '-2'
                {
                    cook.getIngredients(i, x);

                }

                Thread.Sleep(3000);
                Console.WriteLine("\nCooking steps: ");

                for (int i = 0; i <= cook.stepsCount - 1; i++)//might change '-1' to '-2'
                {
                    cook.getSteps(i);
                }
                Console.WriteLine();
                Thread.Sleep(3000);
            }
        }

    }
}