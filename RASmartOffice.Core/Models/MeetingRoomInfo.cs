using RASmartOffice.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RASmartOffice.Core.Models
{
    public class MeetingRoomInfo
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Floor { get; set; }
        public int Capacity { get; set; }
    }

    public class MeetingRoomSummary
    {
        public List<MeetingRoomInfo> MeetingRooms { get; set; }
    }

    public class MeetingRoomObserveInfo : MeetingRoomInfo
    {
        public MeetingRoomStatus Status { get; set; }
        public MeetingRoomBookingInfo CurrentBooking { get; set; }
    }

    public class MeetingRoomObserveSummary
    {
        public List<MeetingRoomObserveInfo> MeetingRooms { get; set; }
    }
}
