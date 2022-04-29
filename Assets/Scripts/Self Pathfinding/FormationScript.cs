using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormationScript : MonoBehaviour
{

    public List<GameObject> navPoints = new List<GameObject>();
    public GameObject navPrefab;
    public Transform formationTransform;
    public int navPointNumber = 6;

    Vector3 mPrevPos = Vector3.zero;
    Vector3 mPosDelta = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHit))
            {
                transform.position = raycastHit.point;
                CreateFormation(raycastHit.point);
                
            }

        }
        if (Input.GetMouseButton(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHit))
            {

            }
            Vector3 dirToLookTarget = (transform.position - raycastHit.point).normalized;
            float TargetAngle = 90 - Mathf.Atan2(dirToLookTarget.z, dirToLookTarget.x) * Mathf.Rad2Deg;
           
            

            if (Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.y, TargetAngle)) > 0.05f ) 
            {
                float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.y, TargetAngle, 360 * Time.deltaTime); //rotate to angle
                transform.eulerAngles = Vector3.up * angle;
            }
            //    mPosDelta = Input.mousePosition - mPrevPos;
            //transform.Rotate(transform.up, -Vector3.Dot(mPosDelta, Camera.main.transform.right), Space.World);

        }
    }

    public void CreateFormation(Vector3 inputVector)
    {

        if (navPoints.Count == 0)
        {
            for (float i = 0; i < navPointNumber; i++)
            {
                if (i % 2 != 0)
                {
                    GameObject newNav = Instantiate(
                    navPrefab,
                    new Vector3(0 + (i * -1), 1, 0 + (i * -1)),
                    Quaternion.Euler(Vector3.zero),
                    transform
                    );
                    newNav.name = "Agent " + i;
                    navPoints.Add(newNav);
                }
                else
                {
                    GameObject newNav = Instantiate(
                    navPrefab,
                    new Vector3(0 + (i), 1, 0 + ((0 * -1))),
                    Quaternion.Euler(Vector3.zero),
                    transform
                    );
                    newNav.name = "Agent " + i;
                    navPoints.Add(newNav);
                }

            }
        }
        else
        {
            GameObject[] navPointobjects; //Something causes the last set of navpoints to not be removed from the list when new agents spawn
            navPointobjects = FindObjectsOfType<GameObject>();
            for (int i = 0; i < formationTransform.childCount; i++)
            {
                Object.Destroy(formationTransform.GetChild(i).gameObject);
                navPoints.Remove(navPointobjects[i]);
                
                
            }

            for (float i = 0; i < navPointNumber; i++)
            {
                if (i % 2 != 0)
                {
                    GameObject newNav = Instantiate(
                    navPrefab,
                    new Vector3(inputVector.x + (i * -1), 1, inputVector.z + (i * -1)),
                    Quaternion.Euler(Vector3.zero),
                    transform
                    );
                    newNav.name = "Agent " + i;
                    navPoints.Add(newNav);
                }
                else
                {
                    GameObject newNav = Instantiate(
                    navPrefab,
                    new Vector3(inputVector.x + (i), 1, 0 + ((inputVector.z))),
                    Quaternion.Euler(Vector3.zero),
                    transform
                    );
                    newNav.name = "Agent " + i;
                    navPoints.Add(newNav);
                }

            }


        }
        


    }

    private void OnDrawGizmos()
    {

        formationTransform = transform;
        if (formationTransform.childCount != 0)
        {
            Vector3 startPosition = formationTransform.GetChild(0).position;
            Vector3 previousPosition = startPosition;
            Gizmos.DrawRay(startPosition, formationTransform.forward * 10);
            foreach (Transform Navpoint in formationTransform)
            {
                Gizmos.DrawSphere(Navpoint.position, .3f);
                previousPosition = Navpoint.position;
            }
        }



    }
    //public void RequestAvailability()
    //{

    //}
}
