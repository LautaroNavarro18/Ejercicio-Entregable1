using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using AccesoDatos.Data;
using AccesoDatos.Models;
using AccesoDatos.Repositories;

namespace AppConsola
{
    internal class Program
    {
        IAutorRepository autorRepositorio = new AutorRepository();
        ICategoriaRepository categoriaRepositorio = new CategoriaRepository();
        ILibroRepository libroRepositorio = new LibroRepository();

        static void Main(string[] args)
        {
            using (var context = new DBcontext())
            {
                context.Database.Migrate();
            }

            var option = new Program();

            bool salir = false;
            while (!salir)
            {
                Console.WriteLine();
                Console.WriteLine("===== Gestion de Biblioteca =====");
                Console.WriteLine("1. Alta Autor");
                Console.WriteLine("2. Alta Categoria");
                Console.WriteLine("3. Alta Libro");
                Console.WriteLine("4. Ver Autores");
                Console.WriteLine("5. Ver Categorias");
                Console.WriteLine("6. Ver Libros");
                Console.WriteLine("7. Modificar Libro");
                Console.WriteLine("8. Eliminar Libro");
                Console.WriteLine("9. Modificar Autor");
                Console.WriteLine("0. Salir");
                Console.Write("Seleccione una opcion: ");

                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        option.AltaAutor();
                        break;
                    case "2":
                        option.AltaCategoria();
                        break;
                    case "3":
                        option.AltaLibro();
                        break;
                    case "4":
                        option.VerAutores();
                        break;
                    case "5":
                        option.VerCategorias();
                        break;
                    case "6":
                        option.VerLibros();
                        break;
                    case "7":
                        option.ModificarLibro();
                        break;
                    case "8":
                        option.EliminarLibro();
                        break;
                    case "9":
                        option.ModificarAutor();
                        break;
                    case "0":
                        salir = true;
                        break;
                    default:
                        Console.WriteLine("Opcion invalida.");
                        break;
                }
            }
        }

        void AltaAutor()
        {
            Console.Write("Nombre del autor: ");
            string nombre = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(nombre))
            {
                Console.WriteLine("El nombre no puede estar vacio.");
                return;
            }

            autorRepositorio.Agregar(new Autor { Nombre = nombre.Trim() });

