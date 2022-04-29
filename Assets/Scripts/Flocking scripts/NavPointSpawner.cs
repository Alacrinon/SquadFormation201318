using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavPointSpawner : MonoBehaviour
{
    public List<GameObject> navPoints = new List<GameObject>();
    public GameObject navPrefab;
    public Transform formationTransform;
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
    }

    public void CreateFormation(Vector3 inputVector)
    {

        if (navPoints.Count == 0)
        {
                    GameObject newNav = Instantiate(
                    navPrefab,
                    new Vector3(inputVector.x, 1, inputVector.z),
                    Quaternion.Euler(Vector3.zero),
                    transform
                    );
                    newNav.name = "Nav ";
                    navPoints.Add(newNav);
               
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

            GameObject newNav = Instantiate(
                     navPrefab,
                     new Vector3(inputVector.x, 1, inputVector.z),
                     Quaternion.Euler(Vector3.zero),
                     transform
                     );
            newNav.name = "Nav ";
            navPoints.Add(newNav);



        }



    }
}
