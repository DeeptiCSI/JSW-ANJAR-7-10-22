
function GetData() {
    if ($("#txtPlateNo").val() == "") {
        $("#txtplateError").html("Please enter plate No");
        document.getElementById("lblmessages").innerHTML += '\n<p style="margin-bottom:5px">' + GetCurrentDate() + " &lt;" + document.getElementById("UserNameId").innerText + "&gt; " + 'Please enter plate No</p>';
        document.getElementsByClassName('footer-area')[0].scrollTop = document.getElementsByClassName('footer-area')[0].scrollHeight;

        return;
    }

    $.ajax('/Home/CheckPlateNo?plateNo=' + $("#txtPlateNo").val(), {
        type: 'Get',  // http method
        //data: { plateNo: $("#txtPlateNo").val()  },  // data to submit
        success: function (data) {
            document.getElementById("lblmessages").innerHTML += '\n<p style="margin-bottom:5px">' + GetCurrentDate() + " &lt;" + document.getElementById("UserNameId").innerText + "&gt; " + data.message + '</p>';
            document.getElementsByClassName('footer-area')[0].scrollTop = document.getElementsByClassName('footer-area')[0].scrollHeight;

            if (data.status == 1) {
                return;
            }

            $("#customers tbody").empty();
            $("#txtPlateNo").val("");

            var plateList = "";
            for (var i = 0; i < data.plateList.length; i++) {
                plateList += "<tr>";

                plateList += "<td>" + data.plateList[i].lineNo + "</td>";
                plateList += "<td><input type='checkbox' name='chkmarking' class='chkmarking' id='chkmark_" + i + "' " + data.plateList[i].marking + "/></td>";
                plateList += "<td><input type='checkbox' name='chkpunching' class='chkpunching' id='chkpunch_" + i + "' " + data.plateList[i].punching + "/></td>";
                plateList += "<td>" + data.plateList[i].prefix_Text + "</td>";
                //plateList += "<td>" + data.plateList[i] +"</td>";

                plateList += "</tr>";
            }

            $("#customers tbody").append(plateList);
        },
        error: function (jqXhr, textStatus, errorMessage) {
            $("#txtplateError").html('Error' + errorMessage);
        }
    });
}

function SendToMarking() {
    var Counter = 0;
    var Line1 = "", Line2 = "", Line3 = "", Line4 = "", Line5 = "", Line6 = "";
    document.getElementById("lblmessages").innerHTML += '\n<p style="margin-bottom:5px">' + GetCurrentDate() + " &lt;" + document.getElementById("UserNameId").innerText + "&gt; " + 'Please wait while sending data to punching</p>';
    document.getElementsByClassName('footer-area')[0].scrollTop = document.getElementsByClassName('footer-area')[0].scrollHeight;

    $('input[name=chkmarking]').each(function () {
        if (Counter > 5) {
            this.checked = false;
        }
        if (this.checked === true)
            Counter = Counter + 1;

        var InnerText = $(this).parent().parent().children("td")[3].innerHTML;
        if (Counter == 0)
            Line1 = InnerText;

        if (Counter == 1)
            Line2 = InnerText;

        if (Counter == 2)
            Line3 = InnerText;

        if (Counter == 3)
            Line4 = InnerText;

        if (Counter == 4)
            Line5 = InnerText;

        if (Counter == 5)
            Line6 = InnerText;
    });


    if (Counter <= 0) {
        document.getElementById("lblmessages").innerHTML += '\n<p style="margin-bottom:5px">' + GetCurrentDate() + " &lt;" + document.getElementById("UserNameId").innerText + "&gt; " + 'Please select atleast 1</p>';
        document.getElementsByClassName('footer-area')[0].scrollTop = document.getElementsByClassName('footer-area')[0].scrollHeight;

        return;
    }
    if (Counter > 6) {
        document.getElementById("lblmessages").innerHTML += '\n<p style="margin-bottom:5px">' + GetCurrentDate() + " &lt;" + document.getElementById("UserNameId").innerText + "&gt; " + 'Maximum Allowed 5</p>';
        document.getElementsByClassName('footer-area')[0].scrollTop = document.getElementsByClassName('footer-area')[0].scrollHeight;

        return;
    }

    //Prefix_Text
    var model = { Line1: Line1, Line2: Line2, Line3: Line3, Line4: Line4, Line5: Line5, Line6: Line6 };

    $.ajax('/Home/SendToMarking', {
        type: 'Post',  // http method
        data: model,  // data to submit
        success: function (data) {
            document.getElementById("lblmessages").innerHTML += '\n<p style="margin-bottom:5px">' + GetCurrentDate() + " &lt;" + document.getElementById("UserNameId").innerText + "&gt; " + data.message + "</p>';
            document.getElementsByClassName('footer-area')[0].scrollTop = document.getElementsByClassName('footer-area')[0].scrollHeight;

            UncheckAll();
        },
        error: function (jqXhr, textStatus, errorMessage) {
            document.getElementById("lblmessages").innerHTML += '\n<p style="margin-bottom:5px">' + GetCurrentDate() + " &lt;" + document.getElementById("UserNameId").innerText + "&gt; " + 'Error ' + errorMessage + '</p>';
            document.getElementsByClassName('footer-area')[0].scrollTop = document.getElementsByClassName('footer-area')[0].scrollHeight;

        }
    });
}

