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