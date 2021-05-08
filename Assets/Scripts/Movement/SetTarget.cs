using UnityEngine;

public class SetTarget : MonoBehaviour
{
    [SerializeField]
    private Target _currentTarget;

    private void Awake()
    {
        MovementController.Instance.SetTarget(_currentTarget, transform);
    }
}
