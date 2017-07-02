
namespace RASmartOffice.Core.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table(nameof(User))]
    public partial class User
    {
        [Key]
        public int ID { get; set; }

        [StringLength(50)]
        public string UserName { get; set; }

        [StringLength(256)]
        public string Password { get; set; }

        [StringLength(50)]
        public string DisplayName { get; set; }

        [StringLength(50)]
        public string Phone { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(256)]
        public string Notes { get; set; }

        public Nullable<System.DateTime> CreateTime { get; set; }

        public Nullable<System.DateTime> UpdateTime { get; set; }

        public int Status { get; set; }

        [InverseProperty(nameof(User))]
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
