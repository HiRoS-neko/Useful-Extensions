using UnityEngine;

namespace UsefulExtensions.Logic
{
    public static class MathfExtensions
    {
        /// <summary>
        /// Calculates the angle difference between two quaternions around a target axis
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="axis">The target axis to calculate the rotation around</param>
        /// <returns>The angle difference between the quaternions</returns>
        public static float AngleDifference(Quaternion a, Quaternion b, Vector3 axis)
        {
            // mock rotate the axis with each quaternion
            var vecA = a * axis;
            var vecB = b * axis;

            // now we need to compute the actual 2D rotation projections on the base plane
            var angleA = Mathf.Atan2(vecA.x, vecA.z) * Mathf.Rad2Deg;
            var angleB = Mathf.Atan2(vecB.x, vecB.z) * Mathf.Rad2Deg;

            // get the signed difference in these angles
            return Mathf.DeltaAngle(angleA, angleB);
        }

        /// <summary>
        /// Calculates the angle difference between two vectors
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>The angle difference between the vectors</returns>
        public static float AngleDifference(Vector3 a, Vector3 b)
        {
            var vecA = Vector3.ProjectOnPlane(a, Vector3.up).normalized;
            var vecB = Vector3.ProjectOnPlane(b, Vector3.up).normalized;

            // now we need to compute the actual 2D rotation projections on the base plane
            var angleA = Mathf.Atan2(vecA.x, vecA.z) * Mathf.Rad2Deg;
            var angleB = Mathf.Atan2(vecB.x, vecB.z) * Mathf.Rad2Deg;

            // get the signed difference in these angles
            return Mathf.DeltaAngle(angleA, angleB);
        }
    }
}
