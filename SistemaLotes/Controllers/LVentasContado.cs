using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaLotes.Models;
using System.Data;

using Newtonsoft.Json;
using System.Net;

using System;
using System.Collections.Generic;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using System.Net.Http.Headers;
using RestSharp;
namespace SistemaLotes.Controllers

{

    public class LVentasContado : Controller
    {

        public static byte[] imagenVentasConatdo = new byte[0];
        private readonly db _ventascontado = null;
        public static int idusuariosU;
        public LVentasContado(db context)
        {
            _ventascontado = context;

        }

        public IActionResult Index()
        {
            DataTable dt;
            DataTable dtLotes;
            DataTable dtetapas;
            DataTable dtcliente;
            List<entidad> entidad = new List<entidad>();
            dt = _ventascontado.SP_LISTARVENTASCONTADO();
            for (int i = 0; i < dt.Rows.Count; i++)
            {


                entidad entidad1 = new entidad();

                entidad1.idventas = dt.Rows[i][0].GetHashCode();
                entidad1.nombre = dt.Rows[i][1].ToString();
                entidad1.preciocontado = Convert.ToDecimal(dt.Rows[i][2]);
                entidad1.nombrecliente = dt.Rows[i][5].ToString();
                entidad1.nombrelotes = dt.Rows[i][7].ToString();
                entidad1.fechaventa = (DateTime)dt.Rows[i][8];
                entidad1.NombreEtapa = dt.Rows[i][9].ToString();
                entidad.Add(entidad1);
                ViewBag.entidad1 = entidad;


            }

            List<entidad> entidad2 = new List<entidad>();
            dtetapas = _ventascontado.SP_ListarEtapasSelectItem();
            for (int i = 0; i < dtetapas.Rows.Count; i++)
            {
                entidad entidad3 = new entidad();
                entidad3.idetapas = dtetapas.Rows[i][0].GetHashCode();
                entidad3.NombreEtapa = dtetapas.Rows[i][1].ToString();
                entidad2.Add(entidad3);
            }


            List<SelectListItem> items = entidad2.ConvertAll(d =>
            {
                return new SelectListItem()
                {

                    Text = d.NombreEtapa.ToString(),
                    Value = d.idetapas.ToString()
                };


            });


            ViewBag.items = items;




            List<entidad> entidadlotes = new List<entidad>();
            dtLotes = _ventascontado.SP_ListarLotesSelectItem();
            for (int i = 0; i < dtLotes.Rows.Count; i++)
            {
                entidad entidades3 = new entidad();
                entidades3.idlotes = dtLotes.Rows[i][0].GetHashCode();
                entidades3.nombrelotes = dtLotes.Rows[i][1].ToString();
                entidadlotes.Add(entidades3);
            }

            List<SelectListItem> itemlotes = entidadlotes.ConvertAll(d =>
            {
                return new SelectListItem()
                {

                    Text = d.nombrelotes.ToString(),
                    Value = d.idlotes.ToString(),
                };


            });


            ViewBag.itemslotes = itemlotes;



            List<entidad> entidadcliente = new List<entidad>();
            dtcliente = _ventascontado.SP_LISTARCLIENTE();
            for (int i = 0; i < dtcliente.Rows.Count; i++)
            {
                entidad entidades4 = new entidad();
                entidades4.idcliente = dtcliente.Rows[i][0].GetHashCode();
                entidades4.nombrecliente = dtcliente.Rows[i][1].ToString();
                entidadcliente.Add(entidades4);
            }

            List<SelectListItem> itemcliente = entidadcliente.ConvertAll(d =>
            {
                return new SelectListItem()
                {

                    Text = d.nombrecliente.ToString(),
                    Value = d.idcliente.ToString(),
                };


            });


            ViewBag.itemcliente = itemcliente;
            imagenVentasConatdo = ValidarLogin.imagen2;
            string imagenbytes = Convert.ToBase64String(imagenVentasConatdo);
            string imagenurls = string.Format("data:image/png;base64,{0}", imagenbytes);
            ViewBag.ventascontado = imagenurls;
            HttpContext.Session.GetString("sess_1");

            return View();
        }

        [HttpPost]


