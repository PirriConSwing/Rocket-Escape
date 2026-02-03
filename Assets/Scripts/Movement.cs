using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] InputAction thrust;
    [SerializeField] InputAction rotation;
    [SerializeField] float thrustStrength = 1500f;
    [SerializeField] float rotationStrength = 1000f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainBoostParticles;
    [SerializeField] ParticleSystem leftBoostParticles;
    [SerializeField] ParticleSystem rightBoostParticles;

    Rigidbody rb;
    AudioSource audioSource;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        thrust.Enable();
        rotation.Enable();
    }

    private void FixedUpdate()
    {
        ProcessThrust();
        ProcessRotation();
    }

    private void ProcessThrust()
    {
        if (thrust.IsPressed())
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    private void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * thrustStrength * Time.fixedDeltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }

        if (!mainBoostParticles.isPlaying)
        {
            mainBoostParticles.Play();
        }
    }
    private void StopThrusting()
    {
        audioSource.Stop();
        mainBoostParticles.Stop();
    }
    private void ProcessRotation() 
    {
        float rotationInput = rotation.ReadValue<float>();

        if (rotationInput < 0)
        {
            RotateRight();
        }

        else if (rotationInput > 0)
        {
            RotateLeft();
        }
        else
        {
            StopRotating();
        }

    }

    private void RotateRight()
    {
        applyRotation(rotationStrength);
        if (!rightBoostParticles.isPlaying)
        {
            leftBoostParticles.Stop();
            rightBoostParticles.Play();
        }
    }
    private void RotateLeft()
    {
        applyRotation(-rotationStrength);
        if (!leftBoostParticles.isPlaying)
        {
            rightBoostParticles.Stop();
            leftBoostParticles.Play();
        }
    }
    private void StopRotating()
    {
        rightBoostParticles.Stop();
        leftBoostParticles.Stop();
    }

    private void applyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.fixedDeltaTime);
        rb.freezeRotation = false;
    }
}
