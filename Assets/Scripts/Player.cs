using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed, jumpForce, playerHeight;

    private Rigidbody _rb;
    private Vector3 _moveDirection;
    private bool _grounded;
    private List<MonoBehaviour> _guns = new List<MonoBehaviour>();
    private int _currentGunIndex;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        
        _guns.Add(GetComponent<GravityGun>());
        _guns.Add(GetComponent<PortalGun>());
    }

    private void Update()
    {
        MyInput();

        _grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * .5f + 0.3f);
        Move();
    }

    private void MyInput()
    {
        _moveDirection = transform.forward * Input.GetAxis("Vertical") +
                        transform.right * Input.GetAxis("Horizontal");
        _moveDirection = _moveDirection.normalized * speed;
        
        if(Input.GetKeyDown(KeyCode.Q))
            ChangeGun();
    }

    private void Move()
    {
        _rb.velocity = new Vector3(_moveDirection.x, _rb.velocity.y, _moveDirection.z);

        if (Input.GetButtonDown("Jump") && _grounded)
            Jump();
    }

    private void Jump()
    {
        _rb.velocity = new Vector3(_rb.velocity.x, 0f, _rb.velocity.z);
        _rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ChangeGun()
    {
        _currentGunIndex = (_currentGunIndex+1)%_guns.Count;
        for (int i = 0; i < _guns.Count; i++)
            _guns[i].enabled = i==_currentGunIndex;
        EventManager.Singleton.OnChangeGun.Invoke(_currentGunIndex);
    }
}