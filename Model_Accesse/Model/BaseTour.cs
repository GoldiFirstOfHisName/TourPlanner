namespace Model_Accesse.Model
{
    public class BaseTour
    {
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }

    }

    public class Tour : BaseTour
    {
        public string description { get; set; } = "Not Described";
        public string routeInfo { get; set; } = "https://tenomad.com/wp-content/themes/unbound/images/No-Image-Found-400x264.png";
        public string distance { get; set; } = "Not Described";
        public string source { get; set; } = "https://tenomad.com/wp-content/themes/unbound/images/No-Image-Found-400x264.png";
        public string destination { get; set; } = "Berlin";

        public string location { get; set; } = "Vienna";
        public string OldName { get; set; } = "Not Described";

    }
}
