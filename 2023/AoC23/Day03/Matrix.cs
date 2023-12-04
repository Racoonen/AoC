using System.Text;

namespace Day03;

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
                        result += validNumbers[0] * validNumbers[1];
                    }
                }
            }
        }
        return result;
    }

    private long GetRightNumber(int rowIndex, int columnIndex)
    {
        if (rowIndex < 0 && rowIndex >= Entries.Length)
        {
            return -1;
        }

        var index = columnIndex + 1;
        if (index >= Entries[rowIndex].Length)
        {
            return -1;
        }

        if (Entries[rowIndex][index].IsNumber)
        {
            return GetNumberOfIndex(rowIndex, index);
        }
        return -1;
    }

    private long GetLeftNumber(int rowIndex, int columnIndex)
    {
        var index = columnIndex - 1;
        if (index < 0)
        {
            return -1;
        }

        if (rowIndex > 0 && rowIndex < Entries.Length)
        {
            if (Entries[rowIndex][index].IsNumber)
            {
                return GetNumberOfIndex(rowIndex, index);
            }
        }
        return -1;
    }

    private long GetNumberOfIndex(int rowIndex, int columnIndex)
    {
        var row = Entries[rowIndex];
        var startIndex = columnIndex;

        var next = true;
        while (next)
        {
            if(startIndex < 0)
            {
                startIndex++;
                break;
            }
            if (row[startIndex].IsNumber)
            {
                startIndex--;
            }
            else
            {
                startIndex++;
                next = false;
            }
        }

        var builder = new StringBuilder();
        while (startIndex < row.Length && row[startIndex].IsNumber)
        {
            builder.Append(row[startIndex].ToString());
            startIndex++;
        }
        if (builder.Length > 0)
            return int.Parse(builder.ToString());

        return -1;
    }

    private long[] GetUpperNumbers(int rowIndex, int columnIndex) => GetNumbersInLine(rowIndex - 1, columnIndex);
    private long[] GetBottomNumbers(int rowIndex, int columnIndex) => GetNumbersInLine(rowIndex + 1, columnIndex);

    private long[] GetNumbersInLine(int rowIndex, int columnIndex)
    {
        var result = new List<long>();
        if (rowIndex < 0 || rowIndex >= Entries.Length)
            return result.ToArray();

        var row = Entries[rowIndex];
        if (row[columnIndex].IsNumber)
        {
            result.Add(GetNumberOfIndex(rowIndex, columnIndex));
        }
        else
        {
            if (((columnIndex - 1) > 0) && row[columnIndex - 1].IsNumber)
            {
                result.Add(GetNumberOfIndex(rowIndex, columnIndex - 1));
            }

            if (((columnIndex + 1) < row.Length) && row[columnIndex + 1].IsNumber)
            {
                result.Add(GetNumberOfIndex(rowIndex, columnIndex + 1));
            }
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
