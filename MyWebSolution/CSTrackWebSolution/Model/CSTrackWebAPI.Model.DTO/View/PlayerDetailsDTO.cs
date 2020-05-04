using AutoMapper;
using CSTrackWebAPI.Model.Entities;
using System;
using System.Collections.Generic;

namespace CSTrackWebAPI.Model.DTO.View
{
    public class PlayerDetailsDTO : IProfileBase
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        public IProfileExpression Configure(IProfileExpression config)
        {

            config.CreateMap<Player, PlayerDetailsDTO>()
                .ForMember(dto => dto.Id, dto => dto.MapFrom(d => d.Id))
               .ForMember(dto => dto.Name, dto => dto.MapFrom(d => d.Name))
               .ForMember(dto => dto.Age, dto => dto.MapFrom(d => d.Age));

            return config;
        }
    }
}
