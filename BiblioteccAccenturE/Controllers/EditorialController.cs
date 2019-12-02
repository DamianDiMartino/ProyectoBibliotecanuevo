using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BiblioteccAccenturE.Models;

namespace BiblioteccAccenturE.Controllers
{
    public class EditorialController : Controller
    {
        private BiblioteccAccenturEEntities dbEditorial;

        public EditorialController()
        {
            this.dbEditorial = new BiblioteccAccenturEEntities();
        }
        public ActionResult ListadoEditoriales()
        {
            List<Editorial> editoriales = this.dbEditorial.Editorial.ToList();
            return View(editoriales);
        }

        public ActionResult AgregarEditorial()
        {
            this.ViewBag.TituloPagina = "AGREGAR EDITORIAL";
            Editorial edit = new Editorial();
            return View("EditarEditorial", edit);
        }

        [HttpPost]
        public ActionResult AgregarEditorial(Editorial edit)
        {
            if (edit.NombreEditorial != null)
            {
                this.ViewBag.TituloPagina = "AGREGAR EDITORAL";
            this.dbEditorial.Editorial.Add(edit);
            this.dbEditorial.SaveChanges();
                return View("CambiosGuardadosEditorial");
            }
            return new HttpStatusCodeResult(505, "Error interno del servidor.");
        }


        public ActionResult EditarEditorial(int id)
        {
            if (id>0)
            {
                this.ViewBag.TituloPagina = "EDITAR EDITORIAL";
            Editorial edit = this.dbEditorial.Editorial.Find(id);
            return View(edit);
            }
            return new HttpStatusCodeResult(505, "Error interno del servidor.");
        }

        [HttpPost]
        public ActionResult EditarEditorial(Editorial edit)
        {
            if (edit.NombreEditorial != null)
            {
                this.ViewBag.TituloPagina = "EDITAR EDITORIAL";
            this.dbEditorial.Editorial.Attach(edit);
            this.dbEditorial.Entry(edit).State = System.Data.Entity.EntityState.Modified;
            this.dbEditorial.SaveChanges();
                return View("CambiosGuardadosEditorial");
            }
            return new HttpStatusCodeResult(505, "Error interno del servidor.");
        }


        public ActionResult EliminarEditorial(int id)
        {
          
            if (id > 0 && VerificaLibroEditorial(id) == 0) { 
            Editorial edit = this.dbEditorial.Editorial.Find(id);
            this.dbEditorial.Editorial.Remove(edit);
            this.dbEditorial.SaveChanges();
            return RedirectToAction("ListadoEditoriales");
            }
            return View("ErrorEliminarEditorial");
        }

        public int VerificaLibroEditorial(int id)
        {
            int flag = 0;
            List<Libro> libros = this.dbEditorial.Libro.ToList();
            foreach(Libro lib in libros)
            {
                if (id == lib.Id_Editorial)
                    {
                        flag = 1;
                    }
            }
            return flag;
        }

        public ActionResult ErrorEliminarEditorial()
        {
            return View("ErrorEliminarEditorial");
        }

        public ActionResult CambiosGuardadosEditorial()
        {
            return View("CambiosGuardadosEditorial");
        }


    }
}