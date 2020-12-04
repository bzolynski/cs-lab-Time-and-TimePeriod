using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using TimeTimePeriod.Library;

namespace TimeTimePeriod.UnitTests
{

    [TestClass]
    public class TimeTests
    {      

        [DataTestMethod]
        [DataRow((byte)12, (byte)10, (byte)55)]
        [DataRow((byte)0, (byte)0, (byte)0)]
        [DataRow((byte)2, (byte) 59, (byte)2)]
        
        public void Time_Constructor_3Parameters_ToString(byte h, byte m, byte s)
        {
            string hhmmss = h.ToString("00.#") + ":" + m.ToString("00.#") + ":" + s.ToString("00.#");
            Time t = new Time(h, m, s);

            Assert.AreEqual(t.ToString(), hhmmss);
        }

        [DataTestMethod]
        [DataRow((byte)12, (byte)10)]
        [DataRow((byte)0, (byte)0)]
        [DataRow((byte)2, (byte)59)]

        public void Time_Constructor_2Parameters_ToString(byte h, byte m)
        {
            string hhmmss = h.ToString("00.#") + ":" + m.ToString("00.#") + ":" + 0.ToString("00.#");
            Time t = new Time(h, m);

            Assert.AreEqual(t.ToString(), hhmmss);
        }

        [DataTestMethod]
        [DataRow((byte)12)]
        [DataRow((byte)0)]
        [DataRow((byte)2)]

        public void Time_Constructor_1Parameter_ToString(byte h)
        {
            string hhmmss = h.ToString("00.#") + ":" + 0.ToString("00.#") + ":" + 0.ToString("00.#");
            Time t = new Time(h);

            Assert.AreEqual(t.ToString(), hhmmss);
        }

        [DataTestMethod]
        [DataRow("23:29:01", (byte)23, (byte)29, (byte)1)]
        [DataRow("00:23:59", (byte)00, (byte)23, (byte)59)]
        [DataRow("00:00:00", (byte)0, (byte)0, (byte)0)]
        [DataRow("23:59:59", (byte)23, (byte)59, (byte)59)]

        public void Time_StringConstructor(string stringTime, byte h, byte m, byte s)
        {

            Time t1 = new Time(stringTime);
            Time t2 = new Time(h, m, s);

            Assert.AreEqual(t1, t2);
        }

        [DataTestMethod]
        [DataRow("23:29:01", (byte)23, (byte)29, (byte)1)]
        [DataRow("00:23:59", (byte)00, (byte)23, (byte)59)]
        [DataRow("00:00:00", (byte)0, (byte)0, (byte)0)]
        [DataRow("23:59:59", (byte)23, (byte)59, (byte)59)]

        public void Time_StringConstructor_EqualValues(string stringTime, byte h, byte m, byte s)
        {

            Time t1 = new Time(stringTime);

            Assert.AreEqual(t1.Hours, h);
            Assert.AreEqual(t1.Minutes, m);
            Assert.AreEqual(t1.Seconds, s);
        }

        [DataTestMethod]
        [DataRow("323:29:01")]
        [DataRow("00:-213:59")]
        [DataRow("00:00:155")]
        [DataRow("23:60:59")]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]

        public void Time_StringConstructor_ArgumentOutOfRangeException(string stringTime)
        {

            Time t = new Time(stringTime);

        }


        [DataTestMethod]
        [DataRow((byte)69, (byte)10, (byte)0)]
        [DataRow((byte)25, (byte)10 ,(byte)55)]
        [DataRow((byte)12, (byte)60, (byte)25)]
        [DataRow((byte)42, (byte)60, (byte)25)]
        [DataRow((byte)12, (byte)10, (byte)125)]
        [DataRow((byte)12, (byte)10, (byte)65)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Time_Constructor_3Parameters_ArgumentOutOfRangeException(byte h, byte m, byte s)
        {
            Time t = new Time(h, m, s);
        }

        [DataTestMethod]
        [DataRow((byte)12, (byte)10, (byte)55,
                 (byte)12, (byte)10, (byte)55, true) ]
        [DataRow((byte)0, (byte)0, (byte)0,
                 (byte)0, (byte)0, (byte)1, false)]
        [DataRow((byte)2, (byte)59, (byte)2,
                 (byte)2, (byte)59, (byte)2, true)]
        public void Time_IEquatable(byte h1, byte m1, byte s1,
                                    byte h2, byte m2, byte s2, bool expected)
        {
            Time t1 = new Time(h1, m1, s1);
            Time t2 = new Time(h2, m2, s2);

            Assert.AreEqual(t1.Equals(t2), expected);
        }

        [DataTestMethod]
        [DataRow((byte)12, (byte)10, (byte)55,
                 (byte)12, (byte)10, (byte)55, 0)]
        [DataRow((byte)0, (byte)0, (byte)0,
                 (byte)0, (byte)0, (byte)1, 1)]
        [DataRow((byte)2, (byte)59, (byte)2,
                 (byte)2, (byte)50, (byte)2, -1)]
        public void Time_IComparable(byte h1, byte m1, byte s1,
                                     byte h2, byte m2, byte s2, int expected)
        {
            Time t1 = new Time(h1, m1, s1);
            Time t2 = new Time(h2, m2, s2);

            Assert.AreEqual(t1.CompareTo(t2), expected);
        }

        

        

    }

