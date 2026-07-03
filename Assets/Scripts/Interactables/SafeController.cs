using UnityEngine;

public class SafeController : MonoBehaviour
{
    private LootBox lootBox;
    
    private void Awake()
    {
        lootBox = GetComponent<LootBox>();
    }

    public void OpenSafe()
    {
        if (lootBox == null)
        {
            Debug.LogError("LootBox component not found!");
            return;
        }

        lootBox.Open();
        
    }

    public bool IsOpen()
    {
        return lootBox != null && lootBox.isOpen;
    }
}