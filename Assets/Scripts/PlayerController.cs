using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public float MovementSpeed;
    private PlayerControls controls = null;

    public string debugText;

    private void Awake()
    {
        controls = new PlayerControls();
    }

    private void OnEnable()
    {
        controls.Player.Enable();
    }

    private void OnDisable()
    {
        controls.Player.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        UpdatePlayerDirection();
    }
    
    private void OnGUI()
    {
        GUI.TextField(new Rect(0f, 0f, 100f, 200f), debugText);
    }

    public void Move()
    {
        var deltaTime = Time.deltaTime;
        var movementInput = controls.Player.Movement.ReadValue<Vector2>();
        var movement = new Vector3()
        {
            x = movementInput.x,
            z = movementInput.y,
        }.normalized;

        transform.Translate(movement * (MovementSpeed * deltaTime), Space.World);
    }

    public void UpdatePlayerDirection()
    {
        var mouseScreenPosition = Mouse.current.position;
        var mouseScreenVector3 = new Vector3(mouseScreenPosition.x.ReadValue(), mouseScreenPosition.y.ReadValue(), 0f);
        var mouseWorldPosition = mouseScreenVector3.ToWorldPoint();

        var playerPosition = transform.position;
        var direction = (mouseWorldPosition - playerPosition);
        direction.y = 0f;
        var normalizedDirection = direction.normalized;
        
        debugText = mouseWorldPosition.ToString();
        transform.LookAt(playerPosition + normalizedDirection * 10, Vector3.up);
    }
}
