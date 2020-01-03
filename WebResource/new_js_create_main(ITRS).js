$ = parent.$;



// To Do 2019 10 15 오후 - Date 넣는것 확인 해보기. 
var LOGIN_USER_DEPART = null;
var LOGIN_USER = null;
var REQUEST_USER = null;
var REQUEST_USER_DEPART = null;
var INTERNAL_CONTRACT_ID = null;
var INTERNAL_CONTRACT_STATE = null;
//법무 담당
var LAW_CHARGE = null;
//법무 본부
var LAW__MASTER = null;


function OnLoad() {

    var formtype = Xrm.Page.ui.getFormType(); //FormType 공부
    Xrm.Page.ui.controls.get("new_request_response_btn").setVisible(false);

    //도움말 사용 안함. 
    //var helper ="도움말 \n"+
    //    "1.	자금거래 : 각종 지원금 제공, 금전 대여 등\n" +
    //    "2.	유가증권 거래(주식 증여·매매·담보제공, 사채 발행 등)\n" +
    //    "3.	자산 거래(영업·판매권 양도, 부동산 임대차, 담보제공 등)\n" +
    //    "4.	상품 또는 용역 거래(CMO·연구·인력·관리 용역, 상품 매매 등)\n";


    //Xrm.Page.getAttribute("new_help").setValue(helper);

    if (formtype == 1) {
        Xrm.Page.ui.controls.get("new_request_response_btn").setVisible(false);
    }

    else
    {
        INTERNAL_CONTRACT_STATE = Xrm.Page.getAttribute("new_p_request_status").getValue();
        INTERNAL_CONTRACT_ID = Xrm.Page.data.entity.getId();
        fieldDisable();
        retrieveRequestUserEntity();
     
    }


}

// 요청서가 수정 상태 일 때는 변경 불가능 하도록. 
// 단 승인권자 일경우 열어 줘야함. 
function fieldDisable() {

    if (INTERNAL_CONTRACT_STATE != 100000000) { 

        Xrm.Page.getControl('new_name').setDisabled(true);
        Xrm.Page.getControl('ownerid').setDisabled(true);
        Xrm.Page.getControl('new_l_my_company').setDisabled(true);
        Xrm.Page.getControl('new_l_opp_company').setDisabled(true);
        Xrm.Page.getControl('new_txt_contract_name').setDisabled(true);
        Xrm.Page.getControl('new_p_global_contract_type').setDisabled(true)
        Xrm.Page.getControl('new_p_process').setDisabled(true);
        Xrm.Page.getControl('new_p_status').setDisabled(true);
        Xrm.Page.getControl('new_cur_amount').setDisabled(true);
        Xrm.Page.getControl('new_ntxt_purpose').setDisabled(true);
        Xrm.Page.getControl('new_p_qustion_1_1').setDisabled(true);
        Xrm.Page.getControl('new_ntxt_qustion_1_2').setDisabled(true);
        Xrm.Page.getControl('new_p_qustion_2_1').setDisabled(true);
        Xrm.Page.getControl('new_ntxt_qustion_2_2').setDisabled(true);
        Xrm.Page.getControl('new_p_qustion_3_1').setDisabled(true);
        Xrm.Page.getControl('new_ntxt_qustion_3_2').setDisabled(true);
        Xrm.Page.getControl('new_p_qustion_4').setDisabled(true);
        Xrm.Page.getControl('new_p_qustion_5').setDisabled(true);
        Xrm.Page.getControl('new_p_qustion_6').setDisabled(true);
        Xrm.Page.getControl('new_p_qustion_7').setDisabled(true);
        Xrm.Page.getControl('new_p_review_1_v2').setDisabled(true);
        Xrm.Page.getControl('new_p_review_2').setDisabled(true);
        Xrm.Page.getControl('new_p_review_3').setDisabled(true);
        Xrm.Page.getControl('new_p_review_4').setDisabled(true);
        Xrm.Page.getControl('new_ntxt_general_opinion').setDisabled(true);
        Xrm.Page.getControl('new_p_review_result').setDisabled(true);
        Xrm.Page.getControl('new_ntxt_review_opinion').setDisabled(true);
        Xrm.Page.getControl('new_l_cur_awaiter').setDisabled(true);
        Xrm.Page.getControl('new_credit_grant').setDisabled(true);
        Xrm.Page.getControl('new_ntxt_review_opinion').setDisabled(true);
        Xrm.Page.getControl('new_nt_comment').setDisabled(true);
        Xrm.Page.getControl('new_nt_comment2').setDisabled(true);
        Xrm.Page.getControl('new_nt_comment3').setDisabled(true);
        Xrm.Page.getControl('new_nt_comment4').setDisabled(true);
        Xrm.Page.getControl('new_nt_comment5').setDisabled(true);
        Xrm.Page.getControl('new_dt_approval1_usertime').setDisabled(true);
        Xrm.Page.getControl('new_dt_approval2_usertime').setDisabled(true);
        Xrm.Page.getControl('new_dt_approval3_usertime').setDisabled(true);
        Xrm.Page.getControl('new_dt_approval4_usertime').setDisabled(true);
        Xrm.Page.getControl('new_dt_approval5').setDisabled(true);

    }
}


