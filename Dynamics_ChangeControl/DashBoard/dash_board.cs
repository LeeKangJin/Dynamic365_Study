using ConsoleAppGwProposal.common;
using HtmlAgilityPack;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using System.Xml;


// 2019-11-08



//GW to CRM 
namespace ConsoleAppGwProposal
{
    class Program
    {


        static void Main(string[] args)
        {

            clsLog.Setting_Logger("ConsoleAppGwProposal", "INFO");
            clsLog.Info("GW ProPosal Migration", "=================================================");
            IOrganizationService service = GetCrmService();

            #region 전자결재동향
            //try
            //{



            //    string strProcedure = "GW_GET_PROPOSAL_DATA";
            //    string strDeleteDBProcedure = "GW_SET_PROPOSAL_INACTIVATE_DATA";
            //    DataSet ds = new DataSet();
            //    uint countLog = 1;
            //    SqlParameter[] param;

            //    / Daily Update(SQL에서 CRM에 추가된 guid만 뽑기

            //     param = new SqlParameter[] {
            //        new SqlParameter("@TYPE", SqlDbType.NVarChar, 10)
            //        {
            //            Value = "1"
            //        }
            //     };
            //    Daily Upsert용 Data 가져오기. 
            //    param = new SqlParameter[] {
            //        new SqlParameter("@TYPE", SqlDbType.NVarChar, 10)
            //        {
            //            Value = "2"
            //        }
            //    };



            //    ds = ReturnDataset(strProcedure, param);


            //    if (ds.Tables.Count > 0)
            //    {
            //        DataTable table = ds.Tables[0];
            //        DataRowCollection rows = table.Rows;

            //        #region Upsert

            //        foreach (DataRow dr in ds.Tables[0].Rows)
            //        {


            //            try
            //            {
            //                Entity gwProposal = new Entity("new_gwproposal");

            //                #region 참조주석

            //                /*
            //               *  number 0 : new_name - SUBJECT
            //               *  number 1 : new_l_contact -  INITIATOR_ID
            //               *  number 2 : new_l_department - INITIATOR_OU_ID
            //               *  number 3 : new_l_gwform - (not exist)
            //               *  number 4 : new_p_gw_status - PI_STATE
            //               *  number 5 : new_p_app_status  - PI_BUSINESS_STATENAME
            //               *  number 6 : new_dt_initiated - INITIATED_DATE
            //               *  number 7 : new_dt_completed - COMPLETED_DATE 
            //               *  number 8 : new_dt_deleted - PI_DELETED 
            //               *  number 9 : new_txt_id - FORM_INST_ID
            //               */

            //                #endregion

            //                내용
            //                gwProposal["new_name"] = !string.IsNullOrEmpty(dr["SUBJECT"].ToString()) ? dr["SUBJECT"].ToString() : "제목 없음";

            //                직원
            //                gwProposal["new_l_contact"] = !string.IsNullOrEmpty(dr["INITIATOR_ID"].ToString()) ? retEntityRefer(service, dr, "contact", "new_txt_companynum", "INITIATOR_ID") : null;

            //                부서
            //                gwProposal["new_l_department"] = !string.IsNullOrEmpty(dr["INITIATOR_OU_ID"].ToString()) ? retEntityRefer(service, dr, "new_department", "new_txt_gw_code", "INITIATOR_OU_ID") : null;

            //                그룹웨어 폼
            //                gwProposal["new_l_gwform"] = !string.IsNullOrEmpty(dr[""]

            //                진행상태
            //                gwProposal["new_p_gw_status"] = !string.IsNullOrEmpty(dr["PI_STATE"].ToString()) ? new OptionSetValue(Convert.ToInt32(dr["PI_STATE"])) : null;

            //                결재상태
            //                gwProposal["new_p_app_status"] = !string.IsNullOrEmpty(dr["PI_BUSINESS_STATENAME"].ToString()) ? new OptionSetValue(Convert.ToInt32(dr["PI_BUSINESS_STATENAME"])) : null;


            //                최초시작일
            //                DateTime temp_datetime;
            //                if (DateTime.TryParse(dr["INITIATED_DATE"].ToString(), out temp_datetime))
            //                {
            //                    gwProposal["new_dt_initiated"] = temp_datetime;
            //                }
            //                else
            //                {
            //                    gwProposal["new_dt_initiated"] = null;
            //                }
            //                완료일
            //                if (DateTime.TryParse(dr["COMPLETED_DATE"].ToString(), out temp_datetime))
            //                {
            //                    gwProposal["new_dt_completed"] = temp_datetime;
            //                }
            //                else
            //                {
            //                    gwProposal["new_dt_completed"] = null;
            //                }


            //                삭제일
            //                if (DateTime.TryParse(dr["PI_DELETED"].ToString(), out temp_datetime))
            //                {
            //                    gwProposal["new_dt_deleted"] = temp_datetime;
            //                }
            //                else
            //                {
            //                    gwProposal["new_dt_deleted"] = null;
            //                }


            //                고유 아이디
            //                gwProposal["new_txt_id"] = !string.IsNullOrEmpty(dr["FORM_INST_ID"].ToString()) ? dr["FORM_INST_ID"].ToString() : "";


            //                2019 - 08 - 14 바뀐 부분
            //                Guid gwProposalId = retID(service, dr, "new_gwproposal", "new_txt_id", "FORM_INST_ID");
            //                var gwProposalId = dr["new_gwproposalId"];

            //                이미 아이디가 있다. (기존의 data가 있다: 업데이트 가능성)
            //                if (!String.IsNullOrEmpty(gwProposalId.ToString()) && Guid.Empty != new Guid(gwProposalId.ToString()))
            //                {
            //                    gwProposal.Id = new Guid(gwProposalId.ToString());
            //                    gwProposal.Id = gwProposalId;
            //                    #region 업데이트

            //                    service.Update(gwProposal);

            //                    Console.WriteLine("GW PROPOSAL Migration::: Service Update 완료" + "GUID:" + gwProposal.Id);
            //                    clsLog.Info("GW PROPOSAL Migration::: Service Update 완료" + "GUID:" + gwProposal.Id, "=================================================");
            //                    #endregion

            //                }
            //                이미 아이디가 없는 경우(Create 실행)
            //                else
            //                {
            //                    #region 생성
            //                    Console.WriteLine("GW PROPOSAL Migration::: Service Create 완료 GUID:" + gwProposal);
            //                    service.Create(gwProposal);
            //                    clsLog.Info("GW PROPOSAL Migration::: Service Create 완료 GUID:" + gwProposal, "=================================================");
            //                    #endregion


            //                }



            //            }
            //            catch (Exception ex_in)
            //            {
            //                Console.WriteLine("CRM에 업데이트/생성  도중 에러");
            //                string errMessage = ex_in.Message.ToString();
            //                clsLog.Error("GW PROPOSAL Migration", "=================================================");
            //                clsLog.Error("GW PROPOSAL Migration ::: CRM에 업데이트/생성  도중 " + countLog + "번째에서 에러발생 ::: ", ex_in.Message + Environment.NewLine + ex_in.StackTrace);

            //            }



            //        }

            //        #endregion
            //    }




            //    #region Delete

            //    임시 삭제 db에 접속
            //    param = new SqlParameter[] {
            //        new SqlParameter("@TYPE", SqlDbType.NVarChar, 10)
            //        {
            //            Value = "3"
            //        }
            //    };
            //    ds = ReturnDataset(strProcedure, param);

            //    if (ds.Tables.Count > 0)
            //    {

            //        DataTable table = ds.Tables[0];
            //        DataRowCollection rows = table.Rows;

            //        State N 인것들
            //        countLog = 1;
            //        foreach (DataRow dr in ds.Tables[0].Rows)
            //        {
            //            try
            //            {
            //                Guid gwProposalId = Guid.Empty;

            //                gwProposalId = retID(service, dr, "new_gwproposal", "new_txt_id", "FORM_INST_ID");
            //                countLog++;
            //                if (gwProposalId != Guid.Empty)
            //                {

            //                    비활성화
            //                    SetStateRequest setStateRequest = new SetStateRequest()
            //                    {
            //                        EntityMoniker = new EntityReference
            //                        {
            //                            Id = gwProposalId,
            //                            LogicalName = "new_gwproposal",
            //                        },
            //                        State = new OptionSetValue(1),
            //                        Status = new OptionSetValue(2)
            //                    };
            //                    service.Execute(setStateRequest);

            //                    임시 DB State 변경
            //                    param = new SqlParameter[] {
            //                    new SqlParameter("@TYPE", SqlDbType.NVarChar, 100)
            //                    {
            //                        Value = dr["FORM_INST_ID"].ToString()
            //                    }
            //                };

            //                    DataSet dc = ReturnDataset(strDeleteDBProcedure, param);

            //                    if (Convert.ToInt32(dc.Tables[0].Rows[0]["CNT"]) == 0)
            //                    {
            //                        clsLog.Error("GW PROPOSAL ERROR : DB 삭제 테이블의 값이 없습니다. " + countLog + "번째 데이터에서 에러 발생", "=================================================");

            //                    }
            //                }


            //            }
            //            catch (Exception ex_in)
            //            {
            //                string errMessage = ex_in.Message.ToString();
            //                clsLog.Error("GW PROPOSAL Migration", "=================================================");
            //                clsLog.Error("GW PROPOSAL Migration ::: CRM 데이터 비활성화 도중 에러발생 ::: " + countLog + "번째 까지 반영후 에러", ex_in.Message + Environment.NewLine + ex_in.StackTrace);

            //            }
            //        }




            //    }


            //    #endregion

            //}


            //catch (Exception ex_out)
            //{
            //    Console.WriteLine("CRM에 연결 도중 에러");
            //    string errMessage = ex_out.Message.ToString();
            //    clsLog.Error("GW PROPOSAL Migration", "=================================================");
            //    clsLog.Error("GW PROPOSAL Migration ::: CRM 연결 및 해제 도중 에러 발생 ::: ", ex_out.Message + Environment.NewLine + ex_out.StackTrace);

            //}
            #endregion

            string strProcedure = "GW_GET_CARD_DATA";
            string strMofProcedure = "GW_GET_CARD_MODIFY_DATA";

            #region 예산 결재 문서
            //assetDocument(service, strProcedure, 0);
            //assetDocument(service, strMofProcedure, 1);
            #endregion
            Console.WriteLine("업무협조시작");
            #region 업무협조-인장날인
            coworkDocument(service, strProcedure, 0);
            //coworkDocument(service, strMofProcedure, 1);
            #endregion
            Console.WriteLine("신규업체등록시작");
            #region 신규업체등록 결재문서
            //newcompanyDocument(service, strProcedure, 0);
            //newcompanyDocument(service, strMofProcedure, 1);
            #endregion
            Console.WriteLine("법카시작");
            #region 법인카드 결재문서
           //cardDocument(service, strProcedure, 0);
           //cardDocument(service, strMofProcedure, 1);

            #endregion
            Console.WriteLine("끝");

        }
        //type 0 - create
        //type 1 - update
        public static void assetDocument(IOrganizationService service, string strProcedure,int type) {


            DataSet ds = new DataSet();
            try
            {
                
                ////최초 Data 가져오기. 그이후로는 업데이트 데이터를 가지고 있기 
                SqlParameter[] param = new SqlParameter[] {
                    new SqlParameter("@TYPE", SqlDbType.NVarChar, 10)
                    {
                        Value = '4'
                    }
                };



                ds = ReturnDataset(strProcedure, param);


                if (ds.Tables.Count > 0)
                {
                    DataTable table = ds.Tables[0];
                    DataRowCollection rows = table.Rows;

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {


                        try
                        {

                            Entity gwLegacy = new Entity("new_form_legacy");


                            //text 데이터 전송
                            gwLegacy["new_name"] = !string.IsNullOrEmpty(dr["FORM_NAME"].ToString()) ? dr["FORM_NAME"].ToString() : "";
                            gwLegacy["new_txt_form_inst_id"] = !string.IsNullOrEmpty(dr["FORM_INST_ID"].ToString()) ? dr["FORM_INST_ID"].ToString() : "";
                            gwLegacy["new_txt_subject"] = !string.IsNullOrEmpty(dr["SUBJECT"].ToString()) ? dr["SUBJECT"].ToString() : "";
                            gwLegacy["new_txt_doc_no"] = !string.IsNullOrEmpty(dr["DOC_NO"].ToString()) ? dr["DOC_NO"].ToString() : "";
                            gwLegacy["new_txt_deleted"] = !string.IsNullOrEmpty(dr["PI_DELETED"].ToString()) ? dr["PI_DELETED"].ToString() : "";
                            gwLegacy["new_txt_priority"] = !string.IsNullOrEmpty(dr["PRIORITY"].ToString()) ? dr["PRIORITY"].ToString() : "";
                                
                            Console.WriteLine("text전송완료");
                            //optionset 데이터 전송

                            var temp = !string.IsNullOrEmpty(dr["PI_BUSINESS_STATENAME"].ToString()) ? dr["PI_BUSINESS_STATENAME"].ToString() : "제목 없음";

                            if (temp == "승인")
                            {
                                gwLegacy["new_p_state"] = new OptionSetValue(100000000);
                            }
                            else if (temp == "반려")
                            {
                                gwLegacy["new_p_state"] = new OptionSetValue(100000001);
                            }
                            else if (temp == "진행")
                            {
                                gwLegacy["new_p_state"] = new OptionSetValue(100000002);
                            }
                            else if (temp == "기안취소")
                            {
                                gwLegacy["new_p_state"] = new OptionSetValue(100000003);
                            }



                            temp = !string.IsNullOrEmpty(dr["LegacyGubun"].ToString()) ? dr["LegacyGubun"].ToString() : "제목 없음";

                            if (temp == "코스트센터")
                            {
                                gwLegacy["new_p_legacygubun"] = new OptionSetValue(100000000);
                            }
                            else if (temp == "WBS")
                            {
                                gwLegacy["new_p_legacygubun"] = new OptionSetValue(100000001);
                            }

                            temp = !string.IsNullOrEmpty(dr["LegacyType"].ToString()) ? dr["LegacyType"].ToString() : "제목 없음";

                            if (temp == "예산추가")
                            {
                                gwLegacy["new_p_legacytype"] = new OptionSetValue(100000000);
                            }
                            else if (temp == "예산전용")
                            {
                                gwLegacy["new_p_legacytype"] = new OptionSetValue(100000001);
                            }
                            else if (temp == "예산배정")
                            {
                                gwLegacy["new_p_legacytype"] = new OptionSetValue(100000002);
                            }

                            Console.WriteLine("optionset전송완료");
                            //Look Up data 전송
                            EntityReference initID = retEntityRefer(service, dr, "contact", "new_txt_companynum", "INITIATOR_ID");
                            gwLegacy["new_l_initiator_ou"] = retEntityRefer(service, dr, "new_department", "new_txt_gw_code", "INITIATOR_OU_ID");
                            gwLegacy["new_l_initiator_id"] = initID;


                            Entity contact = service.Retrieve("contact", initID.Id, new ColumnSet("fullname", "new_txt_team"));
                            gwLegacy["new_txt_initiator_ou"] = contact["new_txt_team"].ToString();
                            gwLegacy["new_txt_initiator_id"] = contact["fullname"].ToString();

                            Console.WriteLine("Lookup 전송완료");
                            //Date data 전송
                            DateTime temp_datetime;

                            if (DateTime.TryParse(dr["INITIATED_DATE"].ToString(), out temp_datetime))
                            {
                                gwLegacy["new_dt_initiated_date"] = temp_datetime;
                            }
                            else
                            {
                                gwLegacy["new_dt_initiated_date"] = null;
                            }

                            if (DateTime.TryParse(dr["COMPLETED_DATE"].ToString(), out temp_datetime))
                            {
                                gwLegacy["new_dt_completed_date"] = temp_datetime;
                            }
                            else
                            {
                                gwLegacy["new_dt_completed_date"] = null;
                            }

                            Console.WriteLine("Date 전송완료");


                            string item = dr["ITEMS"].ToString();


                            HtmlDocument doc = new HtmlDocument();
                            doc.LoadHtml(item);


                         
                            if (temp == "예산추가")
                            {

                                var headers = doc.DocumentNode.SelectNodes("//tr/th");
                                DataTable itemtable = new DataTable();
                                foreach (HtmlNode header in headers)
                                    itemtable.Columns.Add(header.InnerText); // create columns from th
                                                                             // select rows with td elements 
                                foreach (var row in doc.DocumentNode.SelectNodes("//tr[td]"))
                                    itemtable.Rows.Add(row.SelectNodes("td").Select(td => td.InnerText).ToArray());
                                
                                try
                                {

                                    if (dr["LegacyGubun"].ToString() == "코스트센터")
                                    {
                                        gwLegacy["new_txt_legacy_id"] = itemtable.Rows[1]["코스트센터"].ToString();
                                        gwLegacy["new_txt_legacy_name"] = itemtable.Rows[1]["코스트센터명"].ToString();
                                    }
                                    else if (dr["LegacyGubun"].ToString() == "WBS")
                                    {
                                        gwLegacy["new_txt_legacy_id"] = itemtable.Rows[1]["WBS요소"].ToString();
                                        gwLegacy["new_txt_legacy_name"] = itemtable.Rows[1]["WBS요소명"].ToString();
                                    }
                                    // To Do

                                    gwLegacy["new_txt_cost"] = itemtable.Rows[1]["원가요소"].ToString();
                                    gwLegacy["new_txt_cost_name"] = itemtable.Rows[1]["원가요소명"].ToString();
                                    gwLegacy["new_txt_adate"] = itemtable.Rows[1]["조정기간"].ToString();
                                    gwLegacy["new_cur_abudget"] = new Money(Convert.ToDecimal(itemtable.Rows[1]["편성예산"]));
                                    gwLegacy["new_cur_aallocated"] = new Money(Convert.ToDecimal(itemtable.Rows[1]["기 배정금액"]));
                                    gwLegacy["new_cur_aapplicztionamount"] = new Money(Convert.ToDecimal(itemtable.Rows[1]["추가 신청금액"].ToString()));
                                    gwLegacy["new_cur_aperformance"] = new Money(Convert.ToDecimal(itemtable.Rows[1]["실적거래"]));
                                    gwLegacy["new_txt_currency"] = itemtable.Rows[1]["거래통화"].ToString();
                                }
                                catch (Exception e)
                                {
                                    //row index 문제 - 
                                    Console.WriteLine("예산추가ITEM 에러");
                                    throw;
                                }



                            }

                            else if (temp == "예산전용")
                            {


                                //들어오는 table input을 순서대로만 파악하기 때문에.
                                //input 변경시 변경해야함. 
                                //추후 이름으로 파싱해서 데이터 관리할 것 ( #To Do )

                                int table_count = 0;
                                foreach (HtmlNode tab in doc.DocumentNode.SelectNodes("//table"))
                                {
                                    DataTable temp_table = new DataTable();
                                    table_count++;

                                    if (table_count == 1)
                                    {
                                        //추가로 구현 하고 싶다면 예산 전용인지 확인할것. 
                                        continue;
                                    }

                                    foreach (HtmlNode row in tab.SelectNodes("tr"))
                                    {

                                        if (row.SelectNodes("th") != null)
                                        {
                                            foreach (HtmlNode cellheader in row.SelectNodes("th"))
                                            {
                                                temp_table.Columns.Add(cellheader.InnerText);
                                            }
                                        }
                                        if (row.SelectNodes("td") != null)
                                        {

                                            DataRow workRow;
                                            workRow = temp_table.NewRow();
                                            int i = 0;
                                            foreach (HtmlNode cellbody in row.SelectNodes("td"))
                                            {
                                                // To Do 2019 10 30 
                                                //여기서 table 넘버 1, 2냐에 따라 Row를 집어 넣을것
                                                i++;
                                                if (table_count == 2)
                                                {
                                                    insertDataRowTableNumber1(workRow, cellbody.InnerText, i, (dr["LegacyGubun"].ToString()));
                                                }

                                                else if (table_count == 3)
                                                {
                                                    insertDataRowTableNumber2(workRow, cellbody.InnerText, i, (dr["LegacyGubun"].ToString()));
                                                }

                                            }

                                            temp_table.Rows.Add(workRow);
                                            // insert sender information

                                        }


                                    }


                                    if (table_count == 2)
                                    {
                                        if (dr["LegacyGubun"].ToString() == "코스트센터")
                                        {
                                            gwLegacy["new_txt_legacy_id"] = temp_table.Rows[0]["코스트센터"].ToString();
                                            gwLegacy["new_txt_legacy_name"] = temp_table.Rows[0]["코스트센터명"].ToString();
                                           
                                   
                                        }
                                        else if (dr["LegacyGubun"].ToString() == "WBS")
                                        {
                                            gwLegacy["new_txt_legacy_id"] = temp_table.Rows[0]["WBS요소"].ToString();
                                            gwLegacy["new_txt_legacy_name"] = temp_table.Rows[0]["WBS요소명"].ToString();
                                         
                                         

                                        }

                                        gwLegacy["new_txt_cost"] = temp_table.Rows[0]["원가요소"].ToString();
                                        gwLegacy["new_txt_cost_name"] = temp_table.Rows[0]["원가요소명"].ToString();
                                        gwLegacy["new_txt_sdate"] = temp_table.Rows[0]["전용기간"].ToString();
                                        gwLegacy["new_cur_spossibleamount"] = new Money(Convert.ToDecimal(temp_table.Rows[0]["전용 가능금액"].ToString()));
                                        gwLegacy["new_cur_sapplicztionamount"] = new Money(Convert.ToDecimal(temp_table.Rows[0]["전용 신청금액"].ToString()));
                                        gwLegacy["new_txt_currency"] = temp_table.Rows[0]["거래통화"].ToString();
                                        

                                        
                                    }

                                    // insert receiver information
                                    if (table_count == 3)
                                    {

                                        if (dr["LegacyGubun"].ToString() == "코스트센터")
                                        {
                                            gwLegacy["new_txt_rlegacy_id"] = temp_table.Rows[0]["코스트센터"].ToString();
                                            gwLegacy["new_txt_rlegacy_name"] = temp_table.Rows[0]["코스트센터명"].ToString();
                                            gwLegacy["new_txt_rcost"] = temp_table.Rows[0]["리시버 원가요소"].ToString();
                                            gwLegacy["new_txt_rcost_name"] = temp_table.Rows[0]["원가요소명"].ToString();
                                            gwLegacy["new_txt_rdate"] = temp_table.Rows[0]["리시버 기간"].ToString();
                                            gwLegacy["new_cur_rbudget"] = new Money(Convert.ToDecimal(temp_table.Rows[0]["리시버 편성예산"].ToString().Replace(",", "")));
                                            gwLegacy["new_cur_rallocated"] = new Money(Convert.ToDecimal(temp_table.Rows[0]["리시버 배정금액"].ToString().Replace(",", "")));
                                            gwLegacy["new_cur_rperformance"] = new Money(Convert.ToDecimal(temp_table.Rows[0]["리시버 실적"].ToString().Replace(",", "")));

                                        }
                                        else if (dr["LegacyGubun"].ToString() == "WBS")
                                        {
                                            gwLegacy["new_txt_rlegacy_id"] = temp_table.Rows[0]["WBS요소"].ToString();
                                            gwLegacy["new_txt_rlegacy_name"] = temp_table.Rows[0]["WBS요소명"].ToString();
                                            gwLegacy["new_txt_rcost"] = temp_table.Rows[0]["리시버 원가요소"].ToString();
                                            gwLegacy["new_txt_rcost_name"] = temp_table.Rows[0]["원가요소명"].ToString();
                                            gwLegacy["new_txt_rdate"] = temp_table.Rows[0]["리시버 기간"].ToString();
                                            gwLegacy["new_cur_rbudget"] = new Money(Convert.ToDecimal(temp_table.Rows[0]["리시버 편성예산"].ToString().Replace(",", "")));
                                            gwLegacy["new_cur_rallocated"] = new Money(Convert.ToDecimal(temp_table.Rows[0]["리시버 배정금액"].ToString().Replace(",", "")));
                                            gwLegacy["new_cur_rperformance"] = new Money(Convert.ToDecimal(temp_table.Rows[0]["리시버 실적"].ToString().Replace(",", "")));
                                        }
                                        

                                    }

                                    if (type == 0)
                                    {
                                        service.Create(gwLegacy);
                                    }


                                    else if (type == 1)
                                    {

                                        Guid retGuid = retID(service, dr, "new_form_legacy", "new_txt_form_inst_id", "FORM_INST_ID");

                                        if (retGuid == null)
                                        {
                                            //error guid is null..
                                            Console.WriteLine("error- guid is null");
                                        }
                                        gwLegacy.Id = retGuid;
                                        service.Update(gwLegacy);
                                    }


                                }

                            }


                            else if (temp == "예산배정")
                            {
                                var headers = doc.DocumentNode.SelectNodes("//tr/th");
                                DataTable itemtable = new DataTable();
                                foreach (HtmlNode header in headers)
                                    itemtable.Columns.Add(header.InnerText); // create columns from th
                                                                             // select rows with td elements 
                                foreach (var row in doc.DocumentNode.SelectNodes("//tr[td]"))
                                    itemtable.Rows.Add(row.SelectNodes("td").Select(td => td.InnerText).ToArray());


                                try
                                {

                                    if (dr["LegacyGubun"].ToString() == "코스트센터")
                                    {
                                        gwLegacy["new_txt_legacy_id"] = itemtable.Rows[1]["코스트센터"].ToString();
                                        gwLegacy["new_txt_legacy_name"] = itemtable.Rows[1]["코스트센터명"].ToString();
                                    }
                                    else if (dr["LegacyGubun"].ToString() == "WBS")
                                    {
                                        gwLegacy["new_txt_legacy_id"] = itemtable.Rows[1]["WBS요소"].ToString();
                                        gwLegacy["new_txt_legacy_name"] = itemtable.Rows[1]["WBS요소명"].ToString();
                                    }

                                    gwLegacy["new_txt_cost"] = itemtable.Rows[1]["원가요소"].ToString();
                                    gwLegacy["new_txt_cost_name"] = itemtable.Rows[1]["원가요소명"].ToString();
                                    gwLegacy["new_cur_budget"] = new Money(Convert.ToDecimal(itemtable.Rows[1]["편성예산"]));
                                    gwLegacy["new_cur_allocated"] = new Money(Convert.ToDecimal(itemtable.Rows[1]["기 배정금액"]));
                                    gwLegacy["new_cur_applicable"] = new Money(Convert.ToDecimal(itemtable.Rows[1]["배정신청가능금액"]));
                                    gwLegacy["new_cur_application"] = new Money(Convert.ToDecimal(itemtable.Rows[1]["배정신청금액"]));
                                    gwLegacy["new_cur_performance"] = new Money(Convert.ToDecimal(itemtable.Rows[1]["실적"]));
                                    gwLegacy["new_txt_currency"] = itemtable.Rows[1]["거래통화"].ToString();


                                }
                                catch (Exception e)
                                {
                                    //row index 문제 - 
                                    throw;
                                }

                            }

                            // xml 데이터 파싱

                            // To do 
                            doc = new HtmlDocument();
                            doc.LoadHtml(dr["DOMAIN_DATA_CONTEXT"].ToString());

                            var hs = doc.DocumentNode.SelectNodes("//step");
                            

                            if (hs[hs.Count - 1].Attributes["name"].Value == "부서협조")
                            {

                                doc.LoadHtml(hs[hs.Count - 2].OuterHtml);
                                hs = doc.DocumentNode.SelectNodes("//person");

                                string[] result = hs[0].Attributes["title"].Value.ToString().Split(';');
                                gwLegacy["new_txt_doamindata"] = result[1];
                                string[] result2 = hs[0].Attributes["name"].Value.ToString().Split(';');
                                gwLegacy["new_txt_doamindataname"] = result2[0];
                            }
                            else if (hs[hs.Count - 1].Attributes["name"].Value == "일반결재")
                            {
                                //요기 위처럼 
                                doc.LoadHtml(hs[hs.Count - 1].OuterHtml);
                                hs = doc.DocumentNode.SelectNodes("//person");

                                string[] result = hs[0].Attributes["title"].Value.ToString().Split(';');
                                gwLegacy["new_txt_doamindata"] = result[1];
                                string[] result2 = hs[0].Attributes["name"].Value.ToString().Split(';');
                                gwLegacy["new_txt_doamindataname"] = result2[0];
                            } 
                      
                           
                            if (type == 0)
                            {
                                service.Create(gwLegacy);
                            }
                            else if (type == 1)
                            {

                                Guid retGuid = retID(service, dr, "new_form_legacy", "new_txt_form_inst_id", "FORM_INST_ID");

                                if (retGuid == null)
                                {
                                    //error guid is null..
                                    Console.WriteLine("error- guid is null");
                                }
                                gwLegacy.Id = retGuid;
                                service.Update(gwLegacy);
                            }

                        }
                        catch (Exception e)
                        {
                            //Upsert문제 
                        }
                    }
                }
            }
            catch (Exception e)
            {
                //service 문제
            }


        }

