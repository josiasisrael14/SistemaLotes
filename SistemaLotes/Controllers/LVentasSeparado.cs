using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaLotes.Models;
using System.Data;
using System.Net.Http.Headers;

namespace SistemaLotes.Controllers
{
    public class LVentasSeparado : Controller
    {
        public static byte[] imagenVentasSeparado = new byte[0];
        private readonly db _LVentasSeparado;
        public static int idusuariosU;
        public LVentasSeparado(db context)
        {

            _LVentasSeparado=context;   

        }

        public IActionResult Index()
        {
            DataTable dt;
            DataTable dtventassepa;
            DataTable dtcliente;
            List<entidad> entidad = new List<entidad>();
            dt = _LVentasSeparado.SP_LISTARVENTASSEPARADO();
            for (int i = 0; i < dt.Rows.Count; i++)
            {


                entidad entidad1 = new entidad();

                entidad1.idventassepa = dt.Rows[i][0].GetHashCode();
                entidad1.nombrelotes = dt.Rows[i][1].ToString();
                entidad1.NombreEtapa = dt.Rows[i][2].ToString();
                entidad1.nombre = dt.Rows[i][3].ToString();
                entidad1.nombrecliente = dt.Rows[i][4].ToString();
                //entidad1.imagenlotes1 = (byte[])dt.Rows[i][5];
                entidad1.preciocontado = Convert.ToDecimal(dt.Rows[i][6]);
                entidad1.PrecioInicial = Convert.ToDecimal(dt.Rows[i][7]);
                entidad1.LetrasPagar = dt.Rows[i][8].GetHashCode();
                entidad1.restante = Convert.ToDecimal(dt.Rows[i][9]);
                //entidad1.voucher = (byte[])dt.Rows[i][10];
                //entidad1.fechaventaS = (DateTime)dt.Rows[i][11];
                entidad.Add(entidad1);
               


            }

            ViewBag.datosseparado = entidad;

            List<entidad> entidad2 = new List<entidad>();
            dtventassepa = _LVentasSeparado.SP_ListarEtapasSelectItem();
            for (int i = 0; i < dtventassepa.Rows.Count; i++)
            {
                entidad entidad3 = new entidad();
                entidad3.idetapas = dtventassepa.Rows[i][0].GetHashCode();
                entidad3.NombreEtapa = dtventassepa.Rows[i][1].ToString();
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


            ViewBag.itemn = items;




            List<entidad> entidadlotes = new List<entidad>();
            dtventassepa = _LVentasSeparado.SP_ListarLotesSelectItem();
            for (int i = 0; i < dtventassepa.Rows.Count; i++)
            {
                entidad entidades3 = new entidad();
                entidades3.idlotes = dtventassepa.Rows[i][0].GetHashCode();
                entidades3.nombrelotes = dtventassepa.Rows[i][1].ToString();
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
            dtcliente = _LVentasSeparado.SP_LISTARCLIENTE();
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



            imagenVentasSeparado = ValidarLogin.imagen2;
            string imagenbytes = Convert.ToBase64String(imagenVentasSeparado);
            string imagenurls = string.Format("data:image/png;base64,{0}", imagenbytes);
            ViewBag.VentasSeparado = imagenurls;
            HttpContext.Session.GetString("sess_1");


            return View();
        }


        [HttpPost]
        public async Task<IActionResult> guardar(int idlotess, string nombrelotes, int idetapass, string etapa, decimal preciocontado,decimal precioinicial,int letras,decimal restante, string archivoImagenn, int idcliente, IFormFile archivoVoucher ,decimal cuotamonto)
        {
            return Json(new { success = true, message = "Guardado Exitoso" });
        }





        public JsonResult listarpagar(int idventassepa)
        {

            DataTable dt;
            List<entidad> entidad = new List<entidad>();
           

            var datos = new entidad
            {

              idventassepa=idventassepa

            };

            dt = _LVentasSeparado.SP_VENTASSEPARADOPAGAR(datos);

            for (int i = 0; i < dt.Rows.Count; i++)
            {

                entidad entidad1 = new entidad();
                entidad1.preciocontado = dt.Rows[i][0].GetHashCode();
                entidad1.precioi = dt.Rows[i][1].GetHashCode();
                entidad1.letras = dt.Rows[i][2].GetHashCode();
                entidad1.restante = dt.Rows[i][3].GetHashCode();
                entidad1.idlotes = dt.Rows[i][4].GetHashCode();
                entidad1.montopagar = Convert.ToDecimal(dt.Rows[i][5]);
                entidad1.idventassepa = datos.idventassepa;
                entidad.Add(entidad1);

            }


            return Json(entidad);




        }


        [HttpPost]
        public IActionResult GuardarPago(int idventassepa, int idlotesss,decimal restantess, int letraspagar )
        {
            /*,IFormFile archivoVoucher*/
            try
            {
                var datospagar = new entidad
                {
                    idventassepa=idventassepa,
                    idlotes=idlotesss,
                    restante=restantess,
                    letras=letraspagar


                };




                _LVentasSeparado.SP_PAGARVENTASSEPARADO(datospagar);
                return Json(new { success = true, message = "Guardado Exitoso" });



            }
            catch (Exception ex)
            {

                return Json(String.Format("'success':'false','error': " + ex + " "));

            }


            return View();

        }



    }
}
