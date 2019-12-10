using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestfulTest
{
    public class ServiceClass
    {
    }
    public class InputGetDate
    {
        public string UR_Code { get; set; }
        public string DN_Code { get; set; }
        public string GR_Code { get; set; }
        // public string LoginID { get; set; } -- 사번과 일치
        // public string EmpNo { get; set; }-- 사번과 일치
        public string DisplayName_KR { get; set; }
        public string DisplayName_EN { get; set; }

        /*
            CAUTION : 만약 다국어 이름이 추가 될시 코드 추가 해야 함. 
        */

        public string JobTitleCode { get; set; }
        public string JobLevelCode { get; set; }

        public string SortKey { get; set; }
        public string EnterDate { get; set; }

        public string RetireDate { get; set; }
        public string BirthDiv { get; set; }

        public string BirthDate { get; set; }
        public string MailAddress { get; set; }

        public string AdIsUse { get; set; }

        public string PhoneNumberInter { get; set; }
        public string AD_Mobile { get; set; }


        public string ProcessYN { get; set; }
        public string Personal_Info_YN { get; set; }
    }
}