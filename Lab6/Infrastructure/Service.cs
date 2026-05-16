using Lab6.Domain;

namespace Lab6.Infrastructure;

/// <summary>
/// Забезпечує технічне виведення лабораторних розрахунків у консоль і файли.
/// </summary>
public sealed class Service
{
    private readonly string outputDirectory;
    private readonly List<string> protocolLines;

    /// <summary>
    /// Ініціалізує новий сервіс виведення.
    /// </summary>
    public Service(string outputDirectory)
    {
        this.outputDirectory = outputDirectory;
        protocolLines = new List<string>();
    }

    /// <summary>
    /// Записує рядок протоколу у внутрішній буфер і консоль.
    /// </summary>
    public void WriteProtocolLine(string line)
    {
        protocolLines.Add(line);
        Console.WriteLine(line);
    }

    /// <summary>
    /// Записує кілька рядків протоколу.
    /// </summary>
    public void WriteProtocolLines(IEnumerable<string> lines)
    {
        foreach (string line in lines)
        {
            WriteProtocolLine(line);
        }
    }

    /// <summary>
    /// Зберігає розраховані значення сценарію в текстовий файл.
    /// </summary>
    public void SaveCalculatedValues(IReadOnlyList<ScenarioResult> results)
    {
        Directory.CreateDirectory(outputDirectory);

        List<string> lines = new List<string>();

        foreach (ScenarioResult result in results)
        {
            lines.Add($"{result.Name}: {result.Value:F1}");
        }

        string filePath = Path.Combine(outputDirectory, "calculated-values.txt");
        File.WriteAllLines(filePath, lines);
    }

    /// <summary>
    /// Зберігає повний протокол роботи програми в текстовий файл.
    /// </summary>
    public void SaveProtocol()
    {
        Directory.CreateDirectory(outputDirectory);

        string filePath = Path.Combine(outputDirectory, "program-protocol.txt");
        File.WriteAllLines(filePath, protocolLines);
    }
}
