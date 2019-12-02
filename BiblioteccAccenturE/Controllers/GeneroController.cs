using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BiblioteccAccenturE.Models;

namespace BiblioteccAccenturE.Controllers
{
    public class GeneroController : Controller
    {
        private BiblioteccAccenturEEntities dbGenero;

        public GeneroController()
        {
            this.dbGenero = new BiblioteccAccenturEEntities();
        }
        public ActionResult ListadoGeneros()
        {
            List<Genero> generos = this.dbGenero.Genero.ToList();
            return View(generos);

        }

        public ActionResult AgregarGenero()
        {
            this.ViewBag.TituloPagina = "AGREGAR GENERO";
            Genero gen = new Genero();
            return View("EditarGenero", gen);
        }

        [HttpPost]
        public ActionResult AgregarGenero(Genero gen)
        {
            if (gen.NombreGenero != null)
            {
                this.ViewBag.TituloPagina = "AGREGAR GENERO";
            this.dbGenero.Genero.Add(gen);
            this.dbGenero.SaveChanges();
            return View("CambiosGuardadosGenero");
            }
            return new HttpStatusCodeResult(505, "Error interno del servidor.");
        }


        public ActionResult EditarGenero(int id)
        {
            if (id>0)
            {
                this.ViewBag.TituloPagina = "EDITAR GENERO";
            Genero gen = this.dbGenero.Genero.Find(id);
            return View(gen);
            }
            return new HttpStatusCodeResult(505, "Error interno del servidor.");
        }

        [HttpPost]  
        public ActionResult EditarGenero(Genero gen)
        {
            if (gen.NombreGenero!=null)
            {
                this.ViewBag.TituloPagina = "EDITAR GENERO";
                this.dbGenero.Genero.Attach(gen);
                this.dbGenero.Entry(gen).State = System.Data.Entity.EntityState.Modified;
                this.dbGenero.SaveChanges();
                return View("CambiosGuardadosGenero");
            }
            return new HttpStatusCodeResult(505, "Error interno del servidor.");
        }


        public ActionResult EliminarGenero(int id)
        {
            if (id > 0 && VerificaLibroGenero(id) == 0)
            {
                Genero gen = this.dbGenero.Genero.Find(id);
            this.dbGenero.Genero.Remove(gen);
            this.dbGenero.SaveChanges();
            return RedirectToAction("ListadoGeneros");
            }
            return View("ErrorEliminarGenero");
        }


        public int VerificaLibroGenero(int id)
        {
            int flag = 0;
            List<Libro> libros = this.dbGenero.Libro.ToList();
            foreach (Libro lib in libros)
            {
                if (id == lib.Id_Genero)
                {
                    flag = 1;
                }
            }
            return flag;
        }

        public ActionResult ErrorEliminarGenero()
        {
            return View("ErrorEliminarGenero");
        }

        public ActionResult CambiosGuardadosGenero()
        {
            return View("CambiosGuardadosGenero");
        }

    }
}