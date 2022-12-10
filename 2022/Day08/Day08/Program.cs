internal class Program
{


    private static void Main(string[] args)
    {
        var matrix = new Matrix();

        foreach(var line in File.ReadLines("../../../Input.txt"))
            matrix.ParseLine(line);
        matrix.InitializeTreesAround();
        Console.WriteLine(matrix.GetCountOfVisibleTrees());
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

        public int GetCountOfVisibleTrees()
        {
            return matrix.Sum(e => e.Count(t => t.IsVisible));
        }

        public int GetCountOfVisibleTreesFirst()
        {
            var invisibleTrees = new List<Tree>();
            var result = 0;
            foreach(var row in matrix)
            {
                var highest = GetHighestTreeInRows(row);
                foreach(var tree in row)
                {
                    if (tree.ColIndex > highest.leftTree.ColIndex && tree.ColIndex < highest.rightTree.ColIndex)
                        invisibleTrees.Add(tree);
                }
            }

            foreach(var pos in invisibleTrees.ToList())
                if(pos.RowIndex == 0 || pos.ColIndex == 0)
                    invisibleTrees.Remove(pos);

            for(int col =0 ; col < matrix.Count; col++)
            {
                var highest = GetHighestTreeInColumn(col);
                foreach(var possible in invisibleTrees)
                {
                    if (possible.RowIndex > highest.topTree.RowIndex && possible.RowIndex < highest.bottomTree.RowIndex)
                        result++;
                }
            }

            return (matrix.Count * matrix.Count)- result;
        }

        private (Tree topTree, Tree bottomTree) GetHighestTreeInColumn(int columnIndex)
        {
            var column = matrix.Select(e => e[columnIndex]);
            var ordered = column.OrderByDescending(e => e.Size).ThenBy(e => e.RowIndex);
            var first = ordered.FirstOrDefault();
            var second = ordered.ElementAt(1);
            if (first.RowIndex > second.RowIndex)
                return (second, first);
            return (first, second);
        }

        private (Tree leftTree, Tree rightTree) GetHighestTreeInRows(List<Tree> treeRow)
        {
            var ordered = treeRow.OrderByDescending(e=> e.Size).ThenBy(e=> e.ColIndex);
            var first = ordered.FirstOrDefault();
            var second = ordered.ElementAt(1);
            if (first.ColIndex > second.ColIndex)
                return (second, first);
            return (first, second);
        }
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
    }
}