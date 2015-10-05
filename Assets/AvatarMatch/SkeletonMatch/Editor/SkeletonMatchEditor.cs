using UnityEngine;
using System.Collections;
using UnityEditor;
[CustomEditor(typeof(SkeletonMatch))]
public class SkeletonMatchEditor : Editor {

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
      
        if (GUILayout.Button("SkeletonMatchAvatarPosition"))
        {
            ((SkeletonMatch)target).SkeletonMatchAvatarPosition();
        }
    }
}
