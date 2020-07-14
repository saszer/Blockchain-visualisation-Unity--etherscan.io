using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
//sahaj garg www.embracingearth.space
public class HeyAPI : MonoBehaviour
{ 
    public string WEB_URL = "http://api.etherscan.io/api?module=account&action=txlist&address=0xde0b295669a9fd93d5f28d9ec85e40f4cb697bae&startblock=0&endblock=99999999&sort=asc&apikey=YourApiKeyToken";
    public float delayBetweenEachFailedAttempt = 5.1f;
    
    // Calling the API and Getting response 
    public IEnumerator CallAPIProcess(Action<Block_DataModel[]> outcome)
    {
        UnityWebRequest rq = UnityWebRequest.Get(WEB_URL);
        {
            yield return rq.SendWebRequest();

            // rq.downloadHandler.text;
            //  Debug.Log(rq.downloadHandler.text);
            string jsonResult = System.Text.Encoding.UTF8.GetString(rq.downloadHandler.data);

            EtherScanAPIReply_Model data = JsonUtility.FromJson<EtherScanAPIReply_Model>(jsonResult);

            if (data == null) { 
                Debug.LogError($"Null data. Response code: {rq.responseCode}.");
                yield return new WaitForSeconds(delayBetweenEachFailedAttempt);
                StartCoroutine(CallAPIProcess(outcome));
                yield break;
            }

            outcome?.Invoke(data.result);
        }  
    }

}