/*

   Copyright 2016 Esri

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.

   See the License for the specific language governing permissions and
   limitations under the License.

*/
using System;
using System.Collections.Generic;
using System.Text;

namespace GlobeFlyTool
{
    public class PointZ
    {
        public double x;
        public double y;
        public double z;

        public PointZ()
        {
            x = 0;
            y = 0;
            z = 0;
        }
        public PointZ(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public double Norm()
        {
            double val = Math.Sqrt((this.x * this.x) +
                                   (this.y * this.y) +
                                   (this.z * this.z));
            return val;
        }

        public static PointZ operator +(PointZ p1, PointZ p2)
        {
            PointZ newPoint = new PointZ();
            newPoint.x = p1.x + p2.x;
            newPoint.y = p1.y + p2.y;
            newPoint.z = p1.z + p2.z;
            return newPoint;
        }

        public static PointZ operator-(PointZ p1, PointZ p2)
        {
            PointZ newPoint = new PointZ();
            newPoint.x = p1.x - p2.x;
            newPoint.y = p1.y - p2.y;
            newPoint.z = p1.z - p2.z;
            return newPoint;
        }

        public static PointZ operator*(PointZ p, double factor)
        {
            PointZ newPoint = new PointZ(factor * p.x, factor * p.y, factor * p.z);
            return newPoint;
        }
    }
}
