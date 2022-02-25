using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTextGeam : MonoBehaviour
{
    Text text;
    public static int GeamAmount;

    //[SerializeField] private Text GeamCounter;

    //[SerializeField] private int GeamAmount;

    //[SerializeField] private int collidedGeamValue;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        //GeamAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        text.text = GeamAmount.ToString();
        //GeamCounter.text = "GemsValus : " + GeamAmount;
    }
 
}
