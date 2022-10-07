var counter = 1;

function DownCoilerReports(fromDate, Todate, pagesize, pageindex) {
    var moduleId = $('#ModuleId').attr("title");
    var filter = { FromDate: fromDate, ToDate: Todate, moduleId: moduleId, pageSize: pagesize, pageIndex: pageindex };
    $.ajax({
        url: "/Reports/DownCoilerReportData",
        method: "POST",
        data: filter,
        success: function (data) {
            document.getElementById("PageSize").value = data.pageSize;
            document.getElementById("currentPageNo").innerHTML = data.pageIndex + 1;
            document.getElementById("totalPages").innerHTML = (data.totalRecords / data.pageSize) > Math.floor(data.totalRecords / data.pageSize) ? Math.floor(data.totalRecords / data.pageSize) + 1 : Math.floor(data.totalRecords / data.pageSize);
            var table = document.getElementById("ReportsTable");
            $('#ReportsTable').empty();
            var thead = document.createElement("thead");
            thead.style = "white-space:pre;text-align:center;user-select:none";
            var tr1 = document.createElement("tr");
            var tr2 = document.createElement("tr");
            var SapData = document.createElement("th");
            SapData.innerHTML = "DATA RECEIVED FROM SAP";
            SapData.colSpan = "12";
            var ysdata = document.createElement("th");
            ysdata.innerHTML = "Data Sent for Coil Marking ";
            ysdata.colSpan = "11";

            var row = document.createElement("th");
            row.innerHTML = "Sr.No.";
            row.scope = "col";
            var row2 = document.createElement("th");
            row2.innerHTML = "SAP fetchtime";
            row2.scope = "col";
            var row3 = document.createElement("th");
            row3.innerHTML = "MAT ID";
            row3.scope = "col";
            var row4 = document.createElement("th");
            row4.innerHTML = "COIL ID";
            row4.scope = "col";
            var row5 = document.createElement("th");
            row5.innerHTML = "HEAT No";
            row5.scope = "col";
            var row6 = document.createElement("th");
            row6.innerHTML = "GRADE ";
            row6.scope = "col";
            var row7 = document.createElement("th");
            row7.innerHTML = "WIDTH";
            row7.scope = "col";
            var row8 = document.createElement("th");
            row8.innerHTML = "THICKNESS";
            row8.scope = "col";
            var row9 = document.createElement("th");
            row9.innerHTML = "CUST_NAME";
            row9.scope = "col";
            var row10 = document.createElement("th");
            row10.innerHTML = "P_ORDER";
            row10.scope = "col";
            var row11 = document.createElement("th");
            row11.innerHTML = "P_NUMBER";
            row11.scope = "col";
            var row12 = document.createElement("th");
            row12.innerHTML = "AOT_WEIGHT";
            row12.scope = "col";
            var row13 = document.createElement("th");
            row13.innerHTML = "Record ID";
            row13.scope = "col";
            var row14 = document.createElement("th");
            row14.innerHTML = "Data update Time";
            row14.scope = "col";
            var row15 = document.createElement("th");
            row15.innerHTML = "DISC LINE 1";
            row15.scope = "col";
            var row16 = document.createElement("th");
            row16.innerHTML = "DISC LINE 2";
            row16.scope = "col";
            var row17 = document.createElement("th");
            row17.innerHTML = "SHELL LINE 1";
            row17.scope = "col";
            var row18 = document.createElement("th");
            row18.innerHTML = "SHELL LINE 2";
            row18.scope = "col";
            var row19 = document.createElement("th");
            row19.innerHTML = "SHELL LINE 3";
            row19.scope = "col";
            var row20 = document.createElement("th");
            row20.innerHTML = "SHELL LINE 4";
            row20.scope = "col";
            var row21 = document.createElement("th");
            row21.innerHTML = "LOGO STATUS";
            row21.scope = "col";
            var row22 = document.createElement("th");
            row22.innerHTML = "COIL WIDTH";
            row22.scope = "col";
            var row23 = document.createElement("th");
            row23.innerHTML = "COIL DIAMETER";
            row23.scope = "col";




            tr1.append(SapData);
            tr1.append(ysdata);
            tr2.append(row);
            tr2.append(row2);
            tr2.append(row3);
            tr2.append(row4);
            tr2.append(row5);
            tr2.append(row6);
            tr2.append(row7);
            tr2.append(row8);
            tr2.append(row9);
            tr2.append(row10);
            tr2.append(row11);
            tr2.append(row12);
            tr2.append(row13);
            tr2.append(row14);
            tr2.append(row15);
            tr2.append(row16);
            tr2.append(row17);
            tr2.append(row18);
            tr2.append(row19);
            tr2.append(row20);
            tr2.append(row21);
            tr2.append(row22);
            tr2.append(row23);
            thead.append(tr1);
            thead.append(tr2);
            table.append(thead);




            var tblbdy = document.createElement("tbody");
            tblbdy.id = "ColdLevellerDataReportBody";
            $.each(data.reportData, function (i, k) {
                var tr = document.createElement("tr");
                $.each(k, function (x, y) {
                    var td = document.createElement("td");
                    if (x == "slno") {
                        td.innerHTML = counter;
                    }
                    else {
                        if (y == "") {
                            td.innerHTML = "--";
                        }
                        else {
                            td.innerHTML = y;
                        }
                    }
                    td.style = "colour:gray";
                    tr.appendChild(td);
                }
                )
                tblbdy.append(tr);
                table.append(tblbdy);
                counter = counter + 1;
            }
            );
            counter = 1;

            document.getElementById("prvbtn").disabled = false;
            document.getElementById("nextbtn").disabled = false;
        }
    });
}





