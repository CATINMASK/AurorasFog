using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour {

    public Text dialogueText;
    private float delay;        // Delay for text.

    public string phrase0 = "Oh, hello my dear! Wanna have fun?";
    public string phrase1 = "Ah, hello-hello darling. You'r finally awake...";

    private void Update()
    {
        //SkipText();
    }

    private void TextPrinter(string text)
    {
        delay = text.Length / 15f;
        StartCoroutine("TextPrinting", text);
        StartCoroutine("CleanText");
        //StartCoroutine(TextPrinting(text));
        //StartCoroutine(CleanText(delay));
    }

    public IEnumerator TextPrinting(string input)
    {
        for (int i = 0; i < input.Length; i++)
        {
            dialogueText.text += input[i];
            yield return new WaitForSeconds(0.025f);
        }
    }
    public IEnumerator CleanText()
    {
        yield return new WaitForSeconds(delay);
        dialogueText.text = "";
    }

    public void PrintText(int text)
    {
        GameStateMachine stateMachineST = GameObject.Find("GameStateMachine").GetComponent<GameStateMachine>();
        stateMachineST.SetState("cutscene");
        if (text == 0)
        {
            TextPrinter(phrase0);
            //StartCoroutine("stateMachineST.SetStateWithDelay", 3.2f, "poses");
            StartCoroutine("SetStateWithDelay", "poses");
            //StartCoroutine(GameObject.Find("GameStateMachine").GetComponent<GameStateMachine>().SetStateWithDelay(3.2f, "poses"));
            GameObject.Find("Alice").GetComponent<ExpressionsManager>().smiling = true;
        }
        if (text == 1)
        {
            TextPrinter(phrase1);
            StartCoroutine("SetStateWithDelay", "lilithPoses");
            //StartCoroutine(GameObject.Find("GameStateMachine").GetComponent<GameStateMachine>().SetStateWithDelay(3.2f, "lilithPoses"));
            GameObject.Find("Lilith").GetComponent<ExpressionsManager>().smiling = true;
        }
    }

    public void SkipText()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            StopCoroutine("TextPrinting");
            StopCoroutine("CleanText");
            StopCoroutine("SetStateWithDelay");
            dialogueText.text = "";
        }
    }

    public IEnumerator SetStateWithDelay(string state)
    {
        yield return new WaitForSeconds(delay);
        GameObject.Find("GameStateMachine").GetComponent<GameStateMachine>().SetState(state);
    }
}
