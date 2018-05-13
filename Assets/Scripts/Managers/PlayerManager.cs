using UnityEngine;

public class PlayerManager : MonoBehaviour {

    #region Singleton
    public static PlayerManager instance;
    void Awake()
    {
        instance = this;
    }
    #endregion Singleton

    GameObject player;
    public static GameObject Player { get { return instance.player; } }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }


}
