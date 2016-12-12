using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using EFTest.Models;
using EFTest.Quartz;
using Quartz;
using Quartz.Impl;
using ZXing;
using ZXing.QrCode;

namespace EFTest.Controllers
{
    public class HController : Controller
    {
        // GET: H
        public void Index()
        {
            DbContext db = new DbContext();

            Cars c = new Cars() { Name = "宝马" };
            Cars c1 = new Cars() { Name = "保时捷" };
            //Users u = new Users() {Age = 18, UserName = "admin",InterestCarId = null};

            Users u1 = new Users() { Age = 18, UserName = "guest", InterestCarId = 0 };
            db.Carses.Add(c);
            db.Carses.Add(c1);
            // db.Userses.Add(u);
            db.Userses.Add(u1);
            db.SaveChanges();
            //{"无法确定“EFTest.Users_InterestCar”关系的主体端。添加的多个实体可能主键相同。"}

            //模型生成过程中检测到一个或多个验证错误:EFTest.Users_InterestCar: : 多重性与关系“Users_InterestCar”中 Role“Users_InterestCar_Target”中的引用约束冲突。因为 Dependent Role 中的所有属性都不可以为 null，Principal Role 的多重性必须为“1”。

            //模型生成过程中检测到一个或多个验证错误:InterestCarId: Name: 类型中的每个属性名必须唯一。已定义属性名“InterestCarId”。


        }

        public ActionResult Test()
        {
            DbContext db = new DbContext();
            //var x = from u in db.Userses
            //        join c in db.Carses on u.InterestCarId equals c.Id into f
            //        from k in f.DefaultIfEmpty()
            //        select new { name = u.UserName, car = k == null ? "空的" : k.Name };

            //var b = x.ToList();

            var obj = db.Userses.Where(c => c.Age > 10).Select(c => new
            {
                name = c.UserName,
                car = c.InterestCar == null ? "" : c.InterestCar.Name
            }).ToList();

            var obj2 = db.Userses.GroupJoin(db.Carses, u => u.InterestCarId, c => c.Id, (u, c) => new { u, c })
                .SelectMany(p => p.c.DefaultIfEmpty(),
                    (c, d) => new { name = c.u.UserName, car = d == null ? "" : d.Name }).ToList();

            return View(obj2);

        }

        public void TestInsert()
        {
            List<Cars> l = new List<Cars>();
            for (int i = 0; i < 100; i++)
            {
                l.Add(new Cars() { Name = i.ToString() });
            }
            Stopwatch sw = new Stopwatch();
            sw.Start();
            using (var db = new DbContext())
            {
                db.Configuration.AutoDetectChangesEnabled = false;
                // db.Configuration.ValidateOnSaveEnabled = false;
                db.Carses.AddRange(l);
                db.SaveChanges();
                db.Configuration.AutoDetectChangesEnabled = true;
                //db.Configuration.ValidateOnSaveEnabled = true ;

            }
            sw.Stop();
            var x = sw.ElapsedMilliseconds;
        }

        public void Test2()
        {
            DbContext db = new DbContext();
            var u = db.Userses.AsNoTracking().FirstOrDefault(x => x.Id == 4);
            // var c = db.Carses.AsNoTracking().ToList() .Find(b=>b.Id== 16);
            var c = db.Carses.Find(16);
            Cars car = new Cars() { Name = "宝马" };
            u.InterestCar = c;
            db.SaveChanges();

        }

        public void Test3()
        {
            DbContext db = new DbContext();
            var u = db.Userses.FirstOrDefault(x => x.Id == 4);
            // var c = db.Carses.AsNoTracking().ToList() .Find(b=>b.Id== 16);

            DbContext db1 = new DbContext();
            var c = db1.Carses.Find(16);
            Cars car = new Cars() { Name = "宝马" };
            u.InterestCar = c;
            db.SaveChanges();

        }

        public void Test4()
        {

            var user = new Users() { Id = 5, Age = 222, UserName = "test_admin", InterestCarId = 2 };

            using (DbContext db = new DbContext())
            {
                db.Userses.Add(user);
                db.SaveChanges();
            }
        }


        public void QuartzTest()
        {
            StdSchedulerFactory sf = new StdSchedulerFactory();
            var sc = sf.GetScheduler();
            var job = JobBuilder.Create<TestJob>().WithIdentity("testjob", "group1").Build();
            //var trigger = TriggerBuilder.Create().WithIdentity("trigger_test_job", "group1").WithSimpleSchedule(c => c.RepeatForever().WithIntervalInSeconds(20)).StartNow().Build();

            var trigger =
                TriggerBuilder.Create()
                    .WithIdentity("trigger_test_job", "group1")
                    .WithCronSchedule("10,20,30,40,50 16,20 * * * ? ")
                    .Build();
            sc.ScheduleJob(job, trigger);
            sc.Start();
        }


        public ActionResult TestEmail()
        {
            var msg = new MailMessage();
            return View();
        }

        public ActionResult TestEmail2()
        {
            var mailMessage = new MailMessage { Subject = "aaaaaa " };
            return View();
        }


        public void ListTest()
        {


            ThreadPool.QueueUserWorkItem(ce =>
            {
                var l = GetList();
                while (true)
                {
                    var x = l.Where(c => c.Contains("bck")).ToList();
                    Thread.Sleep(20);
                }

            });
            Thread.Sleep(50);

            ThreadPool.QueueUserWorkItem(ce =>
            {
                var l = GetList();
                while (true)
                {
                    l.Add(Guid.NewGuid().ToString("N"));
                    Thread.Sleep(20);
                }
            });
        }
        public void ListTest2()
        {

            var l = GetList();
            var x = l.Where(c => c.Contains("bck")).ToList();
            l.Add("bck");

            var y = l.Where(c => c.Contains("bck")).ToList();

        }

        public List<string> GetList()
        {
            var key = "list_cache";

            var list = HttpRuntime.Cache.Get(key) as List<string>;
            if (list == null)
            {
                var l = new List<string>()
                      {     "admin","root","mv","find","",
                "admin","root","mv","find","",
                "admin",
                "root","mv","find","","admin",
                "root","mv","find",""
                   };

                list = l;
                HttpRuntime.Cache.Insert(key, list, null);
            }

            return list;
        }


        public void Test12()
        {
            int n = 1;
            ++n;
            int y = n++;
        }


        public void T()
        {
            DbContext db = new DbContext();

            //var car = new Cars() { Id = 4 ,Name ="911"};
            var car = db.Carses.FirstOrDefault(x => x.Id == 4);
            var en = db.Entry(car);
            car.Name = "918";
            car.CreateTime = DateTime.Now;
            var en2 = db.Entry(car);

            en.State = EntityState.Unchanged;
            car.Name = "918";
            //  en.Property("Name").IsModified = true;

            db.SaveChanges();

        }


        public void Qrcode()
        {

            string url = "http://twechat.origins.wedochina.cn/p/i/10000083";
          var   options = new QrCodeEncodingOptions
            {
                DisableECI = true,
                CharacterSet = "UTF-8",
                Width = 300,
                Height = 300, Margin = 15
            };
           var  writer = new BarcodeWriter();
            writer.Format = BarcodeFormat.QR_CODE;
            writer.Options = options;
           var img= writer.Write(url);
            img.Save("e:\\1.png");
        }


    }
}