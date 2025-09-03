using Ecommerce.Application.Contracts.Infrastructure;
using Ecommerce.Application.Models.Email;
using FluentEmail.Core;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Ecommerce.Infrastructure.MessageImplementation;

public class EmailService : IEmailService
{
    private readonly IFluentEmail _fluentEmail;
    private readonly EmailFluentSettings _emailFluentSettings;
    public EmailSettings _emailSettings { get; }

    public ILogger<EmailService> _logger { get; }

    public EmailService(IFluentEmail fluentEmail, IOptions<EmailFluentSettings> emailFluentSettings, IOptions<EmailSettings> emailSettings, ILogger<EmailService> logger)
    {
        _fluentEmail = fluentEmail;
        _emailFluentSettings = emailFluentSettings.Value;
        _emailSettings = emailSettings.Value;
        _logger = logger;
    }

    public async Task<bool> SendEmail(EmailMessage email, string token)
    {

        try
        {
            var client = new SendGridClient(_emailSettings.Key);
            var from = new EmailAddress(_emailSettings.Email);
            var subject = email.Subject;
            var to = new EmailAddress(email.To, email.To);

            var plainTextContent = email.Body;
            var htmlContent = $"{email.Body} {_emailSettings.BaseUrlClient}/password/reset/{token}";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);

            return response.IsSuccessStatusCode;
        }
        catch (Exception)
        {
            _logger.LogError("El email no pudo enviarse, existen errores");
            return false;
        }


    }

    public async Task<bool> SendEmailAsync(EmailMessage email, string token)
    {
        var htmlContent = $"{email.Body} {_emailFluentSettings.BaseUrlClient}/password/reset/{token}";

        var result = await _fluentEmail
        .To(email.To)
        .Subject(email.Subject)
        .Body(htmlContent)
        .SendAsync();

        return result.Successful;

    }
}