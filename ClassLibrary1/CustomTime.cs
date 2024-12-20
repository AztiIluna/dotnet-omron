using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary1
{
    public class CustomTime
    {
        public  int Days { get; set; }
        public int Hours { get; set; }
        public int Minutes { get; set; }
        public int Seconds { get; set; }


        public CustomTime(TimeSpan timeSpan )
        {
            Days = timeSpan.Days;
            Hours = timeSpan.Hours;
            Minutes = timeSpan.Minutes;
            Seconds = timeSpan.Seconds;
        }


        public string Text()
        {
                      
            return $"{Days} Days, {Hours} Hours, {Minutes} Minutes, {Seconds} Seconds";

        }

        public TimeSpan ToTimeSpan() => new TimeSpan(Days, Hours, Minutes, Seconds);


        public static bool TryParse( string time, out CustomTime customTime)
        {

            if (time==null)
            {
                customTime = null;
                return false;
            }

            List<short> data = new List<short>();

            var splitTime=time.Split(' ');
            

            foreach (var item in splitTime )
            {
                var d = item.Split(':');
                data.Add(Convert.ToInt16 (d[1]));
            }


            //Days:2 Hours:2 Minutes:48 Seconds:14"

            customTime = new CustomTime(new TimeSpan(data[0], data[1], data[2], data[3]));


            return true;



        }


        ///Ainhoa
        //public static bool operator >=(CustomTime a, CustomTime b) 
        //{
        //    return true;
        //}
        //public static bool operator <=(CustomTime a, CustomTime b)
        //{
        //    return true;
        //}

    }
}
