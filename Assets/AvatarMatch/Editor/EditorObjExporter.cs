/*
Based on ObjExporter.cs, this "wrapper" lets you export to .OBJ directly from the editor menu.
 
This should be put in your "Editor"-folder. Use by selecting the objects you want to export, and select
the appropriate menu item from "Custom->Export". Exported models are put in a folder called
"ExportedObj" in the root of your Unity-project. Textures should also be copied and placed in the
same folder.
N.B. there may be a bug so if the custom option doesn't come up refer to this thread http://answers.unity3d.com/questions/317951/how-to-use-editorobjexporter-obj-saving-script-fro.html */

using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System;

struct ObjMaterial
{
    public string name;
    public string textureName;
}

public class EditorObjExporter : ScriptableObject
{

    private static int vertexOffset = 0;
    private static int normalOffset = 0;
    private static int uvOffset = 0;
    // 
    // 
    //     //User should probably be able to change this. It is currently left as an excercise for
    //     //the reader.
    private static string targetFolder = "ExportedObj";
    // 
    // 
    private static string MeshToString(Mesh mf)
    {
        Transform transform = Selection.gameObjects[0].transform;
        Mesh m = mf;


        StringBuilder sb = new StringBuilder();

        sb.Append("g ").Append(mf.name).Append("\n");
        foreach (Vector3 lv in m.vertices)
        {
       
            float factor = 1;
            //This is sort of ugly - inverting x-component since we're in
            //a different coordinate system than "everyone" is "used to".
           // sb.Append(string.Format("v {0} {1} {2}\n", -wv.x, wv.y, wv.z));
            sb.Append(string.Format("v {0} {1} {2}\n", -lv.x * factor, lv.y * factor, lv.z * factor));
        }
        sb.Append("\n");

        foreach (Vector3 lv in m.normals)
        {
            Vector3 wv = transform.TransformDirection(lv);

            sb.Append(string.Format("vn {0} {1} {2}\n", -wv.x, wv.y, wv.z));
        }
        sb.Append("\n");

        foreach (Vector3 v in m.uv)
        {
            sb.Append(string.Format("vt {0} {1}\n", v.x, v.y));
        }

        for (int material = 0; material < m.subMeshCount; material++)
        {
            sb.Append("\n");

            int[] triangles = m.GetTriangles(material);
            for (int i = 0; i < triangles.Length; i += 3)
            {
                //Because we inverted the x-component, we also needed to alter the triangle winding.
                sb.Append(string.Format("f {1}/{1}/{1} {0}/{0}/{0} {2}/{2}/{2}\n",
                    triangles[i] + 1 + vertexOffset, triangles[i + 1] + 1 + normalOffset, triangles[i + 2] + 1 + uvOffset));
            }
        }

        vertexOffset += m.vertices.Length;
        normalOffset += m.normals.Length;
        uvOffset += m.uv.Length;

        return sb.ToString();
    }
    private static void MeshesToFile(Mesh[] mf, string folder, string filename)
    {

        using (StreamWriter sw = new StreamWriter(folder + "/" + filename + ".obj"))
        {
            //sw.Write("mtllib ./" + filename + ".mtl\n");

            for (int i = 0; i < mf.Length; i++)
            {
                sw.Write(MeshToString(mf[i]));
            }
        }


    }
    // 
    private static bool CreateTargetFolder()
    {
        try
        {
            targetFolder = Selection.gameObjects[0].name;
            System.IO.Directory.CreateDirectory(targetFolder);
        }
        catch
        {
            EditorUtility.DisplayDialog("Error!", "Failed to create target folder! maybe not select an object.", "");
            return false;
        }

        return true;
    }
    [MenuItem("Custom/Export/Export to single OBJ")]
    static void ExportWholeSelectionToSingle()
    {
        if (!CreateTargetFolder())
            return;

      
        
        Transform[] selection = Selection.GetTransforms(SelectionMode.Editable | SelectionMode.ExcludePrefab);

        if (selection.Length == 0)
        {
            EditorUtility.DisplayDialog("No source object selected!", "Please select one or more target objects", "");
            return;
        }
        vertexOffset = 0;
        normalOffset = 0;
        uvOffset = 0;
        int exportedObjects = 0;

        List<Mesh> mfList = new List<Mesh>();

        for (int i = 0; i < selection.Length; i++)
        {
            MeshFilter[] meshfilter = selection[i].GetComponentsInChildren<MeshFilter>();

            for (int m = 0; m < meshfilter.Length; m++)
            {
                exportedObjects++;
                mfList.Add(meshfilter[m].mesh);
            }
            SkinnedMeshRenderer[] skinned = selection[i].GetComponentsInChildren<SkinnedMeshRenderer>();

            for (int m = 0; m < skinned.Length; m++)
            {
                Mesh mesh = new Mesh();
                skinned[m].BakeMesh(mesh);
                exportedObjects++;
                mfList.Add(mesh);
            }

        }

        if (exportedObjects > 0)
        {

     
           MeshesToFile(mfList.ToArray(), targetFolder, targetFolder);


           Debug.Log("Objects exported " + targetFolder+".");
        }
        else
            EditorUtility.DisplayDialog("Objects not exported", "Make sure at least some of your selected objects have mesh filters!", "");
    }
}