using Classes;

namespace Lab8
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Graph graph = CreateGraph();

            //string from = "Balikpapan"; string to = "Sotek";
            //string from = "Loa Kulu"; string to = "Djenebora";
            //string from = "Lemper"; string to = "Balikpapan";
            string from = "Buat"; string to = "Muarasiram";

            //Greedy search
            var res1 = graph.GreedySearch(from, to);
            LinkedList<City> route = new LinkedList<City>();
            City? next = res1;
            while (next != null)
            {
                route.AddFirst(next);
                next = next.Prev;
            }
            Console.WriteLine("=== Greedy search ===");
            Console.WriteLine($"{from}-{to} Distance: {res1.Total}km Route:");
            Console.WriteLine(string.Join(" -> ", route));

            //A* search
            var res2 = graph.AStarAlgorithm(from, to);
            route = new LinkedList<City>();
            next = res2;
            while(next != null)
            {
                route.AddFirst(next);
                next = next.Prev;
            }
            Console.WriteLine("=== A* search ===");
            Console.WriteLine($"{from}-{to} Distance: {res2.Total}km Route:");
            Console.WriteLine(string.Join(" -> ", route));
        }

        static Graph CreateGraph()
        {
            int inf = int.MaxValue;
            Dictionary<string, int> cities = new Dictionary<string, int>() 
            {
                { "Balikpapan", 0 },
                { "Sambodja",   1 },
                { "Menjangau" , 2 },
                { "Gitan" ,     3 },
                { "Buat" ,      4 },
                { "Loa Kulu" ,  5 },
                { "Sidulang" ,  6 },
                { "Induandjat" ,7 },
                { "Djambu" ,    8 },
                { "Muarasiram" ,9 },
                { "Sepan" ,    10 },
                { "Djenebora" ,11 },
                { "Panajam" ,  12 },
                { "Sotek" ,    13 },
                { "Lemper" ,   14 }
            };
            Dictionary<int, string> indexes = new Dictionary<int, string>()
            {
                { 0, "Balikpapan" },
                { 1, "Sambodja"   },
                { 2, "Menjangau"  },
                { 3, "Gitan"      },
                { 4, "Buat"       },
                { 5, "Loa Kulu"   },
                { 6, "Sidulang"   },
                { 7, "Induandjat" },
                { 8, "Djambu"     },
                { 9, "Muarasiram" },
                {10 , "Sepan"     },
                {11 , "Djenebora" },
                {12 , "Panajam"   },
                {13 , "Sotek"     },
                {14 , "Lemper"    }
            };

            int[][] straightMatrix = new int[][]
            {
                new int[]{  0, 32, 39, 63, 68, 81, 96, 93, 85, 96, 29, 13, 12, 31, 83}, //0
                new int[]{ 32,  0, 31, 43, 53, 54, 80, 87, 88,100, 49, 36, 43, 51, 98}, //1
                new int[]{ 39, 31,  0, 25, 28, 48, 56, 58, 57, 68, 35, 34, 44, 40, 68}, //2
                new int[]{ 63, 43, 25,  0, 11, 24, 35, 51, 60, 74, 59, 59, 69, 65, 82}, //3
                new int[]{ 68, 53, 28, 11,  0, 32, 27, 40, 50, 67, 59, 62, 73, 66, 74}, //4
                new int[]{ 81, 54, 48, 24, 32,  0, 41, 67, 81, 95, 83, 80, 89, 88,106}, //5
                new int[]{ 96, 80, 56, 35, 27, 41,  0, 31, 52, 67, 83, 89, 99, 89, 83}, //6
                new int[]{ 93, 87, 58, 51, 40, 67, 31,  0, 24, 35, 73, 84, 94, 78, 55}, //7
                new int[]{ 85, 88, 57, 60, 50, 81, 52, 24,  0, 14, 59, 73, 81, 63, 30}, //8
                new int[]{ 96,100, 68, 74, 67, 95, 67, 35, 14,  0, 65, 81, 87, 67, 21}, //9
                new int[]{ 29, 49, 35, 59, 59, 83, 83, 73, 59, 65,  0, 16, 22,  6, 56}, //10
                new int[]{ 13, 36, 34, 59, 62, 80, 89, 84, 73, 81, 16,  0, 10, 16, 72}, //11
                new int[]{ 12, 43, 44, 69, 73, 89, 99, 94, 81, 87, 22, 10,  0, 20, 76}, //12
                new int[]{ 31, 51, 40, 65, 66, 88, 89, 78, 63, 67,  6, 16, 20,  0, 57}, //13
                new int[]{ 83, 98, 68, 82, 74,106, 83, 55, 30, 21, 56, 72, 76, 57,  0}  //14
            };

            int[][] roadMatrix = new int[][]
            {
                new int[]{  0, 46,inf,inf,127,inf,inf,inf,inf,inf, 70,inf,inf,inf,inf}, //0
                new int[]{ 46,  0,inf, 83,inf, 67,inf,inf,inf,inf,inf,inf,inf,inf,285}, //1
                new int[]{inf,inf,  0, 62,inf,inf,inf,inf,inf,185,inf,inf, 80,inf,inf}, //2
                new int[]{inf, 83, 62,  0,inf,inf, 64,inf,137,inf,inf,inf,148,inf,208}, //3
                new int[]{127,inf,inf,inf,  0,inf,inf,inf,inf,148, 92,inf,inf, 92,inf}, //4
                new int[]{inf, 67,inf,inf,inf,  0,inf,inf,inf,inf,inf,inf,133,inf,228}, //5
                new int[]{inf,inf,inf, 64,inf,inf,  0, 44,inf,inf,139,inf,inf,inf,inf}, //6
                new int[]{inf,inf,inf,inf,inf,inf, 44,  0, 43, 67,inf,173,inf,inf,inf}, //7
                new int[]{inf,inf,inf,137,inf,inf,inf, 43,  0,inf,inf,inf,inf,210,inf}, //8
                new int[]{inf,inf,185,inf,148,inf,inf, 67,inf,  0,233,241,inf,inf,inf}, //9
                new int[]{ 70,inf,inf,inf, 92,inf,139,inf,inf,233,  0, 29,inf, 10,inf}, //10
                new int[]{inf,inf,inf,inf,inf,inf,inf,173,inf,241, 29,  0,inf,inf,inf}, //11
                new int[]{inf,inf, 80,148,inf,133,inf,inf,inf,inf,inf,inf,  0, 37,inf}, //12
                new int[]{inf,inf,inf,inf, 92,inf,inf,inf,210,inf, 10,inf, 37,  0,282}, //13
                new int[]{inf,285,inf,208,inf,228,inf,inf,inf,inf,inf,inf,inf,282,  0}  //14
            };

            return new Graph(straightMatrix, roadMatrix, cities, indexes);
        }
    }
}


