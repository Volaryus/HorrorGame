using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlaneController : MonoBehaviour
{
    public GameObject crashedPlane;
    public GameObject player;
    public GameObject cameraHolder;
    public GameObject flame;
    public GameObject crashObject;
    public GameObject eyeBlink;
    public AudioSource audioSource;
    public AudioClip crashNormal;
    public AudioClip crashFast;
    public AudioClip crash1;
    public AudioClip trueKey;
    public TextMeshProUGUI text;
    public bool crashStarted = false;
    bool isCrashed = false;
    public float timeToCrash = 5f;
    float gameTimer = 5f;
    public GameObject[] flames;
    int flameCount = 0;
    int checkForKey = 0;
    float currentTime = 0f;
    void Start()
    {
        
    }
    void FixedUpdate()
    {
        transform.Translate(-15f * Time.deltaTime, 0, 0);
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        transform.Rotate(moveX, 0, moveY);
    }
    private void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= timeToCrash)
        {
            StartCoroutine(CameraShake(15f, 0.01f));
            if (!crashStarted)
            {
                crashStarted = true;
                audioSource.loop = true;
                audioSource.clip = crashNormal;
                audioSource.Play();

                flame.SetActive(true);
            }
            if (gameTimer > 2f && !isCrashed)
            {

                ArrowGame();
                gameTimer = 0f;
            }
            gameTimer += Time.deltaTime;
        }

    }
    void PlaneCrash()
    {

    }
    void ArrowGame()
    {

        int a = Random.Range(0, 5);
        /* switch (a)
         {
             case 1: text.text = "UP"; if (Input.GetKeyDown(KeyCode.UpArrow)) { audioSource.PlayOneShot(trueKey); } else { flames[flameCount++].SetActive(true); } break;
             case 2: text.text = "DOWN"; if (Input.GetKeyDown(KeyCode.DownArrow)) { audioSource.PlayOneShot(trueKey); } else { flames[flameCount++].SetActive(true); } break;
             case 3: text.text = "RIGHT"; if (Input.GetKeyDown(KeyCode.RightArrow)) { audioSource.PlayOneShot(trueKey); } else { flames[flameCount++].SetActive(true); }; break;
             case 4: text.text = "LEFT"; if (Input.GetKeyDown(KeyCode.LeftArrow)) { audioSource.PlayOneShot(trueKey); } else { flames[flameCount++].SetActive(true); } break;
         }*/

        switch (a)
        {
            case 1: text.text = "UP"; break;
            case 2: text.text = "DOWN"; break;
            case 3: text.text = "RIGHT"; break;
            case 4: text.text = "LEFT"; break;
        }

        StartCoroutine(CheckArrow(a));

        if (flameCount >= 3 && !isCrashed)
        {
            isCrashed = true;
            audioSource.PlayOneShot(crash1);
            audioSource.volume = 0.5f;
            Invoke("CrashSound", 7f);
            text.text = "";
        }
    }
    IEnumerator CameraShake(float duration, float magnitude)
    {
        Vector3 cameraPosition = cameraHolder.transform.localPosition;
        float elapsedTime = 0.0f;
        while (elapsedTime < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;
            cameraHolder.transform.localPosition = new Vector3(x, y, cameraHolder.transform.localPosition.z);
            elapsedTime += Time.deltaTime;
            yield return null;
            // Debug.Log("a");
        }
        cameraHolder.transform.localPosition = cameraPosition;
    }


    IEnumerator CheckArrow(int a)
    {
        yield return new WaitForSeconds(1f);
        if (Input.GetKey(KeyCode.UpArrow)||Input.GetKey(KeyCode.W))
            checkForKey = 1;
        else if (Input.GetKey(KeyCode.DownArrow)||Input.GetKey(KeyCode.S))
            checkForKey = 2;
        else if (Input.GetKey(KeyCode.RightArrow)||Input.GetKey(KeyCode.D))
            checkForKey = 3;
        else if (Input.GetKey(KeyCode.LeftArrow)||Input.GetKey(KeyCode.A))
            checkForKey = 4;

        if (checkForKey == a)
        {
            audioSource.PlayOneShot(trueKey);
            text.text = "CORRECT";
            checkForKey = 0;
        }
        else
        {
            text.text = "WRONG";
            flames[flameCount++].SetActive(true);
            checkForKey = 0;
        }
    }

    void CrashSound()
    {
        audioSource.volume = 1f;
        crashObject.SetActive(true);
        player.SetActive(true);
        crashedPlane.SetActive(true);
        text.text = "";
        eyeBlink.SetActive(true);
        Destroy(gameObject);
    }
}
