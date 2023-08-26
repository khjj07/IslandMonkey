using UnityEngine;

namespace PathCreation.Examples {
    // Example of creating a path at runtime from a set of Points.

    [RequireComponent(typeof(PathCreator))]
    public class GeneratePathExample : MonoBehaviour {

        public bool closedLoop = true;
        public Transform[] wayPoints;

        void Start () {
            if (wayPoints.Length > 0) {
                // Create a new bezier path from the wayPoints.
                BezierPath bezierPath = new BezierPath (wayPoints, closedLoop, PathSpace.xyz);
                GetComponent<PathCreator> ().bezierPath = bezierPath;
            }
        }
    }
}