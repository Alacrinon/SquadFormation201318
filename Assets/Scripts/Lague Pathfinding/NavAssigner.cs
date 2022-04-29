using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavAssigner : MonoBehaviour
{
    int IDnumber = 0;
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
