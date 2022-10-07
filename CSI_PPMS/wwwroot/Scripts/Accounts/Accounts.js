function logoutfunc() {
    window.location.href = '/Accounts/Login'
}
function display_c() {
    var refresh = 500; // Refresh rate in milli seconds
    mytime = setTimeout('display_ct()', refresh)
}

function display_ct() {
    //var x = new Date();
    //var month = x.getMonth() + 1;
    //var x1 = x.getDate() + "/" + month + "/" +  x.getFullYear();
    //var hours = x.getHours();
    //var AMPM = "AM";
    //var minutes = x.getMinutes();
    //var seconds = x.getSeconds();
    //if (hours > 12) {
    //    hours = hours - 12;
    //    AMPM = "PM";
    //    if (hours.toLocaleString().length == 1) {
    //        hours = "0" + hours;
    //    }
    //}
    //if (minutes.toLocaleString().length == 1) {
    //    minutes = "0" + minutes;
    //}
    //if (seconds.toLocaleString().length == 1) {
    //    seconds = "0" + seconds;
    //}
    //x1 = x1 + " - " + hours + ":" + minutes + ":" + seconds + " " + AMPM;
    document.getElementById('lblTime').innerHTML = GetCurrentDate();
    display_c();
}

$(document).ready(function () {
    display_ct();
});



function GetCurrentDate() {
    var x = new Date();
    var month = x.getMonth() + 1;
    var x1 = x.getDate() + "/" + month + "/" + x.getFullYear();
    var hours = x.getHours();
    var AMPM = "AM";
    var minutes = x.getMinutes();
    var seconds = x.getSeconds();
    if (hours > 12) {
        hours = hours - 12;
        AMPM = "PM";
        if (hours.toLocaleString().length == 1) {
            hours = "0" + hours;
        }
    }
    if (minutes.toLocaleString().length == 1) {
        minutes = "0" + minutes;
    }
    if (seconds.toLocaleString().length == 1) {
        seconds = "0" + seconds;
    }
    x1 = x1 + " - " + hours + ":" + minutes + ":" + seconds + " " + AMPM;
    return x1;
}