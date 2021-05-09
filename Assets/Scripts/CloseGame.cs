using UnityEngine;
using UnityEngine.InputSystem;

public class CloseGame : MonoBehaviour
{
    void Update()
    {
        if(Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Application.Quit();
        }
    }
}
