using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun1 : MonoBehaviour
{

    public GameObject flashLight;
    public AudioClip flashOpen;
    public AudioClip flashClose;
    public int flareCount = 3;
    public GameObject flare;
    public GameObject throwPlace;
    public GameObject eyeBlinkEffect;
    public Camera playerCam;
    public float throwForce = 10f;
    public Material skybox;
    AudioSource audioSource;

    //Gun variables
    public float damage;
    public float range = 50f;
    bool flashActive = false;
    // Start is called before the first frame update
    void Start()
    {
        RenderSettings.skybox = skybox;
        audioSource = GetComponent<AudioSource>();
        Invoke("CloseEyeBlink", 5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (!flashActive)
            {
                flashLight.SetActive(true);
                flashActive = true;
                audioSource.PlayOneShot(flashOpen);
            }
            else
            {
                flashLight.SetActive(false);
                flashActive = false;
                audioSource.PlayOneShot(flashClose);

            }

        }
        if (Input.GetKeyDown(KeyCode.G) && flareCount > 0)
        {
            GameObject thrownFlare = Instantiate(flare, throwPlace.transform.position, Quaternion.identity);
            thrownFlare.GetComponent<Rigidbody>().AddForce(playerCam.transform.forward*throwForce,ForceMode.Impulse);
            flareCount--;
        }
    }

    void CloseEyeBlink()
    {
        eyeBlinkEffect.SetActive(false);
    }
}
