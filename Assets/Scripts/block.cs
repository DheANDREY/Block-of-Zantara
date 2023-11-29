using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class block : MonoBehaviour
{
    public Vector3 rotationPoint;
    public float fallTime = 1f; // Boost jatuh blok
    private float previousTime = 0.3f;
    public static int height = 13;
    public static int width = 10;
    private static Transform[,] grid = new Transform[width, height];

    public static block instance;
    private void Awake()
    {
        instance = this;
    }

    private BossController boss;
    private void Start()
    {
        boss = GameObject.FindGameObjectWithTag("Boss").GetComponent<BossController>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-1, 0, 0);
            if (!ValidMove())
            {
                transform.position -= new Vector3(-1, 0, 0);
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += new Vector3(1, 0, 0); //Debug.Log("Current PosX :" + transform.position);
            if (!ValidMove())
            {

                transform.position -= new Vector3(1, 0, 0);
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);
            if (!ValidMove())
            {
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -90);
            }
        }
//===================== PRESS ONCE ========================================
        //if (Time.time - previousTime > (Input.GetKeyDown(KeyCode.DownArrow) ? fallTime / 10 : fallTime))
        //{
        //    transform.position += new Vector3(0, -1, 0);
        //    previousTime = Time.time;
        //    if (!ValidMove())
        //    {
        //        transform.position += new Vector3(0, 1, 0);
        //        AddToGrid();
        //        CheckForLine();
        //        this.enabled = false;
        //        FindObjectOfType<SpawnerBlock>().SpawnNewPrefab();
        //    }
        //}
// ----------------------------------------------------------------------------------------------------------------------------------
//===================== PRESS HOLD ========================================
        if (Time.time - previousTime > (Input.GetKey(KeyCode.DownArrow) ? fallTime / 10 : fallTime))
        {
            transform.position += new Vector3(0, -1, 0);
            previousTime = Time.time;
            if (!ValidMove())
            {
                transform.position += new Vector3(0, 1, 0);
                AddToGrid();
                CheckForLine();
                this.enabled = false;
                FindObjectOfType<SpawnerBlock>().SpawnNewPrefab();
            }
        }
 // ----------------------------------------------------------------------------------------------------------------------------------
    }
    void CheckForLine()
    {
        for(int i = height-1; i >= 0; i--)
        {
            if (HasLine(i))
            {
                DeleteLine(i);
                RowDown(i);
            }
        }
    }
    bool HasLine(int i)
    {
        for(int j = 0; j < width; j++)
        {
            if(grid[j,i] == null)
            {
                return false;
            }                                    
        }
        return true;
    }
    
    void DeleteLine(int i)
    {
        for(int j = 0; j<width; j++)
        {
            Destroy(grid[j, i].gameObject);            
            grid[j, i] = null;

            if (boss != null)
            {

                boss.TakeDamage(0.1f); // Gantilah damageAmount dengan jumlah kerusakan yang sesuai.
            }
        }       
    }

    void RowDown(int i)
    {
        for(int y = i; y < height; y++)
        {
            for(int j = 0; j < width; j++)
            {
                if(grid[j,y] != null)
                {
                    grid[j, y - 1] = grid[j, y];
                    grid[j, y] = null;
                    grid[j, y - 1].transform.position -= new Vector3(0, 1, 0);
                }
            }
        }
    }
    void AddToGrid()
    {
        foreach(Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);
            if (roundedX >= 0 && roundedX < 10 && roundedY >= 0 && roundedY < 12)
            {
                grid[roundedX, roundedY] = children;
            }
            else
            {
                MenuManager.instance.isPlayerLose = true;

            }
            //grid[roundedX, roundedY] = children;
        }
    }
    bool ValidMove()
    {
        foreach (Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);
            if (roundedX < 0 || roundedX >= width || roundedY < 0 || roundedY >= height)
            {
                //Debug.Log("Current PosX :" + transform.position);
                return false;
            }
            if(grid[roundedX, roundedY] != null)
            {
                return false;
            }
        }
        return true;
    }
}
