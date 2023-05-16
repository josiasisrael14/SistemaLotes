using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaLotes.Models;
using System.Data;

namespace SistemaLotes.Controllers
{
    public class Usuarios : Controller
    {

        public static byte[] imagenUsuario = new byte[0];

        private readonly db _usuario;

        public Usuarios(db context)
        {

            _usuario = context;

        }
        public IActionResult Index()
        {

            DataTable dt;
            DataTable dtetapas;
            DataTable dtLotes;
            List<entidad> entidad = new List<entidad>();

            dt=_usuario.ListarUsuario();
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                entidad entidad1 = new entidad();
                entidad1.idusuario = dt.Rows[i][0].GetHashCode();
                entidad1.nombre = dt.Rows[i][1].ToString();
                entidad1.logeo = dt.Rows[i][2].ToString();
                entidad1.fechacreado = (DateTime)dt.Rows[i][3];
                entidad1.contrazeña = dt.Rows[i][4].ToString();

                if (DBNull.Value.Equals(dt.Rows[i][5]))
                {
                    entidad1.foto =null;

                }
                else
                {

                  entidad1.foto = (byte[])dt.Rows[i][5];

                }
           



                entidad.Add(entidad1);

            }

            ViewBag.entidad = entidad;

            imagenUsuario = ValidarLogin.imagen2;
            string imagenbytes = Convert.ToBase64String(imagenUsuario);
            string imagenurl = string.Format("data:image/png;base64,{0}", imagenbytes);
            ViewBag.imagenurl = imagenurl;

            HttpContext.Session.GetString("sess_1");
            return View();
        }


        [HttpPost]
        public IActionResult guardar(int idusuarios, string nombre, string logeo,string password,IFormFile archivoImagen)
        {


            var file = archivoImagen;
            byte[] archivoImagenes = null;
           
          

            try
            {



                if (idusuarios > 0)
                {


                    if (file == null)
                    {

                        using (var ms1 = new MemoryStream())
                        {

                            archivoImagenes = ms1.ToArray();
                        }


                    }
                    else
                    {



                        using (var fs1 = archivoImagen.OpenReadStream())
                        using (var ms1 = new MemoryStream())

                        {
                            //fs1.CopyTo(ms1);
                            archivoImagenes = ms1.ToArray();

                        }

                    }


                    DateTime fechaho = DateTime.Now;
                    var enviardatosupadte = new entidad
                    {
                        idusuario=idusuarios,
                        nombre = nombre,
                        logeo = logeo,
                        fechacreado = fechaho,
                        contrazeña = password,
                        foto = archivoImagenes,
                        idtipo = 2,
                        operacion=2

                    };

                    _usuario.SP_INSERTARORUPDATE(enviardatosupadte);

                    return Json(new { success = true, message = "Guardado Exitoso" });

                }
                else
                {


                    using (var fs1 = archivoImagen.OpenReadStream())
                    using (var ms1 = new MemoryStream())

                    {
                        fs1.CopyTo(ms1);
                        archivoImagenes = ms1.ToArray();

                    }




                    DateTime fechahoy = DateTime.Now;
                    var enviardatos = new entidad
                    {
                        idusuario=idusuarios,
                        nombre = nombre,
                        logeo = logeo,
                        fechacreado = fechahoy,
                        contrazeña = password,
                        foto = archivoImagenes,
                        idtipo = 2,
                        operacion = 1

                    };

                    _usuario.SP_INSERTARORUPDATE(enviardatos);

                    return Json(new { success = true, message = "Guardado Exitoso" });

                }



            }
            catch (Exception ex)
            {


                //return Json(new { error = ex.ToString() });
                return Json(String.Format("'success':'false','error': " + ex + " "));

            }
        }


        public  JsonResult listaridD(int idusuario)
        {

            DataTable dt;
            List<entidad> entidades = new List<entidad>();

            var id = new entidad
            {

                idusuario = idusuario
            };

            dt = _usuario.SP_LISTARUSUARIOSID(id);
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                entidad entidadess = new entidad();

                entidadess.idusuario = dt.Rows[i][0].GetHashCode();
                entidadess.nombre = dt.Rows[i][1].ToString();
                entidadess.logeo = dt.Rows[i][2].ToString();
                entidadess.contrazeña = dt.Rows[i][3].ToString();
                entidadess.imagen = (byte[])dt.Rows[i][4];

                entidades.Add(entidadess);
            }
            return Json(entidades);



        }



    }
}
