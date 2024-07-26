using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarBlue : MonoBehaviour
{
    private void OnTriggerEnter(Collider actor)
    {
        if (actor.gameObject.CompareTag("Player"))
        {
            GameManager.instance.keyBlue++;
            this.gameObject.SetActive(false);
        }
    }
}
