namespace Lab6;

/// <summary>
/// Надає базові послуги введення-виведення для роботи з консоллю та текстовими файлами.
/// </summary>
public sealed class Service
{
    /// <summary>
    /// Записує рядок тексту в консоль.
    /// </summary>
    public void WriteConsole(string line)
    {
        Console.WriteLine(line);
    }

    /// <summary>
    /// Зчитує рядок тексту з консолі.
    /// </summary>
    public string ReadConsole()
    {
        return Console.ReadLine() ?? string.Empty;
    }

    /// <summary>
    /// Зчитує весь вміст текстового файлу.
    /// </summary>
    public string ReadFile(string filePath)
    {
        string content;
        if (File.Exists(filePath))
        {
            content = File.ReadAllText(filePath);
        }
        else
        {
            throw new FileNotFoundException($"Помилка: Файл за шляхом '{filePath}' не знайдено.");
        }

        return content;
    }

    /// <summary>
    /// Записує вміст текстового файлу. Створює директорію, якщо вона не існує.
    /// </summary>
    public void WriteFile(string filePath, string content)
    {
        string? directoryPath = Path.GetDirectoryName(filePath);
        if (directoryPath != null)
        {
            Directory.CreateDirectory(directoryPath);
        }
        else
        {
            // Корінь або порожній шлях, директорію створювати не потрібно
        }

        File.WriteAllText(filePath, content);
    }
}
