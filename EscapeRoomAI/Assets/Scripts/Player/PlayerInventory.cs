using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance;

    private HashSet<string> keys = new HashSet<string>();

    private void Awake()
    {
        // Ensure there's only one instance of the inventory
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void AddKey(string keyName)
    {
        keys.Add(keyName);
    }

    public bool HasKey(string keyName)
    {
        return keys.Contains(keyName);
    }
}
