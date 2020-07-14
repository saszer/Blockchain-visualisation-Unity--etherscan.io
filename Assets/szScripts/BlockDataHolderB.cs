using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDataHolderB : MonoBehaviour
{   //sahaj garg www.embracingearth.space
    //Reads from block_viewmodel.cs and to do anything with any block properties here! 
    //Attach inside prefab of each block. 

    public Block_ViewModel data;
    public int blockIndex; 

    //Your new start, you can do whatever you want from there.
    public void Setup(Block_ViewModel newData, int newBlockIndex)
    {
        data = newData;
        blockIndex = newBlockIndex;

        gameObject.name = data.blockNumber.ToString();

        transform.position = Vector3.one * newBlockIndex;

        //To access any value of this block.
        Debug.Log(data.blockNumber);
       
    }
}
