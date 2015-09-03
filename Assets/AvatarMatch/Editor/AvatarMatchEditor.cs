#region 模块信息
/*----------------------------------------------------------------
// Copyright (C) 2015 广州，蓝弧
//
// 模块名：MatchAvatarEditor
// 创建者：张嘉俊
// 修改者列表：
// 创建日期：#CREATIONDATE#
// 模块描述：
//----------------------------------------------------------------*/
#endregion


using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using System;

[CustomEditor(typeof(AvatarMatch))]
public class AvatarMatchEditor : Editor
{
    //默认模型朝向是Z轴，即面向玩家

    bool showBodyStatus = false;
    bool showLeftArmStatus = false;
    bool showRightArmStatus = false;
    bool showLeftLegStatus = false;
    bool showRightLegStatus = false;
    bool showHeadStatus = false;
    bool showRightHandStatus = false;
    bool showLeftHandStatus = false;
    AvatarMatch _target;
    private SerializedObject mTarget;


  
    //hands
    Transform leftHand_ThumbProximal = null;
    Transform leftHand_ThumbIntermediate = null;
    Transform leftHand_ThumbDistal = null;
    Transform leftHand_IndexProximal = null;
    Transform leftHand_IndexIntermediate = null;
    Transform leftHand_IndexDistal = null;
    Transform leftHand_MiddleProximal = null;
    Transform leftHand_MiddleIntermediate = null;
    Transform leftHand_MiddleDistal = null;
    Transform leftHand_RingProximal = null;
    Transform leftHand_RingIntermediate = null;
    Transform leftHand_RingDistal = null;
    Transform leftHand_LittleProximal = null;
    Transform leftHand_LittleIntermediate = null;
    Transform leftHand_LittleDistal = null;

    Transform rightHand_ThumbProximal = null;
    Transform rightHand_ThumbIntermediate = null;
    Transform rightHand_ThumbDistal = null;
    Transform rightHand_IndexProximal = null;
    Transform rightHand_IndexIntermediate = null;
    Transform rightHand_IndexDistal = null;
    Transform rightHand_MiddleProximal = null;
    Transform rightHand_MiddleIntermediate = null;
    Transform rightHand_MiddleDistal = null;
    Transform rightHand_RingProximal = null;
    Transform rightHand_RingIntermediate = null;
    Transform rightHand_RingDistal = null;
    Transform rightHand_LittleProximal = null;
    Transform rightHand_LittleIntermediate = null;
    Transform rightHand_LittleDistal = null;
    void OnEnable()
    {
        _target = target as AvatarMatch;
        mTarget = serializedObject;
    }

    //一条直线找到督
    Transform GetTerminalChild(Transform t)
    {
        if (t.childCount > 1)
        {
            return t;
        }
        else if (t.childCount == 0)
        {
            return t;
        }
        else
        {
            return GetTerminalChild(t.GetChild(0));
        }
    }
    Transform[] FindLegs(Transform hip)
    {
        Transform[] legs = new Transform[2];
        List<Transform> tmp = new List<Transform>();
        for (int i = 0; i < hip.childCount; i++)
        {
            tmp.Add(GetTerminalChild(hip.GetChild(i)));
        }
        tmp.Sort((left, right) =>
        {
            if (left.position.y > right.position.y)
                return 1;
            else if (left.position.y == right.position.y)
                return 0;
            else
                return -1;
        });
        legs[0] = tmp[0];
        legs[1] = tmp[1];
        return legs;
    }

    Transform FindHand(Transform finger)
    {
        Transform tmp = finger;
        while (tmp != null)
        {
            if (tmp.parent.childCount > 1)
            {
                return tmp.parent;
            }
            tmp = tmp.parent;
        }
        return finger;
    }
    class ChildLevel
    {
        public Transform child;
        public int level;
    }