//현재 버전 retreive로 변경 할 것 
function userCheck(state) {

    var ret = false;
    var html_tag;

 
    //승인 요청 : 로그인 유저 == 요청자
    if (state == 100000000) {
      
         ret = nullEqualCheck(REQUEST_USER.value[0].systemuserid, LOGIN_USER.value[0].systemuserid);

        if (ret) {

            html_tag = "<div style='float:left; padding-right:10px; margin:0px 0px 15px 0px;'><input id='accept_btn' type='button' value='승인요청' style='width:86px; height:32px; background-color:#30B22C; border:none; color:white; cursor: pointer;'/></div>";
            html_tag += "<div style='float:left; padding-right:10px; margin:0px 0px 15px 0px;'><input id='save_btn' type='button' value='변경사항 저장' style='width:86px; height:32px; background-color:#30B22C; border:none; color:white; cursor: pointer;'/></div>";
            
        }

    }
    
    // 승인 반려 - 팀장 : 로그인 유저 == 요청자의 부서의 부서장
    // 로그인 유저의 부서 == 요청자 부서 
    else if (state == 100000001) {

        ret = nullEqualCheck(LOGIN_USER.value[0].systemuserid, REQUEST_USER_DEPART.value[0]._new_l_head_value);

        if (ret) {
            ret = nullEqualCheck(LOGIN_USER_DEPART.value[0].new_name, REQUEST_USER_DEPART.value[0].new_name);
        }

        if (ret) {
            html_tag = "<div style='float:left; padding-right:10px; margin:0px 0px 15px 0px;'><input id='accept_btn1' type='button' value='승인' style='width:86px; height:32px; background-color:#30B22C; border:none; color:white; cursor: pointer;'/></div>";
            html_tag += "<div style='float:left; padding-right:10px; margin:0px 0px 15px 0px;'><input id='reject_btn' type='button' value='반려' style='width:86px; height:32px; background-color:#30B22C; border:none; color:white; cursor: pointer;'/></div>";
            Xrm.Page.getControl('new_nt_comment').setDisabled(false);    
        }
    }
    
    //승인 반려 - 법무실무자 : 로그인 유저의 부서명 == 국내법무팀
    else if (state == 100000002) {
        ret = nullEqualCheck(LOGIN_USER_DEPART.value[0].new_name, '국내법무팀');

        //법무 팀장 이면 안뜨게 

        if (nullEqualCheck(LOGIN_USER_DEPART.value[0]._new_l_head_value, LOGIN_USER.value[0].systemuserid)) {
            ret = false;
        }

        if (ret) {
            html_tag = "<div style='float:left; padding-right:10px; margin:0px 0px 15px 0px;'><input id='accept_btn1' type='button' value='승인' style='width:86px; height:32px; background-color:#30B22C; border:none; color:white; cursor: pointer;'/></div>";
            html_tag += "<div style='float:left; padding-right:10px; margin:0px 0px 15px 0px;'><input id='reject_btn' type='button' value='반려' style='width:86px; height:32px; background-color:#30B22C; border:none; color:white; cursor: pointer;'/></div>";
            Xrm.Page.getControl('new_nt_comment2').setDisabled(false);
            Xrm.Page.getControl('new_p_review_result').setDisabled(false);
            Xrm.Page.getControl('new_ntxt_review_opinion').setDisabled(false);
            
            
        }
    }
    //승인 반려 - 법무팀장   : 로그인 유저의 부서명 == 국내법무팀 && 로그인 유저의 부서장 == 로그인 유저
    else if (state == 100000003) {
        
        ret = nullEqualCheck(LOGIN_USER_DEPART.value[0].new_name, '국내법무팀');

        if (ret) {
            ret = nullEqualCheck(LOGIN_USER_DEPART.value[0]._new_l_head_value, LOGIN_USER.value[0].systemuserid);
        }
        if (ret) {
            html_tag = "<div style='float:left; padding-right:10px; margin:0px 0px 15px 0px;'><input id='accept_btn1' type='button' value='승인' style='width:86px; height:32px; background-color:#30B22C; border:none; color:white; cursor: pointer;'/></div>";
            html_tag += "<div style='float:left; padding-right:10px; margin:0px 0px 15px 0px;'><input id='reject_btn' type='button' value='반려' style='width:86px; height:32px; background-color:#30B22C; border:none; color:white; cursor: pointer;'/></div>";
            Xrm.Page.getControl('new_nt_comment3').setDisabled(false);
            Xrm.Page.getControl('new_p_review_result').setDisabled(false);
            Xrm.Page.getControl('new_ntxt_review_opinion').setDisabled(false);

        }


    }
    //승인 반려 법무담당     : 로그인 유저 id  == 법무 담당의 부서장 id
    else if (state == 100000004) {
.

        ret = nullEqualCheck(LAW_CHARGE.value[0]._new_l_head_value, LOGIN_USER.value[0].systemuserid);
   
        if (ret) {
            
            html_tag = "<div style='float:left; padding-right:10px; margin:0px 0px 15px 0px;'><input id='accept_btn1' type='button' value='승인' style='width:86px; height:32px; background-color:#30B22C; border:none; color:white; cursor: pointer;'/></div>";
            html_tag += "<div style='float:left; padding-right:10px; margin:0px 0px 15px 0px;'><input id='reject_btn' type='button' value='반려' style='width:86px; height:32px; background-color:#30B22C; border:none; color:white; cursor: pointer;'/></div>";
            Xrm.Page.getControl('new_nt_comment4').setDisabled(false);
            Xrm.Page.getControl('new_p_review_result').setDisabled(false);
            Xrm.Page.getControl('new_ntxt_review_opinion').setDisabled(false);
        }
    }
     //승인 반려 법무 본부       :로그인 유저의 부서명 == 법무본부 && 로그인 유저의 부서장 == 로그인 유저
    else if (state == 100000005) {
        
        ret = nullEqualCheck(LAW__MASTER.value[0]._new_l_head_value, LOGIN_USER.value[0].systemuserid);

        if (ret) {

            html_tag = "<div style='float:left; padding-right:10px; margin:0px 0px 15px 0px;'><input id='accept_btn1' type='button' value='승인' style='width:86px; height:32px; background-color:#30B22C; border:none; color:white; cursor: pointer;'/></div>";
            html_tag += "<div style='float:left; padding-right:10px; margin:0px 0px 15px 0px;'><input id='reject_btn' type='button' value='반려' style='width:86px; height:32px; background-color:#30B22C; border:none; color:white; cursor: pointer;'/></div>";
            Xrm.Page.getControl('new_nt_comment5').setDisabled(false);
            Xrm.Page.getControl('new_p_review_result').setDisabled(false);
            Xrm.Page.getControl('new_ntxt_review_opinion').setDisabled(false);
        }

    }
     //결재 완료              : 그냥 결재 완료 버튼
    else if (state == 100000006) {

        html_tag = "<div style='float:left; padding-right:10px; margin:0px 0px 15px 0px;'><input id='print' type='button' value='보고서출력' style='width:86px; height:32px; background-color:#30B22C; border:none; color:white; cursor: pointer;'/></div>";

    }

    //반려                   : 반려
    else if (state == 100000099) {
        html_tag = "<div style='float:left; padding-right:10px; margin:0px 0px 15px 0px;'><input id='reject' type='button' value='반려상태' style='width:86px; height:32px; background-color:#30B22C; border:none; color:white; cursor: pointer;'/></div>";
    }

   
    else { }




    if (html_tag != null) {


        $("#new_request_response_btn_d").html(html_tag);
        Xrm.Page.ui.controls.get("new_request_response_btn").setVisible(true);
        
    }



    $('#print').click(function () {

        var url = "https://itrs.celltrion.com:447/WebResources/new_report";
        url += "?id=" + INTERNAL_CONTRACT_ID;
        window.open(url);

    })


    $("#accept_btn").click(function () {

        var a = Xrm.Page.getAttribute("new_p_request_status").getValue() + 1;
        Xrm.Page.getAttribute("new_p_request_status").setValue(a);
        Xrm.Page.getAttribute("new_accept_again").setValue(100000000);
      
        Xrm.Utility.alertDialog("승인이 요청이 완료 되었습니다");

        

        Xrm.Page.ui.controls.get("new_request_response_btn").setVisible(false);
        Xrm.Page.data.entity.save();
        OnLoad();

    })

    $("#accept_btn1").click(function () {
        
        var reject_op;
        
        if (INTERNAL_CONTRACT_STATE == 100000001) {
            reject_op = Xrm.Page.getAttribute("new_nt_comment").getValue();

            if (reject_op == null) {
                Xrm.Utility.alertDialog("결재의견이 입력되지 않았습니다. 입력후 다시 승인해 주시길 바랍니다.");
                return;
            }

            Xrm.Page.getAttribute("new_p_approver1").setValue(100000000);
            Xrm.Page.getAttribute("new_dt_approval1_usertime").setValue(Date.now());

        }
        else if (INTERNAL_CONTRACT_STATE == 100000002) {
            reject_op = Xrm.Page.getAttribute("new_nt_comment2").getValue();

            if (reject_op == null) {
                Xrm.Utility.alertDialog("결재의견이 입력되지 않았습니다. 입력후 다시 승인해 주시길 바랍니다.");
                return;
            }

            Xrm.Page.getAttribute("new_p_approver2").setValue(100000000);
            Xrm.Page.getAttribute("new_dt_approval2_usertime").setValue(Date.now());

        }

        else if (INTERNAL_CONTRACT_STATE == 100000003) {
            reject_op = Xrm.Page.getAttribute("new_nt_comment3").getValue();

            if (reject_op == null) {
                Xrm.Utility.alertDialog("결재의견이 입력되지 않았습니다. 입력후 다시 승인해 주시길 바랍니다.");
                return;
            }

            Xrm.Page.getAttribute("new_p_approver3").setValue(100000000);
            Xrm.Page.getAttribute("new_dt_approval3_usertime").setValue(Date.now());

        }

        else if (INTERNAL_CONTRACT_STATE == 100000004) {
            reject_op = Xrm.Page.getAttribute("new_nt_comment4").getValue();

            if (reject_op ==null) {
                Xrm.Utility.alertDialog("결재의견이 입력되지 않았습니다. 입력후 다시 승인해 주시길 바랍니다.");
                return;
            }

            Xrm.Page.getAttribute("new_p_approver4").setValue(100000000);
            Xrm.Page.getAttribute("new_dt_approval4_usertime").setValue(Date.now());

        }


        else if (INTERNAL_CONTRACT_STATE == 100000005) {
            reject_op = Xrm.Page.getAttribute("new_nt_comment5").getValue();

            if (reject_op == null) {
                Xrm.Utility.alertDialog("결재의견이 입력되지 않았습니다. 입력후 다시 승인해 주시길 바랍니다.");
                return;
            }

            Xrm.Page.getAttribute("new_p_approver5").setValue(100000000);
            Xrm.Page.getAttribute("new_dt_approval5").setValue(Date.now());

        }

        var a = Xrm.Page.getAttribute("new_p_request_status").getValue() + 1;
        Xrm.Page.getAttribute("new_p_request_status").setValue(a);
        Xrm.Page.getAttribute("new_accept_again").setValue(100000000);


        Xrm.Utility.alertDialog("승인이 완료 되었습니다");

        
        Xrm.Page.ui.controls.get("new_request_response_btn").setVisible(false);
        Xrm.Page.data.entity.save();
        OnLoad();

    })

    $("#save_btn").click(function () {
        Xrm.Page.data.entity.save();
        OnLoad();

    })

    //반려 어떻게 짤지는 논의.
    $("#reject_btn").click(function () {

    //    현재 상태를 보고 
    //    working 어떻게 할지 고민
        
        if (INTERNAL_CONTRACT_STATE == 100000003 || 
            INTERNAL_CONTRACT_STATE == 100000004 ||
            INTERNAL_CONTRACT_STATE == 100000005) {
            setupLawRejectState(INTERNAL_CONTRACT_STATE);
        
           
        }

        else if (INTERNAL_CONTRACT_STATE == 100000002) {
            setupRejectState();
           
        }

        else if (INTERNAL_CONTRACT_STATE == 100000001) {
            setupRejectBeforeState(INTERNAL_CONTRACT_STATE);
           
        }
        
       
        Xrm.Page.ui.controls.get("new_request_response_btn").setVisible(false);
        Xrm.Page.data.entity.save();
        OnLoad();
    })
    
}

