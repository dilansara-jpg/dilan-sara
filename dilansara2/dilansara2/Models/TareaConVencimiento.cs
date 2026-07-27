namespace dilansara2.Models;

public class TareaConVencimiento : Tarea
{
    public DateTime FechaVencimiento { get; set; }

    public int DiasRestantes
    {
        get
        {
            return (FechaVencimiento.Date - DateTime.Now.Date).Days;
        }
    }

    public TareaConVencimiento()
    {
    }

    public TareaConVencimiento(
        string titulo,
        string descripcion,
        Prioridad prioridad,
        Categoria categoria,
        DateTime fechaVencimiento)
        : base(titulo, descripcion, prioridad, categoria)
    {
        FechaVencimiento = fechaVencimiento;
    }

    public override void MostrarInfo()
    {
        base.MostrarInfo();

        Console.WriteLine($"Vence: {FechaVencimiento:dd/MM/yyyy}");
        Console.WriteLine($"Días restantes: {DiasRestantes}");
    }
}