using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public Grid grid;
    public int height;
    public int width;
    // Start is called before the first frame update
    void Awake()
    {
        grid = new Grid(width, height);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //private void OnDrawGizmosSelected()
    //{
    //    Gizmos.DrawCube(transform.position, new Vector3(width, height, 0));
    //}
}
