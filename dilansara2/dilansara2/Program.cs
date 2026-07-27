using dilansara2.Models;
using dilansara2.Services;

GestorTareas gestor = new GestorTareas();

// Cargar tareas guardadas
gestor.CargarDeJSON("tareas.json");

bool salir = false;

while (!salir)
{
    Console.Clear();

    Console.WriteLine("====================================");
    Console.WriteLine("       GESTOR DE TAREAS");
    Console.WriteLine("====================================");
    Console.WriteLine("1. Crear tarea simple");
    Console.WriteLine("2. Crear tarea con vencimiento");
    Console.WriteLine("3. Listar tareas");
    Console.WriteLine("4. Buscar por categoría");
    Console.WriteLine("5. Buscar por prioridad");
    Console.WriteLine("6. Marcar completada");
    Console.WriteLine("7. Mostrar vencidas");
    Console.WriteLine("8. Eliminar tarea");
    Console.WriteLine("9. Salir");

    Console.Write("\nSeleccione una opción: ");
    string? opcion = Console.ReadLine();

    switch (opcion)
    {
        case "1":
        {
            Console.Clear();
            Console.WriteLine("===== CREAR TAREA SIMPLE =====");

            Console.Write("Título: ");
            string titulo = Console.ReadLine()!;

            Console.Write("Descripción: ");
            string descripcion = Console.ReadLine()!;

            Console.WriteLine("\nPrioridad");
            Console.WriteLine("1. Baja");
            Console.WriteLine("2. Media");
            Console.WriteLine("3. Alta");
            Console.WriteLine("4. Crítica");

            Console.Write("Seleccione: ");

            Prioridad prioridad = Console.ReadLine() switch
            {
                "1" => Prioridad.Baja,
                "2" => Prioridad.Media,
                "3" => Prioridad.Alta,
                "4" => Prioridad.Critica,
                _ => Prioridad.Media
            };

            Console.Write("Categoría: ");
            string nombreCategoria = Console.ReadLine()!;

            Categoria categoria = new Categoria(
                nombreCategoria,
                "Azul",
                ""
            );

            Tarea tarea = new Tarea(
                titulo,
                descripcion,
                prioridad,
                categoria
            );

            gestor.Agregar(tarea);

            Console.WriteLine("\n✅ Tarea creada correctamente.");
            Console.WriteLine("\nPresione una tecla para continuar...");
            Console.ReadKey();
            break;
        }

        case "2":
        {
            Console.Clear();
            Console.WriteLine("===== CREAR TAREA CON VENCIMIENTO =====");

            Console.Write("Título: ");
            string titulo = Console.ReadLine()!;

            Console.Write("Descripción: ");
            string descripcion = Console.ReadLine()!;

            Console.WriteLine("\nPrioridad");
            Console.WriteLine("1. Baja");
            Console.WriteLine("2. Media");
            Console.WriteLine("3. Alta");
            Console.WriteLine("4. Crítica");

            Console.Write("Seleccione: ");

            Prioridad prioridad = Console.ReadLine() switch
            {
                "1" => Prioridad.Baja,
                "2" => Prioridad.Media,
                "3" => Prioridad.Alta,
                "4" => Prioridad.Critica,
                _ => Prioridad.Media
            };

            Console.Write("Categoría: ");
            string nombreCategoria = Console.ReadLine()!;

            Categoria categoria = new Categoria(
                nombreCategoria,
                "Rojo",
                ""
            );

            Console.Write("Fecha de vencimiento (dd/MM/yyyy): ");

            DateTime fecha;

            while (!DateTime.TryParse(Console.ReadLine(), out fecha))
            {
                Console.Write("Fecha inválida. Intente nuevamente: ");
            }

            TareaConVencimiento tarea = new TareaConVencimiento(
                titulo,
                descripcion,
                prioridad,
                categoria,
                fecha
            );

            gestor.Agregar(tarea);

            Console.WriteLine("\n✅ Tarea con vencimiento creada correctamente.");
            Console.WriteLine("\nPresione una tecla para continuar...");
            Console.ReadKey();
            break;
        }

        case "3":
        {
            Console.Clear();

            Console.WriteLine("===== LISTADO DE TAREAS =====\n");

            gestor.ListarTodas();

            Console.WriteLine("\nPresione una tecla para continuar...");
            Console.ReadKey();
            break;
        }

        case "4":
        {
            Console.Clear();

            Console.WriteLine("===== BUSCAR POR CATEGORÍA =====");

            Console.Write("Ingrese la categoría: ");
            string categoria = Console.ReadLine()!;

            List<Tarea> resultado = gestor.ListarPorCategoria(categoria);

            Console.WriteLine();

            if (resultado.Count == 0)
            {
                Console.WriteLine("No se encontraron tareas.");
            }
            else
            {
                foreach (Tarea tarea in resultado)
                {
                    tarea.MostrarInfo();
                    Console.WriteLine("----------------------------");
                }
            }

            Console.WriteLine("\nPresione una tecla para continuar...");
            Console.ReadKey();
            break;
        }

       case "5":
{
    Console.Clear();

    Console.WriteLine("===== BUSCAR POR PRIORIDAD =====");

    Console.WriteLine("1. Baja");
    Console.WriteLine("2. Media");
    Console.WriteLine("3. Alta");
    Console.WriteLine("4. Crítica");

    Console.Write("\nSeleccione la prioridad: ");

    Prioridad prioridad = Console.ReadLine() switch
    {
        "1" => Prioridad.Baja,
        "2" => Prioridad.Media,
        "3" => Prioridad.Alta,
        "4" => Prioridad.Critica,
        _ => Prioridad.Media
    };

    List<Tarea> resultado = gestor.ListarPorPrioridad(prioridad);

    Console.WriteLine();

    if (resultado.Count == 0)
    {
        Console.WriteLine("No se encontraron tareas con esa prioridad.");
    }
    else
    {
        Console.WriteLine($"Se encontraron {resultado.Count} tarea(s).\n");

        foreach (Tarea tarea in resultado)
        {
            tarea.MostrarInfo();
            Console.WriteLine("----------------------------");
        }
    }

    Console.WriteLine("\nPresione una tecla para continuar...");
    Console.ReadKey();
    break;
}

        case "6":
{
    Console.Clear();

    Console.WriteLine("===== MARCAR TAREA COMO COMPLETADA =====");

    gestor.ListarTodas();

    Console.Write("\nIngrese el ID de la tarea: ");

    if (int.TryParse(Console.ReadLine(), out int id))
    {
        gestor.Completar(id);

        Console.WriteLine("\n✅ Tarea marcada como completada.");
    }
    else
    {
        Console.WriteLine("\nID inválido.");
    }

    Console.WriteLine("\nPresione una tecla para continuar...");
    Console.ReadKey();
    break;
}

        case "7":
{
    Console.Clear();

    Console.WriteLine("===== TAREAS VENCIDAS =====\n");

    List<Tarea> vencidas = gestor.ObtenerVencidas();

    if (vencidas.Count == 0)
    {
        Console.WriteLine("No hay tareas vencidas.");
    }
    else
    {
        Console.WriteLine($"Se encontraron {vencidas.Count} tarea(s) vencida(s).\n");

        foreach (Tarea tarea in vencidas)
        {
            tarea.MostrarInfo();
            Console.WriteLine("----------------------------");
        }
    }

    Console.WriteLine("\nPresione una tecla para continuar...");
    Console.ReadKey();
    break;
}

        case "8":
{
    Console.Clear();

    Console.WriteLine("===== ELIMINAR TAREA =====\n");

    gestor.ListarTodas();

    Console.Write("\nIngrese el ID de la tarea que desea eliminar: ");

    if (int.TryParse(Console.ReadLine(), out int id))
    {
        gestor.Eliminar(id);

        Console.WriteLine("\n✅ Tarea eliminada correctamente.");
    }
    else
    {
        Console.WriteLine("\n❌ ID inválido.");
    }

    Console.WriteLine("\nPresione una tecla para continuar...");
    Console.ReadKey();
    break;
}

        case "9":
        {
            gestor.GuardarEnJSON("tareas.json");

            Console.WriteLine("\nDatos guardados correctamente.");

            salir = true;
            break;
        }

        default:
        {
            Console.WriteLine("Opción inválida.");
            Console.ReadKey();
            break;
        }
    }
}