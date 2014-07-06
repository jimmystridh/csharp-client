using System.Collections.Generic;


namespace BitPayAPI
{
    /// <summary>
    ///     Provides BitPay invoice parameter handling.
    /// </summary>
    public class InvoiceParams
    {
        /// <summary>
        ///     A pass through variable provided by the merchant and designed to be used by the merchant to correlate
        ///     the invoice with an order or other object in their system.
        ///     This pass through variable can be a JSON-encoded string, for example
        ///     posData:'{"ref":711454,"affiliate":"spring112"}'
        /// </summary>
        public string PosData { get; set; }

        /// <summary>
        ///     A URL to send status update messages to your server (this must be an https URL, unencrypted http
        ///     URLs or any other type of URL is not supported).
        ///     Bitpay.com will send a POST request with a JSON encoding of the invoice to this URL when the invoice
        ///     status changes.
        /// </summary>
        public string NotificationUrl { get; set; }

        /// <summary>
        ///     The transaction speed preference of an invoice determines when an invoice is confirmed.  For the
        ///     high speed setting, it will confirmed as soon as full payment is received on the bitcoin network
        ///     (note, the invoice will go from a status of new to confirmed, bypassing the paid status).  For
        ///     the medium speed setting, the invoice is confirmed after the payment transaction(s) have been
        ///     confrimed by 1 block on the bitcoin network.  For the low speed setting, 6 blocks on the bitcoin
        ///     network are required.  Invoices are considered complete after 6 blocks on the bitcoin network,
        ///     therefore an invoice will go from a paid status directly to a complete status if the transaction
        ///     speed is set to low.
        ///     default value: set in your https://bitpay.com/order-settings
        ///     "high" : An invoice is considered to be "confirmed" immediately upon receipt of payment.
        ///     "medium" : An invoice is considered to be "confirmed" after 1 block confirmation (~10 minutes).
        ///     "low" : An invoice is considered to be "confirmed" after 6 block confirmations (~1 hour).
        ///     NOTE: Orders are posted to your Account Summary after 6 block confirmations regardless of this
        ///     setting.
        /// </summary>
        public string TransactionSpeed { get; set; }

        /// <summary>
        ///     Default: false
        ///     true: Notifications will be sent on every status change.
        ///     false: Notifications are only sent when an invoice is confirmed (according the the transactionSpeed
        ///     setting).
        /// </summary>
        public bool FullNotifications { get; set; }

        /// <summary>
        ///     Bitpay.com will send an email to this email address when the invoice status changes.
        /// </summary>
        public string NotificationEmail { get; set; }

        /// <summary>
        ///     This is the URL for a return link that is displayed on the receipt, to return the shopper back to
        ///     your website after a successful purchase. This could be a page specific to the order, or to their
        ///     account.
        /// </summary>
        public string RedirectUrl { get; set; }

        /// <summary>
        ///     Used to display your public order number to the buyer on the BitPay invoice. In the merchant Account
        ///     Summary page, this value is used to identify the ledger entry.
        /// </summary>
        public string OrderId { get; set; }

        /// <summary>
        ///     Used to display an item description to the buyer.
        /// </summary>
        public string ItemDesc { get; set; }

        /// <summary>
        ///     Used to display an item SKU code or part number to the buyer.
        /// </summary>
        public string ItemCode { get; set; }

        /// <summary>
        ///     default value: false
        ///     true : Indicates a physical item will be shipped (or picked up)
        ///     false : Indicates that nothing is to be shipped for this order
        /// </summary>
        public bool Physical { get; set; }

        /// <summary>
        ///     Used for display purposes only and will be shown on the invoice if provided.
        /// </summary>
        public string BuyerName { get; set; }

        /// <summary>
        ///     Used for display purposes only and will be shown on the invoice if provided.
        /// </summary>
        public string BuyerAddress1 { get; set; }

        /// <summary>
        ///     Used for display purposes only and will be shown on the invoice if provided.
        /// </summary>
        public string BuyerAddress2 { get; set; }

        /// <summary>
        ///     Used for display purposes only and will be shown on the invoice if provided.
        /// </summary>
        public string BuyerCity { get; set; }

        /// <summary>
        ///     Used for display purposes only and will be shown on the invoice if provided.
        /// </summary>
        public string BuyerState { get; set; }

        /// <summary>
        ///     Used for display purposes only and will be shown on the invoice if provided.
        /// </summary>
        public string BuyerZip { get; set; }

        /// <summary>
        ///     Used for display purposes only and will be shown on the invoice if provided.
        /// </summary>
        public string BuyerCountry { get; set; }

        /// <summary>
        ///     Used for display purposes only and will be shown on the invoice if provided.
        /// </summary>
        public string BuyerEmail { get; set; }

        /// <summary>
        ///     Used for display purposes only and will be shown on the invoice if provided.
        /// </summary>
        public string BuyerPhone { get; set; }


        /// <summary>
        ///     Get the entire set of invoice parameters in a dictionary.
        /// </summary>
        /// <returns>A list of key value pairs.</returns>
        public Dictionary<string, string> GetDictionary()
        {
            var parameters = new Dictionary<string, string>();

            parameters.Add("physical", Physical.ToString().ToLower());
            parameters.Add("fullNotifications", FullNotifications.ToString().ToLower());

            if (NotificationUrl != null)
            {
                parameters.Add("notificationURL", NotificationUrl);
            }

            if (TransactionSpeed != null)
            {
                parameters.Add("transactionSpeed", TransactionSpeed);
            }

            if (PosData != null)
            {
                parameters.Add("posData", PosData);
            }

            if (NotificationEmail != null)
            {
                parameters.Add("notificationEmail", NotificationEmail);
            }

            if (RedirectUrl != null)
            {
                parameters.Add("redirectURL", RedirectUrl);
            }

            if (OrderId != null)
            {
                parameters.Add("orderID", OrderId);
            }

            if (ItemDesc != null)
            {
                parameters.Add("itemDesc", ItemDesc);
            }

            if (ItemCode != null)
            {
                parameters.Add("itemCode", ItemCode);
            }

            if (BuyerName != null)
            {
                parameters.Add("buyerName", BuyerName);
            }

            if (BuyerAddress1 != null)
            {
                parameters.Add("buyerAddress1", BuyerAddress1);
            }

            if (BuyerAddress2 != null)
            {
                parameters.Add("buyerAddress2", BuyerAddress2);
            }

            if (BuyerCity != null)
            {
                parameters.Add("buyerCity", BuyerCity);
            }

            if (BuyerState != null)
            {
                parameters.Add("buyerState", BuyerState);
            }

            if (BuyerZip != null)
            {
                parameters.Add("buyerZip", BuyerZip);
            }

            if (BuyerCountry != null)
            {
                parameters.Add("buyerCountry", BuyerCountry);
            }

            if (BuyerEmail != null)
            {
                parameters.Add("buyerEmail", BuyerEmail);
            }

            if (BuyerPhone != null)
            {
                parameters.Add("buyerPhone", BuyerPhone);
            }

            return parameters;
        }
    }
}