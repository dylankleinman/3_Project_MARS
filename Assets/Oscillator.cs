using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent] //only allows one of this script per gameobject

public class Oscillator : MonoBehaviour
{
    //Vector3 specifies 3D values, Important: Movement vector is initially 0,0,0.  
    //In order to multiply vector by offset, you need to input some value in inspector!
    [SerializeField] Vector3 movementVector = new Vector3(10f, 10f, 10f);
    [SerializeField] float period = 4f; //period is how many seconds each oscilation will be

    //todo remove from inspector later
    float movementFactor;  //set this to 0 for not moved, 1 is fully moved
    Vector3 startingPos;

    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
        print(startingPos);
    }

    // Update is called once per frame
    void Update()
    {
        //set movement factor
        if (period <= Mathf.Epsilon)  //dont compare floating point numbers
        {
            return;
        }
        float cycles = Time.time / period;  //grows continually from time = 0;
        const float tau = Mathf.PI * 2; //about 6.28 (full circumfrence)
        float rawSinWave = Mathf.Sin(cycles * tau);  //will oscilate between -1 and 1

        movementFactor = rawSinWave / 2f;
        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPos + offset;
        print("hello");
    }
}
