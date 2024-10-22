using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalScript : MonoBehaviour
{
    public Transform destination;
    public Vector3 playerSize;
    private PortalManager portalManager;

    private void Start()
    {
        portalManager = GetComponentInParent<PortalManager>();
    }

    private void OnTriggerEnter(Collider collider)
    { 
        bool canTeleport = portalManager.canTeleport;

        if(collider.gameObject.tag == "Player")
        {
            collider.gameObject.transform.position = destination.position + playerSize;
            print(collider.gameObject.transform.position);
            portalManager.canTeleport = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            portalManager.canTeleport = true;
        }

    }
}
