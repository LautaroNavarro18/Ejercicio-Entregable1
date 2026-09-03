using System.Collections.Generic;
using AccesoDatos.Models;

namespace AccesoDatos.Repositories
{
    public interface IAutorRepository
    {
        void Agregar(Autor autor);
        List<Autor> ObtenerTodos();
        void Modificar(int id, string nuevoNombre);
    }
}
