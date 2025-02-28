using UnityEngine;

public class BaseBulletController : MonoBehaviour
{
    Pool<BaseBulletController> pool;
    [SerializeField] GunConfig config;
    public float useTime;
    public float countTime;
    public float damage;
    public float shootingForce = 250;
    [SerializeField] CollisionController collisionController;
    public void Init(GunController gun, Pool<BaseBulletController> _pool, GunConfig gunConfig)
    {
        transform.position = gun.transform.position;
        pool = _pool;
        useTime = gunConfig.useTime;
        damage = gunConfig.damageAmt;
        shootingForce = gunConfig.shootingForce;
        collisionController.collisionEnter += HitCheck;
    }
    void Update()
    {
        transform.position += transform.forward * shootingForce * Time.deltaTime;

        countTime += Time.deltaTime;
        if (countTime >= useTime)
        {
            RecyclinABullet();
        }
    }
    void HitCheck(Collider collider)
    {
        if(collider.TryGetComponent(out EnemyController enemy)) enemy.OnHit();
        RecyclinABullet();
    }
    void RecyclinABullet()
    {
        pool.RecycleItem(this);
    }
}
