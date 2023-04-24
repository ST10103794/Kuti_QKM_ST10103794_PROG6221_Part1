using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kuti_QKM_ST10103794_PROG6221_Part1
{

    // !!! after running the code, I figured that through recording the existing dictionaries in the list, 
    // the keys are forgotten or not recorded. hence I added a 'name' and 'step number' field in the class 
    // and struct !!!

    public interface IRecipe { }

    // interface to make both recipe steps and ingredient be recorded in one list
    public interface IRecipeElements
    { }

    // struct to record details of individual ingredients
    // a struct is like a class, but acts like a variable
    // it is not referenced, but every intsance is a new variable
    // I used it because it makes sense to have each ingredient and all it details act as a variable
    public struct IngredientDetails:IRecipeElements
    {
        public string Name;
        public string FoodGroup;
        public string MeasurementType;

        public double Measurement;
        public double calories;
    }

    // class to record recipe steps
    public class RecipeSteps:IRecipeElements
    {
        public int stepNumber;
        public string stepDescription;
    }

    public class Recipe:IRecipe
    {
        // dictionary to record each ingredient's and details to a specific key-value pair.
        // ingredient details are stored in individual struct instance
        private Dictionary<string, IngredientDetails> ingredients;

        // dictionary to record steps
        private Dictionary<int, RecipeSteps> steps;

        // SortedList to record a whole recipe with with a name and all it details into a key-value pair
        // this will keep the recipes sorted in alphabetical order
        private SortedList<string, List<IRecipeElements>> recipes;

        /**
         * constructor initialises existing dictionaries
         */
        public Recipe()
        {
            ingredients = new Dictionary<string, IngredientDetails>();
            steps = new Dictionary<int, RecipeSteps>();
            recipes = new SortedList<string, List<IRecipeElements>>();
        }


        /**
         * Create a recipe and add it to the 'recipes' dictionary
         */
        public void makeRecipe()
        {
            // necessary variables
            int loopKeep = 0;
            double myDouble;
            string input;
            string ingrName = null;
            string title;
            bool containsKey;

            // this keeps track of the amount of calories in the recipie
            double totalCalories = 0;

            // event handler subscribed to delegate, to be informed of when an event happens
            CaloriesCheck += OnCalorieNotification;

            Console.WriteLine("\nWrite a title for your recipe (e.g. How to cook pasta):");
            title = Console.ReadLine().ToLower();

            // verifies if title is already recorded
            containsKey = recipes.ContainsKey(title);
            while (containsKey)
            {
                Console.WriteLine($"\n'{title}' is already a recorded recipe. \nCreate a new one:");
                title = Console.ReadLine();
            }

            Console.WriteLine("\nHow many ingredients are required:");


            
            // below block verifies that user input is an int
            input = Console.ReadLine();
            while (!Int32.TryParse(input, out loopKeep))
            {
                Console.WriteLine("\nWrong input, it should be a number (int). \nEnter again:");
                input = Console.ReadLine();
            }

            // record ingredient details and stores in struct
            for (int i = 0; i < loopKeep; i++)
            {
                Console.WriteLine($"\nIngredient {(i+1)}.");

                Console.WriteLine("What is the name of this ingredient:");
                ingrName = Console.ReadLine().ToLower();

                containsKey = ingredients.ContainsKey(ingrName);
                while (containsKey)
                {
                    Console.WriteLine($"\n'{title}' is already a recorded ingredient. \nCreate a new one:");
                    ingrName = Console.ReadLine();
                    containsKey = ingredients.ContainsKey(ingrName);
                }

                // new instance of 'IngredientDetails' struct
                IngredientDetails ingredient = new IngredientDetails();

                // this records the name of the ingredient
                ingredient.Name = ingrName;

                Console.WriteLine("What food group is it from:");
                ingredient.FoodGroup = chooseFoodGroup();

                Console.WriteLine("What is used to measure it (litres, spoons, grams...):");
                ingredient.MeasurementType = Console.ReadLine();

                Console.WriteLine("How much of the mesurement is needed:");

                // verifies that input is a double
                input = Console.ReadLine();
                while(!Double.TryParse(input, out myDouble))
                {
                    Console.WriteLine("\nWrong input, it should be a number (double). \nEnter again:");
                    input = Console.ReadLine();
                }
                

                ingredient.Measurement = myDouble;

                Console.WriteLine("How many calories does it have:");

                // verifies that input is a double
                input = Console.ReadLine();
                while (!Double.TryParse(input, out myDouble))
                {
                    Console.WriteLine("\nWrong input, it should be a number (double). \nEnter again:");
                    input = Console.ReadLine();
                }

                ingredient.calories = myDouble;
                totalCalories += myDouble;

                // Check if total calories reach 300
                if (totalCalories >= 300)
                {
                    // Raise the calorie notification event
                    CaloriesCheck?.Invoke(title);
                }

                // adding the ingredient details with respective name to ingredients dictionary
                ingredients.Add(ingrName, ingredient);
            }

            Console.WriteLine("\nHow many steps are required:");

            // below block verifies that user input is an int
            input = Console.ReadLine();
            while (!Int32.TryParse(input, out loopKeep))
            {
                Console.WriteLine("\nWrong input, it should be a number (int). \nEnter again:");
                input = Console.ReadLine();
            }

            // record steps and stores in instance of 'RecipeSteps' class

            // this might need modification //

            for (int i = 0; i < loopKeep; i++)
            {
                Console.WriteLine("Step " + (i + 1));

                // new instance of 'RecipeSteps' class
                string description = Console.ReadLine();
                RecipeSteps step = new RecipeSteps { stepNumber = (i + 1), stepDescription = description };
                steps.Add((i + 1), step);
            }


            // new list to store recipe details
            List<IRecipeElements> recipeDetails = new List<IRecipeElements>();

            // Below has 'Cast<IRecipeElements>()>' to casts each 'IngredientDetails' object to 'IRecipeElements'
            // Since 'IngredientDetails' is a variable type, a struct, even making it implement an interface does not make it of
            // the type of the interface, it has to be cast to the interface first.
            // This enables the data to be recorded into 'recipeDetails.
            recipeDetails.AddRange(ingredients.Values.Cast<IRecipeElements>());

            // Since 'RecipeSteps' is a class, which is a reference type, it does not need to be cast to the interface
            // it implements.
            recipeDetails.AddRange(steps.Values);

            // Add the recipe details to the recipes dictionary, with the correct recipe title
            recipes.Add(title, recipeDetails);

            // Clear out the ingredients and steps dictionaries for reuse
            ingredients.Clear();
            steps.Clear();


            /*
            if (totalCalories >= 300)
            {
                Console.WriteLine($"\nTotal calories are now: {totalCalories}");
            }
            */

        }

        /**
         * list all recorded recipes
         */ 
        public void listRecipes()
        {
            int count = 1;
            foreach (var recipe in recipes)
            {
                Console.WriteLine($"{count}. {recipe.Key}");
                count++;

                Thread.Sleep(3000);
            }
        }

        /**
         * choose a food group
         */
        private string chooseFoodGroup()
        {
            string foodGroup;
            int choice;
            Console.WriteLine("\nChoose a food group:" +
                "\n1. Starchy foods" +
                "\n2. Vegetables and fruits" +
                "\n3. Dry beans, peas, lentils and soya" +
                "\n4. Chicken, fish, meat and eggs" +
                "\n5. Milk and dairy products" +
                "\n6. Fats and oil" +
                "\n7. Water.");
            
            // below block verifies that user input is an int
            foodGroup = Console.ReadLine();
            while (!Int32.TryParse(foodGroup, out choice))
            {
                Console.WriteLine("\nWrong input, it should be a number (int). \nEnter again:");
                foodGroup = Console.ReadLine();
            }

            while(choice <1 || choice >7)
            {
                Console.WriteLine("Invalid option.");
                Console.WriteLine("\nChoose a food group:" +
                "\n1. Starchy foods" +
                "\n2. Vegetables and fruits" +
                "\n3. Dry beans, peas, lentils and soya" +
                "\n4. Chicken, fish, meat and eggs" +
                "\n5. Milk and dairy products" +
                "\n6. Fats and oil" +
                "\n7. Water.");

                // below block verifies that user input is an int
                string input = Console.ReadLine();
                while (!Int32.TryParse(input, out choice))
                {
                    Console.WriteLine("\nWrong input, it should be a number (int). \nEnter again:");
                    input = Console.ReadLine();
                }
            }

            switch(choice)
            {
                case 1:
                    foodGroup = "Starchy foods";
                    break;
                case 2:
                    foodGroup = "Vegetables and fruits";
                    break;
                case 3:
                    foodGroup = "Dry beans, peas, lentils and soya";
                    break;
                case 4:
                    foodGroup = "Chicken, fish, meat and eggs";
                    break;
                case 5:
                    foodGroup = "Milk and dairy products";
                    break;
                case 6:
                    foodGroup = "Fats and oil";
                    break;
                case 7:
                    foodGroup = "Water";
                    break;
            }

            return foodGroup;

        }

        /**
         * verifies if recipe exists
         */
        public string containsRecipe(string recipeName)
        {
            bool containsKey;

            // verifies if input is already recorded
            containsKey = recipes.ContainsKey(recipeName);
            while (!containsKey && (recipeName != "stop") )
            {
                Console.WriteLine($"\n'{recipeName}' is not a recorded recipe. \nWrite a correct input, or 'stop' to return:");
                recipeName = Console.ReadLine().ToLower();
                // verifies if input is already recorded
                containsKey = recipes.ContainsKey(recipeName);
            }

            if ( recipeName == "stop" ) 
            {
                return "stop";
            }
            else
            {
                return recipeName;
            }
        }

        /**
         * displays the ingredients and steps of a specific recipe
         */
        public void displayRecipe(string recipeName, double amount)
        {
            // stores all the details of the recipe in 'recipeDetails
            List<IRecipeElements> recipeDetails = recipes[recipeName];

            Console.WriteLine($"Recipe: {recipeName}\n");

            // Display ingredient details
            Console.WriteLine("Ingredients:");
            foreach (var recipeElement in recipeDetails.OfType<IngredientDetails>())
            {
                Console.WriteLine($"\n Ingredient: {recipeElement.Name}" +
                    $"\nFood Group: {recipeElement.FoodGroup} " +
                    $"\nMeasurements: {recipeElement.Measurement * amount} {recipeElement.MeasurementType}" +
                    $"\nCalories: {recipeElement.calories * amount}\n");

                // pauses program
                Thread.Sleep(3000);
            }

            // Display recipe steps
            Console.WriteLine("Steps:");
            foreach (var recipeElement in recipeDetails.OfType<RecipeSteps>())
            {
                Console.WriteLine($"Step {recipeElement.stepNumber}: {recipeElement.stepDescription}");

                // pauses program
                Thread.Sleep(3000);
            }

        }


        /**
         * Delegate that gets activated when event 'CaloriesCheck' happens.
         * As in JavaScript, this act's like a 'callback' (I've studied JS on my personal time, so I know).
         * It is subscribed to by an event handler, then it alerts the event handler of when the event happens so that the handler
         * can be executed.
         */
        public delegate void CaloriesCheckEventHandler(string recipeName);

        /**
         * event associated to 'CaloriesCheckEventHandler'
         */
        public event CaloriesCheckEventHandler CaloriesCheck;


        /**
         * method/event handler
         */
        public static void OnCalorieNotification(string recipeName)
        {
            Console.WriteLine($"\nCalories in '{recipeName}' have reached 300!");

            // pauses program
            Thread.Sleep(2000);
        }

    }
}
