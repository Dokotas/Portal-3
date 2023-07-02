using UnityEngine;

public class PortalGun : MonoBehaviour
{
    [SerializeField] private Portal orange, blue;

    private RaycastHit hit;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f));
            if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("PortalCanvas"))
            {
                if (Input.GetMouseButtonDown(0))
                    CreatePortal(orange, blue);
                else if (Vector3.Distance(hit.point + blue.transform.forward * 0.6f, orange.transform.position)>=5f)
                    CreatePortal(blue, orange);
            }
        }
    }

    private void CreatePortal(Portal portal, Portal other)
    {
        if(Vector3.Distance(hit.point + portal.transform.forward * 0.6f, other.transform.position)<4f && other.gameObject.activeSelf)
            return;
        
        portal.transform.rotation = Quaternion.LookRotation(hit.normal);
        portal.transform.position = hit.point + portal.transform.forward * 0.6f;
        if (!portal.gameObject.activeSelf)
        {
            portal.gameObject.SetActive(true);
            if (other.gameObject.activeSelf)
            {
                EventManager.Singleton.OnBothPortalActive.Invoke();
                portal.ShowTexture();
                other.ShowTexture();
            }
            else if(portal==orange)
                EventManager.Singleton.OnOrangePortalActive.Invoke();
            else
                EventManager.Singleton.OnBluePortalActive.Invoke();

        }
    }
}