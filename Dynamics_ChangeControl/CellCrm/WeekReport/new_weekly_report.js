
$ = parent.$;

function OnLoad() {
    var formtype = Xrm.Page.ui.getFormType();

    setBtn();
    subGridOnload();
}

function setBtn(formtype) {

    var formtype = Xrm.Page.ui.getFormType();
    //신규 생성 화면이 아닐 때
    if (formtype != 1) {
        Xrm.Page.ui.controls.get("new_btn_copy").setVisible(true);

        var html_tag = "<div style='float:right; padding-right: 5px;'><input id='btn_copy_master' type='button' value='Copy to Next Week' style='width:120px; height:25px;background-color:#30B22C;border:none;color:white;cursor: pointer;' /></div>";
        //html_tag += "<div style='float:left; padding-right: 5px;'><input id='down_pdf' type='button' value='다운로드(PDF)' style='width:110px; height:22px;background-color:#30B22C;border:none;color:white;cursor: pointer;' /></div>";
        //html_tag += "<div style='float:left;'><input id='down_etc' type='button' value='다운로드(기타)' style='width:110px; height:22px;background-color:#30B22C;border:none;color:white;cursor: pointer;' /></div>";

        $("#new_btn_copy_d").html(html_tag);

        $("#btn_copy_master").click(function () {
            if (confirm("주간업무보고를 다음 주차로 복사하시겠습니까?")) {
                Xrm.Page.data.entity.attributes.get("new_chk_copy_flag").setValue(true);

                Xrm.Page.data.save().then(
                    function () {
                        if (confirm("복사가 완료되었습니다.\n주간업무보고 리스트로 이동하시겠습니까?")) {
                            parent.parent.$("#Tabnew_weekly_report-main").click(); //상단 "주간업무보고" 메뉴 클릭 효과
                        }
                    },
                    function () {
                        Xrm.Page.data.entity.attributes.get("new_chk_copy_flag").setValue(false);
                    }
                );
            }

        });
    }
    else {
        Xrm.Page.ui.controls.get("new_btn_copy").setVisible(false);
    }
}


