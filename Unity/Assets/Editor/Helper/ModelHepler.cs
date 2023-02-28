using ET;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ET
{
  public static class ModelHepler
  {
    [MenuItem("Tools/Model/ǿ�� T-Pose")]
    static void EnforceTPose() => Selection.activeGameObject.GetComponent<Animator>().EnforcePose();

    [MenuItem("Tools/Model/ǿ�� A-Pose")]
    static void EnforceAPose() => Selection.activeGameObject.GetComponent<Animator>().EnforcePose(PoseType.APose);

    [MenuItem("Tools/Model/ǿ�� I-Pose")]
    static void EnforceIPose() => Selection.activeGameObject.GetComponent<Animator>().EnforcePose(PoseType.IPose);

    [MenuItem("Tools/Model/ǿ�� T-Pose", validate = true),
      MenuItem("Tools/Model/ǿ�� A-Pose", validate = true),
      MenuItem("Tools/Model/ǿ�� I-Pose", validate = true),
      MenuItem("Tools/Model/��ǰ����ֵ", validate = true)]
    private static bool EnforceTPoseValidation()
    {
      var root = Selection.activeObject as GameObject;

      if (!root)
        return false;

      var animator = root.GetComponent<Animator>();

      return animator && animator.avatar && animator.avatar.isValid && animator.avatar.isHuman;
    }

    [MenuItem("Tools/Model/��ǰ����ֵ")]
    static void ShowMuscle()
    {
      Animator animator = Selection.activeGameObject.GetComponent<Animator>();

      //Pose
      HumanPoseHandler handler = new HumanPoseHandler(animator.avatar, animator.transform);
      HumanPose humanPose = new HumanPose();
      handler.GetHumanPose(ref humanPose);
      float[] muscle = humanPose.muscles;

      string result = StringHelper.Format("{0}��ǰPoseֵΪ��\n", animator.name);

      for (int i = 0; i < muscle.Length; i++)
        result = StringHelper.Format("{0}\t{1}", muscle[i], result);

      Debug.Log(StringHelper.Format("{0}\n{1}", result, DateTime.Now));
    }
    /// <summary>
    /// ǿ��ת�����ض�Pose
    /// </summary>
    public static void EnforcePose(this Animator animator, PoseType pose = PoseType.TPose)
    {
      if (!animator.isHuman)
        return;

      Avatar avatar = animator.avatar;
      Transform hipsTrans = animator.GetBoneTransform(HumanBodyBones.Hips);
      Vector3 befPos = hipsTrans.localPosition;

      //Pose
      HumanPoseHandler handler = new HumanPoseHandler(avatar, animator.transform);
      HumanPose humanPose = new HumanPose();
      handler.GetHumanPose(ref humanPose);
      switch (pose)
      {
        case PoseType.IPose:
          humanPose.muscles = new float[] { 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0.5840391f, 0.03125687f, -0.222109f, 0.9898758f, 0.158858f, -0.01497263f, 0.0460687f, 0f, 0.5840399f, 0.03125684f, -0.2221078f, 0.989876f, 0.1588574f, -0.01497229f, 0.04606922f, 0f, 0f, 0f, -0.8483878f, 0.1488085f, 0.2308307f, 0.9997126f, 0.03447684f, -0.003179308f, 0.0005712679f, 0f, 0f, -0.8483936f, 0.1488192f, 0.2308263f, 0.9997125f, 0.03449172f, -0.00318067f, 0.0005715376f, -1.267641f, 0.2838888f, -0.1871099f, 0.6461576f, 0.6679524f, -0.457573f, 0.8116842f, 0.8116838f, 0.6679688f, -0.610897f, 0.8116841f, 0.8116841f, 0.667972f, -0.6107138f, 0.8116836f, 0.8116836f, 0.6679349f, -0.4577479f, 0.8116841f, 0.8116841f, -1.173686f, 0.2816136f, -0.2291577f, 0.646016f, 0.6679507f, -0.4575827f, 0.8116841f, 0.8116841f, 0.6679681f, -0.6109114f, 0.8116842f, 0.8116842f, 0.6679713f, -0.6107396f, 0.8116842f, 0.8116839f, 0.667937f, -0.4577253f, 0.8116836f, 0.8116836f };
          break;

        case PoseType.TPose:
        default:
          humanPose.muscles = new float[] { 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0.5841883f, 0.03081854f, -0.4439961f, 1.000057f, 0.3060482f, -0.01486502f, 0.04515859f, 0f, 0.5841888f, 0.03081855f, -0.4439934f, 1.000057f, 0.3060467f, -0.01486458f, 0.04515881f, 0f, 0f, 0f, 0.3959706f, 0.3021692f, -0.04092348f, 0.9999937f, 0.05493694f, -0.003142329f, 0.0005672743f, 0f, 0f, 0.3959692f, 0.3021694f, -0.04094382f, 0.9999936f, 0.0549617f, -0.003143783f, 0.0005676334f, -1.267641f, 0.2838885f, -0.1871098f, 0.6461577f, 0.6679521f, -0.4575731f, 0.8116842f, 0.8116838f, 0.6679688f, -0.6108971f, 0.8116841f, 0.8116841f, 0.6679721f, -0.6107138f, 0.8116837f, 0.8116837f, 0.6679347f, -0.4577479f, 0.8116841f, 0.8116841f, -1.173686f, 0.2816135f, -0.2291577f, 0.6460159f, 0.667951f, -0.4575827f, 0.8116842f, 0.8116842f, 0.6679681f, -0.6109114f, 0.8116842f, 0.8116842f, 0.6679714f, -0.6107396f, 0.8116842f, 0.8116838f, 0.667937f, -0.4577253f, 0.8116838f, 0.8116838f };
          break;
      }
      handler.SetHumanPose(ref humanPose);

      hipsTrans.localPosition = befPos;  //����ת��ʱ����Ӱ��
    }

    public enum PoseType
    {
      APose,
      IPose,
      TPose,
    }
  }
}
