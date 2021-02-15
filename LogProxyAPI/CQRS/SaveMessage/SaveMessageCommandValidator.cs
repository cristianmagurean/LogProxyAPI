using FluentValidation;

namespace LogProxyAPI.CQRS
{
    public class SaveMessageCommandValidator : AbstractValidator<SaveMessageCommand>
    {
        public SaveMessageCommandValidator()
        {
            RuleFor(r => r.Title).NotEmpty();
            RuleFor(r => r.Text).NotEmpty();
        }
    }
}