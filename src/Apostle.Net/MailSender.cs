using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace Apostle.Net
{
    internal class MailSender
    {
        private const string AuthHeaderTemplate = @"Bearer {0}";
        private const string ContentType = "application/json";

        internal static void Deliver(Mail mail)
        {
            // convert the mail to json format
            var jsonPayload = ConvertMailToJson(mail);

            // send the mail
            Send(jsonPayload);
        }

        internal static void Deliver(Queue<Mail> queue)
        {
            // convert the mail to json format
            var jsonPayload = ConvertMailQueueToJson(queue);

            // send the mail
            Send(jsonPayload);
        }

        private static string ConvertMailQueueToJson(Queue<Mail> queue)
        {
            return string.Empty;
        }

        private static string ConvertMailToJson(Mail mail)
        {
            var json = JsonConvert.SerializeObject(mail);
            return json;
        }

        private static HttpWebRequest CreateWebRequest()
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(Apostle.DeliveryHost.Value);
            httpWebRequest.ContentType = ContentType;
            httpWebRequest.Method = "POST";

            httpWebRequest.Headers.Add("Authorization", string.Format(AuthHeaderTemplate, Apostle.DomainKey));

            return httpWebRequest;
        }

        private static void Send(string jsonPayload)
        {
            var request = CreateWebRequest();

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(jsonPayload);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var httpResponse = (HttpWebResponse)request.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
            }
        }
    }
}