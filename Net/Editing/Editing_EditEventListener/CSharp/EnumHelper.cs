/*

   Copyright 2019 Esri

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

namespace Events
{ 
  public class EnumUtils
	{
    public static List<T> EnumToList<T>()
    {

      Type enumType = typeof(T);

      // Can't use type constraints on value types, so have to do check like this

      if (enumType.BaseType != typeof(Enum))
      {
        throw new ArgumentException("T must be of type System.Enum");
      }

      return new List<T>(Enum.GetNames(enumType) as IEnumerable<T>);

    }
        
	}
}
