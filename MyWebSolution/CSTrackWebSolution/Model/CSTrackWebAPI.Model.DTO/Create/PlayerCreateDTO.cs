using AutoMapper;
using CSTrackWebAPI.Model.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CSTrackWebAPI.Model.DTO.View
{
    public class PlayerCreateDTO : IProfileBase, IValidatableObject
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public int Age { get; set; }

        public IProfileExpression Configure(IProfileExpression config)
        {
            config.CreateMap<PlayerCreateDTO, Player>()
               .ForMember(dto => dto.Name, dto => dto.MapFrom(d => d.Name))
               .ForMember(dto => dto.Age, dto => dto.MapFrom(d => d.Age));

            return config;
        }

        public IEnumerable<ValidationResult> Validate(System.ComponentModel.DataAnnotations.ValidationContext validationContext)
        {
            if (Age <= 0) yield return new ValidationResult("Age should be greater than zero");
        }
    }
}