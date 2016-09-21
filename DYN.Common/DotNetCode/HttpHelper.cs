using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Maticsoft.Common
{
    public static class HttpHelper
    {
        public static string Get(string strUrl)
        {
            string strResult = string.Empty;

            System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(strUrl);
            request.Method = "GET";
            request.KeepAlive = true;
            request.ContentType = "application/x-www-form-urlencoded";

            System.Net.WebResponse response = null;
            System.IO.StreamReader reader = null;
            try
            {
                response = request.GetResponse();
                reader = new System.IO.StreamReader(response.GetResponseStream(), System.Text.Encoding.UTF8);
                strResult = reader.ReadToEnd();
            }
            catch (Exception e)
            {
                return e.ToString();
            }

            return strResult;
        }

        public static string Post(string strUrl, string strPost)
        {
            string strResult = string.Empty;

            System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(strUrl);
            request.Method = "POST";
            request.KeepAlive = true;
            request.ContentType = "application/x-www-form-urlencoded";

            try
            {
                using (Stream postStream = request.GetRequestStream())
                {
                    byte[] buffer = Encoding.UTF8.GetBytes(strPost);
                    postStream.Write(buffer, 0, buffer.Length);
                    postStream.Close();
                    postStream.Dispose();
                }

                System.Net.HttpWebResponse response = null;
                System.IO.StreamReader reader = null;
            
               // response = request.GetResponse();
                try
                {
                    response = (System.Net.HttpWebResponse)request.GetResponse();
                }
                catch (System.Net.WebException ex)
                {
                    response = (System.Net.HttpWebResponse)ex.Response;
                }
                reader = new System.IO.StreamReader(response.GetResponseStream(), System.Text.Encoding.UTF8);
                strResult = reader.ReadToEnd();
            }
            catch (Exception e)
            {
                return e.ToString();
            }

            return strResult;
        }
    }
}
