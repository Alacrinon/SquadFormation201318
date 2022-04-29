using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavPointScript : MonoBehaviour
{
    NavAssigner navAssigner;
    public int assignedID;
    // Start is called before the first frame update
    void Start()
    {
        navAssigner = FindObjectOfType<NavAssigner>();
        //assignedID = navAssigner.AssignID();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
