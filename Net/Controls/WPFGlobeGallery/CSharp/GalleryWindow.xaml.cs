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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Drawing.Imaging;

namespace GlobeGallery
{
    public sealed partial class GalleryWindow : Window
    {
      public MapCollection mapGallery;
			public GlobeView mapView;

      public GalleryWindow()
      {
          InitializeComponent();
      }

      private void OnMapClick(object sender, RoutedEventArgs e)
      {
          mapView = new GlobeView();
					mapView.SelectedMap = (Map) MapsListBox.SelectedItem;
          mapView.Owner = this;
          mapView.Show ();
      }

			private void Window_Closing (object sender, System.ComponentModel.CancelEventArgs e)
			{
					Application.Current.Shutdown ();
			}
    }
}