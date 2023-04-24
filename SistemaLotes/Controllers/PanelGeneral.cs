using Microsoft.AspNetCore.Mvc;
using SistemaLotes.Models;

namespace SistemaLotes.Controllers
{
    public class PanelGeneral : Controller
    {

        private readonly db _PanelGeneral = null;

        public static byte[] imagen3 = new byte[0];

        public PanelGeneral(db context)
        {

            _PanelGeneral = context;

        }

        public IActionResult Index()
        {



            imagen3 = ValidarLogin.imagen2;
            string imagenbytes = Convert.ToBase64String(imagen3);
            string imagenurl = string.Format("data:image/png;base64,{0}", imagenbytes);
            ViewBag.imagenurl = imagenurl;

            var nombre = TempData["nombre"];
            HttpContext.Session.GetString("sess_1");

            var Lotes = _PanelGeneral.SP_CONTARLOTES();
            var Etapas = _PanelGeneral.SP_CONTARETAPAS();
            var Usuarios = _PanelGeneral.SP_CONTARUSUARIOS();
            ViewBag.Lotes = Lotes;  
            ViewBag.Etapas= Etapas;
            ViewBag.Usuarios= Usuarios;

            return View();
        }
    }
}
