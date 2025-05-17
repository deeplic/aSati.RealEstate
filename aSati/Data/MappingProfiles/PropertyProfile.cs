// MappingProfiles/PropertyProfile.cs
using AutoMapper;
using aSati.Shared.Models;

public class PropertyProfile : Profile
{
    public PropertyProfile()
    {
        CreateMap<MainProperty, MainPropertyDto>();
        CreateMap<PropertyUnit, UnitDto>();
        CreateMap<Lease, LeaseDto>();
    }
}
