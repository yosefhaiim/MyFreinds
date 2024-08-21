using System.ComponentModel.DataAnnotations;
namespace MyFreinds.Models
{
    public class Image
    {
       
        [Key]
        public int Id { get; set; }

        public Friend? friend { get; set; }

        [Display(Name ="תמונה")]

        public byte[]? bytes { get; set; }
    }
}
