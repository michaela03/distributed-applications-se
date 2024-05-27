using AutoMapper;
using HotelManagmentAPI.Dto;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace HotelManagmentAPI.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Room, RoomDto>();
            CreateMap<Client, ClientDto>();
            CreateMap<Reservation, ReservationDto>();
            CreateMap<Review, ReviewDto>();
            CreateMap<RoomDto, Room>();
            CreateMap<ClientDto, Client>();
            CreateMap<ReservationDto, Reservation>();
            CreateMap<ReviewDto, Review>();
          
        }
    }
}
