using UnityEngine;

public enum Target
{
    Player,
    Cashier,
}

public class MovementController
{
    public static readonly MovementController Instance = new MovementController();
    public Transform PlayerTrasform { get; private set; }
    public Transform CashierTrasform { get; private set; }

    public void SetTarget(Target target, Transform inputTransform)
    {
        switch(target)
        {
            case Target.Player:
                PlayerTrasform = inputTransform;
                break;

            case Target.Cashier:
                CashierTrasform = inputTransform;
                break;
        }
    }
}
