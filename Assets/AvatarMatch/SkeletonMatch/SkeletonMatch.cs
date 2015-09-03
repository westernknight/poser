using UnityEngine;
using System.Collections;

public class SkeletonMatch : MonoBehaviour
{

    // Use this for initialization
    public Transform body_Hips;
    public Transform body_Spine;
    public Transform body_Chest;

    public Transform rightArm_Shoulder;
    public Transform rightArm_UpperArm;
    public Transform rightArm_LowerArm;
    public Transform rightArm_Hand;

    public Transform rightLeg_UpperLeg;
    public Transform rightLeg_LowerLeg;
    public Transform rightLeg_Foot;
    public Transform rightLeg_Toes;

    public Transform head_Neck;
    public Transform head_Head;
    public Transform head_RightEye;

    public Transform rightHand_ThumbProximal;
    public Transform rightHand_ThumbIntermediate;
    public Transform rightHand_ThumbDistal;
    public Transform rightHand_ThumbTip;
    public Transform rightHand_IndexProximal;
    public Transform rightHand_IndexIntermediate;
    public Transform rightHand_IndexDistal;
    public Transform rightHand_IndexTip;
    public Transform rightHand_MiddleProximal;
    public Transform rightHand_MiddleIntermediate;
    public Transform rightHand_MiddleDistal;
    public Transform rightHand_MiddleTip;
    public Transform rightHand_RingProximal;
    public Transform rightHand_RingIntermediate;
    public Transform rightHand_RingDistal;
    public Transform rightHand_RingTip;
    public Transform rightHand_LittleProximal;
    public Transform rightHand_LittleIntermediate;
    public Transform rightHand_LittleDistal;
    public Transform rightHand_LittleTip;

    public AvatarMatch avatar;
    public void AutoDetectReferences()
    {
        body_Hips = DetectReferencesByNaming(transform, "Root");
        body_Spine = DetectReferencesByNaming(transform, "Spine1");
        body_Chest = DetectReferencesByNaming(transform, "Chest");
        head_Neck = DetectReferencesByNaming(transform, "Neck");
        head_Head = DetectReferencesByNaming(transform, "Head");
        head_RightEye = DetectReferencesByNaming(transform, "Eye");

        rightArm_Shoulder = DetectReferencesByNaming(transform, "Scapula");
        rightArm_UpperArm = DetectReferencesByNaming(transform, "Shoulder");
        rightArm_LowerArm = DetectReferencesByNaming(transform, "Elbow");
        rightArm_Hand = DetectReferencesByNaming(transform, "Wrist");

        rightLeg_UpperLeg = DetectReferencesByNaming(transform, "Hip");
        rightLeg_LowerLeg = DetectReferencesByNaming(transform, "Knee");
        rightLeg_Foot = DetectReferencesByNaming(transform, "Ankle");
        rightLeg_Toes = DetectReferencesByNaming(transform, "Toes");


        rightHand_ThumbProximal = DetectReferencesByNaming(transform, "ThumbFinger1");
        rightHand_ThumbIntermediate = DetectReferencesByNaming(transform, "ThumbFinger2");
        rightHand_ThumbDistal = DetectReferencesByNaming(transform, "ThumbFinger3");
        rightHand_ThumbTip = DetectReferencesByNaming(transform, "ThumbFinger4");

        rightHand_IndexProximal = DetectReferencesByNaming(transform, "IndexFinger1");
        rightHand_IndexIntermediate = DetectReferencesByNaming(transform, "IndexFinger2");
        rightHand_IndexDistal = DetectReferencesByNaming(transform, "IndexFinger3");
        rightHand_IndexTip = DetectReferencesByNaming(transform, "IndexFinger4");

        rightHand_MiddleProximal = DetectReferencesByNaming(transform, "MiddleFinger1");
        rightHand_MiddleIntermediate = DetectReferencesByNaming(transform, "MiddleFinger2");
        rightHand_MiddleDistal = DetectReferencesByNaming(transform, "MiddleFinger3");
        rightHand_MiddleTip = DetectReferencesByNaming(transform, "MiddleFinger4");

        rightHand_RingProximal = DetectReferencesByNaming(transform, "RingFinger1");
        rightHand_RingIntermediate = DetectReferencesByNaming(transform, "RingFinger2");
        rightHand_RingDistal = DetectReferencesByNaming(transform, "RingFinger3");
        rightHand_RingTip = DetectReferencesByNaming(transform, "RingFinger4");

        rightHand_LittleProximal = DetectReferencesByNaming(transform, "PinkyFinger1");
        rightHand_LittleIntermediate = DetectReferencesByNaming(transform, "PinkyFinger2");
        rightHand_LittleDistal = DetectReferencesByNaming(transform, "PinkyFinger3");
        rightHand_LittleTip = DetectReferencesByNaming(transform, "PinkyFinger4");

    }

