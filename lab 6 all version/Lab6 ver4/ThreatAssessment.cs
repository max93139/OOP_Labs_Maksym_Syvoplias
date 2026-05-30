namespace Lab6;

/// <summary>
/// Зберігає результат оцінки загрози: числовий показник, перевантаження та аналіз.
/// Окремий клас необхідний, щоб не змішувати дані оцінки з логікою системи безпеки (SRP).
/// </summary>
public sealed class ThreatAssessment
{
    /// <summary>
    /// Ініціалізує нову оцінку загрози.
    /// </summary>
    public ThreatAssessment(double score, double gForce, string analysis)
    {
        Score = score;
        GForce = gForce;
        Analysis = analysis;
    }

    /// <summary>
    /// Повертає числовий показник серйозності загрози від 0 до 10.
    /// </summary>
    public double Score { get; }

    /// <summary>
    /// Повертає розраховане потенційне перевантаження при зіткненні в одиницях G.
    /// </summary>
    public double GForce { get; }

    /// <summary>
    /// Повертає текстовий аналіз виявленої загрози.
    /// </summary>
    public string Analysis { get; }
}
