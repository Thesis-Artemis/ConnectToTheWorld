using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyArrowFocusController : MonoBehaviour {
	
	enum StyleMove{
		MoveUp, MoveDown, MoveLeft, MoveRight
	}
	[SerializeField] StyleMove styleMove;

	public enum State{
		Hide, Show
	}
	public State myState{get;set;}

	[SerializeField] Image myImage;
	[SerializeField] float deltaMove;
	[SerializeField] float timeMove;

	int idTweenScaleXGameObj = -1;
	int idTweenScaleYGameObj = -1;
	int idTweenAlphaImg = -1;
	int idTweenMoveLocalGameObj = -1;

	Vector3 localPositionSaved;

	private void Awake() {
		localPositionSaved = transform.localPosition;

		myState = State.Hide;
		Color _c = myImage.color;
		_c.a = 0f;
		myImage.color = _c;
	}

	public void Show(){
		if(myState == State.Show){
			return;
		}
		if(idTweenScaleXGameObj != -1 && LeanTween.descr(idTweenScaleXGameObj) != null){
			LeanTween.cancel(idTweenScaleXGameObj);
		}
		if(idTweenScaleYGameObj != -1 && LeanTween.descr(idTweenScaleYGameObj) != null){
			LeanTween.cancel(idTweenScaleYGameObj);
		}
		if(idTweenAlphaImg != -1 && LeanTween.descr(idTweenAlphaImg) != null){
			LeanTween.cancel(idTweenAlphaImg);
		}
		if(idTweenMoveLocalGameObj != -1 && LeanTween.descr(idTweenMoveLocalGameObj) != null){
			LeanTween.cancel(idTweenMoveLocalGameObj);
		}

		myState = State.Show;

		transform.localPosition = localPositionSaved;
		transform.localScale = Vector3.one;

		idTweenAlphaImg = LeanTween.alpha(myImage.rectTransform, 1f, 0.1f).setOnComplete(()=>{
			idTweenAlphaImg = -1;
		}).id;
		idTweenScaleXGameObj = LeanTween.scaleX(gameObject, 0.8f, timeMove).setEase(LeanTweenType.easeInOutSine).setLoopPingPong(-1).id;
		idTweenScaleXGameObj = LeanTween.scaleY(gameObject, 1.2f, timeMove).setEase(LeanTweenType.easeInOutSine).setLoopPingPong(-1).id;
		
		switch(styleMove){
		case StyleMove.MoveDown:
			idTweenMoveLocalGameObj = LeanTween.moveLocalY(gameObject, localPositionSaved.y - deltaMove, timeMove).setEase(LeanTweenType.easeInOutSine).setLoopPingPong(-1).id;
			break;
		case StyleMove.MoveUp:
			idTweenMoveLocalGameObj = LeanTween.moveLocalY(gameObject, localPositionSaved.y + deltaMove, timeMove).setEase(LeanTweenType.easeInOutSine).setLoopPingPong(-1).id;
			break;
		case StyleMove.MoveLeft:
			idTweenMoveLocalGameObj = LeanTween.moveLocalX(gameObject, localPositionSaved.x - deltaMove, timeMove).setEase(LeanTweenType.easeInOutSine).setLoopPingPong(-1).id;
			break;
		case StyleMove.MoveRight:
			idTweenMoveLocalGameObj = LeanTween.moveLocalX(gameObject, localPositionSaved.x + deltaMove, timeMove).setEase(LeanTweenType.easeInOutSine).setLoopPingPong(-1).id;
			break;
		}
	}

	public void Hide(){
		if(myState == State.Hide){
			return;
		}
		if(idTweenScaleXGameObj != -1 && LeanTween.descr(idTweenScaleXGameObj) != null){
			LeanTween.cancel(idTweenScaleXGameObj);
		}
		idTweenScaleXGameObj = -1;
		if(idTweenScaleYGameObj != -1 && LeanTween.descr(idTweenScaleYGameObj) != null){
			LeanTween.cancel(idTweenScaleYGameObj);
		}
		idTweenScaleYGameObj = -1;
		if(idTweenAlphaImg != -1 && LeanTween.descr(idTweenAlphaImg) != null){
			LeanTween.cancel(idTweenAlphaImg);
		}
		idTweenAlphaImg = -1;
		if(idTweenMoveLocalGameObj != -1 && LeanTween.descr(idTweenMoveLocalGameObj) != null){
			LeanTween.cancel(idTweenMoveLocalGameObj);
		}
		idTweenMoveLocalGameObj = -1;

		myState = State.Hide;

		Color _c = myImage.color;
		_c.a = 0f;
		myImage.color = _c;

		transform.localPosition = localPositionSaved;
		transform.localScale = Vector3.one;
	}
}
