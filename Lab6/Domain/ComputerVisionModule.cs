namespace Lab6.Domain;

/// <summary>
/// Розпізнає дорожні об'єкти за даними камер.
/// </summary>
public sealed class ComputerVisionModule
{
    /// <summary>
    /// Розпізнає об'єкти навколо автомобіля.
    /// </summary>
    public IReadOnlyList<string> RecognizeObjects(int cameraCount)
    {
        List<string> objects = new List<string>
        {
            "pedestrian",
            "road works",
            "wet asphalt"
        };

        objects.Add($"{cameraCount} cameras synchronized");
        return objects;
    }

    /// <summary>
    /// Оцінює ризик навколишнього середовища.
    /// </summary>
    public double EstimateEnvironmentRisk(IReadOnlyList<string> recognizedObjects)
    {
        return recognizedObjects.Count * 8.5;
    }
}
