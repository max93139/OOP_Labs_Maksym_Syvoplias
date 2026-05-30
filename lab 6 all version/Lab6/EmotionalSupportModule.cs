using System;

namespace Lab6;

/// <summary>
/// Виявляє емоційний стан водія та активує заспокійливі або стимулюючі функції.
/// </summary>
public class EmotionalSupportModule
{
    private string _emotionalState;
    private double _stressLevel;

    /// <summary>
    /// Конструктор за замовчуванням.
    /// </summary>
    public EmotionalSupportModule()
    {
        _emotionalState = "Neutral";
        _stressLevel = 10.0;
    }

    /// <summary>
    /// Конструктор з параметрами.
    /// </summary>
    public EmotionalSupportModule(string emotionalState, double stressLevel)
    {
        _emotionalState = emotionalState;
        _stressLevel = stressLevel;
    }

    /// <summary>
    /// Конструктор копіювання.
    /// </summary>
    public EmotionalSupportModule(EmotionalSupportModule other)
    {
        _emotionalState = other.EmotionalState;
        _stressLevel = other.StressLevel;
    }

    public string EmotionalState
    {
        get => _emotionalState;
        set => _emotionalState = value;
    }

    public double StressLevel
    {
        get => _stressLevel;
        set => _stressLevel = value;
    }

    public string SuggestAdvice(double stressLevel)
    {
        _stressLevel = stressLevel;
        if (_stressLevel > 70.0)
        {
            _emotionalState = "Stressed";
            return "Виявлено високий рівень стресу! Будь ласка, зробіть кілька глибоких вдихів та зменшіть швидкість.";
        }
        else if (_stressLevel > 30.0)
        {
            _emotionalState = "Fatigued";
            return "Помірна втома. Бажано зробити зупинку на каву або провітрити салон.";
        }
        else
        {
            _emotionalState = "Calm";
            return "Стан водія стабільний та спокійний.";
        }
    }

    public string TurnOnRelaxMusic()
    {
        return "Активовано аудіоплейлист \"Антистрес\" із заспокійливими звуками природи.";
    }

    public string ActivateChromotherapy()
    {
        return "Зміна підсвітки салону на м'який релаксуючий бірюзовий колір (хромотерапія).";
    }
}
