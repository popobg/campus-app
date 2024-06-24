namespace CampusApp.School
{
    internal struct Grade
    {
        private double _mark;
        private string _comment;

        internal double Mark { get; set; }

        internal string Comment { get; set; }

        internal Grade(double mark, string comment)
        {
            Mark = mark;
            Comment = comment;
        }
    }
}
