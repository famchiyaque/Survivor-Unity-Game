using UnityEngine;

public class Vampire1 : MonoBehaviour
{
    float scaleX = 250.0f;
    float scaleY = 250.0f;
    float scaleZ = 1.0f;

    public float moveSpeed = 50f;
    public float moveRange = 200f;

    private float startX;
    private int direction = 1;

    public GameObject fireballPrefab;
    public float fireballSpeed = 300f;
    public Vector2 fireballDirection = Vector2.down;
    public Vector3 fireballScale = new Vector3(60f, 60f, 1f);
    public float shootInterval = 0.8f;
    public int fireballDamage = 20;

    private float shootTimer;

    void Start()
    {
        transform.localScale = new Vector3(scaleX, scaleY, scaleZ);
        // Vector3 topOfScreen = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 1.05f, 0));
        transform.position = new Vector3(275, 500, 0);
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
        GameObject fb = Instantiate(fireballPrefab, spawnPos, Quaternion.identity);
        fb.GetComponent<Fireball>().Initialize(fireballSpeed, fireballDirection, fireballScale,
        fireballDamage, Fireball.FireballPathType.Linear);
    }
    
}
