using Microsoft.AspNetCore.Mvc;
using SistemaLotes.Models;
using System.Data;

namespace SistemaLotes.Controllers
{
    public class Etapas : Controller
    {

        public static byte[] imagenEtapas = new byte[0];
        private readonly db _Etapa = null;

        public Etapas(db context)
        {

            _Etapa = context;

        }
        public IActionResult Index()

        {
            DataTable dt;

            List<entidad> entidad = new List<entidad>();
            dt = _Etapa.SP_ListarEtapas();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                    

                entidad entidad1 = new entidad();

                entidad1.idetapas = dt.Rows[i][0].GetHashCode();
                entidad1.NombreEtapa = dt.Rows[i][1].ToString();
                entidad1.Descripcion= dt.Rows[i][2].ToString();


                entidad.Add(entidad1);
                    
              
            
            }

            ViewBag.entidad = entidad;

            imagenEtapas = ValidarLogin.imagen2;
            string imagenbytes = Convert.ToBase64String(imagenEtapas);
            string imagenurls = string.Format("data:image/png;base64,{0}", imagenbytes);
            ViewBag.imagenurls = imagenurls;
            HttpContext.Session.GetString("sess_1");
            return View();
        }

        [HttpPost]
        public IActionResult guardar(string NombreEtapa, string Descripcion)
        {
            try
            {
                var guardar = new entidad
                {
                 NombreEtapa=NombreEtapa,
                 Descripcion=Descripcion


                };
                _Etapa.SP_insertar_etapas(guardar);
                return Json(new { success = true, message = "Guardado Exitoso" });

            }
            catch (Exception ex)
            {
                return Json(String.Format("'success':'false','error': " + ex + " "));

            }


            return View();
        }



        //[HttpGet("validarnombre")]
        [HttpPost]
        public IActionResult validarnombre(string NombreEtapa)
    {
            

            DataTable validar;
        

            var datosenviar = new entidad
            {

                NombreEtapa = NombreEtapa



            };

            validar = _Etapa.SP_VALIDARNOMBRE(datosenviar);
            if (validar.Rows.Count>0)
            {

                return Json(datosenviar);


            }
            else
            {

                return Json(0);


            }

        }



    }
}
