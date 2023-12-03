using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitNPC : MonoBehaviour

{
    public float displayTime =  0.4f;
    public GameObject dialogBox;
    public AudioClip dialogue2;
    float timerDisplay;

    // Start is called before the first frame update
    void Start()
    {
        dialogBox.SetActive (false);
        timerDisplay = -0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        if(timerDisplay >= 0 )
        {
            timerDisplay -= Time.deltaTime;
            if(timerDisplay < 0)
            {
                dialogBox.SetActive(false);
            }
        }
    }
    public void DisplayDialog()
    {
        timerDisplay = displayTime;
        dialogBox.SetActive(true);
    }
}