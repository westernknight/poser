using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
public class SkeletonPositionSaver : MonoBehaviour
{


    public void Save(string filename)
    {



        SkeletonMatch am = gameObject.GetComponent<SkeletonMatch>();
        if (am)
        {
            FileInfo fi = new FileInfo(filename);
            StreamWriter sw = new StreamWriter(fi.Create());

            List<Transform> lt = new List<Transform>();
            lt.Add(am.body_Hips);
            lt.Add(am.body_Spine);
            lt.Add(am.body_Chest);

            lt.Add(am.rightArm_Shoulder);
            lt.Add(am.rightArm_UpperArm);
            lt.Add(am.rightArm_LowerArm);
            lt.Add(am.rightArm_Hand);
            lt.Add(am.rightLeg_UpperLeg);
            lt.Add(am.rightLeg_LowerLeg);
            lt.Add(am.rightLeg_Foot);
            lt.Add(am.rightLeg_Toes);
            lt.Add(am.head_Neck);
            lt.Add(am.head_Head);
            lt.Add(am.head_RightEye);

            lt.Add(am.rightHand_ThumbProximal);
            lt.Add(am.rightHand_ThumbIntermediate);
            lt.Add(am.rightHand_ThumbDistal);
            lt.Add(am.rightHand_ThumbTip);
            lt.Add(am.rightHand_IndexProximal);
            lt.Add(am.rightHand_IndexIntermediate);
            lt.Add(am.rightHand_IndexDistal);
            lt.Add(am.rightHand_IndexTip);
            lt.Add(am.rightHand_MiddleProximal);
            lt.Add(am.rightHand_MiddleIntermediate);
            lt.Add(am.rightHand_MiddleDistal);
            lt.Add(am.rightHand_MiddleTip);
            lt.Add(am.rightHand_RingProximal);
            lt.Add(am.rightHand_RingIntermediate);
            lt.Add(am.rightHand_RingDistal);
            lt.Add(am.rightHand_RingTip);
            lt.Add(am.rightHand_LittleProximal);
            lt.Add(am.rightHand_LittleIntermediate);
            lt.Add(am.rightHand_LittleDistal);
            lt.Add(am.rightHand_LittleTip);


            foreach (var item in lt)
            {
                if (item)
                {
                    sw.WriteLine(item.gameObject.name);
                    Vector3 vec = item.localPosition;
                    vec.x = -vec.x;
                    string tmp = vec.ToString("f4");
                    tmp = tmp.Substring(1, tmp.Length - 2);
                    Debug.Log(tmp);
                    sw.WriteLine(tmp);
                }

            }
            sw.Close();
        }
        else
        {
            Debug.Log("haven't got AvatarMatch.");
        }

    }


}
/*
 * maya读取文档的脚本
 * 
 * 第一步是载入骨架
 * 然后选择骨架最顶端节点
 * 运行脚本
 proc int CheckMatch(string $a,string $matchString,vector $pos)
{
    string $children[] = `listRelatives -children $a`;
    string $i;
    string $matchName =match( $matchString, $a);
    
    if(size($matchName)>0)
    {
        setAttr -l false ($matchName+".tx");
        setAttr -l false ($matchName+".ty");
        setAttr -l false ($matchName+".tz");
        setAttr($matchName+".tx",$pos.x);
        setAttr($matchName+".ty",$pos.y);
        setAttr($matchName+".tz",$pos.z);
        
        return true;
    }
   for( $i in $children)
   {
            int $result =      CheckMatch($i,$matchString,$pos);   
            if($result == true)
            {
                return true;
            }
   }
    return false;
}

string $sel[] = `ls -sl`;

string $fileName = "G:\\Unity\\poser\\skeleton.txt";
$fileId=fopen($fileName,"r"); 

string $nextLine = `fgetline $fileId`;




while ( size( $nextLine ) > 0 ) 
{
    int $count = size($nextLine)-1;
    $nextLine = `substring $nextLine 1 $count`;
    $nextPos = `fgetline $fileId`;
    vector $pos = $nextPos;

    CheckMatch($sel[0],$nextLine,$pos);
    $nextLine = `fgetline $fileId`;
}
fclose $fileId;
*/