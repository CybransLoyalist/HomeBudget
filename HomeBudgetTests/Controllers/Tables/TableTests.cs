using System.Collections.Generic;
using System.Linq;
using HomeBudget.Controllers.Tables;
using HomeBudget.Extensions;
using NUnit.Framework;

namespace HomeBudgetTests.Controllers.Tables
{
    [TestFixture]
    public class TableTests
    {
        [Test]
        public void TableWithoutCellsGiven_ShallHaveRowsCountZero()
        {
            var table = new Table();
            Assert.AreEqual(0, table.RowsCount);
        }

        [Test]
        public void TableWithoutCellsGiven_ShallHaveColumnsCountZero()
        {
            var table = new Table();
            Assert.AreEqual(0, table.ColumnsCount);
        }

        [Test]
        public void RowsCountShallBeSetProperly()
        {
            var cells = CreateCellsForTableOf3RowsAnd2Cols();

            var table = new Table(cells);

            Assert.AreEqual(3, table.RowsCount);
        }

        [Test]
        public void ColsumnsCountShallBeSetProperly()
        {
            var cells = CreateCellsForTableOf3RowsAnd2Cols();

            var table = new Table(cells);

            Assert.AreEqual(2, table.ColumnsCount);
        }

        [Test]
        public void GettingRows_ShallGetProperCountOfRows()
        {
            var cells = CreateCellsForTableOf3RowsAnd2Cols();

            var table = new Table(cells);

            Assert.AreEqual(3, table.GetRows().Count);
        }

        [Test]
        public void GettingRows_ShallGetRowsWithCompleteData()
        {
            var cells = CreateCellsForTableOf3RowsAnd2Cols();

            var table = new Table(cells);

            var row1 = table.GetRows()[0];
            Assert.AreEqual("row0,col0", row1.GetCells()[0].Value);
            Assert.AreEqual("row0,col1", row1.GetCells()[1].Value);

            var row2 = table.GetRows()[1];
            Assert.AreEqual("row1,col0", row2.GetCells()[0].Value);
            Assert.AreEqual("row1,col1", row2.GetCells()[1].Value);

            var row3 = table.GetRows()[2];
            Assert.AreEqual("row2,col0", row3.GetCells()[0].Value);
            Assert.AreEqual("row2,col1", row3.GetCells()[1].Value);
        }

        [Test]
        public void GettingColumns_ShallGetProperCountOfColumns()
        {
            var cells = CreateCellsForTableOf3RowsAnd2Cols();

            var table = new Table(cells);

            Assert.AreEqual(2, table.GetColumns().Count());
        }


        [Test]
        public void GettingColumns_ShallGetColumnsWithCompleteData()
        {
            var cells = CreateCellsForTableOf3RowsAnd2Cols();

            var table = new Table(cells);

            var column1 = table.GetColumns()[0];
            Assert.AreEqual("row0,col0", column1.GetCells()[0].Value);
            Assert.AreEqual("row1,col0", column1.GetCells()[1].Value);
            Assert.AreEqual("row2,col0", column1.GetCells()[2].Value);

            var column2 = table.GetColumns()[1];
            Assert.AreEqual("row0,col1", column2.GetCells()[0].Value);
            Assert.AreEqual("row1,col1", column2.GetCells()[1].Value);
            Assert.AreEqual("row2,col1", column2.GetCells()[2].Value);
        }

        [Test]
        public void BackgroundColorsInNewTable_ShallBeNull()
        {
            var cells = CreateCellsForTableOf3RowsAnd2Cols();

            var table = new Table(cells);

            Assert.Null(table.GetColumns()[0].GetCells()[0].BackgroundColor);
        }

        private static TableCell[,] CreateCellsForTableOf3RowsAnd2Cols()
        {
            var cells = new List<List<TableCell>>();

            var row1 = new List<TableCell> { new TableCell { Value = "row0,col0" }, new TableCell { Value = "row0,col1" } };
            var row2 = new List<TableCell> { new TableCell { Value = "row1,col0" }, new TableCell { Value = "row1,col1" } };
            var row3 = new List<TableCell> { new TableCell { Value = "row2,col0" }, new TableCell { Value = "row2,col1" } };
            cells.Add(row1);
            cells.Add(row2);
            cells.Add(row3);
            return cells.To2DArray();
        }
    }
}
