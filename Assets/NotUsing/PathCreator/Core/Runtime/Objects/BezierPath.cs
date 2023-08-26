using System.Collections.Generic;
using System.Linq;
using PathCreation.Utility;
using UnityEngine;

namespace PathCreation {
    /// A bezier path is a path made by stitching together any number of (cubic) bezier curves.
    /// A single cubic bezier curve is defined by 4 Points: anchor1, control1, control2, anchor2
    /// The curve moves between the 2 anchors, and the shape of the curve is affected by the positions of the 2 control Points

    /// When two curves are stitched together, they share an anchor Point (end anchor of curve 1 = start anchor of curve 2).
    /// So while one curve alone consists of 4 Points, two curves are defined by 7 unique Points.

    /// Apart from storing the Points, this class also provides methods for working with the path.
    /// For example, adding, inserting, and deleting Points.

    [System.Serializable]
    public class BezierPath {
        public event System.Action OnModified;
        public enum ControlMode { Aligned, Mirrored, Free, Automatic };

 #region Fields

 [SerializeField, HideInInspector]
 List<Vector3> Points;
 [SerializeField, HideInInspector]
 bool isClosed;
 [SerializeField, HideInInspector]
 PathSpace space;
 [SerializeField, HideInInspector]
 ControlMode controlMode;
 [SerializeField, HideInInspector]
 float autoControlLength = .3f;
 [SerializeField, HideInInspector]
 bool boundsUpToDate;
 [SerializeField, HideInInspector]
 Bounds bounds;

 // Normals settings
 [SerializeField, HideInInspector]
 List<float> perAnchorNormalsAngle;
 [SerializeField, HideInInspector]
 float globalNormalsAngle;
 [SerializeField, HideInInspector]
 bool flipNormals;

 #endregion

 #region Constructors

 /// <summary> Creates a two-anchor path centred around the given centre Point </summary>
 ///<param name="isClosed"> Should the end Point connect back to the start Point? </param>
 ///<param name="space"> Determines if the path is in 3d space, or clamped to the xy/xz plane </param>
 public BezierPath (Vector3 centre, bool isClosed = false, PathSpace space = PathSpace.xyz) {

 Vector3 dir = (space == PathSpace.xz) ? Vector3.forward : Vector3.up;
 float width = 2;
 float controlHeight = .5f;
 float controlWidth = 1f;
 Points = new List<Vector3> {
 centre + Vector3.left * width,
 centre + Vector3.left * controlWidth + dir * controlHeight,
 centre + Vector3.right * controlWidth - dir * controlHeight,
 centre + Vector3.right * width
 };

 perAnchorNormalsAngle = new List<float> () { 0, 0 };

 Space = space;
 IsClosed = isClosed;
        }

        /// <summary> Creates a path from the supplied 3D Points </summary>
        ///<param name="Points"> List or array of Points to create the path from. </param>
        ///<param name="isClosed"> Should the end Point connect back to the start Point? </param>
        ///<param name="space"> Determines if the path is in 3d space, or clamped to the xy/xz plane </param>
        public BezierPath (IEnumerable<Vector3> Points, bool isClosed = false, PathSpace space = PathSpace.xyz) {
            Vector3[] PointsArray = Points.ToArray ();

            if (PointsArray.Length < 2) {
                Debug.LogError ("Path requires at least 2 anchor Points.");
            } else {
                controlMode = ControlMode.Automatic;
                this.Points = new List<Vector3> { PointsArray[0], Vector3.zero, Vector3.zero, PointsArray[1] };
                perAnchorNormalsAngle = new List<float> (new float[] { 0, 0 });

                for (int i = 2; i < PointsArray.Length; i++) {
                    AddSegmentToEnd (PointsArray[i]);
                    perAnchorNormalsAngle.Add (0);
                }
            }

            this.Space = space;
            this.IsClosed = isClosed;
        }

