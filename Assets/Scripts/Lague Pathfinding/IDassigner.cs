using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IDassigner : MonoBehaviour
{
    int IDnumber = -1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int AssignID()
    {
        IDnumber += 1;
        return IDnumber;
    }
}
