namespace Lab6.Domain;

/// <summary>
/// Представляє зовнішній каркас автомобіля.
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
        return "Doors are open and boarding is allowed.";
    }

    /// <summary>
    /// Змінює форму кузова відповідно до вибраного режиму трансформації.
    /// </summary>
    public string ChangeShape(TransformationMode mode)
    {
        Shape = mode switch
        {
            TransformationMode.Water => "Hydrodynamic boat capsule",
            TransformationMode.Air => "Wing-supported flight capsule",
            _ => "Road capsule"
        };

        return $"Body shape changed to {Shape}.";
    }
}
