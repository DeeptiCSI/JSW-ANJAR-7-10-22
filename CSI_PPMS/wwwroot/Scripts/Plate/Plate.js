
var counterp = 0;
var counterm = 0;
var logindata = { UserName: "", RoleId: "" };



$(document).ready(function () {
    var ModuleID = GetModuleId();

    $('#customers').on('click', 'input[type="checkbox"]', function (e) {
        var x = this;
        var maxLines = 6;
        var lineNo = e.currentTarget.parentElement.parentElement.children[0].innerHTML;
        if (x.name == "chkpunching") {
            counterp = 0;
            var abc = document.getElementsByName("chkpunching");
            $.each(abc, function (index, value) {
                if (value.value == "true") {
                    counterp = counterp + 1;
                }
            });
            if (x.checked == true) {
                if (counterp < maxLines) {
                    this.value = true;
                    counterp = counterp + 1;
                    $.ajax('/Plate/SaveCheckPunch?id=' + lineNo + '&status=' + x.checked, {
                        type: "get",
                    });
                }
                else {
                    this.checked = false;
                    this.value = false;
                    $.ajax('/Plate/SaveCheckPunch?id=' + lineNo + '&status=' + x.checked, {
                        type: "get",
                    });
                }
            }
            else if (x.checked == false && counterp > 0) {
                this.value = false;
                counterp = counterp - 1;
                $.ajax('/Plate/SaveCheckPunch?id=' + lineNo + '&status=' + x.checked, {
                    type: "get",
                });
            }
        }
        else if (x.name == "chkmarking") {
            counterm = 0;
            var abc = document.getElementsByName("chkmarking");
            $.each(abc, function (index, value) {
                if (value.value == "true") {
                    counterm = counterm + 1;
                }
            });
            if (x.checked == true) {
                if (counterm < maxLines) {
                    this.value = true;
                    counterm = counterm + 1;
                    $.ajax('/Plate/SaveCheckMark?id=' + lineNo + '&status=' + x.checked, {
                        type: "get",
                    });
                    if (document.getElementById("moduleId") == 5) {
                        AppendLine(this);
                    }
                }
                else {
                    this.checked = false;
                    this.value = false;
                    $.ajax('/Plate/SaveCheckMark?id=' + lineNo + '&status=' + x.checked, {
                        type: "get",
                    });
                }
            }
            else if (x.checked == false && counterm > 0) {
                this.value = false;
                counterm = counterm - 1;
                $.ajax('/Plate/SaveCheckMark?id=' + lineNo + '&status=' + x.checked, {
                    type: "get",
                });
                if (document.getElementById("moduleId").value == "5") {
                    RemoveLine(this);
                }
            }

        }
    });

    $('#getData').click(function () {
        counterp = 0;
        counterm = 0;

    });


    if (document.getElementById('DownCoilerAutoModeSwitch') != undefined) {
        var sidebar = document.getElementById("left-section");
        var manualboxes = $('#manualModeTemplate').html();
        sidebar.innerHTML += manualboxes;
    }


    $('#DownCoilerAutoModeSwitch').click(function (e) {
        var sidebar = document.getElementById("left-section");
        var boxes = $('#autoModeTemplate').html();
        var statusboxes = $('#automodestatusboxes').html();
        var manualboxes = $('#manualModeTemplate').html();
        if (this.checked) {
            document.getElementById("AutoModeButtons").innerHTML = "";
            document.getElementById("manualModetemp").remove();
            sidebar.innerHTML = boxes + sidebar.innerHTML;
            document.getElementById("AutoModeButtons").innerHTML = statusboxes;
        }
        else {
            //document.getElementsByClassName("purchesingstatus").remove();
            document.getElementById("AutoModeButtons").innerHTML = "";
            var buttons = document.getElementById("AutoModeButtons");
            var cyclebutton = document.createElement("button");
            cyclebutton.classList.add("load-btn");
            cyclebutton.onclick = "LoadData()";
            cyclebutton.innerHTML = "Start Marking Cycle";
            cyclebutton.type = "button";
            var loadbutton = document.createElement("button");
            loadbutton.classList.add("load-btn");
            loadbutton.onclick = "FeedDataToPlc()";
            loadbutton.innerHTML = "Load Data";
            loadbutton.type = "button";
            loadbutton.id = "sendDataToPlc";
            buttons.appendChild(cyclebutton);
            buttons.appendChild(loadbutton);

            //sidebar.innerHTML = sidebar.innerHTML.replace(boxes, "");
            document.getElementById("autoModeTemp").remove();
            sidebar.innerHTML += manualboxes;
        }
    });

    if (document.getElementById("moduleId").value == "5") {

        $('#Update-Weight').click(function (e) {
            GetWeightDataDC();
            var weight = document.getElementById("WeigthBox").innerText;
            $.ajax({
                url: "/Plate/UpdateWeightdata?weight=" + weight,
                type: "GET",
                success: function (data) {
                    if (data.statusCode == 200) {
                        document.getElementById("MatIdDC").value = data.data;
                        document.getElementById("lblmessages").innerHTML += '\n<p style="margin-bottom:5px">' + GetCurrentDate() + " &lt;" + document.getElementById("UserNameId").innerText + "&gt; " + data.messege + '</p>';
                        document.getElementsByClassName('footer-area')[0].scrollTop = document.getElementsByClassName('footer-area')[0].scrollHeight;
                        document.getElementById("txtPlateNo").value = data.data;
                    }
                }
            })
        });

        GetWeightDataDC();
    }

});


function AppendLine(currentData) {
    var CTBody = document.getElementById("childplatetablebody");
    var count = 0;
    if (CTBody.childNodes != undefined) {
        count = CTBody.children.length;
    }
    const tr = document.createElement("tr");
    tr.id = currentData.parentElement.parentElement.id + "_child";
    const td = document.createElement("td");
    td.innerHTML = count + 1;
    td.bgColor = "#F5F5F5";
    td.align = "center";
    tr.appendChild(td);

    const Shell = document.createElement("td");
    const txt = document.createElement("input");
    txt.type = "checkbox"
    txt.name = "ShellMarking"
    txt.checked = false;
    Shell.append(txt);
    Shell.bgColor = "#F5F5F5";
    tr.appendChild(Shell);

    const Disc = document.createElement("td");
    const txt1 = document.createElement("input");
    txt1.type = "checkbox"
    txt1.name = "DiscMarking"
    txt1.checked = false;
    Disc.append(txt1);
    Disc.bgColor = "#F5F5F5";
    tr.appendChild(Disc);


    const PreFix = document.createElement("td");
    PreFix.innerHTML = currentData.parentElement.parentElement.children[2].innerHTML;
    PreFix.bgColor = "#F5F5F5";
    tr.appendChild(PreFix);

    CTBody.appendChild(tr);

}


function RemoveLine(currentData) {
    var count = 1;
    var child = document.getElementById(currentData.parentElement.parentElement.id + "_child");
    if (child != undefined) {
        child.remove();
    }
    var Ctable = document.getElementById("childplatetablebody").childNodes;
    $.each(Ctable, function (index, value) {
        if (value.children != undefined) {
            value.children[0].innerHTML = count;
            count = count + 1;
        }
    });
    count = 1;
}


