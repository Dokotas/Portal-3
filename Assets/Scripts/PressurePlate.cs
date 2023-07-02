using UnityEngine;
using UnityEngine.Events;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] UnityEvent pressed, notPressed; 
    
    private Animation _animation;

    private void Start()
    {
        _animation = GetComponent<Animation>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        pressed.Invoke();
        _animation.Play("PressurePlateDown");
    }
    
    private void OnCollisionExit(Collision collision)
    {
        notPressed.Invoke();
        _animation.Play("PressurePlateUp");
    }
}
