using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public float MovementSpeed;
    private PlayerControls controls = null;
    [SerializeField] private PlayerData PlayerCoins;
    private Quaternion _cameraRotation;

    private Rigidbody _rigidbody;
    private Inventory inventory;
    [SerializeField] public float IsMoving;
    FMOD.Studio.EventInstance footstepEvent;

    private void Awake()
    {
        controls = new PlayerControls();
        inventory = GetComponent<Inventory>();

        footstepEvent = FMODUnity.RuntimeManager.CreateInstance("event:/VFX/Player/Footstep/footsteploopEvent");
        footstepEvent.start();

        WireControls();
        _cameraRotation = Camera.main.transform.rotation;
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void WireControls()
    {
        controls.Player.Attack.performed += Attack_performed;
        controls.Player.AlternateAttack.performed += AlternateAttack_performed;
        controls.Player.SelectWeaponOne.performed += SelectWeaponOne_performed;
        controls.Player.SelectWeaponTwo.performed += SelectWeaponTwo_performed;
        controls.Player.SelectWeaponThree.performed += SelectWeaponThree_performed;
        controls.Player.BuyWeapon.performed += TryBuyWeapon;
    }  

    private void SelectWeaponOne_performed(InputAction.CallbackContext obj)
    {
        inventory.EquipWeapon(0);
    }
    private void SelectWeaponTwo_performed(InputAction.CallbackContext obj)
    {
        inventory.EquipWeapon(1);
    }

    private void SelectWeaponThree_performed(InputAction.CallbackContext obj)
    {
        inventory.EquipWeapon(2);
    }   

    private void Attack_performed(InputAction.CallbackContext obj)
    {
        inventory.GetEquippedWeapon().TryDoLightAttack();
    }

    private void AlternateAttack_performed(InputAction.CallbackContext obj)
    {
        inventory.GetEquippedWeapon().TryDoHeavyAttack();
    }

    private void TryBuyWeapon(InputAction.CallbackContext obj)
    {
        if (WeaponBuyController.Instance.IsPlayerWithinBuyRange)
        {
            if (!PendingSystem.Instance.IsPending() && PlayerCoins.CurrentValue >= 10)
            {
                PlayerCoins.ChangeValue(-10);
                PendingSystem.Instance.StartPendingTime(60f);
                inventory.GetEquippedWeapon().RepairWeapon();
                FMODUnity.RuntimeManager.PlayOneShot("event:/VFX/Interactions/weaponPurchaseEvent");

            }
        }
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
        var cameraRelativeDirection = movement.ChangeDirectionRelativeToCamera(Camera.main.transform);
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("isMovingParam", IsMoving);
        var currentPosition = transform.position;
        var targetPosition = currentPosition + cameraRelativeDirection;
        _rigidbody.velocity = cameraRelativeDirection * MovementSpeed;
        
        // transform.Translate(cameraRelativeDirection * (MovementSpeed * deltaTime), Space.World);
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