var Counter = 0;
function SendToPunching() {
    var button = document.getElementById("sendToPunchingbtn");
    button.disabled = true;
    counterp = 0;
    $('.completed-box')[0].children[0].innerHTML = "Data Received in Controller";
    var abc = document.getElementsByName("chkpunching");
    $.each(abc, function (index, value) {
        if (value.value == "true") {
            counterp = counterp + 1;
        }
    });
    if (counterp != 0 && counterp <= 6) {
        var model = { Line1: "", Line2: "", Line3: "", Line4: "", Line5: "", Line6: "", plateNo: "" };
        var lineNub = 3;
        if ($('.market-btn')[0] == undefined) {
            lineNub = 2;
        }
        model.plateNo = $('#plateno')[0].value;
        document.getElementById("lblmessages").innerHTML += '\n<p style="margin-bottom:5px">' + GetCurrentDate() + " &lt;" + document.getElementById("UserNameId").innerText + "&gt; " + 'Data Received in Controller, Start Punching</p>';
        document.getElementsByClassName('footer-area')[0].scrollTop = document.getElementsByClassName('footer-area')[0].scrollHeight;

        $('input[name=chkpunching]').each(function () {
            if (this.value == "true" && this.name == "chkpunching") {
                var InnerText = $(this).parent().parent().children("td")[lineNub].innerHTML;
                if (Counter == 0)
                    model.Line1 = InnerText;

                if (Counter == 1)
                    model.Line2 = InnerText;

                if (Counter == 2)
                    model.Line3 = InnerText;

                if (Counter == 3)
                    model.Line4 = InnerText;

                if (Counter == 4)
                    model.Line5 = InnerText;

                if (Counter == 5)
                    model.Line6 = InnerText;

                Counter = Counter + 1;

                if (Counter == 0) {
                    document.getElementById("lblmessages").innerHTML += '\n<p style="margin-bottom:5px">' + GetCurrentDate() + " &lt;" + document.getElementById("UserNameId").innerText + "&gt; " + 'Please select atleast 1</p>';
                    document.getElementsByClassName('footer-area')[0].scrollTop = document.getElementsByClassName('footer-area')[0].scrollHeight;

                    return;
                }
            }
        });
        var userName = document.getElementById("userId").value;
        var modid = document.getElementById("moduleId").value;
        var type = "normal";
        var log = "Data Sent to Punching from user " + userName + " and the data is " + "Line1 = " + model.Line1 + ", " + "Line2 = " + model.Line2 + ", " + "Line3 = " + model.Line3 + ", " + "Line4 = " + model.Line4 + ", " + "Line5 = " + model.Line5 + ", " + "Line6 = " + model.Line6;
        AddLog(userName, modid, log, type);
        $.ajax('/Plate/SendToPunching', {
            type: 'Post',
            data: model,
            success: function (data) {
                button.disabled = false;

                $('.completed-box')[0].children[0].innerHTML = data.messege;
                document.getElementById("lblmessages").innerHTML += '\n<p style="margin-bottom:5px">' + GetCurrentDate() + " &lt;" + document.getElementById("UserNameId").innerText + "&gt; " + data.messege + "</p>";
                document.getElementsByClassName('footer-area')[0].scrollTop = document.getElementsByClassName('footer-area')[0].scrollHeight;
                //ChangeIdleState(5000);

            },
            error: function (jqXhr, textStatus, errorMessage) {
                button.disabled = false;
                $('.completed-box')[0].children[0].innerHTML = "Failed";
                document.getElementById("lblmessages").innerHTML += '\n<p style="margin-bottom:5px">' + GetCurrentDate() + " &lt;" + document.getElementById("UserNameId").innerText + "&gt; " + 'Error in sending data to PLC' + errorMessage + "</p>";
                document.getElementsByClassName('footer-area')[0].scrollTop = document.getElementsByClassName('footer-area')[0].scrollHeight;
                ChangeIdleState(2000);
            }
        });
        Counter = 0;
    }
    else if (counterp > 6) {
        alert('please select only 6 lines of Data');
    }
    else {
        alert('please select atleast 1 Data');
    }
}




function SendToMarking() {
    $('.not-started-box')[0].children[0].innerHTML = "Marking";
    var abc = document.getElementsByName("chkpunching");
    $.each(abc, function (index, value) {
        if (value.value == "true") {
            counterm = counterm + 1;
        }
    });
    if (counterm != 0) {
        var model = { Line1: "", Line2: "", Line3: "", Line4: "", Line5: "", Line6: "", plateNo: "" };
        model.plateNo = $('#plateno')[0].value;
        var lineNub = 3;
        if ($('.market-btn')[0] == undefined) {
            lineNub = 2;
        }
        document.getElementById("lblmessages").innerHTML += '\n<p style="margin-bottom:5px">' + GetCurrentDate() + " &lt;" + document.getElementById("UserNameId").innerText + "&gt; " + 'Please wait while sending data to Marking</p>';
        document.getElementsByClassName('footer-area')[0].scrollTop = document.getElementsByClassName('footer-area')[0].scrollHeight;

        $('input[name=chkmarking]').each(function () {
            if (this.value == "true" && this.name == "chkmarking") {
                var InnerText = $(this).parent().parent().children("td")[lineNub].innerHTML;
                if (Counter == 0)
                    model.Line1 = InnerText;

                if (Counter == 1)
                    model.Line2 = InnerText;

                if (Counter == 2)
                    model.Line3 = InnerText;

                if (Counter == 3)
                    model.Line4 = InnerText;

                if (Counter == 4)
                    model.Line5 = InnerText;

                if (Counter == 5)
                    model.Line6 = InnerText;

                Counter = Counter + 1;

                if (Counter == 0) {
                    document.getElementById("lblmessages").innerHTML += '\n<p style="margin-bottom:5px">' + GetCurrentDate() + " &lt;" + document.getElementById("UserNameId").innerText + "&gt; " + 'Please select atleast 1</p>';
                    document.getElementsByClassName('footer-area')[0].scrollTop = document.getElementsByClassName('footer-area')[0].scrollHeight;

                    return;
                }
            }
        });
        $.ajax('/Plate/SendToMarking', {
            type: 'Post',
            data: model,
            success: function (data) {
                $('.not-started-box')[0].children[0].innerHTML = "Data Updated";
                document.getElementById("lblmessages").innerHTML += '\n<p style="margin-bottom:5px">' + GetCurrentDate() + " &lt;" + document.getElementById("UserNameId").innerText + "&gt; " + data.messege + "</p>";
                document.getElementsByClassName('footer-area')[0].scrollTop = document.getElementsByClassName('footer-area')[0].scrollHeight;

            },
            error: function (jqXhr, textStatus, errorMessage) {
                $('.not-started-box')[0].children[0].innerHTML = "Failed";
                document.getElementById("lblmessages").innerHTML += '\n<p style="margin-bottom:5px">' + GetCurrentDate() + " &lt;" + document.getElementById("UserNameId").innerText + "&gt; " + 'Error' + errorMessage + "</p>";
                document.getElementsByClassName('footer-area')[0].scrollTop = document.getElementsByClassName('footer-area')[0].scrollHeight;

            }
        });
        Counter = 0;
    }
    else {
        alert('please select atleast 1 Data');
    }
}


function TestMechineAutoMode() {
    let automode;
    $.ajax({
        url: '/Plate/CheckAutoMode',
        type: 'GET',
        success: function (data) {
            if (data.connectionStatus == true) {
                if (data.dataReadStatus == true) {
                    if (data.autoModeStatus == true) {
                        $('.not-started-box')[0].children[0].innerHTML = "Auto Mode";
                        document.getElementById("lblmessages").innerHTML += '\n<p style="margin-bottom:5px">' + GetCurrentDate() + " &lt;" + document.getElementById("UserNameId").innerText + "&gt; " + 'Mechine is in auto mode</p>';
                        document.getElementsByClassName('footer-area')[0].scrollTop = document.getElementsByClassName('footer-area')[0].scrollHeight;
                    }
                    else {
                        $('.not-started-box')[0].children[0].innerHTML = "Auto Mode Not Satisfied";
                        document.getElementById("lblmessages").innerHTML += '\n<p style="margin-bottom:5px">' + GetCurrentDate() + " &lt;" + document.getElementById("UserNameId").innerText + "&gt; " + 'Mechine is not in auto mode</p>';
                        document.getElementsByClassName('footer-area')[0].scrollTop = document.getElementsByClassName('footer-area')[0].scrollHeight;
                        alert("Mechine is not in auto mode");
                    }
                }
                else {
                    $('.not-started-box')[0].children[0].innerHTML = "failed";
                    document.getElementById("lblmessages").innerHTML += '\n<p style="margin-bottom:5px">' + GetCurrentDate() + " &lt;" + document.getElementById("UserNameId").innerText + "&gt; " + 'Connection to PLC is Success but not able read the data from DB</p>';
                    document.getElementsByClassName('footer-area')[0].scrollTop = document.getElementsByClassName('footer-area')[0].scrollHeight;
                    alert("Connection to PLC is Success but not able read the data from DB");
                }
            }
            else {
                $('.not-started-box')[0].children[0].innerHTML = "failed";
                document.getElementById("lblmessages").innerHTML += '\n<p style="margin-bottom:5px">' + GetCurrentDate() + " &lt;" + document.getElementById("UserNameId").innerText + "&gt; " + 'Connection to PLC Failed</p>';
                document.getElementsByClassName('footer-area')[0].scrollTop = document.getElementsByClassName('footer-area')[0].scrollHeight;
                alert("Connection to PLC Failed");
            }
            automode = data.autoModeStatus;
        }
    });
    return automode;
}