//int[][] straightMatrix = new int[][]
//{
//    new int[]{  0, 32, 39, 63, 68, 81, 96, 93, 85, 96, 29, 13, 12, 31, 83}, //0
//    new int[]{ 32,  0, 31, 43, 53, 54, 80, 87, 88,100, 49, 36, 43, 51, 98}, //1
//    new int[]{ 39, 31,  0, 25, 28, 48, 56, 58, 57, 68, 35, 34, 44, 40, 68}, //2
//    new int[]{ 63, 43, 25,  0, 11, 24, 35, 51, 60, 74, 59, 59, 69, 65, 82}, //3
//    new int[]{ 68, 53, 28, 11,  0, 32, 27, 40, 50, 67, 59, 62, 73, 66, 74}, //4
//    new int[]{ 81, 54, 48, 24, 32,  0, 41, 67, 81, 95, 83, 80, 89, 88,106}, //5
//    new int[]{ 96, 80, 56, 35, 27, 41,  0, 31, 52, 67, 83, 89, 99, 89, 83}, //6
//    new int[]{ 93, 87, 58, 51, 40, 67, 31,  0, 24, 35, 73, 84, 94, 78, 55}, //7
//    new int[]{ 85, 88, 57, 60, 50, 81, 52, 24,  0, 14, 59, 73, 81, 63, 30}, //8
//    new int[]{ 96,100, 68, 74, 67, 95, 67, 35, 14,  0, 65, 81, 87, 67, 21}, //9
//    new int[]{ 29, 49, 35, 59, 59, 83, 83, 73, 59, 65,  0, 16, 22,  6, 56}, //10
//    new int[]{ 13, 36, 34, 59, 62, 80, 89, 84, 73, 81, 16,  0, 10, 16, 72}, //11
//    new int[]{ 12, 43, 44, 69, 73, 89, 99, 94, 81, 87, 22, 10,  0, 20, 76}, //12
//    new int[]{ 31, 51, 40, 65, 66, 88, 89, 78, 63, 67,  6, 16, 20,  0, 57}, //13
//    new int[]{ 83, 98, 68, 82, 74,106, 83, 55, 30, 21, 56, 72, 76, 57,  0}  //14
//};

//int[][] roadMatrix = new int[][]
//{
//    new int[]{  0, 46, 81,124,127,109,174,203,245,268, 70, 89, 19, 65,317}, //0
//    new int[]{ 46,  0, 54, 83,101, 67,147,176,223,236, 92, 98, 55, 90,285}, //1
//    new int[]{ 81, 54,  0, 62, 44,110, 91,120,162,185, 49, 55, 80, 49,234}, //2
//    new int[]{124, 83, 62,  0, 35, 33, 64, 94,137,160,110,118,148,110,208}, //3
//    new int[]{127,101, 44, 35,  0, 42, 52, 82,125,148, 92,100,123, 92,197}, //4
//    new int[]{109, 67,110, 33, 42,  0, 69,101,143,179,151,159,133,151,228}, //5
//    new int[]{174,147, 91, 64, 52, 69,  0, 44, 86,110,139,144,170,137,158}, //6
//    new int[]{203,176,120, 94, 82,101, 44,  0, 43, 67,166,173,199,165,116}, //7
//    new int[]{245,223,162,137,125,143, 86, 43,  0, 31,210,215,241,210, 80}, //8
//    new int[]{268,236,185,160,148,179,110, 67, 31,  0,233,241,264,233, 58}, //9
//    new int[]{ 70, 92, 49,110, 92,151,139,166,210,233,  0, 29, 41, 10,282}, //10
//    new int[]{ 89, 98, 55,118,100,159,144,173,215,241, 29,  0, 60, 29,287}, //11
//    new int[]{ 19, 55, 80,148,123,133,170,199,241,264, 41, 60,  0, 37,313}, //12
//    new int[]{ 65, 90, 49,110, 92,151,137,165,210,233, 10, 29, 37,  0,282}, //13
//    new int[]{317,285,234,208,197,228,158,116, 80, 58,282,287,313,282,  0}  //14
//};