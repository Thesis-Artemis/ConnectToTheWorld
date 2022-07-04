using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Lean.Pool;
using UnityEngine.Networking;
using System;
using System.Globalization;
using System.Text.RegularExpressions;

[System.Serializable]
public class Circle
{
	public Vector2 point;
	public float radius;
    private float x;
    private float y;

    public Circle(float x, float y, float radius)
    {
        this.x = x;
        this.y = y;
        this.radius = radius;
    }
}

[System.Serializable]
public class RandomValue
{
	public float min, max;

	public RandomValue ()
	{
		min = 0f;
		max = 0f;
	}

	public RandomValue (float _min, float _max)
	{
		min = _min;
		max = _max;
	}
}

[System.Serializable]
public class Bound
{
	public float xLeft;
	public float xRight;
	public float yTop;
	public float yBottom;
}

public static class MyConstant{

	#region Connection Info
	public static string Socket_IP_DeBug = "123-debug.dzogame.vn";
	public static string Socket_IP_Test = "123-test.dzogame.vn";
	public static string Socket_IP_Real = "123-real.dzogame.vn";
	public static int TCPSocket_Port = 1514;
	public static int WebSocket_Port = 1524;
	#endregion
	
	#region Save Info
	public static string rootSaveName{
		get{
			#if UNITY_ANDROID || UNITY_IOS || UNITY_EDITOR
				return "Mobile_DzoPlay_DataInfo.dat";
			#elif UNITY_WEBGL
				return "Web_DzoPlay_DataInfo.dat";
			#else
				return "Win_DzoPlay_DataInfo.dat";
			#endif
		}
	} 
	// public const string rootSaveName = "PC_DzoPlay_DataInfo.dat";
	public static string save_kSfxName{
		get{
			#if UNITY_ANDROID || UNITY_IOS || UNITY_EDITOR
				return "Mobile_DzoPlay_Sfx";
			#elif UNITY_WEBGL
				return "Web_DzoPlay_Sfx";
			#else
				return "Win_DzoPlay_Sfx";
			#endif
		}
	}
	public static string save_kMusicName{
		get{
			#if UNITY_ANDROID || UNITY_IOS || UNITY_EDITOR
				return "Mobile_DzoPlay_Music";
			#elif UNITY_WEBGL
				return "Web_DzoPlay_Music";
			#else
				return "Win_DzoPlay_Music";
			#endif
		}
	}
	public static string save_kVibrationName{
		get{
			#if UNITY_ANDROID || UNITY_IOS || UNITY_EDITOR
				return "Mobile_DzoPlay_Vibration";
			#elif UNITY_WEBGL
				return "Web_DzoPlay_Vibration";
			#else
				return "Win_DzoPlay_Vibration";
			#endif
		}
	}
	public static string save_kVersionDataName{
		get{
			#if UNITY_ANDROID || UNITY_IOS || UNITY_EDITOR
				return "Mobile_DzoPlay_VersionData";
			#elif UNITY_WEBGL
				return "Web_DzoPlay_VersionData";
			#else
				return "Win_DzoPlay_VersionData";
			#endif
		}
	}
	#endregion

	public static long[] POW = new long[64] {1,2,4,8,16,32,64,128,256,512,1024,2048,4096,8192,16384,32768,65536,131072,262144,524288,1048576,2097152,4194304,8388608,16777216,33554432,67108864,134217728,268435456,536870912,1073741824,2147483648L,4294967296L,8589934592L,17179869184L,34359738368L,68719476736L,137438953472L,274877906944L,549755813888L,1099511627776L,2199023255552L,4398046511104L,8796093022208L,17592186044416L,35184372088832L,70368744177664L,140737488355328L,281474976710656L,562949953421312L,1125899906842624L,2251799813685248L,4503599627370496L,9007199254740992L,18014398509481984L,36028797018963968L,72057594037927936L,144115188075855872L,288230376151711744L,576460752303423488L,1152921504606846976L,2305843009213693952L,4611686018427387904L,-9223372036854775808L};

