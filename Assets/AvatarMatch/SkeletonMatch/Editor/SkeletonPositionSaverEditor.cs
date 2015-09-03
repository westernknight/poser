using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(SkeletonPositionSaver))]
public class SkeletonPositionSaverEditor : Editor
{


    public override void OnInspectorGUI()
    {

        if (GUILayout.Button("save"))
        {
            
            string path = EditorUtility.SaveFilePanel("Save avatar position", "", "","txt");
            if (string.IsNullOrEmpty(path)==false)
            {
                SkeletonPositionSaver saver = target as SkeletonPositionSaver;
                saver.Save(path);
            }
            
        }
    }
	
}
