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


        // ������� ������� ����� ����� (create)
        public IActionResult NewFriend()
        {
            // ����� ���� ������ ���
            return View(new Friend ());
        }


        // ���� ���� ���� ���� ���� ���� ���� ��� �� ��� ��� ������ ������������
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult AddFriend(Friend friend)
        {
            // ����� ��� ����
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
            // ����� ����� ��� ������ �� ������ ���� ��� �� ��� ������ �����
            Friend? existingFriend = data.get.Friends.FirstOrDefault(f=> f.ID == newFriend.ID);
           
            // ����� ��� ��� ����
            if (existingFriend == null)
            {
                return RedirectToAction("Friends");

            }

           
            // ����� ����� ���� ���� �������
            data.get.Entry(existingFriend).CurrentValues.SetValues(newFriend);
            // ����� ��� ����
            data.get.SaveChanges();
            // ����� ����� �� ���� ����
            return RedirectToAction("Friends");
        }


        
        // �������� �����
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Friends");
            }

            // ����� �����
            Friend? friend = data.get.Friends.FirstOrDefault(friend => friend.ID == id);
            // ����� ��� ��� ����
            if (friend == null)
            {
                // ��� ���� ���� ������
                return RedirectToAction("Friends");
            }
            // ����� �� ���� ������
            return View(friend);
        }

        // �����
        public IActionResult Delete(int? id)
        {
            // ����� ������� ���� ���� ����
            if (id == null)
            {
                return NotFound();
            }

            // ��� ���� ����� �����
            List<Friend> friendList = data.get.Friends.ToList();

            // ��� ���� �� ����� �� ���� ������� ���� ��� ������ ��� �������
            Friend? friendToRemove = friendList.Find(friend => friend.ID == id);
            
            // ����� ���� �� ���
            if (friendToRemove == null)
            {
                return NotFound();
            }

            // ����� ����
            data.get.Friends.Remove(friendToRemove);
            // �����
            data.get.SaveChanges();
            // ����� ���� ������
            return RedirectToAction(nameof(Friends)); // "Friends" ��� ���� ���� �

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
