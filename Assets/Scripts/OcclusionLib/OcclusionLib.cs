using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OcclusionLib
{
    public static bool CheckIfInFOV(Vector3 targetPosition, Transform referencePoint, float FOV)
    {
        Vector3 dirToTarget = (targetPosition - referencePoint.position).normalized;
        if (Vector3.Angle(referencePoint.forward, dirToTarget) < FOV / 2)
        {
            return true;
        }
        return false;
    }

    public static bool CheckIfVisible(Collider target, Transform referencePoint, float FOV, LayerMask occlusionMask)
    {
        if(CheckIfInFOV(target.transform.position, referencePoint, FOV))
            return !CheckIfOccluded(target.gameObject.GetComponent<Collider>(), referencePoint, occlusionMask);
        return false;
    }

    public static bool CheckIfOccluded(Collider target, Transform referencePoint, LayerMask occlusionMask)
    {
        RaycastHit hitCollider;
        var dir = target.transform.position - referencePoint.position;
        bool hit = Physics.Raycast(referencePoint.position, dir, out hitCollider, dir.magnitude, occlusionMask);
        return !(hit && hitCollider.collider == target);
    }
    
    public static bool CheckIfVisiblePrecise(Vector3 targetPoint, Transform referencePoint, Collider target, float FOV, LayerMask occlusionMask)
    {
        if (CheckIfInFOV(targetPoint, referencePoint, FOV))
            return !CheckIfOccludedPrecise(targetPoint, referencePoint, target, occlusionMask);
        return false;
    }

    public static bool CheckIfOccludedPrecise(Vector3 targetPoint, Transform referencePoint, Collider target, LayerMask occlusionMask)
    {
        RaycastHit hitCollider;
        var dir = targetPoint - referencePoint.position;
        bool hit = Physics.Raycast(referencePoint.position, dir, out hitCollider, dir.magnitude, occlusionMask);
        return !(hit && hitCollider.collider == target);
    }

    public static float GetDistance(Transform point1, Transform point2) {
        return Vector3.Distance(point1.position, point2.position);
    }

   
}