        public static void coworkDocument(IOrganizationService service, string strProcedure, int type) {
           

            try
            {
                DataSet ds = new DataSet();


                ////최초 Data 가져오기. 그이후로는 업데이트 데이터를 가지고 있기 
                SqlParameter[] param = new SqlParameter[] {
                    new SqlParameter("@TYPE", SqlDbType.NVarChar, 10)
                    {
                        Value = '3'
                    }
                };


                ds = ReturnDataset(strProcedure, param);



                if (ds.Tables.Count > 0)
                {
                    DataTable table = ds.Tables[0];
                    DataRowCollection rows = table.Rows;



                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {


                        try
                        {

                            Entity gwDcooper = new Entity("new_form_dcooper4");


                            //text 데이터 전송
                            gwDcooper["new_name"] = !string.IsNullOrEmpty(dr["FORM_NAME"].ToString()) ? dr["FORM_NAME"].ToString() : "";
                            gwDcooper["new_txt_form_inst_id"] = !string.IsNullOrEmpty(dr["FORM_INST_ID"].ToString()) ? dr["FORM_INST_ID"].ToString() : "";
                            gwDcooper["new_txt_subject"] = !string.IsNullOrEmpty(dr["SUBJECT"].ToString()) ? dr["SUBJECT"].ToString() : "";
                            gwDcooper["new_txt_doc_no"] = !string.IsNullOrEmpty(dr["DOC_NO"].ToString()) ? dr["DOC_NO"].ToString() : "";
                            gwDcooper["new_txt_deleted"] = !string.IsNullOrEmpty(dr["PI_DELETED"].ToString()) ? dr["PI_DELETED"].ToString() : "";
                            gwDcooper["new_txt_priority"] = !string.IsNullOrEmpty(dr["PRIORITY"].ToString()) ? dr["PRIORITY"].ToString() : "";



                            Console.WriteLine("text전송완료");
                            //optionset 데이터 전송

                            var temp = !string.IsNullOrEmpty(dr["PI_BUSINESS_STATENAME"].ToString()) ? dr["PI_BUSINESS_STATENAME"].ToString() : "";

                            if (temp == "승인")
                            {
                                gwDcooper["new_p_state"] = new OptionSetValue(100000000);
                            }
                            else if (temp == "반려")
                            {
                                gwDcooper["new_p_state"] = new OptionSetValue(100000001);
                            }
                            else if (temp == "진행")
                            {
                                gwDcooper["new_p_state"] = new OptionSetValue(100000002);
                            }
                            else if (temp == "기안취소")
                            {
                                gwDcooper["new_p_state"] = new OptionSetValue(100000003);
                            }

                            Console.WriteLine("optionset전송완료");
                            //Look Up data 전송

                            EntityReference initID = retEntityRefer(service, dr, "contact", "new_txt_companynum", "INITIATOR_ID");
                            EntityReference initDEPART = retEntityRefer(service, dr, "new_department", "new_txt_gw_code", "INITIATOR_OU_ID");
                            gwDcooper["new_l_initiator_ou"] = initDEPART;
                            gwDcooper["new_l_initiator_id"] = initID;

                            //추가로 string도 저장
                            Entity contact = service.Retrieve("contact", initID.Id, new ColumnSet("fullname", "new_txt_team"));
                            gwDcooper["new_txt_initiator_ou"] = contact["new_txt_team"].ToString();
                            gwDcooper["new_txt_initiator_id"] = contact["fullname"].ToString();
                            
                            //Date data 전송
                            DateTime temp_datetime;

                            if (DateTime.TryParse(dr["INITIATED_DATE"].ToString(), out temp_datetime))
                            {
                                gwDcooper["new_dt_initiated_date"] = temp_datetime;
                            }
                            else
                            {
                                gwDcooper["new_dt_initiated_date"] = null;
                            }

                            if (DateTime.TryParse(dr["COMPLETED_DATE"].ToString(), out temp_datetime))
                            {
                                gwDcooper["new_dt_completed_date"] = temp_datetime;
                            }
                            else
                            {
                                gwDcooper["new_dt_completed_date"] = null;
                            }

                            Console.WriteLine("Date 전송완료");



                            //xml 파싱 부분 
                            XmlDocument xmlDoc = new XmlDocument();
                            xmlDoc.LoadXml(dr["BODY_CONTEXT"].ToString());

                            XmlNodeList class_text = xmlDoc.GetElementsByTagName("CLASS_TEXT");
                            XmlNodeList reception_desk = xmlDoc.GetElementsByTagName("RECEPTION_DESK");
                            XmlNodeList use_purpose = xmlDoc.GetElementsByTagName("USE_PURPOSE");
                            XmlNodeList collapse = xmlDoc.GetElementsByTagName("COLLAPSE");
                            XmlNodeList form_attach = xmlDoc.GetElementsByTagName("FORM_ATTACH");

                            for (int i = 0; i < class_text.Count; i++)
                            {


                                if (class_text[i].InnerText == "법인인감")
                                {
                                    gwDcooper["new_p_class"] = new OptionSetValue(100000000);
                                }
                                else if (class_text[i].InnerText == "사용인감")
                                {
                                    gwDcooper["new_p_class"] = new OptionSetValue(100000001);
                                }
                                else if (class_text[i].InnerText == "직인")
                                {
                                    gwDcooper["new_p_class"] = new OptionSetValue(100000002);
                                }

                                gwDcooper["new_txt_reception_desk"] = reception_desk[i].InnerText;
                                gwDcooper["new_txt_use_purpose"] = use_purpose[i].InnerText;
                                gwDcooper["new_txt_collapse"] = collapse[i].InnerText;
                                gwDcooper["new_txt_form_attach"] = form_attach[i].InnerText;

                                if (type == 0)
                                {
                                    service.Create(gwDcooper);
                                }
                                else if (type == 1)
                                {

                                    Guid retGuid = retID(service, dr, "new_form_legacy", "new_txt_form_inst_id", "FORM_INST_ID");

                                    if (retGuid == null)
                                    {
                                        //error guid is null..
                                        Console.WriteLine("error- guid is null");
                                    }
                                    gwDcooper.Id = retGuid;
                                    service.Update(gwDcooper);
                                }

                            }






                        }
                        catch (Exception e)
                        {
                            //Upsert문제 
                        }
                    }
                }
            }
            catch (Exception e)
            {
                //service 문제
            }

        }