/**
 * 작성중 - 100000000
 * 요청자팀장결재 - 100000001
 * 법무실무자결재 - 100000002
 * 법무팀장결재 - 100000003
 * 법무담당결재 - 100000004
 * 법무담당결재 - 100000004
 * 검토완료 - 100000006
 * 법무본부결재 - 100000005
 * 취소 - 100000099
 *
 * */

////3 법무팀 법무실무자에게 반려
function setupLawRejectState(state) {
   

    if (state == 100000003) {
        var reject_op = Xrm.Page.getAttribute("new_nt_comment3").getValue();
        if (reject_op == null) {
            Xrm.Utility.alertDialog("결재의견이 입력되지 않았습니다. 입력후 다시 반려해 주시길 바랍니다.");
            return;
        }

    }

    else if (state == 100000004) {
        var reject_op = Xrm.Page.getAttribute("new_nt_comment4").getValue();
        if (reject_op == null) {
            Xrm.Utility.alertDialog("결재의견이 입력되지 않았습니다. 입력후 다시 반려해 주시길 바랍니다.");
            return;
        }

    }
    
    else if (state == 100000005) {
        var reject_op = Xrm.Page.getAttribute("new_nt_comment5").getValue();
        if (reject_op == null) {
            Xrm.Utility.alertDialog("결재의견이 입력되지 않았습니다. 입력후 다시 반려해 주시길 바랍니다.");
            return;
        }

    }
    Xrm.Page.getAttribute("new_p_request_status").setValue(100000002);
    Xrm.Page.getAttribute("new_accept_again").setValue(100000001);

    Xrm.Page.getAttribute("new_p_approver2").setValue(null);
    Xrm.Page.getAttribute("new_p_approver3").setValue(null);
    Xrm.Page.getAttribute("new_p_approver4").setValue(null);
    Xrm.Page.getAttribute("new_p_approver5").setValue(null);
    Xrm.Utility.alertDialog("법무 반려 요청이 완료 되었습니다");

}
//2 법무 실무자 아예 취소 
function setupRejectState() {

    if (state == 100000002) {
        var law_op = Xrm.Page.getAttribute("new_ntxt_review_opinion").getValue();
        var reject_op = Xrm.Page.getAttribute("new_nt_comment2").getValue();

        if (law_op.toString() == null) {
            Xrm.Utility.alertDialog("법무팀의견이 입력되지 않았습니다. 입력후 다시 반려해 주시길 바랍니다.");
            return;
        }
        if (reject_op == null) {
            Xrm.Utility.alertDialog("결재의견이 입력되지 않았습니다. 입력후 다시 반려해 주시길 바랍니다.");
            return;
        }

    }

    Xrm.Page.getAttribute("new_p_request_status").setValue(100000099);
    Xrm.Utility.alertDialog("반려 요청이 완료 되었습니다");
}
//1 팀장이 요청자에게 반려 
function setupRejectBeforeState(state) {

    if (state == 100000001) {
      
        var reject_op = Xrm.Page.getAttribute("new_nt_comment").getValue();

        if (reject_op == null) {
            Xrm.Utility.alertDialog("결재의견이 입력되지 않았습니다. 입력후 다시 반려해 주시길 바랍니다.");
            return;
        }

    }

    Xrm.Page.getAttribute("new_p_request_status").setValue(100000000);
    Xrm.Page.getAttribute("new_accept_again").setValue(100000001);
    Xrm.Page.getAttribute("new_p_approver1").setValue(100000001);
    Xrm.Utility.alertDialog("반려 요청이 완료 되었습니다");
}

