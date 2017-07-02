using Newtonsoft.Json;
using RASmartOffice.Core.DBManager;
using RASmartOffice.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RASmartOffice.Controllers
{
    public class MeetingRoomController : Controller
    {
        // GET: MeetingRoom
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MeetingRoomMgmt()
        {
            MeetingRoomSummary mrSummary = new MeetingRoomManager().GetMeetingRoomSummary();
            return View(mrSummary);
        }

        public PartialViewResult ReloadMeetingRoom()
        {
            List<MeetingRoomInfo> mrInfoList = new MeetingRoomManager().GetAllMeetingRooms();
            return PartialView("_MeetingRoomMgmtPartial", mrInfoList);
        }

        public JsonResult SaveMeetingRoom(string meetingRoomJsonStr, bool isEdit)
        {
            MeetingRoomInfo mrInfo = JsonConvert.DeserializeObject<MeetingRoomInfo>(meetingRoomJsonStr);
            if (isEdit)
            {
                new MeetingRoomManager().EditMeetingRoom(mrInfo);
            }
            else
            {
                new MeetingRoomManager().SaveMeetingRoom(mrInfo);
            }
            return Json("success", JsonRequestBehavior.AllowGet);
        }

        public ActionResult Observe()
        {
            MeetingRoomObserveSummary mroSummary = new MeetingRoomManager().GetMeetingRoomObserveSummary();
            return View(mroSummary);
        }
    }
}