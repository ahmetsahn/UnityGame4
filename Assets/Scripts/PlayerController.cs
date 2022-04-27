using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private GameObject focalPoint;
    private float powerUpStrength = 15.0f;
    public float speed;
    public bool hasPowerUp = false;
    public GameObject powerUpIndicator;


    void Start()
    {
       playerRb = GetComponent<Rigidbody>();
       focalPoint = GameObject.Find("Focal Point");
        
    }

    
    void Update()
    {
        
        float verticalInput = Input.GetAxis("Vertical");

        playerRb.AddForce(focalPoint.transform.forward * verticalInput * speed * Time.deltaTime);

        powerUpIndicator.transform.position = transform.position;
    
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PowerUp"))
        {
            powerUpIndicator.SetActive(true);
            hasPowerUp = true;
            Destroy(other.gameObject);
            StartCoroutine(PowerUpCounddownRoutine());
        }
    }


    IEnumerator PowerUpCounddownRoutine()
    {
       
        yield return new WaitForSeconds(7);
        hasPowerUp = false;
        powerUpIndicator.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy") && hasPowerUp)
        {
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;
            enemyRigidbody.AddForce(awayFromPlayer * powerUpStrength,ForceMode.Impulse);
            
          
        }
    }


    

}
