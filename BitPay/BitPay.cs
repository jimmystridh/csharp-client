using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Helpers;

namespace BitPayAPI
{
    /// <summary>
    /// Provides an abstraction of the BitPay server.
    /// </summary>
    public class BitPay
    {
        private const string BaseUrl = "https://bitpay.com/api/";

	    private readonly HttpClient _client;
	    private readonly string _auth;

        /// <summary>
        /// Constructor. Baselines the API key and currencies for all invoices created using this instance.
        /// </summary>
        /// <param name="apiKey">Your API access key as defined at https://bitpay.com/api-keys. </param>
	    public BitPay(string apiKey) {
            _auth = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(apiKey + ": "));
		    _client = new HttpClient {BaseAddress = new Uri(BaseUrl)};
	    }

	    /// <summary>
        /// Creates an invoice using the BitPay Payment Gateway API.
	    /// </summary>
	    /// <param name="price">This is the amount that is required to be collected from the buyer. Note, if this
        /// is specified in a currency other than BTC, the price will be converted into BTC at market exchange
        /// rates to determine the amount collected from the buyer.</param>
        /// <param name="currency">This is the currency code set for the price setting.  The pricing currencies
        /// currently supported are USD, EUR, BTC, and all of the codes listed on this page:
        /// https://bitpay.com/bitcoin­exchange­rates. </param>
	    /// <returns>A BitPay server populated Invoice object.</returns>
        /// <exception cref="BitPayAPI.BitPayException">Handles only errors that occur in the returned data.
        /// Does not handle programming or communication errors.</exception>
        public virtual Invoice CreateInvoice(decimal price, string currency)
        {
		    if(currency.Length > 3) {
			    throw new ArgumentException("Must be a valid currency code");
		    }

            var content = new FormUrlEncodedContent(getParams(price, currency));
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", _auth);
            _client.DefaultRequestHeaders.Add("X-BitPay-Plugin-Info", "CSharplib");

            var result = _client.PostAsync("invoice", content).Result;
            HttpContent response = result.Content;

            return CreateInvoiceObjectFromResponse(response);
	    }

        /// <summary>
        /// Creates an invoice using the BitPay Payment Gateway API.
        /// </summary>
        /// <param name="price">This is the amount that is required to be collected from the buyer. Note, if this
        /// is specified in a currency other than BTC, the price will be converted into BTC at market exchange
        /// rates to determine the amount collected from the buyer.</param>
        /// <param name="currency">The currency to create the invoice in. See https://bitpay.com/bitcoin­exchange­rates for valid currencies.</param>
        /// <param name="parameters">Optional payment notification (IPN) parameters.</param>
        /// <returns>A BitPay server populated Invoice object.</returns>
        /// <exception cref="BitPayAPI.BitPayException">Handles only errors that occur in the returned data.
        /// Does not handle programming or communication errors.</exception>
        public virtual Invoice CreateInvoice(decimal price, string currency, InvoiceParams parameters)
        {
            if (currency.Length > 3)
            {
                throw new ArgumentException("Must be a valid currency code");
            }

            var content = new FormUrlEncodedContent(getParams(price, currency, parameters));
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", _auth);
            _client.DefaultRequestHeaders.Add("X-BitPay-Plugin-Info", "CSharplib");

            var result = _client.PostAsync("invoice", content).Result;
            HttpContent response = result.Content;

            return CreateInvoiceObjectFromResponse(response);
        }

	    /// <summary>
	    /// Get an existing Invoice by it's Id. The Id is used in the url: "https://bitpay.com/invoice?id=&lt;ID&gt;".
	    /// </summary>
	    /// <param name="invoiceId">The Id for the invoice to fetch from the BitPay server.</param>
	    /// <returns>A BitPay server populated Invoice object.</returns>
        /// <exception cref="BitPayAPI.BitPayException">Handles only errors that occur in the returned data.
        /// Does not handle programming or communication errors.</exception>
        public virtual Invoice GetInvoice(string invoiceId)
        {
            string url = BaseUrl + "invoice/" + invoiceId;
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", _auth);
            _client.DefaultRequestHeaders.Add("X-BitPay-Plugin-Info", "CSharplib");

            var result = _client.GetAsync(url).Result;
            HttpContent response = result.Content;

            return CreateInvoiceObjectFromResponse(response);
        }

	    /// <summary>
        /// Get the current Bitcoin Exchange rates in dozens of currencies based on several exchanges.
	    /// </summary>
	    /// <returns>A BitPay server populated Rates object.</returns>
        public virtual Rates GetRates()
        {
            const string url = BaseUrl + "rates";
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", _auth);

            var result = _client.GetAsync(url).Result;
            HttpContent response = result.Content;

            return new Rates(response, this);
        }

        /// <summary>
        /// Creates a list of key/value parameters.
        /// </summary>
        /// <param name="price">The invoice price.</param>
        /// <param name="currency">The invoice currency.</param>
        /// <returns>A list of key/value pairs.</returns>
	    private Dictionary<string, string> getParams(decimal price, string currency)
        {
            return new Dictionary<string, string>
            {
                {"price", price.ToString(CultureInfo.InvariantCulture)},
                {"currency", currency}
            };
	    }

        /// <summary>
        /// Creates a list of key/value parameters including optional API parameters.
        /// </summary>
        /// <param name="price">The invoice price.</param>
        /// <param name="currency">The invoice currency.</param>
        /// <param name="invoiceParams">A populated InvoiceParams object.</param>
        /// <returns>A list of key/value pairs.</returns>
        private Dictionary<string, string> getParams(decimal price, string currency, InvoiceParams invoiceParams)
        {
            var parameters = invoiceParams.GetDictionary();
            parameters.Add("price", price.ToString(CultureInfo.InvariantCulture));
            parameters.Add("currency", currency);
		    return parameters;
	    }

        /// <summary>
        /// Determines whether or not the given dynamic object key collection includes the specified member name.
        /// </summary>
        /// <param name="obj">Expected to be a JSON decoded object.</param>
        /// <param name="name">The name of a key in the JSON object.</param>
        /// <returns></returns>
        private static bool DynamicObjectHasProperty(dynamic obj, string name)
        {
            Dictionary<string, object>.KeyCollection kc = obj.GetDynamicMemberNames();
            return kc.Contains(name);
        }

        /// <summary>
        /// Creates an invoice object given the BitPay server response.
        /// Throws a BitPayException if the content of the response indicates an error occurred.
        /// </summary>
        /// <param name="response">The HTTP response object from the BitPay server when attempting to create
        /// an invoice.</param>
        /// <returns>A populated Inovice object.</returns>
	    private Invoice CreateInvoiceObjectFromResponse(HttpContent response)
        {
            dynamic obj = Json.Decode(response.ReadAsStringAsync().Result);
            if (DynamicObjectHasProperty(obj, "error"))
            {
                throw new BitPayException("Error: " + obj.error.message);
            }

            return new Invoice(obj);
	    }
    }
}