function ColdLevellerReports(fromDate, Todate, pagesize, pageindex) {
    var moduleId = $('#ModuleId').attr("title");
    var filter = { FromDate: fromDate, ToDate: Todate, moduleId: moduleId, pageSize: pagesize, pageIndex: pageindex };
    $.ajax({
        url: "/Reports/ColdLevellerReportData",
        method: "POST",
        data: filter,
        success: function (data) {
            document.getElementById("PageSize").value = data.pageSize;
            document.getElementById("currentPageNo").innerHTML = data.pageIndex + 1;
            document.getElementById("totalPages").innerHTML = (data.totalRecords / data.pageSize) > Math.floor(data.totalRecords / data.pageSize) ? Math.floor(data.totalRecords / data.pageSize) + 1 : Math.floor(data.totalRecords / data.pageSize);
            var table = document.getElementById("ReportsTable");
            $('#ReportsTable').empty();
            var thead = document.createElement("thead");
            thead.style = "white-space:pre;text-align:center;user-select:none";
            var tr1 = document.createElement("tr");
            var tr2 = document.createElement("tr");
            var slno = document.createElement("th");
            slno.innerHTML = "Sl No";
            slno.rowSpan = "2";
            var p_time = document.createElement("th");
            p_time.innerHTML = "Marking Time";
            p_time.rowSpan = "2";
            var SapData = document.createElement("th");
            SapData.innerHTML = "DATA RECEIVED FROM SAP";
            SapData.colSpan = "7";
            var ysdata = document.createElement("th");
            ysdata.innerHTML = "YS_T received from JSW DB";
            ysdata.colSpan = "2";
            var PCIData = document.createElement("th");
            PCIData.innerHTML = "DATA LOADED TO PCI TABLE OF JSW DB";
            PCIData.colSpan = "7";
            var plateno = document.createElement("th");
            plateno.innerHTML = "Plate no";
            plateno.scope = "col";

            var plateno1 = document.createElement("th");
            plateno1.innerHTML = "Plate no";
            plateno1.scope = "col";

            var plateno2 = document.createElement("th");
            plateno2.innerHTML = "Plate no";
            plateno2.scope = "col";

            var grade = document.createElement("th");
            grade.innerHTML = "Grade";
            grade.scope = "col";

            var length = document.createElement("th");
            length.innerHTML = "Length";
            length.scope = "col";

            var thick = document.createElement("th");
            thick.innerHTML = "Thickness";
            thick.scope = "col";

            var width = document.createElement("th");
            width.innerHTML = "Width";
            width.scope = "col";

            var weight = document.createElement("th");
            weight.innerHTML = "Weight";
            weight.scope = "col";

            var yst = document.createElement("th");
            yst.innerHTML = "YS_T";
            yst.scope = "col";

            var yst1 = document.createElement("th");
            yst1.innerHTML = "YS_T";
            yst1.scope = "col";

            var Date = document.createElement("th");
            Date.innerHTML = "Date Time";
            Date.scope = "col";

            var stlgrade = document.createElement("th");
            stlgrade.innerHTML = "Steel Grade";
            stlgrade.scope = "col";

            var ldlength = document.createElement("th");
            ldlength.innerHTML = "Length";
            ldlength.scope = "col";

            var ldthick = document.createElement("th");
            ldthick.innerHTML = "Thickness";
            ldthick.scope = "col";

            var ldwidth = document.createElement("th");
            ldwidth.innerHTML = "Width";
            ldwidth.scope = "col";

            var ldweight = document.createElement("th");
            ldweight.innerHTML = "Weight";
            ldweight.scope = "col";

            tr1.append(slno);
            tr1.append(p_time);
            tr1.append(SapData);
            tr1.append(ysdata);
            tr1.append(PCIData);
            thead.append(tr1);
            tr2.append(plateno);
            tr2.append(grade);
            tr2.append(length);
            tr2.append(thick);
            tr2.append(width);
            tr2.append(weight);
            tr2.append(yst);
            tr2.append(plateno1);
            tr2.append(yst1);
            tr2.append(Date);
            tr2.append(plateno2);
            tr2.append(stlgrade);
            tr2.append(ldlength);
            tr2.append(ldthick);
            tr2.append(ldwidth);
            tr2.append(ldweight);
            thead.append(tr2);
            table.append(thead);


            var tblbdy = document.createElement("tbody");
            tblbdy.id = "ColdLevellerDataReportBody";
            $.each(data.reportData, function (i, k) {
                var tr = document.createElement("tr");
                $.each(k, function (x, y) {
                    var td = document.createElement("td");
                    if (x == "slno") {
                        td.innerHTML = counter;
                    }
                    else {
                        if (y == "") {
                            td.innerHTML = "--";
                        }
                        else {
                            td.innerHTML = y;
                        }
                    }
                    td.style = "colour:gray";
                    tr.appendChild(td);
                }
                )
                tblbdy.append(tr);
                table.append(tblbdy);
                counter = counter + 1;
            }
            );
            counter = 1;

            document.getElementById("prvbtn").disabled = false;
            document.getElementById("nextbtn").disabled = false;

            if (data.length == 0) {

            }

        }
    });
}





