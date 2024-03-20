using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class PlayerCollision : MonoBehaviour
{
   [Header("Assigning SFX")]
   [SerializeField] private AudioClip crashSound;
   [SerializeField] private AudioClip successSound;

   [Header("Assigning VFX")]
   [SerializeField] private ParticleSystem crashParticles;
   [SerializeField] private ParticleSystem successParticles;

   [Header("Gameplay settings")] 
   [SerializeField] private int sequenceDurationSeconds;
   
   private AudioSource _sfx;
   private PlayerController _playerControllerScript;
   private Coroutine _endLevelCoroutine;
   
   private bool _isSoundPlaying;
   private bool _isInvulnerable;
   private bool _isInSequence;
   
   public bool IsInvulnerable
   {
      get => _isInvulnerable;
      set => _isInvulnerable = value;
   }
   
   private void Start()
   {
      _isInvulnerable = false;
      _isSoundPlaying = false;
      _isInSequence = false;
      _playerControllerScript = gameObject.GetComponent<PlayerController>();
      _sfx = GetComponent<AudioSource>();
   }
   private void OnCollisionEnter(UnityEngine.Collision other)
   {
      if (_isInSequence)
      {
         return;
      }
      
      if(other.gameObject.GetComponentInParent<ObstacleDamager>())
      {
         if (!_isInvulnerable)
         {
            _endLevelCoroutine = StartCoroutine(InitiateCrashSequence(sequenceDurationSeconds));
         }
      }
      else if(other.gameObject.GetComponent<LandingPad>())
      {
         _endLevelCoroutine = StartCoroutine(InitiateFinishSequence(sequenceDurationSeconds));
      }
   }

   private void LoadLevel(int levelIndex)
   {
      if(levelIndex == SceneManager.sceneCountInBuildSettings)
      {
         levelIndex = 0;
      }
      SceneManager.LoadScene(levelIndex);
   }

   private void PlayLastSound(AudioClip sound)
   {
      if (_isSoundPlaying == false)
      {
         _sfx.Stop();
         _sfx.PlayOneShot(sound);
         _isSoundPlaying = true;
      }
   }

   private IEnumerator InitiateCrashSequence(int delayTimeSeconds)
   {
      _playerControllerScript.enabled = false;
      PlayLastSound(crashSound);
      crashParticles.Play();
      yield return new WaitForSeconds(delayTimeSeconds);
      TimeAndStatusManager.timeElapsed += Time.timeSinceLevelLoad;
      LoadLevel(SceneManager.GetActiveScene().buildIndex);
   }
   
   private IEnumerator InitiateFinishSequence(int delayTimeSeconds)
   {
      _playerControllerScript.enabled = false;
      PlayLastSound(successSound);
      successParticles.Play();
      yield return new WaitForSeconds(delayTimeSeconds);
      TimeAndStatusManager.timeElapsed += Time.timeSinceLevelLoad;
      LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
   }
}

