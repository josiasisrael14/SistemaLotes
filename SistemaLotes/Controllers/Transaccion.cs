using Microsoft.AspNetCore.Mvc;
using SistemaLotes.Models;
using System.Data;

namespace SistemaLotes.Controllers
{
    public class Transaccion : Controller
    {
        public static byte[] imagenTransaccion = new byte[0];
        private readonly db _listartransacion;


        public Transaccion(db context)
        {
            _listartransacion = context;

        }
        public IActionResult Index()
        {

            DataTable transaccio;
            List<entidad> entidad = new List<entidad>();
            transaccio = _listartransacion.SP_LISTARTRANSACCION();
            for (int i = 0; i < transaccio.Rows.Count; i++)
            {


                entidad entidad1 = new entidad();

                entidad1.idtransaccion = transaccio.Rows[i][0].GetHashCode();
                entidad1.nombretransaccion= transaccio.Rows[i][1].ToString();
                entidad1.estadotransaccion = transaccio.Rows[i][2].ToString();
                entidad1.etapa = transaccio.Rows[i][3].ToString();
                entidad1.nombrelotes = transaccio.Rows[i][4].ToString();
                entidad.Add(entidad1);
                ViewBag.datoss = entidad;


            }

            imagenTransaccion = ValidarLogin.imagen2;
            string imagenbytes = Convert.ToBase64String(imagenTransaccion);
            string imagenurl = string.Format("data:image/png;base64,{0}", imagenbytes);
            ViewBag.transaccion = imagenurl;

            HttpContext.Session.GetString("sess_1");
            return View();
        }

        public JsonResult listaridD(int idtransaccion)
        {
            DataTable dt;
            List<entidad> entidades = new List<entidad>();

           
            var id = new entidad
            {

                idtransaccion = idtransaccion
            };

            dt = _listartransacion.SP_LISTARDETALLETRANSACCION(id);
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                entidad entidadess = new entidad();


                entidadess.idtransaccion = dt.Rows[i][0].GetHashCode();
                entidadess.nombretransaccion = dt.Rows[i][1].ToString();
                entidadess.idetapas = dt.Rows[i][2].GetHashCode();
                entidadess.etapa = dt.Rows[i][3].ToString();
                entidadess.idlotes = dt.Rows[i][4].GetHashCode();
                entidadess.nombrelotes = dt.Rows[i][5].ToString();
                entidadess.imagenlotes1 = (byte[])dt.Rows[i][6];
                entidadess.preciocontado = Convert.ToDecimal(dt.Rows[i][7]);
                entidadess.precioi = Convert.ToDecimal(dt.Rows[i][8]);
                entidadess.letras = dt.Rows[i][9].GetHashCode();
                entidadess.restante = Convert.ToDecimal(dt.Rows[i][10]);
                entidadess.idusuario= dt.Rows[i][11].GetHashCode();
                entidadess.nombre = dt.Rows[i][12].ToString();
                entidadess.idcliente = dt.Rows[i][13].GetHashCode();
                entidadess.nombrecliente = dt.Rows[i][14].ToString();
                var fechas =entidadess.fechaventa = (DateTime)dt.Rows[i][15];
                entidadess.fechaventaconversion=fechas.ToShortDateString();
                entidadess.estado= dt.Rows[i][16].ToString();
                entidadess.voucher = (byte[])dt.Rows[i][17];
                entidadess.montopagar = dt.Rows[i][18] != null ? Convert.ToDecimal(dt.Rows[i][18]) :0;

                //entidadess.montopagar = Convert.ToDecimal(dt.Rows[i][18]);



                entidades.Add(entidadess);
            }

          
            return Json(entidades);
           



        }


        public JsonResult ListarParaGuardarTransaccion(int idtransaccion)
        {
            DataTable dt;
            List<entidad> entidades = new List<entidad>();
            var datosenviar = "";

            var id = new entidad
            {

                idtransaccion = idtransaccion
            };

            dt = _listartransacion.SP_LISTARDETALLETRANSACCION(id);
         
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                entidad entidadess = new entidad();

                var enviardatos = new entidad
                {
                    idlotes = dt.Rows[i][4].GetHashCode(),
                    idusuario = dt.Rows[i][11].GetHashCode(),
                    preciocontado = Convert.ToDecimal(dt.Rows[i][7]),
                    PrecioInicial = Convert.ToDecimal(dt.Rows[i][8]),
                    LetrasPagar = dt.Rows[i][9].GetHashCode(),
                    restante = Convert.ToDecimal(dt.Rows[i][10]),
                    imagenlotes1 = (byte[])dt.Rows[i][6],
                    ImagenProyecto = (byte[])dt.Rows[i][6],
                    idcliente = dt.Rows[i][13].GetHashCode(),
                    voucher = (byte[])dt.Rows[i][17],
                    montopagar = dt.Rows[i][18] != null ? Convert.ToDecimal(dt.Rows[i][18]) : 0,
                    letrasrestante= dt.Rows[i][9].GetHashCode(),
                    nombrelotes = dt.Rows[i][5].ToString(),
                    fechaventa = (DateTime)dt.Rows[i][15],
                    idetapas = dt.Rows[i][2].GetHashCode()


                    

                };

                if (enviardatos.PrecioInicial==0)
                {

                    _listartransacion.SP_INSERTAR_VENTASCONTADO(enviardatos);

                }
                else
                {

                    _listartransacion.SP_INSERTAR_VENTASSEPARADO(enviardatos);

                }

              
               

            }



            return Json(new { success = true, message = "Guardado Exitoso" });

        } 




        public JsonResult RechazarTransaccion(int idtransaccion)
        {


            DataTable dt;
            List<entidad> entidades = new List<entidad>();
            var datosenviar = "";

            var id = new entidad
            {

                idtransaccion = idtransaccion
            };

            dt = _listartransacion.SP_LISTARTRANSACCIONPARAELIMINAR(id);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                entidad entidadess = new entidad();

                var enviardatos = new entidad
                {
                    idlotes = dt.Rows[i][1].GetHashCode(),
                   
                };



                _listartransacion.SP_RECHAZARTRANSACCION(enviardatos);

            }




            return Json(new { success = true, message = "Guardado Exitoso" });

        }








    }
}
