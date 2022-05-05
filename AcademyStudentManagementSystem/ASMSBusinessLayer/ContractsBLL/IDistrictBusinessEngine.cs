using ASMSEntityLayer.ResultModels;
using ASMSEntityLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASMSBusinessLayer.ContractsBLL
{
    public interface IDistrictBusinessEngine
    {
        /// <summary>
        /// This method will return all districts.
        /// Bu methot tüm ilçeleri getirir.
        /// </summary>
        /// <returns></returns>
        //Buraya ekleme silme güncelleme vb metot imzaları yazıllabilir.
        //Biz şuan sadece ihtiyacımız olanları yazacağız.
        IDataResult<ICollection<DistrictVM>> GetAllDistricts();

        /// <summary>
        /// This method will give districts of city
        /// </summary>
        /// <param name="cityId"></param>
        /// <returns></returns>
        IDataResult<ICollection<DistrictVM>> GetDistrictsOfCity(byte cityId);
    }
}
