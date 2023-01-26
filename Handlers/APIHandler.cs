using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using ReceipeBot.Entities;
using TeleSharpPlus.Commands;

namespace ReceipeBot.API
{
    public class APIHandler
    {
        public static async Task<string> GetRandom()
        {
            var req = new Uri("https://www.themealdb.com/api/json/v1/1/random.php");
            var wr = WebRequest.Create(req);
            string result;
            using (var RS = wr.GetResponse().GetResponseStream())
            using (var SR = new StreamReader(RS))
            {
                var s = await SR.ReadToEndAsync();
                result = JsonConvert.DeserializeObject<Response>(s).GetFirst();
            }
            return result.ToString();
        }
        public static async Task<string> SearchByName(string name)
        {
            try
            {
                var req = new Uri("https://www.themealdb.com/api/json/v1/1/search.php?s=" + name);
                var wr = WebRequest.Create(req);
                string SearchByName;
                using (var RS = wr.GetResponse().GetResponseStream())
                using (var SR = new StreamReader(RS))
                {
                    var s = await SR.ReadToEndAsync();
                    SearchByName = JsonConvert.DeserializeObject<Response>(s).GetList();
                    return SearchByName;
                }
            }
            catch (Exception ex)
            {
                Debug.Error(ex.Message);
                return " ";
            }
        }
        public static async Task<string> SearchByIngridient(string name)
        {
            try
            {
                var req = new Uri("https://www.themealdb.com/api/json/v1/1/filter.php?i=" + name);
                var wr = WebRequest.Create(req);
                string SearchByIngridient;
                using (var RS = wr.GetResponse().GetResponseStream())

                using (var SR = new StreamReader(RS))
                {
                    var s = await SR.ReadToEndAsync();
                    SearchByIngridient = JsonConvert.DeserializeObject<Response>(s).GetList();
                    return SearchByIngridient;
                }
            }
            catch (Exception ex)
            {
                Debug.Error(ex.Message);
                return " ";
            }
        }
        public static async Task<string> SearchByCategory(string name)
        {
            try
            {
                var req = new Uri("https://www.themealdb.com/api/json/v1/1/filter.php?c=" + name);
                var wr = WebRequest.Create(req);
                string SearchByIngridient;
                using (var RS = wr.GetResponse().GetResponseStream())
                using (var SR = new StreamReader(RS))
                {
                    var s = await SR.ReadToEndAsync();
                    SearchByIngridient = JsonConvert.DeserializeObject<Response>(s).GetList();
                    return SearchByIngridient;
                }
            }
            catch (Exception ex)
            {
                Debug.Error(ex.Message);
                return " ";
            }
        }
        public static async Task<string> SearchById(string Id)
        {
            try
            {
                var req = new Uri("https://www.themealdb.com/api/json/v1/1/lookup.php?i=" + Id);
                var wr = WebRequest.Create(req);
                string SearchByName;
                using (var RS = wr.GetResponse().GetResponseStream())
                using (var SR = new StreamReader(RS))
                {
                    var s = await SR.ReadToEndAsync();
                    SearchByName = JsonConvert.DeserializeObject<Response>(s).GetFirst();
                    return SearchByName;
                }
            }
            catch (Exception ex)
            {
                Debug.Error(ex.Message);
                return " ";
            }
        }
    }
}
