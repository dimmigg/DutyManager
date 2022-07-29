$(function () {
    $(document).ajaxComplete(function () {
        $.validator.unobtrusive.parse(document);
    });
});

function getDate(dt) {
    var year = dt.getFullYear();
    var month = '';
    var month1 = dt.getMonth() + 1;
    if (month1 < 10) {
        month = '0' + month1;
    } else {
        month = month1;
    }

    var day = '';
    if (dt.getDate() < 10) {
        day = '0' + dt.getDate();
    }
    else {
        day = dt.getDate();
    }
    return year + "-" + month + "-" + day;
}

var dt = (new Date(new Date().setDate(1)));
dt.setMonth(dt.getMonth() + 1);
document.getElementById("BeginDate").value = getDate(dt);
dt.setMonth(dt.getMonth() + 1);
dt.setDate(dt.getDate() - 1);
document.getElementById("EndDate").value = getDate(dt)