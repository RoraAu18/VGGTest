using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    FPSPlayer player;
    Pool<EnemyController> pool;
    Rigidbody rb;
    [SerializeField] CollisionController collisionController;
    public float speed;
    bool isDead;
    public void Init(Pool<EnemyController> _pool)
    {
        pool = _pool;
        TryGetComponent(out rb);
        collisionController.collisionEnter += CheckIfFoundTarget;
        collisionController.collisionStay += FollowPlayer;
        collisionController.collisionExit += CleanTarget;
        isDead = false;
    }

    void CheckIfFoundTarget(Collider collider)
    {
        if (collider.TryGetComponent(out FPSPlayer playerFound)) player = playerFound;
    }
    void FollowPlayer(Collider collider)
    {
        if(player != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed);
        }
    }
    void CleanTarget(Collider collider)
    {
        if (player != null) player = null;
    }
    public void OnHit()
    {
        if (!isDead)
        {
            isDead = true;
            RecycleEnemy();
        }
    }
    public void RecycleEnemy()
    {
        GameManager.Instance.RemoveEnemy();
        pool.RecycleItem(this);
        gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        collisionController.collisionEnter -= CheckIfFoundTarget;
        collisionController.collisionStay -= FollowPlayer;
        collisionController.collisionExit -= CleanTarget;
    }
}
