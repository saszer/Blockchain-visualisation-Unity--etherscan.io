
//sahaj garg www.embracingearth.space

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class IrunHeyAPI : MonoBehaviour
{
    //References
    public HeyAPI heyAPI;
    public BlockDataHolderB blockHolderPrefab;

    //Data container where the key is the Block Number and the value is the active GameObject with matching block number.
    Dictionary<int, BlockDataHolderB> _content = new Dictionary<int, BlockDataHolderB>();

    //Timers
    public float delayBeforeStart = 2;
    public float refreshRate = 15;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(delayBeforeStart);
        while (true) {
            yield return RefreshData();
            yield return new WaitForSeconds(refreshRate);
        }
    }

    IEnumerator RefreshData()
    {
        Block_DataModel[] data = null;
        StartCoroutine(heyAPI.CallAPIProcess(outcome => data = outcome));

        //Wait until data has landed before proceeding.
        while (data == null)
            yield return null;

        //List<BlockDataHolderB> newBlocks = new List<BlockDataHolderB>();

        for (int i = 0; i < data.Length; i++)
        {
            var tmpData = new Block_ViewModel(data[i]);

            if (!_content.ContainsKey(tmpData.blockNumber))
            {
                BlockDataHolderB holder = Instantiate(blockHolderPrefab, transform);
                //We can pass in the index of an object along with it.
                holder.Setup(tmpData, _content.Count);
                _content.Add(tmpData.blockNumber, holder);
                //newBlocks.Add(holder);
                Debug.Log($"Added block of id {tmpData.blockNumber}");
            }
        }

        //You can store and cycle through only new blocks if you want
        //for (int i = 0; i < newBlocks.Count; i++)
        //{
        //    //The current index will be equals to the content dictionary count - new blocks count.
        //    Debug.Log($"Block of id {newBlocks[i].data.blockNumber} is at dictionary index {_content.Count - newBlocks.Count + i}.");
        //}

        BlockDataHolderB lastItem = transform.GetChild(transform.childCount - 1).GetComponent<BlockDataHolderB>();
        //To access an object in the dictionary, you can use a blockNumber -> 
        Debug.Log($"{_content[lastItem.data.blockNumber]}");

        //Below is how to handle the entire data as a whole, by looping through those.
        //Or you can loop through those using foreach
        //foreach (var kvp in _content)
        //    Debug.Log($"Key: {kvp.Key}, Value: {kvp.Value.data.input}");

        //foreach(BlockDataHolderB holder in _content.Values)
        //     Debug.Log($"Value: {holder.data.input}");

        //Check content based on its index
        //int index = 0;
        //foreach (BlockDataHolderB holder in _content.Values)
        //    Debug.Log($"Item {holder.data.blockNumber} is at index {index++} out of {_content.Count}.");

        Debug.Log("~~~~~~~~~~~~~~~~~~~~~~~");

        //Get the first item
        //var holder = _content.First();

        //Get the first item with --
        //var holder = _content.First(o => int.Parse(o.Value.data.gasUsed) > 300);



    }

    //void GetBlockList(EtherScanAPIReply_Model.)

    // Update is called once per frame
    void Update()
    {
        
    }
}
