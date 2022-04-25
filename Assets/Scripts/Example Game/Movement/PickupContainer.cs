/*//////////////////////////////////////////////////////////////////////////////////////////
//      █─▄▄▄▄█▄─█─▄█─▄▄▄─█                                                               //
//      █▄▄▄▄─██─▄▀██─███▀█             Scripts created by Semih Kubilay Çetin            //
//      ▀▄▄▄▄▄▀▄▄▀▄▄▀▄▄▄▄▄▀                                                               //
//////////////////////////////////////////////////////////////////////////////////////////*/
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupContainer : MonoBehaviour
{
    [Header("Container Config")]
    [SKC_ReadOnly] [Space,SerializeField] private List<GameObject> containerList = new List<GameObject>();
    [Space, SerializeField] private GameObject stackPrefab = null;
    [Space,SerializeField] private Transform stackLContainer = null;
    [SerializeField] private Transform stackRContainer = null;
    [Space,SerializeField] private float yDifference = .1f;

    [Space, Header("Wings Config")]
    [Space, SerializeField] private float flyDelay = 2f;
    [Space, SerializeField] private float wingAngle = 30f;
    [Space, SerializeField] private float wingTime = 2f;

    // Private
    private int stackCount = 0;
    private float flyTime = 0f;
    
    // Component
    private Transform stackHolder;
    private PlayerMovement playerMovement;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void FixedUpdate()
    {
        if(playerMovement.OnGround())
        {
            flyTime = 0f;

            stackLContainer.transform.rotation = Quaternion.Lerp(stackLContainer.transform.rotation, Quaternion.Euler(0f, 0f, 0f), wingTime * Time.deltaTime);
            stackRContainer.transform.rotation = Quaternion.Lerp(stackRContainer.transform.rotation, Quaternion.Euler(0f, 0f, 0f), wingTime * Time.deltaTime);
        }
        else
        {
            flyTime += .1f;
            if(flyTime > flyDelay)
            {
                flyTime = 0f;
                LostWings(1);
            }

            stackLContainer.transform.rotation = Quaternion.Lerp(stackLContainer.transform.rotation, Quaternion.Euler(0f, 0f, wingAngle), wingTime * Time.deltaTime);
            stackRContainer.transform.rotation = Quaternion.Lerp(stackRContainer.transform.rotation, Quaternion.Euler(0f, 0f, -wingAngle), wingTime * Time.deltaTime);
        }
    }

    public void AddListItem()
    {
        stackCount++;

        if(stackCount % 2 == 1)
        {
            stackHolder = stackLContainer;
        }
        else if(stackCount % 2 == 0)
        {
            stackHolder = stackRContainer;
        }

        GameObject tempStack = (GameObject)Instantiate(stackPrefab, stackHolder);
        tempStack.name = "StackClone" + " " + stackCount.ToString();

        tempStack.transform.localPosition = Vector3.zero;
        tempStack.transform.localPosition = new Vector3(tempStack.transform.localPosition.x, tempStack.transform.localPosition.y + (stackCount / 2) * yDifference, tempStack.transform.localPosition.z);

        if(!containerList.Contains(tempStack))
        {
            containerList.Add(tempStack);
        }
    }

    public void LostWings(int loss)
    {
        for (int i = 0; i < loss; i++)
        {
            GameObject lastWing = containerList.Last<GameObject>();
            containerList.Remove(lastWing);

            Rigidbody wingRb = lastWing.AddComponent<Rigidbody>();
            wingRb.useGravity = true;

            lastWing.transform.parent = null;
            stackCount--;
            Destroy(lastWing, 2f);
        }
    }

    public int GetListCount()
    {
        return containerList.Count;
    }

    [ContextMenu("Test Stack System")]
    public void TestIt()
    {
        AddListItem();
    }

}
/* Tip    #if UNITY_EDITOR
          Debug.Log("Unity Editor");
          #endif                          Tip End */