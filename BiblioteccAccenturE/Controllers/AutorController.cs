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
            this.ViewBag.TituloPagina = "AGREGAR AUTOR";
            this.dbAutor.Autor.Add(aut);
            this.dbAutor.SaveChanges();
            return RedirectToAction("ListadoAutores");
        }


        public ActionResult EditarAutor(int id)
        {
            this.ViewBag.TituloPagina = "EDITAR AUTOR";
            Autor aut = this.dbAutor.Autor.Find(id);
            return View(aut);
        }

        [HttpPost]
        public ActionResult EditarAutor(Autor aut)
        {
            this.ViewBag.TituloPagina = "EDITAR AUTOR";
            this.dbAutor.Autor.Attach(aut);
            this.dbAutor.Entry(aut).State = System.Data.Entity.EntityState.Modified;
            this.dbAutor.SaveChanges();
            return RedirectToAction("ListadoAutores");
        }


        public ActionResult EliminarAutor(int id)
        {
            Autor aut = this.dbAutor.Autor.Find(id);
            this.dbAutor.Autor.Remove(aut);
            this.dbAutor.SaveChanges();
            return RedirectToAction("ListadoAutores");
        }


    }
}