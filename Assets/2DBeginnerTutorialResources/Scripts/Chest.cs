using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public GameObject cogRef;
    private bool isInsideTrigger = false;
    public bool isOpen = false;
    private Animator chestAnimatorRef;
    private Transform cogCreateRef;

    public AudioClip cogcollectClip;
    public AudioClip chestopenClip;
    AudioSource audioSource;

    // Update is called once per frame
    // I think using something similar to the npc raycast dialogue system will work better for this?
    
    void Update()
    {
        if (isInsideTrigger == true)// is colliding with chest?-vh
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                isOpen = !isOpen; //is chest open or not?
                chestAnimatorRef.SetBool("isOpen?", isOpen); //open or closes chest w ani

                if(isOpen == true)
                {
                //create cog bag
                GameObject cogInstance;
                cogInstance = Instantiate(cogRef, cogCreateRef.position, cogCreateRef.rotation) as GameObject;
                
                RubyController controller = GetComponent<RubyController>();
                controller.PlaySound(chestopenClip);
                }
            }
        }
    }

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.gameObject.CompareTag ("Chest"))// can chest b opened?-vh
        {
            isInsideTrigger = true;
            //ref to child of chest
            Transform chestRef = other.transform.parent.Find("chestA");
            Animator chestAnimator = chestRef.GetComponent< Animator >();
            chestAnimatorRef = chestAnimator;

            cogCreateRef = other.transform.parent.Find("CogCreatePoint");
            
            
        }

        if (other.gameObject.CompareTag ("Pickup"))
        {
            Destroy (other.gameObject);

            RubyController controller = GetComponent<RubyController>();
            controller.PlaySound(cogcollectClip);
        }

    }
   


    
    //Need input later for how to destroy cog bag, pickup and destroy- vh
    //I think the code for this should be the same as when ruby picks up the health collectable and the obj is destroyed

    void OnTriggerExit2D (Collider2D other)
    {
        if (other.gameObject.CompareTag ("Chest"))//close chest
        {
            isInsideTrigger = false;
        }
    }
}
