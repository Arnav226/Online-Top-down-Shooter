using Photon.Pun;
using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject HitEffect;
    public float BulletForce = 30f;

    private int OwnerId;
    private bool ShotBefore = false, Killed = false;
    private GameObject Owner;

    float Timer = 0f;
    Transform Firepoint;
    Rigidbody2D rb;

    void Start()
    {
       rb = GetComponent<Rigidbody2D>();
       Firepoint = GetComponent<Transform>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            collision.collider.gameObject.GetPhotonView().RPC("TakeDamage", RpcTarget.All, UnityEngine.Random.Range(5f, 10f));
        }

        GameObject Explosion = Instantiate(HitEffect, transform.position, Quaternion.identity);
        try
        {
            StartCoroutine(Owner.GetComponent<Shooting>().DestroyBullet(Explosion, 1f));
            StartCoroutine(Owner.GetComponent<Shooting>().DestroyBullet(gameObject, 0f));
        }
        catch (NullReferenceException) {
            return;
        }
    }

    void Update()
    {
        if (!ShotBefore) {
            rb.AddForce(Firepoint.right * BulletForce, ForceMode2D.Impulse);
            ShotBefore = true;
        }

        Timer += Time.deltaTime;

        if (Timer >= 5f && !Killed) {
            Killed = true;
            try
            {
                StartCoroutine(Owner.GetComponent<Shooting>().DestroyBullet(gameObject, 0f));
            }
            catch (NullReferenceException) {
                return;
            }
        }
    }

    public void GetOwnerViewId(int ViewId) {
        OwnerId = ViewId;
        Owner = PhotonView.Find(OwnerId).gameObject;
    }
}
