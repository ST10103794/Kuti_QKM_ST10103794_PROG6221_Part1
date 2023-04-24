using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kuti_QKM_ST10103794_PROG6221_Part1
{
    internal class Recipe
    {
        // necessary variable declarations
        public int ingredientCount = 0;
        public int stepsCount = 0;

        protected string title = "";
        protected string[] steps;
        protected string[] ingredients;
        protected string[] measureType;
        protected double[] measurement;

        // constructor requiring elements
        public Recipe(string title, int howMuchStuff, int howManySteps)
        {
            this.title = title;

            // initialising arrays
            ingredientCount = howMuchStuff;
            ingredients = new string[ingredientCount];
            measurement = new double[ingredientCount];
            measureType = new string[ingredientCount];

            stepsCount = howManySteps;
            steps = new string[stepsCount];

        }
        
        // return protected title out of this class
        public string Title { get { return title; } }

        // fill in the ingredient it details within arrays
        public void setIngredients(int x)
        {
            Console.WriteLine("\nName:");
            this.ingredients[x] = Console.ReadLine();

            Console.WriteLine("\nMeasured by (litres, spoons, etc):");
            this.measureType[x] = Console.ReadLine();

            Console.WriteLine("\nHow many of the measurement needed:");
            this.measurement[x] = Convert.ToDouble(Console.ReadLine());
        }

        // fill in the steps array 
        public void setSteps(int y) => steps[y] = Console.ReadLine();

        /// <summary>
        /// get a specific ingredient and details
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void getIngredients(int x, double y) => Console.WriteLine("* " + (this.measurement[x] * y) + " " + this.measureType[x] + " of " + this.ingredients[x]);

        /// <summary>
        /// get specific cooking step
        /// </summary>
        /// <param name="y"></param>
        public void getSteps(int y) => Console.WriteLine("Step " + y + ":\t-" + steps[y]);
    }
}
