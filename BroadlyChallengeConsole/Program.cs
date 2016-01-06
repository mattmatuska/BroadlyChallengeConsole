using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace BroadlyChallengeConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Func<string, string> get_json = web_address => GetJsonWebRequest(web_address);
                Func<string, Class> decode_json = json_text => Json.Decode(json_text, typeof(Class));

                string class_list_json = get_json.Invoke(("http://challenge.broadly.com/classes"));
                ClassList list = Json.Decode(class_list_json, typeof(ClassList));

                Console.WriteLine("{0}", list.Classes.Average(s => GetNumStudents(s, get_json, decode_json)));
            }
            catch (Exception e) { Console.WriteLine(e.ToString()); }
            
            
            Console.ReadKey();
        }

        /// <summary>
        /// Read the class from the file.
        /// </summary>
        /// <param name="ClassPage">HTTP address of the page.</param>
        /// <returns></returns>
        private static int GetNumStudents(string ClassPage, Func<string, string> GetJson, Func<string, Class> DecodeJson)
        {
            if (!string.IsNullOrEmpty(ClassPage))
            {
                try
                {
                    string class_json = GetJson(ClassPage);
                    Class local_class = Json.Decode(class_json, typeof(Class));

                    return local_class.Students.Count(x => x.Age >= 25) +
                        GetNumStudents(local_class.Next, GetJson, DecodeJson);
                }
                catch (Exception e) { Console.WriteLine(e.ToString()); }
            }

            return 0;
        }

        /// <summary>
        /// Get the JSON content from the webpage. 
        /// </summary>
        /// <param name="WebAddress"></param>
        /// <returns></returns>
        private static string GetJsonWebRequest(string WebAddress)
        {
            // code borrowed from:
            // https://msdn.microsoft.com/en-us/library/456dfw4f(v=vs.110).aspx

            // Create a request for the URL. 
            WebRequest request = WebRequest.Create(WebAddress);
            // If required by the server, set the credentials.
            request.Credentials = CredentialCache.DefaultCredentials;
            // Get the response.
            WebResponse response = request.GetResponse();
            // Display the status.
            //Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            // Get the stream containing content returned by the server.
            Stream dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.
            string responseFromServer = reader.ReadToEnd();
            // Display the content.
            //Console.WriteLine(responseFromServer);
            // Clean up the streams and the response.
            reader.Close();
            response.Close();

            return responseFromServer;
        }
    }
}
