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

    public interface IGwService
    {
        [WebInvoke(UriTemplate = "TestService", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string TestService();

        [WebInvoke(UriTemplate = "INSERT_HRDATA", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        DefaultReturn INSERT_HRDATA(HRInput input);

        [WebInvoke(UriTemplate = "SEARCH_HRDATA", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        OUTRequest SEARCH_HRDATA(HRSearchParam input);

        [WebInvoke(UriTemplate = "SEARCH_LOG_HRDATA", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        OUTRequest SEARCH_LOG_HRDATA(HRSearchParam input);

    }
}

