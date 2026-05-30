namespace Lab6;

/// <summary>
/// Зберігає сталі ідентифікаційні дані розумного автомобіля.
/// </summary>
public sealed class VehicleIdentity
{
    private string _identifier;
    private string _model;
    private int _passengerCapacity;

    /// <summary>
    /// Конструктор за замовчуванням.
    /// </summary>
    public VehicleIdentity()
    {
        _identifier = "SC-2040-01";
        _model = "Synergy Capsule";
        _passengerCapacity = 4;
    }

    /// <summary>
    /// Ініціалізує нові ідентифікаційні дані автомобіля.
    /// </summary>
    public VehicleIdentity(string identifier, string model, int passengerCapacity)
    {
        _identifier = identifier;
        _model = model;
        _passengerCapacity = passengerCapacity;
    }

    /// <summary>
    /// Конструктор копіювання.
    /// </summary>
    public VehicleIdentity(VehicleIdentity other)
    {
        _identifier = other.Identifier;
        _model = other.Model;
        _passengerCapacity = other.PassengerCapacity;
    }

    public string Identifier
    {
        get => _identifier;
        set => _identifier = value;
    }

    public string Model
    {
        get => _model;
        set => _model = value;
    }

    public int PassengerCapacity
    {
        get => _passengerCapacity;
        set => _passengerCapacity = value;
    }
}
