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
            int total_students_over_25 = 0;
            string class_list_json = GetJSON("http://challenge.broadly.com/classes");
            ClassList list = Json.Decode(class_list_json, typeof(ClassList));
            // Console.WriteLine(list.note);

            if (list != null)
            {
                foreach (string class_page in list.classes)
                {
                    int class_students_over_25;

                    string class_json = GetJSON(class_page);
                    Class local_class = Json.Decode(class_json, typeof(Class));
                    class_students_over_25 = local_class.students.Count(x => x.age >= 25);

                    // Console.WriteLine(local_class.note);

                    while (!string.IsNullOrEmpty(local_class.next))
                    {
                        class_json = GetJSON(local_class.next);
                        local_class = Json.Decode(class_json, typeof(Class));
                        // Console.WriteLine(local_class.note);

                        class_students_over_25 += local_class.students.Count(x => x.age >= 25);
                    }

                    total_students_over_25 += class_students_over_25;
                    Console.WriteLine("room: {0} SO25: {1}", local_class.room, class_students_over_25);
                }

                Console.WriteLine("total classes: {0} total SO25: {1} avg SO25: {2}",
                    list.classes.Count, total_students_over_25, total_students_over_25 / list.classes.Count);
            }

            Console.ReadKey();
        }

        private static string GetJSON(string WebAddress)
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
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
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
