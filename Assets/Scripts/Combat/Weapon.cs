using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Transform _firePoint;
    [SerializeField] private GameObject _ammoPrefab;

    public void Shoot()
    {
        GameObject ammo = Instantiate(_ammoPrefab, _firePoint.position, _firePoint.rotation);
        Rigidbody rb = ammo.GetComponent<Rigidbody>();
        rb.linearVelocity = _firePoint.forward * 20f;
    }

}
