using System.Collections.Generic;

namespace Lab6;

/// <summary>
/// Представляє розумний автомобіль як головний скомпонований об'єкт у Версії 3.
/// Він виступає автономним агентом, що реалізує надійність системи через блоки try-catch.
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
    /// Самостійно виконує повний цикл автономного руху за сценарієм, взаємодіючи з підсистемами та обробляючи винятки.
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

        // 2. Моніторинг здоров'я водія та перевірка біометрії
        ScenarioResult healthResult;
        try
        {
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

            // Симулюємо помилку профілю у Сценарії 2 (змінимо перше показання на аномальний пульс 35 bpm)
            if (data.ScenarioNumber == 2)
            {
                FixedSensor anomalousPulse = new FixedSensor("Pulse", 35.0, "bpm");
                List<ISensor> anomalousSensors = new List<ISensor>
                {
                    anomalousPulse,
                    new FixedSensor("Blood pressure", data.DriverPressure, "mmHg"),
                    new FixedSensor("Eye fatigue", data.DriverEyeFatigue, "%")
                };
                DriverStateSensor tempSensor = new DriverStateSensor(anomalousSensors);
                smartSystem.MonitorDriver(); // Це викине доменний виняток профілю при читанні
            }
            else
            {
                // Для Сценарію 3 пульс дорівнює 120 bpm, що викине доменний виняток недієздатності
            }

            healthResult = smartSystem.MonitorDriver();
            results.Add(healthResult);
            LogLine(healthResult.ToProtocolLine(), service, protocolLines);
        }
        catch (SmartCarException ex) when (ex.IsType(SmartCarException.DriverImpairment))
        {
            LogLine($"\n[УВАГА]: Перехоплено виняток: {ex.Message}", service, protocolLines);
            LogLine("[СИСТЕМА БЕЗПЕКИ]: Терміново активовано екстрений медичний протокол! Автопілот перебирає керування.", service, protocolLines);
            healthResult = new ScenarioResult("Медичний моніторинг водія", 100.0, "ЕКСТРЕНИЙ РЕЖИМ: Автопілот активовано примусово!");
            results.Add(healthResult);
        }
        catch (SmartCarException ex) when (ex.IsType(SmartCarException.ProfileMismatch))
        {
            LogLine($"\n[УВАГА]: Перехоплено виняток: {ex.Message}", service, protocolLines);
            LogLine("[СИСТЕМА БЕЗПЕКИ]: Біометричний профіль не підтверджено! Двигун заблоковано, відправлено запит власнику.", service, protocolLines);
            healthResult = new ScenarioResult("Медичний моніторинг водія", 0.0, "БЛОКУВАННЯ: Профіль водія не відповідає власнику!");
            results.Add(healthResult);
        }

        // 3. Прогноз дорожніх ризиків
        ScenarioResult forecastResult;
        try
        {
            // Симулюємо помилку інтерпретації контексту, якщо пристроїв камери мало (наприклад, у Сценарії 2 передамо 3 камери)
            int cameraCount = (data.ScenarioNumber == 2) ? 3 : 6;
            
            // Якщо камери заблоковані, це викине виняток в RecognizeObjects
            IReadOnlyList<string> objects = smartSystem.ComputerVisionModule.RecognizeObjects(cameraCount, data.RoadCondition, data.HasPedestrian, data.HasRoadWorks);
            
            forecastResult = smartSystem.ForecastRoadRisk(healthResult.Value, data.RoadCondition, data.HasPedestrian, data.HasRoadWorks);
            results.Add(forecastResult);
            LogLine(forecastResult.ToProtocolLine(), service, protocolLines);
        }
        catch (SmartCarException ex) when (ex.IsType(SmartCarException.ContextInterpretation))
        {
            LogLine($"\n[УВАГА]: Перехоплено виняток: {ex.Message}", service, protocolLines);
            LogLine("[КОМП'ЮТЕРНИЙ ЗОР]: Камери забруднено! Переходимо на ультразвукові та радарні датчики резервного сканування.", service, protocolLines);
            forecastResult = new ScenarioResult("Прогноз дорожніх ризиків", 25.0, "РЕЗЕРВНИЙ РЕЖИМ: Сканування через радари активоване.");
            results.Add(forecastResult);
        }

        // 4. Трансформація кузова
        try
        {
            // Симулюємо помилку виходу з водного режиму, якщо в Сценарії 2 ми намагаємося вимкнути водний режим на глибині (наприклад, глибина 3.5м)
            if (data.ScenarioNumber == 2)
            {
                LogLine(transformationModule.ExitWaterMode(3.5), service, protocolLines);
            }
            else
            {
                foreach (string line in Transform(data.TransformationMode))
                {
                    LogLine(line, service, protocolLines);
                }
            }
        }
        catch (SmartCarException ex) when (ex.IsType(SmartCarException.WaterExitDepth))
        {
            LogLine($"\n[УВАГА]: Перехоплено виняток: {ex.Message}", service, protocolLines);
            LogLine("[ТРАНСФОРМАЦІЯ]: Спроба відхилена! Гідродинамічний корпус залишається активованим до досягнення мілководдя.", service, protocolLines);
            // Залишаємо водний режим
            LogLine(transformationModule.ActivateMode("Water"), service, protocolLines);
        }

        // 5. Голосове керування
        try
        {
            // Симулюємо невалідну голосову команду або надмірну кількість команд.
            string voiceCommand = data.VoiceCommand;
            if (data.ScenarioNumber == 1)
            {
                voiceCommand = "дурниця";
            }
            else
            {
                if (data.ScenarioNumber == 2)
                {
                    voiceCommand = "Увімкнути автопілот і Змінити клімат";
                }
                else
                {
                    // Сценарій 3 залишається стандартним
                }
            }

            LogLine(smartSystem.HandleVoiceCommand(voiceCommand), service, protocolLines);
        }
        catch (SmartCarException ex) when (ex.IsType(SmartCarException.InvalidVoiceCommand))
        {
            LogLine($"\n[УВАГА]: Перехоплено виняток: {ex.Message}", service, protocolLines);
            LogLine("[ГОЛОСОВИЙ АСИСТЕНТ]: Невідома команда. Будь ласка, повторіть ваш запит чіткіше.", service, protocolLines);
        }
        catch (SmartCarException ex) when (ex.IsType(SmartCarException.TooManyCommands))
        {
            LogLine($"\n[УВАГА]: Перехоплено виняток: {ex.Message}", service, protocolLines);
            LogLine("[ГОЛОСОВИЙ АСИСТЕНТ]: Отримано кілька паралельних команд. Виконуємо лише першу дію.", service, protocolLines);
        }

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
        try
        {
            // Симулюємо конфлікт навігації при відхиленні ймовірності аварії
            double risk = forecastResult.Value;
            if (data.ScenarioNumber == 3)
            {
                // Спеціально передаємо некоректне значення ризику (> 100), щоб викликати конфлікт карт
                risk = 120.0;
            }
            foreach (string line in EnableAutopilot(risk))
            {
                LogLine(line, service, protocolLines);
            }
        }
        catch (SmartCarException ex) when (ex.IsType(SmartCarException.NavigationConflict))
        {
            LogLine($"\n[УВАГА]: Перехоплено виняток: {ex.Message}", service, protocolLines);
            LogLine("[НАВІГАЦІЯ]: Картографічний конфлікт вирішено примусовим перемиканням на локальну офлайн-навігацію.", service, protocolLines);
            // Викликаємо резервний офлайн-автопілот
            foreach (string line in EnableAutopilot(50.0))
            {
                LogLine(line, service, protocolLines);
            }
        }

        // 9. Стабілізація руху шасі з розрахунком передачі трансмісії
        foreach (string line in Stabilize(data.StabilizationSurface, data.VehicleSpeedKmh))
        {
            LogLine(line, service, protocolLines);
        }

        // 10. Самонавчання ШІ
        try
        {
            int currentEpisodes = 15;
            int newEpisodes = 5;
            if (data.ScenarioNumber == 3)
            {
                // Передамо від'ємну кількість епізодів для виклику помилки модуля ШІ
                currentEpisodes = -10;
            }

            if (data.ScenarioNumber == 3)
            {
                LogLine("Активовано нічний режим польоту.", service, protocolLines);
                LogLine(smartSystem.SaveExperience(currentEpisodes, newEpisodes), service, protocolLines);
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
        }
        catch (SmartCarException ex) when (ex.IsType(SmartCarException.AiModuleFailure))
        {
            LogLine($"\n[УВАГА]: Перехоплено виняток: {ex.Message}", service, protocolLines);
            LogLine("[ШТУЧНИЙ ІНТЕЛЕКТ]: Самонавчання призупинено. Відновлюємо попередню стабільну модель нейромережі.", service, protocolLines);
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
