namespace Lab6.Domain;

/// <summary>
/// Визначає доступні режими трансформації розумного автомобіля.
/// </summary>
public enum TransformationMode
{
    Ground,
    Water,
    Air
}

/// <summary>
/// Визначає спрощені стани роботи компонентів.
/// </summary>
public enum ComponentState
{
    Stopped,
    Active,
    Eco,
    Sport,
    Emergency
}

/// <summary>
/// Визначає розпізнані наміри голосових команд.
/// </summary>
public enum CommandIntent
{
    StartTrip,
    EnableAutopilot,
    ChangeClimate,
    ActivateProtection,
    ShowDiagnostics
}
