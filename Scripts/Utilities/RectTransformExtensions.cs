using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class RectTransformExtensions {
    public static bool Overlaps(this RectTransform a, RectTransform b) {
        return a.WorldRect().Overlaps(b.WorldRect());
    }
    public static bool Overlaps(this RectTransform a, RectTransform b, bool allowInverse) {
        return a.WorldRect().Overlaps(b.WorldRect(), allowInverse);
    }

    public static Rect WorldRect(this RectTransform rectTransform) {
        Vector2 sizeDelta = rectTransform.sizeDelta;
        float rectTransformWidth = sizeDelta.x * rectTransform.lossyScale.x;
        float rectTransformHeight = sizeDelta.y * rectTransform.lossyScale.y;

        Vector3 position = rectTransform.position;
        return new Rect(position.x - rectTransformWidth / 2f, position.y - rectTransformHeight / 2f, rectTransformWidth, rectTransformHeight);
    }
    public static void SetTransformPanelAgain(this RectTransform _realPanel, RectTransform _holderPanel){
		float _currentRatio = GetRatioScale(_realPanel, _holderPanel);

		_realPanel.transform.localScale = Vector3.one * _currentRatio;
		_realPanel.position = _holderPanel.position;
	}

	public static float GetRatioScale(RectTransform _realPanel, RectTransform _holderPanel){
		Vector2 _ownSize = _realPanel.rect.size;
		Vector2 _sizeHolder = _holderPanel.rect.size;

		float _ratioW = _sizeHolder.x / _ownSize.x;
		float _ratioH = _sizeHolder.y / _ownSize.y;

		// Debug.Log(_ratioW + " - " + _ratioH);

		float _currentRatio = _ratioH;
		if(_ratioH > _ratioW){
			_currentRatio = _ratioW;
		}

		return _currentRatio;
	}
}
