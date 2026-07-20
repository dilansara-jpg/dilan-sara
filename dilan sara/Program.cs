using System;
using System.IO;
using System.Linq;

class Program
{
    static int validas = 0;
    static int invalidas = 0;

    static int visa = 0;
    static int mastercard = 0;
    static int amex = 0;
    static int discover = 0;

    static void Main()
    {
        int opcion;

        do
        {
            Console.Clear();
            Console.WriteLine("=== VALIDADOR DE TARJETAS ===");
            Console.WriteLine("1. Validar una tarjeta");
            Console.WriteLine("2. Validar desde archivo");
            Console.WriteLine("3. Generar número válido");
            Console.WriteLine("4. Estadísticas");
            Console.WriteLine("5. Salir");
            Console.Write("Seleccione una opción: ");

            int.TryParse(Console.ReadLine(), out opcion);

            switch (opcion)
            {
                case 1:
                    ValidarTarjetaMenu();
                    break;

                case 2:
                    ValidarDesdeArchivoMenu();
                    break;

                case 3:
                    GenerarNumeroValido();
                    break;

                case 4:
                    MostrarEstadisticas();
                    break;

                case 5:
                    Console.WriteLine("Saliendo...");
                    break;

                default:
                    Console.WriteLine("Opción inválida");
                    Pausa();
                    break;
            }

        } while (opcion != 5);
    }

    static void ValidarTarjetaMenu()
    {
        Console.Clear();

        Console.Write("Ingrese el número de tarjeta: ");
        string numero = Console.ReadLine() ?? "";

        string marca = IdentificarMarca(numero);
        bool valida = ValidarTarjeta(numero);

        Console.WriteLine();
        Console.WriteLine($"Número: {numero}");
        Console.WriteLine($"Marca: {marca}");

        if (valida)
        {
            Console.WriteLine("Estado: ✅ VÁLIDA");
            validas++;

            ContarMarca(marca);
        }
        else
        {
            Console.WriteLine("Estado: ❌ INVÁLIDA");
            invalidas++;
        }

        Pausa();
    }

    static bool ValidarTarjeta(string numero)
    {
        if (string.IsNullOrWhiteSpace(numero))
            return false;

        if (!numero.All(char.IsDigit))
            return false;

        int suma = 0;
        bool duplicar = false;

        for (int i = numero.Length - 1; i >= 0; i--)
        {
            int digito = numero[i] - '0';

            if (duplicar)
            {
                digito *= 2;

                if (digito > 9)
                    digito -= 9;
            }

            suma += digito;
            duplicar = !duplicar;
        }

        return suma % 10 == 0;
    }

    static string IdentificarMarca(string numero)
    {
        if (numero.StartsWith("4") &&
           (numero.Length == 13 || numero.Length == 16))
        {
            return "Visa";
        }

        if (numero.Length == 16)
        {
            int prefijo2 = int.Parse(numero.Substring(0, 2));

            if (prefijo2 >= 51 && prefijo2 <= 55)
                return "Mastercard";
        }

        if (numero.Length == 15 &&
            (numero.StartsWith("34") ||
             numero.StartsWith("37")))
        {
            return "American Express";
        }

        if (numero.StartsWith("6011"))
            return "Discover";

        return "Desconocida";
    }

    static void ValidarDesdeArchivoMenu()
    {
        Console.Clear();

        Console.Write("Ruta del archivo: ");
        string ruta = Console.ReadLine() ?? "";

        try
        {
            string[] lineas = File.ReadAllLines(ruta);

            foreach (string numero in lineas)
            {
                string marca = IdentificarMarca(numero);
                bool valida = ValidarTarjeta(numero);

                Console.WriteLine("----------------------------");
                Console.WriteLine($"Número: {numero}");
                Console.WriteLine($"Marca: {marca}");
                Console.WriteLine($"Estado: {(valida ? "VÁLIDA" : "INVÁLIDA")}");

                if (valida)
                {
                    validas++;
                    ContarMarca(marca);
                }
                else
                {
                    invalidas++;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error al leer archivo:");
            Console.WriteLine(ex.Message);
        }

        Pausa();
    }

    static void GenerarNumeroValido()
    {
        Console.Clear();

        Random rnd = new Random();

        string baseNumero = "4";

        for (int i = 0; i < 14; i++)
        {
            baseNumero += rnd.Next(0, 10);
        }

        string numeroCompleto = "";

        for (int digito = 0; digito <= 9; digito++)
        {
            string candidato = baseNumero + digito;

            if (ValidarTarjeta(candidato))
            {
                numeroCompleto = candidato;
                break;
            }
        }

        Console.WriteLine("Número generado:");
        Console.WriteLine(numeroCompleto);
        Console.WriteLine("Marca: Visa");

        Pausa();
    }

    static void MostrarEstadisticas()
    {
        Console.Clear();

        Console.WriteLine("===== ESTADÍSTICAS =====");
        Console.WriteLine($"Tarjetas válidas: {validas}");
        Console.WriteLine($"Tarjetas inválidas: {invalidas}");
        Console.WriteLine();

        Console.WriteLine("Desglose por marca:");
        Console.WriteLine($"Visa: {visa}");
        Console.WriteLine($"Mastercard: {mastercard}");
        Console.WriteLine($"American Express: {amex}");
        Console.WriteLine($"Discover: {discover}");

        Pausa();
    }

    static void ContarMarca(string marca)
    {
        switch (marca)
        {
            case "Visa":
                visa++;
                break;

            case "Mastercard":
                mastercard++;
                break;

            case "American Express":
                amex++;
                break;

            case "Discover":
                discover++;
                break;
        }
    }

    static void Pausa()
    {
        Console.WriteLine();
        Console.WriteLine("Presione ENTER para continuar...");
        Console.ReadLine();
    }
}