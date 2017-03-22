namespace RxLocal.Core.Domain.Customers
{
    public partial class CustomerStatus : DomainModel
    {
        public int CustomerStatusID { get; set; }
        public string CustomerStatusDescription { get; set; }
    }
}
