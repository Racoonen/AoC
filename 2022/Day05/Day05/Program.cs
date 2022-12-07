using System.Security.Cryptography;

internal class Program
{
    private static Matrix matrix9000 = new Matrix(9, false);
    private static Matrix matrix9001 = new Matrix(9, true);

    private static bool doInstructions;
    private static void Main(string[] args)
    {
        foreach(var line in File.ReadLines("../../../Input.txt"))
        {
            Parse(line);
        }
        Console.WriteLine(matrix9000.GetLastCratesAsString());
        Console.WriteLine(matrix9001.GetLastCratesAsString());
    }

    class Matrix
    {
        private readonly Stack<char>[] values;
        private readonly bool isMover90001;

        public Matrix(int size, bool asMover9001) 
        {
            values = new Stack<char>[size];
            isMover90001 = asMover9001;
            for(int i =0; i < size; i++) { values[i] = new Stack<char>(); }
        }

        public void DoReverse()
        {
            foreach(var stack in values) 
            {
                var reversed = stack.ToArray();
                stack.Clear();
                foreach (var c in reversed)
                    stack.Push(c);
            }
        }

        public void Add(int index, char c)
        {
            values[index].Push(c);
        }

        public void DoInstruction(int count, int from, int to)
        {
            if (isMover90001)
                DoInstrutionAsMover9001(count, from, to);
            else
                DoInstrutionAsMover9000(count, from, to);
        }

        private void DoInstrutionAsMover9000(int count, int from, int to)
        {
            for (int i = 0; i < count; i++)
                values[to - 1].Push(values[from - 1].Pop());
        }

        private void DoInstrutionAsMover9001(int count, int from, int to)
        {
            var crates = new List<char>();
            for (int i = 0; i < count; i++)
                crates.Add(values[from - 1].Pop());

            crates.Reverse();
            crates.ForEach(c => values[to - 1].Push(c));
        }

        public string GetLastCratesAsString() => new string(values.Select(e => e.First()).ToArray());
    }

    private static void Parse(string line)
    {
        if (line.Length == 0)
        {
            doInstructions = true;
            matrix9000.DoReverse();
            matrix9001.DoReverse();
            return;
        }
        if (doInstructions)
            ParseAndDoInstruction(line);
        else
            ParseAndAddMatrix(line);
    }

    private static void ParseAndDoInstruction(string line)
    {
        var input = line.Split(' ');
        (int count, int from, int to) instr = (int.Parse(input[1]), int.Parse(input[3]), int.Parse(input[5]));
        matrix9000.DoInstruction(instr.count, instr.from, instr.to);
        matrix9001.DoInstruction(instr.count, instr.from, instr.to);
    }

    private static void ParseAndAddMatrix(string line)
    {
        var chars = line.ToCharArray();
        for (int i = 0; i < chars.Length; i++)
        {
            // skip every 4 char 
            if (i % 4 == 3)
            {
                if (chars[i - 3] == '[')
                {
                    matrix9000.Add((i / 4), chars[i - 2]);
                    matrix9001.Add((i / 4), chars[i - 2]);
                }
            } else if(i == chars.Length-1)
            {
                if (chars[i - 2] == '[')
                {
                    matrix9000.Add((i / 4), chars[i - 1]);
                    matrix9001.Add((i / 4), chars[i - 1]);
                }
            }
        }
    }
}