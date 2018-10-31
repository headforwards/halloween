using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FontThrob : MonoBehaviour {

    [Tooltip("Minimum size of the font")]
    public int MinSize = 8;

    [Tooltip("Maximum size of the font")]
    public int MaxSize= 60;

    [Tooltip("Time between size changes in seconds")]
    public float StepTime = 0.1f;

    private Text text;
    private int vector = 1;

    void Start () {
        text = gameObject.GetComponent<Text>();

        StartCoroutine(ChangeState());
	}
	
	
    IEnumerator ChangeState()
    {
        yield return new WaitForSeconds(StepTime);

        text.fontSize += vector;

        if(text.fontSize >= MaxSize && vector == 1)
        {
            vector = -1;
        }

        if(text.fontSize <= MinSize && vector == -1)
        {
            vector = 1;
        }

        StartCoroutine(ChangeState());
    }
}