        /// <summary> Creates a path from the positions of the supplied 2D Points </summary>
        ///<param name="transforms"> List or array of transforms to create the path from. </param>
        ///<param name="isClosed"> Should the end Point connect back to the start Point? </param>
        ///<param name="space"> Determines if the path is in 3d space, or clamped to the xy/xz plane </param>
        public BezierPath (IEnumerable<Vector2> transforms, bool isClosed = false, PathSpace space = PathSpace.xy):
            this (transforms.Select (p => new Vector3 (p.x, p.y)), isClosed, space) { }

        /// <summary> Creates a path from the positions of the supplied transforms </summary>
        ///<param name="transforms"> List or array of transforms to create the path from. </param>
        ///<param name="isClosed"> Should the end Point connect back to the start Point? </param>
        ///<param name="space"> Determines if the path is in 3d space, or clamped to the xy/xz plane </param>
        public BezierPath (IEnumerable<Transform> transforms, bool isClosed = false, PathSpace space = PathSpace.xy):
            this (transforms.Select (t => t.position), isClosed, space) { }

        /// <summary> Creates a path from the supplied 2D Points </summary>
        ///<param name="Points"> List or array of 2d Points to create the path from. </param>
        ///<param name="isClosed"> Should the end Point connect back to the start Point? </param>
        ///<param name="pathSpace"> Determines if the path is in 3d space, or clamped to the xy/xz plane </param>
        public BezierPath (IEnumerable<Vector2> Points, PathSpace space = PathSpace.xyz, bool isClosed = false):
            this (Points.Select (p => new Vector3 (p.x, p.y)), isClosed, space) { }

        #endregion

        #region Public methods and accessors

        /// Get world space position of Point
        public Vector3 this [int i] {
            get {
                return GetPoint (i);
            }
        }

        /// Get world space position of Point
        public Vector3 GetPoint (int i) {
            return Points[i];
        }

        /// Get world space position of Point
        public void SetPoint (int i, Vector3 localPosition, bool suppressPathModifiedEvent = false) {
            Points[i] = localPosition;
            if (!suppressPathModifiedEvent) {
                NotifyPathModified();
            }
        }

        /// Total number of Points in the path (anchors and controls)
        public int NumPoints {
            get {
                return Points.Count;
            }
        }

        /// Number of anchor Points making up the path
        public int NumAnchorPoints {
            get {
                return (IsClosed) ? Points.Count / 3 : (Points.Count + 2) / 3;
            }
        }

        /// Number of bezier curves making up this path
        public int NumSegments {
            get {
                return Points.Count / 3;
            }
        }

        /// Path can exist in 3D (xyz), 2D (xy), or Top-Down (xz) space
        /// In xy or xz space, Points will be clamped to that plane (so in a 2D path, for example, Points will always be at 0 on z axis)
        public PathSpace Space {
            get {
                return space;
            }
            set {
                if (value != space) {
                    PathSpace previousSpace = space;
                    space = value;
                    UpdateToNewPathSpace (previousSpace);
                }
            }
        }

        /// If closed, path will loop back from end Point to start Point
        public bool IsClosed {
            get {
                return isClosed;
            }
            set {
                if (isClosed != value) {
                    isClosed = value;
                    UpdateClosedState ();
                }
            }
        }

        /// The control mode determines the behaviour of control Points.
        /// Possible modes are:
        /// Aligned = controls stay in straight line around their anchor
        /// Mirrored = controls stay in straight, equidistant line around their anchor
        /// Free = no constraints (use this if sharp corners are needed)
        /// Automatic = controls placed automatically to try make the path smooth
        public ControlMode ControlPointMode {
            get {
                return controlMode;
            }
            set {
                if (controlMode != value) {
                    controlMode = value;
                    if (controlMode == ControlMode.Automatic) {
                        AutoSetAllControlPoints ();
                        NotifyPathModified ();
                    }
                }
            }
        }

        /// When using automatic control Point placement, this value scales how far apart controls are placed
        public float AutoControlLength {
            get {
                return autoControlLength;
            }
            set {
                value = Mathf.Max (value, .01f);
                if (autoControlLength != value) {
                    autoControlLength = value;
                    AutoSetAllControlPoints ();
                    NotifyPathModified ();
                }
            }
        }

