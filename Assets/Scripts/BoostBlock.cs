using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostBlock : MonoBehaviour
{
    public block[] _block;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Block")
        {
            //Debug.Log("Nyentuh block");
            foreach (var _blocks in _block)
            {
                //_blocks.fallTime = 0.1f;
                FindObjectOfType<block>().ModifFallSpeed(0.25f);
                //Debug.Log("After Boost");
            }
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Block")
        {
            //Debug.Log("Nyentuh block");
            foreach (var _blocks in _block)
            {
                //_blocks.fallTime = 0.1f;
                FindObjectOfType<block>().ModifFallSpeed(0.25f);
                //Debug.Log("After Boost");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Block")
        {
            //Debug.Log("Exit Collider");
            foreach (var _blocks in _block)
            {
                //_blocks.fallTime = 1;
                if (_blocks!= null)
                {
                    FindObjectOfType<block>().ModifFallSpeed(1f);
                    //Debug.Log("After Boost");
                }                           
            }
        }
    }

    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "Block")
    //    {
    //        Debug.Log("Nyentuh block");
    //        // Cek apakah GameObject memiliki komponen block
    //        block blockObject = collision.gameObject.GetComponent<block>();
    //        if (blockObject != null)
    //        {
    //            // Ubah nilai fallTime
    //            blockObject.fallTime = 0.1f;
    //            Debug.Log("After Boost");
    //        }
    //    }
    //}
}
