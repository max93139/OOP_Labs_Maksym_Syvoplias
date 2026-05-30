namespace Lab6;

/// <summary>
/// Представляє зовнішній каркас автомобіля та прораховує лобовий опір кузова при трансформації.
/// </summary>
public sealed class Body
{
    /// <summary>
    /// Ініціалізує новий кузов.
    /// </summary>
    public Body(string material, string color)
    {
        Material = material;
        Color = color;
        IsSealed = true;
        Shape = "Road capsule";
    }

    /// <summary>
    /// Повертає матеріал кузова.
    /// </summary>
    public string Material { get; }

    /// <summary>
    /// Повертає колір кузова.
    /// </summary>
    public string Color { get; }

    /// <summary>
    /// Повертає значення, що показує герметичність кузова.
    /// </summary>
    public bool IsSealed { get; private set; }

    /// <summary>
    /// Повертає поточну форму кузова.
    /// </summary>
    public string Shape { get; private set; }

    /// <summary>
    /// Відкриває пасажирські двері та знімає герметизацію салону.
    /// </summary>
    public string OpenDoors()
    {
        IsSealed = false;
        return "Двері відчинено, посадка дозволена.";
    }

    /// <summary>
    /// Змінює форму кузова відповідно до режиму трансформації та розраховує коефіцієнт лобового опору.
    /// </summary>
    public string ChangeShape(string mode)
    {
        double frontalAreaSquareMeters;
        double shapeDragCoefficient;

        if (mode.Equals("Water", StringComparison.OrdinalIgnoreCase))
        {
            Shape = "Гідродинамічна човнова капсула";
            IsSealed = true;
            frontalAreaSquareMeters = 1.65;
            shapeDragCoefficient = 0.18;
        }
        else
        {
            if (mode.Equals("Air", StringComparison.OrdinalIgnoreCase))
            {
                Shape = "Крилатий політний модуль";
                IsSealed = true;
                frontalAreaSquareMeters = 4.20;
                shapeDragCoefficient = 0.08;
            }
            else
            {
                Shape = "Дорожня капсула";
                IsSealed = true;
                frontalAreaSquareMeters = 2.10;
                shapeDragCoefficient = 0.22;
            }
        }

        double dragAreaProduct = frontalAreaSquareMeters * shapeDragCoefficient;
        string sealStatus;
        if (IsSealed)
        {
            sealStatus = "100% водонепроникний герметичний корпус під тиском";
        }
        else
        {
            sealStatus = "негерметичний";
        }

        return $"Форму кузова змінено на {Shape}. Площа лобового перерізу: {frontalAreaSquareMeters:F2} м², коефіцієнт лобового опору (Cd*A): {dragAreaProduct:F3} м². Статус герметизації: {sealStatus}.";
    }
}
