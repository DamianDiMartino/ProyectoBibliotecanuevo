using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BiblioteccAccenturE.Models;

namespace BiblioteccAccenturE.Models
{
    public class ListarLibros
    {
        public string FiltroTitulo { get; set; }
        public int? FiltroAutor { get; set; }
        public int? FiltroGenero { get; set; }
        public int? FiltroEditorial { get; set; }
        public string FiltroISBN { get; set; }

    }
}