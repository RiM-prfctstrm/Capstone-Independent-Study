/*=================================================================================================
 * FILE     : UtilityFormulas.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 9/21/24
 * UPDATED  : 9/21/24
 * 
 * DESC     : Contains commonly used complex formulas to make code more efficient.
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UtilityFormulas 
{
    /// <summary>
    /// Finds the hypotenuse of a triangle using the Pythagorean theorem
    /// </summary>
    /// <param name="side1">First known side of the triangle</param>
    /// <param name="side2">Second known side of the triangle</param>
    /// <returns>The unkown side of the triangle</returns>
    public static float FindHypotenuse(float side1, float side2)
    {
        return Mathf.Sqrt((side1 * side1) + (side2 * side2));
    }

    /// <summary>
    /// Finds the remaining leg of a triangle using the Pythagorean theorem
    /// </summary>
    /// <param name="side1">Hypotenuse of the triangle</param>
    /// <param name="side2">Known side of the triangle</param>
    /// <returns>The unkown side of the triangle</returns>
    public static float FindTriangleLeg(float hypotenuse, float side)
    {
        return Mathf.Sqrt((hypotenuse * hypotenuse) - (side * side));
    }
}