        /// Add new anchor Point to end of the path
        public void AddSegmentToEnd (Vector3 anchorPos) {
            if (isClosed) {
                return;
            }

            int lastAnchorIndex = Points.Count - 1;
            // Set position for new control to be mirror of its counterpart
            Vector3 secondControlForOldLastAnchorOffset = (Points[lastAnchorIndex] - Points[lastAnchorIndex - 1]);
            if (controlMode != ControlMode.Mirrored && controlMode != ControlMode.Automatic) {
                // Set position for new control to be aligned with its counterpart, but with a length of half the distance from prev to new anchor
                float dstPrevToNewAnchor = (Points[lastAnchorIndex] - anchorPos).magnitude;
                secondControlForOldLastAnchorOffset = (Points[lastAnchorIndex] - Points[lastAnchorIndex - 1]).normalized * dstPrevToNewAnchor * .5f;
            }
            Vector3 secondControlForOldLastAnchor = Points[lastAnchorIndex] + secondControlForOldLastAnchorOffset;
            Vector3 controlForNewAnchor = (anchorPos + secondControlForOldLastAnchor) * .5f;

            Points.Add (secondControlForOldLastAnchor);
            Points.Add (controlForNewAnchor);
            Points.Add (anchorPos);
            perAnchorNormalsAngle.Add (perAnchorNormalsAngle[perAnchorNormalsAngle.Count - 1]);

            if (controlMode == ControlMode.Automatic) {
                AutoSetAllAffectedControlPoints (Points.Count - 1);
            }

            NotifyPathModified ();
        }

        /// Add new anchor Point to start of the path
        public void AddSegmentToStart (Vector3 anchorPos) {
            if (isClosed) {
                return;
            }

            // Set position for new control to be mirror of its counterpart
            Vector3 secondControlForOldFirstAnchorOffset = (Points[0] - Points[1]);
            if (controlMode != ControlMode.Mirrored && controlMode != ControlMode.Automatic) {
                // Set position for new control to be aligned with its counterpart, but with a length of half the distance from prev to new anchor
                float dstPrevToNewAnchor = (Points[0] - anchorPos).magnitude;
                secondControlForOldFirstAnchorOffset = secondControlForOldFirstAnchorOffset.normalized * dstPrevToNewAnchor * .5f;
            }

            Vector3 secondControlForOldFirstAnchor = Points[0] + secondControlForOldFirstAnchorOffset;
            Vector3 controlForNewAnchor = (anchorPos + secondControlForOldFirstAnchor) * .5f;
            Points.Insert (0, anchorPos);
            Points.Insert (1, controlForNewAnchor);
            Points.Insert (2, secondControlForOldFirstAnchor);
            perAnchorNormalsAngle.Insert (0, perAnchorNormalsAngle[0]);

            if (controlMode == ControlMode.Automatic) {
                AutoSetAllAffectedControlPoints (0);
            }
            NotifyPathModified ();
        }