        public static void newcompanyDocument(IOrganizationService service, string strProcedure, int type) {



       

            try
            {
                DataSet ds = new DataSet();
                ////최초 Data 가져오기. 그이후로는 업데이트 데이터를 가지고 있기 
                SqlParameter[] param = new SqlParameter[] {
                    new SqlParameter("@TYPE", SqlDbType.NVarChar, 10)
                    {
                        Value = '2'
                    }
                };




                ds = ReturnDataset(strProcedure, param);



                if (ds.Tables.Count > 0)
                {
                    DataTable table = ds.Tables[0];
                    DataRowCollection rows = table.Rows;



                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {


                        try
                        {

                            Entity gwRegcompany = new Entity("new_form_regcompany");


                            //text 데이터 전송
                            gwRegcompany["new_name"] = !string.IsNullOrEmpty(dr["FORM_NAME"].ToString()) ? dr["FORM_NAME"].ToString() : "";
                            gwRegcompany["new_txt_form_inst_id"] = !string.IsNullOrEmpty(dr["FORM_INST_ID"].ToString()) ? dr["FORM_INST_ID"].ToString() : "";
                            gwRegcompany["new_txt_subject"] = !string.IsNullOrEmpty(dr["SUBJECT"].ToString()) ? dr["SUBJECT"].ToString() : "";
                            gwRegcompany["new_txt_doc_no"] = !string.IsNullOrEmpty(dr["DOC_NO"].ToString()) ? dr["DOC_NO"].ToString() : "";
                            gwRegcompany["new_txt_deleted"] = !string.IsNullOrEmpty(dr["PI_DELETED"].ToString()) ? dr["PI_DELETED"].ToString() : "";
                            gwRegcompany["new_txt_regcompany"] = !string.IsNullOrEmpty(dr["RegCompany"].ToString()) ? dr["RegCompany"].ToString() : "";
                            gwRegcompany["new_txt_priority"] = !string.IsNullOrEmpty(dr["PRIORITY"].ToString()) ? dr["PRIORITY"].ToString() : "";




                            Console.WriteLine("text전송완료");
                            //optionset 데이터 전송

                            var temp = !string.IsNullOrEmpty(dr["PI_BUSINESS_STATENAME"].ToString()) ? dr["PI_BUSINESS_STATENAME"].ToString() : "";

                            if (temp == "승인")
                            {
                                gwRegcompany["new_p_state"] = new OptionSetValue(100000000);
                            }
                            else if (temp == "반려")
                            {
                                gwRegcompany["new_p_state"] = new OptionSetValue(100000001);
                            }
                            else if (temp == "진행")
                            {
                                gwRegcompany["new_p_state"] = new OptionSetValue(100000002);
                            }
                            else if (temp == "기안취소")
                            {
                                gwRegcompany["new_p_state"] = new OptionSetValue(100000003);
                            }

                            Console.WriteLine("optionset전송완료");
                            //Look Up data 전송
                            EntityReference initID = retEntityRefer(service, dr, "contact", "new_txt_companynum", "INITIATOR_ID");
                            gwRegcompany["new_l_initiator_ou"] = retEntityRefer(service, dr, "new_department", "new_txt_gw_code", "INITIATOR_OU_ID");
                            gwRegcompany["new_l_initiator_id"] = initID;

                            Entity contact = service.Retrieve("contact", initID.Id, new ColumnSet("fullname", "new_txt_team"));
                            gwRegcompany["new_txt_initiator_ou"] = contact["new_txt_team"].ToString();
                            gwRegcompany["new_txt_initiator_id"] = contact["fullname"].ToString();


                            Console.WriteLine("Lookup 전송완료");
                            //Date data 전송
                            DateTime temp_datetime;

                            if (DateTime.TryParse(dr["INITIATED_DATE"].ToString(), out temp_datetime))
                            {
                                gwRegcompany["new_dt_initiated_date"] = temp_datetime;
                            }
                            else
                            {
                                gwRegcompany["new_dt_initiated_date"] = null;
                            }

                            if (DateTime.TryParse(dr["COMPLETED_DATE"].ToString(), out temp_datetime))
                            {
                                gwRegcompany["new_dt_completed_date"] = temp_datetime;
                            }
                            else
                            {
                                gwRegcompany["new_dt_completed_date"] = null;
                            }

                            Console.WriteLine("Date 전송완료");
                            

                            if (type == 0)
                            {
                                service.Create(gwRegcompany);
                            }
                            else if (type == 1)
                            {

                                Guid retGuid = retID(service, dr, "new_form_legacy", "new_txt_form_inst_id", "FORM_INST_ID");

                                if (retGuid == null)
                                {
                                    //error guid is null..
                                    Console.WriteLine("error- guid is null");
                                }
                                gwRegcompany.Id = retGuid;
                                service.Update(gwRegcompany);
                            }

                        }
                        catch (Exception e)
                        {
                            //Upsert문제 
                        }
                    }
                }
            }
            catch (Exception e)
            {
                //service 문제
            }


        }