        public async Task<IActionResult> guardar(int idlotess, decimal preciocontado, string archivoImagenn, int idcliente, IFormFile archivoVoucher, string nombrelotes, int idetapass, string etapa)
        {
            byte[] archivoImagenes = null;
            byte[] archivoVouchers = null;
            byte[] imagenproyectolotes = null;
            string nombretransaccion = "verificacion";
            string estadotransaccion = "contado";

            try
            {
                var cleanerBase64 = archivoImagenn.Substring(22);
                archivoImagenes = System.Convert.FromBase64String(cleanerBase64);
                //using (var fs1 = archivoImagenn.OpenReadStream())
                //using (var ms1 = new MemoryStream())

                //{
                //    fs1.CopyTo(ms1);
                //    archivoImagenes = ms1.ToArray();

                //}

                using (var fs1 = archivoVoucher.OpenReadStream())
                using (var ms1 = new MemoryStream())

                {
                    fs1.CopyTo(ms1);
                    archivoVouchers = ms1.ToArray();

                }


                DateTime fechaventa = DateTime.Now;
                idusuariosU = ValidarLogin.idusuarios;

                var enviardatos = new entidad
                {
                    nombretransaccion = nombretransaccion,
                    idetapas = idetapass,
                    etapa = etapa,
                    idlotes = idlotess,
                    nombrelotes = nombrelotes,
                    imagenlotes1 = archivoImagenes,
                    preciocontado = preciocontado,
                    PrecioInicial = 0,
                    LetrasPagar = 0,
                    restante = 0,
                    idusuario = idusuariosU,
                    idcliente = idcliente,
                    fechaventaS = fechaventa,
                    estadotransaccion = estadotransaccion,
                    voucher = archivoVouchers,



                };



                
              
               
                int codigoetapa = idetapass;
                var nombreetapa = etapa;
                var nombrelote = nombrelotes;

                var verificar = "verificar codigo -" + codigoetapa + "-" + nombreetapa + "-" + nombrelote ;
              //var enviardatos = new entidad
              //{

                //    idlotes = idlotess,
                //    idusuario = idusuariosU,
                //    preciocontado = preciocontado,
                //    ImagenLotes = archivoImagenes,
                //    ImagenProyecto=archivoVouchers,
                //    idcliente = idcliente,
                //    voucher = archivoVouchers,
                //    nombrelotes = nombrelotes,
                //    fechaventa = fechaventa,
                //    idetapas = idetapass

                //};

                //_ventascontado.SP_INSERTAR_VENTASCONTADO(enviardatos);

              _ventascontado.SP_INSERTTRANSACCION(enviardatos);
                string token = "EAAIw8yc9N6oBACZBHC5Ac1olDpkeSxThZBDTjxWOfbObnnYL4VvOLVFuZA1WsAcsZAduG8uIYRt6bELWMVZCTJheJY3Tc0ulz7PAZBTYayEpQorqZAJXYo8IWFZCZBSIVZBRYvoTkIRDz2bUitT8oJRMVRSJTr1lsim0rykmkU0guxZCXyYDaLURyp4";
                //Identificador de número de teléfono
                string idTelefono = "103679482669791";
                //Nuestro telefono
                string telefono = "51912095876";
                HttpClient client = new HttpClient();
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "https://graph.facebook.com/v15.0/" + idTelefono + "/messages");
                request.Headers.Add("Authorization", "Bearer " + token);

                //var contenido = "\"messaging_product\": \"whatsapp\", \"recipient_type\": \"individual\", \"to\":\"" + telefono + "\",  \"type\":\"text\",\"text\":{\"preview_url\":false ,\"body\":\"" + verificar + "&nbsp" +nombreetapa+ "\" ";
                //request.Content = new StringContent(contenido);



                request.Content = new StringContent("{ \"messaging_product\": \"whatsapp\", \"recipient_type\": \"individual\", \"to\":\"" + telefono + "\",  \"type\":\"text\",\"text\":{\"preview_url\":false ,\"body\":\"" + verificar + "\" }}");


                request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = await client.SendAsync(request);
                //response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();


           

                return Json(new { success = true, message = "Guardado Exitoso" });

            } catch (Exception ex)
            {


                //return Json(new { error = ex.ToString() });
                return Json(String.Format("'success':'false','error': " + ex + " "));

            }
        }



