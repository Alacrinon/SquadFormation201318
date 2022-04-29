using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfinderScript : MonoBehaviour
{
    public Transform formation;
    FormationScript formationscript;
    SquadScript squadScript;
    
    public float speed = 5;
    public float turnSpeed = 90;
    public bool atLocation, facingTarget = true;
    public int ChosenNav;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!atLocation | !facingTarget)
        {
            Pathfind();
        }
    }

    private Vector3 FindNav()
    {
        formationscript = FindObjectOfType<FormationScript>();
        formation = formationscript.transform;
        Vector3 targetNav = formation.GetChild(ChosenNav).position;
        return targetNav;
    }

    private bool FaceTarget(Vector3 LookTarget)
    {
        LookTarget.y = 1;
        facingTarget = false;
        bool localfacingTarget = false;
        Vector3 dirToLookTarget = (LookTarget - transform.position).normalized;
        float TargetAngle = 90 - Mathf.Atan2(dirToLookTarget.z, dirToLookTarget.x) * Mathf.Rad2Deg;

        if (Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.y, TargetAngle)) > 0.05f && transform.position != LookTarget) //if not facing AND not at location
        {
            float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.y, TargetAngle, turnSpeed * Time.deltaTime); //rotate to angle
            transform.eulerAngles = Vector3.up * angle;
        }else
        {
            localfacingTarget = true;
        }
        return localfacingTarget;
    }

    private void MoveToTarget(Vector3 MoveTarget)
    {
        MoveTarget.y = 1;
        float formationAngle = formation.eulerAngles.y;
        
        if (transform.position == MoveTarget)
        {
            atLocation = true;
            if ((Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.y, formationAngle)) > 0.05f))
            {
                float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.y, formationAngle, turnSpeed * Time.deltaTime);
                transform.eulerAngles = Vector3.up * angle;
            }
            else
            {
                facingTarget = true;
            }

        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, MoveTarget, speed * Time.deltaTime);
        }
    }

    public void Pathfind()
    {
        Vector3 target = FindNav();
        if (FaceTarget(target))
        {
            MoveToTarget(target);
        }
        
    }

    
}
