using System;
using System.Collections.Generic;

namespace Lab6;

/// <summary>
/// Координатор симуляції, який завантажує сценарії, керує their виконанням та зберігає результати.
/// </summary>
public class SimulationCoordinator
{
    private string _coordinatorName;

    /// <summary>
    /// Конструктор за замовчуванням.
    /// </summary>
    public SimulationCoordinator()
    {
        _coordinatorName = "Головний координатор симуляції";
    }

    /// <summary>
    /// Конструктор з параметрами.
    /// </summary>
    public SimulationCoordinator(string coordinatorName)
    {
        _coordinatorName = coordinatorName;
    }

    /// <summary>
    /// Конструктор копіювання.
    /// </summary>
    public SimulationCoordinator(SimulationCoordinator other)
    {
        _coordinatorName = other.CoordinatorName;
    }

    public string CoordinatorName
    {
        get => _coordinatorName;
        set => _coordinatorName = value;
    }

    /// <summary>
    /// Виконує повний цикл завантаження сценаріїв, запуску автономних автомобілів та збереження результатів.
    /// </summary>
    public void RunSimulation()
    {
        Service service = new Service();
        SmartCarFactory factory = new SmartCarFactory();
        List<ScenarioResult> allResults = new List<ScenarioResult>();
        List<string> protocolLines = new List<string>();

        try
        {
            IReadOnlyList<ScenarioData> scenarios = ScenarioLoader.LoadScenarios("scenarios.json", service);

            for (int index = 0; index < scenarios.Count; index++)
            {
                ScenarioData scenario = scenarios[index];
                SmartCar smartCar = factory.CreateSmartCar(scenario);

                IReadOnlyList<ScenarioResult> results = smartCar.RunAutonomousCycle(scenario, service, protocolLines);
                allResults.AddRange(results);

                if (index < scenarios.Count - 1)
                {
                    service.WriteConsole("\n[Симуляція]: Натисніть [Enter], щоб перейти до наступного сценарію...");
                    service.ReadConsole();
                }
            }

            SaveSimulationData(service, allResults, protocolLines);
        }
        catch (Exception ex)
        {
            service.WriteConsole($"\nКРИТИЧНА ПОМИЛКА БЕЗПЕКИ: {ex.Message}");
        }
    }

    private void SaveSimulationData(Service service, IReadOnlyList<ScenarioResult> results, IReadOnlyList<string> protocolLines)
    {
        List<string> calculatedLines = new List<string>();

        foreach (ScenarioResult result in results)
        {
            calculatedLines.Add($"{result.Name}: {result.Value:F1}");
        }

        string calculatedContent = string.Join(Environment.NewLine, calculatedLines);
        string protocolContent = string.Join(Environment.NewLine, protocolLines);

        service.WriteFile("Output/calculated-values.txt", calculatedContent);
        service.WriteFile("Output/program-protocol.txt", protocolContent);
    }
}
