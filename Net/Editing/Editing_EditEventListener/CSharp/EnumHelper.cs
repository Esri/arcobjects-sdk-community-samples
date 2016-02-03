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
