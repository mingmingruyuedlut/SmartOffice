
namespace RASmartOffice.Core.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table(nameof(MeetingRoom))]
    public partial class MeetingRoom
    {
        [Key]
        public int ID { get; set; }

        [StringLength(256)]
        public string Name { get; set; }

        public string Description { get; set; }

        public int Floor { get; set; }

        public int Capacity { get; set; }

        [InverseProperty("MeetingRoom")]
        public virtual ICollection<MeetingRoomBooking> MeetingRoomBookings { get; set; }
    }
}
