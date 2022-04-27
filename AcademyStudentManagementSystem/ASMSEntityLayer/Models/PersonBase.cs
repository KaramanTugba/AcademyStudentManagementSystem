using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASMSEntityLayer.Models
{
    public abstract class  PersonBase
    {
        [Key]
        [Column(Order=1)]
        [StringLength(11,MinimumLength =11,ErrorMessage ="T.C. Kimlik numarası 11 karakter olmalıdır.")]
        public string TCNumber { get; set; }
    }
}
