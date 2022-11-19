using IdleClicker.GameLogic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace IdleClicker.CameraControl
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private SquareSpawner _squareSpawner;
        [SerializeField] private float _lerpSpeed = 1.25f;
        [SerializeField] private Camera _cam;
        private float TargetYPosition => _squareSpawner.TotalElements + 1.5f;
        private float _deltaYPos;
        private float _startOrthoSize;

        private void Start()    
        {
            _startOrthoSize = _cam.orthographicSize;
        }

        private void LateUpdate()
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, new(0, TargetYPosition, -10), Time.deltaTime * _lerpSpeed);
            _deltaYPos = TargetYPosition - transform.localPosition.y;
            _cam.orthographicSize = Mathf.Lerp(_cam.orthographicSize, _deltaYPos > 0 ? _startOrthoSize : _startOrthoSize - _deltaYPos * 2, Time.deltaTime * _lerpSpeed * 2);
        }
    }
}