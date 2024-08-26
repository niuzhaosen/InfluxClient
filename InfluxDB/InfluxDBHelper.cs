using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace InfluxDB
{
    public class InfluxDBHelper
    {
        static string _baseAddress;  //地址
        static string _token;  //token
       
        static string _database;  //数据库名
        static string _bucket;  //数据库名

        /// <summary>
        /// 构造函数
        /// </summary>
        public InfluxDBHelper()
        {

            _baseAddress = "http://127.0.0.1:8086/api/v2";
            _token = "bZeUVk5A1kc-riK5XRhPBE4aI7FiJ625VdB2ymMF5lRadV0xiiCkpFr71567nqGnolt9fxq3z8JC2MDGKas6Sw==";
            _database = "test1";
            _bucket = "test1";
        }

        /// <summary>
        /// 读
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public string Query(string sql)
        {
            string pathAndQuery = string.Format("/query?org={0}&bucket={1}&db={2}&rp=autogen&q={3}", _database,_bucket,_bucket, sql);
            string url = "http://127.0.0.1:8086" + pathAndQuery;

            string result = HttpHelperGet(url);
            return result;
        }

        /// <summary>
        /// 写
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public string Write(string paramStr)
        {
            string pathAndQuery = string.Format("/write?org={0}&bucket={1}&precision=ns", _database,_bucket);
            string url = _baseAddress + pathAndQuery;

            string result = HttpHelperPost(url, paramStr);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string HttpHelperGet(string uri)
        {
            try
            {
                string result = string.Empty;
                HttpWebRequest wbRequest = (HttpWebRequest)WebRequest.Create(uri);               
                wbRequest.Method = "GET";

                wbRequest.ContentType = "application/x-www-form-urlencoded";
                wbRequest.Headers.Add("Authorization", "Token " + _token);
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="paramStr"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string HttpHelperPost(string uri, string paramStr)
        {
            try
            {
                string result = string.Empty;
                HttpWebRequest wbRequest = (HttpWebRequest)WebRequest.Create(uri);
                wbRequest.Proxy = null;
                wbRequest.Method = "POST";
                wbRequest.ContentType = "application/x-www-form-urlencoded";
                wbRequest.Headers.Add("Authorization", "Token "+ _token);
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