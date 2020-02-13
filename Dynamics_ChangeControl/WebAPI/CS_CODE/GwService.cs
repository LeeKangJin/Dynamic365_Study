using CELLAPI.Entities.CommonClass;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.DirectoryServices.Protocols;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;

using Newtonsoft.Json;
using Microsoft.Xrm.Sdk;
using System.Data.SqlClient;

// LOCAL 에서 API 호출해 볼 것

namespace CELLAPI.Service
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    // 참고: "리팩터링" 메뉴에서 "이름 바꾸기" 명령을 사용하여 코드 및 config 파일에서 클래스 이름 "CommonService"을 변경할 수 있습니다.
    public class GwService : IGwService
    {

        public string TestService()
        {

            return "Hellow";

        }

        public DefaultReturn INSERT_HRDATA(HRInput input)
        {
            // INPUTPARM Class 만들것. 
            DefaultReturn defaultReturn = new DefaultReturn();

            /* Mendatory filed
                   CONFIG FILE 에서 정보 가져오기 
            */
            
            if (input.UR_Code.Trim() == "")
            {
                defaultReturn.MSG = "로그인 계정이 입력되지 않았습니다.";
                defaultReturn.RESULT = false;
            }

            if (input.EmpNo.Trim() == "")
            {
                defaultReturn.MSG = "사번이 입력되지 않았습니다.";
                defaultReturn.RESULT = false;
            }


            if (input.DN_Code.Trim() == "")
            {
                defaultReturn.MSG = "소속회사가 입력되지 않았습니다.";
                defaultReturn.RESULT = false;
            }

            if (input.GR_Code.Trim() == "")
            {
                defaultReturn.MSG = "소속부서가 입력되지 않았습니다.";
                defaultReturn.RESULT = false;
            }

            if (input.DisplayName_KR.Trim() == "")
            {
                defaultReturn.MSG = "사용자 이름이 입력되지 않았습니다.";
                defaultReturn.RESULT = false;
            }
            if (input.DisplayName_EN.Trim() == "")
            {
                defaultReturn.MSG = "사용자 이름(영문)이 입력되지 않았습니다.";
                defaultReturn.RESULT = false;
            }
            DateTime dt_temp = new DateTime();

            if (!string.IsNullOrEmpty(input.EnterDate) && !DateTime.TryParse(input.EnterDate, out dt_temp))
            {
                defaultReturn.MSG = "입사일자의 날짜 형식이 잘못되었습니다.";
                defaultReturn.RESULT = false;
            }

            if (!string.IsNullOrEmpty(input.RetireDate) && !DateTime.TryParse(input.RetireDate, out dt_temp))
            {
                defaultReturn.MSG = "퇴사일자의 날짜 형식이 잘못되었습니다.";
                defaultReturn.RESULT = false;
            }

            if (!string.IsNullOrEmpty(input.BirthDate) && !DateTime.TryParse(input.BirthDate, out dt_temp))
            {
                defaultReturn.MSG = "생년월일의 날짜 형식이 잘못되었습니다.";
                defaultReturn.RESULT = false;
            }



            if (input.Personal_Info_YN.Trim() == "")
            {
                defaultReturn.MSG = "개인정보 동의를 입력하지 않았습니다.";
                defaultReturn.RESULT = false;
            }
            
                string[] domain_word = input.MailAddress.Split('@');
                string domain = domain_word[1];
            
            if (domain == ConfigurationManager.AppSettings["celltrion_skincure_DOMAIN"]) {
                if (input.EmpNo == input.UR_Code) {
                    // 스킨큐어는 사번정책이 셀트리온과 동일해서 아래와 같은 예외처리
                    defaultReturn.MSG = "스킨큐어는 사번과 로그인 계정이 달라야합니다.";
                    defaultReturn.RESULT = false;
                }

            }
            else {
                if (input.EmpNo != input.UR_Code)
                {
                    defaultReturn.MSG = "사번과 로그인 계정이 일치하지 않습니다.";
                    defaultReturn.RESULT = false;
                }
            }
            // 필수값 체크 실패 및 스킨큐어 UR_CODE값 에러시 리턴
            if (defaultReturn.RESULT == false)
            {
                return defaultReturn;
            }
            //그룹사 로그인 ID & PW & Domain 체크 실패시 리턴
            defaultReturn = Common.CompanyInfoChecking(input.DN_Code.Trim(), input.CompanyID.Trim(), input.CompanyPW.Trim(), domain.Trim());


            if (defaultReturn.RESULT == false)
            {
                return defaultReturn;
            }
            try
            {
                SqlParameter[] param = new SqlParameter[] {

                  new SqlParameter("@UR_Code", SqlDbType.VarChar, 50)
                  {
                      Value = input.UR_Code
                  }
                  , new SqlParameter("@EmpNo", SqlDbType.VarChar, 50)
                  {
                      Value = input.EmpNo
                  }
                  ,new SqlParameter("@DN_Code", SqlDbType.VarChar, 50)
                  {
                      Value = input.DN_Code
                  }
                   ,new SqlParameter("@GR_Code", SqlDbType.VarChar, 50)
                  {
                      Value = input.GR_Code
                  }
                   ,new SqlParameter("@DisplayName_KR", SqlDbType.NVarChar, 100)
                  {
                      Value = input.DisplayName_KR
                  }
                  ,new SqlParameter("@DisplayName_EN", SqlDbType.NVarChar, 100)
                  {
                      Value = input.DisplayName_EN
                  }
                  ,new SqlParameter("@JobTitleCode", SqlDbType.VarChar, 50)
                  {
                      Value = input.JobTitleCode
                  }
                  ,new SqlParameter("@JobLevelCode", SqlDbType.VarChar, 50)
                  {
                      Value = input.JobLevelCode
                  }
                  ,new SqlParameter("@SortKey", SqlDbType.VarChar, 200)
                  {
                      Value = input.SortKey
                  }
                  ,new SqlParameter("@EnterDate", SqlDbType.VarChar, 10)
                  {
                      Value = input.EnterDate
                  }
                  ,new SqlParameter("@RetireDate", SqlDbType.VarChar, 10)
                   {
                      Value = input.RetireDate
                   }
                  ,new SqlParameter("@BirthDiv", SqlDbType.VarChar, 10)
                  {
                      Value = input.BirthDiv
                  }
                  ,new SqlParameter("@BirthDate", SqlDbType.VarChar, 10)
                  {
                      Value = input.BirthDate
                  }
                  ,new SqlParameter("@MailAddress", SqlDbType.VarChar, 100)
                  {
                      Value = input.MailAddress
                  }
                  ,new SqlParameter("@PhoneNumberInter", SqlDbType.VarChar, 50)
                  {
                     Value = input.PhoneNumberInter
                  }
                  ,new SqlParameter("@AD_Mobile", SqlDbType.VarChar, 50)
                  {
                      Value = input.AD_Mobile
                  }
                  ,new SqlParameter("@Personal_Info_YN", SqlDbType.Char, 1)
                  {
                      Value = input.Personal_Info_YN
                  }


            };

                //개발용
                DataSet ds = Common.ReturnDataset_proc("HR_DATA_INSERT_DEV", param, "COVI_SYNCDATA");
                //운영용
                //DataSet ds = Common.ReturnDataset_proc("HR_DATA_INSERT", param, "COVI_SYNCDATA");



                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    defaultReturn.RESULT = Convert.ToBoolean(ds.Tables[0].Rows[0]["RESULT"]); //프로시져 0,1 교환 To Do
                    defaultReturn.MSG = ds.Tables[0].Rows[0]["MSG"].ToString();
                }
                else
                {
                    defaultReturn.RESULT = false;
                    defaultReturn.MSG = "DB Insert 실패 하였습니다.";
                }

            }
            catch (Exception ex)
            {
                defaultReturn.RESULT = false;
                defaultReturn.MSG = ex.Message;
            }

            return defaultReturn;

        }

        public OUTRequest SEARCH_HRDATA(HRSearchParam input)
        {

            OUTRequest retReq = new OUTRequest();

            try
            {
            
                retReq.defaultReturn = Common.CompanyInfoChecking(input.DN_Code.Trim(), input.CompanyID.Trim(), input.CompanyPW.Trim());
         
                if (retReq.defaultReturn.RESULT)
                {

                    SqlParameter[] param = new SqlParameter[] {
                        // -1 = NVarChar MAX
                      new SqlParameter("@SEARCH_TEXT", SqlDbType.NVarChar,200)
                      {
                          Value = input.Search_Text
                      }
                      , new SqlParameter("@DN_Code", SqlDbType.NVarChar,50)
                      {
                          Value = input.DN_Code
                      }


                  };
                    //개발
                    DataSet ds = Common.ReturnDataset_proc("HR_DATA_SELECT_DEV", param, "COVI_SYNCDATA");
                    //운영
                    //DataSet ds = Common.ReturnDataset_proc("HR_DATA_SELECT", param, "COVI_SYNCDATA");

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        HRInput ret = new HRInput();
                        ret.UR_Code = !string.IsNullOrEmpty(dr["UR_Code"].ToString()) ? dr["UR_Code"].ToString() : "";
                        ret.EmpNo = !string.IsNullOrEmpty(dr["EmpNo"].ToString()) ? dr["EmpNo"].ToString() : "";
                        ret.DN_Code = !string.IsNullOrEmpty(dr["DN_Code"].ToString()) ? dr["DN_Code"].ToString() : "";
                        ret.GR_Code = !string.IsNullOrEmpty(dr["GR_Code"].ToString()) ? dr["GR_Code"].ToString() : "";
                        ret.DisplayName_KR = !string.IsNullOrEmpty(dr["DisplayName"].ToString()) ? dr["DisplayName"].ToString() : "";
                        ret.DisplayName_EN = !string.IsNullOrEmpty(dr["ExDisplayName"].ToString()) ? dr["ExDisplayName"].ToString() : "";
                        ret.JobTitleCode = !string.IsNullOrEmpty(dr["JobTitleCode"].ToString()) ? dr["JobTitleCode"].ToString() : "";
                        ret.JobLevelCode = !string.IsNullOrEmpty(dr["JobLevelCode"].ToString()) ? dr["JobLevelCode"].ToString() : "";
                        ret.SortKey = !string.IsNullOrEmpty(dr["SortKey"].ToString()) ? dr["SortKey"].ToString() : "";
                        ret.EnterDate = !string.IsNullOrEmpty(dr["EnterDate"].ToString()) ? dr["EnterDate"].ToString() : "";
                        ret.RetireDate = !string.IsNullOrEmpty(dr["RetireDate"].ToString()) ? dr["RetireDate"].ToString() : "";
                        ret.BirthDiv = !string.IsNullOrEmpty(dr["BirthDiv"].ToString()) ? dr["BirthDiv"].ToString() : "";
                        ret.BirthDate = !string.IsNullOrEmpty(dr["BirthDate"].ToString()) ? dr["BirthDate"].ToString() : "";
                        ret.MailAddress = !string.IsNullOrEmpty(dr["EmailAddress"].ToString()) ? dr["EmailAddress"].ToString() : "";
                        ret.PhoneNumberInter = !string.IsNullOrEmpty(dr["PhoneNumberInter"].ToString()) ? dr["PhoneNumberInter"].ToString() : "";
                        ret.AD_Mobile = !string.IsNullOrEmpty(dr["AD_Mobile"].ToString()) ? dr["AD_Mobile"].ToString() : "";
                        ret.ProcessYN = !string.IsNullOrEmpty(dr["ProcessYN"].ToString()) ? dr["ProcessYN"].ToString() : "";
                        ret.Personal_Info_YN = !string.IsNullOrEmpty(dr["Personal_Info_YN"].ToString()) ? dr["Personal_Info_YN"].ToString() : "";
                        ret.ProcessComplete = !string.IsNullOrEmpty(dr["ProcessComplete"].ToString()) ? dr["ProcessComplete"].ToString() : "";
                        retReq.List.Add(ret);
                  
                    }
                    // 실패시 catch에 걸림. 
                    retReq.defaultReturn.RESULT = true;
                    retReq.defaultReturn.MSG = "DB Search 성공하였습니다." ;

                }
            }
            catch (Exception ex)
            {
                retReq.defaultReturn.RESULT = false;
                retReq.defaultReturn.MSG = "DB Search 실패하였습니다. 원인: " + ex.Message;
            }

            return retReq;


        }

        public OUTRequest SEARCH_LOG_HRDATA(HRSearchParam input)
        {

            OUTRequest retReq = new OUTRequest();

            try
            {

                retReq.defaultReturn = Common.CompanyInfoChecking(input.DN_Code.Trim(), input.CompanyID.Trim(), input.CompanyPW.Trim());

                if (retReq.defaultReturn.RESULT)
                {

                    SqlParameter[] param = new SqlParameter[] {
                        // -1 = NVarChar MAX
                      new SqlParameter("@SEARCH_TEXT", SqlDbType.NVarChar,200)
                      {
                          Value = input.Search_Text
                      }
                      , new SqlParameter("@DN_Code", SqlDbType.NVarChar,50)
                      {
                          Value = input.DN_Code
                      }


                  };
                    
                    //개발
                    DataSet ds = Common.ReturnDataset_proc("HR_DATA_LOG_SELECT_DEV", param, "COVI_SYNCDATA");
                    //운영
                    //DataSet ds = Common.ReturnDataset_proc("HR_DATA_LOG_SELECT", param, "COVI_SYNCDATA");

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        
                        HRLogOut ret = new HRLogOut();

                        //To Do 2020 02 13
                        /*
                        ret.UR_Code = !string.IsNullOrEmpty(dr["UR_Code"].ToString()) ? dr["UR_Code"].ToString() : "";
                        ret.DN_Code = !string.IsNullOrEmpty(dr["DN_Code"].ToString()) ? dr["DN_Code"].ToString() : "";
                        ret.GR_Code = !string.IsNullOrEmpty(dr["GR_Code"].ToString()) ? dr["GR_Code"].ToString() : "";
                        ret.DisplayName_KR = !string.IsNullOrEmpty(dr["DisplayName"].ToString()) ? dr["DisplayName"].ToString() : "";
                        ret.DisplayName_EN = !string.IsNullOrEmpty(dr["ExDisplayName"].ToString()) ? dr["ExDisplayName"].ToString() : "";
                        ret.JobTitleCode = !string.IsNullOrEmpty(dr["JobTitleCode"].ToString()) ? dr["JobTitleCode"].ToString() : "";
                        ret.JobLevelCode = !string.IsNullOrEmpty(dr["JobLevelCode"].ToString()) ? dr["JobLevelCode"].ToString() : "";
                        ret.SortKey = !string.IsNullOrEmpty(dr["SortKey"].ToString()) ? dr["SortKey"].ToString() : "";
                        ret.EnterDate = !string.IsNullOrEmpty(dr["EnterDate"].ToString()) ? dr["EnterDate"].ToString() : "";
                        ret.RetireDate = !string.IsNullOrEmpty(dr["RetireDate"].ToString()) ? dr["RetireDate"].ToString() : "";
                        ret.BirthDiv = !string.IsNullOrEmpty(dr["BirthDiv"].ToString()) ? dr["BirthDiv"].ToString() : "";
                        ret.BirthDate = !string.IsNullOrEmpty(dr["BirthDate"].ToString()) ? dr["BirthDate"].ToString() : "";
                        ret.MailAddress = !string.IsNullOrEmpty(dr["EmailAddress"].ToString()) ? dr["EmailAddress"].ToString() : "";
                        ret.PhoneNumberInter = !string.IsNullOrEmpty(dr["PhoneNumberInter"].ToString()) ? dr["PhoneNumberInter"].ToString() : "";
                        ret.AD_Mobile = !string.IsNullOrEmpty(dr["AD_Mobile"].ToString()) ? dr["AD_Mobile"].ToString() : "";
                        ret.ProcessYN = !string.IsNullOrEmpty(dr["ProcessYN"].ToString()) ? dr["ProcessYN"].ToString() : "";
                        ret.Personal_Info_YN = !string.IsNullOrEmpty(dr["Personal_Info_YN"].ToString()) ? dr["Personal_Info_YN"].ToString() : "";
                        ret.ProcessComplete = !string.IsNullOrEmpty(dr["ProcessComplete"].ToString()) ? dr["ProcessComplete"].ToString() : "";
                        retReq.List.Add(ret);
                        */
                    }
                    // 실패시 catch에 걸림. 
                    retReq.defaultReturn.RESULT = true;
                    retReq.defaultReturn.MSG = "DB Search 성공하였습니다.";

                }
            }
            catch (Exception ex)
            {
                retReq.defaultReturn.RESULT = false;
                retReq.defaultReturn.MSG = "DB Search 실패하였습니다. 원인: " + ex.Message;
            }

            return retReq;


        }


    }
}
