using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGrid : MonoBehaviour
{
    private Grid grid;
    public Camera mainCamera;

    private void Start()
    {
        grid = new Grid(3, 3, 1f, new Vector3(20,0,0));
    }

    private void Update() {
        Vector3 mouseWorldPosition  = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;
        transform.position = mouseWorldPosition;

        if(Input.GetMouseButtonDown(0)) {
            grid.SetValue(this.transform.position, Random.Range(0,10));
        }
        if(Input.GetMouseButtonDown(1)) {
            print(grid.GetValue(this.transform.position));
        }
    }
}
