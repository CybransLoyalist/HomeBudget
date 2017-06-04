using System.Collections.Generic;
using System.Linq;

namespace HomeBudget.Controllers
{
    public class TableSplit
    {
        private readonly List<TableCell> _cells;

        public TableSplit(IEnumerable<TableCell> cells)
        {
            _cells = cells.ToList();
        }

        public List<TableCell> GetCells()
        {
            return _cells;
        }
    }


    public class TableRow : TableSplit //todo consider point of existance of those classes
    {
        public TableRow(IEnumerable<TableCell> cells) : base(cells)
        {
        }
    }

    public class TableColumn : TableSplit
    {
        public TableColumn(IEnumerable<TableCell> cells) : base(cells)
        {
        }
    }
}