	public static sbyte[][] ShalowCopy(this sbyte[][] _originalMatrix){
		int _numberRow=_originalMatrix.Length;
		int _numberCol;
		sbyte[][] _dataClone = new sbyte[_numberRow][];
		for(int _row = 0; _row < _numberRow; _row ++){
			_numberCol=_originalMatrix[_row].Length;
			_dataClone[_row] = new sbyte[_numberCol];
			for(int _col = 0; _col < _numberCol; _col ++){
				_dataClone[_row][_col] = _originalMatrix[_row][_col];
			}
		}
		return _dataClone;
	}
	public static List<List<T>> ShalowCopy<T>(this List<List<T>> _originalMatrix){
		List<List<T>> _dataClone = new List<List<T>>();
		for(int _row = 0; _row < _originalMatrix.Count; _row ++){
			List<T> _tmpListInRow = new List<T>();
			for(int _col = 0; _col < _originalMatrix[_row].Count; _col ++){
				_tmpListInRow.Add(_originalMatrix[_row][_col]);
			}
			_dataClone.Add(_tmpListInRow);
		}
		return _dataClone;
	}
	public static List<T> ShalowCopy<T>(this List<T> _originalMatrix){
		List<T> _dataClone = new List<T>();
		for(int i = 0; i < _originalMatrix.Count; i ++){
			_dataClone.Add(_originalMatrix[i]);
		}
		return _dataClone;
	}
	public static List<List<T>> GetInitializedArray<T>(int _numRow, int _numCol, T _initialValue) {
		List<List<T>> _matrix = new List<List<T>>();
		for(int _row = 0; _row < _numRow; _row ++){
			List<T> _tmpListInRow = new List<T>();
			for(int _col = 0; _col < _numCol; _col ++){
				_tmpListInRow.Add(_initialValue);
			}
			_matrix.Add(_tmpListInRow);
		}
		return _matrix;
	}
	public static bool IsAvailableUserNameAndPass(this string _text){
		if (string.IsNullOrEmpty (_text)) {
			return false;
		}
		
		// string _textCensor = _text.ToLower();
		// for (int i = 0; i < CoreGameManager.instance.gameInfomation.userNameFilterInfo.listSpecialChars.Count; i++) {
		// 	_textCensor = _textCensor.Replace (CoreGameManager.instance.gameInfomation.userNameFilterInfo.listSpecialChars[i], "*");
		// }
		// if (_textCensor.Contains ("*")) {
		// 	return false;
		// }
		return true;
	}

	// public static bool IsAvailableEmail(this string _text){
	// 	if (!_text.Contains ("@")) {
	// 		return false;
	// 	}
	// 	return true;
	// }

	/// <summary>
	/// Xáo trộn vị trí trong mảng có độ dài length
	/// </summary>
	public static List<int> RandomViTri (int _length)
	{
		if (_length == 0) {
			return null;
		}
		List<int> A = new List<int> ();
		for (int i = 0; i < _length; i++) {
			A.Add(i);
		}

		List<int> B = new List<int> ();
		while (A.Count > 0) {
			int vitri = UnityEngine.Random.Range (0, A.Count);
			B.Add(A[vitri]);
			A.RemoveAt(vitri);
		}

		return B;
	}
	public static List<T> RandomList<T> (List<T> _originalList){ 
		List<T> A = new List<T> ();
		if (_originalList.Count == 0) {
			return A;
		}
		for (int i = 0; i < _originalList.Count; i++) {
			A.Add(_originalList[i]);
		}
		List<T> B = new List<T> ();
		while (A.Count > 0) {
			int vitri = UnityEngine.Random.Range (0, A.Count);
			B.Add(A [vitri]);
			A.RemoveAt(vitri);
		}
		return B;
	}
	public static int RandomListBaseOnWeight(List<int> _listWeight){
		if (_listWeight.Count == 0) {
			return -1;
		}
		int _index = -1;
        int _tmpTong = 0;
		for(int i = 0; i < _listWeight.Count; i ++){
			_tmpTong += _listWeight[i];
		}

		int _rdTyLe = UnityEngine.Random.Range(0, _tmpTong);
		for(int i = 0; i < _listWeight.Count; i ++){
			int _tmp = 0;
			for(int j = i; j >= 0; j --){
				_tmp += _listWeight[j];
			}
			if(_rdTyLe < _tmp){
				_index = i;
				break;
			}
		}
        return _index;
    }
	public static List<int> LayViTriRandomTrongMang (int _sovitri, int _lengthList)
	{
		if (_lengthList == 0) {
			return null;
		}
		List<int> A = new List<int> ();
		for (int i = 0; i < _lengthList; i++) {
			A.Add (i);
		}

		List<int> B = new List<int> ();
		int _tmp = 0;
		while (_tmp < _sovitri
			&& _tmp < _lengthList) {
			int vitri = UnityEngine.Random.Range (0, A.Count);
			B.Add (A [vitri]);
			A.RemoveAt (vitri);
			_tmp++;
		}

		return B;
	}

