using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    Rigidbody rigidBody;
    AudioSource audioSource;
    [SerializeField] float rcsThrust = 250f;
    [SerializeField] float mainThrust = 1100f;
    [SerializeField] float levelLoadDelay = 2f;

    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip death;

    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem deathParticles;
    bool isTransitioning = false;

    private int currentLevel;
    private int totalScenes;
    [SerializeField] bool collisionToggle = false;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        currentLevel = SceneManager.GetActiveScene().buildIndex;
        totalScenes = SceneManager.sceneCountInBuildSettings;
        print(totalScenes);
    }

    // Update is called once per frame
    void Update()
    {
        if(!isTransitioning)
        {
            RespondToThrustInput();
            RespondToRotateInput();
            RespondToQuitInput();
            if (Debug.isDebugBuild) RespondToDebugKeys();  //only allow debug keys for debut mode
        }
    }

    private void RespondToQuitInput()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            LoadFirstLevel();
        }
    }

    private void RespondToDebugKeys()
    {
        if (Input.GetKey(KeyCode.C))
        {
            collisionToggle = !collisionToggle;
        }
        if (Input.GetKey(KeyCode.L))
        {
            LoadNextLevel();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(isTransitioning) return;

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                // do nothing
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                if(!collisionToggle) StartDeathSequence();
                break;
        }
    }

    private void StartSuccessSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(success);
        successParticles.Play();
        Invoke("LoadNextLevel", levelLoadDelay); // parameterise time
    }

    private void StartDeathSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(death);
        deathParticles.Play();
        Invoke("ReloadLevel", levelLoadDelay); // parameterise time
    }

    private void ReloadLevel()
    {
        SceneManager.LoadScene(currentLevel);
    }

    private void LoadFirstLevel()
    {
        SceneManager.LoadScene(0);
    }

    private void LoadNextLevel()
    {
        //todo create menu screen and winning screen instead of bringing back to first level
        if (currentLevel + 1 == totalScenes)
        {
           SceneManager.LoadScene(2);
        }
        else
        {
            SceneManager.LoadScene(currentLevel + 1);
        }
    }

    private void RespondToThrustInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            ApplyThrust();
        }
        else
        {
            StopApplyThrust();
        }
    }

    private void StopApplyThrust()
    {
        audioSource.Stop();
        mainEngineParticles.Stop();
    }

    private void ApplyThrust()
    {
        float thrustThisFrame = mainThrust * Time.deltaTime;
        rigidBody.AddRelativeForce(thrustThisFrame * Vector3.up);
        if (!audioSource.isPlaying) //so audio doesnt play on top of itself
        {
            audioSource.PlayOneShot(mainEngine);
        }
        mainEngineParticles.Play();
    }

    private void RespondToRotateInput()
    {
        float rotationThisFrame = rcsThrust * Time.deltaTime;
        if (Input.GetKey(KeyCode.A) && (!isTransitioning))
        {
            RotateManually(rotationThisFrame);
        }
        else if (Input.GetKey(KeyCode.D) && (!isTransitioning))
        {
            RotateManually(-rotationThisFrame);
        }

    }

    private void RotateManually(float rotationThisFrame)
    {
        rigidBody.freezeRotation = true; //take manual control of rotation
        transform.Rotate(Vector3.forward * rotationThisFrame);
        rigidBody.freezeRotation = false; //resume control of rotation
    }
}
