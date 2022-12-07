using System.Runtime.CompilerServices;

internal class Program
{
    private static void Main(string[] args)
    {
        var headDetector = new Detector(4);
        var messageDetector = new Detector(14);
        using(var reader = new StreamReader("../../../Input.txt"))
        {
            do
            {
                var current = (char)reader.Read();
                headDetector.Add(current);
                messageDetector.Add(current);
                if (headDetector.isDone && messageDetector.isDone)
                    break;
               
            }
            while (!reader.EndOfStream);

            Console.WriteLine(headDetector.MarkerIndex);
            Console.WriteLine(messageDetector.MarkerIndex);
        }
    }

    class Detector
    {
        public int MarkerIndex => markerIndex - nextChars.Count;

        private int markerIndex = 0;
        private readonly List<char> marker = new List<char>();
        private readonly Queue<char> nextChars = new Queue<char>();

        private readonly int countOfUnique;

        public bool isDone { get; private set; }

        public Detector(int countOfUniques)
        {
            countOfUnique = countOfUniques;
        }

        public void Add(char current)
        {
            if (isDone)
                return;

            var indexOfCurrent = marker.IndexOf(current);
            if (indexOfCurrent != -1)
            {
                marker.RemoveRange(0, indexOfCurrent + 1);
                while (marker.Count < countOfUnique)
                {
                    if (nextChars.Count != 0)
                        marker.Add(nextChars.Dequeue());
                    else
                    {
                        marker.Add(current);
                        break;
                    }
                }
            }
            else
            {
                if (marker.Count < countOfUnique)
                    marker.Add(current);
                else
                    nextChars.Enqueue(current);
            }
            markerIndex++;

            isDone =  marker.Count == countOfUnique && markerIndex >= (countOfUnique*2) -1;
        }
    }
}