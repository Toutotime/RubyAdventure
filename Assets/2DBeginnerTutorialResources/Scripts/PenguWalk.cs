using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenguWalk : MonoBehaviour
{
    public float speed;
    public float changeTime = 3.0f;
    float timerDisplay;

    Rigidbody2D rigidbody2D;
    float timer;
    int direction = 1;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
       
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }

       }
        void FixedUpdate()
    {
        Vector2 position = rigidbody2D.position;
        position.x = position.x + Time.deltaTime * speed * direction;
        
        rigidbody2D.MovePosition(position);
    }
    }
    
   



