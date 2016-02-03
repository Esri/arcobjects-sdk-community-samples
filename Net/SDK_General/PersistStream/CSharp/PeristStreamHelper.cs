using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using ESRI.ArcGIS.esriSystem;


namespace PeristStream
{
  [ComVisible(true)]
  [Guid("0BB8E127-E6BD-4cd0-B6FC-BEFD62283A4E")]
  public static class PeristStreamHelper
  {
    unsafe public static object Load(System.Runtime.InteropServices.ComTypes.IStream stream)
    {
      // Exit if Stream is NULL
      if (stream == null) { return null; }

      // Get Pointer to Int32
      int cb;
      int* pcb = &cb;

      // Get Size of the object's Byte Array
      byte[] arrLen = new Byte[4];
      stream.Read(arrLen, arrLen.Length, new IntPtr(pcb));
      cb = BitConverter.ToInt32(arrLen, 0);

      // Read the object's Byte Array
      byte[] bytes = new byte[cb];
      stream.Read(bytes, cb, new IntPtr(pcb));

      if (bytes.Length != cb)
        throw new Exception("Error reading object from stream");

      // Deserialize byte array
      AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(PeristStreamHelper.ResolveEventHandler);
      object data = null;
      MemoryStream memoryStream = new MemoryStream(bytes);
      BinaryFormatter binaryFormatter = new BinaryFormatter();
      object objectDeserialize = binaryFormatter.Deserialize(memoryStream);
      if (objectDeserialize != null)
      {
        data = objectDeserialize;
      }
      memoryStream.Close();
      AppDomain.CurrentDomain.AssemblyResolve -= new ResolveEventHandler(PeristStreamHelper.ResolveEventHandler);

      //deserialize ArcObjects
      if (data is string)
      {
        string str = (string)data;
        if (str.IndexOf("http://www.esri.com/schemas/ArcGIS/9.2") != -1)
        {
          IXMLStream readerStream = new XMLStreamClass();
          readerStream.LoadFromString(str);

          IXMLReader xmlReader = new XMLReaderClass();
          xmlReader.ReadFrom((IStream)readerStream);

          IXMLSerializer xmlReadSerializer = new XMLSerializerClass();
          object retObj = xmlReadSerializer.ReadObject(xmlReader, null, null);
          if (null != retObj)
            data = retObj;
        }
      }

      return data;
    }

    unsafe public static void Save(System.Runtime.InteropServices.ComTypes.IStream stream, object data)
    {
      // Exit if Stream or DataTable is NULL
      if (stream == null) { return; }
      if (data == null) { return; }

      //COM objects needs special care...
      if (Marshal.IsComObject(data))
      {
        //*** Create XmlWriter ***
        IXMLWriter xmlWriter = new XMLWriterClass();

        //*** Create XmlStream ***
        IXMLStream xmlStream = new XMLStreamClass();

        //*** Write the object to the stream ***
        xmlWriter.WriteTo(xmlStream as IStream);

        //*** Serialize object ***
        IXMLSerializer xmlSerializer = new XMLSerializerClass();
        xmlSerializer.WriteObject(xmlWriter, null, null, "arcobject", "http://www.esri.com/schemas/ArcGIS/9.2", data);

        string str = xmlStream.SaveToString();

        data = (object)str;
        if (null == data)
          return;
      }

      //make sure that the object is serializable
      if (!data.GetType().IsSerializable)
        throw new Exception("Object is not serializable.");
      
      // Convert the string into a byte array
      MemoryStream memoryStream = new MemoryStream();
      BinaryFormatter binaryFormatter = new BinaryFormatter();
      binaryFormatter.Serialize(memoryStream, data);
      byte[] bytes = memoryStream.ToArray();
      memoryStream.Close();

      // Get Memory Pointer to Int32
      int cb;
      int* pcb = &cb;

      // write the object to the structured stream
      byte[] arrLen = BitConverter.GetBytes(bytes.Length);  // Get Byte Length
      stream.Write(arrLen, arrLen.Length, new IntPtr(pcb));   // Write Byte Length
      stream.Write(bytes, bytes.Length, new IntPtr(pcb));     // Write Btye Array

      if (bytes.Length != cb)
        throw new Exception("Error writing object to stream");
    }

    private static Assembly ResolveEventHandler(object sender, ResolveEventArgs args)
    {
      Assembly assembly = null;
      for (int i = 0; i <= AppDomain.CurrentDomain.GetAssemblies().Length - 1; i++)
      {
        if (args.Name.Split(",".ToCharArray())[0].ToUpper() ==
          AppDomain.CurrentDomain.GetAssemblies()[i].FullName.Split(",".ToCharArray())[0].ToUpper())
        {
          assembly = AppDomain.CurrentDomain.GetAssemblies()[i];
          break;
        }
      }
      return assembly;
    }
  }
}
