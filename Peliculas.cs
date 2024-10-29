using System;

// Definir una interfaz para operaciones de alquiler y devolución
public interface IMovieRental
{
    void Rent(string customerId);
    void Return(string customerId);
}

// Crear clases concretas para cada tipo de película
public class NewReleaseMovie : IMovieRental
{
    public void Rent(string customerId)
    {
        Console.WriteLine("Alquilando película de estreno");
    }

    public void Return(string customerId)
    {
        Console.WriteLine("Devolviendo película de estreno");
    }
}

public class RegularMovie : IMovieRental
{
    public void Rent(string customerId)
    {
        Console.WriteLine("Alquilando película regular");
    }

    public void Return(string customerId)
    {
        Console.WriteLine("Devolviendo película regular");
    }
}

public class OldClassicMovie : IMovieRental
{
    public void Rent(string customerId)
    {
        Console.WriteLine("Alquilando película clásica");
    }

    public void Return(string customerId)
    {
        Console.WriteLine("Devolviendo película clásica");
    }
}

// Aplicar Factory Method para crear instancias de películas según el tipo
public static class MovieRentalFactory
{
    public static IMovieRental CreateMovieRental(string movieType)
    {
        return movieType switch
        {
            "NewRelease" => new NewReleaseMovie(),
            "Regular" => new RegularMovie(),
            "OldClassic" => new OldClassicMovie(),
            _ => throw new ArgumentException("Tipo de película no soportado")
        };
    }
}

// Aplicar patrón de comando para encapsular las operaciones de alquiler y devolución
public interface ICommand
{
    void Execute();
}

public class RentMovieCommand : ICommand
{
    private readonly IMovieRental _movieRental;
    private readonly string _customerId;

    public RentMovieCommand(IMovieRental movieRental, string customerId)
    {
        _movieRental = movieRental;
        _customerId = customerId;
    }

    public void Execute()
    {
        _movieRental.Rent(_customerId);
    }
}

public class ReturnMovieCommand : ICommand
{
    private readonly IMovieRental _movieRental;
    private readonly string _customerId;

    public ReturnMovieCommand(IMovieRental movieRental, string customerId)
    {
        _movieRental = movieRental;
        _customerId = customerId;
    }

    public void Execute()
    {
        _movieRental.Return(_customerId);
    }
}

// RentalManager simplificado que utiliza comandos
public class RentalManager
{
    public void ProcessRental(string movieType, string customerId)
    {
        var movieRental = MovieRentalFactory.CreateMovieRental(movieType);
        ICommand rentCommand = new RentMovieCommand(movieRental, customerId);
        rentCommand.Execute();
    }

    public void ProcessReturn(string movieType, string customerId)
    {
        var movieRental = MovieRentalFactory.CreateMovieRental(movieType);
        ICommand returnCommand = new ReturnMovieCommand(movieRental, customerId);
        returnCommand.Execute();
    }
}

// Método Main para ejecutar el programa
public class Program
{
    public static void Main(string[] args)
    {
        var rentalManager = new RentalManager();

        // Ejemplo de uso
        Console.WriteLine("Procesando alquiler:");
        rentalManager.ProcessRental("NewRelease", "cliente1");

        Console.WriteLine("\nProcesando devolución:");
        rentalManager.ProcessReturn("NewRelease", "cliente1");

        Console.WriteLine("\nProcesando alquiler de película regular:");
        rentalManager.ProcessRental("Regular", "cliente2");

        Console.WriteLine("\nProcesando devolución de película regular:");
        rentalManager.ProcessReturn("Regular", "cliente2");

        Console.WriteLine("\nProcesando alquiler de película clásica:");
        rentalManager.ProcessRental("OldClassic", "cliente3");

        Console.WriteLine("\nProcesando devolución de película clásica:");
        rentalManager.ProcessReturn("OldClassic", "cliente3");
    }
}
