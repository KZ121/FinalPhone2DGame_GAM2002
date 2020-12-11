using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public float knockbackPower = 100;
    public float knockbackDuration = 1;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            StartCoroutine(NewMovement.instance.Knockback(knockbackDuration, knockbackPower, this.transform));
        }
    }
    
    
    private NewMovement player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<NewMovement>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            player.Damage(1);
            StartCoroutine(NewMovement.instance.Knockback(knockbackDuration, knockbackPower, this.transform));
        }
    }
    
        
    
}
