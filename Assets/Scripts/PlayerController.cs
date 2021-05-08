using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public float MovementSpeed;
    private PlayerControls controls = null;
    [SerializeField] public float IsMoving;
    FMOD.Studio.EventInstance footstepEvent;

    private void Awake()
    {
        controls = new PlayerControls();
        footstepEvent = FMODUnity.RuntimeManager.CreateInstance("event:/VFX/Player/Footstep/footsteploopEvent");
        footstepEvent.start();

    }

    private void OnEnable()
    {
        controls.Player.Enable();
    }

    private void OnDisable()
    {
        controls.Player.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        UpdatePlayerDirection();
    }

    public void Move()
    {
        var deltaTime = Time.deltaTime;
        var movementInput = controls.Player.Movement.ReadValue<Vector2>();
        IsMoving = movementInput.x == 0f && movementInput.y == 0f ? 0f : 1f;
        var movement = new Vector3()
        {
            x = movementInput.x,
            z = movementInput.y,
        }.normalized;
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("isMovingParam", IsMoving);
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
        
        transform.LookAt(playerPosition + normalizedDirection * 10, Vector3.up);
    }
}
