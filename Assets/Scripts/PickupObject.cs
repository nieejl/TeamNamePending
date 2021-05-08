using System.Collections;
using System.Collections.Generic;
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
            print ("Item picked up");
            switch(currentPickupType)
            {
                case PickupType.Coin:

                    break;

                case PickupType.Heart:

                    break;
            }


            Destroy(gameObject);
        }
    }
}
