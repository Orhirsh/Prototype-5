using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody targetRb;
    public int pointValue;
    private float minSpeed = 12.0f;
    public float maxSpeed = 16.0f;
    private float maxTorque = 10.0f;
    private float xRange = 4;
    private float ySpawnPos = -3;
    public GameManager gameManager;
    public ParticleSystem explosionParticle;
    // Start is called before the first frame update
    void Start()
    {
        targetRb = GetComponent<Rigidbody>();

        targetRb.AddForce(RandomForce(), ForceMode.Impulse);

        targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque());

        transform.position = RandomSpawnPos();

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /*private void OnMouseDown()
    {
      if(gameManager.isGameActive)
      {  
        Destroy(gameObject);
        gameManager.UpdateScore(pointValue);

        Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
      }
    }*/
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        if(!gameObject.CompareTag("Bad"))
        {
            gameManager.UpdateLives(1);
        }
    }


    public Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed,maxSpeed);          
    }
    float RandomTorque()
    {
        return Random.Range(-maxTorque,maxTorque);
    }
    Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(-xRange,xRange), ySpawnPos);
    }
    public void DestroyTarget()
    {
        //Destroy function  called from ClickAndSwipe Script
        if(gameManager.isGameActive)
        {
            Destroy(gameObject);
            gameManager.UpdateScore(pointValue);

            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);        
        }
    }
}
