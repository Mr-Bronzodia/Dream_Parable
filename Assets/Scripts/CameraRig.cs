using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRig : MonoBehaviour
{
    private Vector3 _defalutPosition;
    private Quaternion _defaultRotation;

    private Vector3 _desiredPosition;
    private Quaternion _desiredRotation;

    [SerializeField] private float _transitionSpeed = 2f;

    private void Start()
    {
        _desiredPosition = transform.position;
        _desiredRotation = transform.rotation;

        _defalutPosition = transform.position;
        _defaultRotation = transform.rotation;
    }


    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, _desiredPosition, Time.deltaTime * _transitionSpeed);
        transform.rotation = Quaternion.Slerp(transform.rotation, _desiredRotation, Time.deltaTime * _transitionSpeed);
    }

    public void MoveRigTo(Vector3 position, Quaternion rotation)
    {
        _desiredPosition = position;
        _desiredRotation = rotation;
    }

    public void MoveRigToDefaultPosition()
    {
        _desiredPosition = _defalutPosition;
        _desiredRotation = _defaultRotation;
    }
}