        /// Insert new anchor Point at given position. Automatically place control Points around it so as to keep shape of curve the same
        public void SplitSegment (Vector3 anchorPos, int segmentIndex, float splitTime) {
            splitTime = Mathf.Clamp01 (splitTime);

            if (controlMode == ControlMode.Automatic) {
                Points.InsertRange (segmentIndex * 3 + 2, new Vector3[] { Vector3.zero, anchorPos, Vector3.zero });
                AutoSetAllAffectedControlPoints (segmentIndex * 3 + 3);
            } else {
                // Split the curve to find where control Points can be inserted to least affect shape of curve
                // Curve will probably be deformed slightly since splitTime is only an estimate (for performance reasons, and so doesn't correspond exactly with anchorPos)
                Vector3[][] splitSegment = CubicBezierUtility.SplitCurve (GetPointsInSegment (segmentIndex), splitTime);
                Points.InsertRange (segmentIndex * 3 + 2, new Vector3[] { splitSegment[0][2], splitSegment[1][0], splitSegment[1][1] });
                int newAnchorIndex = segmentIndex * 3 + 3;
                MovePoint (newAnchorIndex - 2, splitSegment[0][1], true);
                MovePoint (newAnchorIndex + 2, splitSegment[1][2], true);
                MovePoint (newAnchorIndex, anchorPos, true);

                if (controlMode == ControlMode.Mirrored) {
                    float avgDst = ((splitSegment[0][2] - anchorPos).magnitude + (splitSegment[1][1] - anchorPos).magnitude) / 2;
                    MovePoint (newAnchorIndex + 1, anchorPos + (splitSegment[1][1] - anchorPos).normalized * avgDst, true);
                }
            }

            // Insert angle for new anchor (value should be set inbetween neighbour anchor angles)
            int newAnchorAngleIndex = (segmentIndex + 1) % perAnchorNormalsAngle.Count;
            int numAngles = perAnchorNormalsAngle.Count;
            float anglePrev = perAnchorNormalsAngle[segmentIndex];
            float angleNext = perAnchorNormalsAngle[newAnchorAngleIndex];
            float splitAngle = Mathf.LerpAngle (anglePrev, angleNext, splitTime);
            perAnchorNormalsAngle.Insert (newAnchorAngleIndex, splitAngle);

            NotifyPathModified ();
        }

        /// Delete the anchor Point at given index, as well as its associated control Points
        public void DeleteSegment (int anchorIndex) {
            // Don't delete segment if its the last one remaining (or if only two segments in a closed path)
            if (NumSegments > 2 || !isClosed && NumSegments > 1) {
                if (anchorIndex == 0) {
                    if (isClosed) {
                        Points[Points.Count - 1] = Points[2];
                    }
                    Points.RemoveRange (0, 3);
                } else if (anchorIndex == Points.Count - 1 && !isClosed) {
                    Points.RemoveRange (anchorIndex - 2, 3);
                } else {
                    Points.RemoveRange (anchorIndex - 1, 3);
                }

                perAnchorNormalsAngle.RemoveAt (anchorIndex / 3);

                if (controlMode == ControlMode.Automatic) {
                    AutoSetAllControlPoints ();
                }

                NotifyPathModified ();
            }
        }

        /// Returns an array of the 4 Points making up the segment (anchor1, control1, control2, anchor2)
        public Vector3[] GetPointsInSegment (int segmentIndex) {
            segmentIndex = Mathf.Clamp (segmentIndex, 0, NumSegments - 1);
            return new Vector3[] { this [segmentIndex * 3], this [segmentIndex * 3 + 1], this [segmentIndex * 3 + 2], this [LoopIndex (segmentIndex * 3 + 3)] };
        }

        /// Move an existing Point to a new position
        public void MovePoint (int i, Vector3 PointPos, bool suppressPathModifiedEvent = false) {

            if (space == PathSpace.xy) {
                PointPos.z = 0;
            } else if (space == PathSpace.xz) {
                PointPos.y = 0;
            }
            Vector3 deltaMove = PointPos - Points[i];
            bool isAnchorPoint = i % 3 == 0;

            // Don't process control Point if control mode is set to automatic
            if (isAnchorPoint || controlMode != ControlMode.Automatic) {
                Points[i] = PointPos;

                if (controlMode == ControlMode.Automatic) {
                    AutoSetAllAffectedControlPoints (i);
                } else {
                    // Move control Points with anchor Point
                    if (isAnchorPoint) {
                        if (i + 1 < Points.Count || isClosed) {
                            Points[LoopIndex (i + 1)] += deltaMove;
                        }
                        if (i - 1 >= 0 || isClosed) {
                            Points[LoopIndex (i - 1)] += deltaMove;
                        }
                    }
                    // If not in free control mode, then move attached control Point to be aligned/mirrored (depending on mode)
                    else if (controlMode != ControlMode.Free) {
                        bool nextPointIsAnchor = (i + 1) % 3 == 0;
                        int attachedControlIndex = (nextPointIsAnchor) ? i + 2 : i - 2;
                        int anchorIndex = (nextPointIsAnchor) ? i + 1 : i - 1;

                        if (attachedControlIndex >= 0 && attachedControlIndex < Points.Count || isClosed) {
                            float distanceFromAnchor = 0;
                            // If in aligned mode, then attached control's current distance from anchor Point should be maintained
                            if (controlMode == ControlMode.Aligned) {
                                distanceFromAnchor = (Points[LoopIndex (anchorIndex)] - Points[LoopIndex (attachedControlIndex)]).magnitude;
                            }
                            // If in mirrored mode, then both control Points should have the same distance from the anchor Point
                            else if (controlMode == ControlMode.Mirrored) {
                                distanceFromAnchor = (Points[LoopIndex (anchorIndex)] - Points[i]).magnitude;

                            }
                            Vector3 dir = (Points[LoopIndex (anchorIndex)] - PointPos).normalized;
                            Points[LoopIndex (attachedControlIndex)] = Points[LoopIndex (anchorIndex)] + dir * distanceFromAnchor;
                        }
                    }
                }

                if (!suppressPathModifiedEvent) {
                    NotifyPathModified ();
                }
            }
        }

