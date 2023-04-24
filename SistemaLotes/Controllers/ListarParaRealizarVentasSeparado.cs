using Microsoft.AspNetCore.Mvc;
using SistemaLotes.Models;
using System.Data;

namespace SistemaLotes.Controllers
{
    public class ListarParaRealizarVentasSeparado : Controller
    {


        private readonly db _listarparaventaS;

        public ListarParaRealizarVentasSeparado(db context)
        {

            _listarparaventaS = context;


        }

        public IActionResult Index()
        {
            return View();
        }


        public JsonResult datos(int idetapas, int idlotes)
        {
            DataTable dt;
            List<entidad> entidad = new List<entidad>();
            entidad dat = null;

            var datos = new entidad
            {

                idetapas = idetapas,
                idlotes = idlotes

            };

            dt = _listarparaventaS.SP_CREATEVENTASSEPARADOLISTARDATOS(datos);

            for (int i = 0; i < dt.Rows.Count; i++)
            {

                entidad entidad1 = new entidad();
                entidad1.idlotes = dt.Rows[i][0].GetHashCode();
                entidad1.nombrelotes = dt.Rows[i][1].ToString();
                entidad1.idetapas = dt.Rows[i][2].GetHashCode();
                entidad1.etapa = dt.Rows[i][3].ToString();
                entidad1.preciocontado = Convert.ToDecimal(dt.Rows[i][4]);
                entidad1.imagenlotes1 = (byte[])dt.Rows[i][6];
                entidad.Add(entidad1);

            }

           
            return Json(entidad);


        }




    }
}
