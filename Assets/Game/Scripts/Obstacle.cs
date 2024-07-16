using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField]
    private float _minRotateSpeed, _maxRotateSpeed;
    [SerializeField]
    private float _minRotateTime, _maxRotateTime;
    
    private float _rotateTime;
    private float _currentRotateSpeed;
    private float _currentRotateTime;

    private void Awake()
    {
        _currentRotateTime = 0f;
        _currentRotateSpeed = _minRotateSpeed + (_maxRotateSpeed - _minRotateSpeed) * 0.1f * Random.Range(0, 11);
        _rotateTime = _minRotateTime + (_maxRotateTime - _minRotateTime) * 0.1f * Random.Range(0, 11);
        _rotateTime *= Random.Range(0, 2) == 0 ? 1f : -1f;
    }

    private void Update()
    {
        _currentRotateTime += Time.deltaTime;

        if (_currentRotateTime > _rotateTime)
        {
            _currentRotateTime = 0f;
            _currentRotateSpeed = _minRotateSpeed + (_maxRotateSpeed - _minRotateSpeed) * 0.1f * Random.Range(0, 11);
            _rotateTime = _minRotateTime + (_maxRotateTime - _minRotateTime) * 0.1f * Random.Range(0, 11);
            _rotateTime *= Random.Range(0, 2) == 0 ? 1f : -1f;
        }
    }

    private void FixedUpdate()
    {
        transform.Rotate(0, 0, _currentRotateSpeed * Time.fixedDeltaTime);
    }
}
