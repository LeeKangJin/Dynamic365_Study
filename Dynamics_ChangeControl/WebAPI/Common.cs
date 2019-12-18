using CELLAPI.Entities;
using CELLAPI.Entities.CommonClass;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
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

namespace CELLAPI.Service
{
    public class Common
    {

        public static DefaultReturn CompanyInfoChecking(string inputDN, string inputID, string inputPW,string domain) {

            DefaultReturn defaultReturn = new DefaultReturn();
          
            defaultReturn = CompanyInfoCheck(inputDN, inputID,inputPW, domain, "celltrion");                
            defaultReturn = CompanyInfoCheck(inputDN, inputID,inputPW, domain, "celltrion_healthcare");     
            defaultReturn = CompanyInfoCheck(inputDN, inputID,inputPW, domain, "celltrion_welfare");        
            defaultReturn = CompanyInfoCheck(inputDN, inputID,inputPW, domain, "celltrion_gaon");           
            defaultReturn = CompanyInfoCheck(inputDN, inputID,inputPW, domain, "celltrion_ts");             
            defaultReturn = CompanyInfoCheck(inputDN, inputID,inputPW, domain, "celltrion_seoho");          
            defaultReturn = CompanyInfoCheck(inputDN, inputID,inputPW, domain, "celltrion_freezone");       
            defaultReturn = CompanyInfoCheck(inputDN, inputID,inputPW, domain, "celltrion_holdings");       
            defaultReturn = CompanyInfoCheck(inputDN, inputID,inputPW, domain, "celltrion_entertainment");  
            defaultReturn = CompanyInfoCheck(inputDN, inputID,inputPW, domain, "celltrion_skincure");       
            defaultReturn = CompanyInfoCheck(inputDN, inputID,inputPW, domain, "celltrion_pharm");            

            if (defaultReturn == null) {
                defaultReturn.MSG = "없는 회사 코드입니다.";
                defaultReturn.RESULT = false;
            }
            
            return defaultReturn;

        }



