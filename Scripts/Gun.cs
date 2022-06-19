using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Gun : MonoBehaviour
{

    public float damage = 10f;
    public float range = 50f;
    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject flameImpactEffect;
    public GameObject woodImpactEffect;
    public GameObject dirtImpactEffect;
    public GameObject explosionEffect;
    public AudioClip shootSound;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        RaycastHit hit;
        muzzleFlash.Play();
        audioSource.PlayOneShot(shootSound);
        if(Physics.Raycast(fpsCam.transform.position,fpsCam.transform.forward,out hit,range))
        {
            Debug.Log(hit.transform.name);
            Health health=hit.transform.GetComponent<Health>();
            if(health!=null)
            {
                health.TakeDamage(damage);
            }
            if(hit.transform.gameObject.CompareTag("Explosive"))
            {
                GameObject flame = Instantiate(flameImpactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                flame.transform.SetParent(hit.transform);
                
                Destroy(flame, 5f);
                StartCoroutine(ExplosionEffect(hit));
                
                Destroy(hit.transform.gameObject, 5.75f);
            }
            else
            {
                Instantiate(woodImpactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            }

        }
    }

    IEnumerator ExplosionEffect(RaycastHit hit)
    {
        yield return new WaitForSeconds(5f);
        hit.transform.gameObject.SetActive(false);
        Instantiate(explosionEffect, hit.transform.gameObject.transform.position, Quaternion.identity);
        
    }
    
}
