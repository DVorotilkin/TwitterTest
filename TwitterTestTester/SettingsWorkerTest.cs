using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TwitterTest;
using System.IO;
using System.Text;

namespace TwitterTestTester
{
    [TestClass]
    public class SettingsWorkerTester
    {
        string settingsFileName = "..//..//TestSettings.ini";
        const string consumerKey = "GsN18wtO5x3S6TMr7spFz4qMc";
        const string consumerSecret = "43HHAMGtitcMKvWxK7CJ8eyVpEpiK9zUbyU4KSf4da1aFSRxTG";
        const string userAccessToken = "892343342083780609-06JWHPF7d680xo64iGukHlVkeaGeaGE";
        const string userAccessSecret = "2ioYuJZIDEF8FTrWUifrzjsWYObqdiPSrt4Ic9iOEGuZJ";

        [TestMethod]
        public void LoadSettingsTest()
        {
            SettingsWorker settingsWorker = new SettingsWorker(settingsFileName);
            try
            {
                settingsWorker.LoadSettings();
                Assert.AreEqual(consumerKey, settingsWorker.settings.consumerKey);
                Assert.AreEqual(consumerSecret, settingsWorker.settings.consumerSecret);
                Assert.AreEqual(userAccessToken, settingsWorker.settings.userAccessToken);
                Assert.AreEqual(userAccessSecret, settingsWorker.settings.userAccessSecret);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void SaveSettingsTest()
        {
            SettingsWorker settingsWorker = new SettingsWorker(settingsFileName);
            Settings settings = new Settings(
                consumerKey,
                consumerSecret,
                userAccessToken,
                userAccessSecret);
            settingsWorker.settings = settings;
            try
            {
                settingsWorker.SaveSettings();
                byte[] expected = Encoding.ASCII.GetBytes("{ \"consumerKey\":\"GsN18wtO5x3S6TMr7spFz4qMc\",\"consumerSecret\":\"43HHAMGtitcMKvWxK7CJ8eyVpEpiK9zUbyU4KSf4da1aFSRxTG\",\"userAccessSecret\":\"2ioYuJZIDEF8FTrWUifrzjsWYObqdiPSrt4Ic9iOEGuZJ\",\"userAccessToken\":\"892343342083780609-06JWHPF7d680xo64iGukHlVkeaGeaGE\"}");
                byte[] buf = new byte[expected.Length];
                using (System.IO.Stream reader = new FileStream(settingsFileName, FileMode.Open))
                {
                    var input = reader.Read(buf, 0, buf.Length);
                    Assert.AreEqual(expected.ToString(), buf.ToString());
                }
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void LoadSettingsFileNotExist()
        {
            string fileName = "NotExistFileName";
            if (File.Exists(fileName))
                File.Delete(fileName);
            SettingsWorker settingsWorker = new SettingsWorker(fileName);
            try
            {
                settingsWorker.LoadSettings();
                Assert.AreEqual(consumerKey, settingsWorker.settings.consumerKey);
                Assert.AreEqual(consumerSecret, settingsWorker.settings.consumerSecret);
                Assert.AreEqual("", settingsWorker.settings.userAccessToken);
                Assert.AreEqual("", settingsWorker.settings.userAccessSecret);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }
    }
}