function punchingReports(fromDate, Todate, pagesize, pageindex) {
    var moduleId = $('#ModuleId').attr("title");
    var filter = { FromDate: fromDate, ToDate: Todate, moduleId: moduleId, pageSize: pagesize, pageIndex: pageindex };
    $.ajax({
        url: "/Reports/PlatePunchingReportData",
        method: "POST",
        data:filter,
        success: function (data) {
            document.getElementById("PageSize").value = data.pageSize;
            document.getElementById("currentPageNo").innerHTML = data.pageIndex + 1;
            document.getElementById("totalPages").innerHTML = (data.totalRecords / data.pageSize) > Math.floor(data.totalRecords / data.pageSize) ? Math.floor(data.totalRecords / data.pageSize) + 1 : Math.floor(data.totalRecords / data.pageSize);
            var table = document.getElementById("ReportsTable");
            $('#ReportsTable').empty();
            var thead = document.createElement("thead");
            thead.style = "white-space:pre;text-align:center;user-select:none";
            var tr1 = document.createElement("tr");
            var tr2 = document.createElement("tr");
            var slno = document.createElement("th");
            slno.innerHTML = "Sl No";
            slno.rowSpan = "2";
            var p_time = document.createElement("th");
            p_time.innerHTML = "Punching Time";
            p_time.rowSpan = "2";
            var SapData = document.createElement("th");
            SapData.innerHTML = "Plate Data From SAP";
            SapData.colSpan = "11";
            var actualdata = document.createElement("th");
            actualdata.innerHTML = "Actual Printed Data";
            actualdata.colSpan = "6";
            var plateno = document.createElement("th");
            plateno.innerHTML = "Plate Number";
            plateno.scope = "col";
            var heatno = document.createElement("th");
            heatno.innerHTML = "Heat Number";
            heatno.scope = "col";
            var size = document.createElement("th");
            size.innerHTML = "Size (L*W*T)";
            size.scope = "col";
            var weight = document.createElement("th");
            weight.innerHTML = "Weight";
            weight.scope = "col";
            var po = document.createElement("th");
            po.innerHTML = "Purchase Order";
            po.scope = "col";
            var pon = document.createElement("th");
            pon.innerHTML = "Purchase Order Number";
            pon.scope = "col";
            var md = document.createElement("th");
            md.innerHTML = "Material Description";
            md.scope = "col";
            var cn = document.createElement("th");
            cn.innerHTML = "Customer Name";
            cn.scope = "col";
            var cr = document.createElement("th");
            cr.innerHTML = "Customer Reference";
            cr.scope = "col";
            var grade = document.createElement("th");
            grade.innerHTML = "Grade";
            grade.scope = "col";
            var grade2 = document.createElement("th");
            grade2.innerHTML = "Grade Duel";
            grade2.scope = "col";
            var l1 = document.createElement("th");
            l1.innerHTML = "Line 1";
            l1.scope = "col";
            var l2 = document.createElement("th");
            l2.innerHTML = "Line 2";
            l2.scope = "col";
            var l3 = document.createElement("th");
            l3.innerHTML = "Line 3";
            l3.scope = "col";
            var l4 = document.createElement("th");
            l4.innerHTML = "Line 4";
            l4.scope = "col";
            var l5 = document.createElement("th");
            l5.innerHTML = "Line 5";
            l5.scope = "col";
            var l6 = document.createElement("th");
            l6.innerHTML = "Line 6";
            l6.scope = "col";

            tr1.append(slno);
            tr1.append(p_time);
            tr1.append(SapData);
            tr1.append(actualdata);
            thead.append(tr1);
            tr2.append(plateno);
            tr2.append(heatno);
            tr2.append(size);
            tr2.append(weight);
            tr2.append(po);
            tr2.append(pon);
            tr2.append(md);
            tr2.append(cn);
            tr2.append(cr);
            tr2.append(grade);
            tr2.append(grade2);
            tr2.append(l1);
            tr2.append(l2);
            tr2.append(l3);
            tr2.append(l4);
            tr2.append(l5);
            tr2.append(l6);
            thead.append(tr2);
            table.append(thead);

            var tblbdy = document.createElement("tbody");
            tblbdy.id = "PunchingDataReportBody";
            $.each(data.reportData, function (i, k) {
                var tr = document.createElement("tr");
                $.each(k, function (x, y) {
                    var td = document.createElement("td");
                    if (x == "slno") {
                        td.innerHTML = counter;
                    }
                    else {
                        if (y == "") {
                            td.innerHTML = "--";
                        }
                        else {
                            td.innerHTML = y;
                        }
                    }
                    td.style = "colour:gray";
                    tr.appendChild(td);
                }
                )
                tblbdy.append(tr);
                table.append(tblbdy);
                counter = counter + 1;
            }
            );
            counter = 1;

            document.getElementById("prvbtn").disabled = false;
            document.getElementById("nextbtn").disabled = false;
        }
    });

}




