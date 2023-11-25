using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM_Manager : MonoBehaviour
{
    Dictionary<string, Istate> _states = new Dictionary<string, Istate>();
    Istate _actualState;
   public void CreateState(string name ,Istate state)
    {
        if (! _states.ContainsKey(name))
        {
            _states.Add(name, state);
        }
    }

   public void execute()
    {
         _actualState.onUpdate();
    }

  public  void ChangeState(string name)
    {
        if (_states.ContainsKey(name))
        {
            if (_actualState != null)
                _actualState.OnExit();
            
            _actualState = _states[name];
            _actualState.onEnter();


        }
    }
}
