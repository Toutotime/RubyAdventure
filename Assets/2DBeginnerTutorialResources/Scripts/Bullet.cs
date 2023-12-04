using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float force;


    Rigidbody2D rigidbody2D;
    float timer;

    public AudioClip alienshoot1;
    public AudioClip hitClip;

    private GameObject rubyController;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        rubyController = GameObject.FindGameObjectWithTag("RubyController");

        Vector3 direction = rubyController.transform.position - transform.position;
        rigidbody2D.velocity = new Vector2(direction.x, direction.y).normalized * force;

    }

    public void shoot(Vector2 direction, float force)
    {
        GetComponent<Rigidbody2D>().AddForce(direction * force);
    }

    void Update()
    {
      if(transform.position.magnitude > 20.0f)
      {
        Destroy(gameObject);
      }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        RubyController player = other.gameObject.GetComponent<RubyController>();

        if (player != null)
        {
            player.ChangeHealth(-2);
            player.PlaySound(hitClip);
            Destroy(gameObject);

        }
    }
  
}
