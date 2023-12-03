using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public GameObject cogRef;
    private bool isInsideTrigger = false;
    private bool isOpen = false;
    private Animator chestAnimatorRef;
    private Transform cogCreateRef;

    // Update is called once per frame
    // I think using something similar to the npc raycast dialogue system will work better for this?
    
    void Update()
    {
        if (isInsideTrigger == true)// is colliding with chest?
        {
            if (Input.GetButtonDown("E"))
            {
                isOpen = !isOpen; //is chest open or not?
                chestAnimatorRef.SetBool("isOpen?", isOpen); //open or closes chest w ani

                //create cog bag
                GameObject cogInstance;
                cogInstance = Instantiate(cogRef, cogCreateRef.position, cogCreateRef.rotation);
                
            }
        }
    }

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.gameObject.CompareTag ("Chest"))// can chest b opened?
        {
            isInsideTrigger = true;

            cogCreateRef = other.transform.parent.Find("CogCreatePoint");
        }
    }

    //if (other == cogRef) // pickup and destroy cog
   //{
        //Destroy (other.gameObject);
    //} Need input later for how to destroy cog bag
    //I think the code for this should be the same as when ruby picks up the health collectable and the obj is destroyed

    void OnTriggerExit2D (Collider2D other)
    {
        if (other.gameObject.CompareTag ("Chest"))//close chest
        {
            isInsideTrigger = false;
        }
    }
}
