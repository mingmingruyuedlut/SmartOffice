using RASmartOffice.Core.Context;
using RASmartOffice.Core.Entities;
using RASmartOffice.Core.Enums;
using RASmartOffice.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RASmartOffice.Core.DBManager
{
    public class MeetingRoomBookingManager
    {
        private RASOConext _context = new RASOConext();

        public MRBSummary GetMRBSummary(DateTime startDate, DateTime endDate, int userId)
        {
            return new MRBSummary()
            {
                MeetingRoomBookings = GetMeetingRoomBookings(startDate, endDate, userId),
                MeetingRooms = new MeetingRoomManager().GetMeetingRoomDropDownList()
            };
        }

        public List<MeetingRoomBookingInfo> GetMeetingRoomBookings(DateTime startDate, DateTime endDate, int userId)
        {
            List<MeetingRoomBookingInfo> mrbInfoList = new List<MeetingRoomBookingInfo>();
            mrbInfoList = _context.MeetingRoomBooking
                .Include(nameof(MeetingRoom))
                .Where(x => x.StartTime >= startDate && x.EndTime <= endDate && x.OwnerID == userId)
                .Select(x => new MeetingRoomBookingInfo
                {
                    ID = x.ID,
                    Title = x.Title,
                    Description = x.Description,
                    StartTime = x.StartTime,
                    EndTime = x.EndTime,
                    MeetingRoomID = x.MeetingRoomID,
                    MeetingRoomName = x.MeetingRoom.Name
                }).ToList();

            GenerateMRBStatus(mrbInfoList);
            return mrbInfoList;
        }

        private void GenerateMRBStatus(List<MeetingRoomBookingInfo> mrbs)
        {
            foreach (MeetingRoomBookingInfo mrb in mrbs)
            {
                if (mrb.StartTime > DateTime.Now)
                {
                    mrb.Status = MeetingRoomBookingStatus.Waiting;
                }
                else if (mrb.StartTime <= DateTime.Now && mrb.EndTime >= DateTime.Now)
                {
                    mrb.Status = MeetingRoomBookingStatus.Running;
                }
                else if (mrb.EndTime < DateTime.Now)
                {
                    mrb.Status = MeetingRoomBookingStatus.Finished;
                }
            }
        }

        public void SaveMeetingRoomBooking(MeetingRoomBookingInfo mrbInfo)
        {
            MeetingRoomBooking mrb = new MeetingRoomBooking()
            {
                Title = mrbInfo.Title,
                Description = mrbInfo.Description,
                MeetingRoomID = mrbInfo.MeetingRoomID,
                StartTime = mrbInfo.StartTime,
                EndTime = mrbInfo.EndTime,
                OwnerID = mrbInfo.OwnerID
            };

            _context.MeetingRoomBooking.Add(mrb);
            _context.SaveChanges();
        }

        public void EditMeetingRoomBooking(MeetingRoomBookingInfo mrbInfo)
        {
            MeetingRoomBooking mrb = _context.MeetingRoomBooking.Find(mrbInfo.ID);
            mrb.Title = mrbInfo.Title;
            mrb.Description = mrbInfo.Description;
            mrb.MeetingRoomID = mrbInfo.MeetingRoomID;
            mrb.StartTime = mrbInfo.StartTime;
            mrb.EndTime = mrbInfo.EndTime;
            mrb.OwnerID = mrbInfo.OwnerID;

            _context.Entry(mrb).State = EntityState.Modified;
            _context.SaveChanges();
        }

    }
}
