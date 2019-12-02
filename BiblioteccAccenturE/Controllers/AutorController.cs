using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BiblioteccAccenturE.Models;

namespace BiblioteccAccenturE.Controllers
{
    public class AutorController : Controller
    {
        private BiblioteccAccenturEEntities dbAutor;

        public AutorController()
        {
            this.dbAutor = new BiblioteccAccenturEEntities();
        }
        public ActionResult ListadoAutores()
        {
            List<Autor> autores = this.dbAutor.Autor.ToList();
            return View(autores);
        }

        public ActionResult AgregarAutor()
        {
            this.ViewBag.TituloPagina = "AGREGAR AUTOR";
            Autor aut = new Autor();
            return View("EditarAutor", aut);
        }

        [HttpPost]
        public ActionResult AgregarAutor(Autor aut)
        {
            if (aut.NombreYApellido != null)
            {
                this.ViewBag.TituloPagina = "AGREGAR AUTOR";
            this.dbAutor.Autor.Add(aut);
            this.dbAutor.SaveChanges();
                return View("CambiosGuardadosAutor");
            }
            return new HttpStatusCodeResult(505, "Error interno del servidor.");
        }


        public ActionResult EditarAutor(int id)
        {
            if (id>0)
            {
                this.ViewBag.TituloPagina = "EDITAR AUTOR";
            Autor aut = this.dbAutor.Autor.Find(id);
            return View(aut);
            }
            return new HttpStatusCodeResult(505, "Error interno del servidor.");
        }

        [HttpPost]
        public ActionResult EditarAutor(Autor aut)
        {
            if (aut.NombreYApellido != null)
            {
                this.ViewBag.TituloPagina = "EDITAR AUTOR";
            this.dbAutor.Autor.Attach(aut);
            this.dbAutor.Entry(aut).State = System.Data.Entity.EntityState.Modified;
            this.dbAutor.SaveChanges();
            return View("CambiosGuardadosAutor");
            }
            return new HttpStatusCodeResult(505, "Error interno del servidor.");
        }


        public ActionResult EliminarAutor(int id)
        {
            if (id > 0 && VerificaLibroAutor(id)==0)
            {
                Autor aut = this.dbAutor.Autor.Find(id);
            this.dbAutor.Autor.Remove(aut);
            this.dbAutor.SaveChanges();
            return RedirectToAction("ListadoAutores");
            }
            return View("ErrorEliminarAutor");
        }

        public int VerificaLibroAutor(int id)
        {
            int flag = 0;
            List<Libro> libros = this.dbAutor.Libro.ToList();
            IEnumerable<Autor> autores = this.dbAutor.Autor;
            foreach (Libro lib in libros)
            {
                if (lib.Autor.Any(aut => aut.Id_Autor.Equals(id)))
                {
                    flag = 1;
                }
            }
            return flag;
        }

        public ActionResult ErrorEliminarAutor()
        {
            return View("ErrorEliminarAutor");
        }

        public ActionResult CambiosGuardadosAutor()
        {
            return View("CambiosGuardadosAutor");
        }

    }
}