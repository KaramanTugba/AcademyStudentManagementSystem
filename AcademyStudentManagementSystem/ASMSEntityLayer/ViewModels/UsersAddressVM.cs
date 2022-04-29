using ASMSEntityLayer.IdentityModels;
using ASMSEntityLayer.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace ASMSEntityLayer.ViewModels
{
    public class UsersAddressVM
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public string UserId { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Adres başlığı 2 ile 50 karakter aralığında olmalıdır.")]
        public string AddresTitle { get; set; }

        [Required(ErrorMessage = "Mahalle seçimi gereklidir.")]
        public int NeighbourhoodId { get; set; }

        [StringLength(500, ErrorMessage = "Adres detayı en çok 500 karakter olmalıdır.")]
        public string AddressDetails { get; set; }

        [StringLength(5, MinimumLength = 5, ErrorMessage = "Posta kodu 5 karakter olmalıdır.")]
        public string PostCode { get; set; }
        public AppUser AppUser { get; set; }
        public NeighbourhoodVM Neighbourhood { get; set; }
        //includeEntities
        //?? İl ve ilçeye ulaşılabilirmi? 
        public CityVM City { get; set; }
        public DistrictVM District { get; set; }
    }
}