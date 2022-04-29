using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASMSEntityLayer.Models;
using ASMSEntityLayer.ViewModels;

namespace ASMSEntityLayer.Mappings
{
    public class Maps:Profile
    {
        //Buraya Maps ctor gelecektir. İçerisine CreateMap methodu gelecek
        public Maps()
        {
            ////UserAdress i UserAddressVM ye dönüştür.
            //CreateMap<UsersAddress, UserAddressVM>(); //DAL -> BLL
            ////UserAddressVM yi UserAddress e döniştür.
            //CreateMap<UserAddressVM,UsersAddress>(); //PL ->BLL -> DAL


            //Yukarıdakinin aynısını tek seferde yapmak için, UserAddress ile VM yi birbirine dönüştürür.
            CreateMap<UsersAddress,UserAddressVM>().ReverseMap();
        }
    }
}
