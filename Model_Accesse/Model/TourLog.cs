namespace Model_Accesse.Model
{
    public class TourLog : BaseTour
    {
        public int id { get; set; }
        public string date { get; set; } = "09.12.1996";
        public string report { get; set; } = "Was ok";
        public string distance { get; set; } = "200";
        public string totalTime { get; set; }
        public string rating { get; set; } = "3,5";
        public string avgSpeed { get; set; }
        public string burnedCalories { get; set; }
        public string startTime { get; set; } = "00:00";
        public string endTime { get; set; } = "13:00";
        public string comment { get; set; } = "Placeholder Comment";

    }
}