	public static Vector3 ColisionPoint (Vector3 sphere1, Vector3 sphere2, float sphere1radius)
	{
		Vector3 Direction = (sphere2 - sphere1).normalized;
		Vector3 ColisionPoint = sphere1 + (Direction * sphere1radius);
		return ColisionPoint;
	}

//	public static string convertMoneyToLocalLocale (double money)
//	{
//		//		CultureInfo elGR = CultureInfo.CreateSpecificCulture("el-GR");
//		CultureInfo elGR = CultureInfo.CurrentUICulture;
//		string moneyStr = string.Format (elGR, "{0:N0}", money);
//		return moneyStr;
//	}

	public static bool IsOverLap (Vector2 posA, Vector2 posB, Rect b)
	{
		float xoA = posA.x;
		float yoA = posA.y;
		float xoB = posB.x + b.x;
		float yoB = posB.y + b.y;

		return ((xoB > xoA ? xoB - xoA : xoA - xoB) <= ((b.width + 0.1f) / 2) && (yoB > yoA ? yoB
			- yoA
			: yoA - yoB) <= ((b.height + 0.1f) / 2));
	}

	public static bool IsOverLap (Vector3 posA, Rect a, Vector3 posB, Rect b)
	{
		float xoA = posA.x + a.x;
		float yoA = posA.y + a.y;
		float xoB = posB.x + b.x;
		float yoB = posB.y + b.y;

		return ((xoB > xoA ? xoB - xoA : xoA - xoB) <= ((b.width + a.width) / 2) && (yoB > yoA ? yoB
			- yoA
			: yoA - yoB) <= ((b.height + a.height) / 2));
	}

	public static bool IsOverLap (Circle a, Circle b)
	{
		var dx = a.point.x - b.point.x;
		var dy = a.point.y - b.point.y;

		return Mathf.Sqrt (dx * dx + dy * dy) <= a.radius + b.radius;
	}

	public static bool IsOverLap (Circle a, Rect b)
	{
		var px = a.point.x;
		var py = a.point.y;

		if (px < b.xMin)
			px = b.xMin;
		else if (px > b.xMax)
			px = b.xMax;

		if (py < b.yMin)
			py = b.yMin;
		else if (py > b.yMax)
			py = b.yMax;

		var dx = a.point.x - px;
		var dy = a.point.y - py;

		return (dx * dx + dy * dy) <= a.radius * a.radius;
	}

	public static bool isVector3Valid (Vector3 v)
	{
		if (float.IsNaN (v.x)
			|| float.IsNaN (v.y)
			|| float.IsNaN (v.z)) {
			return false;
		}
		return true;
	}

	public static T GetAssest<T> (string _path) where T : UnityEngine.Object
	{
		if (_path.Equals ("")) {
			return null;
		}
		return Resources.Load<T> (_path);
	}

	public static bool IsOutOfRange (Vector3 _point, Vector2 _range, Vector2 _size)
	{
		Vector3 _posCam = Vector3.zero;
		if (_point.x + _size.x / 2 < _posCam.x - _range.x / 2
			|| _point.x - _size.x / 2 > _posCam.x + _range.x / 2
			|| _point.y + _size.y / 2 < _posCam.y - _range.y / 2
			|| _point.y - _size.y / 2 > _posCam.y + _range.y / 2) {
			return true;
		}
		return false;
	}

	public static string ToFixed (this float number, uint decimals)
	{
		return number.ToString ("N" + decimals);
	}

