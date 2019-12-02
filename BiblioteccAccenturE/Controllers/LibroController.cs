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
            this.ViewBag.TituloPagina = "AGREGAR LIBRO";
            return View("EditarLibro");
        }


        [HttpPost]
        public ActionResult Agregar(Libro lib, IEnumerable<int> autores, int gen, int edit)
        {

            if ( CompruebaISBN(lib.ISBN)==0 && edit>0&& gen>0 && lib.ISBN != null && lib.ISBN.Length <= 15 &&  lib.Titulo!=null && autores.FirstOrDefault()>0) {
                this.ViewBag.TituloPagina = "AGREGAR LIBRO";
                foreach (int autorActual in autores)
            {
                Autor autor = dbLibro.Autor.Find(autorActual);
                lib.Autor.Add(autor);
            }
                lib.Id_Genero = gen;
                lib.Id_Editorial = edit;

            dbLibro.Libro.Add(lib);
            dbLibro.SaveChanges();

            return View("CambiosGuardadosLibro");
            }
            return new HttpStatusCodeResult(505, "Error interno del servidor.");
        }

        public ActionResult EditarLibro(int id)
        {
            if (id > 0) {
                this.ViewBag.TituloPagina = "EDITAR LIBRO";
                Libro lib = dbLibro.Libro.Find(id);
            this.ViewBag.id_Genero = lib.Id_Genero;
            this.ViewBag.id_Editorial = lib.Id_Editorial;
            this.ViewBag.Editorial = new Editorial();
            
            return View(lib);
            }       
            return new HttpStatusCodeResult(505, "Error interno del servidor.");
        }

    [HttpPost]
        public ActionResult EditarLibro(Libro lib, IEnumerable<int> autores, int gen, int edit )
        {
            if (edit > 0 && gen > 0 && lib.ISBN != null && lib.Titulo != null && autores.FirstOrDefault() > 0)
            {
                this.ViewBag.TituloPagina = "EDITAR LIBRO";
                Libro librito = this.dbLibro.Libro.Find(lib.Id_Libro);
                librito.Titulo = lib.Titulo;
                librito.Id_Genero = gen;
                librito.Id_Editorial = edit;
                librito.ISBN = lib.ISBN;
                librito.Autor.Clear();
                

                foreach (int autorActual in autores)
                {
                    Autor escritoPor = dbLibro.Autor.Find(autorActual);
                    librito.Autor.Add(escritoPor);
                }

                dbLibro.SaveChanges();

                return View("CambiosGuardadosLibro");
            }
                return new HttpStatusCodeResult(505, "Error interno del servidor.");
            
        }

        public ActionResult ListadoLibros()
        {
            List<Libro> libros = this.dbLibro.Libro.ToList();
            List<Autor> autores = this.dbLibro.Autor.ToList();
            this.ViewBag.ListaAutores = autores;
            this.ViewBag.ListaLibros = libros;
            return View(libros);
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

            if (filtros.FiltroISBN != null)
            {
                qry = qry.Where(librito => librito.ISBN.Contains(filtros.FiltroISBN));
            }

            return View(qry.ToList());
        }

        public ActionResult LibrosPorGenero(int Id)
        {           
            List<Libro> libros = new List<Libro>();

            if (Id >0)
            {
                foreach (Libro lib in this.dbLibro.Libro.ToList())
                {
                    if (lib.Id_Genero == Id)
                    {
                        libros.Add(lib);
                    }
                }
            }
            return View("ListadoLibros", libros);
        }

        public ActionResult LibrosPorAutor(int id)
        {
            List<Libro> libros = new List<Libro>();
            IEnumerable<Autor> autores = this.dbLibro.Autor;

            if (id>0)
            {
                foreach (Libro lib in this.dbLibro.Libro.ToList())
                {
                    if (lib.Autor.Any(aut => aut.Id_Autor.Equals(id)))
                    {
                        libros.Add(lib);
                    }
                }
            }
            return View("ListadoLibros", libros);
        }

        public ActionResult LibrosPorEditorial(int Id)
        {

            List<Libro> libros = new List<Libro>();

            if (Id > 0)
            {
                foreach (Libro lib in this.dbLibro.Libro.ToList())
                {
                    if (lib.Id_Editorial == Id)
                    {
                        libros.Add(lib);
                    }
                }
            }
            return View("ListadoLibros", libros);
        }

        public ActionResult EliminarLibro(int id)
        {
            Libro lib = this.dbLibro.Libro.Find(id);       
            this.dbLibro.Libro.Remove(lib);
            this.dbLibro.SaveChanges();
            return RedirectToAction("ListadoLibros");
        }

        public int CompruebaISBN(string ISBN)
        {
            int flag = 0;
            List<Libro> libros = this.dbLibro.Libro.ToList();
            foreach(Libro lib in libros)
            {
                if (lib.ISBN.Equals(ISBN))
                {
                    flag = 1;
                }
            }

            return flag;
        }

        public ActionResult CambiosGuardadosLibro()
        {
            return View("CambiosGuardadosLibro");
        }

    }
 }
