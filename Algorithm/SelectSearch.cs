using Newtonsoft.Json;
using System.Threading.Tasks;
using ReceipeBot.API;
using TeleSharpPlus;
using TeleSharpPlus.Entities;
using TeleSharpPlus.Entities.Keyboard;

namespace ReceipeBot.Algorithm
{
    public class SelectSearch
    {
        public static async Task<string> MainTask(BotClient bot, Chat chat)
        {
            var keyboard = (new ReplyKeyboardMarkup()
            {
                keyboard = new ReplyKeyboardButton[1][]
                {
                    new ReplyKeyboardButton[4] { new ReplyKeyboardButton { text = "1) Name"}, new ReplyKeyboardButton { text= "2) Ingridient"},new ReplyKeyboardButton { text= "3) Id"},new ReplyKeyboardButton { text= "4) Category"} },
                }
            });
            await BotClient.SendMessageKeyboard(chat.id, "Select Search Type", keyboard);
            var selectButResult = await bot.WaitForMessage(chat.id);
            BotClient.RemoveKeyboard(chat.id, "Print word, what you want to find\n With <b>ID</b>: [1234] - 1234 \n With <b>Ingridient</b>: Some Ingridients, like onion,butter ...\n With <b>Name</b>: Some Product Name");
            string search = (await bot.WaitForMessage(chat.id)).text.ToLower().Replace(' ', '_');
            string result = "";
            switch (selectButResult.text.Remove(1))
            {
                case "1":
                    result = await APIHandler.SearchByName(search);
                    break;
                case "2":
                    result = await APIHandler.SearchByIngridient(search);
                    break;
                case "4":
                    result = await APIHandler.SearchByCategory(search);
                    break;
                case "3":
                    result = await APIHandler.SearchById(search);
                    break;
            }
            return result;
        }
    }
}
