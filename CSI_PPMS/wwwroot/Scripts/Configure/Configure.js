var discCount = 0;
var shellCount = 0;



function editSAPLink() {
    if (document.getElementById('updateButton').type == "button") {
        $("#addSAPLink").attr('readonly', 'readonly');
        document.getElementById('updateButton').type = "hidden";
    }
    else {
        document.getElementById('addSAPLink').removeAttribute('readonly');
        document.getElementById('updateButton').type = "button";
    }
}

function editWeightLink() {
    if (document.getElementById('updateWeightButton').type == "button") {
        $("#addWeightSapLink").attr('readonly', 'readonly');
        document.getElementById('updateWeightButton').type = "hidden";
    }
    else {
        document.getElementById('addWeightSapLink').removeAttribute('readonly');
        document.getElementById('updateWeightButton').type = "button";
    }
}

function UpdateSAPLink() {
    var data = prompt("please confirm your password");
    $.ajax({
        url: "/Accounts/ValidateUser?userId=" + document.getElementById("userIdid").value + "&password=" + data,
        type: "POST",
        success: function (data) {
            if (data == true) {
                sapInput = { SapLinkId: 0, SAPLink: "", SAPUserName: "", SAPPassword: "" };
                sapInput.SapLinkId = document.getElementById('SAPLinkId').value;
                sapInput.SAPLink = document.getElementById('SAPLink').value;
                sapInput.SAPUserName = document.getElementById('SAPUserName').value;
                sapInput.SAPPassword = document.getElementById('SAPPassword').value;
                $.ajax({
                    url: "/Configure/UpdateSapLink",
                    type: 'post',
                    data: sapInput,
                    success: function (data) {
                        alert('Updated Successfully');
                        document.getElementById('updateButton').type = "hidden";

                    }
                })
            }
            else {
                alert("invalid User Password");
            }
        }
    })
}

function CancelUpdateSAPLink() {
    document.getElementById("MainConfigureBody").innerHTML = "";
}


function UpdatePLCDetails(moduleid, type) {
    var data = prompt("please confirm your password");
    $.ajax({
        url: "/Accounts/ValidateUser?userId=" + document.getElementById("userIdid").value + "&password=" + data,
        type: "POST",
        success: function (data) {
            if (data == true) {
                var data = { moduleId: 0, tid: 0, tip: "", tport: 0, pip: "", pport: 0, tslot: 0, track: 0 };
                data.moduleId = moduleid;
                if (moduleid == 1 || moduleid == 3) {
                    if (type == 1) {
                        data.tip = document.getElementById("TechniforIp").value;
                        data.tport = document.getElementById("TechniforPort").value;
                        data.tid = document.getElementById("IPID").value;
                    }
                    else if (type == 2) {
                        data.pip = document.getElementById("PLCIp").value;
                        data.pport = document.getElementById("PLCPort").value;
                        data.tid = document.getElementById("IPID").value;
                    }
                }
                else if (moduleid == 2) {
                    data.tip = document.getElementById("moxaIp").value;
                    data.tslot = document.getElementById("moxaSlot").value;
                    data.track = document.getElementById("Moxarack").value;
                    data.tid = document.getElementById("IPID").value;
                }
                else if (moduleid == 5) {
                    data.tip = document.getElementById("WeightIp").value;
                    data.tport = document.getElementById("WeightPort").value;
                    data.tid = document.getElementById("IPID").value;
                }
                $.ajax({
                    url: "/Configure/UpdatePLCDetails",
                    method: "POST",
                    data: data
                });
            }
            else {
                alert("invalid password")
            }
        }
    });
}


function UpdateSapCredentials(moduleid) {
    var type = 1;
    sapInput = { sapUserName: "", sapPassword: "", sapLinkId: 0 };
    if (moduleid == 5) {
        type = document.getElementById("typeid").value;
    }
    if (type == 1) {
        sapInput.sapLinkId = document.getElementById('addSAPLinkid').value;
    }
    else {
        sapInput.sapLinkId = document.getElementById('addSAPWeightLinkid').value;
    }
    sapInput.sapUserName = document.getElementById("addSAPUser").value;
    sapInput.sapPassword = document.getElementById("addSAPPassword").value;
    $.ajax({
        url: "/Configure/UpdateSapCredentials",
        type: "Post",
        data: sapInput,
        success: function (data) {
            alert('Updated Successfully');
            document.getElementById("UpdateSapCredentialsButton").type = "hidden";
            document.getElementById("addSAPPassword").type = "password";
        }
    })
}





function addUser() {
    if (document.getElementById("addUser").type == "text") {
        document.getElementById("addUser").type = "hidden";
        document.getElementById("addPassword").type = "hidden";
        document.getElementById("createUserButton").type = "hidden";
    }
    else {
        document.getElementById("addUser").type = "text";
        document.getElementById("addPassword").type = "password";
        document.getElementById("createUserButton").type = "button";
    }
}

