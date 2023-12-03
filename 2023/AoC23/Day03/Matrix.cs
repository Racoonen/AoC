using System.Text;

namespace Day03
{
    internal record Matrix(Entry[][] Entries)
    {
        public long CalcGearRatio()
        {
            long result = 0;
            for (int rowIndex = 0; rowIndex < Entries.Length; rowIndex++)
            {
                var row = Entries[rowIndex];
                for (int columnIndex = 0; columnIndex < row.Length; columnIndex++)
                {
                    var entry = row[columnIndex];
                    if (entry.IsGearMarker)
                    {
                        //Console.WriteLine($"Gear found at {rowIndex} {columnIndex}");
                        var numbers = new List<long>
                        {
                            GetLeftNumber(rowIndex, columnIndex),
                            GetRightNumber(rowIndex, columnIndex)
                        };

                        numbers.AddRange(GetUpperNumbers(rowIndex, columnIndex));
                        numbers.AddRange(GetBottomNumbers(rowIndex, columnIndex));

                        var validNumbers = numbers.Where(e => e != -1).ToList();
                        if (validNumbers.Count == 2)
                        {
                            //Console.WriteLine($"Numbers or Gear {validNumbers[0]} {validNumbers[1]}");
                            result += validNumbers[0] * validNumbers[1];
                        }
                        else
                        {
                            Console.WriteLine($"No Numbers found at {rowIndex} {columnIndex}.");
                        }
                    }
                }
            }
            return result;
        }

        private long GetRightNumber(int rowIndex, int columnIndex)
        {
            return GetNumberOfIndex(rowIndex, columnIndex + 1, out var _);
        }

        private long GetLeftNumber(int rowIndex, int columnIndex)
        {
            return GetNumberOfIndex(rowIndex, columnIndex - 1, out var _);
        }

        private long GetNumberOfIndex(int rowIndex, int columnIndex, out int latestIndex)
        {
            var row = Entries[rowIndex];
            var startIndex = columnIndex;
            latestIndex = startIndex;

            while (startIndex > 0 && row[startIndex].IsNumber)
            {
                if ((startIndex - 1) > 0 || !row[startIndex - 1].IsNumber)
                {
                    break;
                }
                startIndex--;
            }
            var builder = new StringBuilder();
            while (startIndex < row.Length && row[startIndex].IsNumber)
            {
                latestIndex = startIndex;
                builder.Append(row[startIndex].ToString());
                startIndex++;
            }
            if (builder.Length > 0)
                return int.Parse(builder.ToString());

            return -1;
        }

        private long[] GetBottomNumbers(int rowIndex, int columnIndex)
        {
            var result = new List<long>();
            var bottomRowIndex = rowIndex + 1;
            if (bottomRowIndex > Entries.Length)
            {
                return result.ToArray();
            }
            var row = Entries[bottomRowIndex];
            var latestIndex = columnIndex;
            if (row[columnIndex].IsNumber)
            {
                result.Add(GetNumberOfIndex(bottomRowIndex, columnIndex, out latestIndex));
            }

            if ((!result.Any() || result[0] == -1) && ((columnIndex - 1) > 0) && row[columnIndex - 1].IsNumber)
            {
                result.Add(GetNumberOfIndex(bottomRowIndex, columnIndex - 1, out latestIndex));
            }
            if (latestIndex <= columnIndex && ((columnIndex + 1) < row.Length) && row[columnIndex + 1].IsNumber)
            {
                result.Add(GetNumberOfIndex(bottomRowIndex, columnIndex + 1, out _));
            }
            return result.ToArray();
        }

