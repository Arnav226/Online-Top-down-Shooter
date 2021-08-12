using UnityEngine;
using Photon.Pun;
using System.Collections;

public class Shooting : MonoBehaviour
{
    public Transform FirePoint;
    public GameObject BulletPrefab;

    public float BulletForce = 30f;

    [Header("Shots per Second")]
    public float SPS = 0.5f;

    float TempTime = 0f;
    PhotonView View;

    void Start()
    {
        View = GetComponent<PhotonView>();    
    }

    // Update is called once per frame
    void Update()
    {
        if (View.IsMine) {
            TempTime += Time.deltaTime;

            if (Input.GetButton("Fire1") && TempTime >= SPS && !GetComponent<Shield>().Protecting) {
                Shoot();
                TempTime = 0f;
            }
        }
    }

    void Shoot()
    {
        GameObject Bullet = PhotonNetwork.Instantiate(BulletPrefab.name, FirePoint.position, FirePoint.rotation);
        Bullet.GetComponent<Bullet>().GetOwnerViewId(gameObject.GetComponent<PhotonView>().ViewID);
    }

    public IEnumerator DestroyBullet(GameObject Bullet, float Delay) {
        yield return new WaitForSecondsRealtime(Delay);

        PhotonNetwork.Destroy(Bullet);
    }
}
