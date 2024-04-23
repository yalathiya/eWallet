namespace api_eWallet.Utilities
{
    /// <summary>
    /// EnmCurrency
    /// </summary>
    public enum EnmCurrency
    {
        /// <summary>
        /// Indian National Rupees
        /// </summary>
        INR,

        /// <summary>
        /// Japanise EnmCurrency
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
    public enum EnmTransactionType
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
    /// EnmOperationt Type
    /// </summary>
    public enum EnmOperation
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
