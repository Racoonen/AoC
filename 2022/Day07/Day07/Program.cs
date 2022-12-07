internal class Program
{
    private static void Main(string[] args)
    {
        var navi = new DirectoryNavigator();
        foreach(var line in File.ReadLines("../../../Input.txt"))
            navi.DoCommand(line);
        Console.WriteLine(navi.GetSumOfDirectoriesWithMax(100000));
        Console.WriteLine(navi.FreeNeededDirToGetFree(70000000, 30000000));
    }

    class DirectoryNavigator
    {
        private Directory topLevel = new Directory("/", null);
        private Directory current;

        public void DoCommand(string path)
        {
            if (path.StartsWith("$ cd"))
                DoCDCommand(path);
            else if(!path.StartsWith("$ ls"))
                ValidateOutput(path);
        }

        public long FreeNeededDirToGetFree(long diskspace, long neededFreeSpace)
        {
            var needToDelete = neededFreeSpace - (diskspace - topLevel.Size);
            if (needToDelete < 0)
                return 0;
            var dir = topLevel.GetDirectoriesBigEnough(needToDelete);
            return dir.Min(e => e.Size);
        } 

        public long GetSumOfDirectoriesWithMax(long max) => topLevel.GetSizeOfDirectoriesWithMax(max);

        private void ValidateOutput(string path)
        {
            var inputs = path.Split(' ');
            if (inputs[0] == "dir")
                current.GetSubDirectory(inputs[1]);
            else
            {
                current.SetFile(inputs[1],long.Parse(inputs[0]));
            }
        }

        private void DoCDCommand(string input)
        {
            var path = input.Split(' ');
            if (path[2] == "/")
                current = topLevel;
            else if (path[2] == "..")
                current = current.top;
            else
                current = current.GetSubDirectory(path[2]);
        }
    }

    class DirectoryFile
    {
        public string Name { get; }
        public long Size { get; }

        public DirectoryFile(string name, long size)
        {
            Name = name;
            Size = size;
        }
    }

    class Directory
    {
        public Directory top { get; private set; }
        public List<Directory> Directories= new List<Directory>();
        private List<DirectoryFile> files = new List<DirectoryFile>();
        public long Size => Directories.Sum(e=> e.Size) + files.Sum(e=> e.Size);
        public string Name { get; }

        public Directory(string name, Directory topDir)
        {
            Name = name;
            top = topDir;
        }

        internal void SetFile(string name, long size)
        {
            var file = files.Where(e=> e.Name == name).FirstOrDefault();
            if (file == null)
                files.Add(new DirectoryFile(name,size));
        }

        internal Directory GetSubDirectory(string name)
        {
            var dir = Directories.FirstOrDefault(e=> e.Name== name);
            if (dir != null)
                return dir;
            dir = new Directory(name, this);
            Directories.Add(dir);
            return dir;
        }

        internal long GetSizeOfDirectoriesWithMax(long max)
        {
            long result = 0;
            if (Size < max)
            {
                result += Size;
            }
            result += Directories.Sum(e => e.GetSizeOfDirectoriesWithMax(max));
            return result;
        }

        internal List<Directory> GetDirectoriesBigEnough(long neededSpace)
        {
            var result = new List<Directory>();

            if (Size >= neededSpace)
                result.Add(this);

            Directories.ForEach(e => result.AddRange(e.GetDirectoriesBigEnough(neededSpace)));
            return result;
        }
    }
}