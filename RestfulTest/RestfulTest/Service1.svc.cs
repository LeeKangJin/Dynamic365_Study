using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Xml.Linq;


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
        public string GetData(InputGetDate param)
        {

            Console.WriteLine(param);
             int c = param.a + param.b;


            return c.ToString();
            //JObject json = new JObject();
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
