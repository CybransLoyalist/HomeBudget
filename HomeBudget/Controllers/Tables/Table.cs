using System.Collections.Generic;

namespace HomeBudget.Controllers.Tables
{
    public class Table
    {
        private readonly TableCell[,] _cells;

        public Table()
        {
            _cells = new TableCell[0,0];
        }

        public Table(TableCell[,] cells)
        {
            _cells = cells;
        }

        public int RowsCount => _cells.GetLength(0);
        public int ColumnsCount => _cells.GetLength(1);

        public IList<TableRow> GetRows()
        {
            var rows = new List<TableRow>();
            for (var i = 0; i < RowsCount; ++i)
            {
                rows.Add(new TableRow(GetRow(i))); 
            }
            return rows;
        }

        public IList<TableColumn> GetColumns()
        {
            var columns = new List<TableColumn>();
            for (var i = 0; i < ColumnsCount; ++i)
            {
                columns.Add(new TableColumn(GetColumn(i)));
            }
            return columns;
        }

        private TableCell[] GetRow(int rowNumber)
        {
            var array = new TableCell[ColumnsCount];
            for (int i = 0; i < ColumnsCount; ++i)
            {
                array[i] = _cells[rowNumber, i];
            }
            return array;
        }

        private TableCell[] GetColumn(int columnNumber)
        {
            var array = new TableCell[RowsCount];
            for (int i = 0; i < RowsCount; ++i)
            {
                array[i] = _cells[i, columnNumber];
            }
            return array;
        }
    }

}