using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damaged : MonoBehaviour
{
    public int maxHealth = 10;             // set this to negative to make the enemy invincible
    public int criticalHealth = 3;         // glow red or something
    public int enemyScore = 1;             // how much score gives killing this enemy
    public int bombDrop = 0;               // bonus bombs dropped on kill
    public int lifeDrop = 0;               // bonus lives dropped on kill

    public GameObject theChildren = null;  // objects to spawn when destroyed (if not null)
    public int nChildren = 2;              // how many of them

    public GameObject deathSmoke;          // particle effect produced when enemy dies
    public GameObject hitSmoke;            // particle effect produced when enemy gets hit

    private float health;
    private GameStatus gameStatus;

    void Start()
    {
        health = maxHealth;
        if(GameObject.FindWithTag("GameController") != null)
        { 
            gameStatus = GameObject.FindWithTag("GameController").GetComponent<GameStatus>();   
        }
    }

    void Update()
    {
        // if(health<criticalHealth) { // glow red or something ... }
    }

    void OnCollisionEnter(Collision col)   // hit effect is produced on collision
    {
        ContactPoint[] contacts = new ContactPoint[10];
        int numContacts = col.GetContacts(contacts);
        if (hitSmoke != null) { Instantiate(hitSmoke, contacts[0].point, Quaternion.identity); }
    }

    public void ApplyDamage(float damage)  // damage is appllied by a call from the projectile
    {
        if (maxHealth < 0) return;
        
        health -= damage; 
        if (health <= 0)   { Die(); }
    }

    void Die()
    {
        // call death animation ... or just explode :)
        Instantiate(deathSmoke, gameObject.transform.position, Quaternion.identity);
        Destroy(gameObject);

        gameStatus.Score(enemyScore, bombDrop, lifeDrop);
    }

    public void OnDestroy()
    {
        // spawn child objects when destroyed at 0 health* (it avoids child spawning when destroyed by hitting player)
        if ((theChildren != null) && (nChildren > 0) && (health <= 0))
        {
            for (int i = 0; i < nChildren; i++)
            {
                Instantiate(theChildren, this.transform.position, Quaternion.identity);
            }
        }
    }

    public void ResetHealth(int h)
    {
        maxHealth = h;
        health = maxHealth;
    }

    public int CurrentHealth()
    {
        return (int)health;
    }
}
