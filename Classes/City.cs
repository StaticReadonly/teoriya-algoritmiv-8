namespace Classes
{
    public class City
    {
        public City Prev { get; set; }
        public string Name { get; set; }
        public int H { get; set; }
        public int F { get; set; }
        public int Total { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null)
                return false;

            if (obj is not City)
                return false;

            var c = (City)obj;
            return Name == c.Name && Total == c.Total;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
