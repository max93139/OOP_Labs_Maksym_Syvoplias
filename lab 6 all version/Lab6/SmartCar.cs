using System.Collections.Generic;

namespace Lab6;

/// <summary>
/// Представляє розумний автомобіль як головний скомпонований об'єкт.
/// Він виступає повністю автономним агентом, що самостійно керує підсистемами (SRP/Хореографія).
/// </summary>
public sealed class SmartCar
{
    private readonly Body body;
    private readonly Engine engine;
    private readonly Chassis chassis;
    private readonly TransformationModule transformationModule;
    private readonly SmartSystem smartSystem;

    /// <summary>
    /// Ініціалізує новий розумний автомобіль зі скомпонованих та агрегованих частин.
    /// </summary>
    public SmartCar(
        VehicleIdentity identity,
        Body body,
        Engine engine,
        Chassis chassis,
        TransformationModule transformationModule,
        SmartSystem smartSystem)
    {
        Identity = identity;
        this.body = body;
        this.engine = engine;
        this.chassis = chassis;
        this.transformationModule = transformationModule;
        this.smartSystem = smartSystem;
    }

    /// <summary>
    /// Повертає ідентифікаційні дані розумного автомобіля.
    /// </summary>
    public VehicleIdentity Identity { get; }

    /// <summary>
    /// Самостійно виконує повний цикл автономного руху за сценарієм, взаємодіючи з підсистемами та логуючи через сервіс.
    /// </summary>
    public IReadOnlyList<ScenarioResult> RunAutonomousCycle(ScenarioData data, Service service, List<string> protocolLines)
    {
        List<ScenarioResult> results = new List<ScenarioResult>();

        LogLine("================================================================================", service, protocolLines);
        LogLine($"СЦЕНАРІЙ {data.ScenarioNumber}: {data.ScenarioName}", service, protocolLines);
        LogLine("================================================================================", service, protocolLines);

        // 1. Активація автомобіля
        foreach (string line in Activate())
        {
            LogLine(line, service, protocolLines);
        }

        // 2. Моніторинг здоров'я водія (система сама опитує свої агреговані датчики!)
        IReadOnlyList<SensorReading> readings = smartSystem.DriverStateSensor.ReadDriverState();
        foreach (SensorReading reading in readings)
        {
            string translatedName;
            switch (reading.Name)
            {
                case "Pulse":
                {
                    translatedName = "Пульс";
                    break;
                }
                case "Blood pressure":
                {
                    translatedName = "Артеріальний тиск";
                    break;
                }
                case "Eye fatigue":
                {
                    translatedName = "Втома очей";
                    break;
                }
                default:
                {
                    translatedName = reading.Name;
                    break;
                }
            }
            LogLine($"{translatedName}: {reading.Value:F1} {reading.Unit}", service, protocolLines);
        }

        ScenarioResult healthResult = smartSystem.MonitorDriver();
        results.Add(healthResult);
        LogLine(healthResult.ToProtocolLine(), service, protocolLines);

        // 3. Прогноз дорожніх ризиків
        ScenarioResult forecastResult = smartSystem.ForecastRoadRisk(
            healthResult.Value,
            data.RoadCondition,
            data.HasPedestrian,
            data.HasRoadWorks
        );
        results.Add(forecastResult);
        LogLine(forecastResult.ToProtocolLine(), service, protocolLines);

        // 4. Трансформація кузова
        foreach (string line in Transform(data.TransformationMode))
        {
            LogLine(line, service, protocolLines);
        }

        // 5. Голосове керування
        LogLine(smartSystem.HandleVoiceCommand(data.VoiceCommand), service, protocolLines);

        // 6. Балансування клімату
        LogLine(smartSystem.BalanceClimate(), service, protocolLines);

        // 7. Система безпеки та захист
        string threatName;
        if (data.HasPedestrian)
        {
            threatName = "загроза зіткнення та втомлений водій";
        }
        else
        {
            threatName = "мокра дорога та втомлений водій";
        }

        foreach (string line in smartSystem.ProtectPassengers(threatName))
        {
            LogLine(line, service, protocolLines);
        }

        // 8. Автопілот та побудова безпечного адаптивного маршруту
        foreach (string line in EnableAutopilot(forecastResult.Value))
        {
            LogLine(line, service, protocolLines);
        }

        // 9. Стабілізація руху шасі з розрахунком передачі трансмісії
        foreach (string line in Stabilize(data.StabilizationSurface, data.VehicleSpeedKmh))
        {
            LogLine(line, service, protocolLines);
        }

        // 10. Самонавчання ШІ
        if (data.ScenarioNumber == 3)
        {
            LogLine("Активовано нічний режим польоту.", service, protocolLines);
            LogLine(smartSystem.SaveExperience(15, 5), service, protocolLines);
        }
        else
        {
            if (data.ScenarioNumber == 2)
            {
                LogLine("Активовано режим сутінків.", service, protocolLines);
                LogLine(smartSystem.SaveExperience(10, 5), service, protocolLines);
            }
            else
            {
                LogLine("Режим день: стандартне освітлення.", service, protocolLines);
                LogLine(smartSystem.SaveExperience(5, 5), service, protocolLines);
            }
        }

        LogLine("", service, protocolLines);
        return results;
    }

    /// <summary>
    /// Допоміжний метод для одночасного логування в консоль та накопичення рядків протоколу.
    /// </summary>
    private void LogLine(string line, Service service, List<string> protocolLines)
    {
        service.WriteConsole(line);
        protocolLines.Add(line);
    }

    /// <summary>
    /// Активує автомобіль і готує системи руху.
    /// </summary>
    public IReadOnlyList<string> Activate()
    {
        return new List<string>
        {
            body.OpenDoors(),
            engine.Start(),
            chassis.ChangeClearance(18.5)
        };
    }

    /// <summary>
    /// Трансформує автомобіль через узгодження кузова, двигуна та модуля трансформації.
    /// </summary>
    public IReadOnlyList<string> Transform(string mode)
    {
        return new List<string>
        {
            transformationModule.ActivateMode(mode),
            body.ChangeShape(mode),
            engine.ChangeMode("Eco")
        };
    }

    /// <summary>
    /// Вмикає автопілот через взаємодію шасі та smart-системи.
    /// </summary>
    public IReadOnlyList<string> EnableAutopilot(double riskValue)
    {
        return new List<string>
        {
            chassis.ActivateAutopilot(),
            smartSystem.BuildRoute(riskValue)
        };
    }

    /// <summary>
    /// Стабілізує рух на вибраному покритті з урахуванням швидкості.
    /// </summary>
    public IReadOnlyList<string> Stabilize(string surfaceName, double speedKmh)
    {
        return chassis.StabilizeMovement(surfaceName, speedKmh);
    }
}
