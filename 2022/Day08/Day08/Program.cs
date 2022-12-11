internal class Program
{
    private static void Main(string[] args)
    {
        var matrix = new Matrix();

        foreach(var line in File.ReadLines("../../../Input.txt"))
            matrix.ParseLine(line);
        matrix.InitializeTreesAround();
        Console.WriteLine(matrix.GetCountOfVisibleTrees());
        Console.WriteLine(matrix.GetHighestScenicScore());
    }

    class Matrix
    {
        private List<List<Tree>> matrix = new List<List<Tree>>();

        public void ParseLine(string line)
        {
            var row = new List<Tree>();
            var rowIndex = matrix.Count;
            for(int i = 0; i < line.Length;i++)
            {
                row.Add(new Tree() { RowIndex = rowIndex, ColIndex = i, Size = int.Parse(line[i].ToString()) });
            }
            matrix.Add(row);
        }

        public void InitializeTreesAround()
        {
            for(int rowIndex =0; rowIndex < matrix.Count; rowIndex++)
            {
                var row = matrix[rowIndex];
                for(int colIndex = 0; colIndex < row.Count; colIndex++)
                {
                    row[colIndex].BottomTree = GetTree(rowIndex + 1, colIndex);
                    row[colIndex].LeftTree= GetTree(rowIndex, colIndex-1);
                    row[colIndex].TopTree = GetTree(rowIndex -1, colIndex);
                    row[colIndex].RightTree= GetTree(rowIndex , colIndex+1);
                }
            }
        }

        private Tree GetTree(int row, int col)
        {
            // works cause of nxn matrix
            if (row < 0 || row >= matrix.Count ||
                col < 0 || col >= matrix.Count)
                return null;

            return matrix[row][col];
        }

        public int GetCountOfVisibleTrees() => matrix.Sum(e => e.Count(t => t.IsVisible));

        public int GetHighestScenicScore() => matrix.SelectMany(e => e.Where(e => e.IsVisible)).Max(e => e.GetScenicScore());
    }


    class Tree
    {
        public int Size { get; init; }
        public int RowIndex { get; init; }
        public int ColIndex { get; init; }
        public Tree TopTree { get; set; }
        public Tree BottomTree{ get; set; }
        public Tree LeftTree { get; set; }
        public Tree RightTree { get; set; }

        public bool IsVisible => DetectVisibility();

        private bool DetectVisibility()
        {
            if (TopTree == null || LeftTree == null || RightTree == null || BottomTree == null)
                return true;

            bool CheckTree(Tree tree, Func<Tree, Tree> SelectNextTree)
             {
                while (tree != null)
                {
                    if (tree.Size >= Size)
                        return false;
                    tree = SelectNextTree(tree);
                }
                return true;
             }

            bool result = CheckTree(TopTree, (tree)=> tree.TopTree);
            if (!result)
                result = CheckTree(BottomTree, (tree) => tree.BottomTree);
            if (!result)
                result = CheckTree(LeftTree, (tree) => tree.LeftTree);
            if (!result)
                result = CheckTree(RightTree, (tree) => tree.RightTree);

            return result;
        }

        public int GetScenicScore()
        {
            int CheckTree(Tree tree, Func<Tree, Tree> SelectNextTree)
            {
                var result = 0;
                while (tree != null)
                {
                    result++;
                    if (tree.Size >= Size)
                        break;
                    tree = SelectNextTree(tree);
                }
                return result;
            }
            int range = CheckTree(TopTree, (tree) => tree.TopTree);
            range *= CheckTree(BottomTree, (tree) => tree.BottomTree);
            range *= CheckTree(LeftTree, (tree) => tree.LeftTree);
            range *= CheckTree(RightTree, (tree) => tree.RightTree);
            return range;
        }
    }
}