function editSAPCredentials() {
    if (document.getElementById("UpdateSapCredentialsButton").type == "button") {
        $("#addSAPUser").attr('readonly', 'readonly');
        document.getElementById("addSAPPassword").type = "password";
        $("#addSAPPassword").attr('readonly', 'readonly');
        document.getElementById("UpdateSapCredentialsButton").type = "hidden";
        document.getElementById("typeid").type = "hidden";
    }
    else {
        document.getElementById("addSAPUser").removeAttribute("readonly")
        document.getElementById("addSAPPassword").type = "text";
        document.getElementById("addSAPPassword").removeAttribute("readonly")
        document.getElementById("UpdateSapCredentialsButton").type = "button";
        document.getElementById("typeid").type = "text";
    }
}

function editIP(moduleId) {
    if (moduleId == 1 || moduleId == 3) {
        if (document.getElementById("UpdateIPButton").type == "button") {
            document.getElementById("Techniforip").type = "hidden";
            document.getElementById("Techniforport").type = "hidden";
            document.getElementById("PLCip").type = "hidden";
            document.getElementById("PLCPort").type = "hidden";
            document.getElementById("UpdateIPButton").type = "hidden";
        }
        else {
            document.getElementById("Techniforip").type = "text";
            document.getElementById("Techniforport").type = "text";
            document.getElementById("PLCip").type = "text";
            document.getElementById("PLCPort").type = "text";
            document.getElementById("UpdateIPButton").type = "button";
        }
    }
    else if (moduleId == 2) {
        if (document.getElementById("UpdateMoxaIPButton").type == "button") {
            document.getElementById("moxaip").type = "hidden";
            document.getElementById("moxaSlot").type = "hidden";
            document.getElementById("moxaRack").type = "hidden";
            document.getElementById("UpdateMoxaIPButton").type = "hidden";
        }
        else {
            document.getElementById("moxaip").type = "text";
            document.getElementById("moxaSlot").type = "text";
            document.getElementById("moxaRack").type = "text";
            document.getElementById("UpdateMoxaIPButton").type = "button";
        }
    }
    else if (moduleId == 5) {
        if (document.getElementById("UpdateweightingIPButton").type == "button") {
            document.getElementById("weightingmachineip").type = "hidden";
            document.getElementById("weightingmachineport").type = "hidden";
            document.getElementById("UpdateweightingIPButton").type = "hidden";
        }
        else {
            document.getElementById("weightingmachineip").type = "text";
            document.getElementById("weightingmachineport").type = "text";
            document.getElementById("UpdateweightingIPButton").type = "button";
        }
    }
}

function createUser() {
    if (document.getElementById("addPassword").value == document.getElementById("caddPassword").value) {
        var data = prompt("please confirm your password");
        var abc = document.getElementById("userIdid").value;
        $.ajax({
            url: "/Accounts/ValidateUser?userId=" + document.getElementById("userIdid").value + "&password=" + data,
            type: "POST",
            success: function (data) {
                if (data == true) {
                    var createuser = { UserName: "", Password: "", moduleId: 0 };
                    createuser.UserName = document.getElementById("addUser").value;
                    createuser.Password = document.getElementById("addPassword").value;
                    createuser.moduleId = document.getElementById("moduleId").value;
                    $.ajax({
                        url: '/Accounts/CreateUser',
                        type: "POST",
                        data: createuser,
                        success: function (data) {
                            alert(data.messege);
                            document.getElementById("addUser").type = "hidden";
                            document.getElementById("addPassword").type = "hidden";
                            document.getElementById("createUserButton").type = "hidden";
                        }
                    })
                }
                else {
                    alert("invalid User Password");
                }
            }
        });
    }
    else {
        alert("passwords didn't match");
    }
}


