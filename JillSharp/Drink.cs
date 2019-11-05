using System;
using System.Collections.Generic;
using System.Text;

namespace JillSharp
{
    public class Drink
    {
        public string DrinkName { get; set; }
        public string DrinkImage { get; set; }
        public string DrinkRecipe { get; set; }

        public Drink(String DrinkName, String DrinkImage, String DrinkRecipe)
        {
            this.DrinkName = DrinkName;
            this.DrinkImage = DrinkImage;
            this.DrinkRecipe = DrinkRecipe;
        }



        
    }
}
