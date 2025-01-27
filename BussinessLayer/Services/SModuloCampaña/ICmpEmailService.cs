using BussinessLayer.DTOs.ModuloCampaña.CmpEmail;

public interface ICmpEmailService
{
    Task SendEmailAsync(CmpEmailMessageDto emailMessage);
}