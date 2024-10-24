using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponType weapon;
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private Transform holderWeapon;
    [SerializeField] private GameObject holderBullet;
    [SerializeField] private float bulletSpeed;

    public void Shoot()
    {
        var bullet = Instantiate(bulletPrefab, holderWeapon.position, holderWeapon.rotation, holderBullet.transform).GetComponent<Bullet>();
        bullet.SetOwner(gameObject);
       
        Rigidbody rigidbodyWeapon = bullet.GetComponent<Rigidbody>();
        if(rigidbodyWeapon != null)
        {
            rigidbodyWeapon.velocity = transform.forward * bulletSpeed;
        }
    }
}