//imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Gun : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI ammo;
    //blaster floats
    public float damage = 10f;
    public float range = 100f;
    public float force = 30f;
    public float health = 100f;
    //ammo floats
    public float maxAmmo = 10f;
    public float currentAmmo;
    public float reloadTime = 1f;
    private bool isReloading = false;

    //text
    public TextMeshProUGUI GUIhealth;
   
    
    //different animations
    public Camera fps;
    public ParticleSystem muzzleFlash;

    public GameObject hitEffect;
    public GameObject groundImpact;
    public GameObject vaccineImpact;


    public Animator animator;

   

    private void Start()
    {
        //sets the staring ammo and locks the cursor to the middle of the screen
        currentAmmo = maxAmmo;

        Cursor.lockState = CursorLockMode.Locked;

    }
    public void Update()
    {
        //dont shoot if reloading
        if (isReloading)
            return;
        
        
        //start reloading if we run out of ammo
        if (currentAmmo <= 0f)
        {
            StartCoroutine(Reload());
            return;
        }

        //sshoot if we click the left mouse button
        if (Input.GetButtonDown("Fire1"))
        {
            
            Shoot();
            
            
        }
        //showing the GUI updating
        ammo.text = "Carbon Crushers Left: " + currentAmmo.ToString();
        GUIhealth.text = "Health Left: " + health.ToString();

    }

    IEnumerator Reload()
    {
        //reloads the gun
        animator.SetBool("Reloading", true);
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
        animator.SetBool("Reloading", false);
        isReloading = false;
    }
    //shoots the weapon
    void Shoot()
    {
        //takes one off from the current ammo
        currentAmmo--;
        
        //turns plays animations
        animator.SetBool("Shooting", true);
        muzzleFlash.Play();
        RaycastHit hit;
        
        //Sends out a raycast from the camera, and outputs the result into the variable hit, and the raycast only goes out a certain distance away
        if (Physics.Raycast(fps.transform.position, fps.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();
            GroundTarget gtarget = hit.transform.GetComponent<GroundTarget>();

            if (target != null)
            {
                //makes the target take 10 damage and shows feedback for hitting enemy
                target.takeDamage(10f);
                GameObject gImpact = Instantiate(vaccineImpact, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(gImpact, 2f);

            }
            

            if (gtarget != null)
            {

                GameObject hEffect = Instantiate(groundImpact, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(hEffect, 2f);

            }
            
        }
        animator.SetBool("Shooting", false);

    }





}
