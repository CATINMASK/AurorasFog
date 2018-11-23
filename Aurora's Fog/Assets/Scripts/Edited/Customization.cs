//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Customization : MonoBehaviour {

    private GameObject playerCamera;
    public GameObject playerHead;
    public GameObject customizationPanel;
    public GameObject mirrorCamera;

    public SkinnedMeshRenderer maleRenderer;
    public Texture[] eyeColors;
    public Material eyeMaterial;
    private int eyeColorIndex = 0;
    private int skinColorIndex = 0;
    public Material skinMaterial;
    public Color[] skinColors;

    public AudioClip gaben;
    public bool canConfirm;
    public GameObject confirmBtn;
    public byte traitsPoints = 1;
    public GameObject nameInput;

    private void Awake()
    {
        CheckPointsCount();
        SkinCleanColor();
        customizationPanel.transform.Find("attributes").Find("playerName").GetComponent<InputField>().text = SystemInfo.deviceName;
        playerCamera = Camera.main.gameObject;
    }

    void Update()
    {
        SetFOV();
        ChangePanel();
    }
    
    public void StartCustomization()
    {
        GameObject.Find("customizationUI").transform.Find("char_pnl").gameObject.SetActive(true);
        GameObject.Find("GameStateMachine").GetComponent<GameStateMachine>().SetState("customization");
        playerCamera.GetComponent<Camera>().enabled = false;
        mirrorCamera.GetComponent<Camera>().enabled = true;
    }

    public void PlaySound(AudioClip clip)                       // For playing sounds from UI buttons.
    {
        GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().clip = clip;
        GetComponent<AudioSource>().Play();
    }

    public void SetFOV()                                        // For changing camera's FOV.
    {
        float playerFOV = mirrorCamera.GetComponent<Camera>().fieldOfView;
        float mouseWheel = Input.GetAxis("Mouse ScrollWheel");
        if (mouseWheel >= 0 && playerFOV >= 20)
            mirrorCamera.GetComponent<Camera>().fieldOfView -= Time.deltaTime * 100;
        if (mouseWheel <= 0 && playerFOV <= 100)
            mirrorCamera.GetComponent<Camera>().fieldOfView += Time.deltaTime * 100;
    }

    // - - - Customization methods - - -

    public void SetHeadType1(float value)
    {
        maleRenderer.SetBlendShapeWeight(11, value);
    }
    public void SetHeadType2(float value)
    {
        maleRenderer.SetBlendShapeWeight(12, value);
    }
    public void SetHeadType3(float value)
    {
        maleRenderer.SetBlendShapeWeight(13, value);
    }
    public void SetHeadType4(float value)
    {
        maleRenderer.SetBlendShapeWeight(14, value);
    }

    public void IncreaseSTR()       // Changes (INC) body size of the character (based on STR).
    {
        float thinValue = maleRenderer.GetBlendShapeWeight(15);
        float muscularValue = maleRenderer.GetBlendShapeWeight(0);
        if (GameObject.Find("Player").GetComponent<PlayerData>().strength < 10)
        {
            if (GameObject.Find("Player").GetComponent<PlayerData>().strength < 6)
                maleRenderer.SetBlendShapeWeight(15, thinValue - 25);
            else
                maleRenderer.SetBlendShapeWeight(0, muscularValue + 10);
        }

        if (GameObject.Find("Player").GetComponent<PlayerData>().strength == 10)
        {
            maleRenderer.SetBlendShapeWeight(15, 0);
            maleRenderer.SetBlendShapeWeight(0, 40);
        }
    }
    public void DecreaseSTR()       // Changes (DEC) body size of the character (based on STR).
    {
        float thinValue = maleRenderer.GetBlendShapeWeight(15);
        float muscularValue = maleRenderer.GetBlendShapeWeight(0);
        if (GameObject.Find("Player").GetComponent<PlayerData>().strength > 1) {
            if (GameObject.Find("Player").GetComponent<PlayerData>().strength < 6)
                maleRenderer.SetBlendShapeWeight(15, thinValue + 25);
            else
                maleRenderer.SetBlendShapeWeight(0, muscularValue - 10);
        }
        if (GameObject.Find("Player").GetComponent<PlayerData>().strength == 1)
        {
            maleRenderer.SetBlendShapeWeight(15, 100);
            maleRenderer.SetBlendShapeWeight(0, 0);
        }
    }

    public void EyeColorUp()
    {
        if (eyeColorIndex < eyeColors.Length - 1)
            eyeColorIndex++;
        else
            eyeColorIndex = 0;
        eyeMaterial.SetTexture("_BaseColorMap", eyeColors[eyeColorIndex]);
    }
    public void SKinColorUp()
    {
        if (skinColorIndex < skinColors.Length - 1)
            skinColorIndex++;
        else
            skinColorIndex = 0;
        skinMaterial.SetColor("_BaseColor", skinColors[skinColorIndex]);
    }
    void SkinCleanColor()       // Cleans skin color (after debugging in editor).
    {
        skinMaterial.SetColor("_BaseColor", skinColors[0]);
    }

    public void EasterEgg()
    {
        int value = Random.Range(0, 101);
        if (GameObject.Find("Player").GetComponent<PlayerData>().intelligence == 3 && value == 3)
            PlaySound(gaben);      
    }

    public void ConfirmCharacter()
    {
        if (canConfirm)
        {
            GameObject.Find("customizationUI").transform.Find("char_pnl").gameObject.SetActive(false);
            GameObject.Find("customizationUI").transform.Find("traits_pnl").gameObject.SetActive(true);
        }
            
        else if (Input.GetKeyDown(KeyCode.E) && canConfirm)
        {
            GameObject.Find("customizationUI").transform.Find("char_pnl").gameObject.SetActive(false);
            GameObject.Find("customizationUI").transform.Find("traits_pnl").gameObject.SetActive(true);
        }
    }
    public void GoBack()
    {
        GameObject.Find("customizationUI").transform.Find("char_pnl").gameObject.SetActive(true);
        GameObject.Find("customizationUI").transform.Find("traits_pnl").gameObject.SetActive(false);
    }
    public void SetTrait(int traitNumber)
    {
        PlayerData playerDataST = GameObject.Find("Player").GetComponent<PlayerData>();
        maleRenderer.SetBlendShapeWeight(5, 0);
        maleRenderer.SetBlendShapeWeight(6, 60);
        playerDataST.fatty = false;
        playerDataST.liar = false;
        playerDataST.gifted = false;
        maleRenderer.SetBlendShapeWeight(10, 0);
        traitsPoints = 0;
        GameObject.Find("traits_txt").GetComponent<Text>().text = traitsPoints.ToString();

        if(traitNumber == 0)
        {
            maleRenderer.SetBlendShapeWeight(10, 50);
            GameObject.Find("Player").GetComponent<PlayerData>().fatty = true;
        }
        else if (traitNumber == 1)
        {
            GameObject.Find("Player").GetComponent<PlayerData>().liar = true;
        }
        else if (traitNumber == 2)
        {
            maleRenderer.SetBlendShapeWeight(5, 60);
            maleRenderer.SetBlendShapeWeight(6, 80);
            GameObject.Find("Player").GetComponent<PlayerData>().gifted = true;
        }
    }

    private void ChangePanel()              // For changing UI Customization/Traits panel
    {
        if (Input.GetKeyDown(KeyCode.E) && canConfirm && GameObject.Find("customizationUI").transform.Find("char_pnl").gameObject.activeSelf)
        {
            GameObject.Find("customizationUI").transform.Find("char_pnl").gameObject.SetActive(false);
            GameObject.Find("customizationUI").transform.Find("traits_pnl").gameObject.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Tab) && GameObject.Find("customizationUI").transform.Find("traits_pnl").gameObject.activeSelf)
            GoBack();
        if (Input.GetKeyDown(KeyCode.Return) && GameObject.Find("customizationUI").transform.Find("traits_pnl").gameObject.activeSelf)
            FinishCustomization();
    }

    public void CheckPointsCount()          // If points == 0 then we can confirm our character.
    {
        if (GameObject.Find("Player").GetComponent<PlayerData>().points == 0)
            canConfirm = true;
        else
            canConfirm = false;
        confirmBtn.SetActive(canConfirm);
    }

    public void FinishCustomization()
    {
        GameObject.Find("customizationUI").transform.Find("char_pnl").gameObject.SetActive(false);
        GameObject.Find("customizationUI").transform.Find("traits_pnl").gameObject.SetActive(false);
        playerCamera.GetComponent<Camera>().enabled = true;
        mirrorCamera.GetComponent<Camera>().enabled = false;
        GameObject.Find("GameStateMachine").GetComponent<GameStateMachine>().SetState("game");
        GetComponent<BoxCollider>().enabled = false;
        canConfirm = false;

        if (GameObject.Find("Player").GetComponent<PlayerData>().fatty == true)
            GameObject.Find("Player").GetComponent<PlayerData>().charisma++;
        GetComponent<Customization>().enabled = false;
    }

    // Applying modification of materials and so on...
    /*
    void ApplyModification(AppearenceDetails detail, int id)
    {
        switch (detail)
        {
            case AppearenceDetails.HAIR_MODEL:
                if (activeHair != null)
                    GameObject.Destroy(activeHair);

                activeHair = GameObject.Instantiate(hairModels[id]);
                activeHair.transform.SetParent(headAnchor);
                activeHair.transform.ResetTransform();
                break;

            case AppearenceDetails.SKIN_COLOR:
                armsMat.SetColor("_Color2", skinColors[id]);
                //skinRenderer.material.color = skinColors[id];
                //armsMat.color = skinColors[id];
                faceMat.SetColor("_Color2", skinColors[id]);
                gensMat.SetColor("_Color2", skinColors[id]);
                legsMat.SetColor("_Color2", skinColors[id]);
                torsoMat.SetColor("_Color2", skinColors[id]);
                break;

            case AppearenceDetails.HAIR_COLOR:
                hairMat.SetColor("_HairTint", hairColors[id]);
                break;

            case AppearenceDetails.EYE_COLOR:
                eyeMat.mainTexture = eyeColors[id];
                break;
        }
    }
    */
}
