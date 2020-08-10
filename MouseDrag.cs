using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDrag : MonoBehaviour
{
   public bool isMouseDragging;
    private GameObject target;
    private Vector3 screenPosition;
    private Vector3 offset;
   public LayerMask maskDrag;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    GameObject ReturnClickedObject(out RaycastHit hit)
    {
        GameObject targetObject = null;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray.origin, ray.direction * 10, out hit, Mathf.Infinity, maskDrag))
        {
            targetObject = hit.collider.gameObject;
        }
        return targetObject;
    }
    // Update is called once per frame
    void Update()
    {
        
            if (Input.GetMouseButtonDown(0))
            {
             
                RaycastHit hitInfo;
                target = ReturnClickedObject(out hitInfo);
               
                if (target != null)
                {
               
                    isMouseDragging = true;
                    Debug.Log("our target position :" + target.transform.position);
                    //Here we Convert world position to screen position.
                    screenPosition = Camera.main.WorldToScreenPoint(target.transform.position);
                    offset = target.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPosition.z));
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                
                    isMouseDragging = false;
                
            }

            if (isMouseDragging)
            {
                //tracking mouse position.
                Vector3 currentScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPosition.z);

                //convert screen position to world position with offset changes.
                Vector3 currentPosition = Camera.main.ScreenToWorldPoint(currentScreenSpace) + offset;

                //It will update target gameobject's current postion.
                target.transform.position = currentPosition;
            }

        
    }
}
