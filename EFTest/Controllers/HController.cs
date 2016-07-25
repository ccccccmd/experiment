using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EFTest.Models;

namespace EFTest.Controllers
{
    public class HController : Controller
    {
        // GET: H
        public void  Index()
        {
            DbContext db = new DbContext();

            Cars c = new Cars() {Name = "宝马"};
            Cars c1 = new Cars() { Name = "保时捷" };
//Users u = new Users() {Age = 18, UserName = "admin",InterestCarId = null};

            Users u1 = new Users() { Age = 18, UserName = "guest",InterestCarId  =0};
            db.Carses.Add(c);
            db.Carses.Add(c1);
           // db.Userses.Add(u);
            db.Userses.Add(u1);
            db.SaveChanges();
            //{"无法确定“EFTest.Users_InterestCar”关系的主体端。添加的多个实体可能主键相同。"}

            //模型生成过程中检测到一个或多个验证错误:EFTest.Users_InterestCar: : 多重性与关系“Users_InterestCar”中 Role“Users_InterestCar_Target”中的引用约束冲突。因为 Dependent Role 中的所有属性都不可以为 null，Principal Role 的多重性必须为“1”。

            //模型生成过程中检测到一个或多个验证错误:InterestCarId: Name: 类型中的每个属性名必须唯一。已定义属性名“InterestCarId”。


        }

        public void Test()
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

            var obj2 = db.Userses.GroupJoin(db.Carses, u => u.InterestCarId, c => c.Id, (u, c) => new {u, c})
                .SelectMany(p => p.c.DefaultIfEmpty(),
                    (c, d) => new {name = c.u.UserName, car = d == null ? "" : d.Name}).ToList();

        }
    }
}