function markingReports(fromDate, Todate, pagesize, pageindex) {
    var moduleId = $('#ModuleId').attr("title");
    var filter = { FromDate: fromDate, ToDate: Todate, moduleId: moduleId, pageSize: pagesize, pageIndex: pageindex };
    $.ajax({
        url: "/Reports/PlateMarkingReportData",
        method: "Post",
        data:filter,
        success: function (data) {
            document.getElementById("PageSize").value = data.pageSize;
            document.getElementById("currentPageNo").innerHTML = data.pageIndex + 1;
            document.getElementById("totalPages").innerHTML = (data.totalRecords / data.pageSize) > Math.floor(data.totalRecords / data.pageSize) ? Math.floor(data.totalRecords / data.pageSize) + 1 : Math.floor(data.totalRecords / data.pageSize);
            var table = document.getElementById("ReportsTable");
            //table.style = "user-select:none";
            $('#ReportsTable').empty();
            var thead = document.createElement("thead");
            thead.style = "white-space:pre;text-align:center;user-select:none";
            var tr1 = document.createElement("tr");
            var tr2 = document.createElement("tr");
            var slno = document.createElement("th");
            slno.innerHTML = "Sl No";
            slno.rowSpan = "2";
            var p_time = document.createElement("th");
            p_time.innerHTML = "Marking Time";
            p_time.rowSpan = "2";
            var SapData = document.createElement("th");
            SapData.innerHTML = "Plate Data From SAP";
            SapData.colSpan = "11";
            var actualdata = document.createElement("th");
            actualdata.innerHTML = "Actual Printed Data";
            actualdata.colSpan = "6";
            var plateno = document.createElement("th");
            plateno.innerHTML = "Plate Number";
            plateno.scope = "col";
            var heatno = document.createElement("th");
            heatno.innerHTML = "Heat Number";
            heatno.scope = "col";
            var size = document.createElement("th");
            size.innerHTML = "Size (L*W*T)";
            size.scope = "col";
            var weight = document.createElement("th");
            weight.innerHTML = "Weight";
            weight.scope = "col";
            var po = document.createElement("th");
            po.innerHTML = "Purchase Order";
            po.scope = "col";
            var pon = document.createElement("th");
            pon.innerHTML = "Purchase Order Number";
            pon.scope = "col";
            var md = document.createElement("th");
            md.innerHTML = "Material Description";
            md.scope = "col";
            var cn = document.createElement("th");
            cn.innerHTML = "Customer Name";
            cn.scope = "col";
            var cr = document.createElement("th");
            cr.innerHTML = "Customer Reference";
            cr.scope = "col";
            var grade = document.createElement("th");
            grade.innerHTML = "Grade";
            grade.scope = "col";
            var grade2 = document.createElement("th");
            grade2.innerHTML = "Grade Duel";
            grade2.scope = "col";
            var l1 = document.createElement("th");
            l1.innerHTML = "Line 1";
            l1.scope = "col";
            var l2 = document.createElement("th");
            l2.innerHTML = "Line 2";
            l2.scope = "col";
            var l3 = document.createElement("th");
            l3.innerHTML = "Line 3";
            l3.scope = "col";
            var l4 = document.createElement("th");
            l4.innerHTML = "Line 4";
            l4.scope = "col";
            var l5 = document.createElement("th");
            l5.innerHTML = "Line 5";
            l5.scope = "col";
            var l6 = document.createElement("th");
            l6.innerHTML = "Line 6";
            l6.scope = "col";

            tr1.append(slno);
            tr1.append(p_time);
            tr1.append(SapData);
            tr1.append(actualdata);
            thead.append(tr1);
            tr2.append(plateno);
            tr2.append(heatno);
            tr2.append(size);
            tr2.append(weight);
            tr2.append(po);
            tr2.append(pon);
            tr2.append(md);
            tr2.append(cn);
            tr2.append(cr);
            tr2.append(grade);
            tr2.append(grade2);
            tr2.append(l1);
            tr2.append(l2);
            tr2.append(l3);
            tr2.append(l4);
            tr2.append(l5);
            tr2.append(l6);
            thead.append(tr2);
            table.append(thead);

            var tblbdy = document.createElement("tbody");
            tblbdy.id = "MarkingDataReportBody";
            $.each(data.reportData, function (i, k) {
                var tr = document.createElement("tr");
                $.each(k, function (x, y) {
                    var td = document.createElement("td");
                    if (x == "slno") {
                        td.innerHTML = counter;
                    }
                    else {
                        if (y == "") {
                            td.innerHTML = "--";
                        }
                        else {
                            td.innerHTML = y;
                        }
                    }
                    td.style = "colour:gray";
                    tr.appendChild(td);
                }
                )
                tblbdy.append(tr);
                table.append(tblbdy);
                counter = counter + 1;
            }
            );
            counter = 1;

            document.getElementById("prvbtn").disabled = false;
            document.getElementById("nextbtn").disabled = false;
        }
    });

}



