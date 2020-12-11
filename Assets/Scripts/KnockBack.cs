using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    [SerializeField] private float knockbackStrenght;
    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody2D rb = collision.collider.GetComponent<Rigidbody2D>();

        if(rb != null)
        {
            Vector3 direction = collision.transform.position - transform.position;
            direction.y = 0;

            rb.AddForce(direction.normalized * knockbackStrenght, ForceMode2D.Impulse);
        }
    }
}
