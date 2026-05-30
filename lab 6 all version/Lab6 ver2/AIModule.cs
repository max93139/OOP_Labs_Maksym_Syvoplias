using System;

namespace Lab6;

/// <summary>
/// Інтегрована система прийняття рішень на базі штучного інтелекту.
/// </summary>
public class AIModule : SmartDevice
{
    private string _version;
    private string _workingMode;
    private int _autonomyLevel;

    /// <summary>
    /// Конструктор за замовчуванням.
    /// </summary>
    public AIModule() : base("Модуль штучного інтелекту", 0.15)
    {
        _version = "v1.0.0";
        _workingMode = "Standard";
        _autonomyLevel = 5;
    }

    /// <summary>
    /// Конструктор з параметрами.
    /// </summary>
    public AIModule(string version, string workingMode, int autonomyLevel) : base("Модуль штучного інтелекту", 0.15)
    {
        _version = version;
        _workingMode = workingMode;
        _autonomyLevel = autonomyLevel;
    }

    /// <summary>
    /// Конструктор копіювання.
    /// </summary>
    public AIModule(AIModule other) : base(other.DeviceName, other.PowerConsumption)
    {
        _version = other.Version;
        _workingMode = other.WorkingMode;
        _autonomyLevel = other.AutonomyLevel;
    }

    public string Version
    {
        get => _version;
        set => _version = value;
    }

    public string WorkingMode
    {
        get => _workingMode;
        set => _workingMode = value;
    }

    public int AutonomyLevel
    {
        get => _autonomyLevel;
        set => _autonomyLevel = value;
    }

    public string AnalyzeData(string inputData)
    {
        return $"[AI Module {_version}]: Аналіз вхідних даних: \"{inputData}\" завершено.";
    }

    public string MakeDecision(string context)
    {
        return $"[AI Module {_version}]: Прийнято оптимальне рішення для контексту \"{context}\" у режимі {_workingMode}.";
    }

    public string AdaptBehavior(string environment)
    {
        return $"[AI Module {_version}]: Поведінка адаптована під оточення: \"{environment}\".";
    }

    public int UpdateModel(int currentEpisodes, int newEpisodes)
    {
        if (currentEpisodes < 0 || newEpisodes < 0)
        {
            throw new ArgumentException("Помилка оновлення моделі штучного інтелекту: від'ємна кількість епізодів навчання!");
        }
        return currentEpisodes + newEpisodes;
    }

    public override string GetStatus()
    {
        return $"Модуль '{DeviceName}' працює. Версія: {_version}, автономність: Рівень {_autonomyLevel}. Енергоспоживання: {PowerConsumption} кВт.";
    }
}