	public static void TweenLookAt (this Transform _myTransform, Vector2 _des, float _rotSpeed)
	{
		Vector2 _dir = _des - (Vector2)_myTransform.position;
		float _zAngle = Mathf.Atan2 (_dir.y, _dir.x) * Mathf.Rad2Deg - 90;
		Quaternion _desiredRot = Quaternion.Euler (0, 0, _zAngle);
		_myTransform.rotation = Quaternion.RotateTowards (_myTransform.rotation, _desiredRot, _rotSpeed * Time.fixedDeltaTime);
	}
	public static void TweenLookAtNow (this Transform _myTransform, Vector2 _des)
	{
		Vector2 _dir = _des - (Vector2)_myTransform.position;
		float _zAngle = Mathf.Atan2 (_dir.y, _dir.x) * Mathf.Rad2Deg - 90;
		_myTransform.rotation = Quaternion.Euler (0, 0, _zAngle);
	}

	public static void TweenMoveAtObjBaseOnCompass (this Transform _myTransform, Transform _compass, float _speedMove)
	{
		Vector3 _pos = _myTransform.position;
		Vector3 _velocity = new Vector3 (0, _speedMove * Time.fixedDeltaTime, 0f); // di chuyển theo y
		_pos += (_compass.rotation * _velocity);
		_myTransform.position = _pos;
	}

	#region Compare 2 Vector
	public static bool V2Equal (Vector2 a, Vector2 b)
	{
		return Vector2.SqrMagnitude (a - b) < 0.0001f;
	}

	#endregion

	// private static System.DateTime dateTime1970 = System.DateTime.MinValue;
	// public static long currentTimeMilliseconds { 
	// 	get 
	// 	{ 
	// 		if(dateTime1970 == System.DateTime.MinValue){
	// 			dateTime1970 = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
	// 		}
	// 		return (long)(System.DateTime.UtcNow - dateTime1970).TotalMilliseconds; 
	// 	} 
	// }
	// public static long GetCurrentTimeMillisecondsAtTime(System.DateTime _dateTime) { 
	// 	long _value = (long)(_dateTime - new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc)).TotalMilliseconds; 
	// 	return _value;
	// }
	// public static System.DateTime ParseToUtcTime(long _currentMilliseconds){
	// 	System.DateTime _start = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
	// 	System.DateTime _time = _start.AddMilliseconds(_currentMilliseconds);
	// 	return _time;
	// }
	public static string FormatToCurrency(long _gold){
		if(_gold < 10){
			return _gold.ToString();
		}
		return string.Format("{0:0,0}", _gold);
	}
	public static string FormatToCurrency(long _gold, long _minGoldCheck){
		if(_gold < 10){
			return _gold.ToString();
		}
		if(_gold <= _minGoldCheck){
			return string.Format("{0:0,0}", _gold);
		}
		// Debug.Log(">>>> " + _gold);
        string _result = "";
        if(_gold<1000){
            _result = _gold+"";
        }else if (_gold < 1000000){
            _result = (_gold / 1000) + "";
            _gold = (_gold % 1000)/10;
            if (_gold > 0)
                if (_gold > 9)
					if (_gold % 10==0)
						_result = _result + "," + (_gold/10) + "K";
					else
                    	_result = _result + "," + _gold + "K";
                else
                    _result = _result + "," +  _gold + "K";
            else
                _result = _result + "K";
        }else if (_gold < 1000000000){
            _result = (_gold / 1000000) + "";
            _gold = (_gold % 1000000)/10000;
            if (_gold > 0)
                if (_gold > 9)
					if (_gold % 10==0)
						_result = _result + "," + (_gold/10) + "M";
					else
                    	_result = _result + "," + _gold + "M";
                else
                    _result = _result + "," + "0" + _gold + "M";
            else
                _result = _result + "M";
        }else if (_gold < 1000000000000){
            _result = (_gold / 1000000000) + "";
            _gold = (_gold % 1000000000)/10000000;
            if (_gold > 0)
                if (_gold > 9)
                    if (_gold % 10==0)
						_result = _result + "," + (_gold/10) + "B";
					else
                    	_result = _result + "," + _gold + "B";
                else
                    _result = _result + "," +  _gold + "B";
            else
                _result = _result + "B";
		}else if (_gold < 1000000000000000){
            _result = (_gold / 1000000000000) + "";
            _gold = (_gold % 1000000000000)/10000000000;
            if (_gold > 0)
                if (_gold > 9)
                    if (_gold % 10==0)
						_result = _result + "," + (_gold/10) + "T";
					else
                    	_result = _result + "," + _gold + "T";
                else
                    _result = _result + "," +  _gold + "T";
            else
                _result = _result + "T";
		}else{
            _result = (_gold / 1000000000000000) + "";
            _gold = (_gold % 1000000000000000)/10000000000000;
            if (_gold > 0)
                if (_gold > 9)
                    if (_gold % 10==0)
						_result = _result + "," + (_gold/10) + "Q";
					else
                    	_result = _result + "," + _gold + "Q";
                else
                    _result = _result + "," +  _gold + "Q";
            else
                _result = _result + "Q";
		}
        return _result;
    }

