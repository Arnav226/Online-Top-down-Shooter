using UnityEngine;
using Photon.Pun;
using System;

public class Health : MonoBehaviour, IPunObservable
{
    public float MaxHealth = 100;
    HealthBar HB;

    [HideInInspector]
    public float CurrentHealth;

    // Start is called before the first frame update
    void Start()
    {
        HB = GetComponentInChildren<HealthBar>();

        CurrentHealth = MaxHealth;
        HB.SetMaxHealth(MaxHealth);
    }

    void Update()
    {
        if (CurrentHealth <= 0f && gameObject.GetPhotonView().IsMine) {
            try
            {
                GameObject.Find("Minimap Canvas").GetComponent<LeaveRoom>().ReturnToLobby();
            }
            catch (Exception) {
                return;
            }
        }
    }

    [PunRPC]
    void TakeDamage (float Damage) {
        CurrentHealth -= Damage;

        HB.SetHealth(CurrentHealth);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(CurrentHealth);
        }
        else if (stream.IsReading && !gameObject.GetPhotonView().IsMine)
        {
            try
            {
                CurrentHealth = (float)stream.ReceiveNext();
                HB.SetHealth(CurrentHealth);
            }
            catch (NullReferenceException) {
                return;
            }
        }
    }
}