function subGridOnload() {

    if ($("#subgrid_detail_d .wj-cell.wj-header").length <= 0) {
        // delay one second and try again.
        setTimeout(subGridOnload, 1000);
        return;
    }

    //Xrm.Page.getControl("subgrid_detail").addOnLoad(subGridOnload_start); 

    var styles = "";
    styles += '<style type="text/css">'

        //Subgrid Header Color
        + '#new_d_input_expected_monday, #new_d_input_expected_tuesday, #new_d_input_expected_wednesday, #new_d_input_expected_thursday, #new_d_input_expected_friday, #new_d_expected_sum {'
        + 'color: #0b44b8; }'
        + '#new_d_input_real_monday, #new_d_input_real_tuesday, #new_d_input_real_wednesday, #new_d_input_real_thursday, #new_d_input_real_friday, #new_d_real_sum {'
        + 'color: #ee0c0c; }'

        //8.2 Version
        + '#new_d_expected_monday_sum .ms-crm-Inline-Value, #new_d_expected_tuesday_sum .ms-crm-Inline-Value, #new_d_expected_wednesday_sum .ms-crm-Inline-Value, #new_d_expected_thursday_sum .ms-crm-Inline-Value, #new_d_expected_friday_sum .ms-crm-Inline-Value, #new_d_expected_sum .ms-crm-Inline-Value,'
        + '#new_d_expected_monday_sum1 .ms-crm-Inline-Value, #new_d_expected_tuesday_sum1 .ms-crm-Inline-Value, #new_d_expected_wednesday_sum1 .ms-crm-Inline-Value, #new_d_expected_thursday_sum1 .ms-crm-Inline-Value, #new_d_expected_friday_sum1 .ms-crm-Inline-Value, #new_d_expected_sum1 .ms-crm-Inline-Value'
        + '{ color: #0b44b8; }'
        + '#new_d_real_monday_sum .ms-crm-Inline-Value, #new_d_real_tuesday_sum .ms-crm-Inline-Value, #new_d_real_wednesday_sum .ms-crm-Inline-Value, #new_d_real_thursday_sum .ms-crm-Inline-Value, #new_d_real_friday_sum .ms-crm-Inline-Value, #new_d_real_sum .ms-crm-Inline-Value,'
        + '#new_d_real_monday_sum1 .ms-crm-Inline-Value, #new_d_real_tuesday_sum1 .ms-crm-Inline-Value, #new_d_real_wednesday_sum1 .ms-crm-Inline-Value, #new_d_real_thursday_sum1 .ms-crm-Inline-Value, #new_d_real_friday_sum1 .ms-crm-Inline-Value, #new_d_real_sum1 .ms-crm-Inline-Value'
        + '{ color: #ee0c0c; }'

        //9.0 Version
        + '#new_d_expected_monday_sum .ms-crm-Inline-Value label, #new_d_expected_tuesday_sum .ms-crm-Inline-Value label, #new_d_expected_wednesday_sum .ms-crm-Inline-Value label, #new_d_expected_thursday_sum .ms-crm-Inline-Value label, #new_d_expected_friday_sum .ms-crm-Inline-Value label, #new_d_expected_sum .ms-crm-Inline-Value label,'
        + '#new_d_expected_monday_sum1 .ms-crm-Inline-Value label, #new_d_expected_tuesday_sum1 .ms-crm-Inline-Value label, #new_d_expected_wednesday_sum1 .ms-crm-Inline-Value label, #new_d_expected_thursday_sum1 .ms-crm-Inline-Value label, #new_d_expected_friday_sum1 .ms-crm-Inline-Value label, #new_d_expected_sum1 .ms-crm-Inline-Value label'
        + '{ color: #0b44b8; }'
        + '#new_d_real_monday_sum .ms-crm-Inline-Value label, #new_d_real_tuesday_sum .ms-crm-Inline-Value label, #new_d_real_wednesday_sum .ms-crm-Inline-Value label, #new_d_real_thursday_sum .ms-crm-Inline-Value label, #new_d_real_friday_sum .ms-crm-Inline-Value label, #new_d_real_sum .ms-crm-Inline-Value label,'
        + '#new_d_real_monday_sum1 .ms-crm-Inline-Value label, #new_d_real_tuesday_sum1 .ms-crm-Inline-Value label, #new_d_real_wednesday_sum1 .ms-crm-Inline-Value label, #new_d_real_thursday_sum1 .ms-crm-Inline-Value label, #new_d_real_friday_sum1 .ms-crm-Inline-Value label, #new_d_real_sum1 .ms-crm-Inline-Value label'
        + '{ color: #ee0c0c; }'

        + '</style>';
    $('body').append(styles);

    //Subgrid Auto Height
    //editible subgrid는 양식의 Auto Height가 작동안하는 버그가 존재하여 수동으로 코드 작성

    var rowHeight = -1;

    //9.0 Version
    if ($(".wj-cells").css("height") != undefined) {
        rowHeight = parseInt($(".wj-cells").css("height").replace("px", ""));
    }

    //8.2 Version(주석)
    else {
        //rowHeight = $(".wj-cell.wj-header.data-selectable").length * 28; //8.2는 Row 하나당 28px;
    }

    if (rowHeight != -1) {
        $("#subgrid_detail_d").css("height", (rowHeight + 150).toString() + "px"); //rowHeight+150px;

        $("#crmCCDataSet_subgrid_detail").css("height", (rowHeight + 70).toString() + "px"); //rowHeight+70px;
        $("#crmCCDataSet_subgrid_detail div:eq(4)").css("height", (rowHeight + 70).toString() + "px"); //200+70px;
    }



    //화면이 Resize 될 때 Height가 늘어나는 버그 Fix
    $(window).resize(function () {
        // do somthing
        var rowHeight = -1;

        //9.0 Version
        if ($(".wj-cells").css("height") != undefined) {
            rowHeight = parseInt($(".wj-cells").css("height").replace("px", ""));
        }

        //8.2 Version(주석)
        else {
            //rowHeight = $(".wj-cell.wj-header.data-selectable").length * 28; //8.2는 Row 하나당 28px;
        }

        if (rowHeight != -1) {
            $("#subgrid_detail_d").css("height", (rowHeight + 150).toString() + "px"); //rowHeight+150px;

            $("#crmCCDataSet_subgrid_detail").css("height", (rowHeight + 70).toString() + "px"); //rowHeight+70px;
            $("#crmCCDataSet_subgrid_detail div:eq(4)").css("height", (rowHeight + 70).toString() + "px"); //200+70px;
        }
    });

}

function ChangeSubgridCss() {
    for (var i = 0; i < $("#subgrid_detail_d .wj-cell").not(".wj-cell.wj-header").length; i++) {
        if ((i % 2) == 0) {
            var element = $("#subgrid_detail_d .wj-cell").not(".wj-cell.wj-header").eq(i);
            var style = element.attr('style');
            element.attr('style', style + ' background-color: red !important');
        }
    }
}

function GridRowSelected(executionContext) {
    executionContext.getFormContext().getData().getEntity().attributes.forEach(function (attr) {
        //debugger;
        //var disabledList = ["new_l_project", "new_l_related_project_detail", "new_l_work_master1", "new_l_work_master2", "new_l_work_master3"];
        var disabledList = ["new_p_division", "new_txt_level1", "new_txt_level2", "new_txt_level3", "new_l_work_master3"];
        if (disabledList.indexOf(attr.getName()) > -1) {
            attr.controls.forEach(function (c) {
                c.setDisabled(true);
            })
        }
    });
}

