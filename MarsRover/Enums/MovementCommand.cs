using System.ComponentModel;

namespace MarsRover.Enums
{
    public enum MovementCommand
    {
        [Description("Move")]
        M,
        [Description("Turn Left")]
        L,
        [Description("Turn Right")]
        R
    }
}