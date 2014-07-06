using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BitPayAPI;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BitPayTest.Unittests
{
    [TestClass]
    public class InvoiceTests
    {
        [TestMethod]
        public void ctor_CurrentCultureUsesCommaAsDecimalSeparator_ParsesCorrectly()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("sv-SE");
            dynamic d = new ExpandoObject();
            d.id = "";
            d.url = "";
            d.status = "";
            d.btcPrice = "1.23";
            d.price = "2.34";
            d.currency = "SEK";

            var invoice = new Invoice(d);

            Assert.AreEqual(1.23,invoice.BtcPrice);
            Assert.AreEqual(2.34,invoice.Price);
        }

        [TestMethod]
        public void ctor_StatusProvided_ParsesCorrectly()
        {
            dynamic d = new ExpandoObject();
            d.id = "";
            d.url = "";
            d.status = "new";
            d.btcPrice = "1.23";
            d.price = "2.34";
            d.currency = "SEK";

            var invoice = new Invoice(d);

            Assert.AreEqual(InvoiceStatus.New,invoice.Status);
        }
    }
}