	public static long GetGoldPrefer(long _gold){
		long _result = 0;
		if(_gold<=0){
			return _result;
		}
		_result = 1;
		while(_result<=500000000){
			if(_result*2>_gold){
				return _result;
			}
			if(_result*5>_gold){
				return _result*2;
			}
			if(_result*10>_gold){
				return _result*5;
			}
			_result=_result*10;
		}
		_result = 500000000;
		return _result;
	}

	///<summary>
	/// Focus tới 1 phần tử trong scroll rect nằm dọc
	///</summary>
	public static void ScrollRectVerticalFocusCenterItem(this ScrollRect _scrollRect, Vector3 _posItem, float _delta, bool _updateNow = true, System.Action _onFinished = null){
		// if(_item == null){
		// 	_scrollRect.verticalNormalizedPosition = 0f;
		// 	return;
		// }
        float halfViewportHeight = _scrollRect.viewport.rect.height / 2;
        float contentHeight = _scrollRect.content.rect.height;

        float localStartPoint = -_scrollRect.content.pivot.y * contentHeight + halfViewportHeight;
        float localEndPoint = (1 - _scrollRect.content.pivot.y) * contentHeight - halfViewportHeight;
        Vector3 itemlocalPosition = _scrollRect.content.transform.InverseTransformPoint(_posItem);

        float normalizedPosition = Mathf.InverseLerp(localStartPoint, localEndPoint, itemlocalPosition.y);
		if(Mathf.Abs(_scrollRect.verticalNormalizedPosition - normalizedPosition) > _delta){
            if(_updateNow){
                _scrollRect.verticalNormalizedPosition = normalizedPosition;
                if(_onFinished != null){
                    _onFinished();
                }
                return;
            }else{
                LeanTween.value(_scrollRect.gameObject, _scrollRect.verticalNormalizedPosition, normalizedPosition, 0.1f)
                    .setOnUpdate((_value)=>{
                        _scrollRect.verticalNormalizedPosition = _value;
                    }).setEase(LeanTweenType.easeInSine)
                    .setOnComplete(()=>{
                        if(_onFinished != null){
                            _onFinished();
                        }
                    });
                return;
            }
        }

        if(_onFinished != null){
            _onFinished();
        }
    }

	///<summary>
	/// Focus tới 1 phần tử trong scroll rect nằm ngang
	///</summary>
	public static void ScrollRectHorizontalFocusCenterItem(this ScrollRect _scrollRect, Vector3 _posItem, float _delta, bool _updateNow = true, System.Action _onFinished = null){
		// if(_item == null){
		// 	_scrollRect.horizontalNormalizedPosition = 0f;
		// 	return;
		// }
        float halfViewportWidth = _scrollRect.viewport.rect.width / 2;
        float contentWidth = _scrollRect.content.rect.width;

        float localStartPoint = -_scrollRect.content.pivot.x * contentWidth + halfViewportWidth;
        float localEndPoint = (1 - _scrollRect.content.pivot.x) * contentWidth - halfViewportWidth;
        Vector3 itemlocalPosition = _scrollRect.content.transform.InverseTransformPoint(_posItem);

        float normalizedPosition = Mathf.InverseLerp(localStartPoint, localEndPoint, itemlocalPosition.x);
		if(Mathf.Abs(_scrollRect.horizontalNormalizedPosition - normalizedPosition) > _delta){
            if(_updateNow){
                _scrollRect.horizontalNormalizedPosition = normalizedPosition;
                if(_onFinished != null){
                    _onFinished();
                }
                return;
            }else{
                LeanTween.value(_scrollRect.gameObject, _scrollRect.horizontalNormalizedPosition, normalizedPosition, 0.1f)
                    .setOnUpdate((_value)=>{
                        _scrollRect.horizontalNormalizedPosition = _value;
                    }).setEase(LeanTweenType.easeInSine)
                    .setOnComplete(()=>{
                        if(_onFinished != null){
                            _onFinished();
                        }
                    });
                return;
            }
        }

        if(_onFinished != null){
            _onFinished();
        }
    }
	public static void SetCanScrollVertical(this ScrollRect _scrollRect){
		if(_scrollRect.content.rect.size.y > ((RectTransform) _scrollRect.transform).rect.size.y){
            _scrollRect.vertical = true;
        }else{
            _scrollRect.vertical = false;
        }
	}
	public static void SetCanScrollHorizontal(this ScrollRect _scrollRect){
		if(_scrollRect.content.rect.size.x > ((RectTransform) _scrollRect.transform).rect.size.x){
            _scrollRect.horizontal = true;
        }else{
            _scrollRect.horizontal = false;
        }
	}