    [TestClass]
    public class TimeTimePeriodTests
    {
        [DataTestMethod]
        [DataRow((byte)12, (byte)10, (byte)55,
                 (byte)12, (byte)10, (byte)55,
                 (byte)0, (byte)21, (byte)50)]
        [DataRow((byte)0, (byte)0, (byte)0,
                 (byte)0, (byte)0, (byte)1,
                 (byte)0, (byte)0, (byte)1)]
        [DataRow((byte)2, (byte)59, (byte)2,
                 (byte)2, (byte)50, (byte)2,
                 (byte)5, (byte)49, (byte)4)]
        public void Time_Plus_TimePeriod(byte h1, byte m1, byte s1,
                              byte h2, byte m2, byte s2,
                              byte h3, byte m3, byte s3)
        {
            Time t = new Time(h1, m1, s1);
            TimePeriod tp = new TimePeriod(h2, m2, s2);
            Time tResult = new Time(h3, m3, s3);

            Assert.AreEqual(t.Plus(tp), tResult);
        }


    }

    [TestClass]
    public class TimePeriodTests
    {
        [DataTestMethod]
        [DataRow((byte)122, (byte)10, (byte)55)]
        [DataRow((byte)10, (byte)0, (byte)0)]
        [DataRow((byte)2, (byte)59, (byte)2)]

        public void TimePeriod_Constructor_3Parameters_ToString(byte h, byte m, byte s)
        {
            string hmmss = h.ToString() + ":" + m.ToString("00.#") + ":" + s.ToString("00.#");
            TimePeriod t = new TimePeriod(h, m, s);

            Assert.AreEqual(t.ToString(), hmmss);
        }

        [DataTestMethod]
        [DataRow((byte)12, (byte)10, (byte)55,
                 (byte)12, (byte)10, (byte)55,
                 (byte)0, (byte)0, (byte)0)]
        [DataRow((byte)0, (byte)0, (byte)0,
                 (byte)0, (byte)0, (byte)1,
                 (byte)0, (byte)0, (byte)1)]
        [DataRow((byte)2, (byte)59, (byte)2,
                 (byte)2, (byte)50, (byte)2,
                 (byte)0, (byte)9, (byte)0)]
        public void TimePeriod_Constructor_3Times(byte h1, byte m1, byte s1,
                                                  byte h2, byte m2, byte s2,
                                                  long tph, byte tpm, byte tps)
        {

            Time t1 = new Time(h1, m1, s1);
            Time t2 = new Time(h2, m2, s2);
            TimePeriod tp = new TimePeriod(t1, t2);
            TimePeriod tpExpected = new TimePeriod(tph, tpm, tps);

            Assert.AreEqual(tp, tpExpected);
        }

        [DataTestMethod]
        [DataRow("122:10:55", (byte)122, (byte)10, (byte)55)]
        [DataRow("10:00:00", (byte)10, (byte)0, (byte)0)]
        [DataRow("2:59:02", (byte)2, (byte)59, (byte)2)]

        public void TimePeriod_StringConstructor(string stringTP, byte h, byte m, byte s)
        {

            TimePeriod t = new TimePeriod(stringTP);

            Assert.AreEqual(t.Seconds, h * 3600 + m * 60 + s);
        }

