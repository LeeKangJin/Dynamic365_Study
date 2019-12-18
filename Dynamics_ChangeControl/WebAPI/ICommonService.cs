using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using CELLAPI.Entities.CommonClass;

namespace CELLAPI.Service
{
    // 참고: "리팩터링" 메뉴에서 "이름 바꾸기" 명령을 사용하여 코드 및 config 파일에서 인터페이스 이름 "ICommonService"을 변경할 수 있습니다.
    [ServiceContract]
    public interface ICommonService
    {
        [WebInvoke(UriTemplate = "TestService", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string TestService();

        [WebInvoke(UriTemplate = "INSERT_HRDATA", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        DefaultReturn INSERT_HRDATA(HRInput input);
        

        [WebInvoke(UriTemplate = "GET_AD_LOGIN", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        OUT_AD_LOGIN GET_AD_LOGIN(IN_AD_LOGIN param);

        [WebInvoke(UriTemplate = "Get_AD_TRACE_LIST", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        OUT_AD_TRACE Get_AD_TRACE_LIST(IN_AD_TRACE param);

        [WebInvoke(UriTemplate = "SEND_EMAIL_MSSQL", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        DefaultReturn SEND_EMAIL_MSSQL(IN_SEND_EMAIL_MSSQL param);

        [WebInvoke(UriTemplate = "GET_CURRENT_TIME", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string GET_CURRENT_TIME();

        //[WebInvoke(UriTemplate = "GET_CURRENT_TIME", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //string GET_CURRENT_TIME(string nation);
    }
}

