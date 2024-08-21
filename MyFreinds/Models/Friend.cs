using MyFreinds.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Net.Mime.MediaTypeNames;
namespace MyFreinds.Models
{
    

    public class Friend
    {
        public Friend()
        {
            Images = new List<Image>();
        }

        [Key]
        public int ID { get; set; }

        [Display(Name = "שם פרטי")]
        public string FirstName { get; set; } = "";

        [Display(Name = "שם משפחה")]
        public string LastName { get; set; } = "";

        [Display(Name = "מספר טלפון"), Phone]

        public string PhonNumber { get; set; } = "";

        [Display(Name = "שם מלא"), NotMapped]
        public string FullName { get { return FirstName + " " + LastName; } }

        [Display(Name = "כתובת אימייל"), EmailAddress(ErrorMessage = " כתובת אימייל לא תקינה")]
        public string EmailAddress { get; set; }

        public List <Image> Images { get; set; }






        [Display (Name= "הוספת תמונה"), NotMapped]
        public IFormFile SetImage
        {  get { return null; }
            set 
            {
                if(value == null)
                {
                    return;
                }
                // יצירת מקום בזכרון לקובץ
                MemoryStream stream = new MemoryStream();            
                value.CopyTo(stream);

                // ממרים את המקום שיצרנו בזיכרון לבייטים
                byte[] strreamArry = stream.ToArray();

                AddImage(strreamArry);

            }
        }

        public void AddImage(byte[] image)
        {
            Image img = new Image();
            img.bytes = image;
            img.friend = this;


			// דרך נוספת
			//  יצירת מופע בתוך קונסטרקטור פרטי
			/*Image img = new Image
            {
                bytes = image,
                friend = this
            };*/

			Images.Add(img);
        }

    }
}