        /// Update the bounding box of the path
        public Bounds CalculateBoundsWithTransform (Transform transform) {
            // Loop through all segments and keep track of the minmax Points of all their bounding boxes
            MinMax3D minMax = new MinMax3D ();

            for (int i = 0; i < NumSegments; i++) {
                Vector3[] p = GetPointsInSegment (i);
                for (int j = 0; j < p.Length; j++) {
                    p[j] = MathUtility.TransformPoint (p[j], transform, space);
                }

                minMax.AddValue (p[0]);
                minMax.AddValue (p[3]);

                List<float> extremePointTimes = CubicBezierUtility.ExtremePointTimes (p[0], p[1], p[2], p[3]);
                foreach (float t in extremePointTimes) {
                    minMax.AddValue (CubicBezierUtility.EvaluateCurve (p, t));
                }
            }

            return new Bounds ((minMax.Min + minMax.Max) / 2, minMax.Max - minMax.Min);
        }

        /// Flip the normal vectors 180 degrees
        public bool FlipNormals {
            get {
                return flipNormals;
            }
            set {
                if (flipNormals != value) {
                    flipNormals = value;
                    NotifyPathModified ();
                }
            }
        }

        /// Global angle that all normal vectors are rotated by (only relevant for paths in 3D space)
        public float GlobalNormalsAngle {
            get {
                return globalNormalsAngle;
            }
            set {
                if (value != globalNormalsAngle) {
                    globalNormalsAngle = value;
                    NotifyPathModified ();
                }
            }
        }

        /// Get the desired angle of the normal vector at a particular anchor (only relevant for paths in 3D space)
        public float GetAnchorNormalAngle (int anchorIndex) {
            return perAnchorNormalsAngle[anchorIndex] % 360;
        }

        /// Set the desired angle of the normal vector at a particular anchor (only relevant for paths in 3D space)
        public void SetAnchorNormalAngle (int anchorIndex, float angle) {
            angle = (angle + 360) % 360;
            if (perAnchorNormalsAngle[anchorIndex] != angle) {
                perAnchorNormalsAngle[anchorIndex] = angle;
                NotifyPathModified ();
            }
        }

        /// Reset global and anchor normal angles to 0
        public void ResetNormalAngles () {
            for (int i = 0; i < perAnchorNormalsAngle.Count; i++) {
                perAnchorNormalsAngle[i] = 0;
            }
            globalNormalsAngle = 0;
            NotifyPathModified ();
        }

        /// Bounding box containing the path
        public Bounds PathBounds {
            get {
                if (!boundsUpToDate) {
                    UpdateBounds ();
                }
                return bounds;
            }
        }

        #endregion

        #region Internal methods and accessors