function AuditReport(fromDate, Todate, pagesize, pageindex) {
    var moduleId = $('#ModuleId').attr("title");
    var filter = { FromDate: fromDate, ToDate: Todate, moduleId: moduleId, pageSize: pagesize, pageIndex: pageindex };
    $.ajax({
        url: "/Reports/AuditReportData",
        method: "Post",
        data: filter,
        success: function (data) {
            document.getElementById("PageSize").value = data.pageSize;
            document.getElementById("currentPageNo").innerHTML = data.pageIndex + 1;
            document.getElementById("totalPages").innerHTML = (data.totalRecords / data.pageSize) > Math.floor(data.totalRecords / data.pageSize) ? Math.floor(data.totalRecords / data.pageSize) + 1 : Math.floor(data.totalRecords / data.pageSize); 
            var table = document.getElementById("ReportsTable");
            $('#ReportsTable').empty();
            var thead = document.createElement("thead");
            thead.style = "white-space:pre;text-align:center;user-select:none";
            var tr1 = document.createElement("tr");
            var slno = document.createElement("th");
            slno.innerHTML = "Sl No";
            var User = document.createElement("th");
            User.innerHTML = "User Name";
            var log = document.createElement("th");
            log.style.width = "850px";
            log.style.overflow = "hidden";
            log.innerHTML = "LOG";
            var type = document.createElement("th");
            type.innerHTML = "Log Type";
            var logtime = document.createElement("th");
            logtime.innerHTML = "Time";





            tr1.append(slno);
            tr1.append(User);
            tr1.append(log);
            tr1.append(type);
            tr1.append(logtime);

            thead.append(tr1);
            table.append(thead);

            var tblbdy = document.createElement("tbody");
            tblbdy.id = "MarkingDataReportBody";
            $.each(data.reportData, function (i, k) {
                var tr = document.createElement("tr");
                $.each(k, function (x, y) {
                    var td = document.createElement("td");
                    if (x == "slno") {
                        td.innerHTML = counter;
                    }
                    else {
                        if (y == "") {
                            td.innerText = "--";
                        }
                        else {
                            td.innerText = y;
                        }
                    }
                    td.style = "colour:gray; overflow: hidden;max-width: 850px;";
                    tr.appendChild(td);
                }
                )
                tblbdy.append(tr);
                table.append(tblbdy);
                counter = counter + 1;
            }
            );
            counter = 1;

            document.getElementById("prvbtn").disabled = false;
            document.getElementById("nextbtn").disabled = false;

        }
    });
}



