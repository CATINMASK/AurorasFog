using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyIK : MonoBehaviour {

    public Transform myCamera;
    public Animator animator;
    public Transform headObject;
    public Transform lookAtObj;
    public float lookAtWeight = 1;
    public float bodyW = 1;
    public float headW = 1;
    public float eyeW = 1;
    public float clampW = 1;

    private void Start()
    {
        headObject = animator.GetBoneTransform(HumanBodyBones.Head);
    }

    /*
    void OnAnimatorIK()
    {
        if (animator && lookAtObj != null)
        {
            animator.SetLookAtPosition(lookAtObj.position);
            animator.SetLookAtWeight(lookAtWeight);
        }

    }
    */

    private void Update()
    {
        //HeadRotating();
    }

    void LateUpdate()
    {
        Transform neckRotation = animator.GetBoneTransform(HumanBodyBones.Neck);
        neckRotation.rotation = Camera.main.transform.rotation * Quaternion.Euler(-180, 0, 90);
    }

    void OnAnimatorIK()
    {
        /*
        Vector3 eulerRotation = Camera.main.transform.eulerAngles;
        eulerRotation.y -= 90f;
        print(eulerRotation);
        animator.SetBoneLocalRotation(HumanBodyBones.Head, Quaternion.Euler(eulerRotation));
        */
        //animator.SetLookAtPosition(animator.GetBoneTransform(HumanBodyBones.Head).position + myCamera.position);
        
                //animator.SetLookAtPosition(lookAtObj.position);
                //animator.SetLookAtWeight(lookAtWeight, bodyW, headW, eyeW, clampW);
        
        //animator.SetLookAtWeight(lookAtWeight);

        // weight, bodyWeight, headWeight, eyesWeight, clampWeight
    }
}
