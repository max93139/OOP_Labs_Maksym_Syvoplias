using System;

namespace Lab6;

/// <summary>
/// Надає поради та пропозиції водієві на основі стану середовища та настрою водія.
/// </summary>
public class RecommendationModule
{
    private string _recommendationType;
    private string _userProfile;

    /// <summary>
    /// Конструктор за замовчуванням.
    /// </summary>
    public RecommendationModule()
    {
        _recommendationType = "General";
        _userProfile = "Standard Driver";
    }

    /// <summary>
    /// Конструктор з параметрами.
    /// </summary>
    public RecommendationModule(string recommendationType, string userProfile)
    {
        _recommendationType = recommendationType;
        _userProfile = userProfile;
    }

    /// <summary>
    /// Конструктор копіювання.
    /// </summary>
    public RecommendationModule(RecommendationModule other)
    {
        _recommendationType = other.RecommendationType;
        _userProfile = other.UserProfile;
    }

    public string RecommendationType
    {
        get => _recommendationType;
        set => _recommendationType = value;
    }

    public string UserProfile
    {
        get => _userProfile;
        set => _userProfile = value;
    }

    public string SuggestRoute(double accidentProbability)
    {
        if (accidentProbability > 50.0)
        {
            return "Рекомендовано обрати безпечний обхідний маршрут через високі дорожні ризики.";
        }
        else
        {
            return "Поточний оптимальний швидкісний маршрут є повністю безпечним.";
        }
    }

    public string SuggestMusic(double stressLevel)
    {
        if (stressLevel > 60.0)
        {
            return "Виявлено підвищений стрес! Запропоновано увімкнути релаксуючий ембієнт.";
        }
        else
        {
            return "Запропоновано стандартний плейлист улюблених треків.";
        }
    }

    public string RemindMeeting(string meetingName)
    {
        return $"Нагадування: Зустріч \"{meetingName}\" запланована найближчим часом.";
    }
}