function AddNewTemplateDB(tempid = 0) {
    var data = prompt("please confirm your password");
    $.ajax({
        url: "/Accounts/ValidateUser?userId=" + document.getElementById("userIdid").value + "&password=" + data,
        type: "POST",
        success: function (data) {
            if (data == true) {
                var userName = document.getElementById("userId").value;
                var modid = document.getElementById("moduleId").value;
                var type = "normal";
                var log = "template added or updated by " + userName + "on" + Date.now();
                AddLog(userName, modid, log, type);
                var rowLines = [
                    "WEIGHT",
                    "HEAT NO",
                    "GRADE",
                    "GRADE2",
                    "SIZE",
                    "CUSTOMER REF",
                    "PO ORDER",
                    "PO NUMBER",
                    "PLATE NO",
                    "CUSTOMER NAME",
                    "STEEL GRADE",
                    "LENGTH",
                    "THICKNESS",
                    "WIDTH",
                    "WEIGHT",
                    "ACT WEIGHT",
                    "COIL NO"
                ];
                var apiData = { templateName: "", Lines: [], moduleId: 0, isDefault: false, templateId: tempid };
                var valid = true;
                var counter = 1;
                if (tempid == 0) {
                    apiData.templateName = document.getElementById("templateNameTag").value;
                    apiData.isDefault = document.getElementById("DefaultTemplate").checked;
                }
                apiData.moduleId = document.getElementById("ModuleIdCon").value;
                var templateBody = document.getElementById("TemplateBody").children;
                $.each(templateBody, function (index, value) {
                    var qwer = value.children[1].innerText
                    var qwert = qwer.substring((qwer.indexOf("<") + 1), (qwer.indexOf(">")));
                    if (value.children[1].innerText != "" && (rowLines.includes(qwert) || (!qwer.includes("<") || !qwer.includes(">")))) {
                        switch (counter) {
                            case 1:
                                apiData.Lines.push(value.children[1].innerText.replace("&lt;", "<").replace("&gt;", ">"));
                                counter++;
                                break;
                            case 2:
                                apiData.Lines.push(value.children[1].innerText.replace("&lt;", "<").replace("&gt;", ">"));
                                counter++;
                                break;
                            case 3:
                                apiData.Lines.push(value.children[1].innerText.replace("&lt;", "<").replace("&gt;", ">"));
                                counter++;
                                break;
                            case 4:
                                apiData.Lines.push(value.children[1].innerText.replace("&lt;", "<").replace("&gt;", ">"));
                                counter++;
                                break;
                            case 5:
                                apiData.Lines.push(value.children[1].innerText.replace("&lt;", "<").replace("&gt;", ">"));
                                counter++;
                                break;
                            case 6:
                                apiData.Lines.push(value.children[1].innerText.replace("&lt;", "<").replace("&gt;", ">"));
                                counter++;
                                break;
                            case 7:
                                apiData.Lines.push(value.children[1].innerText.replace("&lt;", "<").replace("&gt;", ">"));
                                counter++;
                                break;
                            case 8:
                                apiData.Lines.push(value.children[1].innerText.replace("&lt;", "<").replace("&gt;", ">"));
                                counter++;
                                break;
                            case 9:
                                apiData.Lines.push(value.children[1].innerText.replace("&lt;", "<").replace("&gt;", ">"));
                                counter++;
                                break;
                            case 10:
                                apiData.Lines.push(value.children[1].innerText.replace("&lt;", "<").replace("&gt;", ">"));
                                counter++;
                                break;
                            case 11:
                                apiData.Lines.push(value.children[1].innerText.replace("&lt;", "<").replace("&gt;", ">"));
                                counter++;
                                break;
                        }
                    }
                    else if (value.children[1].innerText == "" && !rowLines.includes(value.children[1].innerText)) {
                        if (valid != false) {
                            valid = true;
                        }
                    }
                    else if (!rowLines.includes(value.children[1].innerText)) {
                        valid = false;
                    }
                });
                if (valid == false) {
                    alert("please enter a valid tag");
                }
                else if (apiData.Lines.length > 6 && apiData.moduleId == 5) {
                    alert("please select only 6 lines");
                }
                else {

                    if ((apiData.templateName != null && apiData.templateName != "") || tempid != 0) {

                        $.ajax({
                            url: "/Plate/AddTemplate",
                            type: "POST",
                            data: apiData,
                            success: function (data) {
                                $.each(templateBody, function (index, value) {
                                    value.children[1].innerHTML = "";
                                })
                                document.getElementById("templateNameTag").value = "";
                            }
                        })
                    }
                    else {
                        alert("please enter template name");
                    }
                }
            }
            else {
                alert("invalid password");
            }
        }
    });

}


