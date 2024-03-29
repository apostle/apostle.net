﻿using Apostle.Enums;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Reflection;

namespace Apostle
{
    internal class MailSender
    {
        private const string AuthHeaderTemplate = @"Bearer {0}";
        private const string ContentType = "application/json";

        //internal static DeliveryResult Deliver(Mail mail)
        //{
        //    // convert the mail to json format
        //    var jsonPayload = ConvertMailToJson(mail);

        //    // send the mail
        //    return Send(jsonPayload);
        //}

        internal static DeliveryResult Deliver(Queue queue)
        {
            // convert the mail to json format
            var jsonPayload = ConvertMailQueueToJson(queue);

            // send the mail
            return Send(jsonPayload);
        }

        private static string ConvertMailQueueToJson(Queue queue)
        {
            var json = JsonConvert.SerializeObject(queue, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            return json;
        }

        //private static string ConvertMailToJson(Mail mail)
        //{
        //    var json = JsonConvert.SerializeObject(mail);
        //    return json;
        //}

        private static HttpWebRequest CreateWebRequest()
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(Apostle.DeliveryHost.Value);
            httpWebRequest.ContentType = ContentType;
            httpWebRequest.Method = "POST";

            var currentVersion = Assembly.GetCallingAssembly().GetName().Version;
            var semVer = string.Concat(currentVersion.Major, ".", currentVersion.Minor, ".", currentVersion.Build);

            httpWebRequest.Headers.Add("Authorization", string.Format(AuthHeaderTemplate, Apostle.DomainKey));
            httpWebRequest.Headers.Add("Apostle-Client", string.Concat(".Net/", semVer));

            return httpWebRequest;
        }

        private static DeliveryResult Send(string jsonPayload)
        {
            var result = new DeliveryResult { JsonRequest = jsonPayload, Status = DeliveryStatus.Error };

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
                var responseBody = streamReader.ReadToEnd();
                dynamic response = JsonConvert.DeserializeObject(responseBody);
                result.ApiResponse = responseBody;
                result.Status = response.status.Value.ToLowerInvariant().Equals("queued") ? DeliveryStatus.Queued : DeliveryStatus.Error;

                result.Message = response.message != null ? response.message.Value : string.Empty;
                result.RequestData = response.request_data != null ? response.request_data.Value : string.Empty;
            }

            return result;
        }
    }
}