using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(MMDRename))]
public class MMDRenameEditor : Editor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("reanme to japanese"))
        {
            ((MMDRename)target).RenameToJap();
        }
        else if (GUILayout.Button("reanme to Normal"))
        {
            ((MMDRename)target).RenameToNormal();
        }
        else if (GUILayout.Button("reanme to No Numbner"))
        {
            ((MMDRename)target).RenameToNormalNoNumber();
        }
        else if (GUILayout.Button("reanme to Eng"))
        {
            ((MMDRename)target).RenameToEng();
        }
        else if (GUILayout.Button("reanme to Maya"))
        {
            ((MMDRename)target).RenameToAdvancedSkeleton();
        }
    }

}
