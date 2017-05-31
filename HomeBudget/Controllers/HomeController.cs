using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HomeBudget.Controllers
{
    public class TableCell
    {
        public string Value { get; set; }
        public string BackgroundColor { get; set; }
    }
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var row1 = new List<string> {"a1", "a2", "a3", "a4", "a5" };
            var row2 = new List<string> {"b1", "b2", "b3", "b4", "b5"};
            var row3 = new List<string> {"c1", "c2", "c3", "c4", "c5"};

            var data = new List<List<String>>()
            {
                row1,
                row2,
                row3
            };

            var data2 = new List<List<TableCell>>();
            for(int i = 0; i < data.Count; ++i)
            {
                var list = new List<TableCell>();
                for (int j = 0; j < data[0].Count; ++j)
                {
                    list.Add(new TableCell {Value = data[i][j], BackgroundColor = (j%4 == 0 || j%4 == 1) ? "var(--color2)" : "var(--color3)"});
                }
                data2.Add(list);
            }

            return View(data2);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult OnSubmit(string[] str)
        {
            throw new NotImplementedException();
        }
    }
}