using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("hit");
        GameManager.instance.RestartLevel();
    }
}
