using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIScript : MonoBehaviour
{
    SquadScript squadScript;
    FormationScript formationScript;
    bool spawned = false;
    private void Start()
    {
         
    }
    private void OnGUI()
    {
        formationScript = FindObjectOfType<FormationScript>();
        squadScript = FindObjectOfType<SquadScript>();
        if (formationScript.formationTransform.childCount != 0)
        {
            if (!spawned)
            {
                if (GUI.Button(new Rect(0, 0, 100, 50), "Spawn Squad"))
                {
                    
                    squadScript.CreateSquad();
                    spawned = true;
                }
            }
            else
            {
                if (GUI.Button(new Rect(0, 0, 100, 50), "Pathfind"))
                {
                    
                    squadScript.AssignToPoints();
                    squadScript.FormSquad();
                }
            }
        }
     

    }   
}