function GetSapData(matid = "") {
    //$('.not-started-box')[0].children[0].innerHTML = "Fetching";
    var model = { moduleId: "", RoleId: "", matid: matid, autoMode: false };
    debugger;
    model.autoMode = document.getElementById("DownCoilerAutoModeSwitch").checked;
    model.moduleId = $('#moduleId')[0].innerText;
    model.RoleId = $('#roleId')[0].innerText;
    $.ajax({
        url: '/Plate/GetSapDataDownCoiler',
        type: 'Post',
        data: model,
        success: function (data) {
            if (data.messege != "error") {
                $('#platetablebody').empty();
                $('#plateno')[0].value = data.messege;
                $.each(data.data, function (index, qwe) {
                    const tr = document.createElement("tr");
                    var line = 1
                    $.each(qwe, function (index, value) {
                        if (index == "lineNo") {
                            const td = document.createElement("td")
                            line = value
                            td.textContent = value;
                            td.bgColor = "#F5F5F5";
                            td.align = "center";
                            tr.appendChild(td);
                        }
                        else if (index == "marking") {
                            const td = document.createElement("td");
                            const txt = document.createElement("input");
                            txt.type = "checkbox"
                            txt.id = line.toString()
                            txt.value = value
                            txt.name = "chkmarking"
                            txt.checked = value
                            txt.disabled = true;
                            td.append(txt);
                            td.bgColor = "#F5F5F5";
                            tr.appendChild(td);
                        }
                        else if (index == "punching") {
                            const td = document.createElement("td");
                            const txt = document.createElement("input");
                            txt.type = "checkbox"
                            txt.id = line.toString()
                            txt.value = value
                            txt.name = "chkmarking"
                            txt.checked = value
                            txt.disabled = true;
                            td.append(txt);
                            td.bgColor = "#F5F5F5";
                            tr.appendChild(td);
                        }
                        else if (index == "prefix_Text") {
                            if (value.includes("PLATE NO")) {
                                var splitstring = value.split("-")
                                document.getElementById("txtPlateNo").value = splitstring[1];
                            }
                            const td = document.createElement("td")
                            td.textContent = value;
                            td.bgColor = "#F5F5F5";
                            td.align = "left";
                            if (model.moduleId == 4) {
                                td.contentEditable = false;
                            }
                            else {
                                td.contentEditable = true;
                            }
                            tr.appendChild(td);
                        }
                    });
                    tr.align = "center";
                    tr.vAlign = "center";
                    tr.id = "11_" + line
                    line += 1;
                    document.getElementById("platetablebody").appendChild(tr);
                });

                //AddCheckData();
                AddTemplateLines(document.getElementById("moduleId").value);
                $('.not-started-box')[0].children[0].innerHTML = "Data Fetched";
                document.getElementById('lblmessages').innerHTML += '\n<p style="margin-bottom:5px">' + GetCurrentDate() + " &lt;" + document.getElementById("UserNameId").innerText + "&gt; " + 'Successfully Fetched Data from SAP</p>';
                document.getElementsByClassName('footer-area')[0].scrollTop = document.getElementsByClassName('footer-area')[0].scrollHeight;
                $('#sendDataToPlc')[0].disabled = false;
            }
            else {
                document.getElementById('lblmessages').innerHTML += '\n<p style="margin-bottom:5px">' + GetCurrentDate() + " &lt;" + document.getElementById("UserNameId").innerText + "&gt; " + 'invalid plate number</p>';
                document.getElementsByClassName('footer-area')[0].scrollTop = document.getElementsByClassName('footer-area')[0].scrollHeight;

                alert('invalid plate number');
            }
        }
    })
}



function AddCheckData() {
    var table = document.getElementById("platetablebody");
    $.each(table.childNodes, function (index, value) {
        if (value.children[1].children[0].value == "true") {
            AppendLine(value.children[1].children[0]);
        }
    });
}


function CheckWeighingData() {
    $('.not-started-box')[2].children[0].innerHTML = "Checking";
    $.ajax({
        url: '/Plate/GetWeighingData',
        type: 'GET',
        success: function (data) {
            $('.not-started-box')[2].children[0].innerHTML = data;
            GetSapData();
        }
    });

}




function CheckMarkerReaddHomePosition() {
    $('.not-started-box')[1].children[0].innerHTML = "Checking";
    $.ajax({
        url: '/Plate/CheckMarkerReady',
        type: 'GET',
        success: function (data) {
            if (data.markerInHomePosition != true && data.markerActive != true) {
                $('.not-started-box')[1].children[0].innerHTML = "Marker Ready";
                CheckWeighingData();
            }
            else if (data.markingPosition == false) {
                $('.not-started-box')[1].children[0].innerHTML = "Marker not Ready";
                alert("Marker is not in home position");
            }
            else if (data.markerActive == false) {
                $('.not-started-box')[1].children[0].innerHTML = "Marker not Ready";
                alert("Marker is not active");
            }
        }
    })

}




function LoadData() {
    //$('.not-started-box')[0].children[0].innerHTML = "Loading";
    if (document.getElementById("DownCoilerAutoModeSwitch").value == true) {
        $.ajax({
            url: '/Plate/CheckAutoMode',
            type: 'GET',
            success: function (data) {
                if (data.connectionStatus != true) {
                    if (data.dataReadStatus != true) {
                        if (data.autoModeStatus != true) {
                            $('.not-started-box')[0].children[0].innerHTML = "Auto Mode";
                            document.getElementById("lblmessages").innerHTML += '\n<p style="margin-bottom:5px">' + GetCurrentDate() + " &lt;" + document.getElementById("UserNameId").innerText + "&gt; " + 'Mechine is in auto mode</p>';
                            document.getElementsByClassName('footer-area')[0].scrollTop = document.getElementsByClassName('footer-area')[0].scrollHeight;
                            CheckMarkerReaddHomePosition();
                        }
                        else {
                            $('.not-started-box')[0].children[0].innerHTML = "Auto Mode Not Satisfied";
                            document.getElementById("lblmessages").innerHTML += '\n<p style="margin-bottom:5px">' + GetCurrentDate() + " &lt;" + document.getElementById("UserNameId").innerText + "&gt; " + 'Mechine is not in auto mode</p>';
                            document.getElementsByClassName('footer-area')[0].scrollTop = document.getElementsByClassName('footer-area')[0].scrollHeight;
                            alert("Mechine is not in auto mode")
                        }
                    }
                    else {
                        $('.not-started-box')[0].children[0].innerHTML = "failed";
                        document.getElementById("lblmessages").innerHTML += '\n<p style="margin-bottom:5px">' + GetCurrentDate() + " &lt;" + document.getElementById("UserNameId").innerText + "&gt; " + 'Connection to PLC is Success but not able read the data from DB</p>';
                        document.getElementsByClassName('footer-area')[0].scrollTop = document.getElementsByClassName('footer-area')[0].scrollHeight;
                        alert("Connection to PLC is Success but not able read the data from DB")
                    }
                }
                else {
                    $('.not-started-box')[0].children[0].innerHTML = "failed";
                    document.getElementById("lblmessages").innerHTML += '\n<p style="margin-bottom:5px">' + GetCurrentDate() + " &lt;" + document.getElementById("UserNameId").innerText + "&gt; " + 'Connection to PLC Failed</p>';
                    document.getElementsByClassName('footer-area')[0].scrollTop = document.getElementsByClassName('footer-area')[0].scrollHeight;
                    alert("Connection to PLC Failed")
                }
                if (data.automode == true) {

                }
            }
        });
    }
    else {
        var matid
        if (document.getElementById("MatIdDC") != undefined) {
            matid = document.getElementById("MatIdDC").value;
        }
        else {
            matid = 0;
        }
        GetSapData(matid);
    }
}


