using Microsoft.AspNetCore.Mvc;
using SistemaLotes.Models;
using System.Data;

namespace SistemaLotes.Controllers
{
    public class ReporteVentas : Controller
    {
        public static byte[] imagenVentas = new byte[0];
        private readonly db _ReporteVenta = null;

        public ReporteVentas(db context)
        {

            _ReporteVenta  = context;

        }
        public IActionResult Index()

        {



            imagenVentas = ValidarLogin.imagen2;
            string imagenbytes = Convert.ToBase64String(imagenVentas);
            string imagenurls = string.Format("data:image/png;base64,{0}", imagenbytes);
            ViewBag.imagenVentas = imagenurls;
            HttpContext.Session.GetString("sess_1");
            return View();
        }

        [HttpPost]
        public IActionResult listarReporteVenta( int idempleados)
        {

            DataTable dt = new DataTable();

            var reporte = new entidad
            {
              idusuario=idempleados

            };
            List<entidad> entidad = new List<entidad>();
            dt = _ReporteVenta.SP_REPORTEVENTAUSUARIO(reporte);

            for (int i = 0; i < dt.Rows.Count; i++)
            {

                entidad entidad1 = new entidad();
                entidad1.idventas = dt.Rows[i][0].GetHashCode();
                entidad1.nombre = dt.Rows[i][1].ToString();
                entidad1.preciocontado = Convert.ToDecimal(dt.Rows[i][2]);
                entidad1.nombrecliente = dt.Rows[i][3].ToString();
                entidad1.nombrelotes = dt.Rows[i][5].ToString();
                entidad1.fechaventa = (DateTime)dt.Rows[i][6];
                entidad1.NombreEtapa = dt.Rows[i][7].ToString();
                entidad.Add(entidad1);


            }


            return Json(new { data = entidad });

        }
    }
}
