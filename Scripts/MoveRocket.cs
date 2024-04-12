using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRocket : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1000f;
    [SerializeField] float rotateSpeed = 100f;
    [SerializeField] AudioClip rocket;

    [SerializeField] ParticleSystem rocketJet;
    [SerializeField] ParticleSystem leftJet;
    [SerializeField] ParticleSystem rightJet;


    Rigidbody rb;
    AudioSource audioS;

    // Start is called before the first frame update
    void Start()
    {
        rb= GetComponent<Rigidbody>();
        audioS= GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {

        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.up * Time.deltaTime * moveSpeed);
            if (!audioS.isPlaying)
            {
                audioS.PlayOneShot(rocket);
            }
            if (!rocketJet.isPlaying)
            {
                                rocketJet.Play();
            }
            
        }
        else
        {
            rocketJet.Stop();
            audioS.Stop();
        }

    }
    void ProcessRotation()
    {
        rb.freezeRotation = true;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(Vector3.forward * Time.deltaTime * rotateSpeed);
            leftJet.Play();
        }
        else 
        { 
            leftJet.Stop();
        }
        
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(-Vector3.forward * Time.deltaTime * rotateSpeed);
            rightJet.Play(); 
        }
        else
        { 
            rightJet.Stop(); 
        }
        
        
        rb.freezeRotation = false;
    }
}
