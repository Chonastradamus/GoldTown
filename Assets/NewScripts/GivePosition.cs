using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GivePosition : MonoBehaviour
{

    public delegate bool DetectionEvent(Transform posi);
    public DetectionEvent Detection;

   public void LocationPlayer(Transform pos)
   {
       Debug.Log("dando info");
       Detection(pos);
   }

}
