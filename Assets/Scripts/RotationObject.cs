using UnityEngine;

public class RotationObject : MonoBehaviour
{
    [SerializeField] Transform _targetTransform;
    private Vector3 _lastCameraPosition;

    void Start()
    {
        // _camera = Camera.main;
        UpdateRotation();
    }

    void LateUpdate()
    {
        if (!IsCameraMoved()) return;
            
        UpdateRotation();
    }

    void UpdateRotation()
    {
        LookAtCamera();
        SetLastCameraPosition();
    }

    private void LookAtCamera()
    {
        transform.LookAt(_targetTransform);
    }

    private void SetLastCameraPosition()
    {
        _lastCameraPosition = _targetTransform.position;
    }

    private bool IsCameraMoved()
    {
        return _lastCameraPosition != _targetTransform.position;
    }
}