using UnityEngine;
using Photon.Pun;

public class Synchronize_Bullet : MonoBehaviour, IPunObservable
{
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
        }
        else {
            transform.position = (Vector3)stream.ReceiveNext();
        }
    }
}
