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
using System.Web.Mvc;

namespace RASmartOffice.Core.DBManager
{
    public class MeetingRoomManager
    {
        private RASOConext _context = new RASOConext();

        public MeetingRoomSummary GetMeetingRoomSummary()
        {
            return new MeetingRoomSummary()
            {
                MeetingRooms = GetAllMeetingRooms()
            };
        }

        public List<MeetingRoomInfo> GetAllMeetingRooms()
        {
            return _context.MeetingRoom
                .Select(x => new MeetingRoomInfo()
                {
                    ID = x.ID,
                    Name = x.Name,
                    Description = x.Description,
                    Capacity = x.Capacity,
                    Floor = x.Floor
                }).ToList();
        }

        public void SaveMeetingRoom(MeetingRoomInfo mrInfo)
        {
            MeetingRoom mr = new MeetingRoom()
            {
                Name = mrInfo.Name,
                Description = mrInfo.Description,
                Floor = mrInfo.Floor,
                Capacity = mrInfo.Capacity
            };

            _context.MeetingRoom.Add(mr);
            _context.SaveChanges();
        }

        public void EditMeetingRoom(MeetingRoomInfo mrInfo)
        {
            MeetingRoom mr = _context.MeetingRoom.Find(mrInfo.ID);
            mr.Name = mrInfo.Name;
            mr.Description = mrInfo.Description;
            mr.Floor = mrInfo.Floor;
            mr.Capacity = mrInfo.Capacity;

            _context.Entry(mr).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public List<SelectListItem> GetMeetingRoomDropDownList()
        {
            return _context.MeetingRoom
                .Select(x => new SelectListItem
                {
                    Value = x.ID.ToString(),
                    Text = x.Name
                }).ToList();
        }

        public MeetingRoomObserveSummary GetMeetingRoomObserveSummary()
        {
            return new MeetingRoomObserveSummary
            {
                MeetingRooms = GetAllObserveMeetingRooms()
            };
        }

        public List<MeetingRoomObserveInfo> GetAllObserveMeetingRooms()
        {
            DateTime TodayStart = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            DateTime TodayEnd = new DateTime(DateTime.Now.AddDays(1).Year, DateTime.Now.AddDays(1).Month, DateTime.Now.AddDays(1).Day);
            List<MeetingRoomObserveInfo> mroList = new List<MeetingRoomObserveInfo>();
            List<MeetingRoom> meetingRooms = _context.MeetingRoom.ToList();
            foreach (MeetingRoom mr in meetingRooms)
            {
                MeetingRoomBooking booking = _context.MeetingRoomBooking.Where(x => x.MeetingRoomID == mr.ID && x.StartTime > TodayStart && x.EndTime < TodayEnd && x.EndTime > DateTime.Now).FirstOrDefault();
                MeetingRoomObserveInfo observeInfo = new MeetingRoomObserveInfo()
                {
                    ID = mr.ID,
                    Name = mr.Name,
                    Description = mr.Description,
                    Floor = mr.Floor,
                    Capacity = mr.Capacity,
                    Status = (booking != null && booking.StartTime > DateTime.Now) ? MeetingRoomStatus.Busy : MeetingRoomStatus.Available
                };
                if (booking != null)
                {
                    observeInfo.CurrentBooking = new MeetingRoomBookingInfo()
                    {
                        Title = booking.Title,
                        Description = booking.Description,
                        MeetingRoomID = booking.MeetingRoomID,
                        StartTime = booking.StartTime,
                        EndTime = booking.EndTime,
                        OwnerID = booking.OwnerID
                    };
                }
                mroList.Add(observeInfo);
            }
            return mroList;
        }
    }
}
