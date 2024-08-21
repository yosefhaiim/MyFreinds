namespace MyFreinds.DAL
{
    public class data
    {

      
       string ConectionString = ""
            + "server=DESKTOP-Q2PJJ02\\SQLEXPRESS ;"
            + "Initial Catalog = my_friends ;"
            + "user id=sa ;"
            + "password=1234 ;"
            + "TrustServerCertificate=Yes";

        public data()
        {
            // יצירת מופע של שכבת נתונים עם מחרוזת חיבור
            Layer = new DataLayer(ConectionString);
        }
        
        // משתנה סטטי ליצירת מופע יחיד של המחלקה
        static data GetData;


        // מאפיין סטטי לקבלת שכבת נתונים
        public static DataLayer get
        {
            get
            {
                // יצירת מופע אם לא קיים
                if (GetData == null)
                {
                    GetData = new data();
                }

                // החזרת שכבת הנתונים
                return GetData.Layer;
            }
            
        }

        // מאפיין לשמירת שכבת הנתונים
        public DataLayer Layer { get; set; }


    }
}
