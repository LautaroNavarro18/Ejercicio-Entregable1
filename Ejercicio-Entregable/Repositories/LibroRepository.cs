using System.Collections.Generic;
using System.Linq;
using AccesoDatos.Data;
using AccesoDatos.Models;

namespace AccesoDatos.Repositories
{
    public class LibroRepository : RepositoryBase<Libro>, ILibroRepository
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
            return ObtenerTodosCon("Autor", "Categoria")
                .Where(l => l.Activo)
                .OrderBy(l => l.Titulo)
                .ToList();
        }

        public void Modificar(int id, string nuevoTitulo)
        {
            using (var context = new DBcontext())
            {
                var libro = context.Libros.Find(id);
                if (libro != null)
                {
                    libro.Titulo = nuevoTitulo;
                    context.SaveChanges();
                }
            }
        }

        public void Eliminar(int id)
        {
            using (var context = new DBcontext())
            {
                var libro = context.Libros.Find(id);
                if (libro != null)
                {
                    libro.Activo = false;
                    context.SaveChanges();
                }
            }
        }
    }
}
