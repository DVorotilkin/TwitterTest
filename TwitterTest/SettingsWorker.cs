using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace TwitterTest
{
    [DataContract]
    public class Settings
    {
        [DataMember]
        public string consumerKey;
        [DataMember]
        public string consumerSecret;
        [DataMember]
        public string userAccessToken;
        [DataMember]
        public string userAccessSecret;

        public Settings(string consumerKey, string consumerSecret,
              string userAccessToken, string userAccessSecret)
        {
            this.consumerKey = consumerKey;
            this.consumerSecret = consumerSecret;
            this.userAccessToken = userAccessToken;
            this.userAccessSecret = userAccessSecret;
        }
    }

    public class SettingsWorker
    {
        public Settings settings { get; set; }

        private string settingsFileName;
        public SettingsWorker(string settingsFileName)
        {
            this.settingsFileName = settingsFileName;
            settings = new Settings("", "", "", "");
        }

        public void SaveSettings()
        {
            using (System.IO.Stream writer = new FileStream(settingsFileName, FileMode.Create))
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Settings));
                ser.WriteObject(writer, settings);
            }
        }

        public void LoadSettings()
        {

            using (System.IO.Stream reader = new FileStream(settingsFileName, FileMode.Open))
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Settings));
                settings = (Settings)ser.ReadObject(reader);
            }
        }
    }


}
