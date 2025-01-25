using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUIBelt : MonoBehaviour
{
    public ShopUIItem ItemPrefab;

    public float Spacing = 1f;

    private void Start()
    {
        RegenerateItems();
    }

    public void RegenerateItems()
    {
        float xPos = 0;

        foreach (Transform i in GetComponentInChildren<Transform>())
            Destroy(i.gameObject);

        foreach (ShopItem i in Game.Instance.Shop.Items)
        {
            ShopUIItem uiItem = Instantiate(ItemPrefab, new Vector3(xPos, 0, 0), Quaternion.identity, transform);
            uiItem.Item = i;
            uiItem.Initialize();

            xPos += Spacing;
        }
    }
}
