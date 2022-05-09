using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ASMSEntityLayer.Models;
using ASMSEntityLayer.ViewModels;

namespace ASMSEntityLayer.Mappings
{
    public class Maps:Profile
    {
        // Buraya Maps ctor gelecek metodu gelecektir.
        // İçine CreateMap metodu gelecek
        public Maps()
        {
            ////UserAddress'ı UserAddressesVM'ye dönüştür
            //CreateMap<UsersAddress, UsersAddressVM>(); //DAL--> BLL
            ////UserAddressVM'yi UserAddresses'e dönüştür
            //CreateMap<UsersAddressVM, UsersAddress>(); //PL--> BLL--> DAL

            //Yukarıdakinin aynısı tek seferde yapmak
            // UserAddress ve VM'yi birbirine dönüştür.
            CreateMap<UsersAddress, UsersAddressVM>().ReverseMap();


        }



    }
}
