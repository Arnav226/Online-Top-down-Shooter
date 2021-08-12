using UnityEngine;
using Photon.Pun;
using System;
using UnityEngine.UI;

public class Synchronize_Player : MonoBehaviour, IPunObservable
{
    double LastNetworkDataRecievedTime;
    Vector3 NetworkPosition;
    Rigidbody2D rb;
    Quaternion NetworkRotation;
    PhotonView View;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        View = GetComponent<PhotonView>();

        //Change if it's too slow
        //Default is 20 (SendRate) & 10 (SerializationRate)
        PhotonNetwork.SendRate = 40;
        PhotonNetwork.SerializationRate = 40;
    }

    void Update()
    {
        if (!View.IsMine) {
            UpdateNetworkPosition();
            UpdateNetworkRotation();
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else {
            NetworkPosition = (Vector3)stream.ReceiveNext();
            NetworkRotation = (Quaternion)stream.ReceiveNext();

            LastNetworkDataRecievedTime = info.SentServerTime;
        }
    }

    void UpdateNetworkPosition() {
        Vector3 NewPosition = Vector3.MoveTowards(transform.position, NetworkPosition, 5000f * Time.deltaTime);

        if (Vector3.Distance(transform.position, NetworkPosition) > 2f) {
            NewPosition = NetworkPosition;
        }

        transform.position = NewPosition;
    }

    void UpdateNetworkRotation() {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, NetworkRotation, 1000f * Time.deltaTime);
    }
}
