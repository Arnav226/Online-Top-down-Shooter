using Photon.Pun;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeaveRoom : MonoBehaviour
{
    public void ReturnToLobby() {
        StartCoroutine(Leave());
    }

    IEnumerator Leave () {
        PhotonNetwork.LeaveRoom();

        while (PhotonNetwork.InRoom) {
            yield return null;
        }

        SceneManager.LoadScene("Lobby");
    }
}