        public static DefaultReturn CompanyInfoCheck(string inputDN, string inputID, string inputPW, string domain, string groupName)
        {
            DefaultReturn defaultReturn = new DefaultReturn();

            if (domain == ConfigurationManager.AppSettings[groupName + "_DOMAIN"])
            {

                if (inputDN == ConfigurationManager.AppSettings[groupName + "_DN"])
                {
                    if (inputID == ConfigurationManager.AppSettings[groupName + "_ID"] &&
                        inputPW == ConfigurationManager.AppSettings[groupName + "_PW"])
                    {
                        defaultReturn.MSG = "회사 로그인 아이디와 패스워드가 일치합니다.";
                        defaultReturn.RESULT = true;
                    }
                    else
                    {
                        defaultReturn.MSG = "회사(" + groupName + ") 로그인 아이디 또는 패스워드가 불일치합니다.";
                        defaultReturn.RESULT = false;
                    }

                }
            }
            else {
                defaultReturn.MSG = "입력하시 도메인 값(" + domain+ ") 이 불일치 합니다.";
                defaultReturn.RESULT = false;
            }

            return defaultReturn;

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


        public static IOrganizationService GetCrmService()
        {
            Uri orgUri = new Uri(ConfigurationManager.AppSettings["crmURL"].ToString());
            ClientCredentials credentials = new ClientCredentials();
            credentials.UserName.UserName = ConfigurationManager.AppSettings["crmID"].ToString();
            credentials.UserName.Password = ConfigurationManager.AppSettings["crmPassword"].ToString();

            OrganizationServiceProxy proxy = new OrganizationServiceProxy(orgUri, null, credentials, null);
            IOrganizationService service = (IOrganizationService)proxy;

            return service;
        }
        public static IOrganizationService GetCrmService(string id)
        {
            Uri orgUri = new Uri(ConfigurationManager.AppSettings["crmURL"].ToString());
            ClientCredentials credentials = new ClientCredentials();
            credentials.UserName.UserName = ConfigurationManager.AppSettings["crmID_" + id].ToString();
            credentials.UserName.Password = ConfigurationManager.AppSettings["crmPassword_" + id].ToString();

            OrganizationServiceProxy proxy = new OrganizationServiceProxy(orgUri, null, credentials, null);
            IOrganizationService service = (IOrganizationService)proxy;

            return service;
        }
        public static string getValuefromAttribute(object p, bool Guid)
        {
            if (p.ToString() == "Microsoft.Xrm.Sdk.EntityReference")
            {
                if (Guid)
                {
                    return ((EntityReference)p).Id.ToString();
                }
                else
                {
                    return ((EntityReference)p).Name.ToString();
                }

            }
            if (p.ToString() == "Microsoft.Xrm.Sdk.OptionSetValue")
            {
                return ((OptionSetValue)p).Value.ToString();
            }
            if (p.ToString() == "Microsoft.Xrm.Sdk.Money")
            {
                return ((Money)p).Value.ToString();
            }
            if (p.ToString() == "Microsoft.Xrm.Sdk.AliasedValue")
            {
                return getValuefromAttribute(((AliasedValue)p).Value, Guid);
            }
            else
            {
                return p.ToString();
            }
        }

        public static string getValuefromAttribute(Entity et, string name)
        {
            return getValuefromAttribute(et, name, true);
        }


        public static string getValuefromAttribute(Entity et, string name, bool Guid)
        {
            if (et.Contains(name))
            {
                if (et.Attributes[name].ToString() == "Microsoft.Xrm.Sdk.EntityReference")
                {
                    if (Guid)
                    {
                        return ((EntityReference)et.Attributes[name]).Id.ToString();
                    }
                    else
                    {
                        return ((EntityReference)et.Attributes[name]).Name.ToString();
                    }
                }
                if (et.Attributes[name].ToString() == "Microsoft.Xrm.Sdk.OptionSetValue")
                {
                    return ((OptionSetValue)et.Attributes[name]).Value.ToString();
                }
                if (et.Attributes[name].ToString() == "Microsoft.Xrm.Sdk.Money")
                {
                    return ((Money)et.Attributes[name]).Value.ToString();
                }
                if (et.Attributes[name].ToString() == "Microsoft.Xrm.Sdk.AliasedValue")
                {
                    return getValuefromAttribute(((AliasedValue)et.Attributes[name]).Value, Guid);
                }
                else
                {
                    return et.Attributes[name].ToString();
                }
            }
            else
            {
                return "";
            }
        }

        public static void BulkInsert(IOrganizationService service, List<Entity> entities)
        {
            // Create an ExecuteMultipleRequest object.
            var multipleRequest = new ExecuteMultipleRequest()
            {
                // Assign settings that define execution behavior: continue on error, return responses. 
                Settings = new ExecuteMultipleSettings()
                {
                    ContinueOnError = false,
                    ReturnResponses = true
                },
                // Create an empty organization request collection.
                Requests = new OrganizationRequestCollection()
            };

            // Add a CreateRequest for each entity to the request collection.
            foreach (var entity in entities)
            {
                CreateRequest createRequest = new CreateRequest { Target = entity };
                multipleRequest.Requests.Add(createRequest);
            }

            // Execute all the requests in the request collection using a single web method call.
            ExecuteMultipleResponse multipleResponse = (ExecuteMultipleResponse)service.Execute(multipleRequest);
        }

        public static void BulkUpdate(IOrganizationService service, List<Entity> entities)
        {
            // Create an ExecuteMultipleRequest object.
            var multipleRequest = new ExecuteMultipleRequest()
            {
                // Assign settings that define execution behavior: continue on error, return responses. 
                Settings = new ExecuteMultipleSettings()
                {
                    ContinueOnError = false,
                    ReturnResponses = true
                },
                // Create an empty organization request collection.
                Requests = new OrganizationRequestCollection()
            };

            // Add a UpdateRequest for each entity to the request collection.
            foreach (var entity in entities)
            {
                UpdateRequest updateRequest = new UpdateRequest { Target = entity };
                multipleRequest.Requests.Add(updateRequest);
            }

            // Execute all the requests in the request collection using a single web method call.
            ExecuteMultipleResponse multipleResponse = (ExecuteMultipleResponse)service.Execute(multipleRequest);

        }

        public static DataTable convertEntityCollectionToDataTable(EntityCollection BEC, string[] Cols)
        {
            DataTable dt = new DataTable();
            int total = BEC.Entities.Count;
            for (int i = 0; i < total; i++)
            {
                DataRow row = dt.NewRow();
                Entity myEntity = (Entity)BEC.Entities[i];
                var keys = myEntity.Attributes.Keys;
                foreach (string item in Cols)
                {
                    string columnName = item;

                    string value = "";
                    if (myEntity.Contains(item))
                    {
                        value = getValuefromAttribute(myEntity.Attributes[item], true);
                    }
                    else
                    {
                        value = "";
                    }


                    if (dt.Columns.IndexOf(columnName) == -1)
                    {
                        dt.Columns.Add(item, Type.GetType("System.String"));
                    }
                    row[columnName] = value;
                }
                dt.Rows.Add(row);
            }
            return dt;
        }

        public static string GetIpAddress()
        {
            OperationContext context = OperationContext.Current;
            MessageProperties prop = context.IncomingMessageProperties;
            RemoteEndpointMessageProperty endpoint =
                prop[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
            string ip = endpoint.Address;

            return ip;
        }
    }
}
