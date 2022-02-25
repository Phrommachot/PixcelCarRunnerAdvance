using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCamera : MonoBehaviour
{

    [SerializeField] private Transform background;
    [SerializeField] private float factor0 = 0.8f;

    [SerializeField] private Transform shadow;
    [SerializeField] private float factor1 = 0.5f;



    [SerializeField] private Transform Playermovent;

    private float defaultCamera;
    private float nextCamera;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        defaultCamera = transform.position.x;
        transform.position = new Vector3(Playermovent.transform.position.x, transform.position.y, transform.position.z);
    }

    private void LateUpdate()
    {
        nextCamera = transform.position.x;
        background.position = new Vector3(background.position.x + (nextCamera - defaultCamera) * factor0, background.position.y, background.position.z);
        shadow.position     = new Vector3(shadow.position.x + (nextCamera - defaultCamera) * factor1, shadow.position.y, shadow.position.z);

    }
   
   
}
