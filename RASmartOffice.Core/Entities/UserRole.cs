
namespace RASmartOffice.Core.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table(nameof(UserRole))]
    public partial class UserRole
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("Role")]
        public int RoleID { get; set; }

        public virtual Role Role { get; set; }

        [ForeignKey("User")]
        public int UserID { get; set; }

        public virtual User User { get; set; }
    }
}
