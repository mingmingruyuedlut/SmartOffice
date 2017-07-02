
var isMeetingRoomEdit = false;
var mrEditId = 0;

function bindMeetingRoomMgmtEvent() {
    $(document).on('click', '.mr-add-btn', function () {
        clearAllValues();
        $('.addMeetingRoomDiv').show();
    });

    $(document).on('click', '.mr-cancel-btn', function () {
        clearAllValues();
        $('.addMeetingRoomDiv').hide();
    });

    $(document).on('click', '.editAction', function () {
        $('.addMeetingRoomDiv').show();
        editMeetingRoom(this);
    });

    $(document).on('click', '.deleteAction', function () {
        deleteMeetingRoom(this);
    });

    $(document).on('click', '.mr-save-btn', function () {
        saveMeetingRoom();
    });
}

function saveMeetingRoom() {
    if (validateInputs()) {
        var meetingRoomInfo = getMeetingRoomInfoObj()
        var meetingRoomInfoJsonStr = JSON.stringify(meetingRoomInfo);
        $.ajax({
            url: "/MeetingRoom/SaveMeetingRoom",
            data: { MeetingRoomJsonStr: meetingRoomInfoJsonStr, IsEdit: isMeetingRoomEdit },
            type: "POST",
            success: function (data) {
                if (data == 'success') {
                    reloadMeetingRoom();
                    clearAllValues();
                    $('.addClientDiv').hide();
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

    var mrName = $('.mr-name').val();
    var mrDescription = $('.mr-description').val();
    var mrFloor = $('.mr-floor').val();
    var mrCapacity = $('.mr-capacity').val();

    if (mrName.Trim() == "") {
        valResult = false;
        alert("Please input name");
    }
    else if (mrDescription.Trim() == "") {
        valResult = false;
        alert("Please input description");
    }
    else if (mrFloor.Trim() == "" || !IsNumber(mrFloor.Trim())) {
        valResult = false;
        alert("Please input right format floor");
    }
    else if (mrCapacity.Trim() == "" || !IsNumber(mrCapacity.Trim())) {
        valResult = false;
        alert("Please input right format capacity");
    }

    return valResult;
}

function getMeetingRoomInfoObj() {
    var meetingRoomInfo = new Object();
    if (isMeetingRoomEdit) {
        meetingRoomInfo.ID = mrEditId;
    }
    meetingRoomInfo.Name = $('.mr-name').val();
    meetingRoomInfo.Description = $('.mr-description').val();
    meetingRoomInfo.Floor = $('.mr-floor').val();
    meetingRoomInfo.Capacity = $('.mr-capacity').val();
    // other fields
    return meetingRoomInfo;
}

function reloadMeetingRoom() {
    $.ajax({
        url: "/MeetingRoom/ReloadMeetingRoom",
        data: {},
        type: "POST",
        success: function (data) {
            $('.admin-content').html(data);
            initCommonDataTable('#MeetingRoomDataTbl');
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert("Reload meeting room error! status:" + XMLHttpRequest.status + " readyState:" + XMLHttpRequest.readyState + " textStatus:" + textStatus);
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
    $('.mr-name').val("");
    $('.mr-description').val("");
    $('.mr-floor').val("");
    $('.mr-capacity').val("");
    isMeetingRoomEdit = false;
    mrEditId = 0;
}
