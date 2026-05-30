using System.Collections.Generic;

namespace Lab6;

/// <summary>
/// Представляє розумний автомобіль як головний скомпонований об'єкт у Версії 4.
/// Він виступає повністю автономним агентом, що реалізує взаємодію через події та делегати.
/// </summary>
public sealed class SmartCar
{
    /// <summary>
    /// Делегат для обробки критичних подій розумного автомобіля.
    /// </summary>
    /// <param name="eventMessage">Локалізоване повідомлення про виявлену подію.</param>
    public delegate void SmartCarEventHandler(string eventMessage);

    private readonly Body body;
    private readonly Engine engine;
    private readonly Chassis chassis;
    private readonly TransformationModule transformationModule;
    private readonly SmartSystem smartSystem;

    // Тимчасові поля для доступу до логера в обробниках подій
    private Service? currentService;
    private List<string>? currentProtocol;

    // Оголошення 5 доменних подій на основі нашого кастомного делегата
    public event SmartCarEventHandler? OnDriverImpaired;
    public event SmartCarEventHandler? OnCollisionImminent;
    public event SmartCarEventHandler? OnGpsSignalLost;
    public event SmartCarEventHandler? OnDriverMoodChanged;
    public event SmartCarEventHandler? OnSuddenHealthDrop;

    /// <summary>
    /// Ініціалізує новий розумний автомобіль зі скомпонованих та агрегованих частин та налаштовує події.
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