function AddDCNewTemplateDB(tempid = 0) {
    shellCount = 0;
    discCount = 0;
    var data = prompt("please confirm your password");
    $.ajax({
        url: "/Accounts/ValidateUser?userId=" + document.getElementById("userIdid").value + "&password=" + data,
        type: "POST",
        success: function (data) {
            if (data == true) {
                var userName = document.getElementById("userId").value;
                var modid = document.getElementById("moduleId").value;
                var type = "normal";
                var log = "template added or updated by " + userName + "on" + Date.now();
                AddLog(userName, modid, log, type);
                var rowLines = [
                    "WEIGHT",
                    "HEAT NO",
                    "GRADE",
                    "GRADE2",
                    "SIZE",
                    "CUSTOMER REF",
                    "PO ORDER",
                    "PO NUMBER",
                    "PLATE NO",
                    "CUSTOMER NAME",
                    "STEEL GRADE",
                    "LENGTH",
                    "THICKNESS",
                    "WIDTH",
                    "WEIGHT",
                    "ACT WEIGHT",
                    "COIL NO"
                ];
                var apiData = { templateName: "", Lines: [], moduleId: 0, isDefault: false, templateId: tempid };
                var valid = true;
                var counter = 1;
                if (tempid == 0) {
                    apiData.templateName = document.getElementById("templateNameTag").value;
                    apiData.isDefault = document.getElementById("DefaultTemplate").checked;
                }
                apiData.moduleId = document.getElementById("ModuleIdCon").value;
                var templateBody = document.getElementById("TemplateBody").children;
                $.each(templateBody, function (index, value) {
                    var qwer = value.children[3].innerText
                    var qwert = qwer.substring((qwer.indexOf("<") + 1), (qwer.indexOf(">")));
                    if (value.children[3].innerText != "" && (rowLines.includes(qwert) || (!qwer.includes("<") || !qwer.includes(">")))) {
                        switch (counter) {
                            case 1:
                                apiData.Lines.push({ row: value.children[3].innerText.replace("&lt;", "<").replace("&gt;", ">"), isShell: value.children[1].children[0].checked, isDisc: value.children[2].children[0].checked });
                                counter++;
                                break;
                            case 2:
                                apiData.Lines.push({ row: value.children[3].innerText.replace("&lt;", "<").replace("&gt;", ">"), isShell: value.children[1].children[0].checked, isDisc: value.children[2].children[0].checked });
                                counter++;
                                break;
                            case 3:
                                apiData.Lines.push({ row: value.children[3].innerText.replace("&lt;", "<").replace("&gt;", ">"), isShell: value.children[1].children[0].checked, isDisc: value.children[2].children[0].checked });
                                counter++;
                                break;
                            case 4:
                                apiData.Lines.push({ row: value.children[3].innerText.replace("&lt;", "<").replace("&gt;", ">"), isShell: value.children[1].children[0].checked, isDisc: value.children[2].children[0].checked });
                                counter++;
                                break;
                            case 5:
                                apiData.Lines.push({ row: value.children[3].innerText.replace("&lt;", "<").replace("&gt;", ">"), isShell: value.children[1].children[0].checked, isDisc: value.children[2].children[0].checked });
                                counter++;
                                break;
                            case 6:
                                apiData.Lines.push({ row: value.children[3].innerText.replace("&lt;", "<").replace("&gt;", ">"), isShell: value.children[1].children[0].checked, isDisc: value.children[2].children[0].checked });
                                counter++;
                                break;
                            case 7:
                                apiData.Lines.push({ row: value.children[3].innerText.replace("&lt;", "<").replace("&gt;", ">"), isShell: value.children[1].children[0].checked, isDisc: value.children[2].children[0].checked });
                                counter++;
                                break;
                            case 8:
                                apiData.Lines.push({ row: value.children[3].innerText.replace("&lt;", "<").replace("&gt;", ">"), isShell: value.children[1].children[0].checked, isDisc: value.children[2].children[0].checked });
                                counter++;
                                break;
                            case 9:
                                apiData.Lines.push({ row: value.children[3].innerText.replace("&lt;", "<").replace("&gt;", ">"), isShell: value.children[1].children[0].checked, isDisc: value.children[2].children[0].checked });
                                counter++;
                                break;
                            case 10:
                                apiData.Lines.push({ row: value.children[3].innerText.replace("&lt;", "<").replace("&gt;", ">"), isShell: value.children[1].children[0].checked, isDisc: value.children[2].children[0].checked });
                                counter++;
                                break;
                            case 11:
                                apiData.Lines.push({ row: value.children[3].innerText.replace("&lt;", "<").replace("&gt;", ">"), isShell: value.children[1].children[0].checked, isDisc: value.children[2].children[0].checked });
                                counter++;
                                break;
                        }
                    }
                    else if (value.children[3].innerText == "" && !rowLines.includes(value.children[1].innerText)) {
                        if (valid != false) {
                            valid = true;
                        }
                    }
                    else if (!rowLines.includes(value.children[1].innerText)) {
                        valid = false;
                    }
                });
                if (valid == false) {
                    alert("please enter a valid tag");
                }
                else if (apiData.Lines.length > 6 && apiData.moduleId == 5) {
                    alert("please select only 6 lines");
                }
                else {

                    if ((apiData.templateName != null && apiData.templateName != "") || tempid != 0) {

                        $.ajax({
                            url: "/Plate/AddDCTemplate",
                            type: "POST",
                            data: apiData,
                            success: function (data) {
                                $.each(templateBody, function (index, value) {
                                    value.children[1].innerHTML = "";
                                })
                                document.getElementById("templateNameTag").value = "";
                            }
                        })
                    }
                    else {
                        alert("please enter template name");
                    }
                }
            }
            else {
                alert("invalid password");
            }
        }
    });

}



$(document).ready(function () {
    $('#templatetable').on('click', 'input[type="checkbox"]', function (e) {
        AppendTemplateLine(this);

    });
});



