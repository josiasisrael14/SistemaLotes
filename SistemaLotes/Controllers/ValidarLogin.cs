using Microsoft.AspNetCore.Mvc;
using SistemaLotes.Models;
//using System.Data;
using System.Data.SqlClient;
using System;
using System.Data;

namespace SistemaLotes.Controllers
{
    public class ValidarLogin : Controller
    {

        public static byte[] imagen2 = new byte[0];
        public static int idusuarios;
        private readonly db _login;


        public ValidarLogin(db contexts)
        {


            _login = contexts;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult validar(string logeo , string contrazeña)
        {

            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
          
            string nombre;
            string logeos;
            string tipo;
            int idusuario;

            byte[] imagen1 = new byte[0];
          
            var datos = new entidad
            {
                logeo = logeo,
                contrazeña = contrazeña

            };

            dt = _login.login(datos);
            if (dt.Rows.Count > 0)
            {

                 nombre = dt.Rows[0][0].ToString();
                 logeos = dt.Rows[0][1].ToString();
                 imagen1 = (byte[])dt.Rows[0][2];
                 imagen2 = imagen1;
                //idusuario = dt.Rows[0][4].GetHashCode();
                //imagen2 = imagen1;
                //idusuarios = idusuario;
                

               
                HttpContext.Session.SetString("sess_1", nombre);
               

                return RedirectToAction("index", "PanelGeneral");
               

            }
            else
            {

                TempData["msg"] = "USUARIO INCORRECTO";

                return RedirectToAction("index", "Login");
            }







        }

    }
}
