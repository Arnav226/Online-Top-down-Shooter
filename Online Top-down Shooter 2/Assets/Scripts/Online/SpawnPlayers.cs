using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject PlayerPrefab;

    public float MinX, MinY, MaxX, MaxY;

    // Start is called before the first frame update
    void Start()
    {
        Vector2 RandomPosition = new Vector2(Random.Range(MinX, MaxX), Random.Range(MinY, MaxY));
        PhotonNetwork.Instantiate(PlayerPrefab.name, RandomPosition, Quaternion.identity);
        //GameObject.FindGameObjectWithTag("Minimap").GetComponent<ProperMinimaping>().AdjustMinimap();
    }

}