function AppendTemplateLine(currentData) {
    var CTBody = document.getElementById("templatemaintable");
    var count = 0;
    var tbody = CTBody.getElementsByTagName("tbody")[0];
    if (tbody.childNodes != undefined) {
        count = tbody.children.length;
    }
    const tr = document.createElement("tr");
    tr.id = currentData.parentElement.parentElement.id + "_child";
    const td = document.createElement("td");
    td.innerHTML = count + 1;
    td.bgColor = "#F5F5F5";
    td.align = "center";
    tr.appendChild(td);

    const PreFix = document.createElement("td");
    PreFix.innerHTML = currentData.parentElement.parentElement.children[1].innerHTML;
    PreFix.bgColor = "#F5F5F5";
    PreFix.contentEditable = true;
    tr.appendChild(PreFix);

    tbody.appendChild(tr);

}


function AddNewTemplate() {
    var divtag = document.getElementById("UpdateTemplate");
    if (divtag.style.visibility == "hidden") {
        divtag.style.visibility = "visible";
        var tempname = $('#templatename').html();
        divtag.innerHTML += tempname;
        var temptables = $('#temtables').html();
        divtag.innerHTML += temptables;
        var tempButt = $('#addTemplateButton').html();
        divtag.innerHTML += tempButt;

        $('#templatetable').on('click', 'input[type="checkbox"]', function (e) {
            if (this.checked == true) {
                AppendTemplateLine(this);
            }
            else {
                RemoveTemplateLine(this);
            }

        });
    }
    else {
        divtag.style.visibility = "hidden";
        divtag.innerHTML = "";
    }
}







function RemoveTemplateLine(currentData) {
    counter = 1;
    var mainbody = document.getElementById("templatemaintable");
    var body = mainbody.getElementsByTagName("tbody");
    var x = [];
    $.each(body[0].children, function (a, b) {
        if (b.children[1].innerText == currentData.parentElement.parentElement.children[1].innerText) {
            x.push(b);
        }
    });

    $.each(x, function (z, s) {
        s.remove();
    });

    $.each(body[0].children, function (a, b) {
        if (body[0].children.length != 0) {
            b.children[0].innerText = counter;
            counter = counter + 1;
        }
    });

}


function UpdateTemplatedata() {
    var z = document.getElementById("TemplateSelection").value;
    AddNewTemplateDB(z);
}

function UpdateDCTemplatedata() {
    shellCount = 0;
    discCount = 0;
    var z = document.getElementById("TemplateSelection").value;
    AddDCNewTemplateDB(z);
}



function deletetemplateDB() {
    var data = prompt("please confirm your password");
    $.ajax({
        url: "/Accounts/ValidateUser?userId=" + document.getElementById("userIdid").value + "&password=" + data,
        type: "POST",
        success: function (data) {
            if (data == true) {
                var userName = document.getElementById("userId").value;
                var modid = document.getElementById("moduleId").value;
                var type = "normal";
                var log = "template deleted by " + userName + " on " + Date.now();
                AddLog(userName, modid, log, type);
                var z = document.getElementById("TemplateSelection").value;
                $.ajax({
                    url: "/Plate/DeleteTemplate?templateId=" + z,
                    method: "GET"
                });
            }
            else {
                alert("invalid password");
            }
        }
    });
}



function CreateUserTemplateLoad() {
    var createUSerHTML = $('#CreateUserTemplate').html();
    document.getElementById("MainConfigureBody").innerHTML = createUSerHTML;

}


function cancelCreateUser() {
    document.getElementById("MainConfigureBody").innerHTML = "";
}