        public static void cardDocument(IOrganizationService service, string strProcedure, int type) {

          

            try
            {

                DataSet ds = new DataSet();

                ////최초 Data 가져오기. 그이후로는 업데이트 데이터를 가지고 있기 
                SqlParameter[] param = new SqlParameter[] {
                    new SqlParameter("@TYPE", SqlDbType.NVarChar, 10)
                    {
                        Value = '1'
                    }
                };




                ds = ReturnDataset(strProcedure, param);



                if (ds.Tables.Count > 0)
                {
                    DataTable table = ds.Tables[0];
                    DataRowCollection rows = table.Rows;



                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {


                        try
                        {

                            Entity gwCard = new Entity("new_form_card");


                            //text 데이터 전송
                            gwCard["new_name"] = !string.IsNullOrEmpty(dr["FORM_NAME"].ToString()) ? dr["FORM_NAME"].ToString() : "";
                            gwCard["new_txt_form_inst_id"] = !string.IsNullOrEmpty(dr["FORM_INST_ID"].ToString()) ? dr["FORM_INST_ID"].ToString() : "";
                            gwCard["new_txt_subject"] = !string.IsNullOrEmpty(dr["SUBJECT"].ToString()) ? dr["SUBJECT"].ToString() : "";
                            gwCard["new_txt_doc_no"] = !string.IsNullOrEmpty(dr["DOC_NO"].ToString()) ? dr["DOC_NO"].ToString() : "";
                            gwCard["new_txt_deleted"] = !string.IsNullOrEmpty(dr["PI_DELETED"].ToString()) ? dr["PI_DELETED"].ToString() : "";
                    
                            gwCard["new_txt_priority"] = !string.IsNullOrEmpty(dr["PRIORITY"].ToString()) ? dr["PRIORITY"].ToString() : "";



                            gwCard["new_txt_reason"] = !string.IsNullOrEmpty(dr["Reason"].ToString()) ? dr["Reason"].ToString() : "";
                            gwCard["new_txt_item1"] = !string.IsNullOrEmpty(dr["ITEM1"].ToString()) ? dr["ITEM1"].ToString() : "";
                            gwCard["new_txt_item2"] = !string.IsNullOrEmpty(dr["ITEM2"].ToString()) ? dr["ITEM2"].ToString() : "";
                            gwCard["new_txt_item3"] = !string.IsNullOrEmpty(dr["ITEM3"].ToString()) ? dr["ITEM3"].ToString() : "";


                            Console.WriteLine("text전송완료");
                            //optionset 데이터 전송

                            var temp = !string.IsNullOrEmpty(dr["Reason"].ToString()) ? dr["Reason"].ToString() : "";



                            if (temp == "분실")
                            {
                                gwCard["new_p_reason"] = new OptionSetValue(100000000);
                            }
                            else if (temp == "마그네틱 손상")
                            {
                                gwCard["new_p_reason"] = new OptionSetValue(100000001);
                            }
                            else {
                                gwCard["new_p_reason"] = new OptionSetValue(100000002);
                            }

                            if (dr["FORM_NAME"].ToString() == "법인카드 변경 요청서") {
                                gwCard["new_p_reason"] = null;
                            }




                            temp = !string.IsNullOrEmpty(dr["PI_BUSINESS_STATENAME"].ToString()) ? dr["PI_BUSINESS_STATENAME"].ToString() : "";

                            if (temp == "승인")
                            {
                                gwCard["new_p_state"] = new OptionSetValue(100000000);
                            }
                            else if (temp == "반려")
                            {
                                gwCard["new_p_state"] = new OptionSetValue(100000001);
                            }
                            else if (temp == "진행")
                            {
                                gwCard["new_p_state"] = new OptionSetValue(100000002);
                            }
                            else if (temp == "기안취소") {

                                gwCard["new_p_state"] = new OptionSetValue(100000003);
                            }


                            Console.WriteLine("optionset전송완료");
                            //Look Up data 전송
                            EntityReference initID = retEntityRefer(service, dr, "contact", "new_txt_companynum", "INITIATOR_ID");

                            gwCard["new_l_initiator_ou"] = retEntityRefer(service, dr, "new_department", "new_txt_gw_code", "INITIATOR_OU_ID");
                            gwCard["new_l_initiator_id"] = initID;
                         
                            Entity contact = service.Retrieve("contact", initID.Id, new ColumnSet("fullname", "new_txt_team"));
                            gwCard["new_txt_initiator_ou"] = contact["new_txt_team"].ToString();
                            gwCard["new_txt_initiator_id"] = contact["fullname"].ToString();


                            Console.WriteLine("Lookup 전송완료");
                            //Date data 전송
                            DateTime temp_datetime;

                            if (DateTime.TryParse(dr["INITIATED_DATE"].ToString(), out temp_datetime))
                            {
                                gwCard["new_dt_initiated_date"] = temp_datetime;
                            }
                            else
                            {
                                gwCard["new_dt_initiated_date"] = null;
                            }

                            if (DateTime.TryParse(dr["COMPLETED_DATE"].ToString(), out temp_datetime))
                            {
                                gwCard["new_dt_completed_date"] = temp_datetime;
                            }
                            else
                            {
                                gwCard["new_dt_completed_date"] = null;
                            }

                            Console.WriteLine("Date 전송완료");




                            if (type == 0)
                            {
                                service.Create(gwCard);
                            }
                            else if (type == 1)
                            {

                                Guid retGuid = retID(service, dr, "new_form_legacy", "new_txt_form_inst_id", "FORM_INST_ID");

                                if (retGuid == null)
                                {
                                    //error guid is null..
                                    Console.WriteLine("error - guid is null");
                                }
                                gwCard.Id = retGuid;
                                service.Update(gwCard);
                            }

                        }
                        catch (Exception e)
                        {
                            //Upsert문제 
                        }
                    }
                }
            }
            catch (Exception e)
            {
                //service 문제
            }


        }



