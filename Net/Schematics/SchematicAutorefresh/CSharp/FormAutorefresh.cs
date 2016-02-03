using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Schematic;
using ESRI.ArcGIS.SchematicUI;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Framework;

namespace Autorefresh
{
	public partial class FormAutoRefresh : Form
	{
		private ISchematicInMemoryDiagram m_schematicInMemoryDiagram = null;
		private IApplication m_application = null;

		private int m_minute = 0;
		private int m_second = 4;

		public FormAutoRefresh()
		{
			InitializeComponent();
		}

		~FormAutoRefresh()
		{
		}

		public void InitializeMinute()
		{
			for (int i = 0; i < 10; i++)
			{
				this.IntervalMinute.Items.Add(this.IntervalMinute.FormatString.Insert(0, i.ToString()));
			}

			this.IntervalMinute.SelectedIndex = m_minute;

		}

		public void InitializeSecond()
		{
			for (int i = 0; i < 60; i++)
			{
				this.IntervalSecond.Items.Add(this.IntervalSecond.FormatString.Insert(0, i.ToString()));
			}

			this.IntervalSecond.SelectedIndex = m_second;

		}

		public bool GetAutoOn()
		{
			return AutoOn.Checked;
		}

		public bool GetAutoOff()
		{
			return AutoOff.Checked;
		}

		public void SetAutoOn(bool on)
		{
			AutoOn.Checked = on;
		}

		public void SetAutoOff(bool off)
		{
			AutoOff.Checked = off;
		}

	
		public int GetMinute()
		{
			return IntervalMinute.SelectedIndex;
		}

		public int GetSecond()
		{
			return IntervalSecond.SelectedIndex;
		}

		private void IntervalMinute_SelectedIndexChanged(object sender, EventArgs e)
		{
			InitializeTimer();
		}

		private void IntervalSecond_SelectedIndexChanged(object sender, EventArgs e)
		{
			InitializeTimer();
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			timerAutoRefresh.Stop();
			this.IntervalSecond.SelectedIndex = m_second;
			this.IntervalMinute.SelectedIndex = m_minute;
			this.Hide();
		}

		private void InitializeTimer()
		{
			///Obtaint or to défine the time by millisecondes
			////// <summary>
			/// Obtaint or to défine the time by millisecondes
			/// </summary>
			if (this.IntervalMinute.SelectedIndex != -1 && this.IntervalSecond.SelectedIndex != -1)
			{
				int time = (this.IntervalMinute.SelectedIndex * 60000) + (this.IntervalSecond.SelectedIndex * 1000);
				if (time > 0)
					timerAutoRefresh.Interval = time;
				else
					timerAutoRefresh.Stop();
			}
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			if (this.AutoOn.Checked)
			{
				InitializeTimer();
				if (timerAutoRefresh.Interval > 0)
					timerAutoRefresh.Start();
			}
			else
				timerAutoRefresh.Stop();

			this.Hide();
		}

		private void FormAutoRefresh_Load(object sender, EventArgs e)
		{
			this.IntervalSecond.SelectedIndex = m_second;
			this.IntervalMinute.SelectedIndex = m_minute;
		}

		public void SetSchematicInmemoryDiagram(ISchematicInMemoryDiagram SchMemoryDiag)
		{
			m_schematicInMemoryDiagram = SchMemoryDiag;
		}

		public ESRI.ArcGIS.Framework.IApplication Appli
		{
			set
			{
				m_application = value;
			}
		}

		private void RefreshViewerWindows()
		{
			//refresh viewer window
			IApplicationWindows applicationWindows = m_application as IApplicationWindows;

			ISet set = applicationWindows.DataWindows;
			if (set != null)
			{
				set.Reset();
				IMapInsetWindow dataWindow = (IMapInsetWindow)set.Next();
				while (dataWindow != null)
				{
					dataWindow.Refresh();
					dataWindow = (IMapInsetWindow)set.Next();
				}
			}
		}

		private void timerAutoRefresh_Tick(object sender, EventArgs e)
		{
			if (m_schematicInMemoryDiagram != null && this.AutoOn.Checked)
			{
				timerAutoRefresh.Stop();
				m_schematicInMemoryDiagram.Refresh();

				ILayer layer;
				IDocument doc;
				IMxDocument mxDoc;
				IMaps maps;
				IEnumLayer enumLayers;
				IMap map;
				ISchematicLayer schematicLayer = null;

				doc = m_application.Document;
				mxDoc = doc as IMxDocument;

				if (mxDoc == null) return;

				maps = mxDoc.Maps;

				for (int i = 0; i < maps.Count; i++)
				{
					map = maps.get_Item(i);

					enumLayers = map.get_Layers(null, true);
					enumLayers.Reset();
					layer = enumLayers.Next();
					while (layer != null)
					{
						string sText = layer.Name;
						try
						{
							if (layer is ISchematicLayer)
							{
								schematicLayer = (ISchematicLayer)layer;

								if (schematicLayer.SchematicInMemoryDiagram != null)
								{
									if (schematicLayer.SchematicInMemoryDiagram == m_schematicInMemoryDiagram) break;
								}
								else if (sText == m_schematicInMemoryDiagram.Name) break;
							}
						}
						finally
						{
							layer = null;
						}

						schematicLayer = null;
						layer = enumLayers.Next();
					}

					if (schematicLayer != null)
					{
						IActiveView actiView;
						actiView = (IActiveView)map;
						actiView.Refresh();
					}
				}

				RefreshViewerWindows();
				timerAutoRefresh.Start();
			}
		}
	}
}

