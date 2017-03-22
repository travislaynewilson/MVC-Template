namespace RxLocal.Core.Domain.Customers
{
    /// <summary>
    /// Represents the customer authentication result enumeration
    /// </summary>
    public enum CustomerAuthenticationResults
    {
        /// <summary>
        /// Login successful
        /// </summary>
        Successful = 1,
        /// <summary>
        /// Customer does not exist
        /// </summary>
        CustomerNotExist = 2,
        /// <summary>
        /// Customer not allowed to sign in
        /// </summary>
        LoginDisabled = 3,
        /// <summary>
        /// Customer has been deleted 
        /// </summary>
        Deleted = 4,
        /// <summary>
        /// Customer's password was invalid
        /// </summary>
        InvalidPassword = 5,
        /// <summary>
        /// Customer has not authorized to proceed
        /// </summary>
        NotAuthorized = 6
    }
}
