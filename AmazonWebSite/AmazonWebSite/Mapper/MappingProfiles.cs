using AmazonWebSiteDAL.Entities;
using AmazonWebSitePL.Models;
using AutoMapper;



namespace AmazonWebSitePL.Mapper
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<Image,ImageViewModel>().ReverseMap();
            CreateMap<Items,ItemViewModel>().ReverseMap();
            CreateMap<Order, OrdersViewModel>().ReverseMap();
        }
    }
}
