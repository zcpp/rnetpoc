using RDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rnetpoc.Global
{
    public class Util
    {
        public static string TestDataFrame(DataFrame dataset)
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder sb1 = new StringBuilder();

            sb1.Append("NO,RowName,");
            for (int i = 0; i < dataset.ColumnCount; ++i)
            {
                sb1.Append(dataset.ColumnNames[i]);
                if (i < dataset.ColumnCount - 1)
                {
                    sb1.Append(",");
                }
            }
            sb.AppendLine(sb1.ToString());
            for (int i = 0; i < dataset.RowCount; ++i)
            {
                sb1.Clear();
                sb1.Append(i+1);
                sb1.Append(",");
                sb1.Append(dataset.RowNames[i]);
                sb1.Append(",");

                for (int k = 0; k < dataset.ColumnCount; ++k)
                {
                    sb1.Append(dataset[i, k]);
                    if (k < dataset.ColumnCount - 1)
                    {
                        sb1.Append(",");
                    }
                }
                sb.AppendLine(sb1.ToString());
            }

            return sb.ToString();
        }

        public static string TestCharacterVector(CharacterVector v)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < v.Length; ++i)
            {
                sb.AppendLine(v[i]);
            }

            return sb.ToString();
        }
    }
}
