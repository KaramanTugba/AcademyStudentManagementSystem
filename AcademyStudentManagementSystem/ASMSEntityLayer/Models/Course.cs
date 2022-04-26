using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASMSEntityLayer.Models
{
    [Table("Courses")]
   public class Course:Base<int>
    {
        [Required]
        [StringLength(50,MinimumLength =2, ErrorMessage ="Kurs adı 2 ile 50 arasında olmalıdır.")]
        public string CourseName { get; set; }

        
      
    }
}
