using Microsoft.EntityFrameworkCore;
using MyFreinds.Models;

namespace MyFreinds.DAL;
//  קלאס שמייצג את שכבת הנתונים יורש מקלאס נוסף, שזה דיבי קונטקסט
public class DataLayer: DbContext // הנקודותיים זה הורשה
{

    // קונסטרקטור שמקבל מחרוזת חיבור ומעביר אותה לקונסטרקטור האב
   public DataLayer(string connectionString) : base(GetOptions(connectionString))
   {
       Database.EnsureCreated();
        // להכניס נתונים בפעם הראשונה
        seed();
   } 

    private void seed()
    {
        // בדיקה האם הדאטה בייס מלא כי אם יש בו משהו אני לא רוצה שבכל הפעלה יוכנס השם מני
        if(Friends.Count() > 0)
        {
            return;
        }
        // מלאנו פה שם בשביל בדיקה
        Friend firstFriend = new Friend();

        firstFriend.FirstName = "מני";
        firstFriend.LastName = "לוי";
        firstFriend.EmailAddress = "meny@meny.com";
        firstFriend.PhonNumber = "0000000000";
        // הוספת הפירסט פרינד
        Friends.Add(firstFriend);
        // שומר שינויים
        SaveChanges();
    }


    // יצירת הדאטה בייס
    public DbSet<Friend> Friends { get; set; }


    public DbSet<Image> Images { get; set; }




    //  פונקציה שמחזירה את אפשרויות ההתחברות למסד הנתונים
    private static DbContextOptions GetOptions(string connectionString)
    {
        return SqlServerDbContextOptionsExtensions
            .UseSqlServer(new DbContextOptionsBuilder(), connectionString)
            .Options;
       
    }


}
