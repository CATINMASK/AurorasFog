using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpressionsManager : MonoBehaviour {

    public SkinnedMeshRenderer face;

    public bool alice;
    public bool smiling;
    public float smileValue;
    public float orgasmValue;

    private void Awake()
    {
        //face = GetComponent<SkinnedMeshRenderer>();
    }

    private void Update()
    {
        orgasmValue = GameObject.Find("sexController").GetComponent<Sex>().orgasmMeter;
        if(orgasmValue > 0f)
        {
            face.SetBlendShapeWeight(6, orgasmValue);
            face.SetBlendShapeWeight(10, orgasmValue / 4);
            face.SetBlendShapeWeight(14, orgasmValue);
            face.SetBlendShapeWeight(16, orgasmValue / 10);
            face.SetBlendShapeWeight(23, orgasmValue);
        }
        if (smiling)
        {
            if (alice)
            {
                face.SetBlendShapeWeight(1, smileValue);
                if (smileValue < 80.0f)
                    smileValue += Time.deltaTime * 100.0f;
            }
            else
            {
                face.SetBlendShapeWeight(3, smileValue);
                if (smileValue < 100.0f)
                    smileValue += Time.deltaTime * 100.0f;
            }
        }
        if (!smiling)
        {
            if (alice)
            {
                face.SetBlendShapeWeight(1, smileValue);
                if (smileValue > 0.1f)
                    smileValue -= Time.deltaTime * 100.0f;
            }
            else
            {
                face.SetBlendShapeWeight(3, smileValue);
                if (smileValue > 0.1f)
                    smileValue -= Time.deltaTime * 100.0f;
            }
        }
    }

    public void SetFaceNull()
    {
        smiling = false;
        smileValue = 0;
    }
}
