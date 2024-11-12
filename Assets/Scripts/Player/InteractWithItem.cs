using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class InteractWithItem : MonoBehaviour
{
    [SerializeField]
    LayerMask layerMaskItem;

    [SerializeField]
    LayerMask layerMaskContainer;

    [SerializeField]
    LayerMask layerMaskIgnoreRaycast;

    [SerializeField]
    float maxDistanceGrap = 10;

    Transform currentOblectOnHand;

    void Grap(Transform item, Transform target)
    {
        item.transform.SetParent(target.transform);
        item.transform.localPosition = Vector3.zero;
        item.transform.localRotation = Quaternion.Euler(0,0,0);

        currentOblectOnHand = item;
    }
    void Drop(Transform target)
    {
        currentOblectOnHand.SetParent(target);

        Vector3 randPos = new Vector3(Random.Range(-0.4f, 0.4f),         
                                      0.35f,
                                      Random.Range(-0.4f,0.4f));

        currentOblectOnHand.localPosition = randPos;
        currentOblectOnHand.localRotation = (Quaternion.Euler(Vector3.zero));
        currentOblectOnHand.gameObject.layer = layerMaskIgnoreRaycast.value;
        currentOblectOnHand =null;
    }
    public void Action(Transform target)
    {
        (Transform, InteractObjecs) CurrentItem = CheckItem();

        if (CurrentItem.Item1 == null) return;

        switch (CurrentItem.Item2)
        {
            case InteractObjecs.item:
                if (currentOblectOnHand == null)
                    Grap(CurrentItem.Item1, target);
                break;
            case InteractObjecs.contaner:
                if (currentOblectOnHand != null)
                    Drop(CurrentItem.Item1);
                    break;
        }
    }


    (Transform, InteractObjecs) CheckItem()
    {
        Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);

        Ray ray = Camera.main.ScreenPointToRay(screenCenter);

        Debug.DrawRay(ray.origin, ray.direction * maxDistanceGrap, Color.red);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, maxDistanceGrap))
        {
            if (1 << hit.transform.gameObject.layer == layerMaskItem.value)
            {
                //вывести сообщение
                return (hit.transform, InteractObjecs.item);
            }
            if (1 << hit.transform.gameObject.layer == layerMaskContainer.value)
            {
                return (hit.transform, InteractObjecs.contaner);

            }
        }
        return (null, InteractObjecs.none);
    }


    private void Update()
    {
        CheckItem();
    }
}
public enum InteractObjecs { none, item, contaner }
