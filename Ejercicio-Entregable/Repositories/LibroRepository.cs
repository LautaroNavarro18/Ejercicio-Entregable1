using System.Collections.Generic;
using System.Linq;
using AccesoDatos.Data;
using AccesoDatos.Models;
using Microsoft.EntityFrameworkCore;

namespace AccesoDatos.Repositories
{
    public class LibroRepository : ILibroRepository
    {
        public void Agregar(Libro libro)
        {
            using (var context = new DBcontext())
            {
                context.Libros.Add(libro);
                context.SaveChanges();
            }
        }

        public List<Libro> ObtenerTodos()
        {
            using (var context = new DBcontext())
            {
                return context.Libros
                    .Include(l => l.Autor)
                    .OrderBy(l => l.Titulo)
                    .ToList();
            }
        }
    }
}
