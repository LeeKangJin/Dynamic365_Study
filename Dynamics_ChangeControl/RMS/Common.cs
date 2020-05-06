using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Linq;
using System.DirectoryServices.Protocols;
using System.Net;
using Newtonsoft.Json;


namespace Plugins.Common
{

    /****OptionSet 공통 필드 *****/
    public enum Event : int
    {

        이동 = 100000000,
        불출 = 100000001,
        폐기 = 100000002,
        리뷰 = 100000003

    }

    public enum Type : int
    {
        DS = 100000000,
        IPC_CC = 100000001,
        IPC_PP = 100000002,
        DP = 100000003,
        IPC_DP = 100000004,
        Study = 100000005,
        Media = 100000006,
        Other = 100000007

    }


    class Common
    {
        /// <summary>
        /// OptionSetValue Label 값 가져오기
        /// </summary>

        //유효성 체크 (Null Check.)
        public OUT_PARAM CommonFiledCheck(Entity target)
        {

            OUT_PARAM ret = new OUT_PARAM();
            ret.RESULT = true;

            if (!target.Contains("new_txt_id"))
            {

                ret.MSG = "No id Value is Entered";
                ret.RESULT = false;

            }
            else if (target["new_txt_id"].ToString() == "")
            {
                ret.MSG = "id Value is empty";
                ret.RESULT = false;
            }

            if (!target.Contains("new_ntxt_comment"))
            {
                ret.MSG = "No comment Value is Entered.";
                ret.RESULT = false;
            }
            else if (target["new_ntxt_commnet"].ToString() == "")
            {
                ret.MSG = "comment Value is empty";
                ret.RESULT = false;
            }

            if (!target.Contains("new_txt_pw"))
            {
                ret.MSG = "No password Value is Entered";
                ret.RESULT = false;
            }
            else if (target["new_txt_pw"].ToString() == "")
            {
                ret.MSG = "password Value is empty";
                ret.RESULT = false;
            }
            return ret;
        }

        public OUT_PARAM LocationCheck(Entity target)
        {
            OUT_PARAM ret = CommonFiledCheck(target);

            if (!target.Contains("new_l_change_location"))
            {
                ret.MSG = "Changed Location Value is empty";
                ret.RESULT = false;
            }

            return ret;

        }

        public OUT_PARAM AmountCheck(Entity target)
        {

            OUT_PARAM ret = CommonFiledCheck(target);

            if (!target.Contains("new_issuaranceamount"))
            {
                ret.MSG = "Issuarance amount Value is empty";
                ret.RESULT = false;
            }
            else if (Convert.ToInt32(target["new_issuaranceamount"]) == 0)
            {

                ret.MSG = "Issuarance amount Value is 0";
                ret.RESULT = false;

            }


            return ret;



        }






        public OUT_PARAM GET_AD_LOGIN(IN_AD_LOGIN param)
        {
            //clsLog.Info("[INPUT PARAM]", JsonConvert.SerializeObject(param));

            OUT_PARAM rtn = new OUT_PARAM();
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
                //clsLog.Error("[INPUT PARAM]", JsonConvert.SerializeObject(param));
            }
            catch (Exception err)
            {
                rtn.RESULT = false;
                rtn.MSG = "An unknown error occured when AD connecting ::" + err.Message;
                //clsLog.Error("[INPUT PARAM]", JsonConvert.SerializeObject(param));
            }
            #endregion

            return rtn;
        }




        public static string GetOptionSetValueLabel(string entityName, string fieldName, int optionSetValue, IOrganizationService service)
        {

            var attReq = new RetrieveAttributeRequest();
            attReq.EntityLogicalName = entityName;
            attReq.LogicalName = fieldName;
            attReq.RetrieveAsIfPublished = true;

            var attResponse = (RetrieveAttributeResponse)service.Execute(attReq);
            var attMetadata = (EnumAttributeMetadata)attResponse.AttributeMetadata;

            return attMetadata.OptionSet.Options.Where(x => x.Value == optionSetValue).FirstOrDefault().Label.UserLocalizedLabel.Label;
        }

        // Common Class Type
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

        public class IN_AD_LOGIN
        {
            public string Task { get; set; }
            public string Recordguid { get; set; }
            public string Approval { get; set; }
            public string SystemUser { get; set; }
            public string Step { get; set; }
            public string ID { get; set; }
            public string PW { get; set; }

            public string Approval_Status { get; set; }

            public string Comment { get; set; }
        }

        public class OUT_PARAM
        {
            public bool RESULT { get; set; }
            public string MSG { get; set; }
        }






    }
}
