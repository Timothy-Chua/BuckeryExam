using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarRed : MonoBehaviour
{
    private void OnTriggerEnter(Collider actor)
    {
        if (actor.gameObject.CompareTag("Player"))
        {
            GameManager.instance.keyRed++;
            this.gameObject.SetActive(false);
        }
    }
}
