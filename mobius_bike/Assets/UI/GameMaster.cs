using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static GameMaster Instance { get; private set; }
    
    public Statistics Statistics { get; private set; }
    public DropGenerator DropGenerator { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
       
        Statistics = GetComponentInChildren<Statistics>();
        DropGenerator = GetComponentInChildren<DropGenerator>();
    }
}
