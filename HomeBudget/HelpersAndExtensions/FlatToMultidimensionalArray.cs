using System;

namespace HomeBudget.Helpers
{
    public class FlatToMultidimensionalArray
    {
        public static TModel[,] Fold<TModel>(TModel[] cells, int rowsCount, int columnsCount)
        {
            if (cells.Length != rowsCount * columnsCount)
            {
                throw new Exception("Items count not equal to rowsCount * columnsCount!");
            }

            var result = new TModel[rowsCount, columnsCount];

            for (int i = 0; i < rowsCount; ++i)
            {
                for (int j = 0; j < columnsCount; ++j)
                {
                    result[i, j] = cells[(i * columnsCount) + j];
                }
            }

            return result;
        }
    }
}