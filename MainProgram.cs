using System;
using System.Threading.Tasks;
using TeleSharpPlus;
using ReceipeBot.API;
using TeleSharpPlus.Commands;
using System.Threading;
using ReceipeBot.Handlers;
using System.Text;

namespace ReceipeBot
{
    class MainProgram
    {
        const string BOT_TOKEN = "DO_NOT_PUBLISH_YOUR_KEYS";
        public static void Main()
        {
            Console.ResetColor();
            MainFunction().GetAwaiter().GetResult();
        }
        private static async Task MainFunction()
        {
            try
            {
                SQLHandler.Start();
                BotStart();
                await Task.Delay(-1);
            }
            finally
            {
                SQLHandler.Close();
            }
        }
        private static void BotStart()
        {
            var bot = new BotClient(new BotSettings(BOT_TOKEN, 3000) { WaitTime = 30 });
            bot.OnCommand += async (bot, ex) =>
            {
                new Thread(x => Handler(bot, ex)).Start();
            };
        }

        private static async void Handler(BotClient bot, ExecutedCommandEvArgs ev)
        {
            try
            {
                string Response;
                switch (ev.Command.ToLower())
                {
                    case "start":
                        {
                            Response = "If you want to start cooking just print \n <u>/random</u> to get random recipe\n or \n<u>/search</u> if you want to find somethink more suitable to you";
                            SQLHandler.AddUser(ev.message.from.id, ev.message.from.first_name);
                            break;
                        }
                    case "random":
                        Response = await APIHandler.GetRandom();
                        break;
                    case "search":
                        Response = await Algorithm.SelectSearch.MainTask(bot, ev.message.chat);
                        break;
                    case "bibaboba2dolbaeba":
                        {
                            Debug.Log(ev.args[0]);
                            if (ev.args[0].Length > 0)
                            {
                                SQLHandler.SpamToAll(new StringBuilder().AppendJoin(' ', ev.args).ToString());
                                Response = "Sucess";
                            }
                            else
                            {
                                Response = "Unable To execute Command";
                            }
                            break;
                        }
                    default:
                        Response = "Unknown Command";
                        break;
                }
                ev.message.chat.SendMessage(Response);
            }
            catch (Exception ex) { ev.message.chat.SendMessage("Problem, try again later"); Debug.Error(ex.Message + "\n\t" + ex.StackTrace); }
        }
    }
}
