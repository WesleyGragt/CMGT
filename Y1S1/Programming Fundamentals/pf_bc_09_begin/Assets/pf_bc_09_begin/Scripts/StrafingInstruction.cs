using System;


namespace pf_bc_09_begin
{
    [Serializable]
    public class StrafingInstruction
    {
        public SteeringDirection direction;
        public int duration;
    }

    public enum SteeringDirection
    {
        None = 4,
        Up = 0,
        Down = 1,
        Left = 2,
        Right = 3
    }
}