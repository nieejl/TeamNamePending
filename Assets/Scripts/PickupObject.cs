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
                    FMODUnity.RuntimeManager.PlayOneShot("event:/VFX/Interactions/coinPickupEvent");
                    break;

                case PickupType.Heart:
                    PlayerDataController.Instance.ChangeHealth(1);
                    FMODUnity.RuntimeManager.PlayOneShot("event:/VFX/Interactions/heartPickupEvent");
                    break;
            }

            Destroy(gameObject);
        }
    }
}
