using ASMSEntityLayer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASMSEntityLayer.ViewModels
{
    public class CityVM
    {
        public byte Id { get; set; }
        DateTime CreatedDate { get; set; }
        bool IsDeleted { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "İl adı 2 ile 50 karakter arasında olmalıdır.")]
        public string CityName { get; set; }

        [Required]
        public byte PlateCode { get; set; }

        public  ICollection<DistrictVM> Districts { get; set; }

    }
}
