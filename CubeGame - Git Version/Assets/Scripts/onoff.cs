using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class onoff : NetworkBehaviour {
    
    [SerializeField]
    Behaviour[] ComponentsToDisable;
    [SerializeField]
    string remoteLayerName = "RemotePlayer";
    // Use this for initialization
    void Start () {
        if (!isLocalPlayer)
        {
            componentsToDisable();
            AssignRemotePlayer();
        }
    }
    void AssignRemotePlayer()
    {
        gameObject.layer = LayerMask.NameToLayer(remoteLayerName);

    }

    void componentsToDisable ()
    {
        for (int i = 0; i<ComponentsToDisable.Length; i++)
        {
                ComponentsToDisable[i].enabled = false;
        }
    }

}
