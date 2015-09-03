using UnityEngine;
using System.Collections;

public class SkeletonDebugLine : MonoBehaviour {

    void DrawLine(Transform p)
    {
     for (int i = 0; i < p.childCount; i++)
			{
			 DrawLine(p.GetChild(i));
			}
     if (p.parent)
     {
         Gizmos.DrawLine(p.parent.position, p.position);
     }
         
    }
    void OnDrawGizmos()
    {
        DrawLine(transform);
    }
}
