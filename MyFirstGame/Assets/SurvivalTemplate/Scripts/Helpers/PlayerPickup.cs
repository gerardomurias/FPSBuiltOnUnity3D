using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    public Transform mountPoint;



    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collider)
    {
        print("entró...");
        var pickupable = collider.GetComponent(typeof(IPickupable)) as IPickupable;

        if (pickupable == null)
        { return; }

        pickupable.Pickup(this.gameObject);
    }
}