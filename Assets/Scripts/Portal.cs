using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private Portal other;
    [SerializeField] private Material closed, open;
    public Camera portalCamera;

    private MeshRenderer _meshRenderer;

    private void Start()
    {
        _meshRenderer = GetComponentInChildren<MeshRenderer>();
        other.portalCamera.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        _meshRenderer.sharedMaterial = closed;
        gameObject.SetActive(false);
    }

    //Отображение текстуры
    public void ShowTexture()
    {
        _meshRenderer.sharedMaterial = open;
        _meshRenderer.sharedMaterial.mainTexture = other.portalCamera.targetTexture;
    }

    private void Update()
    {
        // Position
        Vector3 lookerPosition = other.transform.worldToLocalMatrix.MultiplyPoint3x4(Camera.main.transform.position);
        lookerPosition = new Vector3(-lookerPosition.x, lookerPosition.y, -lookerPosition.z);
        portalCamera.transform.localPosition = lookerPosition;

        // Rotation
        Quaternion difference = transform.rotation *
                                Quaternion.Inverse(other.transform.rotation * Quaternion.Euler(0, 180, 0));
        portalCamera.transform.rotation = difference * Camera.main.transform.rotation;

        // Clipping
        portalCamera.nearClipPlane = lookerPosition.magnitude;
    }

    //Телепортация

    private void OnTriggerStay(Collider otherCol)
    {
        float zPos = transform.worldToLocalMatrix.MultiplyPoint3x4(otherCol.transform.position).z;

        if (zPos < 0 && other.gameObject.activeSelf) Teleport(otherCol.transform);
    }

    private void Teleport(Transform obj)
    {
        // Position
        Vector3 localPos = transform.worldToLocalMatrix.MultiplyPoint3x4(obj.position);
        localPos = new Vector3(-localPos.x, localPos.y, -localPos.z);
        obj.position = other.transform.localToWorldMatrix.MultiplyPoint3x4(localPos);

        // Rotation
        Quaternion difference = other.transform.rotation *
                                Quaternion.Inverse(transform.rotation * Quaternion.Euler(0, 180, 0));
        obj.rotation = difference * obj.rotation;
    }
    
}