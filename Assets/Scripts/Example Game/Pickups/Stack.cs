/*//////////////////////////////////////////////////////////////////////////////////////////
//      █─▄▄▄▄█▄─█─▄█─▄▄▄─█                                                               //
//      █▄▄▄▄─██─▄▀██─███▀█             Scripts created by Semih Kubilay Çetin            //
//      ▀▄▄▄▄▄▀▄▄▀▄▄▀▄▄▄▄▄▀                                                               //
//////////////////////////////////////////////////////////////////////////////////////////*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stack : MonoBehaviour
{
    [Header("Stack Config")]
    [Space, SerializeField] private GameObject stackEffect = null;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            StackAdd(other.gameObject);
        }
    }

    private void StackAdd(GameObject player)
    {
        Collider tempCollider = GetComponent<Collider>();
        tempCollider.enabled = false;

        Renderer temp = GetComponent<Renderer>(); // Temp
        temp.enabled = false;

        PickupContainer pickContainer = player.GetComponent<PickupContainer>();
        pickContainer.AddListItem();

        GameObject tempObject = (GameObject)Instantiate(stackEffect, transform.position, transform.rotation);
        Destroy(tempObject, 1f);

        Destroy(this.gameObject, 2f);

    }
}
/* Tip    #if UNITY_EDITOR
          Debug.Log("Unity Editor");
          #endif                          Tip End */