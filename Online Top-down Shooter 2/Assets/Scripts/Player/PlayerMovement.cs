using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviour
{
    public float MoveSpeed;
    public Rigidbody2D rb;
    Camera Cam;

    Vector2 Movement, MousePosition;
    PhotonView View;

    void Start()
    {
        Cam = Camera.main;
        View = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (View.IsMine) {
            Movement.x = Input.GetAxisRaw("Horizontal");
            Movement.y = Input.GetAxisRaw("Vertical");

            MousePosition = Cam.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    void FixedUpdate()
    {
        if (View.IsMine) {
            rb.MovePosition(rb.position + Movement.normalized * MoveSpeed * Time.fixedDeltaTime);

            Vector2 LookDir = MousePosition - rb.position;
            float angle = Mathf.Atan2(LookDir.y, LookDir.x) * Mathf.Rad2Deg;
            rb.rotation = angle;
        }
    }
}
