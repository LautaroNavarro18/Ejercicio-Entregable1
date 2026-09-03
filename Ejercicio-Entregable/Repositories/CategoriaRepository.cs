using System.Collections.Generic;
using System.Linq;
using AccesoDatos.Data;
using AccesoDatos.Models;

namespace AccesoDatos.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        public void Agregar(Categoria categoria)
        {
            using (var context = new DBcontext())
            {
                context.Categorias.Add(categoria);
                context.SaveChanges();
            }
        }

        public List<Categoria> ObtenerTodos()
        {
            using (var context = new DBcontext())
            {
                return context.Categorias
                    .OrderBy(c => c.Nombre)
                    .ToList();
            }
        }
    }
}