            Console.WriteLine("Autor registrado correctamente.");
        }

        void ModificarAutor()
        {
            var autores = autorRepositorio.ObtenerTodos();

            if (!autores.Any())
            {
                Console.WriteLine("No hay autores registrados.");
                return;
            }

            Console.WriteLine("Autores disponibles:");
            foreach (var a in autores)
            {
                Console.WriteLine($"  {a.Id} - {a.Nombre}");
            }

            Console.Write("Seleccione el Id del autor a modificar: ");
            if (!int.TryParse(Console.ReadLine(), out int autorId) || autores.All(a => a.Id != autorId))
            {
                Console.WriteLine("Autor invalido.");
                return;
            }

            Console.Write("Nuevo nombre: ");
            string nuevoNombre = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(nuevoNombre))
            {
                Console.WriteLine("El nombre no puede estar vacio.");
                return;
            }

            autorRepositorio.Modificar(autorId, nuevoNombre.Trim());

            Console.WriteLine("Autor modificado correctamente.");
        }

        void AltaCategoria()
        {
            Console.Write("Nombre de la categoria: ");
            string nombre = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(nombre))
            {
                Console.WriteLine("El nombre no puede estar vacio.");
                return;
            }

            categoriaRepositorio.Agregar(new Categoria { Nombre = nombre.Trim() });

            Console.WriteLine("Categoria registrada correctamente.");
        }

        void AltaLibro()
        {
            var autores = autorRepositorio.ObtenerTodos();

            if (!autores.Any())
            {
                Console.WriteLine("No hay autores registrados. Registre un autor primero.");
                return;
            }

            var categorias = categoriaRepositorio.ObtenerTodos();

            if (!categorias.Any())
            {
                Console.WriteLine("No hay categorias registradas. Registre una categoria primero.");
                return;
            }

            Console.Write("Titulo del libro: ");
            string titulo = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(titulo))
            {
                Console.WriteLine("El titulo no puede estar vacio.");
                return;
            }

            Console.Write("Anio de publicacion: ");
            if (!int.TryParse(Console.ReadLine(), out int anio))
            {
                Console.WriteLine("Anio invalido.");
                return;
            }

            Console.WriteLine("Autores disponibles:");
            foreach (var a in autores)
            {
                Console.WriteLine($"  {a.Id} - {a.Nombre}");
            }

            Console.Write("Seleccione el Id del autor: ");
            if (!int.TryParse(Console.ReadLine(), out int autorId) || autores.All(a => a.Id != autorId))
            {
                Console.WriteLine("Autor invalido.");
                return;
            }

            Console.WriteLine("Categorias disponibles:");
            foreach (var c in categorias)
            {
                Console.WriteLine($"  {c.Id} - {c.Nombre}");
            }

            Console.Write("Seleccione el Id de la categoria: ");
            if (!int.TryParse(Console.ReadLine(), out int categoriaId) || categorias.All(c => c.Id != categoriaId))
            {
                Console.WriteLine("Categoria invalida.");
                return;
            }

            libroRepositorio.Agregar(new Libro
            {
                Titulo = titulo.Trim(),
                AnioPublicacion = anio,
                AutorId = autorId,
                CategoriaId = categoriaId
            });

            Console.WriteLine("Libro registrado correctamente.");
        }

        void VerAutores()
        {
            var autores = autorRepositorio.ObtenerTodos();

            if (!autores.Any())
            {
                Console.WriteLine("No hay autores registrados.");
                return;
            }

            Console.WriteLine();
            Console.WriteLine("Autores registrados:");
            foreach (var autor in autores)
            {
                Console.WriteLine($"  {autor.Id} - {autor.Nombre}");
            }
        }

        void VerCategorias()
        {
            var categorias = categoriaRepositorio.ObtenerTodos();

            if (!categorias.Any())
            {
                Console.WriteLine("No hay categorias registradas.");
                return;
            }

            Console.WriteLine();
            Console.WriteLine("Categorias registradas:");
            foreach (var categoria in categorias)
            {
                Console.WriteLine($"  {categoria.Id} - {categoria.Nombre}");
            }
        }

        void VerLibros()
        {
            var libros = libroRepositorio.ObtenerTodos();

            if (!libros.Any())
            {
                Console.WriteLine("No hay libros registrados.");
                return;
            }

            Console.WriteLine();
            Console.WriteLine("Libros registrados:");
            foreach (var libro in libros)
            {
                Console.WriteLine($"  {libro.Id} - {libro.Titulo} ({libro.AnioPublicacion}) - Autor: {libro.Autor.Nombre} - Categoria: {libro.Categoria.Nombre}");
            }
        }

        void ModificarLibro()
        {
            var libros = libroRepositorio.ObtenerTodos();

            if (!libros.Any())
            {
                Console.WriteLine("No hay libros registrados.");
                return;
            }

            Console.WriteLine("Libros disponibles:");
            foreach (var libro in libros)
            {
                Console.WriteLine($"  {libro.Id} - {libro.Titulo}");
            }

            Console.Write("Seleccione el Id del libro a modificar: ");
            if (!int.TryParse(Console.ReadLine(), out int libroId) || libros.All(l => l.Id != libroId))
            {
                Console.WriteLine("Libro invalido.");
                return;
            }

            Console.Write("Nuevo titulo: ");
            string nuevoTitulo = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(nuevoTitulo))
            {
                Console.WriteLine("El titulo no puede estar vacio.");
                return;
            }

            libroRepositorio.Modificar(libroId, nuevoTitulo.Trim());

            Console.WriteLine("Libro modificado correctamente.");
        }

        void EliminarLibro()
        {
            var libros = libroRepositorio.ObtenerTodos();

            if (!libros.Any())
            {
                Console.WriteLine("No hay libros registrados.");
                return;
            }

            Console.WriteLine("Libros disponibles:");
            foreach (var libro in libros)
            {
                Console.WriteLine($"  {libro.Id} - {libro.Titulo}");
            }

            Console.Write("Seleccione el Id del libro a eliminar: ");
            if (!int.TryParse(Console.ReadLine(), out int libroId) || libros.All(l => l.Id != libroId))
            {
                Console.WriteLine("Libro invalido.");
                return;
            }

            libroRepositorio.Eliminar(libroId);

            Console.WriteLine("Libro eliminado correctamente.");
        }
    }
}
