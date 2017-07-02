
namespace RASmartOffice.Core.Entities
{
    using Enums;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table(nameof(MeetingRoomBooking))]
    public partial class MeetingRoomBooking
    {
        [Key]
        public int ID { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        [ForeignKey("Owner")]
        public int OwnerID { get; set; }

        public virtual User Owner { get; set; }

        [ForeignKey("MeetingRoom")]
        public int MeetingRoomID { get; set; }

        public virtual MeetingRoom MeetingRoom { get; set; }
    }
}