function DeleteUserTemplateLoad() {
    var createUSerHTML = $('#DeleteUserTemplate').html();
    document.getElementById("MainConfigureBody").innerHTML = createUSerHTML;

    $.ajax({
        url: "/Accounts/UserListByModuleId?moduleId=" + document.getElementById("moduleId").value,
        method: "GET",
        success: function (data) {
            var deleteBody = document.getElementById("DeleteUserBody");
            $.each(data, function (x, y) {
                var tr = document.createElement("tr");
                var td1 = document.createElement("td");
                td1.innerText = x + 1;
                var td4 = document.createElement("td");
                td4.innerText = y.userId;
                var td5 = document.createElement("td");
                td5.innerText = y.createdDate;
                var td2 = document.createElement("td");
                td2.innerText = y.userName;
                var td3 = document.createElement("td");
                td3.innerHTML = '<span id="deluser" style="cursor:pointer;"><svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-trash" viewBox="0 0 16 16"><path d="M5.5 5.5A.5.5 0 0 1 6 6v6a.5.5 0 0 1-1 0V6a.5.5 0 0 1 .5-.5zm2.5 0a.5.5 0 0 1 .5.5v6a.5.5 0 0 1-1 0V6a.5.5 0 0 1 .5-.5zm3 .5a.5.5 0 0 0-1 0v6a.5.5 0 0 0 1 0V6z"/><path fill-rule="evenodd" d="M14.5 3a1 1 0 0 1-1 1H13v9a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V4h-.5a1 1 0 0 1-1-1V2a1 1 0 0 1 1-1H6a1 1 0 0 1 1-1h2a1 1 0 0 1 1 1h3.5a1 1 0 0 1 1 1v1zM4.118 4 4 4.059V13a1 1 0 0 0 1 1h6a1 1 0 0 0 1-1V4.059L11.882 4H4.118zM2.5 3V2h11v1h-11z"/></svg ></span>'
                tr.appendChild(td1);
                tr.appendChild(td4);
                tr.appendChild(td5);
                tr.appendChild(td2);
                tr.appendChild(td3);
                deleteBody.appendChild(tr);
            })
            $('#DeleteUserBody').on('click', 'span[id="deluser"]', function (e) {
                var x = this;
                var userId = $(this).parent().parent()[0].children[1].innerText;
                var data = prompt("please confirm your password");
                var abc = document.getElementById("userIdid").value;
                $.ajax({
                    url: "/Accounts/ValidateUser?userId=" + document.getElementById("userIdid").value + "&password=" + data,
                    type: "POST",
                    success: function (data) {
                        if (data == true) {
                            $.ajax({
                                url: "/Accounts/DeleteUser?userId=" + userId,
                                method: "POST",
                                success: function () {
                                    swal("User Deleted");
                                }
                            })
                        }
                        else {
                            alert("invalid password");
                        }
                    }
                });
            })
        }
    })
}

function UserConfTemp(data) {
    var x1 = data.id;
    var menu = document.getElementById("conftbl").children;
    $.each(menu, function (x, y) {
        if (y.id != x1) {
            document.getElementById(y.id).style = "height:5%;border:none;border-radius:10px;background-color:lightgray";
        }
        else {
            document.getElementById(y.id).style = "height:5%;border:none;border-radius:10px;background-color:gray";
            document.getElementById("confcontentdiv").innerHTML = $('#' + y.value).html();
            if (x1 == "userConf") {
                $('#createUserspan').click(function () {
                    CreateUserTemplateLoad();
                })


                $('#deleteUserspan').click(function () {
                    DeleteUserTemplateLoad();
                })
            }
            else if (x1 == "sapconf") {
                var moduleId = document.getElementById("moduleId").value;
                $('#createSAPspan').click(function () {
                    var temp = $("#sapconftemp").html();
                    document.getElementById("MainConfigureBody").innerHTML = temp;
                    getSapData(moduleId);
                });
                $('#EditWeightSap').click(function () {
                    var temp = $("#EditWeightSaptemp").html();
                    document.getElementById("MainConfigureBody").innerHTML = temp;
                    getSapData(moduleId,1);
                });
                getSapData(moduleId);
            }
            else if (x1 == "ipconf") {
                var moduleId = document.getElementById("moduleId").value;
                if (moduleId == 1 || moduleId == 3) {
                    $('#TechniforConfBtn').click(function () {
                        var tcp = $('#TechniforIPtemp').html();
                        document.getElementById("MainConfigureBody").innerHTML = tcp;
                        GetIpData(moduleId, 1);
                    })
                    $('#PLCConfbtn').click(function () {
                        var tcp = $('#PLCIPtemp').html();
                        document.getElementById("MainConfigureBody").innerHTML = tcp;
                        GetIpData(moduleId, 2);
                    })
                }
                if (moduleId == 2) {
                    $('#moxaConfBtn').click(function () {
                        var tcp = $('#moxaIPtemp').html();
                        document.getElementById("MainConfigureBody").innerHTML = tcp;
                        GetIpData(moduleId, 1);
                    })
                }
                if (moduleId == 5) {
                    $('#WeightipConfBtn').click(function () {
                        var tcp = $('#WeightIPtemp').html();
                        document.getElementById("MainConfigureBody").innerHTML = tcp;
                        GetIpData(moduleId, 1);
                    })
                }

            }
            else if (x1 == "tempconf") {
                var mainbtns = $("#templatebtntemp").html();
                document.getElementById("confcontentdiv").innerHTML = mainbtns;
                $('#createTemplate').click(function () {
                    var tcp = $('#temtables').html();
                    document.getElementById("MainConfigureBody").innerHTML = tcp;
                    var flexdiv = document.createElement("div");
                    flexdiv.style = "display:flex;";
                    var tempname = $("#templatename").html();
                    flexdiv.innerHTML += tempname;
                    var btns = $("#addTemplateButton").html();
                    flexdiv.innerHTML += btns;
                    document.getElementById("tempcreateindiv").append(flexdiv);

                    $('#templatemaintable').on('click', 'input[type="checkbox"]', function (e) {
                        var x = this;
                        if (x.id == "ShellPunching") {
                            if (this.checked == true) {
                                if (shellCount > 3) {
                                    this.checked = false;
                                }
                                else {
                                    this.checked = true;
                                    shellCount += 1;
                                }
                            }
                            else {
                                shellCount -= 1;
                            }
                        }
                        else if (x.id == "DiscPunching") {
                            if (this.checked == true) {
                                if (discCount > 1) {
                                    this.checked = false;
                                }
                                else {
                                    this.checked = true;
                                    discCount += 1;
                                }
                            }
                            else {
                                discCount -= 1;
                            }
                        }
                    })


                })
                $('#edittemplate').click(function () {
                    var maindiv = $("#edittemplatetemp").html();
                    document.getElementById("MainConfigureBody").innerHTML = maindiv;
                    var tcp = $('#dropdown').html();
                    document.getElementById("mainEditDiv").innerHTML = tcp;
                    var tables = $("#temtables").html();
                    document.getElementById("mainEditDiv").innerHTML += tables;
                    var flexdiv = document.createElement("div");
                    flexdiv.style = "display:flex;";
                    var tempname = $("#templatename").html();
                    flexdiv.innerHTML += tempname;
                    var btns = $("#UPDATETemplateButton").html();
                    flexdiv.innerHTML += btns;
                    document.getElementById("mainEditDiv").append(flexdiv);
                    updatetempajax();
                    $('#templatemaintable').on('click', 'input[type="checkbox"]', function (e) {
                        var x = this;
                        if (x.name == "ShellPunching") {
                            if (this.checked == true) {
                                if (shellCount > 3) {
                                    this.checked = false;
                                }
                                else {
                                    this.checked = true;
                                    shellCount += 1;
                                }
                            }
                            else {
                                shellCount -= 1;
                            }
                        }
                        else if (x.name == "DiscPunching") {
                            if (this.checked == true) {
                                if (discCount > 1) {
                                    this.checked = false;
                                }
                                else {
                                    this.checked = true;
                                    discCount += 1;
                                }
                            }
                            else {
                                discCount -= 1;
                            }
                        }
                    })
                })
                $('#deletetemplate').click(function () {
                    var tcp = $('#deletetemplatetemp').html();
                    document.getElementById("MainConfigureBody").innerHTML = tcp;
                    var tcp1 = $('#dropdown').html();
                    document.getElementById("mainDelDiv").innerHTML = tcp1;
                    var delbtn = $("#Deletebutt").html();
                    document.getElementById("mainDelDiv").innerHTML += delbtn;
                    Deletetempajax();
                })
            }
        }
    })
}


