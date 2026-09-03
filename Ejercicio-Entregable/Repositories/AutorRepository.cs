using System.Collections.Generic;
using System.Linq;
using AccesoDatos.Data;
using AccesoDatos.Models;

namespace AccesoDatos.Repositories
{
    public class AutorRepository : IAutorRepository
    {
        public void Agregar(Autor autor)
        {
            using (var context = new DBcontext())
            {
                context.Autores.Add(autor);
                context.SaveChanges();
            }
        }

        public List<Autor> ObtenerTodos()
        {
            using (var context = new DBcontext())
            {
                return context.Autores
                    .OrderBy(a => a.Nombre)
                    .ToList();
            }
        }

        public void Modificar(int id, string nuevoNombre)
        {
            using (var context = new DBcontext())
            {
                var autor = context.Autores.Find(id);
                if (autor != null)
                {
                    autor.Nombre = nuevoNombre;
                    context.SaveChanges();
                }
            }
        }
    }
}
