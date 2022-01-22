using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public AudioClip coinPickUp;
 private void OnCollisionEnter2D(Collision2D collision)
 {
     if(collision.collider.tag == "Coin")
     {
         Destroy(collision.collider.gameObject);
     }
 }
}
