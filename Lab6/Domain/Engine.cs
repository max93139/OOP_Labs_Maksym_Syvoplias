namespace Lab6.Domain;

/// <summary>
/// Представляє гібридне джерело руху.
/// </summary>
public sealed class Engine
{
    /// <summary>
    /// Ініціалізує новий двигун.
    /// </summary>
    public Engine(string engineType, int powerKilowatts)
    {
        EngineType = engineType;
        PowerKilowatts = powerKilowatts;
        State = ComponentState.Stopped;
    }

    /// <summary>
    /// Повертає тип двигуна.
    /// </summary>
    public string EngineType { get; }

    /// <summary>
    /// Повертає потужність двигуна в кіловатах.
    /// </summary>
    public int PowerKilowatts { get; }

    /// <summary>
    /// Повертає поточний стан двигуна.
    /// </summary>
    public ComponentState State { get; private set; }

    /// <summary>
    /// Запускає джерело руху.
    /// </summary>
    public string Start()
    {
        State = ComponentState.Active;
        return $"{EngineType} engine started with {PowerKilowatts} kW available.";
    }

    /// <summary>
    /// Зупиняє джерело руху.
    /// </summary>
    public string Stop()
    {
        State = ComponentState.Stopped;
        return "Engine stopped.";
    }

    /// <summary>
    /// Змінює режим роботи двигуна.
    /// </summary>
    public string ChangeMode(ComponentState state)
    {
        State = state;
        return $"Engine mode changed to {State}.";
    }
}
