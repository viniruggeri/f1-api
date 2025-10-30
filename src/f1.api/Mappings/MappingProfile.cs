using AutoMapper;
using F1.Api.Application.DTOs;
using F1.Api.Domain.Entities;
using F1.Api.DTOs;

namespace F1.Api.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Equipe, EquipeDTO>();
        CreateMap<CreateEquipeDTO, Equipe>()
            .ConstructUsing(dto => new Equipe(dto.Nome, dto.Pais, dto.AnoFundacao));
        CreateMap<UpdateEquipeDTO, Equipe>()
            .ForMember(dest => dest.EquipeId, opt => opt.Ignore())
            .ForMember(dest => dest.Pilotos, opt => opt.Ignore());

        CreateMap<Piloto, PilotoDTO>()
            .ForMember(dest => dest.EquipeNome, opt => opt.MapFrom(src => src.Equipe != null ? src.Equipe.Nome : null))
            .ForMember(dest => dest.Idade, opt => opt.MapFrom(src => src.CalcularIdade()));
        
        CreateMap<CreatePilotoDTO, Piloto>()
            .ConstructUsing(dto => new Piloto(dto.Nome, dto.Nacionalidade, dto.EquipeId, dto.DataNascimento));
        
        CreateMap<UpdatePilotoDTO, Piloto>()
            .ForMember(dest => dest.PilotoId, opt => opt.Ignore())
            .ForMember(dest => dest.Equipe, opt => opt.Ignore())
            .ForMember(dest => dest.Resultados, opt => opt.Ignore());

        CreateMap<Corrida, CorridaDTO>();
        CreateMap<CorridaDTO, Corrida>();
    }
}

