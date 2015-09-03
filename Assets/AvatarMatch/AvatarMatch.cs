#region 模块信息
/*----------------------------------------------------------------
// Copyright (C) 2015 广州，蓝弧
//
// 模块名：MatchAvatar
// 创建者：张嘉俊
// 修改者列表：
// 创建日期：#CREATIONDATE#
// 模块描述：
//----------------------------------------------------------------*/
#endregion


using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class AvatarMatch : MonoBehaviour
{
    [HideInInspector]
    public bool showStatus = false;
    /// <summary>
    /// Body
    /// </summary>
    public Transform body_Hips;
    public Transform body_Spine;
    public Transform body_Chest;
    public Transform body_LeftBreast;
    public Transform body_RightBreast;
    /// <summary>
    /// Left Arm
    /// </summary>
    public Transform leftArm_Shoulder;
    public Transform leftArm_UpperArm;
    public Transform leftArm_LowerArm;
    public Transform leftArm_Hand;

    /// <summary>
    /// Right Arm
    /// </summary>
    public Transform rightArm_Shoulder;
    public Transform rightArm_UpperArm;
    public Transform rightArm_LowerArm;
    public Transform rightArm_Hand;

    /// <summary>
    /// Left Leg
    /// </summary>
    public Transform leftLeg_UpperLeg;
    public Transform leftLeg_LowerLeg;
    public Transform leftLeg_Foot;
    public Transform leftLeg_Toes;

    /// <summary>
    /// Right Leg
    /// </summary>
    public Transform rightLeg_UpperLeg;
    public Transform rightLeg_LowerLeg;
    public Transform rightLeg_Foot;
    public Transform rightLeg_Toes;

    /// <summary>
    /// Head
    /// </summary>
    public Transform head_Neck;
    public Transform head_Head;
    public Transform head_LeftEye;
    public Transform head_RightEye;
    public Transform head_Jaw;

    /// <summary>
    /// Left Hand
    /// </summary>
    public Transform leftHand_ThumbProximal;
    public Transform leftHand_ThumbIntermediate;
    public Transform leftHand_ThumbDistal;
    public Transform leftHand_ThumbTip;
    public Transform leftHand_IndexProximal;
    public Transform leftHand_IndexIntermediate;
    public Transform leftHand_IndexDistal;
    public Transform leftHand_IndexTip;
    public Transform leftHand_MiddleProximal;
    public Transform leftHand_MiddleIntermediate;
    public Transform leftHand_MiddleDistal;
    public Transform leftHand_MiddleTip;
    public Transform leftHand_RingProximal;
    public Transform leftHand_RingIntermediate;
    public Transform leftHand_RingDistal;
    public Transform leftHand_RingTip;
    public Transform leftHand_LittleProximal;
    public Transform leftHand_LittleIntermediate;
    public Transform leftHand_LittleDistal;
    public Transform leftHand_LittleTip;

    /// <summary>
    /// Right Hand
    /// </summary>
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

    public float leftHand_ThumbProximal_rotation;
    public float leftHand_ThumbIntermediate_rotation;
    public float leftHand_ThumbDistal_rotation;
    public float leftHand_IndexProximal_rotation;
    public float leftHand_IndexIntermediate_rotation;
    public float leftHand_IndexDistal_rotation;
    public float leftHand_MiddleProximal_rotation;
    public float leftHand_MiddleIntermediate_rotation;
    public float leftHand_MiddleDistal_rotation;
    public float leftHand_RingProximal_rotation;
    public float leftHand_RingIntermediate_rotation;
    public float leftHand_RingDistal_rotation;
    public float leftHand_LittleProximal_rotation;
    public float leftHand_LittleIntermediate_rotation;
    public float leftHand_LittleDistal_rotation;

    public float rightHand_ThumbProximal_rotation;
    public float rightHand_ThumbIntermediate_rotation;
    public float rightHand_ThumbDistal_rotation;
    public float rightHand_IndexProximal_rotation;
    public float rightHand_IndexIntermediate_rotation;
    public float rightHand_IndexDistal_rotation;
    public float rightHand_MiddleProximal_rotation;
    public float rightHand_MiddleIntermediate_rotation;
    public float rightHand_MiddleDistal_rotation;
    public float rightHand_RingProximal_rotation;
    public float rightHand_RingIntermediate_rotation;
    public float rightHand_RingDistal_rotation;
    public float rightHand_LittleProximal_rotation;
    public float rightHand_LittleIntermediate_rotation;
    public float rightHand_LittleDistal_rotation;

    void Start()
    {
    }
    public void AutoDetectReferences()
    {
        DetectReferencesByNaming();
    }
    void DetectReferencesByNaming()
    {
        Transform[] children = transform.GetComponentsInChildren<Transform>();
        DetectLimb(AvatarNaming.BoneType.Arm, AvatarNaming.BoneSide.Left, ref leftArm_UpperArm, ref leftArm_LowerArm, ref leftArm_Hand, children);
        DetectLimb(AvatarNaming.BoneType.Arm, AvatarNaming.BoneSide.Right, ref rightArm_UpperArm, ref rightArm_LowerArm, ref rightArm_Hand, children);
        DetectLimb(AvatarNaming.BoneType.Leg, AvatarNaming.BoneSide.Left, ref leftLeg_UpperLeg, ref leftLeg_LowerLeg, ref leftLeg_Foot, children);
        DetectLimb(AvatarNaming.BoneType.Leg, AvatarNaming.BoneSide.Right, ref rightLeg_UpperLeg, ref rightLeg_LowerLeg, ref rightLeg_Foot, children);
        if (leftLeg_Foot&&leftLeg_Foot.childCount > 0  )
        {
            leftLeg_Toes = leftLeg_Foot.GetChild(0);
        }
        if (rightLeg_Foot&&rightLeg_Foot.childCount > 0)
        {
            rightLeg_Toes = rightLeg_Foot.GetChild(0);
        }
        head_Head = AvatarNaming.GetBone(children, AvatarNaming.BoneType.Head);

        body_LeftBreast = AvatarNaming.GetBone(children,AvatarNaming.BoneType.Breast, AvatarNaming.BoneSide.Left);
        body_RightBreast = AvatarNaming.GetBone(children, AvatarNaming.BoneType.Breast, AvatarNaming.BoneSide.Right);

        body_Hips = AvatarNaming.GetNamingMatch(children, AvatarNaming.pelvis);

        // If pelvis is not an ancestor of a leg, it is not a valid pelvis
        if (body_Hips == null || !AvatarHierarchy.IsAncestor(leftLeg_UpperLeg, body_Hips))
        {
            if (leftLeg_UpperLeg != null) body_Hips = leftLeg_UpperLeg;
            if (rightLeg_UpperLeg != null) body_Hips = rightLeg_UpperLeg;
        }

        body_Chest = GetSameParent(leftArm_UpperArm, rightArm_UpperArm);
        if (body_Chest.parent != body_Hips)
        {
            body_Spine = body_Chest.parent;
        }
        else
        {
            body_Spine = body_Chest;
            body_Chest = null;
        }
        leftArm_Shoulder = GetParentBeforeSameParent(leftArm_UpperArm, rightArm_UpperArm);
        rightArm_Shoulder = GetParentBeforeSameParent(rightArm_UpperArm, leftArm_UpperArm);

        if (head_Head.parent!=body_Hips&&head_Head.parent!=body_Spine&&head_Head.parent!=body_Chest)
        {
            head_Neck = head_Head.parent;
        }
        Transform[] eyes = AvatarNaming.GetBonesOfType(AvatarNaming.BoneType.Eye, children);
        if (eyes.Length>1)
        {
            head_LeftEye = GetLeftest(eyes);
            head_RightEye = GetRightest(eyes);
        }

        MatchFingers();
    }
    private static void DetectLimb(AvatarNaming.BoneType boneType, AvatarNaming.BoneSide boneSide, ref Transform firstBone, ref Transform secondBone, ref Transform lastBone, Transform[] transforms)
    {
        Transform[] limb = AvatarNaming.GetBonesOfTypeAndSide(boneType, boneSide, transforms);

        if (limb.Length < 3)
        {
            //Warning.Log("Unable to detect biped bones by bone names. Please manually assign bone references.", firstBone, true);
            return;
        }

        // Standard biped characters
        if (limb.Length == 3)
        {
            firstBone = limb[0];
            secondBone = limb[1];
            lastBone = limb[2];
        }

        // For Bootcamp soldier type of characters with more than 3 limb bones
        if (limb.Length > 3)
        {
            firstBone = limb[0];
            secondBone = limb[2];
            lastBone = limb[limb.Length - 1];
        }
    }


    ///////////////
    Transform GetSameParent(Transform t1, Transform t2)
    {
        List<Transform> t1Parents = new List<Transform>();
        List<Transform> t2Parents = new List<Transform>();
        Transform tmp1 = t1;
        while (tmp1 != null)
        {
            t1Parents.Add(tmp1);
            tmp1 = tmp1.parent;
        }
        Transform tmp2 = t2;
        while (tmp2 != null)
        {
            t2Parents.Add(tmp2);
            tmp2 = tmp2.parent;
        }

        for (int i = 0; i < t1Parents.Count; i++)
        {
            for (int j = 0; j < t2Parents.Count; j++)
            {
                if (t1Parents[i] == t2Parents[j])
                {
                    return t1Parents[i];
                }
            }
        }

        return null;
    }
    Transform GetParentBeforeSameParent(Transform t1, Transform t2)
    {
        List<Transform> t1Parents = new List<Transform>();
        List<Transform> t2Parents = new List<Transform>();
        Transform tmp1 = t1;
        while (tmp1 != null)
        {
            t1Parents.Add(tmp1);
            tmp1 = tmp1.parent;
        }
        Transform tmp2 = t2;
        while (tmp2 != null)
        {
            t2Parents.Add(tmp2);
            tmp2 = tmp2.parent;
        }

        for (int i = 0; i + 1 < t1Parents.Count; i++)
        {
            for (int j = 0; j < t2Parents.Count; j++)
            {
                if (t1Parents[i + 1] == t2Parents[j])
                {
                    return t1Parents[i];
                }
            }
        }
        return null;
    }
    bool IsParent(Transform t, Transform parent)
    {
        if (t!=null && parent!=null)
        {
            if (t.parent == parent)
            {
                return true;
            }
        }
        return false;
    }
    Transform GetParentBeforeMe(Transform t, Transform me)
    {
        if (t == me)
        {
            return null;
        }
        Transform tmp1 = t.parent;
        if (tmp1 == me)
        {
            return null;
        }
        while (tmp1 != null)
        {
            Transform tmp = tmp1;
            
            tmp1 = tmp1.parent;
            if (tmp1 == me)
            {
                return tmp;
            }
            
        }

        return t;
    }
    class ChildLevel
    {
        public Transform child;
        public int level;
    }
    Transform[] FindDistals(Transform hand)
    {
        if (hand == null) return null;

        Action<Transform, int> act = null;
        List<ChildLevel> childList = new List<ChildLevel>();

        act += (t, l) =>
        {

            if (t.childCount == 0)
            {
                ChildLevel cl = new ChildLevel();
                cl.child = t;
                cl.level = l;
                childList.Add(cl);
            }
            l++;
            for (int i = 0; i < t.childCount; i++)
            {
                act(t.GetChild(i), l);
            }
        };
        act(hand, 0);

        childList.Sort((left, right) =>
        {
            if (left.level < right.level)
                return 1;
            else if (left.level == right.level)
                return 0;
            else
                return -1;
        });
        for (int i = 1; i < childList.Count; i++)
        {
            if (childList[i].level < childList[i - 1].level)
            {
                childList.RemoveRange(i, childList.Count - i);
                break;
            }
        }

        childList.Sort((left, right) =>
        {
            if (left.child.position.z < right.child.position.z)
                return 1;
            else if (left.child.position.z == right.child.position.z)
                return 0;
            else
                return -1;
        });

        Transform[] distal = new Transform[childList.Count];
        for (int i = 0; i < childList.Count; i++)
        {
            distal[i] = childList[i].child;
        }
        return distal;
    }
    Transform GetLeftest(Transform[] t)
    {
        float x = t[0].position.x;
        Transform tmp = t[0];
        for (int i = 0; i < t.Length; i++)
        {
            if (t[i].position.x<x)
            {
                tmp = t[i];
                x = t[i].position.x;
            }
        }
        return tmp;
    }
    Transform GetRightest(Transform[] t)
    {
        float x = t[0].position.x;
        Transform tmp = t[0];
        for (int i = 0; i < t.Length; i++)
        {
            if (t[i].position.x > x)
            {
                tmp = t[i];
                x = t[i].position.x;
            }
        }
        return tmp;
    }
    void MatchFingers()
    {
        //计量需求：手指至少要有两根骨头
        Transform[] rightHandDistals = FindDistals(rightArm_Hand);
        if (rightHandDistals.Length == 1)
        {
            rightHand_LittleProximal = GetParentBeforeMe(rightHandDistals[0], rightArm_Hand);
            rightHand_LittleIntermediate = GetParentBeforeMe(rightHandDistals[0], rightHand_LittleProximal);
            rightHand_ThumbDistal = GetParentBeforeMe(rightHandDistals[0], rightHand_ThumbIntermediate);
            rightHand_ThumbTip = IsParent(rightHandDistals[0], rightHand_ThumbDistal) ? rightHandDistals[0] : null;
        }
        else if (rightHandDistals.Length == 2)
        {
            rightHand_ThumbProximal = GetParentBeforeMe(rightHandDistals[0], rightArm_Hand);
            rightHand_ThumbIntermediate = GetParentBeforeMe(rightHandDistals[0], rightHand_ThumbProximal);
            rightHand_ThumbDistal = GetParentBeforeMe(rightHandDistals[0], rightHand_ThumbIntermediate);
            rightHand_ThumbTip = IsParent(rightHandDistals[0], rightHand_ThumbDistal) ? rightHandDistals[0] : null;

            rightHand_LittleProximal = GetParentBeforeMe(rightHandDistals[1], rightArm_Hand);
            rightHand_LittleIntermediate = GetParentBeforeMe(rightHandDistals[1], rightHand_LittleProximal);
            rightHand_LittleDistal = GetParentBeforeMe(rightHandDistals[1], rightHand_LittleIntermediate);
            rightHand_LittleTip = IsParent(rightHandDistals[1], rightHand_LittleDistal) ? rightHandDistals[2] : null;
        }
        else if (rightHandDistals.Length == 3)
        {
            rightHand_ThumbProximal = GetParentBeforeMe(rightHandDistals[0], rightArm_Hand);
            rightHand_ThumbIntermediate = GetParentBeforeMe(rightHandDistals[0], rightHand_ThumbProximal);
            rightHand_ThumbDistal = GetParentBeforeMe(rightHandDistals[0], rightHand_ThumbIntermediate);
            rightHand_ThumbTip = IsParent(rightHandDistals[0], rightHand_ThumbDistal) ? rightHandDistals[0] : null;

            rightHand_MiddleProximal = GetParentBeforeMe(rightHandDistals[1], rightArm_Hand);
            rightHand_MiddleIntermediate = GetParentBeforeMe(rightHandDistals[1], rightHand_MiddleProximal);
            rightHand_MiddleDistal = GetParentBeforeMe(rightHandDistals[1], rightHand_MiddleIntermediate);
            rightHand_MiddleTip = IsParent(rightHandDistals[1], rightHand_MiddleDistal) ? rightHandDistals[1] : null;

            rightHand_LittleProximal = GetParentBeforeMe(rightHandDistals[2], rightArm_Hand);
            rightHand_LittleIntermediate = GetParentBeforeMe(rightHandDistals[2], rightHand_LittleProximal);
            rightHand_LittleDistal = GetParentBeforeMe(rightHandDistals[2], rightHand_LittleIntermediate);
            rightHand_LittleTip = IsParent(rightHandDistals[2], rightHand_LittleDistal) ? rightHandDistals[2] : null;
        }
        else if (rightHandDistals.Length == 4)
        {
            rightHand_ThumbProximal = GetParentBeforeMe(rightHandDistals[0], rightArm_Hand);
            rightHand_ThumbIntermediate = GetParentBeforeMe(rightHandDistals[0], rightHand_ThumbProximal);
            rightHand_ThumbDistal = GetParentBeforeMe(rightHandDistals[0], rightHand_ThumbIntermediate);
            rightHand_ThumbTip = IsParent(rightHandDistals[0], rightHand_ThumbDistal) ? rightHandDistals[0] : null;

            rightHand_IndexProximal = GetParentBeforeMe(rightHandDistals[1], rightArm_Hand);
            rightHand_IndexIntermediate = GetParentBeforeMe(rightHandDistals[1], rightHand_IndexProximal);
            rightHand_IndexDistal = GetParentBeforeMe(rightHandDistals[1], rightHand_IndexIntermediate);
            rightHand_IndexTip = IsParent(rightHandDistals[1], rightHand_IndexDistal) ? rightHandDistals[1] : null;

            rightHand_MiddleProximal = GetParentBeforeMe(rightHandDistals[2], rightArm_Hand);
            rightHand_MiddleIntermediate = GetParentBeforeMe(rightHandDistals[2], rightHand_MiddleProximal);
            rightHand_MiddleDistal = GetParentBeforeMe(rightHandDistals[2], rightHand_MiddleIntermediate);
            rightHand_MiddleTip = IsParent(rightHandDistals[2], rightHand_MiddleDistal) ? rightHandDistals[2] : null;

            rightHand_LittleProximal = GetParentBeforeMe(rightHandDistals[3], rightArm_Hand);
            rightHand_LittleIntermediate = GetParentBeforeMe(rightHandDistals[3], rightHand_LittleProximal);
            rightHand_LittleDistal = GetParentBeforeMe(rightHandDistals[3], rightHand_LittleIntermediate);
            rightHand_LittleTip = IsParent(rightHandDistals[3], rightHand_LittleDistal) ? rightHandDistals[3] : null;
        }
        else if (rightHandDistals.Length == 5)
        {
            rightHand_ThumbProximal = GetParentBeforeMe(rightHandDistals[0], rightArm_Hand);
            rightHand_ThumbIntermediate = GetParentBeforeMe(rightHandDistals[0], rightHand_ThumbProximal);
            rightHand_ThumbDistal = GetParentBeforeMe(rightHandDistals[0], rightHand_ThumbIntermediate);
            rightHand_ThumbTip = IsParent(rightHandDistals[0], rightHand_ThumbDistal) ? rightHandDistals[0] : null;

            rightHand_IndexProximal = GetParentBeforeMe(rightHandDistals[1], rightArm_Hand);
            rightHand_IndexIntermediate = GetParentBeforeMe(rightHandDistals[1], rightHand_IndexProximal);
            rightHand_IndexDistal = GetParentBeforeMe(rightHandDistals[1], rightHand_IndexIntermediate);
            rightHand_IndexTip = IsParent(rightHandDistals[1], rightHand_IndexDistal) ? rightHandDistals[1] : null;

            rightHand_MiddleProximal = GetParentBeforeMe(rightHandDistals[2], rightArm_Hand);
            rightHand_MiddleIntermediate = GetParentBeforeMe(rightHandDistals[2], rightHand_MiddleProximal);
            rightHand_MiddleDistal = GetParentBeforeMe(rightHandDistals[2], rightHand_MiddleIntermediate);
            rightHand_MiddleTip = IsParent(rightHandDistals[2], rightHand_MiddleDistal) ? rightHandDistals[2] : null;

            rightHand_RingProximal = GetParentBeforeMe(rightHandDistals[3], rightArm_Hand);
            rightHand_RingIntermediate = GetParentBeforeMe(rightHandDistals[3], rightHand_RingProximal);
            rightHand_RingDistal = GetParentBeforeMe(rightHandDistals[3], rightHand_RingIntermediate);
            rightHand_RingTip = IsParent(rightHandDistals[3], rightHand_RingDistal) ? rightHandDistals[3] : null;

            rightHand_LittleProximal = GetParentBeforeMe(rightHandDistals[4], rightArm_Hand);
            rightHand_LittleIntermediate = GetParentBeforeMe(rightHandDistals[4], rightHand_LittleProximal);
            rightHand_LittleDistal = GetParentBeforeMe(rightHandDistals[4], rightHand_LittleIntermediate);
            rightHand_LittleTip = IsParent(rightHandDistals[4], rightHand_LittleDistal) ? rightHandDistals[4] : null;
        }


        Transform[] leftHandDistals = FindDistals(leftArm_Hand);
        if (leftHandDistals.Length == 1)
        {
            leftHand_LittleProximal = GetParentBeforeMe(leftHandDistals[0], leftArm_Hand);
            leftHand_LittleIntermediate = GetParentBeforeMe(leftHandDistals[0], leftHand_LittleProximal);
            leftHand_ThumbDistal = GetParentBeforeMe(leftHandDistals[0], leftHand_ThumbIntermediate);
            leftHand_ThumbTip = IsParent(leftHandDistals[0], leftHand_ThumbDistal) ? leftHandDistals[0] : null;
        }
        else if (leftHandDistals.Length == 2)
        {
            leftHand_ThumbProximal = GetParentBeforeMe(leftHandDistals[0], leftArm_Hand);
            leftHand_ThumbIntermediate = GetParentBeforeMe(leftHandDistals[0], leftHand_ThumbProximal);
            leftHand_ThumbDistal = GetParentBeforeMe(leftHandDistals[0], leftHand_ThumbIntermediate);
            leftHand_ThumbTip = IsParent(leftHandDistals[0], leftHand_ThumbDistal) ? leftHandDistals[0] : null;

            leftHand_LittleProximal = GetParentBeforeMe(leftHandDistals[1], leftArm_Hand);
            leftHand_LittleIntermediate = GetParentBeforeMe(leftHandDistals[1], leftHand_LittleProximal);
            leftHand_LittleDistal = GetParentBeforeMe(leftHandDistals[1], leftHand_LittleIntermediate);
            leftHand_LittleTip = IsParent(leftHandDistals[1], leftHand_LittleDistal) ? leftHandDistals[2] : null;
        }
        else if (leftHandDistals.Length == 3)
        {
            leftHand_ThumbProximal = GetParentBeforeMe(leftHandDistals[0], leftArm_Hand);
            leftHand_ThumbIntermediate = GetParentBeforeMe(leftHandDistals[0], leftHand_ThumbProximal);
            leftHand_ThumbDistal = GetParentBeforeMe(leftHandDistals[0], leftHand_ThumbIntermediate);
            leftHand_ThumbTip = IsParent(leftHandDistals[0], leftHand_ThumbDistal) ? leftHandDistals[0] : null;

            leftHand_MiddleProximal = GetParentBeforeMe(leftHandDistals[1], leftArm_Hand);
            leftHand_MiddleIntermediate = GetParentBeforeMe(leftHandDistals[1], leftHand_MiddleProximal);
            leftHand_MiddleDistal = GetParentBeforeMe(leftHandDistals[1], leftHand_MiddleIntermediate);
            leftHand_MiddleTip = IsParent(leftHandDistals[1], leftHand_MiddleDistal) ? leftHandDistals[1] : null;

            leftHand_LittleProximal = GetParentBeforeMe(leftHandDistals[2], leftArm_Hand);
            leftHand_LittleIntermediate = GetParentBeforeMe(leftHandDistals[2], leftHand_LittleProximal);
            leftHand_LittleDistal = GetParentBeforeMe(leftHandDistals[2], leftHand_LittleIntermediate);
            leftHand_LittleTip = IsParent(leftHandDistals[2], leftHand_LittleDistal) ? leftHandDistals[2] : null;
        }
        else if (leftHandDistals.Length == 4)
        {
            leftHand_ThumbProximal = GetParentBeforeMe(leftHandDistals[0], leftArm_Hand);
            leftHand_ThumbIntermediate = GetParentBeforeMe(leftHandDistals[0], leftHand_ThumbProximal);
            leftHand_ThumbDistal = GetParentBeforeMe(leftHandDistals[0], leftHand_ThumbIntermediate);
            leftHand_ThumbTip = IsParent(leftHandDistals[0], leftHand_ThumbDistal) ? leftHandDistals[0] : null;

            leftHand_IndexProximal = GetParentBeforeMe(leftHandDistals[1], leftArm_Hand);
            leftHand_IndexIntermediate = GetParentBeforeMe(leftHandDistals[1], leftHand_IndexProximal);
            leftHand_IndexDistal = GetParentBeforeMe(leftHandDistals[1], leftHand_IndexIntermediate);
            leftHand_IndexTip = IsParent(leftHandDistals[1], leftHand_IndexDistal) ? leftHandDistals[1] : null;

            leftHand_MiddleProximal = GetParentBeforeMe(leftHandDistals[2], leftArm_Hand);
            leftHand_MiddleIntermediate = GetParentBeforeMe(leftHandDistals[2], leftHand_MiddleProximal);
            leftHand_MiddleDistal = GetParentBeforeMe(leftHandDistals[2], leftHand_MiddleIntermediate);
            leftHand_MiddleTip = IsParent(leftHandDistals[2], leftHand_MiddleDistal) ? leftHandDistals[2] : null;

            leftHand_LittleProximal = GetParentBeforeMe(leftHandDistals[3], leftArm_Hand);
            leftHand_LittleIntermediate = GetParentBeforeMe(leftHandDistals[3], leftHand_LittleProximal);
            leftHand_LittleDistal = GetParentBeforeMe(leftHandDistals[3], leftHand_LittleIntermediate);
            leftHand_LittleTip = IsParent(leftHandDistals[3], leftHand_LittleDistal) ? leftHandDistals[3] : null;
        }
        else if (leftHandDistals.Length == 5)
        {
            leftHand_ThumbProximal = GetParentBeforeMe(leftHandDistals[0], leftArm_Hand);
            leftHand_ThumbIntermediate = GetParentBeforeMe(leftHandDistals[0], leftHand_ThumbProximal);
            leftHand_ThumbDistal = GetParentBeforeMe(leftHandDistals[0], leftHand_ThumbIntermediate);
            leftHand_ThumbTip = IsParent(leftHandDistals[0], leftHand_ThumbDistal) ? leftHandDistals[0] : null;

            leftHand_IndexProximal = GetParentBeforeMe(leftHandDistals[1], leftArm_Hand);
            leftHand_IndexIntermediate = GetParentBeforeMe(leftHandDistals[1], leftHand_IndexProximal);
            leftHand_IndexDistal = GetParentBeforeMe(leftHandDistals[1], leftHand_IndexIntermediate);
            leftHand_IndexTip = IsParent(leftHandDistals[1], leftHand_IndexDistal) ? leftHandDistals[1] : null;

            leftHand_MiddleProximal = GetParentBeforeMe(leftHandDistals[2], leftArm_Hand);
            leftHand_MiddleIntermediate = GetParentBeforeMe(leftHandDistals[2], leftHand_MiddleProximal);
            leftHand_MiddleDistal = GetParentBeforeMe(leftHandDistals[2], leftHand_MiddleIntermediate);
            leftHand_MiddleTip = IsParent(leftHandDistals[2], leftHand_MiddleDistal) ? leftHandDistals[2] : null;

            leftHand_RingProximal = GetParentBeforeMe(leftHandDistals[3], leftArm_Hand);
            leftHand_RingIntermediate = GetParentBeforeMe(leftHandDistals[3], leftHand_RingProximal);
            leftHand_RingDistal = GetParentBeforeMe(leftHandDistals[3], leftHand_RingIntermediate);
            leftHand_RingTip = IsParent(leftHandDistals[3], leftHand_RingDistal) ? leftHandDistals[3] : null;

            leftHand_LittleProximal = GetParentBeforeMe(leftHandDistals[4], leftArm_Hand);
            leftHand_LittleIntermediate = GetParentBeforeMe(leftHandDistals[4], leftHand_LittleProximal);
            leftHand_LittleDistal = GetParentBeforeMe(leftHandDistals[4], leftHand_LittleIntermediate);
            leftHand_LittleTip = IsParent(leftHandDistals[4], leftHand_LittleDistal) ? leftHandDistals[4] : null;
        }
    }
}