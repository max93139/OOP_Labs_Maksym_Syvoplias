using System.Collections.Generic;

namespace Lab6;

/// <summary>
/// Інтерфейс, який узагальнює основні функції керування, діагностики та оновлення для розумних автомобілів.
/// </summary>
public interface ISmartVehicle
{
    /// <summary>
    /// Повертає ідентифікаційні дані автомобіля.
    /// </summary>
    VehicleIdentity Identity { get; }

    /// <summary>
    /// Самостійно виконує повний цикл автономного руху за сценарієм.
    /// </summary>
    IReadOnlyList<ScenarioResult> RunAutonomousCycle(ScenarioData data, Service service, List<string> protocolLines);

    /// <summary>
    /// Активує автомобіль і готує системи руху.
    /// </summary>
    IReadOnlyList<string> Activate();

    /// <summary>
    /// Трансформує автомобіль відповідно до режиму.
    /// </summary>
    IReadOnlyList<string> Transform(string mode);

    /// <summary>
    /// Вмикає автопілот.
    /// </summary>
    IReadOnlyList<string> EnableAutopilot(double riskValue);

    /// <summary>
    /// Стабілізує рух на покритті.
    /// </summary>
    IReadOnlyList<string> Stabilize(string surfaceName, double speedKmh);
}
