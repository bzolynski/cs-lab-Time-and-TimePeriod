using System;
using TimeTimePeriod.Library;

namespace TimeTimePeriod.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Time time1 = new Time(12, 20, 00); 
            Time time2 = new Time("11:40:00");
            Time time3 = new Time("00:-213:59");
            TimePeriod timePeriod = new TimePeriod(10, 35, 00);
            System.Console.WriteLine(timePeriod);
            timePeriod /= 2;

            var coco = time2 + timePeriod;
            var coc2 = time2 - timePeriod;
                var cos = time1 -time2;

                var cos2 = time1 + time2;

        }
    }
}
