using Application.Features.Brands.Dtos;
using Application.Features.Brands.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Brands.Commands.CreateBrand;

public class CreateBrandCommand : IRequest<CreatedBrandDto>
{
    public string Name { get; set; }

    public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, CreatedBrandDto>
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;
        private readonly BrandBusinessRules _brandBusinessRules;

        public CreateBrandCommandHandler(IBrandRepository brandRepository, IMapper mapper, BrandBusinessRules brandBusinessRules)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
            _brandBusinessRules = brandBusinessRules;
        }

        public async Task<CreatedBrandDto> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
        {
            await _brandBusinessRules.BrandNameCanNotBeDuplicatedWhenInserted(request.Name); // business tarafında gerekli kontroller yapılarak create işlemi gerçekleştirme aşamasına geçer

            Brand mappedBrand = _mapper.Map<Brand>(request); //Gelen request i brand clasına map işlemi gerçekleştirdi
            Brand createdBrand = await _brandRepository.AddAsync(mappedBrand); // ekleme işlemi yapıldı
            CreatedBrandDto createdBrandDto = _mapper.Map<CreatedBrandDto>(createdBrand); // veritabanına eklenen kaydın tüm bilgilerinden ilgili alanlarının geri dönüşünü sağlayarak dto doldurmasına imkan sağlar
            
            return createdBrandDto;
        }
    }
}
