using Microsoft.AspNetCore.Mvc;
using SistemaLotes.Models;
using System.Data;

namespace SistemaLotes.Controllers
{
    public class ListarParaRealizarVentas : Controller
    {


        private readonly db _listarparaventa;

        public ListarParaRealizarVentas(db context)
        {

            _listarparaventa = context; 


        }



        public IActionResult Index()

        {

            return View();
        }


       
        public JsonResult datos(int idetapas , int idlotes)
        {
            DataTable dt;
            List<entidad> entidad = new List<entidad>();
            entidad dat = null;

            var datos = new entidad
            {

                idetapas = idetapas,
                idlotes = idlotes

            };
          
                dt = _listarparaventa.SP_CREARVENTASCONTADOLISTARDATOS(datos);

                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    entidad entidad1 = new entidad();
                    entidad1.idlotes = dt.Rows[i][0].GetHashCode();
                    entidad1.nombrelotes = dt.Rows[i][1].ToString();
                    entidad1.preciocontado = Convert.ToDecimal(dt.Rows[i][2]);
                    entidad1.imagenlotes1 = (byte[])dt.Rows[i][4];
                    entidad1.idetapas = dt.Rows[i][5].GetHashCode();
                     entidad1.etapa = dt.Rows[i][6].ToString();
                     entidad.Add(entidad1);

                }

              //string imagenlotes = Convert.ToBase64String();

            /*if ((dt?.Rows?.Count ?? 0) == 0)
            {

                TempData["msg"] = "Lote no esta disponible";

            }*/
            
            
               

            
            return Json(entidad);


        }
    }
}
