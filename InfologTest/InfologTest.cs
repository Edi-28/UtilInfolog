using System;
using Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace InfologTest
{
    [TestClass]
    public class InfologTest
    {
        [TestMethod]
        public void TestSingleton()
        {
            Infolog infolog1 = Infolog.GetInstance();
            Infolog infolog2 = Infolog.GetInstance();

            Assert.IsTrue(infolog1.Equals(infolog2));
        }

        [TestMethod]
        public void TestClearedInfologHasNoMaxMessageType()
        {
            Infolog.Error("Error");

            Assert.IsFalse(Infolog.GetInstance().MaxMessageType == InfologMessageType.None);

            Infolog.GetInstance().Clear();
            Assert.IsTrue(Infolog.GetInstance().MaxMessageType == InfologMessageType.None);
        }

        [TestMethod]
        public void TestMaxMessageTypeInfo()
        {
            Infolog.GetInstance().Clear();
            Infolog.Info("Test");
            Assert.IsTrue(Infolog.GetInstance().MaxMessageType == InfologMessageType.Info);
        }

        [TestMethod]
        public void TestMaxMessageTypeWarning()
        {
            Infolog.GetInstance().Clear();
            Infolog.Info("Test");
            Infolog.Warning("Test");
            Assert.IsTrue(Infolog.GetInstance().MaxMessageType == InfologMessageType.Warning);
        }

        [TestMethod]
        public void TestMaxMessageTypeError()
        {
            Infolog.GetInstance().Clear();
            Infolog.Info("Test");
            Infolog.Warning("Test");
            Infolog.Error("Test");
            Assert.IsTrue(Infolog.GetInstance().MaxMessageType == InfologMessageType.Error);
        }

        [TestMethod]
        public void TestPrefix()
        {
            Infolog.GetInstance().Clear();
            Infolog.SetPrefix("First");
            Infolog.SetPrefix("Second");
            Infolog.Info("InfoMessage");
            Infolog.Info("AnotherInfoMessage");
            Infolog.SetPrefix("Third");
            Infolog.Info("LastInfoMessage");

            var text = Infolog.GetInstance().GetInfologText();

            Assert.IsTrue(text == "First\tSecond\tInfoMessage\nFirst\tSecond\tAnotherInfoMessage\nFirst\tSecond\tThird\tLastInfoMessage\n", text);
        }

        [TestMethod]
        public void TestAdvancedPrefix()
        {
            Infolog.GetInstance().Clear();
            Infolog.SetPrefix("Test");

            this.infoLevel1();

            Infolog.Error("Ende");

            var text = Infolog.GetInstance().GetInfologText();

            Assert.IsTrue(text == "Test\tLevel 1\tLevel 2\tError Level 2\nTest\tLevel 1\tLevel 3\tError Level 3\nTest\tEnde\n",text);

            this.infoLevel1();

            var text1 = Infolog.GetInstance().GetInfologText();

            Assert.IsTrue(text1 == "Test\tLevel 1\tLevel 2\tError Level 2\nTest\tLevel 1\tLevel 3\tError Level 3\nTest\tEnde\nTest\tLevel 1\tLevel 2\tError Level 2\nTest\tLevel 1\tLevel 3\tError Level 3\n", text1);

        }

        private void infoLevel1()
        {
            Infolog.SetPrefix("Level 1");

            this.infoLevel2();

            this.infoLevel3();
        }

        private void infoLevel2()
        {
            Infolog.SetPrefix("Level 2");

            Infolog.Error("Error Level 2");
        }

        private void infoLevel3()
        {
            Infolog.SetPrefix("Level 3");

            Infolog.Error("Error Level 3");
        }
    }
}
