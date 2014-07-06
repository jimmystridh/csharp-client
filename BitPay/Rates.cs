using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Helpers;

namespace BitPayAPI
{
    /// <summary>
    /// Provides an interface to the BitPay server to obtain exchange rate information.
    /// </summary>
    public class Rates
    {
        private readonly BitPay _bitPay;

        /// <summary>
        /// Constructor.  Creates the Rates instance from the BitPay server response.
        /// </summary>
        /// <param name="response">The raw HTTP response from BitPay server api/rates call.</param>
        /// <param name="bitPay">used to update self.</param>
        public Rates(HttpContent response, BitPay bitPay)
        {
            dynamic obj = Json.Decode(response.ReadAsStringAsync().Result);
            _bitPay = bitPay;

            ExchangeRates = new List<Rate>();
            foreach (dynamic rateObj in obj)
            {
                ExchangeRates.Add(new Rate(rateObj.name, rateObj.code, rateObj.rate));
            }
        }

        /// <summary>
        /// Bitcoin exchange rates in a list.
        /// </summary>
        /// <returns>A list of Rate objects.</returns>
        public List<Rate> ExchangeRates { get; private set; }

	    /// <summary>
        /// Updates the exchange rates from the BitPay API.
	    /// </summary>
	    public void Update()
        {
		    ExchangeRates = _bitPay.GetRates().ExchangeRates;
	    }

        /// <summary>
        /// Returns the Bitcoin exchange rate for the given currency code.
        /// Ensure that the currency code is valid, and in all caps.
        /// </summary>
        /// <param name="currencyCode">Three letter currency code in all caps.</param>
        /// <returns>The exchange rate.</returns>
        public decimal GetRate(string currencyCode)
        {
            var rate = ExchangeRates.FirstOrDefault(r => r.Code == currencyCode);
            if (rate == null)
            {
                return 0;
            }

            return rate.ExchangeRate;
        }
    }
}
