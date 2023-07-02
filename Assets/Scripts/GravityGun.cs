using UnityEngine;

public class GravityGun : MonoBehaviour
{
    private Camera _camera;
    private PickedObject _pickedObject;

    private void Start()
    {
        _camera = Camera.main;
    }
    
    private void Update()
    {
        var ray = _camera.ViewportPointToRay(new Vector3(.5f, .5f));

        if (Physics.Raycast(ray, out var hit) && Input.GetMouseButtonDown(0) && hit.collider.CompareTag("Pickable"))
            _pickedObject = hit.transform.gameObject.AddComponent<PickedObject>();

        if (Input.GetMouseButtonUp(0) && _pickedObject)
            Destroy(_pickedObject);
    }
}
