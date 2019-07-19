
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System.ServiceModel.Description;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk.Query;

struct BarcodeNumber {
    public string entity_name;

    public string barcord_name;
    public EntityReference barcord_project;
    public EntityReference barcord_create_name;
    public OptionSetValue barcord_purpose;
}
struct BarcodeCreate {
    public string entity_name;

    public string barcord_name;
    public EntityReference barcord_project;
    public OptionSetValue barcord_purpose;
    public int barcord_count;


}

struct Comparator { }



namespace CRMConsoleTest
{
    class Program
    {
        public static Guid barcodeCreateCreate(IOrganizationService service, BarcodeCreate barcode_info) {



            Console.Write("바코드 생성 생성");
            Entity new_barcode = new Entity(barcode_info.entity_name);



            new_barcode["new_name"] = barcode_info.barcord_name;
            new_barcode["new_l_project"] = barcode_info.barcord_project;
            new_barcode["new_p_object"] = barcode_info.barcord_purpose;
            new_barcode["new_i_count"] = barcode_info.barcord_count;


            Console.Write("바코드 생성 종료");
            return service.Create(new_barcode);
        }
        public static void barcodeNumberCreate(IOrganizationService service , BarcodeNumber barcode_info) {


            Console.Write("바코드 번호 생성");
            Entity new_barcode1 = new Entity("new_barcode");
            new_barcode1.Attributes.Add("new_name", barcode_info.barcord_name);
            new_barcode1["new_l_create_barcode"] = barcode_info.barcord_create_name;
            new_barcode1["new_l_project"] = barcode_info.barcord_project;
            new_barcode1["new_p_object"] = barcode_info.barcord_purpose;

            service.Create(new_barcode1);


            Console.Write("바코드 번호 생성 종료");
        }

        //변경 후 디버깅 용
        public static void updateProject(IOrganizationService service, Guid id,int k) {

            
            Entity new_barcode2 = new Entity("new_barcode");

            var retrievedBarcode = new Entity("new_barcode", new Guid("8F7200C8-3AA9-E911-80E5-00155D012E07"));
            EntityReference t = new EntityReference();
            t.Name = "CTP" + k;
            new_barcode2.Attributes["new_l_projoect"] = t;

            service.Update(new_barcode2);


        }

        public static void update(IOrganizationService service, BarcodeNumber barcoder_info) {

            Console.WriteLine("바코드 업데이트 시작");

            Entity new_barcode2 = new Entity("new_barcode");

            var retrievedBarcode = new Entity("new_barcode", new Guid("8F7200C8-3AA9-E911-80E5-00155D012E07"));



            new_barcode2.Id = retrievedBarcode.Id;

            System.DateTime.Now.ToString("yyyy");

            string name = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");

            new_barcode2["new_name"] = name;

            service.Update(new_barcode2);


            Console.WriteLine("바코드 업데이트 종료");


        }

        public static void retrieve(IOrganizationService service, Guid id) {


            ColumnSet col = new ColumnSet(new String[] { "new_name", "new_l_create_barcode", "new_l_project", "new_p_object" });

            //ColumnSet col = new ColumnSet("new_name");

            var ret = service.Retrieve("new_barcode", new Guid("8F7200C8-3AA9-E911-80E5-00155D012E07"), col);


            string resultStr = "";

 

            if (ret != null)
            {
                if (ret.Attributes.Contains("new_name"))
                {
                    resultStr += ret.Attributes["new_name"].ToString();

                    resultStr += "_";
                }

                if (ret.Attributes.Contains("new_l_project"))
                {
                    resultStr += ((EntityReference)ret.Attributes["new_l_project"]).Name.ToString();
                    resultStr += "_";
                }



                //if (ret.Attributes.Contains("new_l_create_barcode"))
                //{
                //    resultStr += ((EntityReference)ret.Attributes["new_l_create_barcode"]);
                //    resultStr 
                //        += "   /   ";
                //}
                

                if (ret.Attributes.Contains("new_p_object"))
                {
                  
                    resultStr += ((OptionSetValue)ret.Attributes["new_p_object"]).Value.ToString();

                    resultStr += "(";
                    resultStr += ret.FormattedValues["new_p_object"].ToString();
                    resultStr += ")";
                }
                Console.WriteLine(resultStr);

            }
            else
            {

                Console.WriteLine("조회된 결과가 없습니다");
             }


        }