        private long[] GetUpperNumbers(int rowIndex, int columnIndex)
        {
            var result = new List<long>();
            var upperRowIndex = rowIndex - 1;
            if (upperRowIndex < 0)
            {
                return result.ToArray();
            }
            var row = Entries[upperRowIndex];
            var latestIndex = columnIndex;
            if (row[columnIndex].IsNumber)
            {
                result.Add(GetNumberOfIndex(upperRowIndex, columnIndex, out latestIndex));
            }

            if ((!result.Any() || result[0] == -1) && (columnIndex - 1 > 0) && row[columnIndex - 1].IsNumber)
            {
                result.Add(GetNumberOfIndex(upperRowIndex, columnIndex - 1, out latestIndex));
            }
            if (latestIndex <= columnIndex && ((columnIndex + 1) < row.Length) && row[columnIndex + 1].IsNumber)
            {
                result.Add(GetNumberOfIndex(upperRowIndex, columnIndex + 1, out _));
            }
            return result.ToArray();
        }

        public int CalcSumOfValidEntries()
        {
            var result = 0;
            for (int rowIndex = 0; rowIndex < Entries.Length; rowIndex++)
            {
                var row = Entries[rowIndex];
                for (int columnIndex = 0; columnIndex < row.Length; columnIndex++)
                {
                    var entry = row[columnIndex];
                    if (entry.IsNumber)
                    {
                        var number = $"{entry.Value}{GetNextNumbers(row, columnIndex, out var latestIndex)}";
                        if (IsValid(rowIndex, columnIndex, latestIndex))
                        {
                            result += int.Parse(number);
                        }
                        columnIndex = latestIndex;
                    }
                }
            }
            return result;
        }

        private string GetNextNumbers(Entry[] entries, int indexToStart, out int latestIndex)
        {
            var result = new StringBuilder();
            latestIndex = indexToStart + 1;
            while (latestIndex < entries.Length)
            {
                var entry = entries[latestIndex];
                if (!entry.IsNumber)
                {
                    latestIndex--;
                    return result.ToString();
                }
                latestIndex++;
                result.Append(entry.ToString());
            }
            return result.ToString();
        }

        private bool IsValid(int rowIndex, int startColumnIndex, int endColumnIndex)
        {
            if (IsValidInUpperRow(rowIndex, startColumnIndex, endColumnIndex))
            {
                return true;
            }
            if (IsValidInCurrentRow(rowIndex, startColumnIndex, endColumnIndex))
            {
                return true;
            }
            if (IsValidInBottomRow(rowIndex, startColumnIndex, endColumnIndex))
            {
                return true;
            }
            return false;
        }

        private bool IsValidInUpperRow(int rowIndex, int startColumnIndex, int endColumnIndex)
        {
            var previousIndex = rowIndex - 1;
            if (previousIndex > 0)
            {
                // diagonal
                if (IsValidInCurrentRow(previousIndex, startColumnIndex, endColumnIndex))
                {
                    return true;
                }
                var row = Entries[previousIndex];
                for (int current = startColumnIndex; current < endColumnIndex + 1; current++)
                {
                    if (current >= row.Length)
                    {
                        return false;
                    }
                    if (row[current].IsSymbol)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool IsValidInBottomRow(int rowIndex, int startColumnIndex, int endColumnIndex)
        {
            var previousIndex = rowIndex + 1;
            if (previousIndex < Entries.Length)
            {
                // diagonal
                if (IsValidInCurrentRow(previousIndex, startColumnIndex, endColumnIndex))
                {
                    return true;
                }
                var row = Entries[previousIndex];
                for (int current = startColumnIndex; current < endColumnIndex + 1; current++)
                {
                    if (current >= row.Length)
                    {
                        return false;
                    }
                    if (row[current].IsSymbol)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool IsValidInCurrentRow(int rowIndex, int startColumnIndex, int endColumnIndex)
        {
            var row = Entries[rowIndex];
            var nextIndex = endColumnIndex + 1;
            if (nextIndex < row.Length)
            {
                if (row[nextIndex].IsSymbol)
                    return true;
            }
            var previousIndex = startColumnIndex - 1;
            if (previousIndex > 0)
            {
                if (row[previousIndex].IsSymbol)
                    return true;
            }

            return false;
        }
    }
}
