using Microsoft.AspNetCore.Mvc;
using SistemaLotes.Models;
using System.Data;

namespace SistemaLotes.Controllers
{
    public class Clientes : Controller
    {
        private readonly db _clientes;

        public Clientes(db context)
        {

            _clientes = context;

        }
        public IActionResult Index()
        {
            DataTable dt;
            List<entidad> entidad = new List<entidad>();
            dt = _clientes.ListarClientes();
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                entidad entidad1 = new entidad();
                entidad1.idcliente = dt.Rows[i][0].GetHashCode();
                entidad1.nombrecliente = dt.Rows[i][1].ToString();
                entidad1.apellidosgeneral = dt.Rows[i][2].ToString();
                entidad1.celular = dt.Rows[i][3].GetHashCode();
                entidad1.direccion = dt.Rows[i][4].ToString();
                entidad1.Dni = dt.Rows[i][5].GetHashCode();
                entidad.Add(entidad1);

            }

            ViewBag.entidad = entidad;


            return View();
        }






    }
}
