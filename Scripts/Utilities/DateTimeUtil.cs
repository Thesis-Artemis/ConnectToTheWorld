using UnityEngine;
using System;

public static class DateTimeUtil {
    private static DateTime dateTime1970 = System.DateTime.MinValue;
    public static long currentUtcTimeMilliseconds { 
		get 
		{ 
			return DateTime.UtcNow.GetCurrentUtcTimeMillisecondsAtTime();
		} 
	}
	public static long GetCurrentUtcTimeMillisecondsAtTime(this DateTime _dateTime) { 
        if(dateTime1970 == DateTime.MinValue){
            dateTime1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        }
		long _value = (long)(_dateTime - dateTime1970).TotalMilliseconds;
		return _value;
	}
	public static DateTime ParseToUtcTime(long _currentMilliseconds){
        if(dateTime1970 == DateTime.MinValue){
            dateTime1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        }
		DateTime _time = dateTime1970.AddMilliseconds(_currentMilliseconds);
		return _time;
	}
	public static DateTime EndOfDay(this DateTime _dateTime){
        return _dateTime.Date.AddDays(1).AddTicks(-1);
    }
}
