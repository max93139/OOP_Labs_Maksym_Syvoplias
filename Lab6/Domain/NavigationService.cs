namespace Lab6.Domain;

/// <summary>
/// Забезпечує адаптацію маршруту для автономної навігації.
/// </summary>
public sealed class NavigationService
{
    /// <summary>
    /// Формує спокійний маршрут, коли ризик водія або дороги підвищений.
    /// </summary>
    public string BuildAdaptiveRoute(double accidentProbability)
    {
        string route;

        if (accidentProbability >= 50.0)
        {
            route = "Route changed to hospital street with low traffic density.";
        }
        else
        {
            route = "Route remains optimal through central avenue.";
        }

        return route;
    }
}
