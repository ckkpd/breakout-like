﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FallenSprite : MonoBehaviour, IEffect
{
    public float speed = 0.05f;
    public string itemID;
    public float itemDuration;
    public AudioClip obtainingSound;
    public static IEffect effectInstance;

    private void Start()
    {
        effectInstance = this;
    }
    private void FixedUpdate()
    {
        transform.Translate(0, -GameController.instance.fallenItemSpeed, 0);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Atatta");
        if(collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            player.AddEffect(this.itemID, this.itemDuration);
            OnObtained(player);

            Destroy(this.gameObject);
        }
    }

    public void PlaySound()
    {
        AudioSource audioSource = GameObject.FindWithTag("Player").GetComponent<AudioSource>();
        audioSource.PlayOneShot(obtainingSound);
    }

    public abstract void OnEffect(Player player);
    public abstract void OnObtained(Player player);
}
