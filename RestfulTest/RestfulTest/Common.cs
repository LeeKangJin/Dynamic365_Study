
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace RestfulTest
{
    public class Common
    {
        public const int DOMAIN_UN_EQUAL = 0;
        public const int ID_PW_EQUAL = 1;
        public const int ID_PW_UN_EQUAL = -1;

        public class DefaultReturn
        {
            public bool RESULT { get; set; }
            public string MSG { get; set; }
            public DefaultReturn()
            {
                RESULT = true;
                MSG = "";
            }
        }


        public static DefaultReturn CompanyInfoChecking(string inputDN, string inputID, string inputPW, string domain)
        {

            DefaultReturn defaultReturn = new DefaultReturn();

            int chkRet = 0;
            chkRet += CompanyInfoCheck(inputDN, inputID, inputPW, domain, "celltrion");
            chkRet += CompanyInfoCheck(inputDN, inputID, inputPW, domain, "celltrion_healthcare");
            chkRet += CompanyInfoCheck(inputDN, inputID, inputPW, domain, "celltrion_welfare");
            chkRet += CompanyInfoCheck(inputDN, inputID, inputPW, domain, "celltrion_gaon");
            chkRet += CompanyInfoCheck(inputDN, inputID, inputPW, domain, "celltrion_ts");
            chkRet += CompanyInfoCheck(inputDN, inputID, inputPW, domain, "celltrion_seoho");
            chkRet += CompanyInfoCheck(inputDN, inputID, inputPW, domain, "celltrion_freezone");
            chkRet += CompanyInfoCheck(inputDN, inputID, inputPW, domain, "celltrion_holdings");
            chkRet += CompanyInfoCheck(inputDN, inputID, inputPW, domain, "celltrion_entertainment");
            chkRet += CompanyInfoCheck(inputDN, inputID, inputPW, domain, "celltrion_skincure");
            chkRet += CompanyInfoCheck(inputDN, inputID, inputPW, domain, "celltrion_pharm");


            //1일때
            if (chkRet == ID_PW_EQUAL)
            {
                defaultReturn.MSG = "회사 로그인 아이디와 패스워드가 일치합니다.";
                defaultReturn.RESULT = true;
            }
            //-1 일때
            else if (chkRet == ID_PW_EQUAL)
            {
                defaultReturn.MSG = "로그인 아이디 또는 패스워드가 불일치합니다.";
                defaultReturn.RESULT = false;
            }
            //0 일때 
            else if (chkRet == DOMAIN_UN_EQUAL)
            {
                defaultReturn.MSG = "입력하시 도메인 값(" + domain + ") 또는 DN 코드가 불일치 합니다.";
                defaultReturn.RESULT = false;
            }
            //API 에러 로그 추가 To Do 
            else
            {
                defaultReturn.MSG = "Check 함수 오류. 관리자에게 문의 하세요.";
                defaultReturn.RESULT = false;

            }
            return defaultReturn;

        }
        
        public static int CompanyInfoCheck(string inputDN, string inputID, string inputPW, string domain, string groupName)
        {


            if (domain == ConfigurationManager.AppSettings[groupName + "_DOMAIN"])
            {
                if (inputDN == ConfigurationManager.AppSettings[groupName + "_DN"])
                {
                    if (inputID == ConfigurationManager.AppSettings[groupName + "_ID"] &&
                        inputPW == ConfigurationManager.AppSettings[groupName + "_PW"])
                    {
                        return ID_PW_EQUAL;
                    }
                    else
                    {
                        return ID_PW_UN_EQUAL;
                    }

                }
            }
            else
            {
                return DOMAIN_UN_EQUAL;
            }

            return DOMAIN_UN_EQUAL;

        }

        public static DataSet ReturnDataset_proc(string strProcedure, SqlParameter[] param, string ConString)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings[ConString].ConnectionString))
            {
                SqlCommand Cmd = new SqlCommand(strProcedure, conn);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddRange(param);
                Cmd.CommandTimeout = 10000 * 10000;

                SqlDataAdapter Adapt = new SqlDataAdapter(Cmd);
                DataSet Ds = new DataSet();

                conn.Open();

                Adapt.Fill(Ds);
                Adapt.Dispose();

                Cmd.Parameters.Clear();
                Cmd.Dispose();

                conn.Close();
                conn.Dispose();

                return Ds;
            }
        }
        public static DataSet ReturnDataset_proc(string strProcedure, string ConString)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings[ConString].ConnectionString))
            {
                SqlCommand Cmd = new SqlCommand(strProcedure, conn);
                Cmd.CommandType = CommandType.StoredProcedure;
                //Cmd.Parameters.AddRange(param);
                Cmd.CommandTimeout = 10000 * 10000;

                SqlDataAdapter Adapt = new SqlDataAdapter(Cmd);
                DataSet Ds = new DataSet();

                conn.Open();

                Adapt.Fill(Ds);
                Adapt.Dispose();

                Cmd.Parameters.Clear();
                Cmd.Dispose();

                conn.Close();
                conn.Dispose();

                return Ds;
            }
        }
        public static DataSet ReturnDataset_query(string query, string ConString)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings[ConString].ConnectionString))
            {
                SqlCommand Cmd = new SqlCommand(query, conn);
                Cmd.CommandType = CommandType.Text;
                Cmd.CommandTimeout = 10000 * 10000;

                SqlDataAdapter Adapt = new SqlDataAdapter(Cmd);
                DataSet Ds = new DataSet();

                conn.Open();

                Adapt.Fill(Ds);
                Adapt.Dispose();

                Cmd.Parameters.Clear();
                Cmd.Dispose();

                conn.Close();
                conn.Dispose();

                return Ds;
            }
        }

        public static object Execute_nonquery(string query, string ConString)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings[ConString].ConnectionString))
            {
                SqlCommand Cmd = new SqlCommand(query, conn);
                Cmd.CommandType = CommandType.Text;
                Cmd.CommandTimeout = 10000 * 10000;


                conn.Open();

                int cnt = Cmd.ExecuteNonQuery();

                conn.Close();
                conn.Dispose();

                return cnt;
            }
        }
    }
}