function WeightUpdateReport(fromDate, Todate, pagesize, pageindex) {
    var moduleId = $('#ModuleId').attr("title");
    var filter = { FromDate: fromDate, ToDate: Todate, moduleId: moduleId, pageSize: pagesize, pageIndex: pageindex };
    $.ajax({
        url: "/Reports/WeightUpdateReportData",
        method: "Post",
        data: filter,
        success: function (data) {
            document.getElementById("PageSize").value = data.pageSize;
            document.getElementById("currentPageNo").innerHTML = data.pageIndex + 1;
            document.getElementById("totalPages").innerHTML = (data.totalRecords / data.pageSize) > Math.floor(data.totalRecords / data.pageSize) ? Math.floor(data.totalRecords / data.pageSize) + 1 : Math.floor(data.totalRecords / data.pageSize);
            var table = document.getElementById("ReportsTable");
            $('#ReportsTable').empty();
            var thead = document.createElement("thead");
            thead.style = "white-space:pre;text-align:center;user-select:none";
            var tr1 = document.createElement("tr");
            var slno = document.createElement("th");
            slno.innerHTML = "Sl No";
            var coilid = document.createElement("th");
            coilid.innerHTML = "Coil Id";
            var weight = document.createElement("th");
            weight.innerHTML = "Weight";
            var date = document.createElement("th");
            date.innerHTML = "Created Date";
            tr1.append(slno);
            tr1.append(coilid);
            tr1.append(weight);
            tr1.append(date);
            thead.append(tr1);
            table.append(thead);

            var tblbdy = document.createElement("tbody");
            tblbdy.id = "MarkingDataReportBody";
            $.each(data.reportData, function (i, k) {
                var tr = document.createElement("tr");
                $.each(k, function (x, y) {
                    var td = document.createElement("td");
                    if (x == "slno") {
                        td.innerHTML = counter;
                    }
                    else {
                        if (y == "") {
                            td.innerText = "--";
                        }
                        else {
                            td.innerText = y;
                        }
                    }
                    td.style = "colour:gray; overflow: hidden;max-width: 850px;";
                    tr.appendChild(td);
                }
                )
                tblbdy.append(tr);
                table.append(tblbdy);
                counter = counter + 1;
            }
            );
            counter = 1;

            document.getElementById("prvbtn").disabled = false;
            document.getElementById("nextbtn").disabled = false;
        }
    });
}



