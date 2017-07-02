using RASmartOffice.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace RASmartOffice.Core.Models
{
    public class MRBSummary
    {
        public List<MeetingRoomBookingInfo> MeetingRoomBookings { get; set; }
        public List<SelectListItem> MeetingRooms { get; set; }
    }

    public class MeetingRoomBookingInfo
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public MeetingRoomBookingStatus Status { get; set; }

        public int OwnerID { get; set; }

        public int MeetingRoomID { get; set; }

        public string MeetingRoomName { get; set; }
    }
}
