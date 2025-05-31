using UnityEngine;

public class LeftBat : MonoBehaviour
{
    public GameObject fireballPrefab;

    public float fireballSpeed = 200f;
    public Vector2 fireballDirection = Vector2.down;
    public Vector3 fireballScale = new Vector3(20f, 20f, 1f);
    public float shootInterval = 6f;
    public int fireballDamage = 10;

    private float shootTimer;


    void Start()
    {

    }

    void Update()
    {
        shootTimer += Time.deltaTime;
        if (shootTimer >= shootInterval)
        {
            ShootFireball();
            shootTimer = 0f;
            ShotCounter.shotCounter += 1;
        }
    }

    void ShootFireball()
    {
        Vector3 spawnPos = transform.position + Vector3.down * 0.5f; // Adjust offset as needed
        GameObject fb = Instantiate(fireballPrefab, spawnPos, Quaternion.identity);
        fb.GetComponent<Fireball>().Initialize(fireballSpeed, fireballDirection, fireballScale,
        fireballDamage, Fireball.FireballPathType.ArchRight);
    }
}