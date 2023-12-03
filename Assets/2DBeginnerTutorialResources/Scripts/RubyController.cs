using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

﻿public class RubyController : MonoBehaviour
{
    public float speed = 3.0f;
    
    public int maxHealth = 5;
    
    public GameObject projectilePrefab;
    public GameObject HealthEffectPrefab;
    public GameObject HitEffectPrefab;
    //public GameObject GameOverText;
    //public GameObject WinScreen;

   




    public AudioClip throwcogClip;
    public AudioClip hitSound;
    public AudioClip dialogue2;

    
    public int health { get { return currentHealth; }}
    int currentHealth;

    int score;

    
    
    public float timeInvincible = 2.0f;
    bool isInvincible;
    float invincibleTimer;
    
    Rigidbody2D rigidbody2d;
    float horizontal;
    float vertical;
    
    Animator animator;
    Vector2 lookDirection = new Vector2(1,0);

    AudioSource audioSource;


    public GameObject endTextGameObject;
    public TextMeshProUGUI gameEndText;
    public bool gameOver;
    
    
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        
        currentHealth = maxHealth;

        audioSource= GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        
        Vector2 move = new Vector2(horizontal, vertical);
        
        if(!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }
        
        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);
        
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }
        
        if(Input.GetKeyDown(KeyCode.C))
        {
            Launch();
            
        }

        //Other npcs besides Jambi have their own key bindings assigned to them
        //I think a better way for this would be to have a bunch of true and false statements for each npc
        //for the time being tho and because I wanted to avoid breaking anything with the original talking raycast script
        //all npcs have their own keybindings for the time being
        //so rabbit npc can be interacted with the z key
        //pengu npc that doesn't give a quest hint = v key
        //pengu npc with quest + chest hint = f key

        if(Input.GetKeyDown(KeyCode.X))
        {
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));
            if (hit.collider != null)
            {
                NonPlayerCharacter character = hit.collider.GetComponent<NonPlayerCharacter>();
                if (character != null)
                {
                    character.DisplayDialog();
                    PlaySound(dialogue2);
                }
            }

        }

        if(Input.GetKeyDown(KeyCode.Z))
        {
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("RabbitNPC"));
            if (hit.collider != null)
            {
                RabbitNPC character = hit.collider.GetComponent<RabbitNPC>();
                if (character != null)
                {
                    character.DisplayDialog();
                    PlaySound(dialogue2);
                }
            }
        }

        if(Input.GetKeyDown(KeyCode.V))
        {
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("PenguNPC"));
            if (hit.collider != null)
            {
                PenguNPC character = hit.collider.GetComponent<PenguNPC>();
                if (character != null)
                {
                    character.DisplayDialog();
                    PlaySound(dialogue2);
                }
            }
        }

        if(Input.GetKeyDown(KeyCode.F))
        {
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("PenguQuest"));
            if (hit.collider !=null)
            {
                PenguQuest character = hit.collider.GetComponent<PenguQuest>();
                if (character != null)
                {
                    character.DisplayDialog();
                    PlaySound(dialogue2);
                }
            }
        }

        if(!gameOver && health <= 0)
        {
            gameOver = true; 
            endTextGameObject.SetActive(true);
            gameEndText.text = ("You lost! Press R to Restart!");

        }

    }

//if(currentHealth < 0)
       // {
        //    gameOverText.SetActive(true);
         //   gameOverText.text = "You lost! Press R to Restart!";
       // }
       //  if (Input.GetKey(KeyCode.R))

      //  {

        //    if (gameOver == true)

          //  {

           //   SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // this loads the currently active scene

           // }

      //  }
       // if (currentScore = 2)
       // {
       //     gameOverText.SetActive(false);
       //     gameOverText.text = "You won! Game Created by Group 5.";
       // }
   // }
    
    void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;

        rigidbody2d.MovePosition(position);
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (isInvincible)
                return;
            
            isInvincible = true;
            invincibleTimer = timeInvincible;

            //GameObject hiteffect = Instantiate(HitEffectPrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);
            //this is suppose to trigger the hit effect but instead triggers the health effect particles
            //we're running out of time so I just kind of left this in and commented it out

            PlaySound(hitSound);
        }
        
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        GameObject healtheffect = Instantiate(HealthEffectPrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);
        //this works unlike the hit effect prefab. would not recommend changing this

        UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);
    }

    public void ChangeScore(int amount)
    {
        score += amount;

    }

//insert restart function here when we return to this project

    
    void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);

        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDirection, 300);

        animator.SetTrigger("Launch");
        
        PlaySound(throwcogClip);
        
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

}