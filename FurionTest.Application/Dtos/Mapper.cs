using FurionTest.Core.Models;

namespace FurionTest.Application.Dtos
{
    public class Mapper : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<Order, ViewOrder>()
                .Map(dest => dest.CustomName, src => src.CustomId + src.Name)
                .Map(dest => dest.Id, src => src.Id.ToString().Replace("1234", "****"));
        }
    }
}