function DownloadReport() {
    var dropdownValue = document.getElementById('ReportSelection').value;
    var fromDate = document.getElementById("From-Date").value;
    var Todate = document.getElementById("To-Date").value;
    if (dropdownValue == "Marking-Report") {
        markingReportsDownload(fromDate, Todate);
    }
    else if (dropdownValue == "Punching-Report") {
        punchingReportsDownload(fromDate, Todate);
    }
    else if (dropdownValue == "Cold-Leveller") {
        coldLevellerReportsDownload(fromDate, Todate);
    }
    else if (dropdownValue == "Down-Coiler") {
        DownCoilerReportsDownload(fromDate, Todate);
    }
    else if (dropdownValue == "Audit-Report") {
        AuditReportDownload(fromDate, Todate);
    }
    else if (dropdownValue == "WeightUpdateReport") {
        ManualWeightUpadateReportDownload(fromDate, Todate);
    }
}


function markingReportsDownload(fromDate, Todate) {
    var moduleId = $('#ModuleId').attr("title");
    var filter = { FromDate: fromDate, ToDate: Todate, moduleId: moduleId };
    $.ajax({
        url: "/Reports/PlateMarkingReportDataDownload",
        method: "Post",
        data: filter,
        success: function (data) {
            var excell = data.data;
            var contentType = 'application/vnd.ms-excel';
            var blob1 = b64toBlob(excell, contentType);

            const a = document.createElement("a");
            const url = window.URL.createObjectURL(blob1);
            a.href = url;
            a.download = 'PlateMarking Report' + new Date().toDateString() + new Date().toTimeString();
            a.click();


        }
    });
}



function coldLevellerReportsDownload(fromDate, Todate) {
    var moduleId = $('#ModuleId').attr("title");
    var filter = { FromDate: fromDate, ToDate: Todate, moduleId: moduleId };
    $.ajax({
        url: "/Reports/ColdLevellerReportDataDownload",
        method: "Post",
        data: filter,
        success: function (data) {
            var excell = data.data;
            var contentType = 'application/vnd.ms-excel';
            var blob1 = b64toBlob(excell, contentType);
            const a = document.createElement("a");
            const url = window.URL.createObjectURL(blob1);
            a.href = url;
            a.download = 'ColdLeveller Report' + new Date().toDateString() + new Date().toTimeString();
            a.click();
        }
    });
}


