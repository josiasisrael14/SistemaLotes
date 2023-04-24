using Microsoft.AspNetCore.Mvc;
using SistemaLotes.Models;
using System.Data;

namespace SistemaLotes.Controllers
{
    public class LotesSegunEtapas : Controller
    {
        private readonly db _Lotes = null;
        public LotesSegunEtapas(db context)
        {

            _Lotes = context;

        }
        public IActionResult Index(int id)
        {


            DataTable dt;

            List<entidad> entidad = new List<entidad>();

            var idlotes = new entidad
            {

                idetapas = id

            };

            dt = _Lotes.SP_LISTAR_LOTES_SEGUN_ETAPAS(idlotes);
            for (int i = 0; i < dt.Rows.Count; i++)
            {


                entidad entidad1 = new entidad();

                entidad1.idlotes = dt.Rows[i][0].GetHashCode();
                entidad1.estado = dt.Rows[i][2].ToString();
                entidad1.colindantes = dt.Rows[i][3].ToString();
                entidad1.preciocontado = Convert.ToDecimal(dt.Rows[i][4]);
                entidad1.nombrelotes = dt.Rows[i][9].ToString();
                //entidad1.PrecioInicial = dt.Rows[i][5].GetHashCode();
                //entidad1.LetrasPagar = dt.Rows[i][6].GetHashCode();
                //entidad1.ImagenLotes = (byte[])dt.Rows[i][7];
                //entidad1.ImagenProyecto = (byte[])dt.Rows[i][8];
                entidad.Add(entidad1);
                ViewBag.entidad = entidad;


            }


            return View();
        }


        public IActionResult ListarLotesPorId(int id)
        {

            DataTable dt;

            List<entidad> entidad = new List<entidad>();

            var idlotes = new entidad
            {

                idetapas = id

            };

            dt = _Lotes.SP_LISTAR_LOTES_SEGUN_ETAPAS(idlotes);
            for (int i = 0; i < dt.Rows.Count; i++)
            {


                entidad entidad1 = new entidad();

                entidad1.idlotes = dt.Rows[i][0].GetHashCode();
                entidad1.estado = dt.Rows[i][2].ToString();
                entidad1.colindantes = dt.Rows[i][3].ToString();
                entidad1.preciocontado = Convert.ToDecimal(dt.Rows[i][4]);
                entidad1.PrecioInicial = Convert.ToDecimal(dt.Rows[i][5]);
                entidad1.LetrasPagar = dt.Rows[i][6].GetHashCode();
                //entidad1.ImagenLotes = (byte[])dt.Rows[i][7];
                //entidad1.ImagenProyecto = (byte[])dt.Rows[i][8];
                entidad.Add(entidad1);
                ViewBag.entidad = entidad;


            }

            return RedirectToAction("index", "Lotes", ViewBag.entidad);


        }
    }
}
