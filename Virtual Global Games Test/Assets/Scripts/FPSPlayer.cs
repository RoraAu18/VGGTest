using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSPlayer : MonoBehaviour
{
    public CharacterController cc;
    public CollisionController collisionController;
    Rigidbody rb;
    GameManager gameManager => GameManager.Instance;
    HealthController healthController => HealthController.Instance;
    [Header("DetectionZone")]
    public Collider detectionZone;
    public Collider internalCollider;
    public float speed = 10;
    public float jumpHeight;
    public float gravity;
    Vector3 velocity;
    bool isGrounded;
    float rotSpeed = 80;
    public void Init()
    {
        TryGetComponent(out collisionController);
        TryGetComponent(out rb);
        transform.position = gameManager.startLine.position;
        collisionController.collisionEnter += OnCollided;
    }
    void Update()
    {
        if (GameManager.Instance.gameState == GameState.GameOff) return;
        BasicMovement();
        Jump();
        FixView();
    }
    void BasicMovement()
    {
        var movX = Input.GetAxis("Horizontal");
        var movZ = Input.GetAxis("Vertical");

        Vector3 mov = transform.forward * movZ * speed * Time.deltaTime;
        mov += transform.right * movX * speed * Time.deltaTime;

        cc.Move(mov);
    }
    void Jump()
    {
        isGrounded = cc.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; 
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        velocity.y += gravity * Time.deltaTime;
        cc.Move(velocity * Time.deltaTime);
    }
    void FixView()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        transform.eulerAngles += Vector3.up * mouseX * rotSpeed * Time.deltaTime;
    }
    void OnCollided(Collider collider)
    {
        if (collider.TryGetComponent(out EnemyController enemy))
        {
            healthController.ReduceHealth(0.1f);
            enemy.RecycleEnemy();
        }
        else if (collider.TryGetComponent(out Collectable collectable)) collectable.OnCollected();
        else if (collider.transform == gameManager.finishLine)
        {
            gameManager.OnGameEnded(won: true);
        }
    }
    private void OnDisable()
    {
        collisionController.collisionEnter -= OnCollided;
    }
}
