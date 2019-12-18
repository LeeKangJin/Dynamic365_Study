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
    public class CommonService : ICommonService
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
         

            if (defaultReturn.RESULT == false)
            {
                return defaultReturn;
            }

            if (input.UR_Code.Trim() == "") {
                defaultReturn.MSG  = "사원번호가 입력되지 않았습니다.";
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
                defaultReturn.MSG = "생년월일 날짜 형식이 잘못되었습니다.";
                defaultReturn.RESULT = false;
            }



            if (input.Personal_Info_YN.Trim() == "")
            {
                defaultReturn.MSG = "개인정보 동의를 입력하지 않았습니다.";
                defaultReturn.RESULT = false;
            }



            // 필수값 체크 실패시 리턴
            if (defaultReturn.RESULT == false)
            {
                return defaultReturn;
            }

            string[] domain_word = input.MailAddress.Split('@');
            string domain = domain_word[1];

            //그룹사 로그인 ID & PW 체크 실패시 리턴
            defaultReturn = Common.CompanyInfoChecking(input.DN_Code.Trim(), input.CompanyID.Trim(), input.CompanyPW.Trim(),domain.Trim());




            if (defaultReturn.RESULT == false) {
                return defaultReturn;
            }


            try
            {

                SqlParameter[] param = new SqlParameter[] {

                  new SqlParameter("@UR_Code", SqlDbType.VarChar, 50)
                  {
                      Value = input.UR_Code
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

                DataSet ds = Common.ReturnDataset_proc("HR_DATA_INSERT", param, "COVI_SYNCDATA");



                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    defaultReturn.RESULT = Convert.ToBoolean(ds.Tables[0].Rows[0]["RESULT"]); //프로시져 0,1 교환 To Do
                    defaultReturn.MSG = ds.Tables[0].Rows[0]["MSG"].ToString();
                }
            }
            catch (Exception ex)
            {
                defaultReturn.RESULT = false;
                defaultReturn.MSG = ex.Message;
            }

            return defaultReturn;



        }

        public OUT_AD_LOGIN GET_AD_LOGIN(IN_AD_LOGIN param)
        {
            clsLog.Setting_Logger("COMMON", "GET_AD_LOGIN", "INFO");
            //clsLog.Info("[INPUT PARAM]", JsonConvert.SerializeObject(param));

            OUT_AD_LOGIN rtn = new OUT_AD_LOGIN();
            rtn.RESULT = false;
            rtn.MSG = "";

            var domainName = "celltrion.com";

            #region 1. AD 인증 확인
            //string strSQL = "";
            try
            {
                using (var ldap = new LdapConnection(domainName))
                {
                    var networkCredential = new NetworkCredential(param.ID, param.PW, domainName);
                    ldap.SessionOptions.VerifyServerCertificate = new VerifyServerCertificateCallback((con, cer) => true);
                    ldap.SessionOptions.SecureSocketLayer = false;
                    ldap.SessionOptions.ProtocolVersion = 3;
                    ldap.AuthType = AuthType.Negotiate;
                    ldap.Bind(networkCredential);
                }
                rtn.RESULT = true;
                // If the bind succeeds, we have a valid user/pass.                
            }
            catch (LdapException ldapEx)
            {
                rtn.RESULT = false;
                rtn.MSG = "ID or PW is incorrect.";
                // Error Code 0x31 signifies invalid credentials, anything else will be caught outside.
                if (!ldapEx.ErrorCode.Equals(49))
                {
                    rtn.MSG = ldapEx.Message;
                }
                clsLog.Setting_Logger("COMMON", "GET_AD_LOGIN", "ERROR");
                //clsLog.Error("[INPUT PARAM]", JsonConvert.SerializeObject(param));
                clsLog.Error("[오류정보]", "LdapException:" + rtn.MSG);
            }
            catch (Exception err)
            {
                rtn.RESULT = false;
                rtn.MSG = "An unknown error occured when AD connecting ::" + err.Message;
                clsLog.Setting_Logger("COMMON", "GET_AD_LOGIN", "ERROR");
                //clsLog.Error("[INPUT PARAM]", JsonConvert.SerializeObject(param));
                clsLog.Error("[오류정보]", "AD Exception:" + rtn.MSG);
            }
            #endregion

            clsLog.Info("[OUT PARAM]", JsonConvert.SerializeObject(rtn));

            return rtn;
        }

        public OUT_AD_TRACE Get_AD_TRACE_LIST(IN_AD_TRACE param)
        {
            clsLog.Setting_Logger("COMMON", "Get_AD_TRACE_LIST", "INFO");
            clsLog.Info("[INPUT PARAM]", JsonConvert.SerializeObject(param));

            OUT_AD_TRACE rtn = new OUT_AD_TRACE();
            rtn.RESULT = false;
            rtn.MSG = "";
            rtn.List = new List<TRACE_LIST>();

            string strSQL = "";
            //string strconn = "";
            try
            {
                string approvalguid = "";
                if (!string.IsNullOrEmpty(param.Approvalguid))
                {
                    approvalguid = "and (recordguid = '" + param.Approvalguid + "' or recordguid = '{" + param.Approvalguid + "}'   )";
                }

                string approval = "";

                if (!string.IsNullOrEmpty(param.Approval))
                {
                    approval = "and approval like '" + param.Approval + "%' ";
                }

                string systemuser = "";
                if (!string.IsNullOrEmpty(param.SystemUser))
                {
                    systemuser = "and systemuser like '" + param.SystemUser + "%' ";
                }

                string id = "";
                if (!string.IsNullOrEmpty(param.ID))
                {
                    id = "and userid = '" + param.ID + "' ";
                }

                string fromdate = "";
                if (!string.IsNullOrEmpty(param.Fromdate))
                {
                    fromdate = "and convert(varchar(10), createdon,23) >= '" + param.Fromdate + "' ";
                }

                string todate = "";
                if (!string.IsNullOrEmpty(param.Todate))
                {
                    todate = "and convert(varchar(10), createdon,23) <= '" + param.Todate + "' ";
                }

                strSQL =
                   string.Format(@" 
                    SELECT idx, recordguid, approval, systemuser, step, userid, approval_status, comment, result, msg, createdon FROM ADLog
                    WHERE 1=1
                    {0} 
                    {1}
                    {2}
                    {3}
                    {4}
                    {5}
                ", approvalguid, approval, systemuser, id, fromdate, todate);

                DataSet ds = Common.ReturnDataset_query(strSQL, "CETS_Quiz");

                DataTable dt = ds.Tables[0];

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    TRACE_LIST tl = new TRACE_LIST();
                    tl.idx = dt.Rows[i]["idx"].ToString();
                    tl.approvalguid = dt.Rows[i]["recordguid"].ToString();
                    tl.approval = dt.Rows[i]["approval"].ToString();
                    tl.systemuser = dt.Rows[i]["systemuser"].ToString();
                    tl.step = dt.Rows[i]["step"].ToString();
                    tl.userid = dt.Rows[i]["userid"].ToString();
                    tl.Approval_Status = dt.Rows[i]["approval_status"].ToString();
                    tl.Comment = dt.Rows[i]["comment"].ToString();
                    tl.result = dt.Rows[i]["result"].ToString();
                    tl.msg = dt.Rows[i]["msg"].ToString();
                    tl.createdon = Convert.ToDateTime(dt.Rows[i]["createdon"]).ToString("yyyy-MM-dd HH:mm:ss");

                    rtn.List.Add(tl);
                }

                rtn.RESULT = true;
                rtn.MSG = "";//
                rtn.MSG = strSQL;
                //msg = "";
                //msg += "rtn.RESULT:" + rtn.RESULT.ToString() + "//";
                //msg += "rtn.MSG:" + strSQL + "//" + Environment.NewLine;
                //msg += "list=========" + Environment.NewLine;
                //for(int i=0; i<rtn.List.Count; i++)
                //{
                //    msg += rtn.List[i].idx + "/" + 
                //        rtn.List[i].approvalguid + "/" + 
                //        rtn.List[i].approval + "/"+ 
                //        rtn.List[i].systemuser + "/"+ 
                //        rtn.List[i].step + "/" +
                //        rtn.List[i].userid + "/" +
                //        rtn.List[i].result + "/" +
                //        rtn.List[i].msg + "/" +
                //        rtn.List[i].createdon + "/" +
                //        Environment.NewLine;
                //}

                clsLog.Info("[OUT PARAM]", JsonConvert.SerializeObject(rtn));
            }
            catch (Exception err)
            {
                rtn.RESULT = false;
                rtn.MSG = err.Message;

                clsLog.Setting_Logger("COMMON", "Get_AD_TRACE_LIST", "ERROR");
                clsLog.Error("[오류정보]", "Exception:" + rtn.MSG);
            }
            return rtn;
        }

        public DefaultReturn SEND_EMAIL_MSSQL(IN_SEND_EMAIL_MSSQL param)
        {
            clsLog.Setting_Logger("COMMON", "SEND_EMAIL_MSSQL", "INFO");
            clsLog.Info("[INPUT PARAM]", JsonConvert.SerializeObject(param));

            DefaultReturn rtn = new DefaultReturn();

            #region CRM 직원리스트 가져오기
            try
            {
                SqlParameter[] sqlParam = {
                    new SqlParameter("@profile_name", SqlDbType.NVarChar, 100)
                    {
                        Value = !string.IsNullOrEmpty(param.PROFILE_NAME) ? (object)param.PROFILE_NAME : DBNull.Value
                    }
                    ,new SqlParameter("@recipients", SqlDbType.VarChar, -1)
                    {
                        Value = !string.IsNullOrEmpty(param.RECIPIENTS) ? (object)param.RECIPIENTS : DBNull.Value
                    }
                    ,new SqlParameter("@copy_recipients", SqlDbType.VarChar, -1)
                    {
                        Value = !string.IsNullOrEmpty(param.COPY_RECIPIENTS) ? (object)param.COPY_RECIPIENTS : DBNull.Value
                    }
                    ,new SqlParameter("@blind_copy_recipients", SqlDbType.VarChar, -1)
                    {
                        //Value = "hyunjo.kim@celltrion.com" //
                        Value = !string.IsNullOrEmpty(param.BLIND_COPY_RECIPIENTS) ? (object)param.BLIND_COPY_RECIPIENTS : DBNull.Value
                    }
                    ,new SqlParameter("@from_address", SqlDbType.VarChar, -1)
                    {
                        Value = !string.IsNullOrEmpty(param.FROM_ADDRESS) ? (object)param.FROM_ADDRESS : DBNull.Value
                    }
                    ,new SqlParameter("@reply_to", SqlDbType.VarChar, -1)
                    {
                        Value = !string.IsNullOrEmpty(param.REPLY_TO) ? (object)param.REPLY_TO : DBNull.Value
                    }
                    ,new SqlParameter("@subject", SqlDbType.NVarChar, 255)
                    {
                        Value = !string.IsNullOrEmpty(param.SUBJECT) ? (object)param.SUBJECT : DBNull.Value
                    }
                    ,new SqlParameter("@body", SqlDbType.NVarChar, -1)
                    {
                        Value = !string.IsNullOrEmpty(param.BODY) ? (object)param.BODY : DBNull.Value
                    }
                    ,new SqlParameter("@body_format", SqlDbType.VarChar, 20)
                    {
                        Value = !string.IsNullOrEmpty(param.BODY_FORMAT) ? param.BODY_FORMAT : "TEXT"
                    }
                };

                DataSet ds = Common.ReturnDataset_proc("sp_send_dbmail", sqlParam, "msdb");

            }
            catch (Exception err)
            {
                rtn.RESULT = false;
                rtn.MSG = err.Message;
                clsLog.Setting_Logger("COMMON", "SEND_EMAIL_MSSQL", "ERROR");
                clsLog.Error("[INPUT PARAM]", JsonConvert.SerializeObject(param));
                clsLog.Error("[오류정보]", "Send Email Exception:" + rtn.MSG);
            }

            clsLog.Info("[OUT PARAM]", JsonConvert.SerializeObject(rtn));

            #endregion

            return rtn;
        }


        public string GET_CURRENT_TIME()
        {
            //string dt = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");

            //return dt;

            /* 필리핀에 맞게 수정 */
            string dt = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, "Taipei Standard Time").ToString("yyyy-MM-ddTHH:mm:ss");

            return dt;
        }

        //public string GET_CURRENT_TIME(string nation)
        //{
        //    DateTime utcNow = DateTime.UtcNow;

        //    string dt = "";
        //    //Philippines
        //    switch (nation)
        //    {
        //        case "Philippines":
        //            dt = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(utcNow, "Taipei Standard Time").ToString("yyyy-MM-ddTHH:mm:ss");
        //            break;
        //        default:
        //            dt = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(utcNow, "Korea Standard Time").ToString("yyyy-MM-ddTHH:mm:ss");
        //            break;
        //    }

        //    return dt;
        //}
    }
}
