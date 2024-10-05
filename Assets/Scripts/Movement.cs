using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField]
    float mainThrust = 100f;

    [SerializeField]
    float rotationSpeed = 1f;

    [SerializeField]
    ParticleSystem jetParticles;

    [SerializeField]
    AudioClip mainEngine;

    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

        // Lock rotation on Y and Z axes, allowing only X-axis rotation
        rb.constraints =
            RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
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
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);

        if (!jetParticles.isPlaying)
        {
            jetParticles.Play();
        }

        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
    }

    void StopThrusting()
    {
        if (jetParticles.isPlaying)
        {
            jetParticles.Stop();
        }

        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(-1); // Rotate left
        }
        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(1); // Rotate right
        }
    }

    void ApplyRotation(float direction)
    {
        // Apply rotation along X-axis (change based on your desired axis)
        transform.Rotate(Vector3.right * rotationSpeed * Time.deltaTime * direction);
    }
}