function FeedDataToPlc() {
    var model = { shellLine1: "", shellLine2: "", shellLine3: "", shellLine4: "", discLine1: "", discLine2: "", isShell: false, isDisc: false, plateNo: "" };
    var data = document.getElementById("platetablebody").childNodes;
    var shellCount = 0;
    var discCount = 0;
    $.each(data, function (index, value) {
        if (value.nodeName != "#text") {
            if (value.children[1].children[0].checked && shellCount < 4) {
                if (shellCount == 0) {
                    model.shellLine1 = value.children[3].innerHTML;
                }
                if (shellCount == 1) {
                    model.shellLine2 = value.children[3].innerHTML;
                }
                if (shellCount == 2) {
                    model.shellLine3 = value.children[3].innerHTML;
                }
                if (shellCount == 3) {
                    model.shellLine4 = value.children[3].innerHTML;
                }
                shellCount = shellCount + 1;
            }
            if (value.children[2].children[0].checked && discCount < 2) {
                if (discCount == 0) {
                    model.discLine1 = value.children[3].innerHTML;
                }
                if (discCount == 1) {
                    model.discLine2 = value.children[3].innerHTML;
                }
                discCount = discCount + 1;
            }
        }
    });
    //model.isShell = document.getElementById("shellmarkingcheck").checked;
    //model.isDisc = document.getElementById("discmarkingcheck").checked;
    model.plateNo = $('#plateno')[0].value;
    $.ajax({
        url: '/Plate/SendToPunchingDownCoiler',
        type: 'POST',
        data: model,
        success: function (data) {

        }
    });
}


function SendToMarkingColdLeveller() {
    $('.completed-box')[0].children[0].innerHTML = "Pending";
    var model = { PlateNumber: "", SteelGrade: "", Thickness: "", Width: "", Length: "", Weight: "", plateNo: "" };
    document.getElementById("lblmessages").innerHTML += '\n<p style="margin-bottom:5px">' + GetCurrentDate() + " &lt;" + document.getElementById("UserNameId").innerText + "&gt; " + 'Please wait while Loading Data</p>';
    document.getElementsByClassName('footer-area')[0].scrollTop = document.getElementsByClassName('footer-area')[0].scrollHeight;
    var x = document.getElementById("platetablebody").children;
    for (var i = 0; i <= x.length - 1; i++) {
        var InnerText = x[i].children[1].innerHTML;

        if (InnerText.includes("STEEL GRADE -"))
            model.SteelGrade = InnerText;

        if (InnerText.includes("LENGTH -"))
            model.Length = InnerText;

        if (InnerText.includes("THICKNESS -"))
            model.Thickness = InnerText;

        if (InnerText.includes("WIDTH -"))
            model.Width = InnerText;

        if (InnerText.includes("WEIGHT -"))
            model.Weight = InnerText;

        Counter = Counter + 1;
    }

    model.plateNo = $('#plateno')[0].value;
    $.ajax('/Plate/LoadDataColdLeveller', {
        type: 'Post',
        data: model,
        success: function (data) {
            Counter = 0;
            if (data.statusCode != 200) {
                alert(data.messege)
                $('.completed-box')[0].children[0].innerHTML = "Failed";
                document.getElementById("lblmessages").innerHTML += '\n<p style="margin-bottom:5px">' + GetCurrentDate() + " &lt;" + document.getElementById("UserNameId").innerText + "&gt; " + data.messege + "</p>";
                document.getElementsByClassName('footer-area')[0].scrollTop = document.getElementsByClassName('footer-area')[0].scrollHeight;
            }
            else {
                $('.completed-box')[0].children[0].innerHTML = "Completed";
                document.getElementById("lblmessages").innerHTML += '\n<p style="margin-bottom:5px">' + GetCurrentDate() + " &lt;" + document.getElementById("UserNameId").innerText + "&gt; " + data.messege + "</p>";
                document.getElementsByClassName('footer-area')[0].scrollTop = document.getElementsByClassName('footer-area')[0].scrollHeight;
            }
        },
        error: function (jqXhr, textStatus, errorMessage) {
            $('.completed-box')[0].children[0].innerHTML = "Failed";
            document.getElementById("lblmessages").innerHTML += '\n<p style="margin-bottom:5px">' + GetCurrentDate() + " &lt;" + document.getElementById("UserNameId").innerText + "&gt; " + 'Error' + errorMessage + "</p>";
            document.getElementsByClassName('footer-area')[0].scrollTop = document.getElementsByClassName('footer-area')[0].scrollHeight;

        }
    });
}


function YSValueUpdate(roleid, moduleid, type) {
    var model = { plateNo: "", grade: "", mininumThickness: "", maximumThickness: "", ySValue: "" }
    model.plateNo = document.getElementById("plateno").value;
    if (document.getElementById("MinThicknessText") != undefined) {
        model.mininumThickness = document.getElementById("MinThicknessText").value;
    }
    if (document.getElementById("MaxThicknessText") != undefined) {
        model.maximumThickness = document.getElementById("MaxThicknessText").value;
    }
    model.grade = document.getElementById("gradeText").value;
    model.ySValue = document.getElementById("YSValueText").value
    $.ajax({
        url: "/SAP/UpdateYsValue",
        method: "POST",
        data: model,
        success: function (ysResponse) {
            alert(ysResponse.messege);
            document.getElementById("lblmessages").innerHTML += '\n<p style="margin-bottom:5px">' + GetCurrentDate() + " &lt;" + document.getElementById("UserNameId").innerText + "&gt; " + ysResponse.messege + "</p>";
            document.getElementsByClassName('footer-area')[0].scrollTop = document.getElementsByClassName('footer-area')[0].scrollHeight;
            if (type == 1) {
                GetPlateData(roleid, moduleid);
                document.getElementById("load-button").disabled = false;
            }
        }
    });
}

var rowLines = [
    "JSW",
    "<WEIGHT>",
    "<HEAT NO>",
    "<GRADE>",
    "<GRADE2>",
    "<SIZE>",
    "<CUSTOMER REF>",
    "<PO ORDER>",
    "<PO NUMBER>",
    "<PLATE NO>",
    "<CUSTOMER NAME>"
]
var clrowlines = [
    "STEEL GRADE -<data>",
    "LENGTH -<data>",
    "THICKNESS -<data>",
    "WIDTH -<data>",
    "WEIGHT -<data>"
];



