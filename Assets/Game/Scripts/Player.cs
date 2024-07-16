using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private AudioClip _moveClip, _loseClip;

    [SerializeField] private GamePlayManager _gamePlayManager;
    [SerializeField] private GameObject _explosionPrefab;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Play movement sound
            SoundManager.Instance.PlaySound(_moveClip);
            // Reverse rotation direction
            _rotateSpeed *= -1f;
        }
    }

    private void FixedUpdate()
    {
        // Rotate the player based on current rotation speed
        transform.Rotate(0, 0, _rotateSpeed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if collided with an obstacle
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            // Instantiate explosion effect at the player's position
            Instantiate(_explosionPrefab, transform.GetChild(0).position, Quaternion.identity);

            // Play losing sound
            SoundManager.Instance.PlaySound(_loseClip);

            // Inform GamePlayManager that the game has ended
            _gamePlayManager.GameEnded();

            // Destroy the player object
            Destroy(gameObject);
        }
    }
}
