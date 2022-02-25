using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Geam : MonoBehaviour
{
    //[SerializeField] private string GeamName;
    //public int GeamValue;

    //[SerializeField] private int GeamAmount;

    //[SerializeField] private int collidedGeamValue;

    /**void Start()
    {
        GeamName = name.Substring(0, 4);
        switch (GeamName)
        {
            case "Gems_0":
                GeamValue = 1;
                break;
            case "Gems_1":
                GeamValue = 5;
                break;
            case "Gems_2":
                GeamValue = 10;
                break;
            case "Gems_3":
                GeamValue = 15;
                break;
            case "Gems_4":
                GeamValue = 20;
                break;

        }
     }
    */

    void OnTriggerEnter2D(Collider2D col)
    {
        ScoreTextGeam.GeamAmount += 1;
        Destroy(gameObject);
    }

   /**private void OnTriggerEnter2D(Collider2D collision)
    {
        collidedGeamValue = collision.gameObject.GetComponent<Geam>().GeamValue;
        GeamAmount += collidedGeamValue;
        Destroy(collision.gameObject);
    }
   */

}
