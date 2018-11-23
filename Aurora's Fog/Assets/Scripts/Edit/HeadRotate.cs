using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadRotate : MonoBehaviour {

    public Transform targetObject;
    public GameObject charObject;
    public bool inTrigger;
    public bool lookAt;             // Should object lookAt target?

    private void Awake()
    {
    }

    private void Update()
    {
        if (lookAt)
            transform.LookAt(targetObject);

        if (inTrigger && (GameObject.Find("GameStateMachine").GetComponent<GameStateMachine>().currentState == "game" || GameObject.Find("GameStateMachine").GetComponent<GameStateMachine>().currentState == "pause" || GameObject.Find("GameStateMachine").GetComponent<GameStateMachine>().currentState == "cutscene"))
        {
            charObject.GetComponent<Animator>().enabled = false;
            Vector3 targetDir = targetObject.position - transform.position;

            // The step size is equal to speed times frame time.
            float step = 5f * Time.deltaTime;

            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDir);
        }
        else
            charObject.GetComponent<Animator>().enabled = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            inTrigger = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            inTrigger = false;
    }
}
