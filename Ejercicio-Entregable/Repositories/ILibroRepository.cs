using System.Collections.Generic;
using AccesoDatos.Models;

namespace AccesoDatos.Repositories
{
    public interface ILibroRepository
    {
        void Agregar(Libro libro);
        List<Libro> ObtenerTodos();
        void Modificar(int id, string nuevoTitulo);
        void Eliminar(int id);
    }
}
