using UnityEngine;

public class PickupObject : MonoBehaviour
{
    public enum PickupType
    {
        Heart,
        Coin,
    }

    [SerializeField]
    private PickupType currentPickupType;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            switch(currentPickupType)
            {
                case PickupType.Coin:
                    PlayerDataController.Instance.ChangeCoins(1);
                    break;

                case PickupType.Heart:
                    PlayerDataController.Instance.ChangeHealth(1);
                    break;
            }

            Destroy(gameObject);
        }
    }
}
