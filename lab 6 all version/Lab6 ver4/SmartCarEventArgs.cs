using System;
using System.Collections.Generic;

namespace Lab6;

/// <summary>
/// Аргументи події для розумного автомобіля, які переносять повідомлення та посилання на сервіси логування.
/// Це гарантує потік-безпечність та усуває використання спільних полів стану класу.
/// </summary>
public sealed class SmartCarEventArgs : EventArgs
{
    private readonly string _message;
    private readonly Service _service;
    private readonly List<string> _protocolLines;

    /// <summary>
    /// Конструктор за замовчуванням.
    /// </summary>
    public SmartCarEventArgs()
    {
        _message = string.Empty;
        _service = new Service();
        _protocolLines = new List<string>();
    }

    /// <summary>
    /// Конструктор з повними параметрами.
    /// </summary>
    public SmartCarEventArgs(string message, Service service, List<string> protocolLines)
    {
        _message = message;
        _service = service;
        _protocolLines = protocolLines;
    }

    /// <summary>
    /// Конструктор копіювання.
    /// </summary>
    public SmartCarEventArgs(SmartCarEventArgs other)
    {
        _message = other.Message;
        _service = other.LoggingService;
        _protocolLines = other.ProtocolLines;
    }

    public string Message => _message;
    public Service LoggingService => _service;
    public List<string> ProtocolLines => _protocolLines;
}