        //attribute 에서 condition 이 있으면 개네들 뽑기. 
        //여러개 있으면 하나만 뽑기 - 마지막 녀석 ( Unique 할 때 쓸려고 만든 함수 )
        // entityname -조회할 엔티티명
        // attribute - 조회할 어트리뷰트
        // condition - 같아야 되는 값

        // To Do 2019 10 30 
        public static DataRow insertDataRowTableNumber1(DataRow inDataRow,string insertText, int insertNumber,string type) {

         
            if (insertNumber == 1) { 

                if(type == "코스트센터") inDataRow["코스트센터"] = insertText;
                else if(type =="WBS") inDataRow["WBS요소"] = insertText;
            }
            if (insertNumber == 2) { 
                if(type =="코스트 센터") inDataRow["코스트센터명"] = insertText;
                else if(type =="WBS") inDataRow["WBS요소명"] = insertText;
            }

            if (insertNumber == 3) inDataRow["원가요소"] = insertText;
            if (insertNumber == 4) inDataRow["원가요소명"] = insertText;
            if (insertNumber == 5) inDataRow["전용기간"] = insertText;
            if (insertNumber == 6) inDataRow["전용 가능금액"] = insertText;
            if (insertNumber == 7) inDataRow["전용 신청금액"] = insertText;
            if (insertNumber == 8) inDataRow["거래통화"] = insertText;
          
            return inDataRow;

        }