        /// Update the bounding box of the path
        void UpdateBounds () {
            if (boundsUpToDate) {
                return;
            }

            // Loop through all segments and keep track of the minmax Points of all their bounding boxes
            MinMax3D minMax = new MinMax3D ();

            for (int i = 0; i < NumSegments; i++) {
                Vector3[] p = GetPointsInSegment (i);
                minMax.AddValue (p[0]);
                minMax.AddValue (p[3]);

                List<float> extremePointTimes = CubicBezierUtility.ExtremePointTimes (p[0], p[1], p[2], p[3]);
                foreach (float t in extremePointTimes) {
                    minMax.AddValue (CubicBezierUtility.EvaluateCurve (p, t));
                }
            }

            boundsUpToDate = true;
            bounds = new Bounds ((minMax.Min + minMax.Max) / 2, minMax.Max - minMax.Min);
        }

        /// Determines good positions (for a smooth path) for the control Points affected by a moved/inserted anchor Point
        void AutoSetAllAffectedControlPoints (int updatedAnchorIndex) {
            for (int i = updatedAnchorIndex - 3; i <= updatedAnchorIndex + 3; i += 3) {
                if (i >= 0 && i < Points.Count || isClosed) {
                    AutoSetAnchorControlPoints (LoopIndex (i));
                }
            }

            AutoSetStartAndEndControls ();
        }

        /// Determines good positions (for a smooth path) for all control Points
        void AutoSetAllControlPoints () {
            if (NumAnchorPoints > 2) {
                for (int i = 0; i < Points.Count; i += 3) {
                    AutoSetAnchorControlPoints (i);
                }
            }

            AutoSetStartAndEndControls ();
        }

        /// Calculates good positions (to result in smooth path) for the controls around specified anchor
        void AutoSetAnchorControlPoints (int anchorIndex) {
            // Calculate a vector that is perpendicular to the vector bisecting the angle between this anchor and its two immediate neighbours
            // The control Points will be placed along that vector
            Vector3 anchorPos = Points[anchorIndex];
            Vector3 dir = Vector3.zero;
            float[] neighbourDistances = new float[2];

            if (anchorIndex - 3 >= 0 || isClosed) {
                Vector3 offset = Points[LoopIndex (anchorIndex - 3)] - anchorPos;
                dir += offset.normalized;
                neighbourDistances[0] = offset.magnitude;
            }
            if (anchorIndex + 3 >= 0 || isClosed) {
                Vector3 offset = Points[LoopIndex (anchorIndex + 3)] - anchorPos;
                dir -= offset.normalized;
                neighbourDistances[1] = -offset.magnitude;
            }

            dir.Normalize ();

            // Set the control Points along the calculated direction, with a distance proportional to the distance to the neighbouring control Point
            for (int i = 0; i < 2; i++) {
                int controlIndex = anchorIndex + i * 2 - 1;
                if (controlIndex >= 0 && controlIndex < Points.Count || isClosed) {
                    Points[LoopIndex (controlIndex)] = anchorPos + dir * neighbourDistances[i] * autoControlLength;
                }
            }
        }

        /// Determines good positions (for a smooth path) for the control Points at the start and end of a path
        void AutoSetStartAndEndControls () {
            if (isClosed) {
                // Handle case with only 2 anchor Points separately, as will otherwise result in straight line ()
                if (NumAnchorPoints == 2) {
                    Vector3 dirAnchorAToB = (Points[3] - Points[0]).normalized;
                    float dstBetweenAnchors = (Points[0] - Points[3]).magnitude;
                    Vector3 perp = Vector3.Cross (dirAnchorAToB, (space == PathSpace.xy) ? Vector3.forward : Vector3.up);
                    Points[1] = Points[0] + perp * dstBetweenAnchors / 2f;
                    Points[5] = Points[0] - perp * dstBetweenAnchors / 2f;
                    Points[2] = Points[3] + perp * dstBetweenAnchors / 2f;
                    Points[4] = Points[3] - perp * dstBetweenAnchors / 2f;

                } else {
                    AutoSetAnchorControlPoints (0);
                    AutoSetAnchorControlPoints (Points.Count - 3);
                }
            } else {
                // Handle case with 2 anchor Points separately, as otherwise minor adjustments cause path to constantly flip
                if (NumAnchorPoints == 2) {
                    Points[1] = Points[0] + (Points[3] - Points[0]) * .25f;
                    Points[2] = Points[3] + (Points[0] - Points[3]) * .25f;
                } else {
                    Points[1] = (Points[0] + Points[2]) * .5f;
                    Points[Points.Count - 2] = (Points[Points.Count - 1] + Points[Points.Count - 3]) * .5f;
                }
            }
        }

