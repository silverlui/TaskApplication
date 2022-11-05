using FluentValidation;
using TaskApplication.Data.Models;

namespace TaskApplication.Data.Validation;

public class TodoValidator : AbstractValidator<Todo>
{
    public TodoValidator()
    {
        RuleFor(todo => todo.Id).NotNull();
        RuleFor(todo => todo.Description)
            .NotNull()
            .NotEmpty()
            .Length(1, 50)
            .WithMessage("Description is required");
        RuleFor(todo => todo.UpdatedAt)
            .GreaterThanOrEqualTo(todo => todo.CreatedAt);
    }
}