        public static DataRow insertDataRowTableNumber2(DataRow inDataRow, string insertText, int insertNumber, string type)
        {
   
            if (insertNumber == 1)
            {
                if (type == "코스트센터") inDataRow["코스트센터"] = insertText;
                else if (type == "WBS") inDataRow["WBS요소"] = insertText;
            }
            if (insertNumber == 2)
            {
                if (type == "코스트 센터") inDataRow["코스트센터명"] = insertText;
                else if (type == "WBS") inDataRow["WBS요소명"] = insertText;
            }
            if (insertNumber == 3) inDataRow["리시버 원가요소"] = insertText;
            if (insertNumber == 4) inDataRow["원가요소명"] = insertText;
            if (insertNumber == 5) inDataRow["리시버 기간"] = insertText;
            if (insertNumber == 6) inDataRow["리시버 편성예산"] = insertText;
            if (insertNumber == 7) inDataRow["리시버 배정금액"] = insertText;
            if (insertNumber == 8) inDataRow["리시버 실적"] = insertText;

            return inDataRow;

        }

        public static DataSet ReturnDataset(string strProcedure,SqlParameter[] param)
        {

        
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbcon"].ConnectionString))
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



        public static DataSet ReturnDataset(string strProcedure)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbcon"].ConnectionString))
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

        public static IOrganizationService GetCrmService()
        {
          
                
            //app.config 읽게 하기
            Uri orgUri = new Uri(ConfigurationManager.AppSettings["crmURL"]);
           
            ClientCredentials credentials = new ClientCredentials();

            credentials.UserName.UserName = ConfigurationManager.AppSettings["crmID"].ToString();
            credentials.UserName.Password = ConfigurationManager.AppSettings["crmPW"].ToString();

            OrganizationServiceProxy proxy = new OrganizationServiceProxy(orgUri, null, credentials, null);
            IOrganizationService service = (IOrganizationService)proxy;

            return service;
        }
       
        public static Guid retID(IOrganizationService service, DataRow dr,string entityName,string filedName,string id)
        {

            QueryExpression qe = new QueryExpression(entityName);

            ConditionExpression condition = new ConditionExpression();
            condition.AttributeName = filedName;
            condition.Operator = ConditionOperator.Equal;
            condition.Values.Add(dr[id].ToString());

            FilterExpression filter = new FilterExpression();
            filter.Conditions.Add(condition);
            qe.Criteria.AddFilter(filter);

            EntityCollection mulRet = service.RetrieveMultiple(qe);


            foreach (Entity contact in mulRet.Entities)
            {
                return contact.Id;
            }

            return Guid.Empty;

        }
        //Retrieve Multiple 효과
        public static EntityReference retEntityRefer(IOrganizationService service, DataRow dr, string entityName, string entityFieldName, string drFieldName)
        {
            QueryExpression qe = new QueryExpression(entityName);

            ConditionExpression condition = new ConditionExpression();
            condition.AttributeName = entityFieldName;

            condition.Operator = ConditionOperator.Equal;
            condition.Values.Add(dr[drFieldName].ToString());

            FilterExpression filter = new FilterExpression();
            filter.Conditions.Add(condition);
            qe.Criteria.AddFilter(filter);

            EntityCollection mulRet = service.RetrieveMultiple(qe);

            //id를 가져옴
            Guid recordId = new Guid();
            foreach (Entity row in mulRet.Entities)
            {

                recordId = row.Id;


            }
            return new EntityReference(entityName, recordId);

        }


    }
}