function nullEqualCheck(a, b) {

    if (a != null && b != null) {

        if (a.toString() == b.toString()) {
            return true;
        }

    }
    return false;
}


function retrieveRequestUserEntity() {
    var clientURL = Xrm.Page.context.getClientUrl();
    var req = new XMLHttpRequest();
    var requestID = Xrm.Page.getAttribute("ownerid").getValue()[0].id;
    requestID = requestID.replace("{", "").replace("}", "");
    var query = "/api/data/v8.2/systemusers?$select=_new_l_department_value&$filter=systemuserid eq " + requestID;

    req.open("GET", encodeURI(clientURL + query), true);
    req.setRequestHeader("Accept", "application/json");
    req.setRequestHeader("Content-Type", "application/json;charset=utf-8");
    req.setRequestHeader("OData-MaxVersion", "4.0");
    req.setRequestHeader("OData-Version", "4.0");


    req.onreadystatechange = function () {
        if (this.readyState == 4) {
            req.onreadystatechange = null;
            if (this.status == 200) {
                var data = JSON.parse(this.response);
                if (true) {
                    REQUEST_USER = data;
                    retrieveLoginUserEntity();
                }
            }
            else {
                var error = JSON.parse(this.response).error;
                alert("Error retrieving Record ? " + error.message);
            }
        }
    };
    req.send();


}

