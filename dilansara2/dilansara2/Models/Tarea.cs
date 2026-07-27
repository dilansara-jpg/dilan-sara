using dilansara2.Interfaces;

namespace dilansara2.Models;

public class Tarea : IExportable
{
    private static int contador = 1;

    public int Id { get; set; }
    public string Titulo { get; set; }
    public string Descripcion { get; set; }
    public Prioridad Prioridad { get; set; }
    public Categoria Categoria { get; set; }
    public bool Completada { get; set; }
    public DateTime FechaCreacion { get; set; }

    public Tarea()
    {
        Id = contador++;
        Titulo = "";
        Descripcion = "";
        Categoria = new Categoria();
        FechaCreacion = DateTime.Now;
        Completada = false;
    }

    public Tarea(string titulo, string descripcion,
                 Prioridad prioridad,
                 Categoria categoria)
    {
        Id = contador++;
        Titulo = titulo;
        Descripcion = descripcion;
        Prioridad = prioridad;
        Categoria = categoria;
        FechaCreacion = DateTime.Now;
        Completada = false;
    }

    public virtual void MostrarInfo()
    {
        Console.WriteLine("--------------------------------");
        Console.WriteLine($"ID: {Id}");
        Console.WriteLine($"Título: {Titulo}");
        Console.WriteLine($"Descripción: {Descripcion}");
        Console.WriteLine($"Prioridad: {Prioridad}");
        Console.WriteLine($"Categoría: {Categoria.Nombre}");
        Console.WriteLine($"Creada: {FechaCreacion:dd/MM/yyyy}");
        Console.WriteLine($"Estado: {(Completada ? "Completada" : "Pendiente")}");
    }

    public virtual string Exportar()
    {
        return $"{Id}|{Titulo}|{Prioridad}|{Completada}";
    }
}