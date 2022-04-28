using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASMSDataAccessLayer.ContractsDAL;
using ASMSEntityLayer.Models;
namespace ASMSDataAccessLayer.ImplementationsDAL
{
   public class CityRepo:RepositoryBase<City,byte>,ICityRepo
    {
        //RepositoryBase katılım almamızın sebebi içindeki CRUD metodlarını kullanabilmek
        //ICityRepo kalıtım almamızın sebebi DI yapısına uygun çalışmak
        public CityRepo(MyContext myContext):base(myContext)
        {
            //ctor oluşturmamızın sebebi kalıtım aldığımız class ın ctor ında myContext istendiği için
        }
    }
}
