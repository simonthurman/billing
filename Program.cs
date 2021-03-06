﻿using System;
using System.IO;
using System.Net;

namespace Billing
{
    class Program
    {
        static void Main(string[] args)
        {
            String authurl = "https://login.microsoftonline.com/common/oauth2/authorize";
            WebRequest myAuthRequest = WebRequest.Create(authurl);
            WebResponse authRespone = myAuthRequest.GetResponse();


            //String myurl = "https://management.azure.com/providers/Microsoft.Billing/enrollmentAccounts/83294316/providers/Microsoft.Consumption/usageDetails?api-version=2019-10-01";
            String myurl = "https://consumption.azure.com/v3/enrollments/83294316/usagedetails/download?startTime=2020-01-01&endTime=2020-01-02";


            Console.WriteLine("Enter token: ");
            String token = Console.ReadLine();

            WebRequest myRequest = WebRequest.Create(myurl);
            //myRequest.Credentials = CredentialCache.DefaultCredentials;
            myRequest.Headers.Add(HttpRequestHeader.Authorization, "Bearer " + token);
            myRequest.ContentType ="application/json";


            WebResponse response = myRequest.GetResponse();
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);

            String path = @"c:\demo\consumption.json";

            //FileStream myStream = File.Create(path);
            StreamWriter myWriter = new StreamWriter(path);

            using (Stream billingStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(billingStream);
                string responseFromServer = reader.ReadToEnd();
                myWriter.WriteLine(responseFromServer);
                //Console.WriteLine(responseFromServer);
            }
            Console.WriteLine("Finished");
        }
    }
}