        public async Task<IActionResult> llamar()
        {
            string token = "EAAIw8yc9N6oBAAqHOmuYuES9VG8JhrgCZAWZBZBIwvHznjXOSc2wDf4k8V9jNKv4MCSigKXdual5cULr9yGp7JJMwcF4CHUFlrgBwA27A4oqqNy2s8AOWEQgYMXYp441ZABZA8YB5U7H6i3vYaKxQiphZBxjJZCBehzUh7VubbcYtZBFkFDUqSE9yxzeVkk96FzBvDkvmZBZAPeAZDZD";
            //Identificador de número de teléfono
            string idTelefono = "103679482669791";
            //Nuestro telefono
            string telefono = "51912095876";
            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "https://graph.facebook.com/v15.0/" + idTelefono + "/messages");
            request.Headers.Add("Authorization", "Bearer " + token);
            request.Content = new StringContent("{ \"messaging_product\": \"whatsapp\", \"to\": \"" + telefono + "\", \"type\": \"template\", \"template\": { \"name\": \"hello_world\", \"language\": { \"code\": \"en_US\" } } }");
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = await client.SendAsync(request);
            //response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            return Json(new { success = true, message = "Guardado Exitoso" });
        }


        public class Result
        {
            public int dni { get; set; }
            public string nombres { get; set; }
            public string apellidoPaterno { get; set; }
            public string apellidoMaterno { get; set; }


            public string codVerifica { get; set; }


        }

        //public class ObjetoP
        //{
        //    public bool success { get; set; }
        //    //public Result result { get; set; }
        //}

        public class datosciudadano
        {
            public bool success { get; set; }
            public string nombres { get; set; }

            public string apellidoPaterno { get; set; }
            public string apellidoMaterno { get; set; }
            public int dni { get; set; }
            public string codVerifica { get; set; }
            public string mensaje { get; set; }

        }
        public class respuesta
        {
            public int dni { get; set; }
            public string nombres { get; set; }
            public string apellidoPaterno { get; set; }
            public string apellidoMaterno { get; set; }
            public string codVerifica { get; set; }



        }
        [HttpPost]
        public JsonResult Reniec(int dni)
        {
            List<datosciudadano> datos=new List<datosciudadano>();

            //dni = "72276852";

            datosciudadano datosc = new datosciudadano();
            string url = "https://dniruc.apisperu.com/api/v1/dni/"+dni+"?token=eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJlbWFpbCI6Impvc2lhc2lzcmFlbDE0MTE2QGdtYWlsLmNvbSJ9.I5ZSfBpDClEWCsN4zv5BkRrYfmJXkxGbCW4-s_rZlu0";
        //string url = "https://dniruc.apisperu.com/api/v1/dni/46828733?token=eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJlbWFpbCI6Impvc2lhc2lzcmFlbDE0MTE2QGdtYWlsLmNvbSJ9.I5ZSfBpDClEWCsN4zv5BkRrYfmJXkxGbCW4-s_rZlu0";

            var web_request = (HttpWebRequest)System.Net.WebRequest.Create(url);

            using(var response = web_request.GetResponse())
            using (var reader = new StreamReader(response.GetResponseStream()))
            {

                string resultado = reader.ReadToEnd();
                string jsonviene = Convert.ToString(resultado);


                var respuesta = JsonConvert.DeserializeObject<Result>(jsonviene);

                if (respuesta != null)
                {

                   
                    datosc.success=true;
                    datosc.mensaje = "peticion completa";
                    datosc.apellidoPaterno = respuesta.apellidoPaterno;
                    datosc.apellidoMaterno = respuesta.apellidoMaterno;
                    datosc.dni = respuesta.dni;
                    datosc.codVerifica = respuesta.codVerifica;
                    datosc.nombres = respuesta.nombres;
                    datos.Add(datosc);

                }
                else
                {

                    datosc.success=false;
                    datosc.mensaje = "nro de dni no valido";
                }

            }


            return Json(datos);

        }




        [HttpPost]

        public IActionResult guardarcliente(string nombrereniec, string apellidoP, int celular,string direccion,int dnis)
        {
            try
            {


                var enviarcliente = new entidad
                {

                    nombrecliente = nombrereniec,
                    apellidosgeneral = apellidoP,
                    celular = celular,
                    direccion = direccion,
                    Dni = dnis,


                };

                _ventascontado.SP_INSERTARCLIENTE(enviarcliente);
                return Json(new { success = true, message = "Guardado Exitoso" });

            }
            catch (Exception ex)
            {


                return Json(String.Format("'success':'false','error': " + ex + " "));

            }




        }



    }
}
