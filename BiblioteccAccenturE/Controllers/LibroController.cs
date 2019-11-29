using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BiblioteccAccenturE.Models;

namespace BiblioteccAccenturE.Controllers
{
    public class LibroController : Controller
    {
        private BiblioteccAccenturEEntities dbLibro;

        public LibroController()
        {
            this.dbLibro = new BiblioteccAccenturEEntities();
        }


        public ActionResult Agregar()
        {
            return View("EditarLibro");
        }


        [HttpPost]
        public ActionResult Agregar(Libro lib, IEnumerable<int> autores, int gen, int edit)
        {


            foreach (int autorActual in autores)
            {
                Autor autor = dbLibro.Autor.Find(autorActual);
                lib.Autor.Add(autor);
            }
            lib.Id_Genero = gen;
            lib.Id_Editorial = edit;
            lib.ISBN = "111111111111112";

            dbLibro.Libro.Add(lib);
            dbLibro.SaveChanges();

            return Content("Libro aniadido satisfactoriamente");
        }

        public ActionResult EditarLibro(int id)
        {
            Libro lib = dbLibro.Libro.Find(id);
            return View(lib);
        }

        [HttpPost]
        public ActionResult EditarLibro(Libro lib, IEnumerable<int> autores, int gen, int edit )
        {

            Libro librito = this.dbLibro.Libro.Find(lib.Id_Libro);
            librito.Titulo = lib.Titulo;
            librito.Id_Genero = gen;
            librito.Id_Editorial = edit;
            librito.Autor.Clear();

            foreach (int autorActual in autores)
            {
                Autor escritoPor = dbLibro.Autor.Find(autorActual);
                librito.Autor.Add(escritoPor);
            }

            dbLibro.SaveChanges();

            return Content("Libro editado satisfactoriamente");
        }

        public ActionResult Listar()
        {
            return View(dbLibro.Libro.ToList());
        }

        [HttpPost]
        public ActionResult ListadoLibros(ListarLibros filtros)
        {

            IQueryable<Libro> qry = dbLibro.Libro;

            if (filtros.FiltroTitulo != null)
            {
                qry = qry.Where(librito => librito.Titulo.Contains(filtros.FiltroTitulo));
            }

            if (filtros.FiltroAutor.HasValue)
            {
                qry = qry.Where(librito =>
                    librito.Autor.Any(
                           aut => aut.Id_Autor.Equals(filtros.FiltroAutor.Value)
                           )
                );
            }

            if (filtros.FiltroGenero.HasValue)
            {
                qry = qry.Where(librito => librito.Id_Genero.Equals(filtros.FiltroGenero.Value));
            }

            if (filtros.FiltroEditorial.HasValue)
            {
                qry = qry.Where(librito => librito.Id_Editorial.Equals(filtros.FiltroEditorial.Value));
            }

            return View(qry.ToList());
        }

        public ActionResult ListadoLibros()
        {
            List<Libro> libros = this.dbLibro.Libro.ToList();
            List<Autor> autores = this.dbLibro.Autor.ToList();
            this.ViewBag.ListaAutores = autores;
            this.ViewBag.ListaLibros = libros;
            return View(libros);
        }



        public ActionResult EliminarLibro(int id)
        {
            Libro lib = this.dbLibro.Libro.Find(id);
            this.dbLibro.Libro.Remove(lib);
            this.dbLibro.SaveChanges();
            return RedirectToAction("ListadoLibros");
        }

    }
 }
