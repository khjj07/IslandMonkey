using System.Collections.Generic;
using UnityEngine;

namespace PathCreation.Utility
{
    public static class VertexPathUtility
    {

		public static PathSplitData SplitBezierPathByAngleError(BezierPath bezierPath, float maxAngleError, float minVertexDst, float accuracy)
        {
			PathSplitData splitData = new PathSplitData();

            splitData.vertices.Add(bezierPath[0]);
            splitData.tangents.Add(CubicBezierUtility.EvaluateCurveDerivative(bezierPath.GetPointsInSegment(0), 0).normalized);
            splitData.cumulativeLength.Add(0);
            splitData.anchorVertexMap.Add(0);
			splitData.minMax.AddValue(bezierPath[0]);

            Vector3 prevPointOnPath = bezierPath[0];
            Vector3 lastAddedPoint = bezierPath[0];

            float currentPathLength = 0;
            float dstSinceLastVertex = 0;

            // Go through all segments and split up into vertices
            for (int segmentIndex = 0; segmentIndex < bezierPath.NumSegments; segmentIndex++)
            {
                Vector3[] segmentPoints = bezierPath.GetPointsInSegment(segmentIndex);
                float estimatedSegmentLength = CubicBezierUtility.EstimateCurveLength(segmentPoints[0], segmentPoints[1], segmentPoints[2], segmentPoints[3]);
                int divisions = Mathf.CeilToInt(estimatedSegmentLength * accuracy);
                float increment = 1f / divisions;

                for (float t = increment; t <= 1; t += increment)
                {
                    bool isLastPointOnPath = (t + increment > 1 && segmentIndex == bezierPath.NumSegments - 1);
                    if (isLastPointOnPath)
                    {
                        t = 1;
                    }
                    Vector3 PointOnPath = CubicBezierUtility.EvaluateCurve(segmentPoints, t);
                    Vector3 nextPointOnPath = CubicBezierUtility.EvaluateCurve(segmentPoints, t + increment);

                    // angle at current Point on path
                    float localAngle = 180 - MathUtility.MinAngle(prevPointOnPath, PointOnPath, nextPointOnPath);
                    // angle between the last added vertex, the current Point on the path, and the next Point on the path
                    float angleFromPrevVertex = 180 - MathUtility.MinAngle(lastAddedPoint, PointOnPath, nextPointOnPath);
                    float angleError = Mathf.Max(localAngle, angleFromPrevVertex);


                    if ((angleError > maxAngleError && dstSinceLastVertex >= minVertexDst) || isLastPointOnPath)
                    {

                        currentPathLength += (lastAddedPoint - PointOnPath).magnitude;
                        splitData.cumulativeLength.Add(currentPathLength);
                        splitData.vertices.Add(PointOnPath);
                        splitData.tangents.Add(CubicBezierUtility.EvaluateCurveDerivative(segmentPoints, t).normalized);
						splitData.minMax.AddValue(PointOnPath);
                        dstSinceLastVertex = 0;
                        lastAddedPoint = PointOnPath;
                    }
                    else
                    {
                        dstSinceLastVertex += (PointOnPath - prevPointOnPath).magnitude;
                    }
                    prevPointOnPath = PointOnPath;
                }
                splitData.anchorVertexMap.Add(splitData.vertices.Count - 1);
            }
			return splitData;
		}

		public static PathSplitData SplitBezierPathEvenly(BezierPath bezierPath, float spacing, float accuracy)
        {
			PathSplitData splitData = new PathSplitData();

            splitData.vertices.Add(bezierPath[0]);
            splitData.tangents.Add(CubicBezierUtility.EvaluateCurveDerivative(bezierPath.GetPointsInSegment(0), 0).normalized);
            splitData.cumulativeLength.Add(0);
            splitData.anchorVertexMap.Add(0);
			splitData.minMax.AddValue(bezierPath[0]);

            Vector3 prevPointOnPath = bezierPath[0];
            Vector3 lastAddedPoint = bezierPath[0];

            float currentPathLength = 0;
            float dstSinceLastVertex = 0;

            // Go through all segments and split up into vertices
            for (int segmentIndex = 0; segmentIndex < bezierPath.NumSegments; segmentIndex++)
            {
                Vector3[] segmentPoints = bezierPath.GetPointsInSegment(segmentIndex);
                float estimatedSegmentLength = CubicBezierUtility.EstimateCurveLength(segmentPoints[0], segmentPoints[1], segmentPoints[2], segmentPoints[3]);
                int divisions = Mathf.CeilToInt(estimatedSegmentLength * accuracy);
                float increment = 1f / divisions;

                for (float t = increment; t <= 1; t += increment)
                {
                    bool isLastPointOnPath = (t + increment > 1 && segmentIndex == bezierPath.NumSegments - 1);
                    if (isLastPointOnPath)
                    {
                        t = 1;
                    }
                    Vector3 PointOnPath = CubicBezierUtility.EvaluateCurve(segmentPoints, t);
					dstSinceLastVertex += (PointOnPath - prevPointOnPath).magnitude;

					// If vertices are now too far apart, go back by amount we overshot by
					if (dstSinceLastVertex > spacing) {
						float overshootDst = dstSinceLastVertex - spacing;
						PointOnPath += (prevPointOnPath-PointOnPath).normalized * overshootDst;
						t-=increment;
					}

                    if (dstSinceLastVertex >= spacing || isLastPointOnPath)
                    {
                        currentPathLength += (lastAddedPoint - PointOnPath).magnitude;
                        splitData.cumulativeLength.Add(currentPathLength);
                        splitData.vertices.Add(PointOnPath);
                        splitData.tangents.Add(CubicBezierUtility.EvaluateCurveDerivative(segmentPoints, t).normalized);
						splitData.minMax.AddValue(PointOnPath);
                        dstSinceLastVertex = 0;
                        lastAddedPoint = PointOnPath;
                    }
                    prevPointOnPath = PointOnPath;
                }
                splitData.anchorVertexMap.Add(splitData.vertices.Count - 1);
            }
			return splitData;
		}

       
	   public class PathSplitData {
		   public List<Vector3> vertices = new List<Vector3>();
		   public List<Vector3> tangents = new List<Vector3>();
		   public List<float> cumulativeLength = new List<float>();
		   public List<int> anchorVertexMap = new List<int>();
		   public MinMax3D minMax = new MinMax3D();
	   }
    }
}