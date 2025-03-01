using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform player;
    public float yRotLimit;
    public float rotSpeed = 1;
    public float movingSmootherBy = 0.2f;
    private Vector3 refSpeed = Vector3.zero;
    public float yAngle = 0;

    void Update()
    {
        if (GameManager.Instance.gameState == GameState.GameOff) return;
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;
        if (!GameManager.Instance.aimingToShoot)
        {
            transform.eulerAngles += Vector3.up * mouseX * rotSpeed * Time.deltaTime;

            var currAngles = transform.eulerAngles;

            yAngle += -mouseY * rotSpeed * Time.deltaTime;
            yAngle = Mathf.Clamp(yAngle, -yRotLimit, yRotLimit);
            currAngles.x = yAngle;
            transform.eulerAngles = currAngles;
        }
        transform.position = Vector3.SmoothDamp(transform.position, player.transform.position, ref refSpeed, movingSmootherBy);
    }
}
