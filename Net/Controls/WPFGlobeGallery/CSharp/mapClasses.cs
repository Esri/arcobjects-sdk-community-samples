using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Media.Imaging;


namespace GlobeGallery
{
	/// <summary>
	/// This class describes a single map - its location, the map name
	/// </summary>

		public class Map
		{
			public Map (string path)
			{
					_path = path;
					_source = new Uri (path);
					_image = BitmapFrame.Create (_source);
					string sub = path.Substring (path.LastIndexOf ("\\") + 1);
					_mapName = sub.Substring(0,sub.Length-4);
			}

			private string _path;
			private string _mapName;
			public string MapName { get { return _mapName; } }

			private Uri _source;
			public string Source { get { return _path; } }

			private BitmapFrame _image;
			public BitmapFrame Image { get { return _image; } set { _image = value; } }

		}

		/// <summary>
		/// This class represents a collection of map images in a directory.
		/// </summary>
		public class MapCollection: ObservableCollection<Map>
		{
			public MapCollection () { }

			public MapCollection (string path) : this (new DirectoryInfo (path)) { }

			public MapCollection (DirectoryInfo directory)
			{
				_directory = directory;
				Update ();
			}

			public string Path
			{
				set
				{
					_directory = new DirectoryInfo (value);
					Update ();
				}
				get { return _directory.FullName; }
			}

			public DirectoryInfo Directory
			{
				set
				{
					_directory = value;
					Update ();
				}
				get { return _directory; }
			}

			private void Update ()
			{
				this.Clear ();
				try
				{
					foreach (FileInfo f in _directory.GetFiles ("*.jpg"))
						Add (new Map (f.FullName));
				}
				catch (DirectoryNotFoundException)
				{
					System.Windows.MessageBox.Show ("No Such Directory");
				}
			}
			DirectoryInfo _directory;
		}

		/// <summary>
		/// This class returns a local data path as global variable.
		/// </summary>
		public class data
		{
			public static string GetLocalDataPath ()
			{
				string rootPath = Environment.CurrentDirectory;
				int position = rootPath.LastIndexOf ("\\");
				string subString = rootPath.Substring (0, position);
				return rootPath.Substring(0,subString.LastIndexOf("\\"));
			}
		}

}

	

