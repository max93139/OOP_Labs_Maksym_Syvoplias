namespace Lab6.Domain;

/// <summary>
/// Захищає пасажирів через узгодження захисних реакцій.
/// </summary>
public sealed class SafetySystem
{
    /// <summary>
    /// Активує подушки безпеки та блокування салону у відповідь на загрозу.
    /// </summary>
    public IReadOnlyList<string> ActivateProtection(string threatName)
    {
        return new List<string>
        {
            $"Threat detected: {threatName}.",
            "Airbags prepared.",
            "Passenger compartment locked."
        };
    }
}
