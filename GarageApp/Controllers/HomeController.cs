using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GarageApp.Models;
using System.Collections;
using System.Threading;

namespace GarageApp.Controllers
{
    public class HomeController : Controller
    {
        List<CarModel> carList = new List<CarModel>();

        //Fyller på carList med 10 bilar. Om det finns IN/UT tidsstämplar så läggs bara sådana in i listan. En 20 ms delar för att randomfunktionen buggade.
        private List<CarModel> CarList (DateTime? IN, DateTime? UT) 
        {
            int i = 0;
            while (i < 10) {
                CarModel car = RandomCar();
            if (car.In > IN && car.Out < UT)
            {
                carList.Add(RandomCar());
            }
                i++;
                Thread.Sleep(20);
            }
                  

            return (carList);
        }

        public ActionResult Index()
        {
            DateTime startDate = new DateTime(2018, 2, 16, 7, 0, 0);
            return View(CarList(startDate, DateTime.Now));
        }
               

        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            DateTime timeIn = DateTime.Parse(collection["IN"]);
            DateTime timeOut = DateTime.Parse(collection["OUT"]);
            ViewBag.IN = timeIn;
            ViewBag.OUT = collection["OUT"];
            carList = CarList(timeIn, timeOut);
            int totalPrice = 0;
            foreach(CarModel c in carList)
            {
                totalPrice = totalPrice + c.Price;

            }

            ViewBag.totalPrice = totalPrice;

            return View(carList);
          
        }

        private CarModel RandomCar()
        {            
            CarModel car = new CarModel();
            car.Reg = RandomReg();
            car.In = RandomTime();            
            Thread.Sleep(20);
            car.Out = RandomTime();
            
            TimeSpan T = car.Out - car.In;

            if (T.Minutes < 0)
            {
                return RandomCar();
            }
                      
            car.timeSpan = T;
            car.Price = T.Hours * 12;
            car.Paid = true;                   

            return (car);
        }
        private Random gen = new Random();
        DateTime RandomTime()
        {
            DateTime startDate = new DateTime(2018, 2, 16, 7, 0, 0);
            DateTime endDate = new DateTime(2018, 2, 17, 0, 0, 0);
            TimeSpan timeSpan = endDate - startDate;
            var randomTest = new Random();
            TimeSpan newSpan = new TimeSpan(0, randomTest.Next(0, (int)timeSpan.TotalMinutes), 0);
            DateTime newDate = startDate + newSpan;
            return newDate;
        }
        private string RandomReg()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var stringChars = new char[3];
            var random = new Random();
            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
                stringChars[i] = chars[random.Next(chars.Length)];
            }
            var letters = new String(stringChars);
            int numbers = random.Next(0, 999);
            string reg = letters + numbers.ToString();
            return reg;
        }

      
    }
}