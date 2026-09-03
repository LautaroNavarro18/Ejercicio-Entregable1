using System.Collections.Generic;
using AccesoDatos.Models;

namespace AccesoDatos.Repositories
{
    public interface ICategoriaRepository
    {
        void Agregar(Categoria categoria);
        List<Categoria> ObtenerTodos();
    }
}