function retrieveLoginUserEntity() {
    var clientURL = Xrm.Page.context.getClientUrl();
    var req = new XMLHttpRequest();
    var loginuserID = Xrm.Page.context.getUserId();
    loginuserID = loginuserID.replace("{", "");
    loginuserID = loginuserID.replace("}", "");
    var query = "/api/data/v8.2/systemusers?$select=_new_l_department_value&$filter=systemuserid eq " +  loginuserID ;

    req.open("GET", encodeURI(clientURL + query), true);
    req.setRequestHeader("Accept", "application/json");
    req.setRequestHeader("Content-Type", "application/json;charset=utf-8");
    req.setRequestHeader("OData-MaxVersion", "4.0");
    req.setRequestHeader("OData-Version", "4.0");


    req.onreadystatechange = function () {
        if (this.readyState == 4) {
            req.onreadystatechange = null;
            if (this.status == 200) {
                var data = JSON.parse(this.response);
                if (true) {
                    LOGIN_USER = data;
                    retrieveRequestUserDepartMentEntity();
                }
            }
            else {
                var error = JSON.parse(this.response).error;
                alert("Error retrieving Record ? " + error.message);
            }
        }
    };
    req.send();


}

function retrieveRequestUserDepartMentEntity() {
    var clientURL = Xrm.Page.context.getClientUrl();
    var req = new XMLHttpRequest();
    

    var requestID = REQUEST_USER.value[0]._new_l_department_value;
    requestID = requestID.replace("{", "");
    requestID = requestID.replace("}", "");
    var query = "/api/data/v8.2/new_departments?$filter=new_departmentid eq " + requestID;

    req.open("GET", encodeURI(clientURL + query), true);
    req.setRequestHeader("Accept", "application/json");
    req.setRequestHeader("Content-Type", "application/json;charset=utf-8");
    req.setRequestHeader("OData-MaxVersion", "4.0");
    req.setRequestHeader("OData-Version", "4.0");


    req.onreadystatechange = function () {
        if (this.readyState == 4) {
            req.onreadystatechange = null;
            if (this.status == 200) {
                var data = JSON.parse(this.response);
                if (true) {
                    REQUEST_USER_DEPART = data;
                    retrieveLoginUserDepartMentEntity();

                }
            }
            else {
                var error = JSON.parse(this.response).error;
                alert("Error retrieving Record ? " + error.message);
            }
        }
    };
    req.send();


}



