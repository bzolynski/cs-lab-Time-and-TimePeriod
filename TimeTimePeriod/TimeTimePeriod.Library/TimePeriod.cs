using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace TimeTimePeriod.Library
{
    public struct TimePeriod : IEquatable<TimePeriod>, IComparable<TimePeriod>
    {
        public long Seconds { get; private set; }

        /// <summary>
        /// Tworzenie nowego zakresu czasu
        /// </summary>
        /// <param name="hours">Ilość godzin większa od 0</param>
        /// <param name="minutes">Ilość minut w zakresie 0 - 59</param>
        /// <param name="seconds">Ilość sekund w zakresie 0 - 59</param>
        public TimePeriod(long hours, byte minutes, byte seconds)
        {
            if (hours < 0 || minutes > 59 || seconds > 59 ||
                              minutes < 0 || seconds < 0)
                throw new ArgumentOutOfRangeException();

            Seconds = hours * 3600 + minutes * 60 + seconds;
        }
        /// <summary>
        /// Tworzenie nowego zakresu czasu
        /// </summary>
        /// <param name="hours">Ilość godzin większa od 0</param>
        /// <param name="minutes">Ilość minut w zakresie 0 - 59</param>
        public TimePeriod(long hours, byte minutes) : this(hours, minutes, 0) { }

        /// <summary>
        /// Tworzenie nowego zakresu czasu
        /// </summary>
        /// <param name="seconds">Ilość sekund większa od 0</param>
        public TimePeriod(long seconds)
        {
            if (seconds < 0)
                throw new ArgumentOutOfRangeException();

            Seconds = seconds;
        }

        /// <summary>
        /// Tworzenie nowego zakresu czasu
        /// </summary>
        /// <param name="stringTimePeriod">Zakres czasu podany w formacie: h:mm:ss</param>
        public TimePeriod(string stringTimePeriod)
        {
            try
            {
                var times = stringTimePeriod.Split(':');
                var hours = long.Parse(times[0]);
                var minutes = byte.Parse(times[1]);
                var seconds = byte.Parse(times[2]);

                if (minutes > 59 || seconds > 59 ||
                hours < 0 || minutes < 0 || seconds < 0)
                    throw new ArgumentOutOfRangeException();

                Seconds = hours * 3600 + minutes * 60 + seconds;


            }
            catch (Exception)
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Zakres czasu będący różnicą dwóch dat
        /// </summary>
        /// <param name="a">Data 1</param>
        /// <param name="b">Data 2</param>
        public TimePeriod(Time a, Time b)
        {
            if (a > b)
                Seconds = Time.ConvertToSeconds(a - b);
            else if (b > a)
                Seconds = Time.ConvertToSeconds(b - a);
            else
                Seconds = 0;

        }

        public override string ToString() => ConvertToHHMMSS(Seconds).hours.ToString() + ":" + ConvertToHHMMSS(Seconds).minutes.ToString("00.#") + ":" + ConvertToHHMMSS(Seconds).seconds.ToString("00.#");

        private (long hours, byte minutes, byte seconds) ConvertToHHMMSS(long seconds)
        {
            var remainingSeconds = seconds;
            var hour = (byte)(seconds / 3600);
            remainingSeconds = remainingSeconds - (hour * 3600);
            var minutes = (byte)(remainingSeconds / 60);
            remainingSeconds = remainingSeconds - (minutes * 60);
            var secs = (byte)(remainingSeconds);

            return (hour, minutes, secs);
        }


        public bool Equals([AllowNull] TimePeriod other) => this.Seconds == other.Seconds;

        public int CompareTo([AllowNull] TimePeriod other)
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



        public TimePeriod Plus(TimePeriod timePeriod) => this + timePeriod;
        public static TimePeriod Plus(TimePeriod a, TimePeriod b) => a + b;

        public TimePeriod Difference(TimePeriod timePeriod) => this - timePeriod;
        public static TimePeriod Difference(TimePeriod a, TimePeriod b) => a - b;

        /// <summary>
        /// Mnożenie zakresu czasu
        /// </summary>
        /// <param name="multiplier">Mnożnik musi być większy bądź równy 0</param>
        /// <returns></returns>
        public TimePeriod Multiplication(double multiplier)
        {
            if (multiplier < 0)
                throw new ArgumentOutOfRangeException();
            return this * multiplier;
        }

        /// <summary>
        /// Mnożenie zakresu czasu
        /// </summary>
        /// <param name="a">Zakres czasu do pomnożenie</param>
        /// <param name="multiplier">Mnożnik musi być większy od 0</param>
        /// <returns></returns>
        public static TimePeriod Multiplication(TimePeriod a, double multiplier)
        {
            if (multiplier < 0)
                throw new ArgumentOutOfRangeException();
            return a * multiplier;
        }

        /// <summary>
        /// Dzielenie przez liczbę większą od zera
        /// </summary>
        /// <param name="divider">Divider greater than 0</param>
        /// <returns></returns>
        public TimePeriod Division(double divider) => this / divider;

        /// <summary>
        /// Dzielenie przez liczbę większą od zera
        /// </summary>
        /// <param name="a">Time period</param>
        /// <param name="divider">Divider greater than 0</param>
        /// <returns></returns>
        public static TimePeriod Division(TimePeriod a, double divider) => a / divider;

        /// <summary>
        /// Dzielenie przez liczbę większą od zera
        /// </summary>
        /// <param name="a">Time period</param>
        /// <param name="divider">Divider greater than 0</param>
        /// <returns></returns>
        public static TimePeriod operator /(TimePeriod a, double divider)
        {
            if (divider <= 0)
                throw new ArgumentOutOfRangeException();

            return new TimePeriod((long)(a.Seconds / divider));
        }

        public static TimePeriod operator *(TimePeriod a, double multiplier) => new TimePeriod((long)(a.Seconds * multiplier));
        public static TimePeriod operator +(TimePeriod a, TimePeriod b) => new TimePeriod(a.Seconds + b.Seconds);
        public static TimePeriod operator -(TimePeriod a, TimePeriod b) => new TimePeriod(Math.Abs(a.Seconds - b.Seconds));
        public static bool operator >(TimePeriod a, TimePeriod b) => a.Seconds > b.Seconds;
        public static bool operator <(TimePeriod a, TimePeriod b) => a.Seconds < b.Seconds;
        public static bool operator >=(TimePeriod a, TimePeriod b) => a.Seconds >= b.Seconds;
        public static bool operator <=(TimePeriod a, TimePeriod b) => a.Seconds <= b.Seconds;
        public static bool operator ==(TimePeriod a, TimePeriod b) => a.Seconds == b.Seconds;
        public static bool operator !=(TimePeriod a, TimePeriod b) => a.Seconds != b.Seconds;


        public override bool Equals(object obj)
        {
            if (obj is TimePeriod tp)
                return this == tp;
            return false;
        }

        public override int GetHashCode()
        {
            return (int)Seconds / 2137;
        }

    }
}
