using ChartTestApi.Enums;

namespace ChartTestApi.Models
{
    public class Track
    {
        public int Id { get; set; }
        public int FirstId { get; set; }

        public int SecondId { get; set; }

        public int Distance { get; set; }

        public Surface Surface { get; set; }

        public MaxSpeed MaxSpeed { get; set; }


    }
}
