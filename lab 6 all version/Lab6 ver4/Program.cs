using System;
using System.Collections.Generic;

namespace Lab6;

/// <summary>
/// Точка входу в лабораторну роботу симуляції розумного автомобіля.
/// </summary>
public static class Program
{
    /// <summary>
    /// Головний метод, який ініціалізує та запускає координатор симуляції.
    /// </summary>
    public static void Main()
    {
        RunSimulation();
    }

    /// <summary>
    /// Виконує повний цикл завантаження сценаріїв, запуску автономних автомобілів та збереження результатів.
    /// </summary>
    private static void RunSimulation()
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
                else
                {
                    // Останній сценарій виконано, додаткова пауза не потрібна.
                }
            }

            SaveSimulationData(service, allResults, protocolLines);
        }
        catch (Exception ex)
        {
            service.WriteConsole($"\nКРИТИЧНА ПОМИЛКА БЕЗПЕКИ: {ex.Message}");
        }
    }

    /// <summary>
    /// Форматує та зберігає результати обчислень і протокол роботи програми у текстові файли.
    /// </summary>
    private static void SaveSimulationData(Service service, IReadOnlyList<ScenarioResult> results, IReadOnlyList<string> protocolLines)
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
