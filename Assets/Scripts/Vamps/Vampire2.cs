using UnityEngine;

public class Vampire2 : MonoBehaviour
{
    float scaleX = 280.0f;
    float scaleY = 280.0f;
    float scaleZ = 1.0f;

    public float moveSpeed = 70f;
    public float moveRange = 280f;

    private float startX;
    private int direction = 1;

    public GameObject fireballPrefab;
    public float fireballSpeed = 150f;
    public Vector2 fireballDirection = Vector2.down;
    public Vector3 fireballScale = new Vector3(40f, 40f, 1f);
    public float shootInterval = 0.3f;
    public int fireballDamage = 30;

    private float shootTimer;

    void Start()
    {
        transform.localScale = new Vector3(scaleX, scaleY, scaleZ);
        startX = transform.position.x;
    }

    void Update()
    {
        // Move left/right
        transform.Translate(Vector3.right * direction * moveSpeed * Time.deltaTime);

        // Reverse direction when reaching the edge of the move range
        if (Mathf.Abs(transform.position.x - startX) > moveRange)
        {
            direction *= -1;
        }

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

        GameObject fb1 = Instantiate(fireballPrefab, spawnPos, Quaternion.identity);
        fb1.GetComponent<Fireball>().Initialize(fireballSpeed, fireballDirection, 
        fireballScale, fireballDamage, Fireball.FireballPathType.Spiral, 0f);

        GameObject fb2 = Instantiate(fireballPrefab, spawnPos, Quaternion.identity);
        fb2.GetComponent<Fireball>().Initialize(fireballSpeed, fireballDirection, 
        fireballScale, fireballDamage, Fireball.FireballPathType.Spiral, Mathf.PI);
    }
}
