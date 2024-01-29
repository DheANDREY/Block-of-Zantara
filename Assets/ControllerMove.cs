using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerMove : MonoBehaviour
{
    public void MoveRight()
    {
        Debug.Log("MoveRight");
        block.instance.MoveRight2();
    }
    public void MoveLeft()
    {
        Debug.Log("MoveLeft");
        block.instance.MoveLeft2();
    }
    public void MoveUp()
    {
        Debug.Log("MoveUp");
        block.instance.MoveUp2();
    }
    public void MoveDown()
    {
        Debug.Log("MoveDown");
        block.instance.MoveDown2();
    }

}