    public void SkeletonMatchAvatarPosition()
    {
        if (avatar)
        {
            body_Hips.position= avatar.body_Hips.position;
            body_Spine.position= avatar.body_Spine.position;
            body_Chest.position= avatar.body_Chest.position;

            rightArm_Shoulder.position= avatar.rightArm_Shoulder.position;
            rightArm_UpperArm.position= avatar.rightArm_UpperArm.position;
            rightArm_LowerArm.position= avatar.rightArm_LowerArm.position;
            rightArm_Hand.position= avatar.rightArm_Hand.position;

            rightLeg_UpperLeg.position= avatar.rightLeg_UpperLeg.position;
            rightLeg_LowerLeg.position= avatar.rightLeg_LowerLeg.position;
            rightLeg_Foot.position= avatar.rightLeg_Foot.position;
            rightLeg_Toes.position= avatar.rightLeg_Toes.position;

            head_Neck.position= avatar.head_Neck.position;
            head_Head.position= avatar.head_Head.position;
            head_RightEye.position = avatar.head_RightEye.position;

            rightHand_ThumbProximal.position= avatar.rightHand_ThumbProximal.position;
            rightHand_ThumbIntermediate.position= avatar.rightHand_ThumbIntermediate.position;
            rightHand_ThumbDistal.position= avatar.rightHand_ThumbDistal.position;
            rightHand_ThumbTip.position= avatar.rightHand_ThumbTip.position;
            rightHand_IndexProximal.position= avatar.rightHand_IndexProximal.position;
            rightHand_IndexIntermediate.position= avatar.rightHand_IndexIntermediate.position;
            rightHand_IndexDistal.position= avatar.rightHand_IndexDistal.position;
            rightHand_IndexTip.position= avatar.rightHand_IndexTip.position;
            rightHand_MiddleProximal.position= avatar.rightHand_MiddleProximal.position;
            rightHand_MiddleIntermediate.position= avatar.rightHand_MiddleIntermediate.position;
            rightHand_MiddleDistal.position= avatar.rightHand_MiddleDistal.position;
            rightHand_MiddleTip.position= avatar.rightHand_MiddleTip.position;
            rightHand_RingProximal.position= avatar.rightHand_RingProximal.position;
            rightHand_RingIntermediate.position= avatar.rightHand_RingIntermediate.position;
            rightHand_RingDistal.position= avatar.rightHand_RingDistal.position;
            rightHand_RingTip.position= avatar.rightHand_RingTip.position;
            rightHand_LittleProximal.position= avatar.rightHand_LittleProximal.position;
            rightHand_LittleIntermediate.position= avatar.rightHand_LittleIntermediate.position;
            rightHand_LittleDistal.position= avatar.rightHand_LittleDistal.position;
            rightHand_LittleTip.position= avatar.rightHand_LittleTip.position;
        }
        else
        {
            Debug.Log("avatar is null");
        }
    }
    Transform DetectReferencesByNaming(Transform t, string name)
    {

        if (t.name == name)
        {
            return t;
        }
        for (int i = 0; i < t.childCount; i++)
        {
            Transform tmp = DetectReferencesByNaming(t.GetChild(i), name);
            if (tmp)
            {
                return tmp;
            }
        }
        return null;
    }
}
