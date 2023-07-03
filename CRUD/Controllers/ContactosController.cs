using Microsoft.AspNetCore.Mvc;
using CRUD.Models;
using CRUD.Models.Datos;
using Microsoft.CodeAnalysis;

namespace CRUD.Controllers
{
    public class ContactosController : Controller
    {   
        /**
         * Instancia de la clase ContactoDatos.
         */
        ContactoDatos _ContactoDatos = new ContactoDatos();
        public IActionResult Listar()
        {
            var o_lista = _ContactoDatos.Listar();

            return View(o_lista);
        }

        public IActionResult _RevisarConexion() {
            ContactoDatos _Revisar = new ContactoDatos();
            ViewBag.Message = _Revisar._VerificarConexion();
            return View();
        }

        public IActionResult FormGuardar()
        {
            /**
             * Metodo para visualizar formulario de insercion de datos
             */
            return View();
        }

        public IActionResult Eliminar(int id)
        {
            var data = _ContactoDatos.Obterner(id);
            return View(data);
        }
        [HttpPost]
        public IActionResult Eliminar(ContactosModel ocm)
        {
            Boolean response = _ContactoDatos.Eliminar(ocm);
            if(response == true)
            {
                return RedirectToAction("Listar");
            }
            else
            {
                return View();
            }
        }

        public IActionResult Editar(int id) {
            var data = _ContactoDatos.Obterner(id);
            return View(data); 
        }

        [HttpPost]
        public IActionResult Editar(ContactosModel ocm)
        {
            /**
             * Funcion para procesar datos de actualizacion.
             */
            if (!ModelState.IsValid)
                return View();
            var respuesta = _ContactoDatos.Editar(ocm);
            if (respuesta == true)
            {
                return RedirectToAction("listar");
            }
            else
            {
                return View();
            }
           
           
        }

        [HttpPost]
        public IActionResult FormGuardar(ContactosModel ocm)
        {
            /**
             *  Metodo para registrar un nuevo contacto el la DB.
             * **/
            if (!ModelState.IsValid)
            {
                return View();
            }
            else
            {
                var respuesta = _ContactoDatos.Guardar(ocm);
                if (respuesta == true)
                {
                    return RedirectToAction("listar");
                }
                else
                {
                    return View();
                }
            }
        }
    }
}
