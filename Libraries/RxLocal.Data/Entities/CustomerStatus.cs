using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RxLocal.Data.Entities
{
    [Table("CustomerStatuses")]
    public class CustomerStatus : BaseEntity
    {
        [Key]
        public int CustomerStatusID { get; set; }
        public string CustomerStatusDescription { get; set; }
    }
}
