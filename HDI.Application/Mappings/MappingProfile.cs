using AutoMapper;
using HDI.Application.DTOs.Agreement;
using HDI.Application.DTOs.WorkItem;
using HDI.Application.DTOs.Partner;    // Eksik
using HDI.Application.DTOs.Keyword;    // Eksik
using HDI.Domain.Entities;

namespace HDI.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Agreement, AgreementDto>().ReverseMap();
        CreateMap<CreateAgreementRequest, Agreement>();
        CreateMap<UpdateAgreementRequest, Agreement>();

        CreateMap<WorkItem, WorkItemDto>()
            .ForMember(dest => dest.AgreementTitle, opt => opt.MapFrom(src => src.Agreement.Title));

        CreateMap<Partner, PartnerDto>().ReverseMap();
        CreateMap<CreatePartnerRequest, Partner>();

        CreateMap<Keyword, KeywordDto>().ReverseMap();
        CreateMap<CreateKeywordRequest, Keyword>();
    }
}