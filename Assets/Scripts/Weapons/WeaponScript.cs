using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponScript : MonoBehaviour
{
    public GameObject theCamera;
    public GameObject shootPoint;
    public GameObject[] theProjectiles;
    public GameObject theBomb;
    public TextMeshProUGUI weaponText;

    private int selected;
    private float reloadTime;
    private float thrust;
    private Vector3 throwFrom;
    private Quaternion throwRotation;

    private float reloading;

    void Start()
    {
        selected = 0;
        if(theProjectiles.Length > 1) { selected = GameStatus.selectedWeapon; }

        // initialize weapon parameters
        thrust = theProjectiles[selected].GetComponent<ProjectileLastingOnGround>().speed;
        reloadTime = theProjectiles[selected].GetComponent<ProjectileLastingOnGround>().coolDown;

        // Update weapon text (if there)
        if (weaponText != null)
        {
            string textbuffer = "Weapon: " + theProjectiles[selected].name;
            weaponText.text = textbuffer;
        }
    }

    void Update()
    {
        // update reload time
        if (reloading > 0) { reloading -= Time.deltaTime; }
    }

    public void Throw()
    {
        if (reloading <= 0)
        {
            // update coordinates of shooting point
            throwFrom = shootPoint.transform.position;
            throwRotation = shootPoint.transform.rotation;

            // Instantiate the projectile at the position and rotation of the shootpoint
            GameObject clone = Instantiate(theProjectiles[selected], throwFrom, throwRotation);

            // Give the cloned object an initial velocity
            clone.GetComponent<Rigidbody>().velocity = transform.TransformDirection(theCamera.transform.forward * thrust);

            // reset reload time
            reloading = reloadTime;
        }

    }

    public void Swap()
    {
        // swap weapon (only affects current level) - this should only be enabled for testing, normal gameplay uses 1 weapon per level
        int nWeapons = theProjectiles.Length;
        selected = (selected + 1) % nWeapons;

        // Load current projectile speed and reload time
        thrust = theProjectiles[selected].GetComponent<ProjectileLastingOnGround>().speed;
        reloadTime = theProjectiles[selected].GetComponent<ProjectileLastingOnGround>().coolDown;

        // Update weapon text (if there)
        if (weaponText != null)
        {
            string textbuffer = "Weapon: " + theProjectiles[selected].name;
            weaponText.text = textbuffer;
        }
    }

    public void Bomb()
    {
        if (GameStatus.nBombs > 0)
        {
            // Instantiate the bomb at the position and rotation of the shootpoint
            GameObject clone = Instantiate(theBomb, throwFrom, throwRotation);

            // Give the bomb  its initial velocity
            float bombThrust = theBomb.GetComponent<ProjectileLastingOnGround>().speed;
            clone.GetComponent<Rigidbody>().velocity = transform.TransformDirection(theCamera.transform.forward * thrust);

            // remove bomb from inventory
            GameStatus.nBombs--;
        }
    }

}
