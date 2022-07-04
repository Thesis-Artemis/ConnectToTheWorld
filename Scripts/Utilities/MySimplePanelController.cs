using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MySimplePanelController : MonoBehaviour {
    public System.Action onPressBack;
    
    public virtual void ResetData (){}
	public virtual void RefreshData (){}
    public virtual void InitData(){}
    public virtual void Show (bool _updateNow = true, System.Action _onFinished = null){}
	public virtual void Hide (bool _updateNow = true, System.Action _onFinished = null){}
}