function SendToPunching() {
    var button = document.getElementById("sendToPunchingbtn");
    button.disabled = true;
    var Counter = 0;
    var Line1 = "", Line2 = "", Line3 = "", Line4 = "", Line5 = "", Line6 = "";
    document.getElementById("lblmessages").innerHTML += '\n<p style="margin-bottom:5px">' + GetCurrentDate() + " &lt;" + document.getElementById("UserNameId").innerText + "&gt; " + 'Please wait while sending data to punching</p>';
    document.getElementsByClassName('footer-area')[0].scrollTop = document.getElementsByClassName('footer-area')[0].scrollHeight;

    $('input[name=chkpunching]').each(function () {
        if (Counter > 5) {
            this.checked = false;
        }
        if (this.checked === true)
            Counter = Counter + 1;

        var InnerText = $(this).parent().parent().children("td")[3].innerHTML;
        if (Counter == 0)
            Line1 = InnerText;

        if (Counter == 1)
            Line2 = InnerText;

        if (Counter == 2)
            Line3 = InnerText;

        if (Counter == 3)
            Line4 = InnerText;

        if (Counter == 4)
            Line5 = InnerText;

        if (Counter == 5)
            Line6 = InnerText;
    });


    if (Counter <= 0) {
        document.getElementById("lblmessages").innerHTML += '\n<p style="margin-bottom:5px">' + GetCurrentDate() + " &lt;" + document.getElementById("UserNameId").innerText + "&gt; " + 'Please select atleast 1</p>';
        document.getElementsByClassName('footer-area')[0].scrollTop = document.getElementsByClassName('footer-area')[0].scrollHeight;

        return;
    }
    if (Counter > 6) {
        document.getElementById("lblmessages").innerHTML += '\n<p style="margin-bottom:5px">' + GetCurrentDate() + " &lt;" + document.getElementById("UserNameId").innerText + "&gt; " + 'Maximum Allowed 5</p>';
        document.getElementsByClassName('footer-area')[0].scrollTop = document.getElementsByClassName('footer-area')[0].scrollHeight;

        return;
    }

    //Prefix_Text
    var model = { Line1: Line1, Line2: Line2, Line3: Line3, Line4: Line4, Line5: Line5, Line6: Line6 };

    $.ajax('/Home/SendToPunching', {
        type: 'Post',  // http method
        data: model,  // data to submit
        success: function (data) {
            button.disabled = false;
            document.getElementById("lblmessages").innerHTML += '\n<p style="margin-bottom:5px">' + GetCurrentDate() + " &lt;" + document.getElementById("UserNameId").innerText + "&gt; " + data.message + "</p>";
            document.getElementsByClassName('footer-area')[0].scrollTop = document.getElementsByClassName('footer-area')[0].scrollHeight;

            UncheckAll();
        },
        error: function (jqXhr, textStatus, errorMessage) {
            button.disabled = false;
            document.getElementById("lblmessages").innerHTML += '\n<p style="margin-bottom:5px">' + GetCurrentDate() + " &lt;" + document.getElementById("UserNameId").innerText + "&gt; " + 'Error' + errorMessage + "</p>";
            document.getElementsByClassName('footer-area')[0].scrollTop = document.getElementsByClassName('footer-area')[0].scrollHeight;

        }
    });
}

function UncheckAll() {
    $('input[name=chkmarking]').each(function () {
        this.checked = false;
    });

    $('input[name=chkpunching]').each(function () {
        this.checked = false;
    });
}

$(document).on("change", "input[name='chkmarking']", function () {
    var counter = 0;
    $('input[name=chkmarking]').each(function () {
        if (counter > 5) {
            this.checked = false;
        }
        if (this.checked === true)
            counter = counter + 1;
    });
});

$(document).on("change", "input[name='chkpunching']", function () {
    var counter = 0;
    $('input[name=chkpunching]').each(function () {
        if (counter > 5) {
            this.checked = false;
        }
        if (this.checked === true)
            counter = counter + 1;
    });
});


$(document).ready(function () {
});