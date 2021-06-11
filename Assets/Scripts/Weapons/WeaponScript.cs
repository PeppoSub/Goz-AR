using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponScript : MonoBehaviour
{
    public GameObject theCamera;
    public GameObject shootPoint;
    public GameObject[] theProjectiles;
    public TextMeshProUGUI weaponText;

    static private int selected = 0;
    private float reloadTime;
    private float thrust;
    private Vector3 throwFrom;
    private float reloading;

    void Start()
    {
        thrust = theProjectiles[selected].GetComponent<ProjectileLastingOnGround>().speed;
        reloadTime = theProjectiles[selected].GetComponent<ProjectileLastingOnGround>().coolDown;
     }

    void Update()
    {
        throwFrom = shootPoint.transform.position;
        if (reloading > 0) { reloading -= Time.deltaTime; }

        string textbuffer = "Weapon: " + theProjectiles[selected].name;
        weaponText.text = textbuffer;
    }

    public void Throw()
    {
        if (reloading <= 0)
        {

            // Instantiate the projectile at the position and rotation of this transform + offset
            GameObject clone = Instantiate(theProjectiles[selected], throwFrom, Quaternion.identity);
            clone.transform.LookAt(theCamera.transform.forward);

            // Give the cloned object an initial velocity
            clone.GetComponent<Rigidbody>().velocity = transform.TransformDirection(theCamera.transform.forward * thrust);

            reloading = reloadTime;
        }

    }

    public void Swap()
    {
        int nWeapons = theProjectiles.Length;
        selected = (selected + 1) % nWeapons;

        // Load current projectile speed and reload time
        thrust = theProjectiles[selected].GetComponent<ProjectileLastingOnGround>().speed;
        reloadTime = theProjectiles[selected].GetComponent<ProjectileLastingOnGround>().coolDown;
    }

}
