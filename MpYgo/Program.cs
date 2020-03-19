using System;
using System.IO;
using System.Net;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;

namespace MpYgo
{
    class Program
    {
        private static string Token = "1138986549:AAFtLi9AzPg8Ajaw-YVKHG9QVqIizhaqzt0";

        public static void Main(string[] args)
        {
            var client = new TelegramBotClient(Token);

            client.OnMessage += BotOnMessageReceived;

            client.StartReceiving();

            Console.ReadLine();
        }


        public static async void BotOnMessageReceived(object sender, MessageEventArgs messageEventArgs)
        {
            var message = messageEventArgs.Message;

            if (message == null || message.Type != MessageType.Text)
                return;

            var cartInfo = GetCardInfo(message.Text);


            Console.WriteLine(cartInfo);
        }

        public static string GetCardInfo(string cardId)
        {
            try
            {

                var url = "http://220.134.173.17/gameking/card/ocg_show.asp?call_no=";
                var request = WebRequest.Create($"{url}{cardId}");

                request.Method = "GET";
                request.ContentType = "text/html; charset=big5";

                using (var httpResponse = (HttpWebResponse) request.GetResponse())
                {
                    var response = httpResponse.GetResponseStream();
                    if (response == null)
                        return string.Empty;
                    using (var streamReader = new StreamReader(response))
                    {
                        var result = streamReader.ReadToEnd();

                        return result;
                    }

                }
            }
            catch
            {
                //ignore
                return string.Empty;
            }
        }
    }
}
