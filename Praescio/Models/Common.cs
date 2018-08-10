using Praescio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web;

namespace Praescio.Models
{
    public static class Common
    {
        public static Mst_Account ACCOUNT
        {
            get { return (Mst_Account)HttpContext.Current.Session[Constant.ACCOUNT]; }
            set { HttpContext.Current.Session[Constant.ACCOUNT] = value; }
        }

        public static Mst_Account PARENTACCOUNT
        {
            get { return (Mst_Account)HttpContext.Current.Session[Constant.PARENTACCOUNT]; }
            set { HttpContext.Current.Session[Constant.PARENTACCOUNT] = value; }
        }

        public static Mst_Account STUDENTACCOUNT
        {
            get { return (Mst_Account)HttpContext.Current.Session[Constant.STUDENTACCOUNT]; }
            set { HttpContext.Current.Session[Constant.STUDENTACCOUNT] = value; }
        }

        public static string baseUrl = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["BaseUrl"]);


    }

    public static class Constant
    {
        public const string ACCOUNT = "ACCOUNT";
        public const string PARENTACCOUNT = "PARENTACCOUNT";
        public const string STUDENTACCOUNT = "STUDENTACCOUNT";

    }
    public static class CommonGenerics
    {
        static HttpClient _client = new HttpClient()
        {
            BaseAddress = new Uri(Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["BaseUrl"]))
        };

        public static HttpResponseMessage PostAsynch<T>(string url, T model)
        {
            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //_client.DefaultRequestHeaders.Add("tokenUserAuth", "aliahmedk");
            _client.DefaultRequestHeaders.Add("tokenUserAuth", Convert.ToString(HttpContext.Current.Session["UserID"]).ToLower());
            HttpResponseMessage responseMessage = _client.PostAsync(url, model, new JsonMediaTypeFormatter()).Result;
            return responseMessage;
        }

        public static HttpResponseMessage PutAsynch<T>(string url, T model)
        {
            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _client.DefaultRequestHeaders.Add("tokenUserAuth", Convert.ToString(HttpContext.Current.Session["UserID"]).ToLower());
            HttpResponseMessage responseMessage = _client.PutAsync(url, model, new JsonMediaTypeFormatter()).Result;//.PutAsynch(url, model, new JsonMediaTypeFormatter()).Result;
            return responseMessage;
        }

        public static HttpResponseMessage GetAsynch(string url)
        {
            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _client.DefaultRequestHeaders.Add("tokenUserAuth", Convert.ToString(HttpContext.Current.Session["UserID"]).ToLower());
            HttpResponseMessage responseMessage = _client.GetAsync(url).Result;
            return responseMessage;
        }

        public static HttpResponseMessage DeleteAsynch(string url)
        {
            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _client.DefaultRequestHeaders.Add("tokenUserAuth", Convert.ToString(HttpContext.Current.Session["UserID"]).ToLower());
            HttpResponseMessage responseMessage = _client.DeleteAsync(url).Result;
            return responseMessage;
        }

        //public static T GetAsynch<T>(string url)
        //{
        //    HttpResponseMessage responseMessage = CommonGenerics.GetAsynch(url);
        //    if (responseMessage.IsSuccessStatusCode)
        //    {
        //        var responseData = responseMessage.Content.ReadAsStringAsync().Result;
        //        var list = JsonConvert.DeserializeObject<T>(responseData);
        //        return list;
        //    }
        //    return default(T);
        //}
    }
}