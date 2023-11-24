using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Istate 
{

    void onEnter();

    void onUpdate();

    void OnExit();
}
