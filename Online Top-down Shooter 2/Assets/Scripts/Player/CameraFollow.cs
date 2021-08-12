using UnityEngine;
using Photon.Pun;

public class CameraFollow : MonoBehaviour
{
    PhotonView View;
    Transform PlayerPos;

    Transform CamPos;
    float CameraPosZ;

    // Start is called before the first frame update
    void Start()
    {
        View = GetComponent<PhotonView>();

        PlayerPos = GetComponent<Transform>();

        CamPos = Camera.main.GetComponent<Transform>();
        CameraPosZ = Camera.main.GetComponent<Transform>().position.z;
    }

    // Update is called once per frame
    void Update()
    {
        if (View.IsMine) {
            CamPos.position = new Vector3(PlayerPos.position.x, PlayerPos.position.y, CameraPosZ);
        }
    }
}