	/// <summary>
	/// Returns a normalized direction vector pointing at target from origin
	/// </summary>
	public static Vector3 DirectionVector(Vector3 _origin, Vector3 _target) {
		return (_target - _origin).normalized;
	}

	public static string ConvertString(string _text, int _maxLength){
		string _result = _text;
		if(_result.Length > _maxLength){
			_result = _result.Substring(0, _maxLength) + "...";
		}
		return _result;
	}
	public static IEnumerator DownloadIcon(this MonoBehaviour _monoBehavior, string _url, System.Action<Texture2D> _onFinished) {
		UnityWebRequest www = UnityWebRequestTexture.GetTexture(_url);
        yield return www.SendWebRequest();
        if(www.isNetworkError || www.isHttpError) {
            Debug.LogError(www.error);
            if(_onFinished != null){
                _onFinished(null);
            }
        }else {
            Texture2D _myTexture = (Texture2D) ((DownloadHandlerTexture)www.downloadHandler).texture;
            if(_onFinished != null){
                _onFinished(_myTexture);
            }
        }
        www.Dispose();
	}

	public static bool IsAppInstalled(string bundleID){
		#if UNITY_EDITOR
		return true;
		#elif UNITY_ANDROID
		AndroidJavaClass up = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject ca = up.GetStatic<AndroidJavaObject>("currentActivity");
		AndroidJavaObject packageManager = ca.Call<AndroidJavaObject>("getPackageManager");
		Debug.Log(" ********LaunchOtherApp ");
		AndroidJavaObject launchIntent = null;
		//if the app is installed, no errors. Else, doesn't get past next line
		try{
			launchIntent = packageManager.Call<AndroidJavaObject>("getLaunchIntentForPackage",bundleID);
			//        
			//        ca.Call("startActivity",launchIntent);
		}catch(System.Exception ex){
			Debug.Log("exception"+ex.Message);
		}
		if(launchIntent == null)
			return false;
		return true;
		#else
		return false;
		#endif
	}

