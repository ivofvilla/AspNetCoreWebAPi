using AutoMapper;
using SmartSchool.Api.Helpers;
using SmartSchool.Api.V2.Dtos;
using SmartSchool.API.Models;

namespace SmartSchool.Api.V2.Helpers
{
    public class SmartSchoolProfile : Profile
    {
        public SmartSchoolProfile()
        {
            CreateMap<Aluno, AlunoDto>()
                .ForMember(
                    dest => dest.Nome,
                    opt => opt.MapFrom(src => $"{src.Nome} {src.Sobrenome}")
                )
                .ForMember(
                    dest => dest.Idade,
                    opt => opt.MapFrom(src => src.DataNascimento.GetIdadeAtual())
                );

            CreateMap<AlunoDto, Aluno>();
            CreateMap<Aluno, AlunoRegistroDto>().ReverseMap();

            CreateMap<Professor, ProfessorDto>()
                .ForMember(
                    dest => dest.Nome,
                    opt => opt.MapFrom(src => $"{src.Nome} {src.Sobrenome}")
                );

            CreateMap<ProfessorDto, Professor>();
            CreateMap<Professor, ProfessorRegistroDto>().ReverseMap();
        }
    }
}