function DownCoilerReportsDownload(fromDate, Todate) {
    var moduleId = $('#ModuleId').attr("title");
    var filter = { FromDate: fromDate, ToDate: Todate, moduleId: moduleId };
    $.ajax({
        url: "/Reports/DowncoilerReportDataDownload",
        method: "Post",
        data: filter,
        success: function (data) {
            var excell = data.data;
            var contentType = 'application/vnd.ms-excel';
            var blob1 = b64toBlob(excell, contentType);
            const a = document.createElement("a");
            const url = window.URL.createObjectURL(blob1);
            a.href = url;
            a.download = 'Downcoiler report' + new Date().toDateString() + new Date().toTimeString();
            a.click();
        }
    });
}




function punchingReportsDownload(fromDate, Todate){
    var moduleId = $('#ModuleId').attr("title");
    var filter = { FromDate: fromDate, ToDate: Todate, moduleId: moduleId };
    $.ajax({
        url: "/Reports/PlatePunchingReportDataDownload",
        method: "Post",
        data: filter,
        success: function (data) {
            var excell = data.data;
            var contentType = 'application/vnd.ms-excel';
            var blob1 = b64toBlob(excell, contentType);
            const a = document.createElement("a");
            const url = window.URL.createObjectURL(blob1);
            a.href = url;
            a.download = 'PlatePunching Report'+ new Date().toDateString() + new Date().toTimeString();
            a.click();
        }
    });
}

function b64toBlob(b64Data, contentType, sliceSize) {
    contentType = contentType || 'application/vnd.ms-excel';
    sliceSize = sliceSize || 512;

    var byteCharacters = atob(b64Data);
    var byteArrays = [];

    for (var offset = 0; offset < byteCharacters.length; offset += sliceSize) {
        var slice = byteCharacters.slice(offset, offset + sliceSize);

        var byteNumbers = new Array(slice.length);
        for (var i = 0; i < slice.length; i++) {
            byteNumbers[i] = slice.charCodeAt(i);
        }

        var byteArray = new Uint8Array(byteNumbers);

        byteArrays.push(byteArray);
    }

    var blob = new Blob(byteArrays, { type: contentType });
    return blob;
}




function AuditReportDownload(fromDate, Todate) {
    var moduleId = $('#ModuleId').attr("title");
    var filter = { FromDate: fromDate, ToDate: Todate, moduleId: moduleId };
    $.ajax({
        url: "/Reports/AuditReportDataDownload",
        method: "Post",
        data: filter,
        success: function (data) {
            var excell = data.data;
            var contentType = 'application/vnd.ms-excel';
            var blob1 = b64toBlob(excell, contentType);
            const a = document.createElement("a");
            const url = window.URL.createObjectURL(blob1);
            a.href = url;
            a.download = 'Audit Report' + new Date().toDateString() + new Date().toTimeString();
            a.click();
        }
    });
}


function ManualWeightUpadateReportDownload(fromDate, Todate) {
    var moduleId = $('#ModuleId').attr("title");
    var filter = { FromDate: fromDate, ToDate: Todate, moduleId: moduleId };
    $.ajax({
        url: "/Reports/WeightUpdateReportReportDataDownload",
        method: "Post",
        data: filter,
        success: function (data) {
            var excell = data.data;
            var contentType = 'application/vnd.ms-excel';
            var blob1 = b64toBlob(excell, contentType);
            const a = document.createElement("a");
            const url = window.URL.createObjectURL(blob1);
            a.href = url;
            a.download = 'Manual Weight Update Report' + new Date().toDateString() + new Date().toTimeString();
            a.click();
        }
    });
}