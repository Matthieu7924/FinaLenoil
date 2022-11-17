using _06_Entity.ViewModel;

namespace _06_Entity.Services
{
    public interface IEmailService
    {
        Task SenEmailAsync(EmailViewModel emailViewModel);
    }
}