        public static void retrieveMul(IOrganizationService service) {


            QueryExpression qe = new QueryExpression("new_barcode");
            qe.ColumnSet.AddColumns("new_name", "new_l_create_barcode", "new_l_project", "new_p_object");

            var mulRet = service.RetrieveMultiple(qe);


          
            foreach (var i in mulRet.Entities) {

                string barcord_name = "";

                if (i.Attributes["new_name"].ToString() == "A") {




                    Console.Write(i.Attributes["new_name"].ToString());

                    Console.Write("_");

                    EntityReference temp = (EntityReference)i.Attributes["new_l_project"];


                    Console.Write(temp.Name);


                    Console.Write("_");

                    OptionSetValue temp2 = (OptionSetValue)i.Attributes["new_p_object"];

                    Console.Write("(" + i.FormattedValues["new_p_object"].ToString() + ")");
                    Console.Write(temp2.Value);

                    Console.Write("\n");

                }


            }


        }
        public static void Delete(IOrganizationService service, Guid id) {

            Console.Write("삭제를 시작합니다.");
            service.Delete("new_barcode", id);
            Console.Write("삭제가 완료되었습니다.");

        }


        static void Main(string[] args)
        {

         
            Console.WriteLine("HelloWorld!");
            try
            {
                IOrganizationService service = GetCrmService();




                System.DateTime.Now.ToString("yyyy");
                string curTime = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");


                //바코드 생성 정보 입력
                BarcodeCreate barcode_create_info = new BarcodeCreate();

                barcode_create_info.entity_name = "new_create_barcode";
                barcode_create_info.barcord_name = curTime.ToString()+ "_이강진";
                barcode_create_info.barcord_project = new EntityReference("new_project", new Guid("c4e62314-09a9-e911-80e5-00155d012e07"));
                barcode_create_info.barcord_purpose = new OptionSetValue(100000000);
                barcode_create_info.barcord_count = 15;

                Guid barcodeGUID = barcodeCreateCreate(service, barcode_create_info);

                //바코드 번호 정보 입력
                BarcodeNumber barcord_number_info = new BarcodeNumber();

                barcord_number_info.entity_name = "new_barcode";
                barcord_number_info.barcord_create_name = new EntityReference("new_create_barcode",barcodeGUID);
                barcord_number_info.barcord_purpose = barcode_create_info.barcord_purpose;
                barcord_number_info.barcord_project = barcode_create_info.barcord_project;
               


                for (int i = 0; i < barcode_create_info.barcord_count; i++) {


                    barcord_number_info.barcord_name = i.ToString();
                    barcodeNumberCreate(service, barcord_number_info);

                }





                //retrieve(service, new Guid("8F7200C8-3AA9-E911-80E5-00155D012E07"));

                //retrieveMul(service);




                #region Delete


                #endregion



            }
            catch (Exception e) {

                
                Console.WriteLine(e.ToString());
            }







            ////recode delete

            // service.Delete("new_name",guid);



            //C # Grammer

            //ctrl +k+c 전체 주석
            //ctrl +k+u 전체 주석 해제


            //String vs string 
            // String : 클래스 사용할 때 
            // string : 단순 text 사용할 때 
            // 성능면에서 유리한거지 기능은 똑같음



            Console.ReadLine();


        }

        public static IOrganizationService GetCrmService()
        {
            Uri orgUri = new Uri("https://rmp.celltrion.com:447/XRMServices/2011/Organization.svc");
            ClientCredentials credentials = new ClientCredentials();
            credentials.UserName.UserName = "rmp@celltrion.com";
            credentials.UserName.Password = "celltrion0!";

            OrganizationServiceProxy proxy = new OrganizationServiceProxy(orgUri, null, credentials, null);
            IOrganizationService service = (IOrganizationService)proxy;

            return service;
        }

    }
}

