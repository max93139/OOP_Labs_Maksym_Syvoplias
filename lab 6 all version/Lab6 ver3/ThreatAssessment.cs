using System;

namespace Lab6;

/// <summary>
/// Зберігає результат оцінки загрози: числовий показник, перевантаження та аналіз.
/// </summary>
public sealed class ThreatAssessment
{
    private double _score;
    private double _gForce;
    private string _analysis;

    /// <summary>
    /// Конструктор за замовчуванням (Canonical Class Template).
    /// </summary>
    public ThreatAssessment()
    {
        _score = 0.0;
        _gForce = 1.0;
        _analysis = "No active threats detected.";
    }

    /// <summary>
    /// Ініціалізує нову оцінку загрози.
    /// </summary>
    public ThreatAssessment(double score, double gForce, string analysis)
    {
        _score = score;
        _gForce = gForce;
        _analysis = analysis;
    }

    /// <summary>
    /// Конструктор копіювання (Canonical Class Template).
    /// </summary>
    public ThreatAssessment(ThreatAssessment other)
    {
        _score = other.Score;
        _gForce = other.GForce;
        _analysis = other.Analysis;
    }

    /// <summary>
    /// Повертає числовий показник серйозності загрози від 0 до 10.
    /// </summary>
    public double Score
    {
        get
        {
            return _score;
        }
        set
        {
            _score = value;
        }
    }

    /// <summary>
    /// Повертає розраховане потенційне перевантаження при зіткненні в одиницях G.
    /// </summary>
    public double GForce
    {
        get
        {
            return _gForce;
        }
        set
        {
            _gForce = value;
        }
    }

    /// <summary>
    /// Повертає текстовий аналіз виявленої загрози.
    /// </summary>
    public string Analysis
    {
        get
        {
            return _analysis;
        }
        set
        {
            _analysis = value;
        }
    }
}
