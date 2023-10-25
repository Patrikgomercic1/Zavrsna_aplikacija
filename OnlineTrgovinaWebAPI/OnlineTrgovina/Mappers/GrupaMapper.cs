using System;
using AutoMapper;
using EdunovaApp.Models;
using OnlineTrgovina.Models;
using OnlineTrgovina.Models.DTO;

namespace OnlineTrgovina.Mappers
{
    public class InventarMapper
    {

        public static Mapper InitializeAutomapper()
        {
            var config = new MapperConfiguration(cfg =>
            {

                cfg.CreateMap<Inventar, InventarDTO>()
                .ForMember(dest => dest.Proizvod, act => act.MapFrom(src => src.Proizvod!.Naziv))
                .ForMember(dest => dest.SifraProizvod, act => act.MapFrom(src => src.Proizvod.Sifra));

            });
            var mapper = new Mapper(config);
            return mapper;
        }

        public static Mapper InitializeAutomapperKrace()
        {
            return new Mapper(
                new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Inventar, InventarDTO>()
                    .ForMember(dest => dest.Proizvod, act => act.MapFrom(src => src.Proizvod!.Naziv))
                    .ForMember(dest => dest.SifraProizvod, act => act.MapFrom(src => src.Proizvod.Sifra));

                }));
        }
    }
}

