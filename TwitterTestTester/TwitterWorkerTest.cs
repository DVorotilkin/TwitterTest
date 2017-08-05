using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using TwitterTest;

namespace TwitterTestTester
{
    [TestClass]
    public class TwitterWorkerTest
    {
        [TestMethod]
        public void LettersCountTest()
        {
            string testStr = new String(Enumerable.Range('a', 'z' - 'a').Select(i => (Char)i).ToArray());
            testStr += testStr;
            var result = TwitterWorker.lettersCount(testStr);
            Assert.AreEqual(testStr.Length / 2, result.Count, "In map length");
            foreach (var i in result)
            {
                Assert.AreEqual(i.Value, 2f/testStr.Length, $"In count of {i.Key}");
            }
        }
    }
}

