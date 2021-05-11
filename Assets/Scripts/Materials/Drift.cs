using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Drift : MonoBehaviour
{
    public Vector2 direction;
    public Quaternion rotationFrom;
    public Quaternion rotationTo;
    public float speed = 1f;
    public Camera Camera;
    
    private Vector2 _endPoint;
    private Vector2 _origin;

    // Start is called before the first frame update
    void Start()
    {
         _endPoint = new Vector2(Random.Range(Camera.ViewportToWorldPoint(new Vector3(0,0)).x, Camera.ViewportToWorldPoint(new Vector3(1,0)).x), Camera.ViewportToWorldPoint(new Vector3(0,0)).y - 1);
         _origin = new Vector2(this.transform.position.x,this.transform.position.y);

        // TODO: The top code must be replaced. Need to define game "space" so that we can ascertain endpoints such as: Top and bottom playing field
        // This is so that we can initialize better spawn origins and the track for which the materials drifting ends

        direction = _endPoint - _origin;
        direction.Normalize();

        rotationFrom = Quaternion.identity;
        rotationTo = Quaternion.Euler(0f,0f,Random.Range(-20f,20f));
    }

    // Update is called once per frame
    void Update()
    {
        drifitng();
        rotating();
    }

    void drifitng() => this.GetComponent<Rigidbody2D>().MovePosition(this.GetComponent<Rigidbody2D>().position + direction * speed * Time.deltaTime);

    void rotating() => this.transform.rotation = Quaternion.Slerp(rotationFrom, rotationTo,5f);
}