function GetIpData(moduleId, type) {
    $.ajax({
        url: '/Configure/GetSapLink?moduleId=' + moduleId,
        type: "GET",
        success: function (data) {
            if (moduleId == 1 || moduleId == 3) {
                if (type == 1) {
                    document.getElementById("TechniforIp").value = data.techniforIP;
                    document.getElementById("TechniforPort").value = data.techniforPort;
                    document.getElementById("IPID").value = data.tcpId;
                }
                else {

                    document.getElementById("PLCIp").value = data.plcip;
                    document.getElementById("PLCPort").value = data.plcPort;
                    document.getElementById("IPID").value = data.tcpId;
                }
            }
            else if (moduleId == 2) {
                document.getElementById("moxaIp").value = data.techniforIP;
                document.getElementById("moxaSlot").value = data.techniforslot;
                document.getElementById("Moxarack").value = data.techniforRack;
                document.getElementById("IPID").value = data.tcpId;
            }
            else if (moduleId == 5) {
                document.getElementById("WeightIp").value = data.techniforIP;
                document.getElementById("WeightPort").value = data.techniforPort;
                document.getElementById("IPID").value = data.tcpId;
            }
        }
    })
}


function getSapData(moduleid,type = 0) {
    $.ajax({
        url: '/Configure/GetSapLink?moduleId=' + moduleid,
        type: "GET",
        success: function (data) {
            if (moduleid != 5) {
                document.getElementById('SAPLink').value = data.sapLink;
                document.getElementById('SAPLinkId').value = data.sapLinkId;
                document.getElementById("SAPPassword").value = data.sapPassword;
                document.getElementById("SAPUserName").value = data.sapUserName;
            }
            else {
                if (type != 0) {
                    document.getElementById('SAPLink').value = data.sapWeightLink;
                    document.getElementById('SAPLinkId').value = data.sapWeightId;
                    document.getElementById("SAPPassword").value = data.sapPassword;
                    document.getElementById("SAPUserName").value = data.sapUserName;
                }
                else {
                    document.getElementById('SAPLink').value = data.sapLink;
                    document.getElementById('SAPLinkId').value = data.sapLinkId;
                    document.getElementById("SAPPassword").value = data.sapPassword;
                    document.getElementById("SAPUserName").value = data.sapUserName;
                }
            }
        }
    });
}

