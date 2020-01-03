var $ = parent.$;

function setCookie(name, value, expiredays) {
    var todayDate = new Date(); //현재시간 구하고 
    todayDate.setDate(todayDate.getDate() + expiredays); // 기간 설정하고 
    document.cookie = name + "=" + escape(value) + "; path=/; expires=" + todayDate.toGMTString() + ";";  //쿠키 설정값    
}

function getCookie(name) {
    var nameOfCookie = name + "=";
    var x = 0;
    while (x <= document.cookie.length) {
        var y = (x + nameOfCookie.length);
        if (document.cookie.substring(x, y) == nameOfCookie) {
            if ((endOfCookie = document.cookie.indexOf(";", y)) == -1)
                endOfCookie = document.cookie.length;
            return unescape(document.cookie.substring(y, endOfCookie));
        }
        x = document.cookie.indexOf(" ", x) + 1;
        if (x == 0)
            break;
    }
    return "";
}


	var gColor = "";
	var colorstr = "";
	colorstr += "<div class='colorBox' style='color:#8C8C8C;float:left;cursor:pointer;'>■</div>";
	colorstr += "<div class='colorBox' style='color:#5D5D5D;float:left;cursor:pointer;'>■</div>";
	colorstr += "<div class='colorBox' style='color:#002266;float:left;cursor:pointer;'>■</div>";
	colorstr += "<div class='colorBox' style='color:#000042;float:left;cursor:pointer;'>■</div>";
	//colorstr += "<div class='colorBox' style='color:#B2CCFF;float:left;cursor:pointer;'>■</div>";
	$(".ms-crm-inlineheader-content").append(colorstr);


function SkinLoad() {

    //탭
    gColor = getCookie("crmformcolor");
    if (gColor == null || gColor == "") {
        gColor = "#eaeaea";
    }
    $("div.ms-crm-InlineTab-Read").each(function () {
        if ($(this).attr("id") != "") {
            $(this).css("border", "1px solid "+gColor);
            $(this).css("width", "98%");
        }
    });
    
    $(".colorBox").click(function () {
        setCookie("crmformcolor",$(this).css("color"),99);
        SkinLoad();
    });

    //탭제목
    $(".ms-crm-InlineTabHeader").css("background-color", gColor);	
	$("h2.ms-crm-Form").css("color", "#FFFFFF");
	$("h2.ms-crm-Form").css("font-family", "Malgun Gothic");

	if(gColor == "#eaeaea")
		$("h2.ms-crm-Form").css("color", "#000000");

    //섹션
    $(".ms-crm-FormSection-Container").css("margin-left", "10px");

    //필드(레이블)
    $("td.ms-crm-ReadField-Normal").each(function () {
        if ($(this).text() != "") {
            $(this).css("border-bottom", "1px solid " + gColor);
			$(this).css("background-color", "#eaeaea");
			$(this).css("padding-left", "5px");
        }
    });


    //필드(값)
    $("td.ms-crm-Field-Data-Print").each(function () {
        if ($(this).text() != "") {
            $(this).css("border-bottom", "1px solid "+gColor);
        }
    });

    //그리드
	$(".ms-crm-absolutePosition").css("border-top", "1px solid " + gColor);
    $(".ms-crm-absolutePosition").css("border-bottom", "1px solid " + gColor);
    $(".ms-crm-absolutePosition").css("border-left", "1px solid " + gColor);
    $(".ms-crm-absolutePosition").css("border-right", "1px solid " + gColor);
    $(".ms-crm-absolutePosition").css("margin-bottom", "10px");

    //버튼 하단의 CSS만 제거
    //일단은 다른거 우선 하고 오자. 
    //$("table[name={16cb692a-5f8a-4afd-948a-a8da791d3bc2}_section_15].tr.td.ms-crm-Field-Data-Print ms-crm-Inline-DynamicGutter").css("border-bottom", "10px solid " + gColor);

    //var childTag = $("{a94226d0-d13c-5c21-12e0-2726611abb77}").children('.ms-crm-Field-Data-Print ms-crm-Inline-DynamicGutter');

    //  childTag.css("border-bottom", "10px solid " + gColor);

}