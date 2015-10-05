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
    void OnEnable()
    {
        AutoDetectReferences();
    }
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
            PositionAssignment(body_Hips, avatar.body_Hips);
            PositionAssignment(body_Spine, avatar.body_Spine);
            PositionAssignment(body_Chest, avatar.body_Chest);

            PositionAssignment(rightArm_Shoulder, avatar.rightArm_Shoulder);
            PositionAssignment(rightArm_UpperArm, avatar.rightArm_UpperArm);
            PositionAssignment(rightArm_LowerArm, avatar.rightArm_LowerArm);
            PositionAssignment(rightArm_Hand, avatar.rightArm_Hand);

            PositionAssignment(rightLeg_UpperLeg, avatar.rightLeg_UpperLeg);
            PositionAssignment(rightLeg_LowerLeg, avatar.rightLeg_LowerLeg);
            PositionAssignment(rightLeg_Foot, avatar.rightLeg_Foot);
            PositionAssignment(rightLeg_Toes, avatar.rightLeg_Toes);

            PositionAssignment(head_Neck, avatar.head_Neck);
            PositionAssignment(head_Head, avatar.head_Head);
            PositionAssignment(head_RightEye, avatar.head_RightEye);

            PositionAssignment(rightHand_ThumbProximal, avatar.rightHand_ThumbProximal);
            PositionAssignment(rightHand_ThumbIntermediate, avatar.rightHand_ThumbIntermediate);
            PositionAssignment(rightHand_ThumbDistal, avatar.rightHand_ThumbDistal);
            PositionAssignment(rightHand_ThumbTip, avatar.rightHand_ThumbTip);
            PositionAssignment(rightHand_IndexProximal, avatar.rightHand_IndexProximal);
            PositionAssignment(rightHand_IndexIntermediate, avatar.rightHand_IndexIntermediate);
            PositionAssignment(rightHand_IndexDistal, avatar.rightHand_IndexDistal);
            PositionAssignment(rightHand_IndexTip, avatar.rightHand_IndexTip);
            PositionAssignment(rightHand_MiddleProximal, avatar.rightHand_MiddleProximal);
            PositionAssignment(rightHand_MiddleIntermediate, avatar.rightHand_MiddleIntermediate);
            PositionAssignment(rightHand_MiddleDistal, avatar.rightHand_MiddleDistal);
            PositionAssignment(rightHand_MiddleTip, avatar.rightHand_MiddleTip);
            PositionAssignment(rightHand_RingProximal, avatar.rightHand_RingProximal);
            PositionAssignment(rightHand_RingIntermediate, avatar.rightHand_RingIntermediate);
            PositionAssignment(rightHand_RingDistal, avatar.rightHand_RingDistal);
            PositionAssignment(rightHand_RingTip, avatar.rightHand_RingTip);
            PositionAssignment(rightHand_LittleProximal, avatar.rightHand_LittleProximal);
            PositionAssignment(rightHand_LittleIntermediate, avatar.rightHand_LittleIntermediate);
            PositionAssignment(rightHand_LittleDistal, avatar.rightHand_LittleDistal);
            PositionAssignment(rightHand_LittleTip, avatar.rightHand_LittleTip);



        }
        else
        {
            Debug.Log("avatar is null");
        }
    }
    void PositionAssignment(Transform a, Transform b)
    {
        if (a != null && b != null)
        {
            a.position = b.position;
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
