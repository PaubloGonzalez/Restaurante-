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
    public class CursoModel
    {

        public List<CursoEnt> ConsultaCursos()
        {
            using (var client = new HttpClient())
            {
                string token = HttpContext.Current.Session["Token"].ToString();
                string url = ConfigurationManager.AppSettings["urlApi"].ToString() + "api/ConsultaCursos";

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage resp = client.GetAsync(url).Result;

                if (resp.IsSuccessStatusCode)
                {
                    return resp.Content.ReadFromJsonAsync<List<CursoEnt>>().Result;
                }

                return new List<CursoEnt>();
            }
        }

        public CursoEnt ConsultaCurso(long q)
        {
            using (var client = new HttpClient())
            {
                string token = HttpContext.Current.Session["Token"].ToString();
                string url = ConfigurationManager.AppSettings["urlApi"].ToString() + "api/ConsultaCurso?q=" + q;

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage resp = client.GetAsync(url).Result;

                if (resp.IsSuccessStatusCode)
                {
                    return resp.Content.ReadFromJsonAsync<CursoEnt>().Result;
                }

                return null;
            }
        }

        public List<CarritoEnt> consultarCarrito(long q)
        {
            using (var client = new HttpClient())
            {
                string token = HttpContext.Current.Session["Token"].ToString();
                string url = ConfigurationManager.AppSettings["urlApi"].ToString() + "api/consultarCarrito?q=" + q;

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage resp = client.GetAsync(url).Result;

                if (resp.IsSuccessStatusCode)
                {
                    return resp.Content.ReadFromJsonAsync<List<CarritoEnt>>().Result;
                }

                return new List<CarritoEnt>();
            }
        }

        public List<CarritoEnt> consultarMisCursos(long q)
        {
            using (var client = new HttpClient())
            {
                string token = HttpContext.Current.Session["Token"].ToString();
                string url = ConfigurationManager.AppSettings["urlApi"].ToString() + "api/consultarMisCursos?q=" + q;

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage resp = client.GetAsync(url).Result;

                if (resp.IsSuccessStatusCode)
                {
                    return resp.Content.ReadFromJsonAsync<List<CarritoEnt>>().Result;
                }

                return new List<CarritoEnt>();
            }
        }

        public int AgregarCursoCarrito(CarritoEnt entidad)
        {
            using (var client = new HttpClient())
            {
                string token = HttpContext.Current.Session["Token"].ToString();
                string url = ConfigurationManager.AppSettings["urlApi"].ToString() + "api/AgregarCursoCarrito";
                JsonContent body = JsonContent.Create(entidad); //Serializar

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage resp = client.PostAsync(url, body).Result;

                if (resp.IsSuccessStatusCode)
                {
                    return resp.Content.ReadFromJsonAsync<int>().Result;
                }

                return 0;
            }
        }

        public int RemoverCursoCarrito(long q)
        {
            using (var client = new HttpClient())
            {
                string token = HttpContext.Current.Session["Token"].ToString();
                string url = ConfigurationManager.AppSettings["urlApi"].ToString() + "api/RemoverCursoCarrito?q=" + q;

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage resp = client.DeleteAsync(url).Result;

                if (resp.IsSuccessStatusCode)
                {
                    return resp.Content.ReadFromJsonAsync<int>().Result;
                }

                return 0;
            }
        }

        public int ConfirmarPagoCarrito(CarritoEnt entidad)
        {
            using (var client = new HttpClient())
            {
                string token = HttpContext.Current.Session["Token"].ToString();
                string url = ConfigurationManager.AppSettings["urlApi"].ToString() + "api/ConfirmarPagoCarrito";
                JsonContent body = JsonContent.Create(entidad); //Serializar

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage resp = client.PostAsync(url, body).Result;

                if (resp.IsSuccessStatusCode)
                {
                    return resp.Content.ReadFromJsonAsync<int>().Result;
                }

                return 0;
            }
        }

        public long RegistrarCurso(CursoEnt entidad)
        {
            using (var client = new HttpClient())
            {
                string token = HttpContext.Current.Session["Token"].ToString();
                string url = ConfigurationManager.AppSettings["urlApi"].ToString() + "api/RegistrarCurso";
                JsonContent body = JsonContent.Create(entidad); //Serializar

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage resp = client.PostAsync(url, body).Result;

                if (resp.IsSuccessStatusCode)
                {
                    return resp.Content.ReadFromJsonAsync<long>().Result;
                }

                return 0;
            }
        }

        public void ActualizarRutaCurso(CursoEnt entidad)
        {
            using (var client = new HttpClient())
            {
                string token = HttpContext.Current.Session["Token"].ToString();
                string url = ConfigurationManager.AppSettings["urlApi"].ToString() + "api/ActualizarRutaCurso";
                JsonContent body = JsonContent.Create(entidad); //Serializar

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage resp = client.PutAsync(url, body).Result;
            }
        }

        public void ActualizarCurso(CursoEnt entidad)
        {
            using (var client = new HttpClient())
            {
                string token = HttpContext.Current.Session["Token"].ToString();
                string url = ConfigurationManager.AppSettings["urlApi"].ToString() + "api/ActualizarCurso";
                JsonContent body = JsonContent.Create(entidad); //Serializar

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage resp = client.PutAsync(url, body).Result;
            }
        }

    }
}