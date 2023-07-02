using UnityEngine;

public class PickedObject : MonoBehaviour
{
    private Transform _camera;
    private Rigidbody _rb;

    private float _distanceToPickedObject;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.useGravity = false;
        _camera = Camera.main.transform;
        _distanceToPickedObject = Vector3.Distance(transform.position, _camera.position);
    }

    void FixedUpdate()
    {
        var destination = _camera.position + _camera.forward * _distanceToPickedObject;
        if (Vector3.Distance(destination, transform.position) > 1f)
        {
            var direction = (destination - transform.position).normalized * 15f;
            _rb.velocity = direction;
        }
        else
        {
            _rb.velocity = Vector3.zero;
        }
    }

    private void Update()
    {
        _distanceToPickedObject += Input.mouseScrollDelta.y * Settings.ScrollSensitivity;
    }

    private void OnDestroy()
    {
        _rb.useGravity = true;
    }
}