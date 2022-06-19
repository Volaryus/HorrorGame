using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Enemy : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip horrorSound;
    public Animator animController;
    public GameObject jumpscareCam;
    public GameObject deathScreen;
    public GameObject monsterAudio;
    public GameObject deathMusic;
    private NavMeshAgent navMesh;
    private Transform targetPosition;

    // Start is called before the first frame update
    void Start()
    {
        navMesh = GetComponent<NavMeshAgent>();
        targetPosition = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            navMesh.SetDestination(targetPosition.position);
        }

    }

    /*  private void OnCollisionEnter(Collision collision)
      {
          if(collision.gameObject.CompareTag("Player"))
          {
              Debug.Log("Player Died");
          }
      } */
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            audioSource.PlayOneShot(horrorSound);
            animController.SetBool("IsTriggered", true);
            jumpscareCam.SetActive(true);
            other.gameObject.SetActive(false);
            Debug.Log("Died");
            StartCoroutine(DeathScreen());
        }
    }

    IEnumerator DeathScreen()
    {
        yield return new WaitForSeconds(2);
        deathScreen.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        monsterAudio.SetActive(false);
        deathMusic.SetActive(true);
    }
}
