using AutoMapper;
using CSTrackWebAPI.Model.Entities;
using System;

namespace CSTrackWebAPI.Model.DTO.View
{
    public class PlayerSmallDetailsDTO : IProfileBase
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public IProfileExpression Configure(IProfileExpression config)
        {
            config.CreateMap<Player, PlayerSmallDetailsDTO>()
                .ForMember(dto => dto.Id, dto => dto.MapFrom(d => d.Id))
               .ForMember(dto => dto.Name, dto => dto.MapFrom(d => d.Name));

            return config;
        }
    }
}
