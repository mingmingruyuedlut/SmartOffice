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
    public class MeetingRoomBookingController : Controller
    {
        // GET: MeetingRoomBooking
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MRBMgmt()
        {
            LoginUser loginUser = (LoginUser)Session["CurrentLoginUser"];
            DateTime defaultStartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            DateTime defaultEndDate = defaultStartDate.AddDays(1);
            MRBSummary mrbSummary = new MeetingRoomBookingManager().GetMRBSummary(defaultStartDate, defaultEndDate, loginUser.UserId);
            return View(mrbSummary);
        }

        public JsonResult SaveMRB(string mrbJsonStr, bool isEdit)
        {
            LoginUser loginUser = (LoginUser)Session["CurrentLoginUser"];
            MeetingRoomBookingInfo mrbInfo = JsonConvert.DeserializeObject<MeetingRoomBookingInfo>(mrbJsonStr);
            mrbInfo.StartTime = mrbInfo.StartTime.ToLocalTime(); // convert UAT to Local Time because of JSON.stringify convert the datetime object to UAT
            mrbInfo.EndTime = mrbInfo.EndTime.ToLocalTime(); // convert UAT to Local Time because of JSON.stringify convert the datetime object to UAT
            mrbInfo.OwnerID = loginUser.UserId;
            if (isEdit)
            {
                new MeetingRoomBookingManager().EditMeetingRoomBooking(mrbInfo);
            }
            else
            {
                new MeetingRoomBookingManager().SaveMeetingRoomBooking(mrbInfo);
            }
            return Json("success", JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult ReloadMRB()
        {
            LoginUser loginUser = (LoginUser)Session["CurrentLoginUser"];
            DateTime defaultStartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            DateTime defaultEndDate = defaultStartDate.AddDays(1);
            List<MeetingRoomBookingInfo> mrbInfoList = new MeetingRoomBookingManager().GetMeetingRoomBookings(defaultStartDate, defaultEndDate, loginUser.UserId);
            return PartialView("_MRBMgmtPartial", mrbInfoList);
        }
    }
}