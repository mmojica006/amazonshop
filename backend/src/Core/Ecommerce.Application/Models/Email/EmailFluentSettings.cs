namespace Ecommerce.Application.Models.Email;

/// <summary>
/// SE va a hacer match con la seccion de appsetting
/// </summary>
public class EmailFluentSettings
{
    public string? Email { get; set; } = string.Empty;
    public string? Host { get; set; } = string.Empty;
    public int Port { get; set; }
    public string? BaseUrlClient { get; set; }
}