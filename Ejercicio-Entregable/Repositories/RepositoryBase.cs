using System.Collections.Generic;
using System.Linq;
using AccesoDatos.Data;
using Microsoft.EntityFrameworkCore;

namespace AccesoDatos.Repositories
{
    public class RepositoryBase<T> where T : class
    {
        public List<T> ObtenerTodosCon(params string[] propiedadesRelacionadas)
        {
            using (var _context = new DBcontext())
            {
                IQueryable<T> query = _context.Set<T>().AsNoTracking();

                foreach (string propiedadRelacionada in propiedadesRelacionadas)
                {
                    query = query.Include(propiedadRelacionada);
                }

                return query.ToList();
            }
        }
    }
}
