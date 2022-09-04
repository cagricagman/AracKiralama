using FluentValidation;

namespace Application.Features.Brands.Commands.CreateBrand;

public class CreateBrandCommandValidator : AbstractValidator<CreateBrandCommand>
{
    public CreateBrandCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty().WithMessage("İsim Alanı Boş Geçilemez");
        RuleFor(c => c.Name).MinimumLength(3).WithMessage("Min. 3 Karakter Giriniz");
    }
}