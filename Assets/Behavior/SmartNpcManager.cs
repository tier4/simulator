using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartNpcManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        List<GameObject> smartpedestrians = new List<GameObject>();
        foreach (Transform child in transform)
        {
           if ( child.gameObject.name.StartsWith("Pedestrians") )
            {
                var pedestrans = child;
                //
                foreach( Transform sps in pedestrans.transform)
                {
                    smartpedestrians.Add(sps.gameObject);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}