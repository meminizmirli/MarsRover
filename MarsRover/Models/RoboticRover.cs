using System.Collections.Generic;

namespace MarsRover.Models
{
    public class RoboticRover
    {
        public int XCoordinate { get; set; }
        public int YCoordinate { get; set; }
        public int DirectionId { get; set; }
        public List<int> MovementCommads { get; set; }
    }
}