function Deletetempajax() {
    moduleid = document.getElementById("moduleId").value;
    $.ajax({
        url: "/Plate/GetTemplateDropDown?ModuleId=" + moduleid,
        type: "GET",
        success: function (data) {
            var dropDown = document.getElementById("TemplateSelection");
            $.each(data, function (index, value) {
                var newOption = document.createElement("option");
                newOption.style = "width:100%;text-align:center;border-color:black;background-color:gray;color:white;font-weight:500;border-radius:5px";
                newOption.id = value.id;
                newOption.value = value.id;
                newOption.innerHTML = value.templateName;
                dropDown.appendChild(newOption);
            });
        }

    });
}


function updatetempajax() {
    moduleid = document.getElementById("moduleId").value;
    $.ajax({
        url: "/Plate/GetTemplateDropDown?ModuleId=" + moduleid,
        type: "GET",
        success: function (data) {
            var dropDown = document.getElementById("TemplateSelection");
            $.each(data, function (index, value) {
                var newOption = document.createElement("option");
                newOption.style = "width:100%;text-align:center;border-color:black;background-color:gray;color:white;font-weight:500;border-radius:5px";
                newOption.id = value.id;
                newOption.value = value.id;
                newOption.innerHTML = value.templateName;
                dropDown.appendChild(newOption);
            });
        }

    });
    $('#TemplateSelection').change(function (e) {
        var lines = 7;
        shellCount = 0;
        discCount = 0;
        var TemplateRows = document.getElementById("TemplateSelection").value;
        var modid = document.getElementById("moduleId").value;
        document.getElementById("templateNameTag").value = document.getElementById(TemplateRows).innerText;
        if (TemplateRows != 0 && TemplateRows != undefined) {
            $.ajax({
                url: "/Plate/GetTemplateRows?TemplateId=" + TemplateRows,
                type: "GET",
                success: function (response) {
                    document.getElementById("TemplateBody").innerHTML = "";
                    $.each(response, function (index, value) {
                        var row = document.createElement("tr");
                        var cell1 = document.createElement("td");
                        cell1.textContent = index + 1;
                        cell1.bgColor = "#F5F5F5";
                        cell1.align = "center";
                        row.appendChild(cell1);
                        if (modid == 5) {

                            var shell = document.createElement("td");
                            var txt = document.createElement("input");
                            txt.type = "checkbox";
                            txt.name = "ShellPunching";
                            txt.value = value.isShell;
                            txt.id = (index + 1).toString();
                            txt.checked = value.isShell;
                            shell.bgColor = "#F5F5F5";
                            shell.append(txt);
                            row.append(shell);
                            

                            var disc = document.createElement("td");
                            var txt1 = document.createElement("input");
                            txt1.type = "checkbox";
                            txt1.value = value.isDisc;
                            txt1.name = "DiscPunching";
                            txt1.id = (index + 1).toString();
                            txt1.checked = value.isDisc;
                            disc.bgColor = "#F5F5F5"
                            disc.append(txt1);
                            row.append(disc);
                            if (value.isShell == true) {
                                shellCount += 1;
                            }
                            if (value.isDisc == true) {
                                discCount += 1;
                            }
                        }

                        var cell2 = document.createElement("td");
                        cell2.textContent = value.row;
                        cell2.bgColor = "#F5F5F5";
                        cell2.contentEditable = true;
                        row.appendChild(cell2);
                        document.getElementById("TemplateBody").append(row);
                    });
                    lines = lines - response.length
                    if (lines != 0) {
                        for (i = 1; i <= lines; i++) {
                            var row = document.createElement("tr");
                            var cell1 = document.createElement("td");
                            cell1.textContent = response.length + i;
                            cell1.bgColor = "#F5F5F5";
                            cell1.align = "center";
                            row.appendChild(cell1);
                            if (modid == 5) {

                                var shell = document.createElement("td");
                                var txt = document.createElement("input");
                                txt.type = "checkbox";
                                txt.name = "ShellPunching";
                                txt.id = (response.length + i).toString();
                                shell.bgColor = "#F5F5F5";
                                shell.append(txt);
                                row.append(shell);

                                var disc = document.createElement("td");
                                var txt1 = document.createElement("input");
                                txt1.type = "checkbox";
                                txt1.name = "DiscPunching";
                                txt1.id = (response.length + i).toString();
                                disc.bgColor = "#F5F5F5"
                                disc.append(txt1);
                                row.append(disc);
                            }

                            var cell2 = document.createElement("td");
                            cell2.textContent = "";
                            cell2.bgColor = "#F5F5F5";
                            cell2.contentEditable = true;
                            row.appendChild(cell2);
                            document.getElementById("TemplateBody").append(row);
                        }
                    }
                }
            });
        }
    });
}