function AddTemplateLines(moduleid) {
    var rows;
    onlyrows = [];
    var TemplateRows = document.getElementById("TemplateSelection").value;
    $.ajax({
        url: "/Plate/GetTemplateRows?TemplateId=" + TemplateRows,
        type: "GET",
        success: function (response) {
            rows = response;
            $.each(rows, function (a, b) {
                onlyrows.push(b.row);
            });
            if (moduleid != 4) {
                if (onlyrows.length < 11) {
                    $.each(rowLines, function (x, y) {
                        if (!onlyrows.includes(y)) {
                            xyz = { id: 0, row: y };;
                            rows.push(xyz);
                        }
                    })
                }
            }
            else {
                if (onlyrows.length < 5) {
                    $.each(clrowlines, function (x, y) {
                        if (!onlyrows.includes(y)) {
                            xyz = { id: 0, row: y };;
                            rows.push(xyz);
                        }
                    })
                }
            }
            if (document.getElementById("childplatetablebody").children.length != 0) {
                document.getElementById("childplatetablebody").innerHTML = "";
            }
            var coilData = document.getElementById("platetablebody").children;
            $.each(coilData, function (q, w) {
                coilData[q].children[1].children[0].value = false;
                coilData[q].children[1].children[0].checked = false;
            });



            $.each(rows, function (x, y) {
                if (y.id != 0) {
                    $.each(coilData, function (a, b) {
                        var text = coilData[a].children[2].innerText;
                        if (text.includes("JSW") && y.row.includes("JSW")) {
                            coilData[a].children[1].children[0].value = true;
                            coilData[a].children[1].children[0].checked = true;
                            AppendLine(coilData[a].children[1].children[0]);
                        }
                        else if (text.includes("COIL NO") && y.row.includes("COIL NO")) {
                            coilData[a].children[1].children[0].value = true;
                            coilData[a].children[1].children[0].checked = true;
                            AppendLine(coilData[a].children[1].children[0]);
                        }
                        else if (text.includes("HEAT NO") && y.row.includes("HEAT NO")) {
                            coilData[a].children[1].children[0].value = true;
                            coilData[a].children[1].children[0].checked = true;
                            AppendLine(coilData[a].children[1].children[0]);
                        }
                        else if (text.includes("WIDTH") && y.row.includes("WIDTH")) {
                            coilData[a].children[1].children[0].value = true;
                            coilData[a].children[1].children[0].checked = true;
                            AppendLine(coilData[a].children[1].children[0]);
                        }
                        else if (text.includes("THICK") && y.row.includes("THICK")) {
                            coilData[a].children[1].children[0].value = true;
                            coilData[a].children[1].children[0].checked = true;
                            AppendLine(coilData[a].children[1].children[0]);
                        }
                        else if (text.includes("P ORDER") && y.row.includes("P ORDER")) {
                            coilData[a].children[1].children[0].value = true;
                            coilData[a].children[1].children[0].checked = true;
                            AppendLine(coilData[a].children[1].children[0]);
                        }
                        else if (text.includes("PO NUMBER") && y.row.includes("PO NUMBER")) {
                            coilData[a].children[1].children[0].value = true;
                            coilData[a].children[1].children[0].checked = true;
                            AppendLine(coilData[a].children[1].children[0]);
                        }
                        else if (text.includes("CUST NAME") && y.row.includes("CUST NAME")) {
                            coilData[a].children[1].children[0].value = true;
                            coilData[a].children[1].children[0].checked = true;
                            AppendLine(coilData[a].children[1].children[0]);
                        }
                        else if ((text.includes("GRADE") && y.row.includes("GRADE")) && (!text.includes("GRADE2") && !y.row.includes("GRADE2"))) {
                            coilData[a].children[1].children[0].value = true;
                            coilData[a].children[1].children[0].checked = true;
                            AppendLine(coilData[a].children[1].children[0]);
                        }
                        else if (text.includes("GRADE2") && y.row.includes("GRADE2")) {
                            coilData[a].children[1].children[0].value = true;
                            coilData[a].children[1].children[0].checked = true;
                            AppendLine(coilData[a].children[1].children[0]);
                        }
                        else if (text.includes("ACT WEIGHT") && y.row.includes("ACT WEIGHT")) {
                            coilData[a].children[1].children[0].value = true;
                            coilData[a].children[1].children[0].checked = true;
                            AppendLine(coilData[a].children[1].children[0]);
                        }
                    });
                }
            });
        }
    });


}


