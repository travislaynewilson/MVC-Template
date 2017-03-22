namespace RxLocal.Core.Domain.Customers
{
    public partial class CustomerType : DomainModel
    {
        public int CustomerTypeID { get; set; }
        public string CustomerTypeDescription { get; set; }
        public int PriceTypeID { get; set; }
    }
}
