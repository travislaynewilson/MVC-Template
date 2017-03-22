using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RxLocal.Data.Entities
{
    [Table("CustomerTypes")]
    public class CustomerType : BaseEntity
    {
        [Key]
        public int CustomerTypeID { get; set; }
        public string CustomerTypeDescription { get; set; }
        public int PriceTypeID { get; set; }
    }
}
