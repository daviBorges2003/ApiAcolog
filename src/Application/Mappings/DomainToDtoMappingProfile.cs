using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcologAPI.src.Domain.Entities;
using Application.DTOs;
using AutoMapper;

namespace AcologAPI.src.Application.Mappings
{
    public class DomainToDtoMappingProfile : Profile
    {
        public DomainToDtoMappingProfile()
        {
            CreateMap<Users, UserDTO>().ReverseMap();
        }
    }
}