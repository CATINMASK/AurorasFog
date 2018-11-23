using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sex : MonoBehaviour {

    public GameObject player;
    public GameObject camera;

    public GameObject alice;
    public GameObject aliceHead;
    public GameObject catPanty;

    public GameObject lilith;
    public GameObject lilithHead;
    public GameObject lilithPanty;


    public bool startedSex;
    public float orgasmMeter;

    public GameObject speedUpText;
    public GameObject cumText;
    public float sexSpeed = 0.75f;
    public float maxSpeed = 2.5f;
    public bool canSpeedUp;
    public bool canCum;
    public bool startCumming;
    public float sexTime;
    public bool startSlowingDown;
    public bool cummed;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        //alice  = GameObject.FindWithTag("Alice");
        camera = GameObject.FindWithTag("MainCamera");
    }

    private void Update()
    {
        if (startedSex)
        {
            sexTime += Time.deltaTime;
            player.transform.GetChild(0).GetComponent<Animator>().speed = sexSpeed;
            alice.GetComponent<Animator>().speed = sexSpeed;
            lilith.GetComponent<Animator>().speed = sexSpeed;
        }
        if (sexTime >= 12.0f && sexSpeed < maxSpeed)
            canSpeedUp = true;

        if (!cummed)
            speedUpText.SetActive(canSpeedUp);
        else
            speedUpText.SetActive(false);

        if (Input.GetKeyDown(KeyCode.Q) && canSpeedUp && sexSpeed < maxSpeed && !cummed)
        {
            sexSpeed += 0.25f;
            sexTime = 0.0f;
            canSpeedUp = false;
        }
        if (sexSpeed >= 1.75f & !cummed)
        {
            canCum = true;
            cumText.SetActive(true);
        }

        if (canCum && Input.GetKeyDown(KeyCode.Space) && !cummed)
        {
            GameObject.Find("sexSounds").GetComponent<SexSounds>().LastMoan();
            StartCoroutine(GameObject.Find("dialogueManager").GetComponent<Dialogue>().TextPrinting("That was...amasing!"));
            StartCoroutine(GameObject.Find("dialogueManager").GetComponent<Dialogue>().CleanText());
            startSlowingDown = true;
            cummed = true;
            speedUpText.SetActive(false);
            cumText.SetActive(false);
        }
        if (startSlowingDown && sexSpeed >= 0)
        {
            sexSpeed -= Time.deltaTime * 0.2f;
        }

        if (startedSex && orgasmMeter <= 100.0f)
        {
            orgasmMeter += Time.deltaTime * 1.25f;
        }
    }

    public void StartSex(string pose) {

        StartCoroutine(SetPose(pose));
    }

    IEnumerator SetPose(string pose)
    {
        if ((pose == "balance" || pose == "wildride2" || pose == "wildride2Lilith" || pose == "balanceLilith") && PlayerPrefs.GetInt("additionalPoses") != 1)
        {
            StopAllCoroutines();
            yield return 0;
        }
        GameObject.Find("GameStateMachine").GetComponent<GameStateMachine>().SetState("sex");
        camera.transform.SetParent(GameObject.FindWithTag("playerHead").transform);
        if (cummed)
        {
            startSlowingDown = false;
            cummed = false;
            canCum = false;
            sexSpeed = 0.5f;
            orgasmMeter = 0.0f;
            sexTime = 0.0f;
        }
        GameObject.Find("eyesPPV").GetComponent<Animator>().Play("closeEyes");
        yield return new WaitForSeconds(0.1f);
        lilith.SetActive(true);
        alice.SetActive(true);
        startedSex = true;

        alice.GetComponent<ExpressionsManager>().SetFaceNull();
        alice.GetComponent<CapsuleCollider>().enabled = false;
        alice.GetComponent<Animator>().Play(pose);

        lilith.GetComponent<ExpressionsManager>().SetFaceNull();
        lilith.GetComponent<CapsuleCollider>().enabled = false;
        lilith.GetComponent<Animator>().Play(pose);

        player.transform.GetChild(0).GetComponent<Animator>().Play(pose);
        catPanty.SetActive(false);
        lilithPanty.SetActive(false);

        yield return new WaitForSeconds(0.1f);

        //camera.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        camera.GetComponent<FPCamera>().xClampMin = -85;
        camera.GetComponent<FPCamera>().xClampMax = 85;
        aliceHead.GetComponent<HeadRotate>().lookAt = false;
        lilithHead.GetComponent<HeadRotate>().lookAt = false;

        GameObject.Find("sexSounds").GetComponent<SexSounds>().StopMoan();
        GameObject.Find("sexSounds").GetComponent<SexSounds>().StartMoan();

        switch (pose)
        {
            case "lift":
                lilith.SetActive(false);
                alice.SetActive(true);
                camera.GetComponent<FPCamera>().xClampMax = 45;
                aliceHead.GetComponent<HeadRotate>().lookAt = true;
                alice.transform.position = new Vector3(1.51f, 0.618f, -2.027f);
                alice.transform.rotation = Quaternion.Euler(0f, 0f, 0f);

                player.transform.position = new Vector3(1.51f, 0.618f, -1.72f);
                player.transform.rotation = Quaternion.Euler(0f, 180f, 0f);

                GameObject.Find("Gen1").GetComponent<Animator>().Play("dickErect");
                break;
            case "puppy":
                lilith.SetActive(false);
                alice.SetActive(true);
                camera.GetComponent<FPCamera>().xClampMax = 45;
                alice.transform.position = new Vector3(1.545518f, 0.6542414f, -1.893004f);
                alice.transform.rotation = Quaternion.Euler(0f, -194.711f, 0f);

                player.transform.position = new Vector3(1.51f, 0.618f, -1.5296f);
                player.transform.rotation = Quaternion.Euler(0f, 180f, 0f);

                GameObject.Find("Gen1").GetComponent<Animator>().Play("dickErect");
                break;
            case "wildride":
                lilith.SetActive(false);
                alice.SetActive(true);
                camera.GetComponent<FPCamera>().xClampMin = -37;

                aliceHead.GetComponent<HeadRotate>().lookAt = true;

                alice.transform.position = new Vector3(1.51f, 0.618f, -2.027f);
                alice.transform.rotation = Quaternion.Euler(0f, 180f, 0f);

                player.transform.position = new Vector3(1.51f, 0.6f, -1.8637f);
                player.transform.rotation = Quaternion.Euler(0f, 0f, 0f);

                GameObject.Find("Gen1").GetComponent<Animator>().Play("dickErect2");

                break;
            case "balance":
                if (PlayerPrefs.GetInt("additionalPoses") == 1)
                {
                    lilith.SetActive(false);
                    alice.SetActive(true);
                    camera.GetComponent<FPCamera>().xClampMax = 45;
                    aliceHead.GetComponent<HeadRotate>().lookAt = true;
                    alice.transform.position = new Vector3(1.51f, 0f, -0.924803f);
                    alice.transform.rotation = Quaternion.Euler(0f, 0f, 0f);

                    player.transform.position = new Vector3(1.51f, 0f, -0.72f);
                    player.transform.rotation = Quaternion.Euler(0f, 180f, 0f);

                    GameObject.Find("Gen1").GetComponent<Animator>().Play("dickErect2");
                    camera.transform.SetParent(GameObject.FindWithTag("Player").transform);
                }
                break;
            case "wildride2":
                if (PlayerPrefs.GetInt("additionalPoses") == 1)
                {
                    lilith.SetActive(false);
                    alice.SetActive(true);
                    aliceHead.GetComponent<HeadRotate>().lookAt = true;
                    alice.transform.position = new Vector3(1.51f, 0.63f, -1.855f);
                    alice.transform.rotation = Quaternion.Euler(0f, -23f, 0f);

                    player.transform.position = new Vector3(1.51f, 0.618f, -1.72f);
                    player.transform.rotation = Quaternion.Euler(0f, 180f, 0f);

                    GameObject.Find("Gen1").GetComponent<Animator>().Play("dickErect");
                }
                break;
            // Lilith
            case "bounce":
                lilith.SetActive(true);
                alice.SetActive(false);
                GameObject.Find("GameStateMachine").GetComponent<GameStateMachine>().SetState("sexLilith");
                camera.GetComponent<FPCamera>().xClampMin = -37;
                camera.GetComponent<FPCamera>().xClampMax = 60;
                lilithHead.GetComponent<HeadRotate>().lookAt = true;
                lilith.transform.position = new Vector3(1.51f, 0.618f, -1.721f);
                lilith.transform.rotation = Quaternion.Euler(0f, 180f, 0f);

                player.transform.position = new Vector3(1.51f, 0.618f, -1.72f);
                player.transform.rotation = Quaternion.Euler(0f, 0f, 0f);

                GameObject.Find("Gen1").GetComponent<Animator>().Play("dickErect2");
                break;
            case "puppyLilith":
                lilith.SetActive(true);
                alice.SetActive(false);
                GameObject.Find("GameStateMachine").GetComponent<GameStateMachine>().SetState("sexLilith");
                camera.GetComponent<FPCamera>().xClampMax = 45;
                lilith.transform.position = new Vector3(1.545518f, 0.6542414f, -1.893004f);
                lilith.transform.rotation = Quaternion.Euler(0f, -194.711f, 0f);

                player.transform.position = new Vector3(1.51f, 0.618f, -1.5296f);
                player.transform.rotation = Quaternion.Euler(0f, 180f, 0f);

                GameObject.Find("Gen1").GetComponent<Animator>().Play("dickErect2");
                break;
            case "reins":
                lilith.SetActive(true);
                alice.SetActive(false);
                GameObject.Find("GameStateMachine").GetComponent<GameStateMachine>().SetState("sexLilith");
                camera.GetComponent<FPCamera>().xClampMax = 45;
                lilith.transform.position = new Vector3(1.51f, 0.625f, -1.898909f);
                lilith.transform.rotation = Quaternion.Euler(0f, 180f, 0f);

                player.transform.position = new Vector3(1.51f, 0.618f, -1.5296f);
                player.transform.rotation = Quaternion.Euler(0f, 180f, 0f);

                GameObject.Find("Gen1").GetComponent<Animator>().Play("dickErect");
                break;
            case "balanceLilith":
                if (PlayerPrefs.GetInt("additionalPoses") == 1)
                {
                    lilith.SetActive(true);
                    alice.SetActive(false);
                    GameObject.Find("GameStateMachine").GetComponent<GameStateMachine>().SetState("sexLilith");
                    camera.GetComponent<FPCamera>().xClampMax = 45;
                    lilithHead.GetComponent<HeadRotate>().lookAt = true;
                    lilith.transform.position = new Vector3(1.51f, 0f, -0.924803f);
                    lilith.transform.rotation = Quaternion.Euler(0f, 0f, 0f);

                    player.transform.position = new Vector3(1.51f, 0f, -0.72f);
                    player.transform.rotation = Quaternion.Euler(0f, 180f, 0f);

                    GameObject.Find("Gen1").GetComponent<Animator>().Play("dickErect2");
                    camera.transform.SetParent(GameObject.FindWithTag("Player").transform);
                }
                break;
            case "wildride2Lilith":
                if (PlayerPrefs.GetInt("additionalPoses") == 1)
                {
                    lilith.SetActive(true);
                    alice.SetActive(false);
                    GameObject.Find("GameStateMachine").GetComponent<GameStateMachine>().SetState("sexLilith");
                    lilithHead.GetComponent<HeadRotate>().lookAt = true;
                    lilith.transform.position = new Vector3(1.51f, 0.63f, -1.855f);
                    lilith.transform.rotation = Quaternion.Euler(0f, -23f, 0f);

                    player.transform.position = new Vector3(1.51f, 0.618f, -1.72f);
                    player.transform.rotation = Quaternion.Euler(0f, 180f, 0f);

                    GameObject.Find("Gen1").GetComponent<Animator>().Play("dickErect");
                }
                break;
        }
        //StartTiming();
    }

    public void SetOutfit(GameObject outfit)
    {
        outfit.SetActive(!outfit.activeSelf);
    }
}