    //获得最深的孩子
    Transform FindLastChild(Transform shoulder)
    {
        if (shoulder == null) return null;

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
        act(shoulder, 0);

        childList.Sort((left, right) =>
        {
            if (left.level > right.level)
                return 1;
            else if (left.level == right.level)
                return 0;
            else
                return -1;
        });

        return childList[childList.Count - 1].child;
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


    Transform GetSameParentAtLevel(Transform t1, Transform t2, int level)
    {
        int i = 1;
        Transform tmp1 = t1.parent;
        Transform tmp2 = t2.parent;
        while (tmp1 != null && tmp2 != null)
        {
            if (tmp1 == tmp2)
            {
                if (i >= level)
                {
                    return tmp2;
                }
                else return null;
            }
            tmp1 = tmp1.parent;
            tmp2 = tmp2.parent;
            i++;
        }
        return null;
    }
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
    Transform GetParentBeforeMe(Transform t, Transform me)
    {
        if (t == me)
        {
            return null;
        }
        Transform tmp1 = t.parent;

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
    Transform IsParent(Transform t, Transform parent)
    {
        if (t.parent == parent)
        {
            return t;
        }
        else return t.parent;
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
    bool HaveDownChild(Transform t)
    {
        for (int i = 0; i < t.childCount; i++)
        {
            if (t.GetChild(i).position.y < t.position.y)
            {
                return true;
            }
        }
        return false;
    }
    Transform FindHip(Transform main)
    {
        List<Transform> childList = new List<Transform>();
        Action<Transform> act = null;
        act += (t) =>
        {
            childList.Add(t);

            for (int i = 0; i < t.childCount; i++)
            {
                act(t.GetChild(i));
            }
        };
        act(main);


        childList.Sort((left, right) =>
        {
            //从小到大
            if (left.position.y > right.position.y)
                return 1;
            else if (left.position.y == right.position.y)
                return 0;
            else
                return -1;
        });


        for (int i = 0; i < childList.Count; i++)
        {
            for (int j = 0; j < childList.Count; j++)
            {
                if (i != j)
                {
                    Transform hip = GetSameParentAtLevel(childList[i], childList[j], 3);
                    if (hip != null)
                    {
                        //hip 以一般比较高为根据
                        if (hip.position.y - main.position.y > 0.1f)
                        {
                            return hip;
                        }

                    }

                }
            }
        }
        return null;
    }

    public override void OnInspectorGUI()
    {
        

        mTarget.Update();
        FingersSlider();
        

        showBodyStatus = EditorGUILayout.Foldout(showBodyStatus, "Show Body Status");

        if (showBodyStatus)
        {
            _target.body_Hips = EditorGUILayout.ObjectField("body_Hips", _target.body_Hips, typeof(Transform),true) as Transform;
            _target.body_Spine = EditorGUILayout.ObjectField("body_Spine", _target.body_Spine, typeof(Transform),true) as Transform;
            _target.body_Chest = EditorGUILayout.ObjectField("body_Chest", _target.body_Chest, typeof(Transform),true) as Transform;
            _target.body_LeftBreast = EditorGUILayout.ObjectField("body_LeftBreast", _target.body_LeftBreast, typeof(Transform),true) as Transform;
            _target.body_RightBreast = EditorGUILayout.ObjectField("body_RightBreast", _target.body_RightBreast, typeof(Transform),true) as Transform;
        }
        showLeftArmStatus = EditorGUILayout.Foldout(showLeftArmStatus, "Show Left Arm Status");
        if (showLeftArmStatus)
        {
            _target.leftArm_Shoulder = EditorGUILayout.ObjectField("leftArm_Shoulder", _target.leftArm_Shoulder, typeof(Transform),true) as Transform;
            _target.leftArm_UpperArm = EditorGUILayout.ObjectField("leftArm_UpperArm", _target.leftArm_UpperArm, typeof(Transform),true) as Transform;
            _target.leftArm_LowerArm = EditorGUILayout.ObjectField("leftArm_LowerArm", _target.leftArm_LowerArm, typeof(Transform),true) as Transform;
            _target.leftArm_Hand = EditorGUILayout.ObjectField("leftArm_Hand", _target.leftArm_Hand, typeof(Transform),true) as Transform;
        }
        showRightArmStatus = EditorGUILayout.Foldout(showRightArmStatus, "Show Right Arm Status");
        if (showRightArmStatus)
        {
            _target.rightArm_Shoulder = EditorGUILayout.ObjectField("rightArm_Shoulder", _target.rightArm_Shoulder, typeof(Transform),true) as Transform;
            _target.rightArm_UpperArm = EditorGUILayout.ObjectField("rightArm_UpperArm", _target.rightArm_UpperArm, typeof(Transform),true) as Transform;
            _target.rightArm_LowerArm = EditorGUILayout.ObjectField("rightArm_LowerArm", _target.rightArm_LowerArm, typeof(Transform),true) as Transform;
            _target.rightArm_Hand = EditorGUILayout.ObjectField("rightArm_Hand", _target.rightArm_Hand, typeof(Transform),true) as Transform;
        }
        showLeftLegStatus = EditorGUILayout.Foldout(showLeftLegStatus, "Show Left Leg Status");

        if (showLeftLegStatus)
        {
            _target.leftLeg_UpperLeg = EditorGUILayout.ObjectField("leftLeg_UpperLeg", _target.leftLeg_UpperLeg, typeof(Transform),true) as Transform;
            _target.leftLeg_LowerLeg = EditorGUILayout.ObjectField("leftLeg_LowerLeg", _target.leftLeg_LowerLeg, typeof(Transform),true) as Transform;
            _target.leftLeg_Foot = EditorGUILayout.ObjectField("leftLeg_Foot", _target.leftLeg_Foot, typeof(Transform),true) as Transform;
            _target.leftLeg_Toes = EditorGUILayout.ObjectField("leftLeg_Toes", _target.leftLeg_Toes, typeof(Transform),true) as Transform;
        }
        showRightLegStatus = EditorGUILayout.Foldout(showRightLegStatus, "Show Right Leg Status");

        if (showRightLegStatus)
        {
            _target.rightLeg_UpperLeg = EditorGUILayout.ObjectField("rightLeg_UpperLegr", _target.rightLeg_UpperLeg, typeof(Transform),true) as Transform;
            _target.rightLeg_LowerLeg = EditorGUILayout.ObjectField("rightLeg_LowerLeg", _target.rightLeg_LowerLeg, typeof(Transform),true) as Transform;
            _target.rightLeg_Foot = EditorGUILayout.ObjectField("rightLeg_Foot", _target.rightLeg_Foot, typeof(Transform),true) as Transform;
            _target.rightLeg_Toes = EditorGUILayout.ObjectField("rightLeg_Toes", _target.rightLeg_Toes, typeof(Transform),true) as Transform;
        }
        showHeadStatus = EditorGUILayout.Foldout(showHeadStatus, "Show Head Status");
        if (showHeadStatus)
        {
            _target.head_Neck = EditorGUILayout.ObjectField("head_Neck", _target.head_Neck, typeof(Transform),true) as Transform;
            _target.head_Head = EditorGUILayout.ObjectField("head_Head", _target.head_Head, typeof(Transform),true) as Transform;
            _target.head_LeftEye = EditorGUILayout.ObjectField("head_LeftEye", _target.head_LeftEye, typeof(Transform),true) as Transform;
            _target.head_RightEye = EditorGUILayout.ObjectField("head_RightEye", _target.head_RightEye, typeof(Transform),true) as Transform;
            _target.head_Jaw = EditorGUILayout.ObjectField("head_Jaw", _target.head_Jaw, typeof(Transform),true) as Transform;
        }
        showLeftHandStatus = EditorGUILayout.Foldout(showLeftHandStatus, "Show Left Hand Status");
        if (showLeftHandStatus)
        {
            _target.leftHand_ThumbProximal = EditorGUILayout.ObjectField("leftHand_ThumbProximal", _target.leftHand_ThumbProximal, typeof(Transform),true) as Transform;
            _target.leftHand_ThumbIntermediate = EditorGUILayout.ObjectField("leftHand_ThumbIntermediate", _target.leftHand_ThumbIntermediate, typeof(Transform),true) as Transform;
            _target.leftHand_ThumbDistal = EditorGUILayout.ObjectField("leftHand_ThumbDistal", _target.leftHand_ThumbDistal, typeof(Transform),true) as Transform;
            _target.leftHand_ThumbTip = EditorGUILayout.ObjectField("leftHand_ThumbTip", _target.leftHand_ThumbTip, typeof(Transform), true) as Transform;

            _target.leftHand_IndexProximal = EditorGUILayout.ObjectField("leftHand_IndexProximal", _target.leftHand_IndexProximal, typeof(Transform),true) as Transform;
            _target.leftHand_IndexIntermediate = EditorGUILayout.ObjectField("leftHand_IndexIntermediate", _target.leftHand_IndexIntermediate, typeof(Transform),true) as Transform;
            _target.leftHand_IndexDistal = EditorGUILayout.ObjectField("leftHand_IndexDistal", _target.leftHand_IndexDistal, typeof(Transform),true) as Transform;
            _target.leftHand_IndexTip = EditorGUILayout.ObjectField("leftHand_IndexTip", _target.leftHand_IndexTip, typeof(Transform), true) as Transform;

            _target.leftHand_MiddleProximal = EditorGUILayout.ObjectField("leftHand_MiddleProximal", _target.leftHand_MiddleProximal, typeof(Transform),true) as Transform;
            _target.leftHand_MiddleIntermediate = EditorGUILayout.ObjectField("leftHand_MiddleIntermediate", _target.leftHand_MiddleIntermediate, typeof(Transform),true) as Transform;
            _target.leftHand_MiddleDistal = EditorGUILayout.ObjectField("leftHand_MiddleDistal", _target.leftHand_MiddleDistal, typeof(Transform),true) as Transform;
            _target.leftHand_MiddleTip = EditorGUILayout.ObjectField("leftHand_MiddleTip", _target.leftHand_MiddleTip, typeof(Transform), true) as Transform;

            _target.leftHand_RingProximal = EditorGUILayout.ObjectField("leftHand_RingProximal", _target.leftHand_RingProximal, typeof(Transform),true) as Transform;
            _target.leftHand_RingIntermediate = EditorGUILayout.ObjectField("leftHand_RingIntermediate", _target.leftHand_RingIntermediate, typeof(Transform),true) as Transform;
            _target.leftHand_RingDistal = EditorGUILayout.ObjectField("leftHand_RingDistal", _target.leftHand_RingDistal, typeof(Transform),true) as Transform;
            _target.leftHand_RingTip = EditorGUILayout.ObjectField("leftHand_RingTip", _target.leftHand_RingTip, typeof(Transform), true) as Transform;

            _target.leftHand_LittleProximal = EditorGUILayout.ObjectField("leftHand_LittleProximal", _target.leftHand_LittleProximal, typeof(Transform),true) as Transform;
            _target.leftHand_LittleIntermediate = EditorGUILayout.ObjectField("leftHand_LittleIntermediate", _target.leftHand_LittleIntermediate, typeof(Transform),true) as Transform;
            _target.leftHand_LittleDistal = EditorGUILayout.ObjectField("leftHand_LittleDistal", _target.leftHand_LittleDistal, typeof(Transform),true) as Transform;
            _target.leftHand_LittleTip = EditorGUILayout.ObjectField("leftHand_LittleTip", _target.leftHand_LittleTip, typeof(Transform), true) as Transform;
        }
        showRightHandStatus = EditorGUILayout.Foldout(showRightHandStatus, "Show Right Hand Status");
        if (showRightHandStatus)
        {
            _target.rightHand_ThumbProximal = EditorGUILayout.ObjectField("rightHand_ThumbProximal", _target.rightHand_ThumbProximal, typeof(Transform), true) as Transform;
            _target.rightHand_ThumbIntermediate = EditorGUILayout.ObjectField("rightHand_ThumbIntermediate", _target.rightHand_ThumbIntermediate, typeof(Transform), true) as Transform;
            _target.rightHand_ThumbDistal = EditorGUILayout.ObjectField("rightHand_ThumbDistal", _target.rightHand_ThumbDistal, typeof(Transform), true) as Transform;
            _target.rightHand_ThumbTip = EditorGUILayout.ObjectField("rightHand_ThumbTip", _target.rightHand_ThumbTip, typeof(Transform), true) as Transform;

            _target.rightHand_IndexProximal = EditorGUILayout.ObjectField("rightHand_IndexProximal", _target.rightHand_IndexProximal, typeof(Transform), true) as Transform;
            _target.rightHand_IndexIntermediate = EditorGUILayout.ObjectField("rightHand_IndexIntermediate", _target.rightHand_IndexIntermediate, typeof(Transform), true) as Transform;
            _target.rightHand_IndexDistal = EditorGUILayout.ObjectField("rightHand_IndexDistal", _target.rightHand_IndexDistal, typeof(Transform), true) as Transform;
            _target.rightHand_IndexTip = EditorGUILayout.ObjectField("rightHand_IndexTip", _target.rightHand_IndexTip, typeof(Transform), true) as Transform;

            _target.rightHand_MiddleProximal = EditorGUILayout.ObjectField("rightHand_MiddleProximal", _target.rightHand_MiddleProximal, typeof(Transform), true) as Transform;
            _target.rightHand_MiddleIntermediate = EditorGUILayout.ObjectField("rightHand_MiddleIntermediate", _target.rightHand_MiddleIntermediate, typeof(Transform), true) as Transform;
            _target.rightHand_MiddleDistal = EditorGUILayout.ObjectField("rightHand_MiddleDistal", _target.rightHand_MiddleDistal, typeof(Transform), true) as Transform;
            _target.rightHand_MiddleTip = EditorGUILayout.ObjectField("rightHand_MiddleTip", _target.rightHand_MiddleTip, typeof(Transform), true) as Transform;

            _target.rightHand_RingProximal = EditorGUILayout.ObjectField("rightHand_RingProximal", _target.rightHand_RingProximal, typeof(Transform), true) as Transform;
            _target.rightHand_RingIntermediate = EditorGUILayout.ObjectField("rightHand_RingIntermediate", _target.rightHand_RingIntermediate, typeof(Transform), true) as Transform;
            _target.rightHand_RingDistal = EditorGUILayout.ObjectField("rightHand_RingDistal", _target.rightHand_RingDistal, typeof(Transform), true) as Transform;
            _target.rightHand_RingTip = EditorGUILayout.ObjectField("rightHand_RingTip", _target.rightHand_RingTip, typeof(Transform), true) as Transform;

            _target.rightHand_LittleProximal = EditorGUILayout.ObjectField("rightHand_LittleProximal", _target.rightHand_LittleProximal, typeof(Transform), true) as Transform;
            _target.rightHand_LittleIntermediate = EditorGUILayout.ObjectField("rightHand_LittleIntermediate", _target.rightHand_LittleIntermediate, typeof(Transform), true) as Transform;
            _target.rightHand_LittleDistal = EditorGUILayout.ObjectField("rightHand_LittleDistal", _target.rightHand_LittleDistal, typeof(Transform), true) as Transform;
            _target.rightHand_LittleTip = EditorGUILayout.ObjectField("rightHand_LittleTip", _target.rightHand_LittleTip, typeof(Transform), true) as Transform;
        }


        if (GUILayout.Button("Match"))
        {
            _target.AutoDetectReferences();         
           
        }
        


        mTarget.ApplyModifiedProperties();

    }


    void ErrorBones(Transform t)
    {
        Debug.Log("not enough bones " + t);
    }
    void FingersSlider()
    {
        leftHand_ThumbProximal = _target.leftHand_ThumbProximal;
        if (leftHand_ThumbProximal)
        {
            leftHand_ThumbProximal.localRotation = Quaternion.Slerp(Quaternion.identity, Quaternion.Euler(45, -45, 0), _target.leftHand_ThumbProximal_rotation);
            mTarget.FindProperty("leftHand_ThumbProximal").objectReferenceValue = leftHand_ThumbProximal;
        }
        leftHand_ThumbIntermediate = _target.leftHand_ThumbIntermediate;
        if (leftHand_ThumbIntermediate)
        {
            leftHand_ThumbIntermediate.localRotation = Quaternion.Slerp(Quaternion.identity, Quaternion.Euler(45, -45, 0), _target.leftHand_ThumbIntermediate_rotation);
            mTarget.FindProperty("leftHand_ThumbIntermediate").objectReferenceValue = leftHand_ThumbIntermediate;
        }
        leftHand_ThumbDistal = _target.leftHand_ThumbDistal;
        if (leftHand_ThumbDistal)
        {
            leftHand_ThumbDistal.localRotation = Quaternion.Slerp(Quaternion.identity, Quaternion.Euler(45, -45, 0), _target.leftHand_ThumbDistal_rotation);
            mTarget.FindProperty("leftHand_ThumbDistal").objectReferenceValue = leftHand_ThumbDistal;
        }
        leftHand_IndexProximal = _target.leftHand_IndexProximal;
        if (leftHand_IndexProximal)
        {
            leftHand_IndexProximal.localRotation = Quaternion.Slerp(Quaternion.Euler(-10, 0, 0), Quaternion.Euler(10, 0, 90), _target.leftHand_IndexProximal_rotation);
            mTarget.FindProperty("leftHand_IndexProximal").objectReferenceValue = leftHand_IndexProximal;
        }
        leftHand_IndexIntermediate = _target.leftHand_IndexIntermediate;
        if (leftHand_IndexIntermediate)
        {
            leftHand_IndexIntermediate.localRotation = Quaternion.Slerp(Quaternion.identity, Quaternion.Euler(0, 0, 110), _target.leftHand_IndexIntermediate_rotation);
            mTarget.FindProperty("leftHand_IndexIntermediate").objectReferenceValue = leftHand_IndexIntermediate;
        }
        leftHand_IndexDistal = _target.leftHand_IndexDistal;
        if (leftHand_IndexDistal)
        {
            leftHand_IndexDistal.localRotation = Quaternion.Slerp(Quaternion.identity, Quaternion.Euler(0, 0, 90), _target.leftHand_IndexDistal_rotation);
            mTarget.FindProperty("leftHand_IndexDistal").objectReferenceValue = leftHand_IndexDistal;
        }
        leftHand_MiddleProximal = _target.leftHand_MiddleProximal;
        if (leftHand_MiddleProximal)
        {
            leftHand_MiddleProximal.localRotation = Quaternion.Slerp(Quaternion.identity, Quaternion.Euler(0, 0, 90), _target.leftHand_MiddleProximal_rotation);
            mTarget.FindProperty("leftHand_MiddleProximal").objectReferenceValue = leftHand_MiddleProximal;
        }
        leftHand_MiddleIntermediate = _target.leftHand_MiddleIntermediate;
        if (leftHand_MiddleIntermediate)
        {
            leftHand_MiddleIntermediate.localRotation = Quaternion.Slerp(Quaternion.identity, Quaternion.Euler(0, 0, 110), _target.leftHand_MiddleIntermediate_rotation);
            mTarget.FindProperty("leftHand_MiddleIntermediate").objectReferenceValue = leftHand_MiddleIntermediate;
        }
        leftHand_MiddleDistal = _target.leftHand_MiddleDistal;
        if (leftHand_MiddleDistal)
        {
            leftHand_MiddleDistal.localRotation = Quaternion.Slerp(Quaternion.identity, Quaternion.Euler(0, 0, 90), _target.leftHand_MiddleDistal_rotation);
            mTarget.FindProperty("leftHand_MiddleDistal").objectReferenceValue = leftHand_MiddleDistal;
        }
        leftHand_RingProximal = _target.leftHand_RingProximal;
        if (leftHand_RingProximal)
        {
            leftHand_RingProximal.localRotation = Quaternion.Slerp(Quaternion.Euler(5, 0, 0), Quaternion.Euler(-5, 0, 90), _target.leftHand_RingProximal_rotation);
            mTarget.FindProperty("leftHand_RingProximal").objectReferenceValue = leftHand_RingProximal;
        }
        leftHand_RingIntermediate = _target.leftHand_RingIntermediate;
        if (leftHand_RingIntermediate)
        {
            leftHand_RingIntermediate.localRotation = Quaternion.Slerp(Quaternion.identity, Quaternion.Euler(0, 0, 110), _target.leftHand_RingIntermediate_rotation);
            mTarget.FindProperty("leftHand_RingIntermediate").objectReferenceValue = leftHand_RingIntermediate;
        }
        leftHand_RingDistal = _target.leftHand_RingDistal;
        if (leftHand_RingDistal)
        {
            leftHand_RingDistal.localRotation = Quaternion.Slerp(Quaternion.identity, Quaternion.Euler(0, 0, 90), _target.leftHand_RingDistal_rotation);
            mTarget.FindProperty("leftHand_RingDistal").objectReferenceValue = leftHand_RingDistal;
        }
        leftHand_LittleProximal = _target.leftHand_LittleProximal;
        if (leftHand_LittleProximal)
        {
            leftHand_LittleProximal.localRotation = Quaternion.Slerp(Quaternion.Euler(10, 0, 0), Quaternion.Euler(-10, 0, 90), _target.leftHand_LittleProximal_rotation);
            mTarget.FindProperty("leftHand_LittleProximal").objectReferenceValue = leftHand_LittleProximal;
        }
        leftHand_LittleIntermediate = _target.leftHand_LittleIntermediate;
        if (leftHand_LittleIntermediate)
        {
            leftHand_LittleIntermediate.localRotation = Quaternion.Slerp(Quaternion.identity, Quaternion.Euler(0, 0, 110), _target.leftHand_LittleIntermediate_rotation);
            mTarget.FindProperty("leftHand_LittleIntermediate").objectReferenceValue = leftHand_LittleIntermediate;
        }
        leftHand_LittleDistal = _target.leftHand_LittleDistal;
        if (leftHand_LittleDistal)
        {
            leftHand_LittleDistal.localRotation = Quaternion.Slerp(Quaternion.identity, Quaternion.Euler(0, 0, 90), _target.leftHand_LittleDistal_rotation);
            mTarget.FindProperty("leftHand_LittleDistal").objectReferenceValue = leftHand_LittleDistal;
        }
      
        _target.leftHand_ThumbProximal_rotation = EditorGUILayout.Slider("leftHand_ThumbProximal", _target.leftHand_ThumbProximal_rotation, 0, 1);
        _target.leftHand_ThumbIntermediate_rotation = EditorGUILayout.Slider("leftHand_ThumbIntermediate", _target.leftHand_ThumbIntermediate_rotation, 0, 1);
        _target.leftHand_ThumbDistal_rotation = EditorGUILayout.Slider("leftHand_ThumbDistal", _target.leftHand_ThumbDistal_rotation, 0, 1);
        _target.leftHand_IndexProximal_rotation = EditorGUILayout.Slider("leftHand_IndexProximal", _target.leftHand_IndexProximal_rotation, 0, 1);
        _target.leftHand_IndexIntermediate_rotation = EditorGUILayout.Slider("leftHand_IndexIntermediate", _target.leftHand_IndexIntermediate_rotation, 0, 1);
        _target.leftHand_IndexDistal_rotation = EditorGUILayout.Slider("leftHand_IndexDistal", _target.leftHand_IndexDistal_rotation, 0, 1);
        _target.leftHand_MiddleProximal_rotation = EditorGUILayout.Slider("leftHand_MiddleProximal", _target.leftHand_MiddleProximal_rotation, 0, 1);
        _target.leftHand_MiddleIntermediate_rotation = EditorGUILayout.Slider("leftHand_MiddleIntermediate", _target.leftHand_MiddleIntermediate_rotation, 0, 1);
        _target.leftHand_MiddleDistal_rotation = EditorGUILayout.Slider("leftHand_MiddleDistal", _target.leftHand_MiddleDistal_rotation, 0, 1);
        _target.leftHand_RingProximal_rotation = EditorGUILayout.Slider("leftHand_RingProximal", _target.leftHand_RingProximal_rotation, 0, 1);
        _target.leftHand_RingIntermediate_rotation = EditorGUILayout.Slider("leftHand_RingIntermediate", _target.leftHand_RingIntermediate_rotation, 0, 1);
        _target.leftHand_RingDistal_rotation = EditorGUILayout.Slider("leftHand_RingDistal", _target.leftHand_RingDistal_rotation, 0, 1);
        _target.leftHand_LittleProximal_rotation = EditorGUILayout.Slider("leftHand_LittleProximal", _target.leftHand_LittleProximal_rotation, 0, 1);
        _target.leftHand_LittleIntermediate_rotation = EditorGUILayout.Slider("leftHand_LittleIntermediate", _target.leftHand_LittleIntermediate_rotation, 0, 1);
        _target.leftHand_LittleDistal_rotation = EditorGUILayout.Slider("leftHand_LittleDistal", _target.leftHand_LittleDistal_rotation, 0, 1);




        EditorGUILayout.Separator();

        rightHand_ThumbProximal = _target.rightHand_ThumbProximal;
        if (rightHand_ThumbProximal)
        {
            rightHand_ThumbProximal.localRotation = Quaternion.Slerp(Quaternion.identity, Quaternion.Euler(45, 45, 0), _target.rightHand_ThumbProximal_rotation);
            mTarget.FindProperty("rightHand_ThumbProximal").objectReferenceValue = rightHand_ThumbProximal;
        }
        rightHand_ThumbIntermediate = _target.rightHand_ThumbIntermediate;
        if (rightHand_ThumbIntermediate)
        {
            rightHand_ThumbIntermediate.localRotation = Quaternion.Slerp(Quaternion.identity, Quaternion.Euler(45, 45, 0), _target.rightHand_ThumbIntermediate_rotation);
            mTarget.FindProperty("rightHand_ThumbIntermediate").objectReferenceValue = rightHand_ThumbIntermediate;
        }
        rightHand_ThumbDistal = _target.rightHand_ThumbDistal;
        if (rightHand_ThumbDistal)
        {
            rightHand_ThumbDistal.localRotation = Quaternion.Slerp(Quaternion.identity, Quaternion.Euler(45, 45, 0), _target.rightHand_ThumbDistal_rotation);
            mTarget.FindProperty("rightHand_ThumbDistal").objectReferenceValue = rightHand_ThumbDistal;
        }
        rightHand_IndexProximal = _target.rightHand_IndexProximal;
        if (rightHand_IndexProximal)
        {
            rightHand_IndexProximal.localRotation = Quaternion.Slerp(Quaternion.Euler(-10, 0, 0), Quaternion.Euler(10, 0, -90), _target.rightHand_IndexProximal_rotation);
            mTarget.FindProperty("rightHand_IndexProximal").objectReferenceValue = rightHand_IndexProximal;
        }
        rightHand_IndexIntermediate = _target.rightHand_IndexIntermediate;
        if (rightHand_IndexIntermediate)
        {
            rightHand_IndexIntermediate.localRotation = Quaternion.Slerp(Quaternion.identity, Quaternion.Euler(0, 0, -110), _target.rightHand_IndexIntermediate_rotation);
            mTarget.FindProperty("rightHand_IndexIntermediate").objectReferenceValue = rightHand_IndexIntermediate;
        }
        rightHand_IndexDistal = _target.rightHand_IndexDistal;
        if (rightHand_IndexDistal)
        {
            rightHand_IndexDistal.localRotation = Quaternion.Slerp(Quaternion.identity, Quaternion.Euler(0, 0, -90), _target.rightHand_IndexDistal_rotation);
            mTarget.FindProperty("rightHand_IndexDistal").objectReferenceValue = rightHand_IndexDistal;
        }
        rightHand_MiddleProximal = _target.rightHand_MiddleProximal;
        if (rightHand_MiddleProximal)
        {
            rightHand_MiddleProximal.localRotation = Quaternion.Slerp(Quaternion.identity, Quaternion.Euler(0, 0, -90), _target.rightHand_MiddleProximal_rotation);
            mTarget.FindProperty("rightHand_MiddleProximal").objectReferenceValue = rightHand_MiddleProximal;
        }
        rightHand_MiddleIntermediate = _target.rightHand_MiddleIntermediate;
        if (rightHand_MiddleIntermediate)
        {
            rightHand_MiddleIntermediate.localRotation = Quaternion.Slerp(Quaternion.identity, Quaternion.Euler(0, 0, -110), _target.rightHand_MiddleIntermediate_rotation);
            mTarget.FindProperty("rightHand_MiddleIntermediate").objectReferenceValue = rightHand_MiddleIntermediate;
        }
        rightHand_MiddleDistal = _target.rightHand_MiddleDistal;
        if (rightHand_MiddleDistal)
        {
            rightHand_MiddleDistal.localRotation = Quaternion.Slerp(Quaternion.identity, Quaternion.Euler(0, 0, -90), _target.rightHand_MiddleDistal_rotation);
            mTarget.FindProperty("rightHand_MiddleDistal").objectReferenceValue = rightHand_MiddleDistal;
        }
        rightHand_RingProximal = _target.rightHand_RingProximal;
        if (rightHand_RingProximal)
        {
            rightHand_RingProximal.localRotation = Quaternion.Slerp(Quaternion.Euler(5, 0, 0), Quaternion.Euler(-5, 0, -90), _target.rightHand_RingProximal_rotation);
            mTarget.FindProperty("rightHand_RingProximal").objectReferenceValue = rightHand_RingProximal;
        }
        rightHand_RingIntermediate = _target.rightHand_RingIntermediate;
        if (rightHand_RingIntermediate)
        {
            rightHand_RingIntermediate.localRotation = Quaternion.Slerp(Quaternion.identity, Quaternion.Euler(0, 0, -110), _target.rightHand_RingIntermediate_rotation);
            mTarget.FindProperty("rightHand_RingIntermediate").objectReferenceValue = rightHand_RingIntermediate;
        }
        rightHand_RingDistal = _target.rightHand_RingDistal;
        if (rightHand_RingDistal)
        {
            rightHand_RingDistal.localRotation = Quaternion.Slerp(Quaternion.identity, Quaternion.Euler(0, 0, -90), _target.rightHand_RingDistal_rotation);
            mTarget.FindProperty("rightHand_RingDistal").objectReferenceValue = rightHand_RingDistal;
        }
        rightHand_LittleProximal = _target.rightHand_LittleProximal;
        if (rightHand_LittleProximal)
        {
            rightHand_LittleProximal.localRotation = Quaternion.Slerp(Quaternion.Euler(10, 0, 0), Quaternion.Euler(-10, 0, -90), _target.rightHand_LittleProximal_rotation);
            mTarget.FindProperty("rightHand_LittleProximal").objectReferenceValue = rightHand_LittleProximal;
        }
        rightHand_LittleIntermediate = _target.rightHand_LittleIntermediate;
        if (rightHand_LittleIntermediate)
        {
            rightHand_LittleIntermediate.localRotation = Quaternion.Slerp(Quaternion.identity, Quaternion.Euler(0, 0, -110), _target.rightHand_LittleIntermediate_rotation);
            mTarget.FindProperty("rightHand_LittleIntermediate").objectReferenceValue = rightHand_LittleIntermediate;
        }
        rightHand_LittleDistal = _target.rightHand_LittleDistal;
        if (rightHand_LittleDistal)
        {
            rightHand_LittleDistal.localRotation = Quaternion.Slerp(Quaternion.identity, Quaternion.Euler(0, 0, -90), _target.rightHand_LittleDistal_rotation);
            mTarget.FindProperty("rightHand_LittleDistal").objectReferenceValue = rightHand_LittleDistal;
        }




        _target.rightHand_ThumbProximal_rotation = EditorGUILayout.Slider("rightHand_ThumbProximal", _target.rightHand_ThumbProximal_rotation, 0, 1);
        _target.rightHand_ThumbIntermediate_rotation = EditorGUILayout.Slider("rightHand_ThumbIntermediate", _target.rightHand_ThumbIntermediate_rotation, 0, 1);
        _target.rightHand_ThumbDistal_rotation = EditorGUILayout.Slider("rightHand_ThumbDistal", _target.rightHand_ThumbDistal_rotation, 0, 1);
        _target.rightHand_IndexProximal_rotation = EditorGUILayout.Slider("rightHand_IndexProximal", _target.rightHand_IndexProximal_rotation, 0, 1);
        _target.rightHand_IndexIntermediate_rotation = EditorGUILayout.Slider("rightHand_IndexIntermediate", _target.rightHand_IndexIntermediate_rotation, 0, 1);
        _target.rightHand_IndexDistal_rotation = EditorGUILayout.Slider("rightHand_IndexDistal", _target.rightHand_IndexDistal_rotation, 0, 1);
        _target.rightHand_MiddleProximal_rotation = EditorGUILayout.Slider("rightHand_MiddleProximal", _target.rightHand_MiddleProximal_rotation, 0, 1);
        _target.rightHand_MiddleIntermediate_rotation = EditorGUILayout.Slider("rightHand_MiddleIntermediate", _target.rightHand_MiddleIntermediate_rotation, 0, 1);
        _target.rightHand_MiddleDistal_rotation = EditorGUILayout.Slider("rightHand_MiddleDistal", _target.rightHand_MiddleDistal_rotation, 0, 1);
        _target.rightHand_RingProximal_rotation = EditorGUILayout.Slider("rightHand_RingProximal", _target.rightHand_RingProximal_rotation, 0, 1);
        _target.rightHand_RingIntermediate_rotation = EditorGUILayout.Slider("rightHand_RingIntermediate", _target.rightHand_RingIntermediate_rotation, 0, 1);
        _target.rightHand_RingDistal_rotation = EditorGUILayout.Slider("rightHand_RingDistal", _target.rightHand_RingDistal_rotation, 0, 1);
        _target.rightHand_LittleProximal_rotation = EditorGUILayout.Slider("rightHand_LittleProximal", _target.rightHand_LittleProximal_rotation, 0, 1);
        _target.rightHand_LittleIntermediate_rotation = EditorGUILayout.Slider("rightHand_LittleIntermediate", _target.rightHand_LittleIntermediate_rotation, 0, 1);
        _target.rightHand_LittleDistal_rotation = EditorGUILayout.Slider("rightHand_LittleDistal", _target.rightHand_LittleDistal_rotation, 0, 1);
    }
}



//SerializedProperty leftArm_Shoulder = mTarget.FindProperty("leftArm_Shoulder");
//leftArm_Shoulder.objectReferenceValue = highestChild;