	public static IEnumerator TweenValue(long _fromValue, long _toValue, int _tick, System.Action<long> _onUpdate = null, System.Action<long> _onFinished = null) {
		double _virtualValue = _fromValue;
		long _realValue = _toValue;
		double _deltaValue = (_realValue - _virtualValue) / _tick;
		if(_deltaValue < 2&&_deltaValue>=0){
			if(_onFinished != null){
				_onFinished(_realValue);
			}
			yield break;
		}
			
		while(true){
			yield return Yielders.Get(0.05f);
            _virtualValue += _deltaValue;
			if(_onUpdate != null){
				_onUpdate((long) _virtualValue);
			}
            if(_deltaValue < 0){
                if(_virtualValue <= _realValue){
                    _virtualValue = _realValue;
                    break;
                }
            }else if(_deltaValue > 0){
                if(_virtualValue >= _realValue){
                    _virtualValue = _realValue;
                    break;
                }
            }else{
                _virtualValue = _realValue;
                break;
            }
		}
		if(_onFinished != null){
			_onFinished(_realValue);
		}
	}
	public static void ShowPanel(ref int _tweenId, CanvasGroup _canvasGroupPanel, float _alpha, bool _blocksRaycasts, bool _updateNow = true, System.Action _onFinished = null){
		if(_tweenId != -1 && LeanTween.descr(_tweenId) != null){
			LeanTween.cancel(_tweenId);
		}
		_tweenId = -1;
		if(_updateNow){
			_canvasGroupPanel.alpha = _alpha;
			_canvasGroupPanel.blocksRaycasts = _blocksRaycasts;
            if(_onFinished != null){
                _onFinished();
            }
		}else{
			_canvasGroupPanel.blocksRaycasts = _blocksRaycasts;
			_tweenId = LeanTween.alphaCanvas(_canvasGroupPanel, _alpha, 0.1f).setOnComplete(()=>{
				if(_onFinished != null){
					_onFinished();
				}
			}).id;
		}
	}
	public static IEnumerator ShowPanel(Image _img, float _alpha, float _timeAppear = 0.2f, float _timeDisappear = 0.2f, float _timeDuraion = 0.2f){
		bool _isFnished = false;
		LeanTween.alpha(_img.rectTransform, _alpha, _timeAppear).setOnComplete(() => {
			_isFnished = true;
		});
		yield return new WaitUntil(() => _isFnished);
		yield return Yielders.Get(_timeDuraion);

		_isFnished = false;
		LeanTween.alpha(_img.rectTransform, 0f, _timeDisappear).setOnComplete(() => {
			_isFnished = true;
		});
		yield return new WaitUntil(() => _isFnished);
	}
	public static T RandomEnumValue<T> () {
		var v = System.Enum.GetValues (typeof (T));
		return (T) v.GetValue (new System.Random ().Next(v.Length));
	}
	public static void DebugMatrix(this List<List<int>> _matrix){
		Debug.Log("<color=green>-------------------------------------------</color>");
        for(int _dong = 0; _dong < _matrix.Count; _dong ++){
            string _tmp = "";
            for(int _cot = 0; _cot < _matrix[_dong].Count; _cot ++){
                _tmp += "<color=green>" + string.Format("{0:00}", _matrix[_dong][_cot]) + "</color>";
                if(_cot + 1 < _matrix[_dong].Count){
                    _tmp += " ";
                }
            }
            Debug.Log(_tmp);
        }
        Debug.Log("<color=green>-------------------------------------------</color>");
	}
	public static void DebugMatrix(this List<List<sbyte>> _matrix){
		Debug.Log("<color=green>-------------------------------------------</color>");
        for(int _dong = 0; _dong < _matrix.Count; _dong ++){
            string _tmp = "";
            for(int _cot = 0; _cot < _matrix[_dong].Count; _cot ++){
				if(_matrix[_dong][_cot] < 0){
					_tmp += "<color=green>" + string.Format("{0}", _matrix[_dong][_cot]) + "</color>";
				}else{
					_tmp += "<color=green>" + string.Format("{0:00}", _matrix[_dong][_cot]) + "</color>";
				}
                if(_cot + 1 < _matrix[_dong].Count){
                    _tmp += " ";
                }
            }
            Debug.Log(_tmp);
        }
        Debug.Log("<color=green>-------------------------------------------</color>");
	}
	public static bool IsValidEmail(string email)
	{
		if (string.IsNullOrWhiteSpace(email))
			return false;

		try
		{
			// Normalize the domain
			email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
									RegexOptions.None, TimeSpan.FromMilliseconds(200));

			// Examines the domain part of the email and normalizes it.
			string DomainMapper(Match match)
			{
				// Use IdnMapping class to convert Unicode domain names.
				var idn = new IdnMapping();

				// Pull out and process domain name (throws ArgumentException on invalid)
				string domainName = idn.GetAscii(match.Groups[2].Value);

				return match.Groups[1].Value + domainName;
			}
		}
		catch (RegexMatchTimeoutException e)
		{
			return false;
		}
		catch (ArgumentException e)
		{
			return false;
		}

