using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private Transform holderWeapon;
    [SerializeField] private GameObject holderBullet;
    [SerializeField] private float bulletSpeed;
    private Character character;

    public void SetOwner(Character _character)
    {
        this.character = _character;
    }

    public void Shoot()
    {
        var bullet = Instantiate(bulletPrefab, holderWeapon.position, holderWeapon.rotation, holderBullet.transform).GetComponent<Bullet>();
        Rigidbody rigidbodyBullet = bullet.GetComponent<Rigidbody>();
        bullet.SetOwner(character);
        if(bullet != null)
        {
            rigidbodyBullet.velocity = transform.forward * bulletSpeed;
        }
    }
}