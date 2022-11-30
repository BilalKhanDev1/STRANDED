using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAreaSpawner : MonoBehaviour
{
    public GameObject itemToSpread;
    public int numItemsToSpawn = 50;

    public float itemXSpread = 150;
    public float itemYSpread = 0;
    public float itemZSpread = 150;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < numItemsToSpawn; i++)
        {
            SpreadItem();
        }
    }
    private void Update()
    {
        if (numItemsToSpawn < 50)
        {
            SpreadItem();
        }
    }


    void SpreadItem()
    {
        Vector3 randPosition = new Vector3(Random.Range(-itemXSpread, itemXSpread), Random.Range(-itemYSpread, itemYSpread), Random.Range(-itemZSpread, itemZSpread)) + transform.position;
        GameObject clone = Instantiate(itemToSpread, randPosition, itemToSpread.transform.rotation);
    }
}