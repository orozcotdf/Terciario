using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ColegioTerciario.Lib;

namespace ColegioTerciario.Test
{
    [TestClass]
    public class MailTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            var mail = ColegioTerciario.Lib.Mailer.SendInscripcion("nahuel.chaves@gmail.com", "asdasd", "TEST");

            Assert.AreEqual(true, mail);
        }
    }
}
