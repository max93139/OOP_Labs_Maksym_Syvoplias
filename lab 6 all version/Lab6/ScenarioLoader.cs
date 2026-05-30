using System.Text.Json;

namespace Lab6;

/// <summary>
/// Забезпечує зчитування та десеріалізацію конфігурації датчиків розумного автомобіля через сервіс введення-виведення.
/// </summary>
public static class ScenarioLoader
{
    /// <summary>
    /// Завантажує список сценаріїв з JSON-файлу за допомогою сервісу введення-виведення.
    /// </summary>
    public static IReadOnlyList<ScenarioData> LoadScenarios(string filePath, Service service)
    {
        string jsonContent = service.ReadFile(filePath);
        JsonSerializerOptions options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        List<ScenarioData>? scenarios = JsonSerializer.Deserialize<List<ScenarioData>>(jsonContent, options);

        if (scenarios != null)
        {
            return scenarios;
        }
        else
        {
            throw new InvalidOperationException("Помилка: Не вдалося десеріалізувати файл scenarios.json (отримано порожнє значення).");
        }
    }
}
