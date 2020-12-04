using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace TimeTimePeriod.Library
{
    public struct Time : IEquatable<Time>, IComparable<Time>
    {
        public byte Hours { get; private set; }
        public byte Minutes { get; private set; }
        public byte Seconds { get; private set; }

        public Time(byte hours, byte minutes, byte seconds)
        {

            if (hours > 23 || minutes > 59 || seconds > 59 ||
                hours < 0 || minutes < 0 || seconds < 0)
                throw new ArgumentOutOfRangeException();

            Seconds = seconds;
            Minutes = minutes;
            Hours = hours;

        }
        public Time(byte hours, byte minutes) : this(hours, minutes, 0) { }
        public Time(byte hours) : this(hours, 0, 0) { }

        public Time(string stringTime)
        {
            try
            {
                var times = stringTime.Split(':');
                var hours = byte.Parse(times[0]);
                var minutes = byte.Parse(times[1]);
                var seconds = byte.Parse(times[2]);

                if (hours > 23 || minutes > 59 || seconds > 59 ||
                hours < 0 || minutes < 0 || seconds < 0)
                    throw new ArgumentOutOfRangeException();

                Seconds = seconds;
                Minutes = minutes;
                Hours = hours;

            }
            catch (Exception)
            {
                throw new ArgumentOutOfRangeException();
            }
        }



        public override string ToString() => Hours.ToString("00.#") + ":" + Minutes.ToString("00.#") + ":" + Seconds.ToString("00.#");

        public bool Equals([AllowNull] Time other) => (ConvertToSeconds(this) == ConvertToSeconds(other)) ? true : false;

        public int CompareTo([AllowNull] Time other)
        {
            int result = 0;
            if (this > other)
                result = -1;
            if (this.Equals(other))
                result = 0;
            if (this < other)
                result = 1;

            return result;
        }

        public Time Plus(TimePeriod tp) => this + tp;
        public static Time Plus(Time t, TimePeriod tp) => t + tp;
        public Time Minus(TimePeriod tp) => this - tp;
        public static Time Minus(Time t, TimePeriod tp) => t - tp;
        public static Time operator +(Time t, TimePeriod tp)
        {
            var secondsA = ConvertToSeconds(t);
            var secondsB = tp.Seconds;

            var totalSeconds = secondsA + secondsB;

            return new Time((byte)(ConvertSecondsToTime(totalSeconds).hour % 24), ConvertSecondsToTime(totalSeconds).minute, ConvertSecondsToTime(totalSeconds).second);

        }
        public static Time operator -(Time t, TimePeriod tp)
        {
            var secondsA = ConvertToSeconds(t);
            var secondsB = tp.Seconds;

            var totalSeconds = secondsA - secondsB;

            return new Time((byte)(ConvertSecondsToTime(totalSeconds).hour % 24), ConvertSecondsToTime(totalSeconds).minute, ConvertSecondsToTime(totalSeconds).second);

        }
        public static Time operator +(Time a, Time b)
        {
            var secondsA = ConvertToSeconds(a);
            var secondsB = ConvertToSeconds(b);

            var totalSeconds = secondsA + secondsB;

            return new Time((byte)(ConvertSecondsToTime(totalSeconds).hour % 24), ConvertSecondsToTime(totalSeconds).minute, ConvertSecondsToTime(totalSeconds).second);

        }
        public static Time operator -(Time a, Time b)
        {
            var secondsA = ConvertToSeconds(a);
            var secondsB = ConvertToSeconds(b);

            var subResult = 0;
            if (secondsA > secondsB)
                subResult = secondsA - secondsB;
            else if (secondsA < secondsB)
                subResult = secondsB - secondsA;
            else
                subResult = 0;

            return new Time(ConvertSecondsToTime(subResult).hour, ConvertSecondsToTime(subResult).minute, ConvertSecondsToTime(subResult).second);

        }
        public static bool operator >(Time a, Time b) => ConvertToSeconds(a) > ConvertToSeconds(b);
        public static bool operator <(Time a, Time b) => ConvertToSeconds(a) < ConvertToSeconds(b);
        public static bool operator >=(Time a, Time b) => ConvertToSeconds(a) >= ConvertToSeconds(b);
        public static bool operator <=(Time a, Time b) => ConvertToSeconds(a) <= ConvertToSeconds(b);
        public static bool operator ==(Time a, Time b) => ConvertToSeconds(a) == ConvertToSeconds(b);
        public static bool operator !=(Time a, Time b) => ConvertToSeconds(a) != ConvertToSeconds(b);

        public static int ConvertToSeconds(Time time) => time.Hours * 3600 + time.Minutes * 60 + time.Seconds;
        public static (byte hour, byte minute, byte second) ConvertSecondsToTime(long seconds)
        {
            var remainingSeconds = seconds;
            var hour = (byte)(seconds / 3600);
            remainingSeconds = remainingSeconds - (hour * 3600);
            var minutes = (byte)(remainingSeconds / 60);
            remainingSeconds = remainingSeconds - (minutes * 60);
            var secs = (byte)(remainingSeconds);

            return (hour, minutes, secs);
        }

        public override bool Equals(object obj)
        {
            if (obj is Time t)
                return this == t;
            return false;
        }

        public override int GetHashCode()
        {
            return ConvertToSeconds(this);
        }

    }



}
