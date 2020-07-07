# UnityOcclusionTest

##Overwiev
Unity occlusion/visibility library with example of usage. It's capable of working in two modes: simple and precise. Simple mode works with target position directly while precise mode operates with two points and target. More abouth that in methods description.

##Technical info
The library itself contains very basic functionality. It's supposed to be used as an Instance-free API where caller should just call it's methods when needed to minimize perfomance hit. 

##Methods
###bool CheckIfInFOV(Vector3 targetPosition, Transform referencePoint, float FOV)
returns *true* if angle between target position and reference point is within bounds of reference point's FOV
###bool CheckIfVisible(Collider target, Transform referencePoint, float FOV, LayerMask occlusionMask)
return *true* if target is detected in reference point's FOV and not obscured by obstacles specified in occlusion mask
###bool CheckIfOccluded(Collider target, Transform referencePoint, LayerMask occlusionMask)
returns *true* if target is occluded by obstacles specified in occlusion mask
###bool CheckIfVisiblePrecise(Vector3 targetPoint, Transform referencePoint, Transform target, float FOV, LayerMask occlusionMask)
returns *true* if target point is in reference point's FOV, and the target is detected between reference and target points while not being obscured
###bool CheckIfOccludedPrecise(Vector3 targetPoint, Transform referencePoint, Collider target, LayerMask occlusionMask)
returns *true* if target is detected between reference and target points while not being obscured by obstacles 