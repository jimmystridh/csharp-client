using System;

namespace BitPayAPI
{
    /// <summary>
    /// Provides an interface to a single exchange rate.
    /// </summary>
    public class Rate
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="name">The full display name of the currency.</param>
        /// <param name="code">The three letter code for the currency, in all caps.</param>
        /// <param name="exchangeRate">The numeric exchange rate of this currency provided by the BitPay server.</param>
        public Rate(string name, string code, decimal exchangeRate)
        {
            Name = name;
            Code = code;
            ExchangeRate = exchangeRate;
        }

        /// <summary>
        /// The full display name of the currency.
        /// </summary>
        /// <returns></returns>
        public string Name { get; private set; }

        /// <summary>
        /// The three letter code for the currency, in all caps.
        /// </summary>
        /// <returns></returns>
        public string Code { get; private set; }

        /// <summary>
        /// The numeric exchange rate of this currency provided by the BitPay server.
        /// </summary>
        /// <returns></returns>
        public decimal ExchangeRate { get; private set; }
    }
}
