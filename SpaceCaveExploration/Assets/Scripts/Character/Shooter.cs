using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour {
    [Header("Shooting Configurations")]
    public Transform StartingPosition;
    public GameObject ProjectilePrefab;
    public float Cooldown = 2.0f;
    private float m_TimeRemainingToShoot = 0.0f;

    private void Update() {
        m_TimeRemainingToShoot -= Time.deltaTime;
    }

    public void Shoot(Vector3 direction) {
        if(m_TimeRemainingToShoot <= 0.0f) {
            SoundManager.instance.PlayEffect(SoundBank.instance.WeaponShot);
            m_TimeRemainingToShoot = Cooldown;
            Projectile p = Instantiate(ProjectilePrefab, StartingPosition.position, Quaternion.identity).GetComponent<Projectile>();
            p.SetDirection(direction);
        }
    }
}