//subgrid에서 detail OnSave 이벤트
function GridRowOnSave() {
    var subgrid_detail = Xrm.Page.getControl("subgrid_detail").getGrid();
    var detailRows = subgrid_detail.getRows();
    var rowCount = detailRows.getLength();

    //주간M 월~금(예상)
    var new_d_expected_monday_sum = 0.00;
    var new_d_expected_tuesday_sum = 0.00;
    var new_d_expected_wednesday_sum = 0.00;
    var new_d_expected_thursday_sum = 0.00;
    var new_d_expected_friday_sum = 0.00;

    //주간M 월~금(실적)
    var new_d_real_monday_sum = 0.00;
    var new_d_real_tuesday_sum = 0.00;
    var new_d_real_wednesday_sum = 0.00;
    var new_d_real_thursday_sum = 0.00;
    var new_d_real_friday_sum = 0.00;

    for (var i = 0; i < rowCount; i++) {

        var rowEntity = detailRows.get(i).getData().getEntity();
        new_d_expected_monday_sum += Number(rowEntity.getAttributes().get("new_d_input_expected_monday").getValue());
        new_d_expected_tuesday_sum += Number(rowEntity.getAttributes().get("new_d_input_expected_tuesday").getValue());
        new_d_expected_wednesday_sum += Number(rowEntity.getAttributes().get("new_d_input_expected_wednesday").getValue());
        new_d_expected_thursday_sum += Number(rowEntity.getAttributes().get("new_d_input_expected_thursday").getValue());
        new_d_expected_friday_sum += Number(rowEntity.getAttributes().get("new_d_input_expected_friday").getValue());

        new_d_real_monday_sum += Number(rowEntity.getAttributes().get("new_d_input_real_monday").getValue());
        new_d_real_tuesday_sum += Number(rowEntity.getAttributes().get("new_d_input_real_tuesday").getValue());
        new_d_real_wednesday_sum += Number(rowEntity.getAttributes().get("new_d_input_real_wednesday").getValue());
        new_d_real_thursday_sum += Number(rowEntity.getAttributes().get("new_d_input_real_thursday").getValue());
        new_d_real_friday_sum += Number(rowEntity.getAttributes().get("new_d_input_real_friday").getValue());
    }

    Xrm.Page.data.entity.attributes.get("new_d_expected_monday_sum").setValue(new_d_expected_monday_sum);
    Xrm.Page.data.entity.attributes.get("new_d_expected_tuesday_sum").setValue(new_d_expected_tuesday_sum);
    Xrm.Page.data.entity.attributes.get("new_d_expected_wednesday_sum").setValue(new_d_expected_wednesday_sum);
    Xrm.Page.data.entity.attributes.get("new_d_expected_thursday_sum").setValue(new_d_expected_thursday_sum);
    Xrm.Page.data.entity.attributes.get("new_d_expected_friday_sum").setValue(new_d_expected_friday_sum);

    Xrm.Page.data.entity.attributes.get("new_d_real_monday_sum").setValue(new_d_real_monday_sum);
    Xrm.Page.data.entity.attributes.get("new_d_real_tuesday_sum").setValue(new_d_real_tuesday_sum);
    Xrm.Page.data.entity.attributes.get("new_d_real_wednesday_sum").setValue(new_d_real_wednesday_sum);
    Xrm.Page.data.entity.attributes.get("new_d_real_thursday_sum").setValue(new_d_real_thursday_sum);
    Xrm.Page.data.entity.attributes.get("new_d_real_friday_sum").setValue(new_d_real_friday_sum);

    Xrm.Page.data.entity.attributes.get("new_d_expected_sum").setValue(new_d_expected_monday_sum + new_d_expected_tuesday_sum + new_d_expected_wednesday_sum + new_d_expected_thursday_sum + new_d_expected_friday_sum);
    Xrm.Page.data.entity.attributes.get("new_d_real_sum").setValue(new_d_real_monday_sum + new_d_real_tuesday_sum + new_d_real_wednesday_sum + new_d_real_thursday_sum + new_d_real_friday_sum);

    /*
    always: The data is always sent with a save.
    never: The data is never sent with a save. When this is used, the field(s) in the form for this attribute cannot be edited.
    dirty: Default behavior. The data is sent with the save when it has changed.
     */
    Xrm.Page.getAttribute("new_d_expected_monday_sum").setSubmitMode("never");
    Xrm.Page.getAttribute("new_d_expected_tuesday_sum").setSubmitMode("never");
    Xrm.Page.getAttribute("new_d_expected_wednesday_sum").setSubmitMode("never");
    Xrm.Page.getAttribute("new_d_expected_thursday_sum").setSubmitMode("never");
    Xrm.Page.getAttribute("new_d_expected_friday_sum").setSubmitMode("never");

    Xrm.Page.getAttribute("new_d_real_monday_sum").setSubmitMode("never");
    Xrm.Page.getAttribute("new_d_real_tuesday_sum").setSubmitMode("never");
    Xrm.Page.getAttribute("new_d_real_wednesday_sum").setSubmitMode("never");
    Xrm.Page.getAttribute("new_d_real_thursday_sum").setSubmitMode("never");
    Xrm.Page.getAttribute("new_d_real_friday_sum").setSubmitMode("never");

    Xrm.Page.getAttribute("new_d_expected_sum").setSubmitMode("never");
    Xrm.Page.getAttribute("new_d_real_sum").setSubmitMode("never");
}