        // Підписка на події в конструкторі
        OnDriverImpaired += HandleDriverImpairedEvent;
        OnCollisionImminent += HandleCollisionImminentEvent;
        OnGpsSignalLost += HandleGpsSignalLostEvent;
        OnDriverMoodChanged += HandleDriverMoodChangedEvent;
        OnSuddenHealthDrop += HandleSuddenHealthDropEvent;
    }

    /// <summary>
    /// Повертає ідентифікаційні дані розумного автомобіля.
    /// </summary>
    public VehicleIdentity Identity { get; }

    /// <summary>
    /// Самостійно виконує повний цикл автономного руху за сценарієм, взаємодіючи з підсистемами, обробляючи винятки та викликаючи події.
    /// </summary>
    public IReadOnlyList<ScenarioResult> RunAutonomousCycle(ScenarioData data, Service service, List<string> protocolLines)
    {
        currentService = service;
        currentProtocol = protocolLines;
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

            // Симулюємо зміну настрою водія при втомі очей більше 30%
            if (data.DriverEyeFatigue >= 30.0)
            {
                OnDriverMoodChanged?.Invoke($"Камера розпізнавання обличчя зафіксувала високу втому очей водія ({data.DriverEyeFatigue:F1}%).");
            }
            else
            {
                // Звичайний настрій
            }

            // Симулюємо ProfileMismatchException у Сценарії 2 (змінимо перше показання на аномальний пульс 35 bpm)
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
                smartSystem.MonitorDriver(); // Це викине ProfileMismatchException при читанні
            }
            else
            {
                // Для Сценарію 3 пульс дорівнює 120 bpm, що викине DriverImpairmentException
            }

            healthResult = smartSystem.MonitorDriver();
            results.Add(healthResult);
            LogLine(healthResult.ToProtocolLine(), service, protocolLines);
        }
        catch (DriverImpairmentException ex)
        {
            LogLine($"\n[УВАГА]: Перехоплено виняток: {ex.Message}", service, protocolLines);
            
            // Викликаємо події раптового колапсу здоров'я та недієздатності
            OnSuddenHealthDrop?.Invoke($"Медичні датчики зафіксували небезпечний пульс водія: {data.DriverPulse:F1} bpm.");
            OnDriverImpaired?.Invoke("Водій повністю недієздатний за медичними показниками!");

            healthResult = new ScenarioResult("Медичний моніторинг водія", 100.0, "ЕКСТРЕНИЙ РЕЖИМ: Автопілот активовано примусово!");
            results.Add(healthResult);
        }
        catch (ProfileMismatchException ex)
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
            // Симулюємо неминуче зіткнення при наявності пішохода
            if (data.HasPedestrian)
            {
                OnCollisionImminent?.Invoke("Виявлено пішохода безпосередньо перед автомобілем на небезпечній відстані!");
            }
            else
            {
                // Немає загрози зіткнення
            }

            // Симулюємо ContextInterpretationException, якщо пристроїв камери мало (наприклад, у Сценарії 2 передамо 3 камери)
            int cameraCount = (data.ScenarioNumber == 2) ? 3 : 6;
            
            // Якщо камери заблоковані, це викине виняток в RecognizeObjects
            IReadOnlyList<string> objects = smartSystem.ComputerVisionModule.RecognizeObjects(cameraCount, data.RoadCondition, data.HasPedestrian, data.HasRoadWorks);
            
            forecastResult = smartSystem.ForecastRoadRisk(healthResult.Value, data.RoadCondition, data.HasPedestrian, data.HasRoadWorks);
            results.Add(forecastResult);
            LogLine(forecastResult.ToProtocolLine(), service, protocolLines);
        }
        catch (ContextInterpretationException ex)
        {
            LogLine($"\n[УВАГА]: Перехоплено виняток: {ex.Message}", service, protocolLines);
            LogLine("[КОМП'ЮТЕРНИЙ ЗОР]: Камери забруднено! Переходимо на ультразвукові та радарні датчики резервного сканування.", service, protocolLines);
            forecastResult = new ScenarioResult("Прогноз дорожніх ризиків", 25.0, "РЕЗЕРВНИЙ РЕЖИМ: Сканування через радари активоване.");
            results.Add(forecastResult);
        }

        // 4. Трансформація кузова
        try
        {
            // Симулюємо WaterExitDepthException, якщо в Сценарії 2 ми намагаємося вимкнути водний режим на глибині (наприклад, глибина 3.5м)
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
        catch (WaterExitDepthException ex)
        {
            LogLine($"\n[УВАГА]: Перехоплено виняток: {ex.Message}", service, protocolLines);
            LogLine("[ТРАНСФОРМАЦІЯ]: Спроба відхилена! Гідродинамічний корпус залишається активованим до досягнення мілководдя.", service, protocolLines);
            // Залишаємо водний режим
            LogLine(transformationModule.ActivateMode("Water"), service, protocolLines);
        }

        // 5. Голосове керування
        try
        {
            // Симулюємо InvalidVoiceCommandException (якщо в сценарії 1 відправити команду "nonsense" або пусту)
            // Симулюємо TooManyCommandsException (якщо в сценарії 2 відправити команду "Увімкнути автопілот і Змінити клімат")
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
        catch (InvalidVoiceCommandException ex)
        {
            LogLine($"\n[УВАГА]: Перехоплено виняток: {ex.Message}", service, protocolLines);
            LogLine("[ГОЛОСОВИЙ АСИСТЕНТ]: Невідома команда. Будь ласка, повторіть ваш запит чіткіше.", service, protocolLines);
        }
        catch (TooManyCommandsException ex)
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
            // Симулюємо NavigationConflictException при відхиленні ймовірності аварії
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
        catch (NavigationConflictException ex)
        {
            LogLine($"\n[УВАГА]: Перехоплено виняток: {ex.Message}", service, protocolLines);
            
            // Викликаємо подію втрати зв'язку GPS
            OnGpsSignalLost?.Invoke("Навігаційні супутники не відповідають через внутрішній збій синхронізації.");

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
                // Передамо від'ємну кількість епізодів для виклику AiModuleFailureException
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
        catch (AiModuleFailureException ex)
        {
            LogLine($"\n[УВАГА]: Перехоплено виняток: {ex.Message}", service, protocolLines);
            LogLine("[ШТУЧНИЙ ІНТЕЛЕКТ]: Самонавчання призупинено. Відновлюємо попередню стабільну модель нейромережі.", service, protocolLines);
        }

        LogLine("", service, protocolLines);
        
        // Очищаємо тимчасові посилання
        currentService = null;
        currentProtocol = null;

        return results;
    }

    // Обробники 5 критичних доменних подій
    private void HandleDriverImpairedEvent(string msg)
    {
        LogLine($"\n[ПОДІЯ - БЛОКУВАННЯ]: {msg}", currentService!, currentProtocol!);
        LogLine("[ОБРОБНИК ПОДІЇ]: Системи керма повністю заблоковані. Автомобіль рухається виключно в автономному режимі.", currentService!, currentProtocol!);
    }

    private void HandleCollisionImminentEvent(string msg)
    {
        LogLine($"\n[ПОДІЯ - РИЗИК ЗІТКНЕННЯ]: {msg}", currentService!, currentProtocol!);
        LogLine("[ОБРОБНИК ПОДІЇ]: ЕКСТРЕНЕ ГАЛЬМУВАННЯ! Переднатягувачі ременів затягнуті на 250N. Подушки безпеки приведені в секундну готовність. Координати передано екстреним службам.", currentService!, currentProtocol!);
    }

    private void HandleGpsSignalLostEvent(string msg)
    {
        LogLine($"\n[ПОДІЯ - GPS]: {msg}", currentService!, currentProtocol!);
        LogLine("[ОБРОБНИК ПОДІЇ]: Зв'язок втрачено. Активовано інерціальний блок орієнтації на базі гіроскопів та резервні офлайн-карти.", currentService!, currentProtocol!);
    }

    private void HandleDriverMoodChangedEvent(string msg)
    {
        LogLine($"\n[ПОДІЯ - НАСТРІЙ ВОДІЯ]: {msg}", currentService!, currentProtocol!);
        LogLine("[ОБРОБНИК ПОДІЇ]: Зміна підсвітки салону на релаксуючу бірюзову та запуск аудіоплейлиста \"Антистрес\" для стабілізації стану водія.", currentService!, currentProtocol!);
    }

    private void HandleSuddenHealthDropEvent(string msg)
    {
        LogLine($"\n[ПОДІЯ - ЗДОРОВ'Я ВОДІЯ]: {msg}", currentService!, currentProtocol!);
        LogLine("[ОБРОБНИК ПОДІЇ]: Увага! Знижуємо температуру клімату до 20°C, інтенсивність вентиляції збільшена до 50 м³/год, здійснюється безпечне автоматичне паркування на узбіччі та виклик швидкої допомоги.", currentService!, currentProtocol!);
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