function GetPlateData(roleid, moduleid) {
    var rows;
    onlyrows = [];
    if (moduleid != 4) {
        var TemplateRows = document.getElementById("TemplateSelection").value;
        $.ajax({
            url: "/Plate/GetTemplateRows?TemplateId=" + TemplateRows,
            type: "GET",
            success: function (response) {
                rows = response;
                $.each(rows, function (a, b) {
                    onlyrows.push(b.row);
                });
                //if (moduleid != 4 && moduleid != 5) {
                //    if (onlyrows.length < 11) {
                //        $.each(rowLines, function (x, y) {
                //            if (!onlyrows.includes(y)) {
                //                xyz = { id: 0, row: y };;
                //                rows.push(xyz);
                //            }
                //        })
                //    }
                //}
                //else {
                //    if (onlyrows.length < 5) {
                //        $.each(clrowlines, function (x, y) {
                //            if (!onlyrows.includes(y)) {
                //                xyz = { id: 0, row: y };;
                //                rows.push(xyz);
                //            }
                //        })
                //    }
                //}
            }
        });
    }

    var cp = 0;
    var cm = 0;
    var z = 0;
    if (moduleid != 4) {
        z = document.getElementById("TemplateSelection").value;
    }
    var plateInput = { plateNo: "", roleId: 0, moduleId: 0, templateId: z };

    plateInput.plateNo = $("#txtPlateNo").val();
    plateInput.roleId = roleid;
    plateInput.moduleId = moduleid;
    if (plateInput.plateNo == "") {
        plateInput.plateNo = "PA31980B1";
        document.getElementById("txtPlateNo").value = "PA31980B1";
    }
    $.ajax({
        url: "/SAP/GetPlateData",
        method: "GET",
        data: plateInput,
        success: function (data) {
            var userName = document.getElementById("userId").value;
            var modid = document.getElementById("moduleId").value;
            var plateNo = document.getElementById("txtPlateNo").value;
            if (plateNo == "") {
                plateNo = "PA31980B1";
                document.getElementById("txtPlateNo").value = "PA31980B1";
            }
            var type = "normal";
            var log = "Data fetched from SAP " + (data.messege != "error" ? "successfully " + "for Plate No " + $("#txtPlateNo").val() + " on " + Date.now() + "and his role is " + $('#roleId').val() : data.messege);
            AddLog(userName, modid, log, type);
            if (data.messege != "error") {
                $('#platetablebody').empty();
                $('#plateno')[0].value = data.messege;
                var line = 1
                if (moduleid == 1 || moduleid == 2 || moduleid == 3 || moduleid == 5) {
                    if (rows.length != 0) {
                        //$.each(rows, function (id, rowdata) {
                        $.each(data.data, function (index, qwe) {
                            var val = true;
                            //let rowname = rowdata.row.substring(rowdata.row.indexOf("<")+1, rowdata.row.indexOf(">"));
                            //if (qwe.prefix_Text.includes(rowname)) {
                            //    val = true;
                            //    if (rowname == "JSW") {
                            //        if (qwe.prefix_Text == rowname) {
                            //            val = true;
                            //        }
                            //        else {
                            //            val = false;
                            //        }
                            //    }
                            //}
                            if (val == true) {
                                const tr = document.createElement("tr");
                                $.each(qwe, function (index, value) {
                                    if (index == "lineNo") {
                                        const td = document.createElement("td")
                                        td.textContent = line;
                                        td.bgColor = "#F5F5F5";
                                        td.align = "center";
                                        tr.appendChild(td);
                                        line++;
                                    }
                                    else if (index == "punching") {
                                        if (moduleid != 4) {
                                            const td = document.createElement("td");
                                            const txt = document.createElement("input");
                                            txt.type = "checkbox"
                                            txt.name = "chkpunching"
                                            txt.id = line.toString()
                                            //if (moduleid != 5) {
                                            //    if (line != 0 && cp < 6) {
                                            //        txt.checked = true;
                                            //        txt.value = true;
                                            //        cp++;
                                            //    }
                                            //    else {
                                            //        txt.checked = false;
                                            //        txt.value = false;
                                            //    }
                                            //}
                                            //else {
                                            txt.checked = value;
                                            txt.value = value;
                                            if (moduleid == 5) {
                                                txt.disabled = true;
                                            }
                                            //}
                                            td.append(txt);
                                            td.bgColor = "#F5F5F5";
                                            tr.appendChild(td);
                                        }
                                    }
                                    else if (index == "marking") {
                                        if ($('.market-btn')[0] != undefined || moduleid == 5) {
                                            const td = document.createElement("td");
                                            const txt = document.createElement("input");
                                            txt.type = "checkbox"
                                            txt.id = line.toString()
                                            txt.name = "chkmarking"
                                            //if (moduleid != 5) {
                                            //    if (line != 0 && cm < 6) {
                                            //        txt.checked = true;
                                            //        txt.value = true;
                                            //        cm++
                                            //    }
                                            //    else {
                                            //        txt.checked = false;
                                            //        txt.value = false;
                                            //    }
                                            //}
                                            //else {
                                            txt.checked = value;
                                            txt.value = value;
                                            if (moduleid == 5) {
                                                txt.disabled = true;
                                            }
                                            //}
                                            td.append(txt);
                                            td.bgColor = "#F5F5F5";
                                            tr.appendChild(td);
                                        }
                                    }
                                    else if (index == "prefix_Text") {
                                        const td = document.createElement("td")
                                        td.textContent = value.replace("GRADE2 -", "");
                                        td.bgColor = "#F5F5F5";
                                        td.align = "left";
                                        if (moduleid == 4) {
                                            td.contentEditable = false;
                                        }
                                        else {
                                            td.contentEditable = true;
                                        }
                                        tr.appendChild(td);
                                        tr.align = "center";
                                        tr.vAlign = "center";
                                        tr.id = "11_" + line
                                        document.getElementById("platetablebody").appendChild(tr);
                                    }
                                });
                            }
                        });
                        //});
                    }
                }
                else if (moduleid == 4) {
                    //if (rows.length != 0) {
                    //$.each(rows, function (id, rowdata) {
                    $.each(data.data, function (index, qwe) {
                        //let rowname = rowdata.row.replace('<data>', '');
                        //if (qwe.prefix_Text.includes(rowname)) {
                        const tr = document.createElement("tr");
                        $.each(qwe, function (index, value) {
                            if (index == "lineNo") {
                                const td = document.createElement("td")
                                td.textContent = line;
                                td.bgColor = "#F5F5F5";
                                td.align = "center";
                                tr.appendChild(td);
                                line++;
                            }
                            else if (index == "punching") {
                                if (moduleid != 4) {
                                    const td = document.createElement("td");
                                    const txt = document.createElement("input");
                                    txt.type = "checkbox";
                                    txt.name = "chkpunching";
                                    txt.id = line.toString();
                                    txt.value = value;
                                    txt.checked = value;
                                    td.append(txt);
                                    td.bgColor = "#F5F5F5";
                                    tr.appendChild(td);
                                }
                            }
                            else if (index == "marking") {
                                if ($('.market-btn')[0] != undefined) {
                                    const td = document.createElement("td");
                                    const txt = document.createElement("input");
                                    txt.type = "checkbox";
                                    txt.id = line.toString();
                                    txt.value = value;
                                    txt.name = "chkmarking";
                                    txt.checked = value;
                                    td.append(txt);
                                    td.bgColor = "#F5F5F5";
                                    tr.appendChild(td);
                                }
                            }
                            else if (index == "prefix_Text") {
                                const td = document.createElement("td");
                                td.textContent = value;
                                td.bgColor = "#F5F5F5";
                                td.align = "left";
                                if (moduleid == 4) {
                                    td.contentEditable = false;
                                }
                                else {
                                    td.contentEditable = true;
                                }
                                tr.appendChild(td);
                                tr.align = "center";
                                tr.vAlign = "center";
                                tr.id = "11_" + line;
                                document.getElementById("platetablebody").appendChild(tr);
                            }
                        });
                        //}
                    });
                    //});
                    //}












                    //$.each(data.data, function (index, qwe) {
                    //    const tr = document.createElement("tr");
                    //$.each(qwe, function (index, value) {
                    //if (index == "lineNo") {
                    //    const td = document.createElement("td")
                    //    td.textContent = line;
                    //    td.bgColor = "#F5F5F5";
                    //    td.align = "center";
                    //    tr.appendChild(td);
                    //    line++;
                    //}
                    //else if (index == "punching") {
                    //    if (moduleid != 4) {
                    //        const td = document.createElement("td");
                    //        const txt = document.createElement("input");
                    //        txt.type = "checkbox";
                    //        txt.name = "chkpunching";
                    //        txt.id = line.toString();
                    //        txt.value = value;
                    //        txt.checked = value;
                    //        td.append(txt);
                    //        td.bgColor = "#F5F5F5";
                    //        tr.appendChild(td);
                    //    }
                    //}
                    //else if (index == "marking") {
                    //    if ($('.market-btn')[0] != undefined) {
                    //        const td = document.createElement("td");
                    //        const txt = document.createElement("input");
                    //        txt.type = "checkbox";
                    //        txt.id = line.toString();
                    //        txt.value = value;
                    //        txt.name = "chkmarking";
                    //        txt.checked = value;
                    //        td.append(txt);
                    //        td.bgColor = "#F5F5F5";
                    //        tr.appendChild(td);
                    //    }
                    //}
                    //else if (index == "prefix_Text") {
                    //    const td = document.createElement("td");
                    //    td.textContent = value;
                    //    td.bgColor = "#F5F5F5";
                    //    td.align = "left";
                    //    if (moduleid == 4) {
                    //        td.contentEditable = false;
                    //    }
                    //    else {
                    //        td.contentEditable = true;
                    //    }
                    //    tr.appendChild(td);
                    //    tr.align = "center";
                    //    tr.vAlign = "center";
                    //    tr.id = "11_" + line;
                    //    document.getElementById("platetablebody").appendChild(tr);
                    //}
                    //});
                    //});
                }
                if (moduleid == 4) {
                    YSSubForm(data, plateInput.roleId, plateInput.moduleId);
                }
                document.getElementById('lblmessages').innerHTML += '\n<p style="margin-bottom:5px">' + GetCurrentDate() + " &lt;" + document.getElementById("UserNameId").innerText + "&gt; " + 'Successfully Fetched Data from SAP</p>';
                document.getElementsByClassName('footer-area')[0].scrollTop = document.getElementsByClassName('footer-area')[0].scrollHeight;
                //if (document.getElementById("txtPlateNo").value != "PA31980B1") {
                //    document.getElementById("punchingStatusDisplay").innerText = "Successfully fetched Data from SAP";
                //    document.getElementById("markingStatusDisplay").innerText = "Successfully fetched Data from SAP";
                //}
            }
            else {
                document.getElementById('lblmessages').innerHTML += '\n<p style="margin-bottom:5px">' + GetCurrentDate() + " &lt;" + document.getElementById("UserNameId").innerText + "&gt; " + 'Data Not found</p>';
                document.getElementsByClassName('footer-area')[0].scrollTop = document.getElementsByClassName('footer-area')[0].scrollHeight;
                //document.getElementById("punchingStatusDisplay").innerText = "Data Not found";
                //document.getElementById("markingStatusDisplay").innerText = "Data Not found";
                alert('Data Not found');
            }
        }
    });
}




