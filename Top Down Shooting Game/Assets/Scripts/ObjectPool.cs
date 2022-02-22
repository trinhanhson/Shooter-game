using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectPool : MonoBehaviour
{
    public Text bulletText;
    public static ObjectPool sharedInstance;
    public int amountToPool;
    [SerializeField] GameObject objectToPool;
    public List<GameObject> pooledObjects;

    void Awake()
    {
        sharedInstance = this;
    }

    public void StartGame(int difficulty)
    {
        amountToPool = difficulty;
        GameObject temp;
        for (int i = 0; i < amountToPool; i++)
        {
            temp = Instantiate(objectToPool);
            temp.SetActive(false);
            temp.transform.SetParent(transform);
            pooledObjects.Add(temp);
        }
        bulletText.text = "Bullet: " + amountToPool;
    }

    void Update()
    {
        int bulletRemain = 0;
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                bulletRemain++;
            }
        }
        bulletText.text = "Bullet: " + bulletRemain;
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }
}