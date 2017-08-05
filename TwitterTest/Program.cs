using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweetinvi;

namespace TwitterTest
{
    class Program
    {
        static void Main(string[] args)
        {
            TwitterWorker twitterWorker = new TwitterWorker();
            Console.WriteLine("Autorization...");
            try
            {
                twitterWorker.Autorize();
            }
            catch(Exception e)
            {
                Console.WriteLine("Cann't autorise user. Error: " + e.Message);
                Console.ReadLine();
                return;
            }
            Console.WriteLine("Autorization successful");
            string currentUserName;
            do
            {
                Console.WriteLine("Enter User Name:");
                currentUserName = Console.ReadLine();
                if (currentUserName == "") return;
                if (!currentUserName.StartsWith("@"))
                    currentUserName = currentUserName.Insert(0, "@");
                try
                {
                    var jsonMap = JsonSerializer.ToJson(
                        TwitterWorker.lettersCount(
                            twitterWorker.Get5Message(currentUserName)));
                    Console.WriteLine(jsonMap);
                    Console.WriteLine("Tweeting...");
                    var message = $"{currentUserName}, статистика для последних 5 твитов:{jsonMap.ToString()}";
                    twitterWorker.Post(message);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: " + e.Message);
                }

            } while (currentUserName != "");           
        }
    }
}