function YSSubForm(data, roleId, moduleId) {
    if (data.data[0].prefix_Text == "STEEL GRADE -") {

        $.ajax(
            {
                url: "/SAP/GetYSValueFromOracleDb?plateNo=" + data.messege,
                method: "GET",
                success: function (res) {
                    if (res != null) {
                        var childs = document.getElementById("platetablebody").children;
                        $.each(childs, function (a, b) {
                            if (b.children[1].innerHTML.includes("STEEL GRADE -")) {
                                b.children[1].innerHTML += " " + res.ys;
                            }

                        });
                        document.getElementById('lblmessages').innerHTML += '\n<p style="margin-bottom:5px">' + GetCurrentDate() + " &lt;" + document.getElementById("UserNameId").innerText + "&gt; " + 'YS value is not present in the SAP but the data is present in "tablemassgrade" table</p>';
                        document.getElementsByClassName('footer-area')[0].scrollTop = document.getElementsByClassName('footer-area')[0].scrollHeight;
                        var leftSection = document.getElementById("left-section");
                        if (document.getElementById("newForm") != undefined) {
                            leftSection.removeChild(leftSection.lastChild);
                            document.getElementById("load-button").disabled = false;
                            var maintable = document.getElementById("MainTable");
                            maintable.style = "max-width:83.333333%;margin-right:0px";
                        }
                        alert("YS value is not present in the SAP but the data is present in 'tablemassgrade' table");
                    }
                    else {
                        document.getElementById("load-button").disabled = true;
                        var conf = confirm("Do you want to update YS value");
                        if (conf == true) {
                            var maintable = document.getElementById("MainTable");
                            maintable.style = "max-width:75%;margin-right:0px";

                            var leftSection = document.getElementById("left-section");

                            if (document.getElementById("newForm") == undefined) {
                                leftSection.innerHTML += '<div id="newForm" style="height: 40%; width: 140%; background-color: lightblue; position: absolute; bottom: 30%; margin: 5px; border-radius: 10px; border-style: solid; border-color: lightgray; border-width: 3px; "><h4 style="padding: 5%; font-weight: 600; text-align: center; ">Grade Specification</h4><div id="gradeDiv" style="display: flex; justify-content: space-between; margin: 5px 0px; "><p id="grade" style="width: 30%; background-color: lightblue; border: none; margin-left: 10px; height: 3%; font-weight: bold; ">Grade</p><input id="gradeText" type="text" spellcheck="false" data-ms-editor="true" style="width: 55%; margin-right: 10px; height: 2%; border-radius: 4px; border-width: 3px; border-style: double; "></div><div id="MinThicknessDiv" style="display: flex; justify-content: space-between; margin: 5px 0px; "><p id="MinThickness" style="width: 30%; background-color: lightblue; border: none; margin-left: 10px; height: 3%; font-weight: bold; ">Min Thickness</p><input id="MinThicknessText" type="text" spellcheck="false" data-ms-editor="true" style="width: 55%; margin-right: 10px; height: 2%; border-radius: 4px; border-width: 3px; border-style: double; "></div><div id="MaxThicknessDiv" style="display: flex; justify-content: space-between; margin: 5px 0px; "><p id="MaxThickness" style="width: 30%; background-color: lightblue; border: none; margin-left: 10px; height: 3%; font-weight: bold; ">Max Thickness</p><input id="MaxThicknessText" type="text" spellcheck="false" data-ms-editor="true" style="width: 55%; margin-right: 10px; height: 2%; border-radius: 4px; border-width: 3px; border-style: double; "></div><div id="YSValueDiv" style="display: flex; justify-content: space-between; margin: 5px 0px; "><p id="YSValue" style="width: 30%; background-color: lightblue; border: none; margin-left: 10px; height: 3%; font-weight: bold; ">YS Value</p><input id="YSValueText" type="text" spellcheck="false" data-ms-editor="true" style="width: 55%; margin-right: 10px; height: 2%; border-radius: 4px; border-width: 3px; border-style: double; "></div><button id="yssubmit" style="height: 10%; width: 40%; margin: auto; display: block; background-color: deepskyblue; border-style: solid; border-radius: 8px;" onclick="YSValueUpdatedemo()">Update</button></div>';
                            }
                            var platNo = document.getElementById("plateno").value;
                            $.ajax({
                                url: "/SAP/GetGrade?plateNo=" + platNo,
                                method: "GET",
                                success: function (grade) {
                                    gradeText.value = grade;
                                }
                            });
                            $('#yssubmit').click(function (e) {
                                YSValueUpdate(roleId, moduleId);
                            });
                        }
                    }
                }
            }
        );
    }
    else {
        var leftSection = document.getElementById("left-section");
        if (document.getElementById("newForm") != undefined) {
            leftSection.removeChild(leftSection.lastChild);
            document.getElementById("load-button").disabled = false;
            var maintable = document.getElementById("MainTable");
            maintable.style = "max-width:83.333333%;margin-right:0px";
        }
    }
}



function RefreshSequence() {
    var refresh = 500; // Refresh rate in milli seconds
    mytime = setTimeout('GetSequenceMode()', refresh)
}


var count = 0;
function GetSequenceMode() {
    $.ajax({
        url: "/Plate/GetMarkerSequence",
        type: "GET",
        success: function (data) {
            if (data.mode == "2") {
                document.getElementById("modeTag").innerText = "Manual Mode";
            }
            else if ((data.mode == "1")) {
                document.getElementById("modeTag").innerText = "Auto Mode";
            }
            else if (data.mode == "3") {
                document.getElementById("modeTag").innerText = "Fault Mode";
            }
            else if (data.mode == "100") {
                document.getElementById("modeTag").innerText = "Connection To PLC Failed";
            }
            if (data.mode == "1" && data.sequence == "11") {
                count = count + 1;
                if (count > 5) {
                    count = 0;
                    $.ajax({
                        url: "/Plate/SetMarkingBit",
                        type: "GET"
                    });
                }
            }
            if (data.sequence == "10") {
                document.getElementById("punchingStatusDisplay").innerText = "Machine ready";
                document.getElementById("sequenceTag").innerText = data.sequence.length == 1 ? "16X0" + data.sequence : "16X" + data.sequence;
            }
            else if (data.sequence == "1") {
                document.getElementById("punchingStatusDisplay").innerText = "Machine not ready";
                document.getElementById("sequenceTag").innerText = data.sequence.length == 1 ? "16X0" + data.sequence : "16X" + data.sequence;
            }
            else if (data.sequence == "100") {
                document.getElementById("punchingStatusDisplay").innerText = "Connection To PLC Failed";
                document.getElementById("sequenceTag").innerText = data.sequence.length == 1 ? "16X0" + data.sequence : "16X" + data.sequence;
            }
            else if (data.sequence == "11") {
                document.getElementById("punchingStatusDisplay").innerText = "Data Updated in PLC";
                document.getElementById("sequenceTag").innerText = data.sequence.length == 1 ? "16X0" + data.sequence : "16X" + data.sequence;
            }
            else if (data.sequence == "12") {
                document.getElementById("punchingStatusDisplay").innerText = "Start Punching";
                document.getElementById("sequenceTag").innerText = data.sequence.length == 1 ? "16X0" + data.sequence : "16X" + data.sequence;
            }
            else if (data.sequence == "13") {
                document.getElementById("punchingStatusDisplay").innerText = "Punching started";
                document.getElementById("sequenceTag").innerText = data.sequence.length == 1 ? "16X0" + data.sequence : "16X" + data.sequence;
            }
            else if (data.sequence == "14") {
                document.getElementById("punchingStatusDisplay").innerText = "Punching started";
                document.getElementById("sequenceTag").innerText = data.sequence.length == 1 ? "16X0" + data.sequence : "16X" + data.sequence;
            }
            else if (data.sequence == "15") {
                document.getElementById("punchingStatusDisplay").innerText = "Punching started";
                document.getElementById("sequenceTag").innerText = data.sequence.length == 1 ? "16X0" + data.sequence : "16X" + data.sequence;
            }
            else if (data.sequence == "16") {
                document.getElementById("punchingStatusDisplay").innerText = "Punching In Process";
                document.getElementById("sequenceTag").innerText = data.sequence.length == 1 ? "16X0" + data.sequence : "16X" + data.sequence;
            }
            else if (data.sequence == "17") {
                document.getElementById("punchingStatusDisplay").innerText = "Punching Completed";
                document.getElementById("sequenceTag").innerText = data.sequence.length == 1 ? "16X0" + data.sequence : "16X" + data.sequence;
                var userName = document.getElementById("userId").value;
                var modid = document.getElementById("moduleId").value;
                var type = "normal";
                var log = "Punching Completed";
                AddLog(userName, modid, log, type);
                document.getElementById('lblmessages').innerHTML += '\n<p style="margin-bottom:5px">' + GetCurrentDate() + " &lt;" + document.getElementById("UserNameId").innerText + "&gt; " + 'Punching Completed</p>';
                document.getElementsByClassName('footer-area')[0].scrollTop = document.getElementsByClassName('footer-area')[0].scrollHeight;
            }
            else if (data.sequence == "18") {
                document.getElementById("punchingStatusDisplay").innerText = "Slider Up";
                document.getElementById("sequenceTag").innerText = data.sequence.length == 1 ? "16X0" + data.sequence : "16X" + data.sequence;
            }
            else {
                document.getElementById("punchingStatusDisplay").innerText = "Error in Sequence";
                document.getElementById("sequenceTag").innerText = data.sequence.length == 1 ? "16X0" + data.sequence : "16X" + data.sequence;
            }






            if (data.mode == "1" && (data.sequence == "10" || data.sequence == "11")) {
                document.getElementById('sendToPunchingbtn').disabled = false;
            }
            else {
                document.getElementById('sendToPunchingbtn').disabled = true;
            }
            RefreshSequence();
        }
    });
}



