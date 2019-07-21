using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Rocket : MonoBehaviour
{
    Rigidbody rigidBody;
    AudioSource audioSource;
<<<<<<< HEAD
<<<<<<< HEAD
    [SerializeField] float rcsThrust = 250f;
    [SerializeField] float mainThrust = 1100f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip death;
    [SerializeField] AudioClip success;
    enum State {Alive, Dying, Transcending};


    [SerializeField] State state = State.Alive;
=======
    [SerializeField] float rcsThrust = 300f;
    [SerializeField] float mainThrust = 100f;
>>>>>>> parent of 803a2c2... Rocket wont fly
=======
    [SerializeField] float rcsThrust = 300f;
    [SerializeField] float mainThrust = 100f;
>>>>>>> parent of 803a2c2... Rocket wont fly

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(state == State.Alive)
        {
            RespondToThrustInput();
            RespondToRotateInput();
        }

    }

    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                print("Ok");
                break;
<<<<<<< HEAD
<<<<<<< HEAD
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartDeathSequence();
                break;
        }
    }

    private void StartSuccessSequence()
    {
        state = State.Transcending;
        audioSource.Stop();
        audioSource.PlayOneShot(success);
        Invoke("LoadNextLevel", 1f); //parameterize this time
    }

    private void StartDeathSequence()
    {
        state = State.Dying;
        audioSource.Stop();
        audioSource.PlayOneShot(death);
        Invoke("LoadFirstLevel", 1f);
    }

    private void LoadFirstLevel()    
    {
        SceneManager.LoadScene(0);
    }

    private void LoadNextLevel()
    {
        SceneManager.LoadScene(1); // todo allow for more levels
    }

    private void RespondToThrustInput()
=======
            case "Fuel":
                print("Fuel");
                break;
            default:
                print("Dead");
                break;
        }
    }
=======
            case "Fuel":
                print("Fuel");
                break;
            default:
                print("Dead");
                break;
        }
    }
>>>>>>> parent of 803a2c2... Rocket wont fly

    private void Thrust()
>>>>>>> parent of 803a2c2... Rocket wont fly
    {
        float thrustThisFrame = mainThrust * Time.deltaTime;
        if (Input.GetKey(KeyCode.Space))
        {
<<<<<<< HEAD
            ApplyThrust(thrustThisFrame);
=======
            rigidBody.AddRelativeForce(thrustThisFrame * Vector3.up);
            if (!audioSource.isPlaying) //so audio doesnt play on top of itself
            {
                audioSource.Play();
            }
>>>>>>> parent of 803a2c2... Rocket wont fly
        }
        else
        {
            audioSource.Stop();
        }
    }

    private void ApplyThrust(float thrustThisFrame)
    {
        rigidBody.AddRelativeForce(thrustThisFrame * Vector3.up);
        if (!audioSource.isPlaying) //so audio doesnt play on top of itself
        {
            audioSource.PlayOneShot(mainEngine);
        }
    }

    private void RespondToRotateInput()
    {
        rigidBody.freezeRotation = true; //take manual control of rotation

        float rotationThisFrame = rcsThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationThisFrame);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotationThisFrame);
        }
        rigidBody.freezeRotation = false; //resume control of rotation
    }


}
