using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public float MovementSpeed;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void Move()
    {
        var movement = new Vector3();
        var deltaTime = Time.deltaTime;

        if (Keyboard.current.wKey.isPressed) movement.z += 1;
        if (Keyboard.current.sKey.isPressed) movement.z -= 1;
        if (Keyboard.current.aKey.isPressed) movement.x -= 1;
        if (Keyboard.current.dKey.isPressed) movement.x += 1;
        
        movement.Normalize();
        transform.Translate(movement * (MovementSpeed * deltaTime));
    }

    public void UpdatePlayerDirection()
    {
    }
}
