using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject[] objects;
    public Camera Camera;
    public Vector2 spawnMin;
    public Vector2 spawnMax;
    public float spawnRate;
    public float nextSpawn;
    private float curTime = 0.0f; // A definition that does not need to be here

    // Start is called before the first frame update
    void Start()
    {
        spawnMin = Camera.ViewportToWorldPoint(new Vector3(0,1));
        Vector3 offset = new Vector2(0,3);
        spawnMax = Camera.ViewportToWorldPoint(new Vector3(1,1)) + offset;
        
        nextSpawn = curTime + spawnRate;
    }

    // Update is called once per frame
    void Update()
    {
        curTime += Time.deltaTime;
        if(curTime >= nextSpawn)
        {
            GameObject  i = Instantiate(objects[Random.Range(0, objects.Length)], new Vector2(Random.Range(spawnMin.x, spawnMax.x), Random.Range(spawnMin.y, spawnMax.y)), Quaternion.identity);
            i.GetComponent<Drift>().Camera = Camera;
            nextSpawn = curTime + spawnRate;
        }
    }
}