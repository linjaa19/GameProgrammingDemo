using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{

    [SerializeField] GameObject[] itemPrefab;
    [SerializeField] float secondSpawn=0.5f;
    [SerializeField] float minTras=-11;
    [SerializeField] float maxTras=11;


    
    
    GameObject item;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ItemSpawner());
    }

    IEnumerator ItemSpawner() {
        while (true)
        {

            var spawnpoint = Random.Range(minTras, maxTras);
            var position = new Vector3(transform.position.x, transform.position.y,spawnpoint);
            item = Instantiate(itemPrefab[Random.RandomRange(0, itemPrefab.Length)],position, Quaternion.identity);
            
            yield return new WaitForSeconds(secondSpawn);
            Destroy(item, 30f);
        }
    }



    // Update is called once per frame
    void Update()
    {
       
        
        
    }
}
