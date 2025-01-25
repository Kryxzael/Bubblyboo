using Assets.Etc;

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUIBelt : MonoBehaviour
{
    public ShopUIItem ItemPrefab;
    public GameObject OutOfStockPrefab;

    public float Spacing = 1f;

    public float AnimationDelay = 0.25f;

    private void Start()
    {
        RegenerateItems(true);
    }

    public void RegenerateItems(bool animateEntry)
    {
        StartCoroutine(CoRegenerateItems(animateEntry));
    }

    private IEnumerator CoRegenerateItems(bool animateEntry)
    {
        float xPos = 0;

        foreach (Transform i in GetComponentInChildren<Transform>())
            Destroy(i.gameObject);

        int itemIndex = 0;
        foreach (ShopItem i in Game.Instance.Shop.Items)
        {
            if (i == null)
            {
                Instantiate(OutOfStockPrefab, new Vector3(xPos, 0, 0), Quaternion.identity, transform);
            }
            else
            {
                ShopUIItem uiItem = Instantiate(ItemPrefab, new Vector3(xPos, 0, 0), Quaternion.identity, transform);
                uiItem.Item = i;
                uiItem.ShopIndex = itemIndex;
                uiItem.Initialize();

                if (animateEntry)
                    uiItem.GetComponent<ScaleIn>().ScaleNow();
            }

            if (animateEntry)
                yield return new WaitForSeconds(AnimationDelay);

            xPos += Spacing;
            itemIndex++;
        }
    }
}
