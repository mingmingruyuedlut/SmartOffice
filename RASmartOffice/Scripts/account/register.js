function bindRegisterPageEvent() {
    $('.login-back-btn').bind('click', function () {
        window.location.href = "/Account/Login";
    });
}