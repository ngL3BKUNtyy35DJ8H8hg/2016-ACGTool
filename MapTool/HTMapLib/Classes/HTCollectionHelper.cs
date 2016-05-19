using System;
using System.Collections.Generic;
using System.Text;

namespace HTMapLib.Classes
{
    public static class HTCollectionHelper
    {
        /// <summary>
        /// http://en.wikipedia.org/wiki/Geographic_coordinate_conversion
        /// Input a double latitude or longitude in the decimal format
        /// e.g. 87.728056
        /// </summary>
        /// <param name="coord"></param>
        /// <returns></returns>
        public static string DecimalToDMS(double coord)
        {
            string output, degrees, minutes, seconds;

            // gets the modulus the coordinate divided by one (MOD1).
            // in other words gets all the numbers after the decimal point.
            // e.g. mod = 87.728056 % 1 == 0.728056
            //
            // next get the integer part of the coord. On other words the whole number part.
            // e.g. intPart = 87

            double mod = coord % 1;
            int intPart = (int)coord;

            //set degrees to the value of intPart
            //e.g. degrees = "87"

            degrees = intPart.ToString();

            // next times the MOD1 of degrees by 60 so we can find the integer part for minutes.
            // get the MOD1 of the new coord to find the numbers after the decimal point.
            // e.g. coord = 0.728056 * 60 == 43.68336
            //      mod = 43.68336 % 1 == 0.68336
            //
            // next get the value of the integer part of the coord.
            // e.g. intPart = 43

            coord = mod * 60;
            mod = coord % 1;
            intPart = (int)coord;

            // set minutes to the value of intPart.
            // e.g. minutes = "43"
            minutes = intPart.ToString();

            //do the same again for minutes
            //e.g. coord = 0.68336 * 60 == 41.0016
            //e.g. intPart = 41
            coord = mod * 60;
            intPart = (int)coord;

            // set seconds to the value of intPart.
            // e.g. seconds = "41"
            seconds = intPart.ToString();

            // I used this format for android but you can change it 
            // to return in whatever format you like
            // e.g. output = "87/1,43/1,41/1"
            //output = degrees + "/1," + minutes + "/1," + seconds + "/1";

            //Standard output of D°M′S″
            output = degrees + "°" + minutes + "'" + seconds + "\"";

            return output;
        }

        public static string DecimalToDMS(double coord, ref string degrees, ref string minutes, ref string seconds)
        {
            string output;

            // gets the modulus the coordinate divided by one (MOD1).
            // in other words gets all the numbers after the decimal point.
            // e.g. mod = 87.728056 % 1 == 0.728056
            //
            // next get the integer part of the coord. On other words the whole number part.
            // e.g. intPart = 87

            double mod = coord % 1;
            int intPart = (int)coord;

            //set degrees to the value of intPart
            //e.g. degrees = "87"

            degrees = intPart.ToString();

            // next times the MOD1 of degrees by 60 so we can find the integer part for minutes.
            // get the MOD1 of the new coord to find the numbers after the decimal point.
            // e.g. coord = 0.728056 * 60 == 43.68336
            //      mod = 43.68336 % 1 == 0.68336
            //
            // next get the value of the integer part of the coord.
            // e.g. intPart = 43

            coord = mod * 60;
            mod = coord % 1;
            intPart = (int)coord;

            // set minutes to the value of intPart.
            // e.g. minutes = "43"
            minutes = intPart.ToString();

            //do the same again for minutes
            //e.g. coord = 0.68336 * 60 == 41.0016
            //e.g. intPart = 41
            coord = mod * 60;
            intPart = (int)coord;

            // set seconds to the value of intPart.
            // e.g. seconds = "41"
            seconds = intPart.ToString();

            // I used this format for android but you can change it 
            // to return in whatever format you like
            // e.g. output = "87/1,43/1,41/1"
            //output = degrees + "/1," + minutes + "/1," + seconds + "/1";

            //Standard output of D°M′S″
            output = degrees + "°" + minutes + "'" + seconds + "\"";

            return output;
        }
        /*
         * Conversion DMS to decimal 
         *
         * Input: latitude or longitude in the DMS format ( example: N 43° 36' 15.894")
         * Return: latitude or longitude in decimal format   
         * hemisphereOUmeridien => {W,E,S,N}
         *
         */
        public static double DMSToDecimal(string hemisphereOUmeridien, double degres, double minutes, double secondes)
        {
            double LatOrLon = 0;
            double signe = 1.0;

            if ((hemisphereOUmeridien.Equals("W")) || (hemisphereOUmeridien.Equals("S"))) { signe = -1.0; }
            LatOrLon = signe * (Math.Floor(degres) + Math.Floor(minutes) / 60.0 + secondes / 3600.0);

            return (LatOrLon);
        }

        /*
         * Conversion N10.39168, E107.13765 to decimal 
         *
         * Input: latitude or longitude in the DMS format ( example: N 43° 36' 15.894")
         * Return: latitude or longitude in decimal format   
         * hemisphereOUmeridien => {W,E,S,N}
         *
         */
        public static double StringCoordToDecimalCoord(string value)
        {
            double LatOrLon = 0;
            double signe = 1.0;
            //Lat
            string hemisphereOUmeridien = value.Substring(0, 1); //{W,E,S,N}
            if ((hemisphereOUmeridien.Equals("W")) || (hemisphereOUmeridien.Equals("S"))) { signe = -1.0; }
            LatOrLon = signe * double.Parse(value.Substring(1, value.Length - 1));

            return (LatOrLon);
        }
    }
}
