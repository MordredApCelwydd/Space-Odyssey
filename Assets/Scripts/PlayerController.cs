using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    [Header("Rocket Speed")]
    [SerializeField] private float thrustSpeed;
    [SerializeField] private float rotationSpeed;

    [Header("Assigning SFX")]
    [SerializeField] private AudioClip thrusterSound;
    
    [Header("Assigning VFX")]
    [SerializeField] private ParticleSystem mainEngineParticles;
    [SerializeField] private ParticleSystem rightBoosterParticles;
    [SerializeField] private ParticleSystem leftBoosterParticles;
    
    [SerializeField] private ParticleSystem leftTurnParticles;
    [SerializeField] private ParticleSystem rightTurnParticles;
    
    private AudioSource _sfx;
    private Rigidbody _rb;
    private Vector3 _movementVector;
    private Vector3 _vfxVector;
    private bool _sfxPlaying;

    private void Start()
    {
        _sfxPlaying = false;
        _sfx = GetComponent<AudioSource>();
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        _movementVector = new Vector3(0, 0, 0);
        _vfxVector = new Vector3(0, 0, 0);

        if (Input.GetKey(KeyCode.Space))
        {
            _vfxVector.x = 1;
            _movementVector.y = 1;
        }

        if (Input.GetKey(KeyCode.A))
        {
            _vfxVector.y = 1;
            _movementVector.z =+ rotationSpeed;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            _vfxVector.z = 1;
            _movementVector.z =+ rotationSpeed * -1;
        }
    }
    private void FixedUpdate()
    {
        RocketMove(_movementVector);
        SfxController(_movementVector);
        VfxController(_vfxVector);
    }

    private void RocketMove(Vector3 movement)
    {
        _rb.freezeRotation = true;
        _rb.AddRelativeForce(0, movement.y * thrustSpeed, 0);
        
        Quaternion rocketRotation = Quaternion.Euler(0, 0, movement.z * Time.deltaTime);
        _rb.MoveRotation(_rb.rotation * rocketRotation);
        _rb.freezeRotation = false;
    }

    private void SfxController(Vector3 movement)
    {
        if (_movementVector != Vector3.zero && _sfxPlaying == false)
        {
            _sfx.PlayOneShot(thrusterSound);
            _sfxPlaying = true;
        }
        else if (_movementVector == Vector3.zero)
        {
            _sfx.Stop();
            _sfxPlaying = false;
        }
    }

    private void VfxController(Vector3 movement)
    {
        if (movement.x == 1)
        {
            rightBoosterParticles.Play();
            leftBoosterParticles.Play();
            mainEngineParticles.Play();
        }
        else
        {
            rightBoosterParticles.Stop();
            leftBoosterParticles.Stop();
            mainEngineParticles.Stop();
        }

        if (movement.y != 0)
        {
            rightTurnParticles.Play();
        }
        else 
        {
            rightTurnParticles.Stop();
        }
        
        if (movement.z != 0)
        {
            leftTurnParticles.Play();
        }
        else
        {
            leftTurnParticles.Stop();
        }
    }
}