function GetCurrentDate() {
    var x = new Date();
    var month = x.getMonth() + 1;
    var x1 = x.getDate() + "/" + (month.length == 1 ? "0" + month : month) + "/" + x.getFullYear();
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


var weightingdataUpdate = false;
var fetchDataFromSAP = false;
var matidvalid = 0;

function GetWeightDataDC() {
    if (document.getElementById("DownCoilerAutoModeSwitch").checked == false) {

        $.ajax({
            url: '/Plate/GetWeighingData',
            method: "GET",
            success: function (data) {
                data = data.replace(" ", "");
                if (data == "") {

                    GetWeightDataDC();
                }
                else if (/^[0-9]+$/.test(data)) {
                    if (document.getElementById("DownCoilerAutoModeSwitch").checked == false) {
                        document.getElementById("WeigthBox").innerHTML = data;
                    }
                    GetWeightDataDC();
                }
                else {
                    GetWeightDataDC();
                }
            },
            error: function () {
                GetWeightDataDC();
            }
        });
    }
    else if (document.getElementById("DownCoilerAutoModeSwitch").checked == true) {
        $.ajax({
            url: "/Plate/DCAutoModeData",
            type: "GET",
            success: function (data) {
                if (data.matid != matidvalid) {
                    weightingdataUpdate = false;
                    fetchDataFromSAP = false;
                }
                if (document.getElementById("DownCoilerAutoModeSwitch").checked == true) {
                    document.getElementById("DCMatIdBox").innerText = data.matId == 0 ? "--" : data.matId;
                    if (data.liveWeight > 1000) {
                        document.getElementById("DCLiveWeight").innerText = data.liveWeight;
                    }
                    document.getElementById("DCPosition").innerText = data.position == 0 ? "--" : data.position;
                    document.getElementById("DCMachineMode").innerText = data.machineMode;
                    document.getElementById("DCMarkerPosition").innerText = data.markerHomePosition;
                    document.getElementById("DCMarkerFault").innerText = data.markerFault;
                    document.getElementById("DCMarkerReady").innerText = data.markerReady;
                    document.getElementById("DCMarkerActive").innerText = data.markerActive;
                    document.getElementById("DCCycleStatus").innerText = data.markingCycleStatus;
                    document.getElementById("DCAbourtStatus").innerText = data.markingAbortStatus;
                }
                if (data.position == 16 && weightingdataUpdate == false && matidvalid != data.matId && data.liveWeight > 1000) {
                    matidvalid = data.matId;
                    document.getElementById("DCWeighingStatusBox").innerText = "weighting data update started";
                    var weight = document.getElementById("DCLiveWeight").innerText;
                    $.ajax({
                        url: "/Plate/UpdateWeightdata?weight=" + weight,
                        type: "GET",
                        success: function (data) {
                            if (data.statusCode == 200) {
                                //document.getElementById("MatIdDC").value = data.matId;
                                document.getElementById("lblmessages").innerHTML += '\n<p style="margin-bottom:5px">' + GetCurrentDate() + " &lt;" + document.getElementById("UserNameId").innerText + "&gt; " + data.messege + '</p>';
                                document.getElementsByClassName('footer-area')[0].scrollTop = document.getElementsByClassName('footer-area')[0].scrollHeight;
                                document.getElementById("txtPlateNo").value = data.data;
                                weightingdataUpdate = true;
                                document.getElementById("DCWeighingStatusBox").innerText = "weighting data updated"
                                GetWeightDataDC();
                            }

                        }
                    })
                }
                else if (data.position == 17 && matidvalid != data.matId && fetchDataFromSAP == false) {
                    matidvalid = data.matId;
                    if (fetchDataFromSAP == false) {
                        GetPlateDataDC();
                        fetchDataFromSAP = true;
                    }
                    GetWeightDataDC();
                    weightingdataUpdate = false;
                }
                else {
                    GetWeightDataDC();
                }

            },
            error: function () {
                GetWeightDataDC();
            }
        });
    }
}


function UpdateWeightInOracleAndSAP() {
    $.ajax({
        url: "/Plate/UpdateWeightInOracleAndSAP?Weight=" + document.getElementById("WeigthBox").innerHTML,
        type: "GET",
        success: function (data) {
            if (data.statusCode == 200) {
                document.getElementById("MatIdDC").value = data.data.matId;
                $('#plateno')[0].value = data.data.coilId;
                var newboxes = $('#weightDataUpdateTemplate').html();
                var sidebar = document.getElementById("left-section");
                sidebar.innerHTML += newboxes;
                var response = confirm("Do you want to continue with marking");
                if (response == true) {
                    GetSapData(data.data.matid);
                }
                document.getElementById('lblmessages').innerHTML += '\n<p style="margin-bottom:5px">' + GetCurrentDate() + " &lt;" + document.getElementById("UserNameId").innerText + "&gt; " + data.messege + '</p>';
                document.getElementsByClassName('footer-area')[0].scrollTop = document.getElementsByClassName('footer-area')[0].scrollHeight;
            }
            else {
                document.getElementById('lblmessages').innerHTML += '\n<p style="margin-bottom:5px">' + GetCurrentDate() + " &lt;" + document.getElementById("UserNameId").innerText + "&gt; " + data.messege + '</p>';
                document.getElementsByClassName('footer-area')[0].scrollTop = document.getElementsByClassName('footer-area')[0].scrollHeight;
            }
        }
    });
}



function AddLog(userid, moduleid, log, type) {
    $.ajax({
        url: "/Plate/SaveLogs?userId=" + userid + "&moduleId=" + moduleid + "&log=" + log + "&type=" + type,
        method: "GET"
    });
}



function ManualWeightUpdateTemp() {
    var manuupt = $('#manualWeightUpdateTemp').html();
    document.getElementById("manualModetemp").innerHTML += manuupt;
}


function CancelWeightUpdate() {
    document.getElementById("manualUpdateTempnew").remove();
}


function UpdateWeightDataManual() {
    var weight = document.getElementById("WeightText").value;
    var coilid = document.getElementById("CoilIdText").value;
    var inputmodel = { coilId: coilid, weight :weight}
    $.ajax({
        url: "/Plate/ManualWeightUpdate",
        type: "POST",
        data: inputmodel,
        success: function (data) {
            if (data.statusCode == 200) {
                //document.getElementById("MatIdDC").value = data.matId;
                document.getElementById("lblmessages").innerHTML += '\n<p style="margin-bottom:5px">' + GetCurrentDate() + " &lt;" + document.getElementById("UserNameId").innerText + "&gt; " + data.messege + '</p>';
                document.getElementsByClassName('footer-area')[0].scrollTop = document.getElementsByClassName('footer-area')[0].scrollHeight;
                document.getElementById("txtPlateNo").value = data.data;
                weightingdataUpdate = true;
                document.getElementById("DCWeighingStatusBox").innerText = "weighting data updated"
            }
        }
    });
}

