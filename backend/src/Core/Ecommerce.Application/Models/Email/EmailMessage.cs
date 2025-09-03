namespace Ecommerce.Application.Models.Email;

/// <summary>
/// REPRESENTA LA ESTRUCTA QUE SE VA A ENVIAR POR CORREO
/// </summary>
public class EmailMessage
{
    public string? To { get; set; } = string.Empty;
    public string? Subject { get; set; } = string.Empty;
    public string? Body { get; set; } = string.Empty;
}