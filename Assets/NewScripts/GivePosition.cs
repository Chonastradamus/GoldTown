using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GivePosition : MonoBehaviour
{

    public delegate void DetectionEvent(Vector3 posi);
    public event DetectionEvent Detection;

   public void LocationPlayer(Vector3 pos)
   {
       Debug.Log("dando info");
       Detection(pos);
   }

}
