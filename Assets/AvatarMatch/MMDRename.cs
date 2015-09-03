using UnityEngine;
using System.Collections;

public class MMDRename : MonoBehaviour
{

    public void RenameToAdvancedSkeleton()
    {
        AvatarMatch am = gameObject.GetComponent<AvatarMatch>();
        if (am)
        {
            am.body_Hips.name = "Root";
            am.body_Spine.name = "Spine1";
            am.body_Chest.name = "Chest";
            am.head_Neck.name = "Neck";
            am.head_Head.name = "Head";
            am.rightArm_Shoulder.name = "Scapula";
            am.rightArm_UpperArm.name = "Shoulder";
            am.rightArm_LowerArm.name = "Elbow";
            am.rightArm_Hand.name = "Wrist";
            am.rightHand_ThumbProximal.name = "ThumbFinger1";
            am.rightHand_ThumbIntermediate.name = "ThumbFinger2";
            am.rightHand_ThumbDistal.name = "ThumbFinger3";
            am.rightHand_ThumbTip.name = "ThumbFinger4";

            am.rightHand_IndexProximal.name = "IndexFinger1";
            am.rightHand_IndexIntermediate.name = "IndexFinger2";
            am.rightHand_IndexDistal.name = "IndexFinger3";
            am.rightHand_IndexTip.name = "IndexFinger4";

            am.rightHand_MiddleProximal.name = "MiddleFinger1";
            am.rightHand_MiddleIntermediate.name = "MiddleFinger2";
            am.rightHand_MiddleDistal.name = "MiddleFinger3";
            am.rightHand_MiddleTip.name = "MiddleFinger4";

            am.rightHand_RingProximal.name = "RingFinger1";
            am.rightHand_RingIntermediate.name = "RingFinger2";
            am.rightHand_RingDistal.name = "RingFinger3";
            am.rightHand_RingTip.name = "RingFinger4";

            am.rightHand_LittleProximal.name = "PinkyFinger1";
            am.rightHand_LittleIntermediate.name = "PinkyFinger2";
            am.rightHand_LittleDistal.name = "PinkyFinger3";
            am.rightHand_LittleTip.name = "PinkyFinger4";

            am.rightLeg_UpperLeg.name = "Hip";
            am.rightLeg_LowerLeg.name = "Knee";
            am.rightLeg_Foot.name = "Ankle";
            am.rightLeg_Toes.name = "Toes";
        }
    }
    public void RenameToJap()
    {
        RenameToJap(transform);
    }
    public void RenameToNormal()
    {
        RenameToNormal(transform);
    }
    public void RenameToNormalNoNumber()
    {
        RenameToNormalNoNumber(transform);
    }
    public void RenameToEng()
    {
        RenameToEng(transform);
    }
    void RenameToJap(Transform t)
    {
        MMD4MecanimBone bone = t.GetComponent<MMD4MecanimBone>();
        if (bone != null)
        {
            t.name = bone.boneData.nameJp;
            //Debug.Log();
        }
        for (int i = 0; i < t.childCount; i++)
        {

            RenameToJap(t.GetChild(i));
        }

    }
    void RenameToEng(Transform t)
    {
        MMD4MecanimBone bone = t.GetComponent<MMD4MecanimBone>();
        if (bone != null)
        {
            t.name = bone.boneData.nameEn;
            //Debug.Log();
        }
        for (int i = 0; i < t.childCount; i++)
        {

            RenameToEng(t.GetChild(i));
        }

    }
    void RenameToNormal(Transform t)
    {
        MMD4MecanimBone bone = t.GetComponent<MMD4MecanimBone>();
        if (bone != null)
        {
            t.name = bone.boneData.skeletonName;
            //Debug.Log();
        }
        for (int i = 0; i < t.childCount; i++)
        {

            RenameToNormal(t.GetChild(i));
        }

    }

    void RenameToNormalNoNumber(Transform t)
    {
        MMD4MecanimBone bone = t.GetComponent<MMD4MecanimBone>();
        if (bone != null)
        {

            string[] tmp = bone.boneData.skeletonName.Split('!'); 
            if (tmp.Length>1)
            {
                t.name = tmp[1];
            }    
            else
            {
                tmp = bone.boneData.skeletonName.Split('.');
                if (tmp.Length > 1)
                {
                    t.name = tmp[1];
                }  
            }
        }
        for (int i = 0; i < t.childCount; i++)
        {

            RenameToNormalNoNumber(t.GetChild(i));
        }

    }



}
