using System;
using System.Collections.Generic;
using static RestfulTest.Common;

namespace RestfulTest
{
    /*
    kangjin Lee 2019-12-20
    */

    //Select Multiple Data 
    public class OUTRequest
    {
        public DefaultReturn defaultReturn { get; set; }
        public List<HRInput> List { get; set; }

        public OUTRequest()
        {
            defaultReturn = new DefaultReturn();
            List = new List<HRInput>();
        }

    }

    public class HROutput
    {

        public string DN_Code { get; set; }         // 소속회사
        public string UR_Code { get; set; }         // 사번
        public string CompanyID { get; set; }       // ID
        public string CompanyPW { get; set; }       // PW
        public string DisplayName_KR { get; set; }  //이름

        public HROutput()
        {
            DN_Code = "";
            UR_Code = "";
            CompanyID = "";
            CompanyPW = "";
            DisplayName_KR = "";
        }


    }


    //Insert Data
    public class HRInput
    {


        public string UR_Code { get; set; }
        public string DN_Code { get; set; }
        public string GR_Code { get; set; }
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

        public string PhoneNumberInter { get; set; }
        public string AD_Mobile { get; set; }


        public string ProcessYN { get; set; }
        public string Personal_Info_YN { get; set; }

        public string CompanyID { get; set; }
        public string CompanyPW { get; set; }

        public HRInput()
        {
            UR_Code = "";
            DN_Code = "";
            GR_Code = "";
            DisplayName_KR = "";
            DisplayName_EN = "";

            JobTitleCode = "";
            JobLevelCode = "";

            SortKey = "1";
            EnterDate = "";

            RetireDate = "";
            BirthDiv = "";

            BirthDate = "";
            MailAddress = "";
            PhoneNumberInter = "";
            AD_Mobile = "";


            ProcessYN = "";
            Personal_Info_YN = "";
            CompanyID = "";
            CompanyPW = "";


        }
    }
}
