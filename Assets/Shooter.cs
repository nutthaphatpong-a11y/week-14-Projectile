using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("References")]
    public Transform shootPoint;     // จุดยิง
    public GameObject bulletPrefab;  // Prefab กระสุน
    public GameObject target;         // เป้าหมาย

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        { 
         Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * 5f, Color.red, 5f);

            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

            if(hit.collider != null)
            {
                target.transform.position = new Vector2(hit.point.x, hit.point.y);
                Debug.Log("hit " + hit.collider.name);

                Vector2 projectiveVelocity = CalculateVelocity(shootPoint.position, hit.point, 1f);

                GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);
                Rigidbody2D shootBullet = bullet.GetComponent<Rigidbody2D>();

                shootBullet.linearVelocity = projectiveVelocity;
                
            }
        }
    }

    Vector2 CalculateVelocity(Vector2 origin, Vector2 target, float time)
    {
        Vector2 distance = target - origin;

        float velocityx = distance.x / time;
        float velocityy = distance.y / time;

        Vector2 projectiveVelocity = new Vector2(velocityx, velocityy);

        return projectiveVelocity;
    }

}
