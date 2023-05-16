using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

            DataTable L = new DataTable();
            List<entidad>listar=new List<entidad>();
            L = _ReporteVenta.SP_LISTAREMPLEADOS();

            for (int i=0; i<L.Rows.Count; i++)
            {
                entidad datos=new entidad();
                datos.idusuario = L.Rows[i][0].GetHashCode();
                datos.nombre = L.Rows[i][1].ToString();
                listar.Add(datos);

            }

            List<SelectListItem> usuariosL = listar.ConvertAll(d =>
            {
                return new SelectListItem()
                {

                    Text = d.nombre.ToString(),
                    Value = d.idusuario.ToString(),
                    Selected = false
                };

            });


            imagenVentas = ValidarLogin.imagen2;
            string imagenbytes = Convert.ToBase64String(imagenVentas);
            string imagenurls = string.Format("data:image/png;base64,{0}", imagenbytes);
            ViewBag.imagenVentas = imagenurls;
            ViewBag.usuarios = usuariosL;
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
                string fechaformateada=entidad1.fechaventa.ToString("dd/MM/yyyy HH:mm:ss");
                entidad1.fechaventa=DateTime.Parse(fechaformateada);    
                entidad1.etapa = dt.Rows[i][7].ToString();
                entidad.Add(entidad1);


            }


            return Json(new { data = entidad,fechaFormato ="dd/MM/yyyy HH:mm:ss" });

        }
    }
}
