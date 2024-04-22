namespace api_eWallet.Common
{
    /// <summary>
    /// Currency
    /// </summary>
    public enum Currency
    {
        /// <summary>
        /// Indian National Rupees
        /// </summary>
        INR,

        /// <summary>
        /// Japanise Currency
        /// </summary>
        YEN,

        /// <summary>
        /// US Doller
        /// </summary>
        USD
    }

    /// <summary>
    /// Transaction Type
    /// </summary>
    public enum TransactionType
    {
        /// <summary>
        /// Deposit
        /// </summary>
        D,

        /// <summary>
        /// Transfer
        /// </summary>
        T,

        /// <summary>
        /// Withdrawl
        /// </summary>
        W
    }

    /// <summary>
    /// Operationt Type
    /// </summary>
    public enum Operation
    {
        /// <summary>
        /// Create 
        /// </summary>
        C,

        /// <summary>
        /// Update
        /// </summary>
        U
    }
}
