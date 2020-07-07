using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
//using System.Numerics;

public class LibUsage : MonoBehaviour
{
    public GameObject target;
    public GameObject refPoint;
    [Range(0f, 360f)] public float FOVAngle;
    [SerializeField] private LayerMask occlusionMask;
    public Text StatusText;
    public bool precise = false;
    [Min(1)]public int FOVResolution =1;
    private Collider targetCollider;

    public void DrawFOV(bool precise)
    {
        Vector3 viewAngleA = DirFromAngle(-FOVAngle / 2, false);
        Vector3 viewAngleB = DirFromAngle(FOVAngle / 2, false);
        Debug.DrawLine(refPoint.transform.position, refPoint.transform.position + viewAngleA * OcclusionLib.GetDistance(target.transform, refPoint.transform));
        Debug.DrawLine(refPoint.transform.position, refPoint.transform.position + viewAngleB * OcclusionLib.GetDistance(target.transform, refPoint.transform));
        if (precise)
            foreach (Vector3 point in CalculateRayPoints(FOVResolution))
            {
                if (!OcclusionLib.CheckIfVisiblePrecise(point, refPoint.transform, targetCollider, FOVAngle, occlusionMask))
                    Debug.DrawLine(refPoint.transform.position, point, Color.red);
                else
                    Debug.DrawLine(refPoint.transform.position, point, Color.green);
            }
        else
        {
            if (OcclusionLib.CheckIfVisible(targetCollider, refPoint.transform, FOVAngle, occlusionMask))
                Debug.DrawLine(refPoint.transform.position, target.transform.position, Color.green);
            else
                Debug.DrawLine(refPoint.transform.position, target.transform.position, Color.red);
        }
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += refPoint.transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
    
    public Vector3[] CalculateRayPoints(int resolution)
    {
        Vector3[] result = new Vector3[resolution];
        Vector3 test = targetCollider.bounds.max - targetCollider.bounds.min;
        test = Vector3.Normalize(test);
        float distance = Vector3.Distance(targetCollider.bounds.max, targetCollider.bounds.min);
        float modifier = distance / resolution;
        for(int i=0; i<resolution; i++)
        {
            if(i%2==0)
                result[i] = targetCollider.bounds.center + (modifier/2 * test * i);
            else
                result[i] = targetCollider.bounds.center - (modifier/2 * test * i);
        }
        return result;

    }

    private void Start()
    {
        targetCollider = target.GetComponent<Collider>();
    }
    void Update()
    {
        DrawFOV(precise);
        StatusText.text = "Distance: " + OcclusionLib.GetDistance(target.transform, refPoint.transform) + "\n";
        StatusText.text +="CheckIfVisible: "+ OcclusionLib.CheckIfVisible(targetCollider, refPoint.transform, FOVAngle, occlusionMask).ToString()+"\n";
        StatusText.text +="CheckIfInFOV: "+ OcclusionLib.CheckIfInFOV(target.transform.position, refPoint.transform, FOVAngle).ToString()+"\n";
        StatusText.text +="CheckIfOccluded: "+ OcclusionLib.CheckIfOccluded(targetCollider, refPoint.transform, occlusionMask).ToString()+"\n";
    }
}

