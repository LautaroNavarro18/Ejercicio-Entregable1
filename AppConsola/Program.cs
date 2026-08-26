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
                Console.WriteLine("2. Alta Libro");
                Console.WriteLine("3. Ver Libros");
                Console.WriteLine("0. Salir");
                Console.Write("Seleccione una opcion: ");

                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        option.AltaAutor();
                        break;
                    case "2":
                        option.AltaLibro();
                        break;
                    case "3":
                        option.VerLibros();
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

        void AltaLibro()
        {
            var autores = autorRepositorio.ObtenerTodos();

            if (!autores.Any())
            {
                Console.WriteLine("No hay autores registrados. Registre un autor primero.");
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

            libroRepositorio.Agregar(new Libro
            {
                Titulo = titulo.Trim(),
                AnioPublicacion = anio,
                AutorId = autorId
            });

            Console.WriteLine("Libro registrado correctamente.");
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
                Console.WriteLine($"- {libro.Titulo} ({libro.AnioPublicacion}) - Autor: {libro.Autor.Nombre}");
            }
        }
    }
}
