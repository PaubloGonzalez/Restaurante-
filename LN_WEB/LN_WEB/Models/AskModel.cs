using LN_WEB.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Web;

namespace LN_WEB.Models
{
    public class AskModel
    {
        public List<AskEnt> CheckAsks()
        {
            using (var client = new HttpClient())
            {
                string token = HttpContext.Current.Session["Token"].ToString();
                string url = ConfigurationManager.AppSettings["urlApi"].ToString() + "api/CheckAsks";

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage resp = client.GetAsync(url).Result;

                if (resp.IsSuccessStatusCode)
                {
                    return resp.Content.ReadFromJsonAsync<List<AskEnt>>().Result;
                }

                return new List<AskEnt>();
            }
        }


        public AskEnt CheckAsk(long q)
        {
            using (var client = new HttpClient())
            {
                string token = HttpContext.Current.Session["Token"].ToString();
                string url = ConfigurationManager.AppSettings["urlApi"].ToString() + "api/CheckAsk?q=" + q;

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage resp = client.GetAsync(url).Result;

                if (resp.IsSuccessStatusCode)
                {
                    return resp.Content.ReadFromJsonAsync<AskEnt>().Result;
                }

                return null;
            }
        }


 


        public int RegisterAsk(AskEnt entidad)
        {
            using (var client = new HttpClient())
            {

                string url = ConfigurationManager.AppSettings["urlApi"].ToString() + "api/RegisterAsk";
                JsonContent body = JsonContent.Create(entidad); //Serializar
                HttpResponseMessage resp = client.PostAsync(url, body).Result;

                if (resp.IsSuccessStatusCode)
                {
                    return resp.Content.ReadFromJsonAsync<int>().Result;
                }

                return 0;
            }
        }


        public int RemoveAsk(long q)
        {
            using (var client = new HttpClient())
            {
                string token = HttpContext.Current.Session["Token"].ToString();
                string url = ConfigurationManager.AppSettings["urlApi"].ToString() + "api/RemoveAsk?q=" + q;

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage resp = client.DeleteAsync(url).Result;

                if (resp.IsSuccessStatusCode)
                {
                    return resp.Content.ReadFromJsonAsync<int>().Result;
                }

                return 0;
            }
        }

        public void UpdateAsk(AskEnt entidad)
        {
            using (var client = new HttpClient())
            {
                string url = ConfigurationManager.AppSettings["urlApi"].ToString() + "api/UpdateAsk";
                JsonContent body = JsonContent.Create(entidad); //Serializar

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer");
                HttpResponseMessage resp = client.PutAsync(url, body).Result;
            }
        }

    }
}