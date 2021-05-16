using System.Linq;
using UnityEngine;

public class Platform : MonoBehaviour, IDamageable
{
    [SerializeField]
    private EdgeCollider2D[] _walls = new EdgeCollider2D[4];

    public void TakeDamage()
    {
        print(gameObject.name + " takes damage");
    }

    /*--------------------------------------------------------------------------------------------- 
    * Cast a ray in all four directions (right, up, left, down) and check if there is a platform
    * adjacent to this one.
    */
    void CheckAdjacent()
    {
        float yOffset  = 1.755f - 0.9225f; // Calculated from the edge collider height
        Vector2 origin = (Vector2)transform.position + new Vector2(0, yOffset);
        float rotation = 90;
        float xLen     = 1.5f;
        float yLen     = 1.25f;
        for (int i = 0; i < 4; i++)
        {
            float angle = i * rotation * Mathf.Deg2Rad;
            Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
            float length = xLen * Mathf.Abs(direction.x) + yLen * Mathf.Abs(direction.y);
            RaycastHit2D[] hits = Physics2D.RaycastAll(origin, direction, length, LayerMask.GetMask("Platform"))
                                           .Where(hit => hit.collider.gameObject != gameObject).ToArray();
            // Debug.DrawLine(origin, origin + new Vector2(xLen * direction.x, yLen * direction.y), Color.red);
            
            if (hits.Length == 0)     _walls[i].enabled = true;
            foreach (var hit in hits) _walls[i].enabled = false;
        }
    }

    void Update()
    {
        CheckAdjacent();
    }
}
