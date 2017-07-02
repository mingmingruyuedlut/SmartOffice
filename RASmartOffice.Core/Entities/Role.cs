
namespace RASmartOffice.Core.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table(nameof(Role))]
    public partial class Role
    {
        [Key]
        public int ID { get; set; }

        [StringLength(10)]
        public string RoleCode { get; set; }

        [StringLength(256)]
        public string Description { get; set; }

        [InverseProperty("Role")]
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
