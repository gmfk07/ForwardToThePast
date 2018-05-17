using UnityEngine;

public class PlayerManager : MonoBehaviour {

    #region Singleton
    public static PlayerManager instance;
    void Awake()
    {
        instance = this;
        player = GameObject.FindGameObjectWithTag("Player");
    }
    #endregion Singleton

    GameObject player;
    public GameObject Player { get { return instance.player; } }

    private void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
    }


}
