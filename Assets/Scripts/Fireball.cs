using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float speed = 200.0f;
    public Vector2 direction = Vector2.down;
    public Vector3 scale;
    public enum FireballPathType { Linear, Spiral, ZigZag, ArchLeft, ArchRight };
    public FireballPathType pathType;
    public float phaseOffset;

    private float angle;
    private float frequency = 2f;
    private float amplitude = 2f;

    public float horizontalSpeed = 50f; // Controls how far right it goes
    public float curvature = 50f; 

    public int damage = 0;

    private Camera mainCam;
    private Renderer rend;

    private float localTime = 0f;

    private Vector3 initialPosition;
    public float duration = 2f; // how long it flies
    public float horizontalAmplitude = 8f;

    void Start()
    {
        mainCam = Camera.main;
        rend = GetComponent<Renderer>();
        initialPosition = transform.position;
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
        localTime += Time.deltaTime;

        switch (pathType)
        {
            case FireballPathType.Linear:
                transform.Translate(direction * speed * Time.deltaTime);
                break;

            case FireballPathType.Spiral:
                float t = Time.time + phaseOffset;
                Vector3 offsetX = new Vector3(Mathf.Sin(t * frequency) * amplitude, 0f, 0f);
                Vector3 velocity = direction * speed * Time.deltaTime;
                transform.position += velocity + offsetX;
                break;

            case FireballPathType.ZigZag:
                float zigzag = Mathf.Sin((Time.time + phaseOffset) * frequency * 2) * amplitude * 1.5f;
                transform.position += (Vector3)(direction * speed * Time.deltaTime) + new Vector3(zigzag, 0f, 0f);
                break;

            case FireballPathType.ArchRight:
                float tRight = localTime + phaseOffset;

                float xRight = tRight * horizontalSpeed;                     // Linear horizontal movement
                float yRight = -Mathf.Pow(tRight, 2) * curvature;           // Parabolic vertical movement

                transform.position = initialPosition + new Vector3(xRight, yRight, 0f);
                break;

            case FireballPathType.ArchLeft:
                float tLeft = localTime + phaseOffset;

                float xLeft = -tLeft * horizontalSpeed;                    
                float yLeft = -Mathf.Pow(tLeft, 2) * curvature;            

                transform.position = initialPosition + new Vector3(xLeft, yLeft, 0f);
                break;
        }

        if (!IsVisibleFrom(rend, mainCam))
        {
            Destroy(gameObject);
        }
    }

    public void Initialize(
        float newSpeed, Vector2 newDirection, Vector3 newScale, int newDamage, 
        FireballPathType pathType, float phaseOffset = 0f
    )
    {
        this.speed = newSpeed;
        this.direction = newDirection.normalized;
        this.scale = newScale;
        this.pathType = pathType;
        this.phaseOffset = phaseOffset;
        this.damage = newDamage;

        this.transform.localScale = scale;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Fireball hit: " + other.name + " with tag: " + other.tag);
        if (other.CompareTag("Fireball") || other.CompareTag("Vampire"))
            return;

        if (other.CompareTag("Soldier"))
        {
            Soldier.playerHealth -= this.damage;
            Destroy(gameObject);
        }
    }

    bool IsVisibleFrom(Renderer renderer, Camera camera)
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
        return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
    }
}
