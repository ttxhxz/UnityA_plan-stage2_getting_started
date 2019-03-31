using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    public AudioClip keyGrad;

    private GameObject player;
    private PlayerInventory playerInventory;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag(Tags.player);
        playerInventory = player.GetComponent<PlayerInventory>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            AudioSource.PlayClipAtPoint(keyGrad, transform.position);
            playerInventory.hasKey = true;
            Destroy(this.gameObject);
        }
    }
}
