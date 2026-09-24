using UnityEngine;

/*
 * CubicBezierMath holds De Casteljau point and tangent sampling for four
 * control points. CubicBezierCurve supplies their current scene positions.
 */

public static class CubicBezierMath
{
    public static Vector3 SamplePoint(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        // TODO Slice 3.1: evaluate the cubic at t with De Casteljau. Slice 3 is on your own.
        // Check: DeCasteljauCubic_SamplesPointFromEquivalentCubicFormula passes.
        // Next: Slice 3.2 in Demo/CubicBezierCurve.cs.
        Vector3 a1 = Vector3.Lerp(p0, p1, t);
        Vector3 b1 = Vector3.Lerp(p1, p2, t);
        Vector3 c1 = Vector3.Lerp(p2, p3, t);
        
        Vector3 a2 = Vector3.Lerp(a1, b1, t);
        Vector3 b2 = Vector3.Lerp(b1, c1, t);

        Vector3 q = Vector3.Lerp(a2, b2, t);
        
        return q;
    }

    public static Vector3 SampleTangent(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        // Slice 3.5: return the cubic derivative at t from your De Casteljau
        // construction. Do not normalize it.
        // Stuck? Show ChatGPT your construction and ask: "Differentiate this with respect
        // to t using the product rule. Explain each step." Then derive it yourself.
        // Check: DeCasteljauCubic_SamplesTangentFromFinalInterpolationSegment passes.
        // Next: Slice 3.6 in Demo/CubicBezierCurve.cs.
        Vector3 sampleTangent = (-3 * p0 * (1 - t) * (1 - t)) 
                                + (3 * p1 * (3 * t * t - 4 * t + 1)) 
                                + (3 * p2 * (-3 * (t * t) + 2 * t)) 
                                + (3 * p3 * t * t);
        return sampleTangent;
    }
}
