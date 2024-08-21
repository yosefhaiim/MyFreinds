using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyFreinds.DAL;
using MyFreinds.Models;
using System.Diagnostics;

namespace MyFreinds.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }



        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }




        public IActionResult Friends()
        {
            List<Friend> friends = data.get.Friends.ToList();
            return View(friends);
        }


        // פונקציה שמעבירה לחלון רישום (create)
        public IActionResult NewFriend()
        {
            // החזרת חלון לרישום השם
            return View(new Friend ());
        }


        // מספר שהוא צריך לקבל משהו מסוג פוסט ולא גט כמו שאר הבקשות הדיפולטיביות
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult AddFriend(Friend friend)
        {
            // שמירת חבר נוסף
            data.get.Friends.Add(friend);
            data.get.SaveChanges();
            return RedirectToAction("Friends");
        }

       




        public IActionResult Details(int? id)
        {
            if(id == null)
            {
                return RedirectToAction("Friends");
            }

            Friend? friend = data.get.Friends. Include(f => f.Images).ToList().FirstOrDefault(friend => friend.ID == id);

            if (friend == null)
            {
                return RedirectToAction("Friends");
            }

            return View(friend);
        }






        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult EditFriendSaved(Friend newFriend)
        {
            if (newFriend == null)
            {
                return RedirectToAction("Friends");

            }
            // יצירת משתנה חדש ובדיקה על התעודת זהות שלו על מנת להציגה בחלון
            Friend? existingFriend = data.get.Friends.FirstOrDefault(f=> f.ID == newFriend.ID);
           
            // בדיקה האם הוא קיים
            if (existingFriend == null)
            {
                return RedirectToAction("Friends");

            }

           
            // עדכון הדאטה בייס בחבר שקיבלנו
            data.get.Entry(existingFriend).CurrentValues.SetValues(newFriend);
            // שמירת חבר נוסף
            data.get.SaveChanges();
            // החזרת החלון עם החבר מחדש
            return RedirectToAction("Friends");
        }


        
        // פונקציית עריכה
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Friends");
            }

            // הכרזת משתנה
            Friend? friend = data.get.Friends.FirstOrDefault(friend => friend.ID == id);
            // בדיקה האם הוא קיים
            if (friend == null)
            {
                // אני מפנה אותו לפעולה
                return RedirectToAction("Friends");
            }
            // מחזיר את חלון פריינד
            return View(friend);
        }

        // מחיקה
        public IActionResult Delete(int? id)
        {
            // בדיקה שהתעודת זהות אינה ריקה
            if (id == null)
            {
                return NotFound();
            }

            // אני בונה רשימת חברים
            List<Friend> friendList = data.get.Friends.ToList();

            // אני אומר לו למצוא את החבר שהתעודת זהות שלו מתאימה לזו שהוכנסה
            Friend? friendToRemove = friendList.Find(friend => friend.ID == id);
            
            // בדיקה שהוא לא ריק
            if (friendToRemove == null)
            {
                return NotFound();
            }

            // מחיקת החבר
            data.get.Friends.Remove(friendToRemove);
            // שמירה
            data.get.SaveChanges();
            // החזרת עמוד הרשימה
            return RedirectToAction(nameof(Friends)); // "Friends" הוא לוקח אותי ל

        }







        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult AddNewImage(Friend friend)
        {
            Friend? friendFromDb = data.get.Friends.FirstOrDefault(f => f.ID == friend.ID);

            if (friendFromDb == null) 
            {
                return NotFound();
            }

            byte[]? firstImage = friendFromDb.Images.First().bytes;

            if (firstImage == null)
            {
                return NotFound();
            }
            friendFromDb.AddImage(firstImage);
            data.get.SaveChanges();
            return RedirectToAction("Details", new { ID = friendFromDb.ID });



        }







        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
