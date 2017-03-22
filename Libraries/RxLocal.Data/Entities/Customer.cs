using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RxLocal.Data.Entities
{
    [Table("Customers")]
    public class Customer : BaseEntity
    {
        [Key]
        public int CustomerID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string NameSuffix { get; set; }
        public string Company { get; set; }
        public int CustomerTypeID { get; set; }
        public int CustomerStatusID { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Phone2 { get; set; }
        public string MobilePhone { get; set; }
        public string Fax { get; set; }
        public string MainAddress1 { get; set; }
        public string MainAddress2 { get; set; }
        public string MainAddress3 { get; set; }
        public string MainCity { get; set; }
        public string MainState { get; set; }
        public string MainZip { get; set; }
        public string MainCountry { get; set; }
        public string MainCounty { get; set; }
        public string MailAddress1 { get; set; }
        public string MailAddress2 { get; set; }
        public string MailAddress3 { get; set; }
        public string MailCity { get; set; }
        public string MailState { get; set; }
        public string MailZip { get; set; }
        public string MailCountry { get; set; }
        public string MailCounty { get; set; }
        public string OtherAddress1 { get; set; }
        public string OtherAddress2 { get; set; }
        public string OtherAddress3 { get; set; }
        public string OtherCity { get; set; }
        public string OtherState { get; set; }
        public string OtherZip { get; set; }
        public string OtherCountry { get; set; }
        public string OtherCounty { get; set; }
        public bool CanLogin { get; set; }
        public string LoginName { get; set; }
        public byte[] PasswordHash { get; set; }
        public int? RankID { get; set; }
        public int? EnrollerID { get; set; }
        public int? SponsorID { get; set; }
        public DateTime? BirthDate { get; set; }
        public string CurrencyCode { get; set; }
        public string PayableToName { get; set; }
        public int? DefaultWarehouseID { get; set; }
        public int PayableTypeID { get; set; }
        public decimal CheckThreshold { get; set; }
        public int? LanguageID { get; set; }
        public string Gender { get; set; }
        public string TaxCode { get; set; }
        public int TaxCodeTypeID { get; set; }
        public bool IsSalesTaxExempt { get; set; }
        public string SalesTaxCode { get; set; }
        public DateTime? SalesTaxExemptExpireDate { get; set; }
        public string VatRegistration { get; set; }
        public int BinaryPlacementTypeID { get; set; }
        public bool UseBinaryHoldingTank { get; set; }
        public bool IsInBinaryHoldingTank { get; set; }
        public bool? IsEmailSubscribed { get; set; }
        public string EmailSubscribeIP { get; set; }
        public string Notes { get; set; }
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
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }


        public virtual CustomerType CustomerType { get; set; }
        public virtual CustomerStatus CustomerStatus { get; set; }
    }
}
