using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private float _speed = 8;
    [SerializeField] private AudioClip _laserAudioClip;
    private AudioSource _laserAudioSource;
    private bool _isEnemyLaser = false;
    private Player _player;

    private void Start()
    {
        _laserAudioSource = GetComponent<AudioSource>();
        if (_laserAudioSource == null)
        {
            Debug.Log("_laserAudioSource is null");
        }
        else
        {
            _laserAudioSource.clip = _laserAudioClip;
        }

        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player == null)
        {
            Debug.Log("Laser::_player is null");
        }
        
        _laserAudioSource.Play();
    }

    void Update()
    {
        
        LaserMovement();
        LaserBoundaries();
    }
    
    private void LaserMovement()
    {
        if (_isEnemyLaser == false)
        {
            transform.Translate(Vector3.up * (_speed * Time.deltaTime));
        }
        else
        {
            transform.Translate(Vector3.down * (_speed * Time.deltaTime));
        }
            
    }

    private void LaserBoundaries()
    {
        if (transform.position.y > 7f || transform.position.y < -7f)
        {
            if (transform.parent)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && _isEnemyLaser == true)
        {
            _player.PlayerDmg();
        }
    }

    public void EnemyLaser()
    {
        _isEnemyLaser = true;
    }
    
}
