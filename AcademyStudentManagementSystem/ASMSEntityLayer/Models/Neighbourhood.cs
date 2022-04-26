using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASMSEntityLayer.Models
{
    [Table("Neighbourhood")]
    public class Neighbourhood
    {
        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Mahalle adı 2 ile 50 karakter arasında olmalıdır.")]    
        public string NeighbourhoodName { get; set; }

        //ilişki ilçeyle

        public int DistrictId { get; set; }
        [ForeignKey("DistrictId")]
        public virtual District District { get; set; }

        //ilişki 
        public virtual ICollection<UsersAddress> UsersAddresses { get; set; }

    }
}
