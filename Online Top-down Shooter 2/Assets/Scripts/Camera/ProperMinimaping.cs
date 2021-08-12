using UnityEngine;
using Photon.Pun;
using System.Collections;

public class ProperMinimaping : MonoBehaviour
{
    private GameObject[] Players;

    private void Start()
    {
        StartCoroutine(AdjustMinimap());
    }

    public IEnumerator AdjustMinimap() {
        yield return new WaitForSecondsRealtime(0.1f);

        Players = GameObject.FindGameObjectsWithTag("Player");

        //for (int PlayerIndex = 0; PlayerIndex < Players.Length; PlayerIndex++)
        //{
        //    Debug.Log("Running for loop");
        //    if (!Players[PlayerIndex].GetPhotonView().IsMine)
        //    {
        //        Debug.Log("Disabling Non-controllable players on minimap");
        //        Players[PlayerIndex].SetActive(false);
        //    }
        //}

        foreach (GameObject Player in Players) {
            Debug.Log("Running loop");
            if (!Player.GetPhotonView().IsMine)
            {
                Debug.Log("Disabling Non-controllable players on minimap");
                Player.SetActive(false);
            }
        }
    }
}
