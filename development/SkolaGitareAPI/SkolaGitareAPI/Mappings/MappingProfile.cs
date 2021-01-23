using AutoMapper;
using SkolaGitareAPI.Data.DTOs;
using SkolaGitareAPI.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkolaGitareAPI.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Person, PersonDTO>();
            CreateMap<Appointment, AppointmentDTO>();
            CreateMap<AppointmentRequest, AppointmentRequestDTO>();
            CreateMap<Membership, MembershipDTO>();
            CreateMap<MembershipType, MembershipTypeDTO>();
            CreateMap<Transaction, TransactionDTO>().ForMember(x => x.Membership, opt => opt.MapFrom(x => x.Membership));
        }
    }
}
