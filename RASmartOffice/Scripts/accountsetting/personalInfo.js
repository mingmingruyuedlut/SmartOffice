/******Personal Information Page******/
/******Some Common Functions In smartstock.js******/

function bindPersonalInformationEvent() {
    $('.account-save-btn').bind('click', function () {
        var email = $('#Email').val();
        var phone = $('#Phone').val();
        var displayname = $('#DisplayName').val();
        var userId = $('#UserId').val();
        var notes = $('#Notes').val();
        if (email != "" && !checkEmailFormart(email)) {
            alert("Email format is invalid, please try again.");
        }
        else if (phone != "" && !checkPhoneFormat(phone)) {
            alert("Phone format is invalid, please try again.");
        }
        else if (email != "" && !checkEmailIsValid(email, userId)) {
            alert("This email is existed, please change another one.");
        }
        else if (phone != "" && !checkPhoneIsValid(phone, userId)) {
            alert("This phone is existed, please change another one.");
        }
        else if (displayname == "") {
            alert("User display name is empty.");
        }
        else {
            saveAccountInfo();
        }
    });
}

function checkEmailIsValid(email, userId) {
    var vResult = true;
    $.ajax({
        url: "/AccountSetting/CheckEmailIsValid",
        data: { Email: email, UserId: userId },
        type: "POST",
        async: false,
        success: function (data) {
            if (data == false) {
                vResult = false;
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert("Check email is valid error! status:" + XMLHttpRequest.status + " readyState:" + XMLHttpRequest.readyState + " textStatus:" + textStatus);
            vResult = false;
        }
    });
    return vResult;
}

function checkPhoneIsValid(phone, userId) {
    var vResult = true;
    $.ajax({
        url: "/AccountSetting/CheckPhoneIsValid",
        data: { Phone: phone, UserId: userId },
        type: "POST",
        async: false,
        success: function (data) {
            if (data == false) {
                vResult = false;
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert("Check phone is valide error! status:" + XMLHttpRequest.status + " readyState:" + XMLHttpRequest.readyState + " textStatus:" + textStatus);
            vResult = false;
        }
    });
    return vResult;
}

function checkAccountUserName(username, userId) {
    var vResult = true;
    $.ajax({
        url: "/AccountSetting/CheckAccountUserName",
        data: { UserName: username, UserId: userId },
        type: "POST",
        async: false,
        success: function (data) {
            if (data == false) {
                vResult = false;
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert("Check admin account user name error! status:" + XMLHttpRequest.status + " readyState:" + XMLHttpRequest.readyState + " textStatus:" + textStatus);
            vResult = false;
        }
    });
    return vResult;
}

function saveAccountInfo() {
    var accountInfo = getAccountInfoObj()
    var accountInfoJsonStr = JSON.stringify(accountInfo);
    $.ajax({
        url: "/AccountSetting/SaveAccountInformation",
        data: { AccountJsonStr: accountInfoJsonStr },
        type: "POST",
        success: function (data) {
            if (data == 'success') {
                alert("Save successfully.");
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert("Save admin account info error! status:" + XMLHttpRequest.status + " readyState:" + XMLHttpRequest.readyState + " textStatus:" + textStatus);
        }
    });
}

function getAccountInfoObj() {
    var account = new Object();
    account.UserId = $('#UserId').val();
    account.DisplayName = $('#DisplayName').val();
    account.Email = $('#Email').val();
    account.Phone = $('#Phone').val();
    account.Notes = $('#Notes').val();
    return account;
}

/******Personal Information Page******/