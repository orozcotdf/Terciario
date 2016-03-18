using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ColegioTerciarioTests
{
    [TestClass]
    public class MailTests
    {
        [TestMethod]
        public void InscripcionMails()
        {
            var mail = ColegioTerciario.Lib.Mailer.SendInscripcion("nahuel.chaves@gmail.com", "testUrl", "testHTML");

            Assert.IsTrue(mail);
        }
    }
}
