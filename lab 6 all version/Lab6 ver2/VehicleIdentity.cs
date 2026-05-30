namespace Lab6;

/// <summary>
/// Зберігає сталі ідентифікаційні дані розумного автомобіля.
/// </summary>
public sealed class VehicleIdentity
{
    /// <summary>
    /// Ініціалізує нові ідентифікаційні дані автомобіля.
    /// </summary>
    public VehicleIdentity(string identifier, string model, int passengerCapacity)
    {
        Identifier = identifier;
        Model = model;
        PassengerCapacity = passengerCapacity;
    }

    /// <summary>
    /// Повертає унікальний ідентифікатор автомобіля.
    /// </summary>
    public string Identifier { get; }

    /// <summary>
    /// Повертає назву моделі автомобіля.
    /// </summary>
    public string Model { get; }

    /// <summary>
    /// Повертає підтримувану кількість пасажирів.
    /// </summary>
    public int PassengerCapacity { get; }
}
