using Photon.Pun;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [HideInInspector]
    public bool Protecting = false;

    private GameObject Protection, Gun;
    PhotonView View;

    // Start is called before the first frame update
    void Start()
    {
        View = GetComponent<PhotonView>();

        for (int Childcount = 0; Childcount < gameObject.transform.childCount; Childcount++)
        {
            if (gameObject.transform.GetChild(Childcount).transform.CompareTag("Shield")) {
                Protection = gameObject.transform.GetChild(Childcount).gameObject;
            }

            else if (gameObject.transform.GetChild(Childcount).transform.CompareTag("Gun")) {
                Gun = gameObject.transform.GetChild(Childcount).gameObject;
            }
        }

        Protection.SetActive(false);
        Protection.GetComponent<SpriteRenderer>().enabled = true;
        Protection.GetComponent<BoxCollider2D>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire2") && !Protecting && View.IsMine)
        {
            View.RPC("UpdateActivity", RpcTarget.All, false);
        }

        if (!Input.GetButton("Fire2") && Protecting && View.IsMine) {
            View.RPC("UpdateActivity", RpcTarget.All, true);
        }
    }

    [PunRPC]
    void UpdateActivity(bool GunActivity) {
        Gun.SetActive(GunActivity);
        Protection.SetActive(!GunActivity);
        Protecting = !GunActivity;
    }
}
