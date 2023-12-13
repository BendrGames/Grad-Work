using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    void OnEnterState();
    void OnExitState();
    void UpdateState();
    
    void ContinueButtonClocked();
}
