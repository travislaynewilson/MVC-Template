using System;
using RxLocal.Core.Domain.Common;

namespace RxLocal.Core.Domain.Customers
{
    public partial class Customer : DomainModel
    {
        public int CustomerID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string NameSuffix { get; set; }
        public string FullName { get; set; }
        public string Company { get; set; }
        public string DisplayName { get; set; }
        public Gender Gender { get; set; }
        public DateTime? BirthDate { get; set; }

        public int CustomerTypeID { get; set; }
        public int CustomerStatusID { get; set; }
        public int DefaultWarehouseID { get; set; }
        public string CurrencyCode { get; set; }
        public int LanguageID { get; set; }
        public bool IsEmailSubscribed { get; set; }
        public string EmailSubscribeIP { get; set; }
        public string Notes { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }

        public Address MainAddress { get; set; }
        public Address MailingAddress { get; set; }
        public Address OtherAddress { get; set; }

        public string Email { get; set; }
        public string Phone { get; set; }
        public string Phone2 { get; set; }
        public string MobilePhone { get; set; }
        public string Fax { get; set; }

        public string TaxID { get; set; }
        public string PayableToName { get; set; }
        public int PayableTypeID { get; set; }
        public string SalesTaxCode { get; set; }
        public bool IsSalesTaxExempt { get; set; }
        public decimal CheckThreshold { get; set; }
        public string VatRegistration { get; set; }

        public bool CanLogin { get; set; }
        public string LoginName { get; set; }
        public string Password { get; set; }

        public int? EnrollerID { get; set; }
        public int? SponsorID { get; set; }
        public int RankID { get; set; }
        public bool UseBinaryHoldingTank { get; set; }
        public bool IsInBinaryHoldingTank { get; set; }
        public int BinaryPlacementTypeID { get; set; }

        public string Field1 { get; set; }
        public string Field2 { get; set; }
        public string Field3 { get; set; }
        public string Field4 { get; set; }
        public string Field5 { get; set; }
        public string Field6 { get; set; }
        public string Field7 { get; set; }
        public string Field8 { get; set; }
        public string Field9 { get; set; }
        public string Field10 { get; set; }
        public string Field11 { get; set; }
        public string Field12 { get; set; }
        public string Field13 { get; set; }
        public string Field14 { get; set; }
        public string Field15 { get; set; }

        public DateTime? Date1 { get; set; }
        public DateTime? Date2 { get; set; }
        public DateTime? Date3 { get; set; }
        public DateTime? Date4 { get; set; }
        public DateTime? Date5 { get; set; }


        public virtual CustomerType CustomerType { get; set; }
        public virtual CustomerStatus CustomerStatus { get; set; }
    }
}