		try
		{
			return Regex.IsMatch(email,
				@"^[^@\s]+@[^@\s]+\.[^@\s]+$",
				RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
		}
		catch (RegexMatchTimeoutException)
		{
			return false;
		}
	}
	public static void clear(this InputField _inputfield){
        _inputfield.Select();
        _inputfield.text = "";
    }
	public static string GetNameRankKingChess(int _exp){
        int _count = 0;
        int _module = 0;
        string _rankName = "";
        if (_exp <= 1500)
        {
            _rankName = "Pawn I";
        }
        else if (_exp > 1500 && _exp <= 1600)
        {
            _count = (_exp - 1500) / 25;
            _module = (_exp - 1500) % 25;
            switch (_count)
            {
                case 0:
                    _rankName = "Pawn II";
                    break;
                case 1:
                    if (_module > 0)
                        _rankName = "Pawn III";
                    else
                        _rankName = "Pawn II";
                    break;
                case 2:
                    if (_module > 0)
                        _rankName = "Pawn IV";
                    else
                        _rankName = "Pawn III";
                    break;
                case 3:
                    if (_module > 0)
                        _rankName = "Pawn V";
                    else
                        _rankName = "Pawn IV";
                    break;
            }
        }
        else if (_exp > 1600 && _exp <= 1750)
        {
            _count = (_exp - 1600) / 30;
            _module = (_exp - 1600) % 30;
            switch (_count)
            {
                case 0:
                    _rankName = "Castle I";
                    break;
                case 1:
                    if (_module > 0)
                        _rankName = "Castle II";
                    else
                        _rankName = "Castle I";
                    break;
                case 2:
                    if (_module > 0)
                        _rankName = "Castle III";
                    else
                        _rankName = "Castle II";
                    break;
                case 3:
                    if (_module > 0)
                        _rankName = "Castle IV";
                    else
                        _rankName = "Castle III";
                    break;
                case 4:
                    if (_module > 0)
                        _rankName = "Castle V";
                    else
                        _rankName = "Castle IV";
                    break;
            }
        }
        else if (_exp > 1750 && _exp <= 1950)
        {
            _count = (_exp - 1750) / 40;
            _module = (_exp - 1750) % 40;
            switch (_count)
            {
                case 0:
                    _rankName = "Bishop I";
                    break;
                case 1:
                    if (_module > 0)
                        _rankName = "Bishop II";
                    else
                        _rankName = "Bishop I";
                    break;
                case 2:
                    if (_module > 0)
                        _rankName = "Bishop III";
                    else
                        _rankName = "Bishop II";
                    break;
                case 3:
                    if (_module > 0)
                        _rankName = "Bishop IV";
                    else
                        _rankName = "Bishop III";
                    break;
                case 4:
                    if (_module > 0)
                        _rankName = "Bishop V";
                    else
                        _rankName = "Bishop IV";
                    break;
            }
        }
        else if (_exp > 1950 && _exp <= 2200)
        {
            _count = (_exp - 1950) / 50;
            _module = (_exp - 1950) % 50;
            switch (_count)
            {
                case 0:
                    _rankName = "Knight I";
                    break;
                case 1:
                    if (_module > 0)
                        _rankName = "Knight II";
                    else
                        _rankName = "Knight I";
                    break;
                case 2:
                    if (_module > 0)
                        _rankName = "Knight III";
                    else
                        _rankName = "Knight II";
                    break;
                case 3:
                    if (_module > 0)
                        _rankName = "Knight IV";
                    else
                        _rankName = "Knight III";
                    break;
                case 4:
                    if (_module > 0)
                        _rankName = "Knight V";
                    else
                        _rankName = "Knight IV";
                    break;
            }
        }
        else if (_exp > 2200 && _exp <= 2500)
        {
            _count = (_exp - 2200) / 60;
            _module = (_exp - 2200) % 60;
            switch (_count)
            {
                case 0:
                    _rankName = "Queen I";
                    break;
                case 1:
                    if (_module > 0)
                        _rankName = "Queen II";
                    else
                        _rankName = "Queen I";
                    break;
                case 2:
                    if (_module > 0)
                        _rankName = "Queen III";
                    else
                        _rankName = "Queen II";
                    break;
                case 3:
                    if (_module > 0)
                        _rankName = "Queen IV";
                    else
                        _rankName = "Queen III";
                    break;
                case 4:
                    if (_module > 0)
                        _rankName = "Queen V";
                    else
                        _rankName = "Queen IV";
                    break;
            }
        }
        else if (_exp > 2500)
        {
            _count = (_exp - 2500) / 70;
            _module = (_exp - 2500) % 70;
            switch (_count)
            {
                case 0:
                    _rankName = "King I";
                    break;
                case 1:
                    if (_module > 0)
                        _rankName = "King II";
                    else
                        _rankName = "King I";
                    break;
                case 2:
                    if (_module > 0)
                        _rankName = "King III";
                    else
                        _rankName = "King II";
                    break;
                default:
                    _rankName = "King III";
                    break;
            }
        }
        return _rankName;
    }
}