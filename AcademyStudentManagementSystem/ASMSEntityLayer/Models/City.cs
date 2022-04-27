using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASMSEntityLayer.Models
{
    [Table("Cities")]
    [Index(nameof(PlateCode),IsUnique=true)]
    public class City:Base<byte>
    {
        [StringLength(50,MinimumLength =2,ErrorMessage ="İl adı 2 ile 50 karakter arasında olmalıdır.")]
        public string CityName { get; set; }

        [Required]
        public byte PlateCode { get; set; }

        //İlişkiler kullanılacak
        public virtual ICollection<District> Districts { get; set; }
        //Yukarıdakini kullanırsak, .ToList() yapmamız gerekir.
        //Aşağıdakinde ise buna gerek kalmaz.
        //public virtual List<District> Districts{get;set;}
    }
}
