namespace Lab6;

/// <summary>
/// Розпізнає дорожні об'єкти за даними камер та оцінює ризик середовища.
/// </summary>
public sealed class ComputerVisionModule
{
    /// <summary>
    /// Розпізнає об'єкти навколо автомобіля за замовчуванням.
    /// </summary>
    public IReadOnlyList<string> RecognizeObjects(int cameraCount)
    {
        return RecognizeObjects(cameraCount, "вологий асфальт", true, true);
    }

    /// <summary>
    /// Розпізнає об'єкти навколо автомобіля за заданими параметрами сценарію.
    /// </summary>
    public IReadOnlyList<string> RecognizeObjects(int cameraCount, string roadCondition, bool hasPedestrian, bool hasRoadWorks)
    {
        List<string> objects = new List<string>();

        if (hasPedestrian)
        {
            double pedestrianConfidence = 0.984;
            double pedestrianDistanceMeter = 14.5;
            objects.Add($"пішохід (відстань: {pedestrianDistanceMeter:F1}м, впевненість: {pedestrianConfidence:P1})");
        }
        else
        {
            // У цьому сценарії пішоходів не виявлено
        }

        if (hasRoadWorks)
        {
            double roadWorksConfidence = 0.912;
            double roadWorksDistanceMeter = 45.0;
            objects.Add($"дорожні роботи (відстань: {roadWorksDistanceMeter:F1}м, впевненість: {roadWorksConfidence:P1})");
        }
        else
        {
            // У цьому сценарії дорожніх робіт не виявлено
        }

        double surfaceConfidence = 0.991;
        objects.Add($"{roadCondition} (впевненість: {surfaceConfidence:P1})");

        objects.Add($"{cameraCount} камер синхронізовано та активні");
        return objects;
    }

    /// <summary>
    /// Оцінює ризик навколишнього середовища, сумуючи вагові коефіцієнти для кожного розпізнаного об'єкта.
    /// </summary>
    public double EstimateEnvironmentRisk(IReadOnlyList<string> recognizedObjects)
    {
        double totalRiskScore = 0.0;

        foreach (string obj in recognizedObjects)
        {
            string normalizedObject = obj.ToLowerInvariant();
            double individualRisk;

            if (normalizedObject.Contains("пішохід", StringComparison.Ordinal))
            {
                individualRisk = 15.5;
            }
            else
            {
                if (normalizedObject.Contains("дорожні роботи", StringComparison.Ordinal))
                {
                    individualRisk = 8.0;
                }
                else
                {
                    if (normalizedObject.Contains("волог", StringComparison.Ordinal) || 
                        normalizedObject.Contains("вод", StringComparison.Ordinal) ||
                        normalizedObject.Contains("ожелед", StringComparison.Ordinal) ||
                        normalizedObject.Contains("сніг", StringComparison.Ordinal) ||
                        normalizedObject.Contains("лід", StringComparison.Ordinal))
                    {
                        individualRisk = 12.5;
                    }
                    else
                    {
                        individualRisk = 2.0;
                    }
                }
            }

            totalRiskScore += individualRisk;
        }

        return Math.Min(100.0, totalRiskScore);
    }
}
