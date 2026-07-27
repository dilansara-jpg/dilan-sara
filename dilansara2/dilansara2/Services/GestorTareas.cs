using System.Text.Json;
using dilansara2.Models;

namespace dilansara2.Services;

public class GestorTareas
{
    public List<Tarea> Tareas { get; set; }

    public GestorTareas()
    {
        Tareas = new List<Tarea>();
    }

    public void Agregar(Tarea tarea)
    {
        Tareas.Add(tarea);
    }

    public void Completar(int id)
    {
        Tarea? tarea = Tareas.FirstOrDefault(t => t.Id == id);

        if (tarea != null)
            tarea.Completada = true;
    }

    public List<Tarea> ListarPorCategoria(string categoria)
    {
        return Tareas.Where(t => t.Categoria.Nombre.Equals(categoria, StringComparison.OrdinalIgnoreCase)).ToList();
    }

    public List<Tarea> ListarPorPrioridad(Prioridad prioridad)
    {
        return Tareas.Where(t => t.Prioridad == prioridad).ToList();
    }

    public List<Tarea> ObtenerVencidas()
    {
        return Tareas.Where(t =>
            t is TareaConVencimiento tv &&
            tv.FechaVencimiento < DateTime.Now &&
            !tv.Completada).ToList();
    }

    public void Eliminar(int id)
    {
        Tarea? tarea = Tareas.FirstOrDefault(t => t.Id == id);

        if (tarea != null)
            Tareas.Remove(tarea);
    }

    public void ListarTodas()
    {
        if (Tareas.Count == 0)
        {
            Console.WriteLine("\nNo hay tareas registradas.");
            return;
        }

        foreach (Tarea tarea in Tareas)
        {
            tarea.MostrarInfo();
            Console.WriteLine("----------------------------");
        }
    }

    public void GuardarEnJSON(string archivo)
    {
        var opciones = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        string json = JsonSerializer.Serialize(Tareas, opciones);

        File.WriteAllText(archivo, json);
    }

    public void CargarDeJSON(string archivo)
    {
        if (!File.Exists(archivo))
            return;

        try
        {
            string json = File.ReadAllText(archivo);

            List<Tarea>? lista = JsonSerializer.Deserialize<List<Tarea>>(json);

            if (lista != null)
                Tareas = lista;
        }
        catch
        {
            Console.WriteLine("Error al cargar el archivo JSON.");
        }
    }
}