        /// Update Point positions for new path space
        /// (for example, if changing from xy to xz path, y and z axes will be swapped so the path keeps its shape in the new space)
        void UpdateToNewPathSpace (PathSpace previousSpace) {
            // If changing from 3d to 2d space, first find the bounds of the 3d path.
            // The axis with the smallest bounds will be discarded.
            if (previousSpace == PathSpace.xyz) {
                Vector3 boundsSize = PathBounds.size;
                float minBoundsSize = Mathf.Min (boundsSize.x, boundsSize.y, boundsSize.z);

                for (int i = 0; i < NumPoints; i++) {
                    if (space == PathSpace.xy) {
                        float x = (minBoundsSize == boundsSize.x) ? Points[i].z : Points[i].x;
                        float y = (minBoundsSize == boundsSize.y) ? Points[i].z : Points[i].y;
                        Points[i] = new Vector3 (x, y, 0);
                    } else if (space == PathSpace.xz) {
                        float x = (minBoundsSize == boundsSize.x) ? Points[i].y : Points[i].x;
                        float z = (minBoundsSize == boundsSize.z) ? Points[i].y : Points[i].z;
                        Points[i] = new Vector3 (x, 0, z);
                    }
                }
            } else {
                // Nothing needs to change when going to 3d space
                if (space != PathSpace.xyz) {
                    for (int i = 0; i < NumPoints; i++) {
                        // from xz to xy
                        if (space == PathSpace.xy) {
                            Points[i] = new Vector3 (Points[i].x, Points[i].z, 0);
                        }
                        // from xy to xz
                        else if (space == PathSpace.xz) {
                            Points[i] = new Vector3 (Points[i].x, 0, Points[i].y);
                        }
                    }
                }
            }

            NotifyPathModified ();
        }

        /// Add/remove the extra 2 controls required for a closed path
        void UpdateClosedState () {
            if (isClosed) {
                // Set positions for new controls to mirror their counterparts
                Vector3 lastAnchorSecondControl = Points[Points.Count - 1] * 2 - Points[Points.Count - 2];
                Vector3 firstAnchorSecondControl = Points[0] * 2 - Points[1];
                if (controlMode != ControlMode.Mirrored && controlMode != ControlMode.Automatic) {
                    // Set positions for new controls to be aligned with their counterparts, but with a length of half the distance between start/end anchor
                    float dstBetweenStartAndEndAnchors = (Points[Points.Count - 1] - Points[0]).magnitude;
                    lastAnchorSecondControl = Points[Points.Count - 1] + (Points[Points.Count - 1] - Points[Points.Count - 2]).normalized * dstBetweenStartAndEndAnchors * .5f;
                    firstAnchorSecondControl = Points[0] + (Points[0] - Points[1]).normalized * dstBetweenStartAndEndAnchors * .5f;
                }
                Points.Add (lastAnchorSecondControl);
                Points.Add (firstAnchorSecondControl);
            } else {
                Points.RemoveRange (Points.Count - 2, 2);

            }

            if (controlMode == ControlMode.Automatic) {
                AutoSetStartAndEndControls ();
            }

            if (OnModified != null) {
                OnModified ();
            }
        }

        /// Loop index around to start/end of Points array if out of bounds (useful when working with closed paths)
        int LoopIndex (int i) {
            return (i + Points.Count) % Points.Count;
        }

        // Called when the path is modified
        public void NotifyPathModified () {
            boundsUpToDate = false;
            if (OnModified != null) {
                OnModified ();
            }
        }

        #endregion

    }
}