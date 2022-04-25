/*//////////////////////////////////////////////////////////////////////////////////////////
//      █─▄▄▄▄█▄─█─▄█─▄▄▄─█                                                               //
//      █▄▄▄▄─██─▄▀██─███▀█             Scripts created by Semih Kubilay Çetin            //
//      ▀▄▄▄▄▄▀▄▄▀▄▄▀▄▄▄▄▄▀                                                               //
//////////////////////////////////////////////////////////////////////////////////////////*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickups : MonoBehaviour
{
    [Header("Pickup Config")]
    [SerializeField] private GameObject pickupEffect = null;
    [SerializeField] private double gainCoin = 5;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            GUIController.Instance.UpdateCurrency(gainCoin);

            GameObject tempEffect = (GameObject)Instantiate(pickupEffect, transform);
            Destroy(tempEffect, 1f);

            GetComponent<Renderer>().enabled = false;
            Destroy(this.gameObject, 2f);
        }
    }
}
/* Tip    #if UNITY_EDITOR
          Debug.Log("Unity Editor");
          #endif                          Tip End */