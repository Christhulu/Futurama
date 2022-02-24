using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Futurama2
{
    class Program
    {
        static void Main(string[] args)
        {

            string futuramaUrl= String.Format("http://futuramaapi.herokuapp.com/api/quotes/30");
            WebRequest requestObjGet = WebRequest.Create(futuramaUrl);
            requestObjGet.Method = "GET";
            HttpWebResponse responseObjGet = null;

            /* Cast WebRequest to HttpWebResponse*/
            responseObjGet = (HttpWebResponse)requestObjGet.GetResponse();

            string futuramaResult = null;

            //Response object is in a stream rn
            //Import system.io to read
            using (Stream stream = responseObjGet.GetResponseStream())
            {
                StreamReader sr = new StreamReader(stream);
                futuramaResult = sr.ReadToEnd();
                sr.Close();
            }

            //Serialization
            var serializer = new JavaScriptSerializer();
            List<QuoteObject> quoteList = (List<QuoteObject>)serializer.Deserialize(futuramaResult, typeof(List<QuoteObject>));

            int item = 1;
            foreach(QuoteObject quoteObj in quoteList)
            {

                Console.WriteLine("Quote {0}: ", item);
                Console.WriteLine(quoteObj.character);
                Console.WriteLine(quoteObj.quote);
                Console.WriteLine(quoteObj.image);
                Console.WriteLine();

                item++;

            }

            //Console keeps closing early and it's annoying
            string v = Console.ReadLine();

        }
    }
}
