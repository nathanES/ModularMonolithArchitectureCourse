using Ardalis.Result;
using MediatR;
using RiverBooks.EmailSending.EmailBackgroundService;
using RiverBooks.EmailsSending.Contracts;

namespace RiverBooks.EmailSending.Integrations;

internal class SendEmailCommandHandler //:  IRequestHandler<SendEmailCommand, Result<Guid>>
{
    private readonly ISendEmail _emailSender;

    public SendEmailCommandHandler(ISendEmail emailSender)
    {
        _emailSender = emailSender;
    }
    
    
    public async Task<Result<Guid>> Handle(SendEmailCommand request, 
        CancellationToken ct)
    {
        await _emailSender.SendEmailAsync(request.To,
            request.From,
            request.Subject,
            request.Body);
    
        return Guid.Empty;
    }
}