using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ReceipeBot.Entities
{
    public class Response
    {
        [JsonProperty] public Recipe[] meals;
        public string GetFirst()
        {
            return meals[0].ToString();
        }
        public string GetInlineList()
        {
            try
            {
                string temp = " ";
                if (meals?.Length > 0)
                    if ((meals[0]?.GetSource()) != null)
                    {
                        for (int i = 0; i < meals.Length & i < 20; i++)
                        {
                            var x = meals[i];
                            temp += $"<a href=\"{x.GetSource()}\">[{x.GetID()}] {x.GetName()}</a>\n";
                        }
                    }
                    else
                    {
                        for (int i = 0; i < meals.Length & i < 20; i++)
                        {
                            var x = meals[i];
                            temp += $"[{x.GetID()}] {x.GetName()}\n";
                        }
                    }
                return temp;
            }
            catch (Exception ex)
            {
                Debug.Error(ex.Message);
                return " ";
            }
        }
    }
    public class Recipe
    {
        //Used [JsonProperty] here, bcs there is too much data, and if i use [public] it will create a mess in property explorer
        #region Properties
        [JsonProperty] string idMeal; [JsonProperty] string strMeal; [JsonProperty] string strDrinkAlternate; [JsonProperty] string strCategory; [JsonProperty] string strArea; [JsonProperty] string strInstructions; [JsonProperty] string strMealThumb; [JsonProperty] string strTags; [JsonProperty] string strIngredient1; [JsonProperty] string strIngredient2; [JsonProperty] string strIngredient3; [JsonProperty] string strIngredient4; [JsonProperty] string strIngredient5; [JsonProperty] string strIngredient6; [JsonProperty] string strIngredient7; [JsonProperty] string strIngredient8; [JsonProperty] string strIngredient9; [JsonProperty] string strIngredient10; [JsonProperty] string strIngredient11; [JsonProperty] string strIngredient12; [JsonProperty] string strIngredient13; [JsonProperty] string strIngredient14; [JsonProperty] string strIngredient15; [JsonProperty] string strIngredient16; [JsonProperty] string strIngredient17; [JsonProperty] string strIngredient18; [JsonProperty] string strIngredient19; [JsonProperty] string strIngredient20; [JsonProperty] string strMeasure1; [JsonProperty] string strMeasure2; [JsonProperty] string strMeasure3; [JsonProperty] string strMeasure4; [JsonProperty] string strMeasure5; [JsonProperty] string strMeasure6; [JsonProperty] string strMeasure7; [JsonProperty] string strMeasure8; [JsonProperty] string strMeasure9; [JsonProperty] string strMeasure10; [JsonProperty] string strMeasure11; [JsonProperty] string strMeasure12; [JsonProperty] string strMeasure13; [JsonProperty] string strMeasure14; [JsonProperty] string strMeasure15; [JsonProperty] string strMeasure16; [JsonProperty] string strMeasure17; [JsonProperty] string strMeasure18; [JsonProperty] string strMeasure19; [JsonProperty] string strMeasure20; [JsonProperty] string strSource;
        #endregion
        string GetInlineImage() => $"<a href=\"{strMealThumb}\">Image</a>";

        public int GetID() => int.Parse(idMeal);

        public string GetSource() => strSource;

        public List<string> GetIngridientsArray()
        {
            //I'm not stupid ok?
            //I'm just not smart yet, so i dont know how to change API on other's site
            List<string> temp = new List<string>();
            temp.Add(strIngredient1 + " - " + strMeasure1);
            temp.Add(strIngredient2 + " - " + strMeasure2);
            temp.Add(strIngredient3 + " - " + strMeasure3);
            temp.Add(strIngredient4 + " - " + strMeasure4);
            temp.Add(strIngredient5 + " - " + strMeasure5);
            temp.Add(strIngredient6 + " - " + strMeasure6);
            temp.Add(strIngredient7 + " - " + strMeasure7);
            temp.Add(strIngredient8 + " - " + strMeasure8);
            temp.Add(strIngredient9 + " - " + strMeasure9);
            temp.Add(strIngredient10 + " - " + strMeasure10);
            temp.Add(strIngredient11 + " - " + strMeasure11);
            temp.Add(strIngredient12 + " - " + strMeasure12);
            temp.Add(strIngredient13 + " - " + strMeasure13);
            temp.Add(strIngredient14 + " - " + strMeasure14);
            temp.Add(strIngredient15 + " - " + strMeasure15);
            temp.Add(strIngredient16 + " - " + strMeasure16);
            temp.Add(strIngredient17 + " - " + strMeasure17);
            temp.Add(strIngredient18 + " - " + strMeasure18);
            temp.Add(strIngredient19 + " - " + strMeasure19);
            temp.Add(strIngredient20 + " - " + strMeasure20);
            foreach (var x in temp)
                if (x.Length < 4)
                    temp.Remove(x);

            return temp;
        }
        string GetIngridientsLine()
        {
            string temp = "<u>Ingridients:</u>\n";
            var indgridients = GetIngridientsArray();
            for (int i = 0; i < indgridients.Count(); i += 2)
                temp += ($"{indgridients[i]},  {indgridients[i + 1]}\n");
            return temp.Split(",  -", StringSplitOptions.RemoveEmptyEntries)[0].Trim().Replace("&", "and");
        }
        public string GetName() => $"<b><u>{strMeal.Replace("&", "and")}</u></b>";
        private string GetInstructions() => "<u>Instructions:</u>\n" + strInstructions.Replace("&", "and");
        public override string ToString() => $"{GetName()}\n{GetIngridientsLine()}\n{GetInstructions()}\n{GetInlineImage()}";
    }
}
