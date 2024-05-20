using System.Linq;

namespace Classes
{
    public class Graph
    {
        private Dictionary<string, int> _Cities = new Dictionary<string, int>();
        private Dictionary<int, string> _Indexes = new Dictionary<int, string>();

        private int[][] _straightMatrix { get; set; }
        private int[][] _roadMatrix { get; set; }

        public Graph(int[][] straightMatrix, int[][] roadMatrix, Dictionary<string, int> cities, Dictionary<int, string> indexes)
        {
            _straightMatrix = straightMatrix;
            _roadMatrix = roadMatrix;
            _Cities = cities;
            _Indexes = indexes;
        }

        public City AStarAlgorithm(string from, string to)
        {
            bool found = false;
            City? res = null;

            LinkedList<City> next = new LinkedList<City>();
            LinkedList<City> closed = new LinkedList<City>();
            
            //Starting city
            City start = new City()
            {
                Name = from,
                F = 0,
                H = _straightMatrix[_Cities[from]][_Cities[to]]
            };
            start.Total = start.F + start.H;

            closed.AddFirst(start);

            //Fill next list with all cities connected to start city
            int[] roadMatrix = _roadMatrix[_Cities[start.Name]];
            for (int i = 0; i < roadMatrix.Length; i++)
            {
                if (i == _Cities[start.Name] || roadMatrix[i] == int.MaxValue)
                    continue;

                int[] straightMatrix = _straightMatrix[i];
                City nextCity = new City()
                {
                    Prev = start,
                    Name = _Indexes[i],
                    F = roadMatrix[i],
                    H = straightMatrix[_Cities[to]]
                };
                nextCity.Total = nextCity.F + nextCity.H;
                next.AddFirst(nextCity);
            }

            //While route not found
            while (!found)
            {
                //Minimal cost from next list
                int min = next.Min(x => x.Total);
                //Routes with minimal cost
                IEnumerable<City> list = next.Where(x => x.Total == min);

                //Lists for deletion and addition of cities
                LinkedList<City> addNext = new LinkedList<City>();
                LinkedList<City> removeNext = new LinkedList<City>();

                //Loop through all cities with minimal route cost
                foreach (City city in list)
                {
                    //If destination is reached
                    if (city.Name == to)
                    {
                        res = city;
                        found = true;
                        break;
                    }

                    //Delete city from next list if there are any better routes through this city
                    var anotherRoutes = closed.Where(x => x.Name == city.Name && x.Total < city.Total);
                    if (anotherRoutes.Count() != 0)
                    {
                        removeNext.AddFirst(city);
                        continue;
                    }

                    //Examine road matrix for city
                    roadMatrix = _roadMatrix[_Cities[city.Name]];
                    for(int i = 0; i < roadMatrix.Length; i++)
                    {
                        //Skip if there is no connection or route loops to itself or previous city
                        if (i == _Cities[city.Prev.Name] || i == _Cities[city.Name] || roadMatrix[i] == int.MaxValue)
                            continue;

                        //Next city
                        int[] straightMatrix = _straightMatrix[i];
                        City nextCity = new City()
                        {
                            Prev = city,
                            Name = _Indexes[i],
                            F = roadMatrix[i],
                            H = straightMatrix[_Cities[to]]
                        };
                        nextCity.Total = city.Total - city.H + nextCity.F + nextCity.H;
                        
                        //Skip this city if there is already better option in closed list
                        var checkClosed = closed.FirstOrDefault(x => x.Name == nextCity.Name && x.Total < nextCity.Total);
                        if (checkClosed != null)
                            continue;

                        addNext.AddFirst(nextCity);
                    }

                    removeNext.AddFirst(city);
                    closed.AddFirst(city);
                }

                //Remove cities from next list
                foreach(var i in removeNext)
                {
                    next.Remove(i);
                }
                //Add cities to next list
                foreach(var i in addNext)
                {
                    next.AddFirst(i);
                }
            }

            if (res == null)
                throw new Exception("Path was not found");

            return res;
        }
    }
}