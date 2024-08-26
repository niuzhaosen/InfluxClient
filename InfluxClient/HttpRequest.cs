using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace InfluxClient
{
    internal class HttpRequest
    {

        public string HttpGet(string url, string token)
        {
            try
            {
                string result;
                HttpWebRequest wbRequest = (HttpWebRequest)WebRequest.Create(url);
                wbRequest.Method = "GET";

                wbRequest.ContentType = "application/x-www-form-urlencoded";
                wbRequest.Headers.Add("Authorization", "Token " + token);
                wbRequest.Headers.Add("Content-Type", "text/plain; charset=utf-8");
                wbRequest.Headers.Add("Accept", "application/json");
                HttpWebResponse wbResponse = (HttpWebResponse)wbRequest.GetResponse();
                using (Stream responseStream = wbResponse.GetResponseStream())
                {
                    using (StreamReader sReader = new StreamReader(responseStream))
                    {
                        result = sReader.ReadToEnd();
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;

            }


        }
        public string HttpPost(string url,string token,string paramStr)
        {
            try
            {
                string result = string.Empty;
                HttpWebRequest wbRequest = (HttpWebRequest)WebRequest.Create(url);
                wbRequest.Proxy = null;
                wbRequest.Method = "POST";
                wbRequest.ContentType = "application/x-www-form-urlencoded";
                wbRequest.Headers.Add("Authorization", "Token " + token);
                wbRequest.Headers.Add("Content-Type", "text/plain; charset=utf-8");
                wbRequest.Headers.Add("Accept", "application/json");
                wbRequest.ContentLength = Encoding.UTF8.GetByteCount(paramStr);


                byte[] data = Encoding.UTF8.GetBytes(paramStr);
                using (Stream requestStream = wbRequest.GetRequestStream())
                {
                    using (StreamWriter swrite = new StreamWriter(requestStream))
                    {
                        swrite.Write(paramStr);
                    }
                }
                HttpWebResponse wbResponse = (HttpWebResponse)wbRequest.GetResponse();
                using (Stream responseStream = wbResponse.GetResponseStream())
                {
                    using (StreamReader sread = new StreamReader(responseStream))
                    {
                        result = sread.ReadToEnd();
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
