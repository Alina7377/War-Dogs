
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] float _smoothness;
    [SerializeField] Transform _targetObject;
    private Vector3 _initalOffset;
    private Vector3 _cameraPosition;

    void Start()
    {
        //_initalOffset = transform.position - _targetObject.position;
    }

    private void LateUpdate()
    {
        /*
        if (_targetObject == null)
            return;
        _cameraPosition = _targetObject.position + _initalOffset;
        transform.position = Vector3.Lerp(transform.position, _cameraPosition, _smoothness * Time.fixedDeltaTime);*/
    }

}
