namespace CustomExtCriteriaCS
{
	public class EnumCollapsedElts : ESRI.ArcGIS.Schematic.IEnumSchematicInMemoryFeature
	{
		private System.Collections.Generic.List<ESRI.ArcGIS.Schematic.ISchematicInMemoryFeature> m_listElements;
		private int m_maxElements;
		private int m_currentIndex;

		public EnumCollapsedElts()
		{
			m_listElements = new System.Collections.Generic.List<ESRI.ArcGIS.Schematic.ISchematicInMemoryFeature>();
			m_maxElements = 100; // Default
		}

		~EnumCollapsedElts()
		{
			m_listElements = null;
		}

		public int MaxElements
		{
			get
			{
				return m_maxElements;
			}
			set
			{
				m_maxElements = value;
			}
		}

		public void Initialize(ref ESRI.ArcGIS.Schematic.IEnumSchematicInMemoryFeature relatedElements)
		{
			m_currentIndex = 0;
			m_listElements = new System.Collections.Generic.List<ESRI.ArcGIS.Schematic.ISchematicInMemoryFeature>();

			relatedElements.Reset();
			ESRI.ArcGIS.Schematic.ISchematicInMemoryFeature schemElement = relatedElements.Next();

			// add all Schematic feature to the list
			while ((schemElement != null) && (m_listElements.Count < m_maxElements))
			{
				m_listElements.Add(schemElement);
				schemElement = relatedElements.Next();
			}
		}

		#region IEnumSchematicElement Implementations
		public int Count
		{
			get
			{
				return m_listElements.Count;
			}
		}

		public ESRI.ArcGIS.Schematic.ISchematicInMemoryFeature Next()
		{
			if (m_currentIndex < (m_listElements.Count - 1))
			{
				// return the element at m_currentIndex, then increment it
				return m_listElements[m_currentIndex++];
			}
			else
			{
				return null;
			}
		}

		public void Reset()
		{
			m_currentIndex = 0;
		}
		#endregion

		public void Add(ESRI.ArcGIS.Schematic.ISchematicInMemoryFeature value)
		{
			if (m_listElements == null)
			{
				m_listElements = new System.Collections.Generic.List<ESRI.ArcGIS.Schematic.ISchematicInMemoryFeature>();
			}

			if (m_listElements.Count < m_maxElements)
			{
				m_listElements.Add(value);
			}
		}
	}
}
