using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaLotes.Models;
using System.Data;

namespace SistemaLotes.Controllers
{
    public class Lotes : Controller
    {

        public static byte[] imagenLotes = new byte[0];
        private readonly db _Lotes = null;
       
        public Lotes(db context)
        {

            _Lotes = context;
           

        }

        [HttpGet]
        public IActionResult Index()

         

        {
            DataTable dt;
            DataTable dtetapa;

            List<entidad> entidad = new List<entidad>();
            dt = _Lotes.SP_LISTARLOTES();
            for (int i = 0; i < dt.Rows.Count; i++)
            {


                entidad entidad1 = new entidad();

                entidad1.idlotes = dt.Rows[i][0].GetHashCode();
                entidad1.estado = dt.Rows[i][2].ToString();
                entidad1.colindantes= dt.Rows[i][3].ToString();
                entidad1.preciocontado = Convert.ToDecimal(dt.Rows[i][4]);
                entidad1.nombrelotes = dt.Rows[i][9].ToString();
                //entidad1.PrecioInicial = dt.Rows[i][5].GetHashCode();
                //entidad1.LetrasPagar = dt.Rows[i][6].GetHashCode();
                //entidad1.ImagenLotes = (byte[])dt.Rows[i][7];
                //entidad1.ImagenProyecto = (byte[])dt.Rows[i][8];
                entidad.Add(entidad1);
                ViewBag.entidad = entidad;


            }






            List<entidad> entidadetapa = new List<entidad>();
            dtetapa = _Lotes.SP_ListarEtapas();
            for (int i = 0; i < dtetapa.Rows.Count; i++)
            {
                entidad entidades4 = new entidad();
                entidades4.idetapas = dtetapa.Rows[i][0].GetHashCode();
                entidades4.NombreEtapa = dtetapa.Rows[i][1].ToString();
                entidadetapa.Add(entidades4);
            }

            List<SelectListItem> itemetapa = entidadetapa.ConvertAll(d =>
            {
                return new SelectListItem()
                {

                    Text = d.NombreEtapa.ToString(),
                  
                    Value = d.idetapas.ToString(),
                };


            });


            ViewBag.itemetapa = itemetapa;



            imagenLotes = ValidarLogin.imagen2;
            string imagenbytes = Convert.ToBase64String(imagenLotes);
            string imagenurls = string.Format("data:image/png;base64,{0}", imagenbytes);
            ViewBag.lotes = imagenurls;
            HttpContext.Session.GetString("sess_1");

            return View();



        }


        //[HttpGet("{validarexistencia}")]
        //public async Task<ActionResult<bool>> validarexistencia(string datosvalidar)
        //{

        //    var  user =await _contexts.nombrelotes.FirstOrDefaultAsync(u=>u.no)

        //}



        [HttpPost]

        public IActionResult guardar(int idetapas, string colindantes, decimal precio ,IFormFile fotolote, IFormFile fotoproyecto, string nombrelotes)

          
        {
            DataTable validar;

            var datosenviar = new entidad{

                nombrelotes=nombrelotes,
                idetapas=idetapas

            };

              validar = _Lotes.SP_VALIDARDATOSEXISTENTES(datosenviar);
            if (validar.Rows.Count > 0)
            {



                return Json(datosenviar);





            }
            else
            {



                byte[] archivofotolotes = null;
                byte[] archivofotoproyecto = null;

                try
                {

                    using (var fs1 = fotolote.OpenReadStream())
                    using (var ms1 = new MemoryStream())

                    {
                        fs1.CopyTo(ms1);
                        archivofotolotes = ms1.ToArray();

                    }


                    using (var fs1 = fotoproyecto.OpenReadStream())
                    using (var ms1 = new MemoryStream())

                    {
                        fs1.CopyTo(ms1);
                        archivofotoproyecto = ms1.ToArray();

                    }




                    string estado = "disponible";
                    int precioinicial = 0;
                    int letraspagar = 0;

                    int restante = 0;

                    var datos = new entidad
                    {
                        idetapas = idetapas,
                        estado = estado,
                        colindantes = colindantes,
                        preciocontado = precio,
                        PrecioInicial = precioinicial,
                        LetrasPagar = letraspagar,
                        ImagenLotes = archivofotolotes,
                        ImagenProyecto = archivofotoproyecto,
                        nombrelotes = nombrelotes,
                        restante = restante


                    };

                    _Lotes.SP_INSERTARLOTES(datos);

                    return Json(0);
                    //return Json(new { success = true, message = "Guardado Exitoso" });

                }
                catch (Exception ex)
                {

                    return Json(String.Format("'success':'false','error': " + ex + " "));

                }

            }
           
        }
       
    }
}
