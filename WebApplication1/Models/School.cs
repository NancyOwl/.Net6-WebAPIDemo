using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class School
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "必填")]
        public string Name { get; set; }
        
        public string Logo { get; set; }
        
        [Required(ErrorMessage = "必填")]
        [StringLength(100, ErrorMessage = "地址長度不正確(至少八個字)", MinimumLength = 8)]
        public string Address { get; set; }
        
        [Required(ErrorMessage = "必填")]
        [StringLength(20, ErrorMessage = "電話長度不正確(至少八碼)", MinimumLength = 8)]
        public string Tel { get; set; }

        [EmailAddress(ErrorMessage = "Email 格式不正確")]
        public string Email { get; set; }
    }
}
