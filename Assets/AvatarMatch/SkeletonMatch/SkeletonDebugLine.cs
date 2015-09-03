using UnityEngine;
using System.Collections;

public class SkeletonDebugLine : MonoBehaviour {

    Color color = Color.red;
    void DrawLine(Transform p)
    {
     for (int i = 0; i < p.childCount; i++)
			{
			 DrawLine(p.GetChild(i));
			}
     if (p.parent)
     {
         Gizmos.color = color;
         if (color == Color.red)
         {
             color = Color.gray;
         }
         else
         {
             color = Color.red;
         }
         Gizmos.DrawLine(p.parent.position, p.position);
     }
         
    }
    void OnDrawGizmos()
    {
        DrawLine(transform);
    }
}
