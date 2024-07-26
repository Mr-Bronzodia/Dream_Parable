using UnityEngine;

public class CameraRig : MonoBehaviour
{
    private Vector3 _defaultPosition;
    private Quaternion _defaultRotation;

    private Vector3 _desiredPosition;
    private Quaternion _desiredRotation;

    [SerializeField] private float _transitionSpeed = 2f;

    private void Start()
    {
        _desiredPosition = transform.position;
        _desiredRotation = transform.rotation;

        _defaultPosition = transform.position;
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
        _desiredPosition = _defaultPosition;
        _desiredRotation = _defaultRotation;
    }
}
