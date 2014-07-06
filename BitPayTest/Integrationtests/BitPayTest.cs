using BitPayAPI;
using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BitPayTest
{
    [TestClass]
    public class BitPayTest
    {
        private const string ApiKey = "Your BitPay API Key";
        
        [TestMethod]
        public void testShouldGetInvoiceId()
        {
            const decimal price = 100.0m;
            var bitpay = new BitPay(ApiKey);
            var basicInvoice = bitpay.CreateInvoice(price, "USD");

            Assert.IsNotNull(basicInvoice.Id, "Invoice created with id=NULL");
            Assert.IsNotNull(basicInvoice.Url, "Invoice created with url=NULL");
            Assert.IsNotNull(basicInvoice.Status, "Invoice created with status=NULL");
            Assert.IsNotNull(basicInvoice.BtcPrice, "Invoice created with btcPrice=NULL");
        }

        [TestMethod]
        public void testShouldCreateInvoiceOneTenthBTC()
        {
            const decimal price = 0.1m;

            var bitpay = new BitPay(ApiKey);
            Invoice invoice = bitpay.CreateInvoice(price, "BTC");

            Assert.AreEqual(price, invoice.BtcPrice, "Invoice not created correctly: 0.1BTC");
        }

        [TestMethod]
        public void testShouldCreateInvoice100USD()
        {
            const decimal price = 100.0m;

            var bitpay = new BitPay(ApiKey);
            Invoice invoice = bitpay.CreateInvoice(price, "USD");

            Assert.AreEqual(price, invoice.Price, "Invoice not created correctly: 100USD");
        }

        [TestMethod]
        public void testShouldCreateInvoice10EUR()
        {
            const decimal price = 10.0m;

            var bitpay = new BitPay(ApiKey);
            Invoice invoice = bitpay.CreateInvoice(price, "EUR");

            Assert.AreEqual(price, invoice.Price, "Invoice not created correctly: 10EUR");
        }

        [TestMethod]
        public void testShouldGetInvoice()
        {
            const decimal price = 10.0m;

            var bitpay = new BitPay(ApiKey);
            Invoice invoice = bitpay.CreateInvoice(price, "EUR");
            Invoice retreivedInvoice = bitpay.GetInvoice(invoice.Id);

            Assert.AreEqual(invoice.Id, retreivedInvoice.Id, "Expected invoice not retreived");
        }

        [TestMethod]
        public void testShouldCreateInvoiceWithAdditionalParams()
        {
            const decimal price = 100.0m;
            var parameters = new InvoiceParams
            {
                BuyerName = "Satoshi",
                BuyerEmail = "satoshi@bitpay.com",
                FullNotifications = true,
                NotificationEmail = "satoshi@bitpay.com"
            };
            var bitpay = new BitPay(ApiKey);

            Invoice invoice = bitpay.CreateInvoice(price, "USD", parameters);

            Assert.IsNotNull(invoice, "Invoice not created");
        }
	
        [TestMethod]
        public void testShouldGetExchangeRates() 
        {
            var bitpay = new BitPay(ApiKey);		
                
            Rates rates = bitpay.GetRates();		

            Assert.IsNotNull(rates.ExchangeRates, "Exchange rates not retrieved");
        }

        [TestMethod]
        public void testShouldGetUSDExchangeRate()
        {
            var bitpay = new BitPay(ApiKey);		
            Rates rates = bitpay.GetRates();

            decimal rate = rates.GetRate("USD");

            Assert.IsTrue(rate != 0, "Exchange rate not retrieved: USD");
        }
	
        [TestMethod]
        public void testShouldGetEURExchangeRate()
        {
            var bitpay = new BitPay(ApiKey);		
            Rates rates = bitpay.GetRates();

            decimal rate = rates.GetRate("EUR");

            Assert.IsTrue(rate != 0, "Exchange rate not retrieved: EUR");
        }
	
        [TestMethod]
        public void testShouldGetCNYExchangeRate() 
        {
            var bitpay = new BitPay(ApiKey);
            Rates rates = bitpay.GetRates();

            decimal rate = rates.GetRate("CNY");

            Assert.IsTrue(rate != 0, "Exchange rate not retrieved: CNY");
        }
	
        [TestMethod]
        public void testShouldUpdateExchangeRates() 
        {
            var bitpay = new BitPay(ApiKey);		
            Rates rates = bitpay.GetRates();		
            
            rates.Update();
		
            Assert.IsNotNull(rates.ExchangeRates, "Exchange rates not retrieved after update");
        }
    }
}
