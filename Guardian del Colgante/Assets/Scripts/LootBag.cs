using System.Collections.Generic;
using UnityEngine;

public class LootBag : MonoBehaviour
{
    public GameObject droppedItemPrefab;
    public List<Loot> lootList = new List<Loot>();

    Loot GetDroppedItem() { 
    
        int randomNumber = Random.Range(0, 101); //1-100
        List<Loot> possibleLoot = new List<Loot>();

        foreach (Loot item in lootList) {
            if (randomNumber <= item.dropChance) {
                possibleLoot.Add(item);
            }
        }

        if (possibleLoot.Count > 0) { 
        
            Loot droppedItem = possibleLoot[Random.Range(0, possibleLoot.Count)];
            return droppedItem;
        }
        Debug.Log("No item dropped");
        return null;
    }

    public void InstantiateLoot(Vector3 spawnPosition) { 
        Loot droppedItem = GetDroppedItem();

        if (droppedItem != null) {
            GameObject lootGameObject = Instantiate(droppedItem.lootPrefab, spawnPosition, Quaternion.identity);

            float dropForce = 300f;
            Vector2 dropDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            lootGameObject.GetComponent<Rigidbody2D>().AddForce(dropDirection * dropForce, ForceMode2D.Impulse);
        }

    }

}
