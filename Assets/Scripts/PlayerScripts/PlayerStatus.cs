﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{

    private int MAX_HEALTH = 5;
    private int SPEED_UPGRADE = 5;
    private float SPEED_UPGRADE_TIME = 5.0f;
    int points;
    int health;
    bool onCrack;

    Rigidbody rb;
    Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        points = 0;
        health = 3;

        onCrack = false;

        rb = GetComponent<Rigidbody>();
        startPosition = transform.position;


    }

    // Update is called once per frame
    void Update()
    {

    }

    // collisions
    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.tag);

        // collectables
        if (other.gameObject.CompareTag("Coin"))
        {
            points++;
            other.gameObject.SetActive(false);
        }
        if (other.gameObject.CompareTag("HealthPotion")) {
            health++;
            other.gameObject.SetActive(false);

        }
        if (other.gameObject.CompareTag("Amphetamine"))
        {
            onCrack = true;
            other.gameObject.SetActive(false);
            Invoke("SoberUp", SPEED_UPGRADE_TIME);
        }
        // damage
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Enemy collision");
            TakeDamage(1);
        }

        if (other.gameObject.CompareTag("Deathzone"))
        {
            Debug.Log("You failed.");
            Respawn();
        }
    }

    // helper functions
    public int GetPoints() { return points; }
    public void AddPoint(int points) { this.points += points; }
    private void Respawn() {

        // TODO: Juicy animation

        // reset position
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = true;
        // Do positioning, etc
        transform.position = startPosition;
        transform.rotation = Quaternion.identity;
        // Re-enable the physics and set start position to that of the turret
        rb.isKinematic = false;
        transform.position = transform.position;

        // reset health
        health = MAX_HEALTH;
    }
    public int GetHealth() { return health; }
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0) Respawn();
    }
    public int SpeedUpgrade() { return onCrack ? SPEED_UPGRADE : 1; }
    public bool OnCrack() { return onCrack; }
    private void SoberUp() { onCrack = false;  }
}