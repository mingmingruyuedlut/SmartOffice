
var isMeetingRoomBookingEdit = false;
var mrbEditId = 0;

function bindMeetingRoomBookingEvent() {
    $(document).on('click', '.mrb-add-btn', function () {
        clearAllValues();
        $('.bookMeetingRoomDiv').show();
    });

    $(document).on('click', '.mrb-cancel-btn', function () {
        clearAllValues();
        $('.bookMeetingRoomDiv').hide();
    });

    $(document).on('click', '.editAction', function () {
        $('.bookMeetingRoomDiv').show();
        editMeetingRoom(this);
    });

    $(document).on('click', '.deleteAction', function () {
        deleteMeetingRoom(this);
    });

    $(document).on('click', '.mrb-save-btn', function () {
        saveMeetingRoomBooking();
    });
}

function saveMeetingRoomBooking() {
    if (validateInputs()) {
        var mrbInfo = getMRBInfoObj()
        var mrbInfoJsonStr = JSON.stringify(mrbInfo);
        $.ajax({
            url: "/MeetingRoomBooking/SaveMRB",
            data: { MRBJsonStr: mrbInfoJsonStr, IsEdit: isMeetingRoomBookingEdit },
            type: "POST",
            success: function (data) {
                if (data == 'success') {
                    reloadMeetingRoomBooking();
                    clearAllValues();
                    $('.bookMeetingRoomDiv').hide();
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert("Save meeting room info error! status:" + XMLHttpRequest.status + " readyState:" + XMLHttpRequest.readyState + " textStatus:" + textStatus);
            }
        });
    }
    else {
        //to-do
    }
}

function validateInputs() {
    var valResult = true;

    var mrId = $('#MeetingRoomId').val();
    var mrbTitle = $('.booking-title').val();
    var mrbDescription = $('.booking-description').val();
    var mrbDateTimeRange = $('.booking-date-time-range').val();

    if (mrId.Trim() == "") {
        valResult = false;
        alert("Please select location");
    }
    else if (mrbTitle.Trim() == "") {
        valResult = false;
        alert("Please input title");
    }
    else if (mrbDescription.Trim() == "") {
        valResult = false;
        alert("Please input description");
    }
    else if (mrbDateTimeRange.Trim() == "") {
        valResult = false;
        alert("Please select booking date time range");
    }

    return valResult;
}

function getMRBInfoObj() {
    var mrbInfo = new Object();
    if (isMeetingRoomBookingEdit) {
        mrbInfo.ID = mrbEditId;
    }
    mrbInfo.MeetingRoomID = $('#MeetingRoomId').val();
    mrbInfo.Title = $('.booking-title').val();
    mrbInfo.Description = $('.booking-description').val();
    mrbInfo.StartTime = new Date($('.daterangepicker').find('input[name="daterangepicker_start"]').val());
    mrbInfo.EndTime = new Date($('.daterangepicker').find('input[name="daterangepicker_end"]').val());
    // other fields
    return mrbInfo;
}

function reloadMeetingRoomBooking() {
    $.ajax({
        url: "/MeetingRoomBooking/ReloadMRB",
        data: {},
        type: "POST",
        success: function (data) {
            $('.admin-content').html(data);
            initCommonDataTable('#MeetingRoomDataTbl');
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert("Reload meeting room booking error! status:" + XMLHttpRequest.status + " readyState:" + XMLHttpRequest.readyState + " textStatus:" + textStatus);
        }
    });
}

function editMeetingRoom(obj) {
    var rowObj = $(obj).closest('tr');
    isMeetingRoomEdit = true;
    mrEditId = $(rowObj).data('mrid');

    $('.mr-name').val($(rowObj).data('mrname'));
    $('.mr-description').val($(rowObj).data('mrdescription'));
    $('.mr-floor').val($(rowObj).data('mrfloor'));
    $('.mr-capacity').val($(rowObj).data('mrcapacity'));
}

function deleteMeetingRoom(obj) {
    var rowObj = $(obj).closest('tr');
    //to-do
}

function clearAllValues() {
    $('#MeetingRoomId').val("");
    $('.booking-title').val("");
    $('.booking-description').val("");
    isMeetingRoomBookingEdit = false;
    mrbEditId = 0;
}

