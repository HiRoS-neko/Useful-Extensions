namespace UsefulExtensions.Logic
{
    public static class FloatExtensions
    {
        public static float ConstrainAngle(this float angle)
        {
            return (angle + 360) % 360;
        }
    }
}
