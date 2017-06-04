using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using HomeBudget.Controllers.Tables;
using HomeBudget.Extensions;
using HomeBudget.Helpers;

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
            var row1 = new List<string> { "a1", "a2", "a3", "a4", "a5" };
            var row2 = new List<string> { "b1", "b2", "b3", "b4", "b5" };
            var row3 = new List<string> { "c1", "c2", "c3", "c4", "c5" };

            var data = new List<List<String>>()
            {
                row1,
                row2,
                row3
            };
            var data2 = new List<List<TableCell>>();
            for (int i = 0; i < data.Count; ++i)
            {
                var list = new List<TableCell>();
                for (int j = 0; j < data[0].Count; ++j)
                {
                    list.Add(new TableCell { Value = data[i][j] });
                }
                data2.Add(list);
            }
            //BackgroundColor = (j % 4 == 0 || j % 4 == 1) ? "var(--color2)" : "var(--color3)"
            var table = new Table(data2.To2DArray());
            var columns = table.GetColumns();
            for (int i = 0; i < columns.Count; i++)
            {
                var column = columns[i];
                foreach (var tableCell in column.GetCells())
                {
                    tableCell.BackgroundColor = (i % 4 == 0 || i % 4 == 1) ? "var(--color2)" : "var(--color3)";
                }
            }
            foreach (var cell in columns.Last().GetCells())
            {
                cell.BackgroundColor = "var(--color4)";
            }

            var tableRows = table.GetRows();
            foreach (var cell in tableRows[1].GetCells())
            {
                cell.BackgroundColor = "var(--color5)";
            }

            return View(table);
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
        public ActionResult OnSubmit(string[] cells, int rowsCount, int columnsCount)
        {
            var cells2 = FlatToMultidimensionalArray.Fold(cells.Select(a => new TableCell { Value = a }).ToArray(), rowsCount, columnsCount);

            var table = new Table(cells2);
            throw new NotImplementedException();
        }
    }
}