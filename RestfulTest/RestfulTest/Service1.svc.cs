using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Xml.Linq;
using static RestfulTest.Common;

namespace RestfulTest
{
    // 참고: "리팩터링" 메뉴에서 "이름 바꾸기" 명령을 사용하여 코드, svc 및 config 파일에서 클래스 이름 "Service1"을 변경할 수 있습니다.
    // 참고: 이 서비스를 테스트하기 위해 WCF 테스트 클라이언트를 시작하려면 솔루션 탐색기에서Service1.svc나 Service1.svc.cs를 선택하고 디버깅을 시작하십시오.
    public class Service1 : IService1
    {

        public string test()
        {
            return "hello world!";
        }

        public OUTRequest SEARCH_HRDATA(HROutput output)
        {


            DefaultReturn defaultReturn = new DefaultReturn();


            defaultReturn = Common.CompanyInfoChecking(output.DN_Code.Trim(), output.CompanyID.Trim(), output.CompanyPW.Trim(), output.CompanyID.Trim());
            HRInput ret = new HRInput();
            OUTRequest retReq = new OUTRequest();

            if (defaultReturn.RESULT == false)
            {
                // 들어온 정보가 정확지 않습니다. ( Company ID/PW error)
            }

            else
            {


                try
                {

                    SqlParameter[] param = new SqlParameter[] {

                      new SqlParameter("@UR_Code", SqlDbType.VarChar, 50)
                      {
                          Value = output.UR_Code
                      }
                      ,new SqlParameter("@DN_Code", SqlDbType.VarChar, 50)
                      {
                          Value = output.DN_Code
                      }
                      ,new SqlParameter("@DisplayName_KR", SqlDbType.NVarChar, 50)
                      {
                          Value = output.DisplayName_KR
                      }

                  };

                    DataSet ds = Common.ReturnDataset_proc("HR_DATA_SELECT", param, "COVI_SYNCDATA");

                    if (ds.Tables.Count > 1)
                    {
                        // To do 
                        //Table이 여러개 오면 어찌할까 에러인가?
                    }


                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {

                        ret = new HRInput();
                        ret.UR_Code = ds.Tables[0].Rows[i]["UR_Code"].ToString();
                        ret.DN_Code = ds.Tables[0].Rows[i]["DN_Code"].ToString();
                        ret.GR_Code = ds.Tables[0].Rows[i]["GR_Code"].ToString();
                        ret.DisplayName_KR = ds.Tables[0].Rows[i]["DisplayName_KR"].ToString();
                        ret.JobTitleCode = ds.Tables[0].Rows[i]["JobTitleCode"].ToString();
                        ret.JobLevelCode = ds.Tables[0].Rows[i]["JobLevelCode"].ToString();
                        ret.SortKey = ds.Tables[0].Rows[i]["SortKey"].ToString();
                        ret.EnterDate = ds.Tables[0].Rows[i]["EnterDate"].ToString();
                        ret.RetireDate = ds.Tables[0].Rows[i]["RetireDate"].ToString();
                        ret.BirthDiv = ds.Tables[0].Rows[i]["BirthDiv"].ToString();
                        ret.BirthDate = ds.Tables[0].Rows[i]["BirthDate"].ToString();
                        ret.MailAddress = ds.Tables[0].Rows[i]["MailAddress"].ToString();
                        ret.PhoneNumberInter = ds.Tables[0].Rows[i]["PhoneNumberInter"].ToString();
                        ret.AD_Mobile = ds.Tables[0].Rows[i]["AD_Mobile"].ToString();

                        retReq.List.Add(ret);
                    }


                }
                catch (Exception e)
                {
                    //Sql Parameter Setting 에러 . 

                }

            }

            return retReq;


        }



        public string GetData(InputGetDate param)
        {




            return "";
            //json = (JObject)value;


            //add retdata = new add();
            //retdata.add1 = Convert.ToInt32(json["first"]);
            //retdata.add2 = Convert.ToInt32(json["second"]);

            //int result = retdata.add1 + retdata.add2;


            //return string.Format("입력하신 데이터의 결과는  : {0} 입니다", result.ToString());
        }


        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
    }
}