function retrieveLoginUserDepartMentEntity() {
    var clientURL = Xrm.Page.context.getClientUrl();
    var req = new XMLHttpRequest();

   
    var loginID = LOGIN_USER.value[0]._new_l_department_value;
    loginID = loginID.replace("{", "");
    loginID = loginID.replace("}", "");
    var query = "/api/data/v8.2/new_departments?$filter=new_departmentid eq " + loginID;

    req.open("GET", encodeURI(clientURL + query), true);
    req.setRequestHeader("Accept", "application/json");
    req.setRequestHeader("Content-Type", "application/json;charset=utf-8");
    req.setRequestHeader("OData-MaxVersion", "4.0");
    req.setRequestHeader("OData-Version", "4.0");


    req.onreadystatechange = function () {
        if (this.readyState == 4) {
            req.onreadystatechange = null;
            if (this.status == 200) {
                var data = JSON.parse(this.response);
                if (true) { 
                    LOGIN_USER_DEPART = data;
                    retrieveLawChargeEntity();
                }
            }
            else {
                var error = JSON.parse(this.response).error;
                alert("Error retrieving Record ? " + error.message);
            }
        }
    };
    req.send();


}

function retrieveLawChargeEntity() {
    var clientURL = Xrm.Page.context.getClientUrl();
    var req = new XMLHttpRequest();

    var LAW_CHARGE_ID = "296fff90-85cf-e911-80e5-00155d012e07";

    //부서 이름이 법무 담당인 것을 가져옴. 
    var query = "/api/data/v8.2/new_departments?$filter=new_departmentid eq " + LAW_CHARGE_ID;

    req.open("GET", encodeURI(clientURL + query), true);
    req.setRequestHeader("Accept", "application/json");
    req.setRequestHeader("Content-Type", "application/json;charset=utf-8");
    req.setRequestHeader("OData-MaxVersion", "4.0");
    req.setRequestHeader("OData-Version", "4.0");


    req.onreadystatechange = function () {
        if (this.readyState == 4) {
            req.onreadystatechange = null;
            if (this.status == 200) {
                var data = JSON.parse(this.response);
                if (true) {
                    LAW_CHARGE = data;
                    retrieveLawMasterEntity();
                }
            }
            else {
                var error = JSON.parse(this.response).error;
                alert("Error retrieving Record ? " + error.message);
            }
        }
    };
    req.send();


}



function retrieveLawMasterEntity() {
    var clientURL = Xrm.Page.context.getClientUrl();
    var req = new XMLHttpRequest();

    var LAW_MASTER_ID = "18609fe6-e6ee-e911-80e5-00155d012e07";
    
    //부서 이름이 법무 본부인 것을 가져옴. 
    var query = "/api/data/v8.2/new_departments?$filter=new_departmentid eq " + LAW_MASTER_ID;

    req.open("GET", encodeURI(clientURL + query), true);
    req.setRequestHeader("Accept", "application/json");
    req.setRequestHeader("Content-Type", "application/json;charset=utf-8");
    req.setRequestHeader("OData-MaxVersion", "4.0");
    req.setRequestHeader("OData-Version", "4.0");


    req.onreadystatechange = function () {
        if (this.readyState == 4) {
            req.onreadystatechange = null;
            if (this.status == 200) {
                var data = JSON.parse(this.response);
                if (true) {
                    LAW__MASTER = data;
                    userCheck(INTERNAL_CONTRACT_STATE);
                }
            }
            else {
                var error = JSON.parse(this.response).error;
                alert("Error retrieving Record ? " + error.message);
            }
        }
    };
    req.send();


}
