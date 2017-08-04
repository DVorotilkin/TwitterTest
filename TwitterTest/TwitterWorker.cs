using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweetinvi;
using Tweetinvi.Parameters;

namespace TwitterTest
{
    class TwitterWorker
    {
        public void Autorize()
        {
            SettingsWorker settingsWorker = new SettingsWorker("Settings.ini");

            settingsWorker.LoadSettings();
            
            Auth.SetUserCredentials(settingsWorker.settings.consumerKey,
                                    settingsWorker.settings.consumerSecret,
                                    settingsWorker.settings.userAccessToken,
                                    settingsWorker.settings.userAccessSecret);

            if (User.GetAuthenticatedUser() == null) //invalid Credentials
            {
                try
                {
                    var appCredentials = new Tweetinvi.Models.TwitterCredentials(
                        settingsWorker.settings.consumerKey,
                        settingsWorker.settings.consumerSecret);
                    var authenticationContext = AuthFlow.InitAuthentication(appCredentials);
                    Process.Start(authenticationContext.AuthorizationURL);
                    var pinCode = Console.ReadLine();
                    var newCredentials = AuthFlow.CreateCredentialsFromVerifierCode(pinCode, authenticationContext);
                    Auth.SetCredentials(newCredentials);
                    settingsWorker.settings.userAccessToken = newCredentials.AccessToken;
                    settingsWorker.settings.userAccessSecret = newCredentials.AccessTokenSecret;
                    settingsWorker.SaveSettings();
                }
                catch
                {
                    throw new Exception("Autorization Failed!");
                }
            }
                
        }

        public void Post(string message)
        {
            
            var user = User.GetAuthenticatedUser();
            if (user == null)
                throw new Exception("Not Autorized!");
            string chunk;
            while (message.Length != 0)
            {
                chunk = message.Substring(0, 140);
                message = message.Remove(0, 140);
                Tweet.PublishTweet(chunk);
            }
        }

        public string Get5Message(string userName)
        {
            string messagesText = string.Empty;
            var userId = User.GetUserFromScreenName(userName);
            if (userId == null)
                throw new Exception("User not found");
            var lastTweets = Timeline.GetUserTimeline(userId, 5).ToArray();
            int mediaLength = 0;
            foreach (var i in lastTweets)
            {
                foreach (var j in i.Media)
                    mediaLength += j.DisplayURL.Length;
                if (mediaLength > 0)
                    messagesText += i.Text.Remove(i.Text.Length - mediaLength + 2);
                else
                    messagesText += i.Text;
                mediaLength = 0;
            }
            return messagesText;
        }

        public Dictionary<char, uint> lettersCount(string input)
        {
            Dictionary<char, uint> map = new Dictionary<char, uint>();
            foreach (var i in input)
            {
                if (char.IsLetter(i))
                    if (map.ContainsKey(i))
                        map[i]++;
                    else
                        map.Add(i, 1);
            }
            return map.OrderBy(i =>i.Key).ToDictionary((keyItem) => keyItem.Key, (valueItem) => valueItem.Value);
        }

    }
}
