using System.Collections.Generic;
using UnityEngine;

public class LootPool : MonoBehaviour
{
    [SerializeField] private List<LootItem> lootList;

    public void DropLoot() {
        foreach(LootItem loot in lootList) {
            if(Random.Range(0, 100) <= loot.dropChance) {
                Instantiate(loot.item, new Vector3(transform.position.x, 0 , transform.position.z), Quaternion.identity);
            }
        }
    }
}