        [DataTestMethod]
        [DataRow((byte)12, (byte)10, (byte)55,
                 (byte)12, (byte)10, (byte)55, true)]
        [DataRow((byte)0, (byte)0, (byte)0,
                 (byte)0, (byte)0, (byte)1, false)]
        [DataRow((byte)2, (byte)59, (byte)2,
                 (byte)2, (byte)59, (byte)2, true)]
        public void TimePeriod_IEquatable(byte h1, byte m1, byte s1,
                                    byte h2, byte m2, byte s2, bool expected)
        {
            TimePeriod tp1 = new TimePeriod(h1, m1, s1);
            TimePeriod tp2 = new TimePeriod(h2, m2, s2);

            Assert.AreEqual(tp1.Equals(tp2), expected);
        }

        [DataTestMethod]
        [DataRow((byte)12, (byte)10, (byte)55,
                 (byte)12, (byte)10, (byte)55, 0)]
        [DataRow((byte)0, (byte)0, (byte)0,
                 (byte)0, (byte)0, (byte)1, 1)]
        [DataRow((byte)2, (byte)59, (byte)2,
                 (byte)2, (byte)50, (byte)2, -1)]
        public void TimePeriod_IComparable(byte h1, byte m1, byte s1,
                                     byte h2, byte m2, byte s2, int expected)
        {
            TimePeriod tp1 = new TimePeriod(h1, m1, s1);
            TimePeriod tp2 = new TimePeriod(h2, m2, s2);

            Assert.AreEqual(tp1.CompareTo(tp2), expected);
        }

        [DataTestMethod]
        [DataRow((byte)12, (byte)10, (byte)55,
                 (byte)12, (byte)10, (byte)55,
                 (byte)24, (byte)21, (byte)50)]
        [DataRow((byte)0, (byte)0, (byte)0,
                 (byte)0, (byte)0, (byte)1,
                 (byte)0, (byte)0, (byte)1)]
        [DataRow((byte)24, (byte)59, (byte)2,
                 (byte)2, (byte)50, (byte)2,
                 (byte)27, (byte)49, (byte)4)]
        public void TimePeriod_Plus(byte h1, byte m1, byte s1,
                                     byte h2, byte m2, byte s2,
                                     byte h3, byte m3, byte s3)
        {
            TimePeriod tp1 = new TimePeriod(h1, m1, s1);
            TimePeriod tp2 = new TimePeriod(h2, m2, s2);
            TimePeriod tpExpected = new TimePeriod(h3, m3, s3);

            Assert.AreEqual(tp1.Plus(tp2), tpExpected);

        }

        [DataTestMethod]
        [DataRow((byte)12, (byte)10, (byte)55,
                 (byte)12, (byte)10, (byte)55,
                 (byte)0, (byte)0, (byte)0)]
        [DataRow((byte)0, (byte)0, (byte)0,
                 (byte)0, (byte)0, (byte)1,
                 (byte)0, (byte)0, (byte)1)]
        [DataRow((byte)24, (byte)59, (byte)2,
                 (byte)2, (byte)50, (byte)2,
                 (byte)22, (byte)9, (byte)0)]
        public void TimePeriod_Diff(byte h1, byte m1, byte s1,
                                     byte h2, byte m2, byte s2,
                                     byte h3, byte m3, byte s3)
        {
            TimePeriod tp1 = new TimePeriod(h1, m1, s1);
            TimePeriod tp2 = new TimePeriod(h2, m2, s2);
            TimePeriod tpExpected = new TimePeriod(h3, m3, s3);

            Assert.AreEqual(tp1.Difference(tp2), tpExpected);

        }

        [DataTestMethod]
        [DataRow((byte)12, (byte)10, (byte)55,
                 2,
                 (byte)0, (byte)0, (byte)0)]
        [DataRow((byte)0, (byte)0, (byte)0,
                 10,
                 (byte)0, (byte)0, (byte)1)]
        [DataRow((byte)24, (byte)58, (byte)2,
                 1,5,
                 (byte)22, (byte)9, (byte)0)]
        public void TimePeriod_Diff(byte h1, byte m1, byte s1,
                                     double multi,
                                     byte h3, byte m3, byte s3)
        {
            TimePeriod tp = new TimePeriod(h1, m1, s1);
            TimePeriod tpExpected = new TimePeriod(h3, m3, s3);

            Assert.AreEqual(tp.Multiplication(multi), tpExpected);

        }
    }
}
