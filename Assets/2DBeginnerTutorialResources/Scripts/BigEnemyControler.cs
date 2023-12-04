using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigEnemyControler : MonoBehaviour
{
    public float speed;
    public bool vertical;
    public float changeTime = 3.0f;

    public AudioClip hitClip;
    public AudioClip robotwalkClip;
    
    Rigidbody2D rigidbody2D;
    float timer;
    int direction = 1;

    public bool broken; 

    

    public ParticleSystem smokeEffect;
    public GameObject BEBullet;
    public Transform  bulletPos;

   
    AudioSource audioSource;

    Animator animator;

    private RubyController rubyController;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();   
        timer = changeTime;
        animator = GetComponent<Animator>();
        
        GameObject rubyControllerObject = GameObject.FindWithTag("RubyController"); //this line of code finds the RubyController script by looking for a "RubyController" tag on Ruby


        if (rubyControllerObject != null)

        {

            rubyController = rubyControllerObject.GetComponent<RubyController>(); //and this line of code finds the rubyController and then stores it in a variable

            print ("Found the RubyConroller Script!");

        }

        if (rubyController == null)

        {

            print ("Cannot find GameController Script!");

        }
    } 

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }

        if(timer > 2)
        {
            timer = 1;
            shoot();

        }

        {
            //remember ! inverse the test, so if broken is true !broken will be false and return won't be executed.
        if(!broken)
        {
            return;
        }
        }
    }

   void FixedUpdate()
    {
        //remember ! inverse the test, so if broken is true !broken will be false and return won’t be executed.
        if(!broken)
        {
            return;
        }
        
        Vector2 position = rigidbody2D.position;
        
        if (vertical)
        {
            position.y = position.y + Time.deltaTime * speed * direction;
            animator.SetFloat("Move X", 1);
            animator.SetFloat("Move Y", direction);
        }
        else
        {
            position.x = position.x + Time.deltaTime * speed * direction;
            animator.SetFloat("Move X", direction);
            animator.SetFloat("Move Y", 1);
        }
        
        rigidbody2D.MovePosition(position);
    }

    void shoot()
    {
        Instantiate(BEBullet, bulletPos.position, Quaternion.identity);

    }



    void OnCollisionEnter2D(Collision2D other)
    {
        RubyController player = other.gameObject.GetComponent<RubyController>();

        if (player != null)
        {
            player.ChangeHealth(-3);
            player.PlaySound(hitClip);
            //because big enemy = ruby will take more damage when walking into it -GY
            
        }
         
        
    }

    public void Fix()
    {
        broken = false;
        rigidbody2D.simulated = false;

        animator.SetTrigger("Fixed");
        smokeEffect.Stop();

        if (rubyController != null)
        {
            
             rubyController.ChangeScore(1); //this line of code is increasing Score by 1!
        
        }
    }
     public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);

    } 
}
