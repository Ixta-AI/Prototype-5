using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public int pointValue;
    public ParticleSystem explosionParticle;

    private Rigidbody targetRb;
    private GameManager gameManager;// Reference Var

    private float minSpeed = 12.0f;
    private float maxSpeed = 16.0f;
    private float maxTorque = 10;
    private float xRange = 4;
    private float ySpawnPos = -2;
    // Start is called before the first frame update
    void Start()
    {
        targetRb = GetComponent<Rigidbody>();//Grab rb of object
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();//Get Game Manager & GMScript component

        targetRb.AddForce(RandomForce(), ForceMode.Impulse);//Add upwards force
        targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);//Add rotation to object
        //Spawn object in random range
        transform.position = RandomSpawnPos();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

//Methods to call
    Vector3 RandomForce()//Upwards force multiplied by random range
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }

    float RandomTorque()//Spin
    {
        return Random.Range(-maxTorque, maxTorque);
    }

    Vector3 RandomSpawnPos()//Spawn under camera (X between -4 & 4, Y -6 beneath the screen and Zero Z)
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos);
    }

    private void OnMouseDown()// Unity provided code (when mouse click on object)
    {
        Destroy(gameObject);
        Instantiate(explosionParticle,transform.position, explosionParticle.transform.rotation);// Spawn particle on object location with matching rotation
        gameManager.UpdateScore(pointValue);// Update score on mouse click 
    }

    private void OnTriggerEnter(Collider other)// Unity provided code (when object hits other trigger)
    {
        Destroy(gameObject);
        if (!gameObject.CompareTag("Bad"))
        {
            gameManager.GameOver();
        }
        
    }
}
