using Microsoft.Xna.Framework;

namespace Hotel
{
    public class ConfigModel
    {
        public string LayoutPath { get; set; }
        public float HTELength { get; set; }
        public float ElevatorSpeed { get; set; }
        public float WalkingSpeed { get; set; }
        public int FilmDuration { get; set; }
        public int CleaningDuration { get; set; }
        public int Survivability { get; set; }
        public float StaircaseWeight { get; set; }
        public float ReceptionistWorkLenght { get; set; }
        public int NumberOfCleaners { get; set; }
    }
}
