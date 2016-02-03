Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports ESRI.ArcGIS.DataSourcesRaster
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.ADF.CATIDs
Imports System.Runtime.InteropServices

Namespace CustomRasterBuilder
    Interface IThumbnailBuilder
        Inherits IRasterBuilder
#Region "ThumbnailBuilder Members"
        '''<summary>
        ''' Raster Builder to which the ThumbnailBuilder is attached.
        '''</summary>
        Property InnerRasterBuilder() As IRasterBuilder
#End Region
    End Interface

    ''' <summary>
    ''' The Raster Type Factory class that is used to create the Custom 
    ''' Raster Type.
    ''' </summary>
    <Guid("C6629CC4-B301-451a-9481-4D7751E9701C")> _
    <ClassInterface(ClassInterfaceType.None)> _
    <ProgId("CustomRasterBuilder.ThumbnailFactory")> _
    <ComVisible(True)> _
    Public Class ThumbnailFactory
        Implements IRasterTypeFactory
#Region "Private Members"
        Private myRasterTypeNames As IStringArray
        ' List of Raster Types that the factory can create.
        Private myUID As UID
        ' UID for the Thumbnail Raster type.
#End Region

#Region "IRasterTypeFactory Members"

        Public Sub New()
            ' The Raster Type name should follow the pattern 
            ' 'Thumbnail ' follwed by the name of the built-in 
            ' Raster Type to attach the Thumbnail Builder to.            
            myRasterTypeNames = New StrArrayClass()
            myRasterTypeNames.Add("Thumbnail Raster Dataset")
            myRasterTypeNames.Add("Thumbnail QuickBird")

            myUID = New UIDClass()
            myUID.Value = "{C6629CC4-B301-451a-9481-4D7751E9701C}"
        End Sub

        ''' <summary>
        ''' The UID for the factory.
        ''' </summary>
        Public ReadOnly Property CLSID() As UID Implements IRasterTypeFactory.CLSID
            Get
                Return myUID
            End Get
        End Property

        ''' <summary>
        ''' The main function which creates the Raster Type object.
        ''' </summary>
        ''' <param name="RasterTypeName">Name of the Raster Type to create.</param>
        ''' <returns></returns>
        Public Function CreateRasterType(ByVal RasterTypeName As String) As IRasterType Implements IRasterTypeFactory.CreateRasterType
            Try
                Select Case RasterTypeName
                    Case "Thumbnail Raster Dataset"
                        If True Then
                            ' Create a Raster Type Name object.
                            Dim theRasterTypeName As IRasterTypeName = New RasterTypeNameClass()
                            ' Assign the name of the built-in Raster Type to the name object.
                            ' The Name field accepts a path to an .art file as well 
                            ' the name for a built-in Raster Type.
                            theRasterTypeName.Name = RasterTypeName.Replace("Thumbnail ", "")
                            ' Use the Open function from the IName interface to get the Raster Type object.
                            Dim theRasterType As IRasterType = DirectCast(DirectCast(theRasterTypeName, IName).Open(), IRasterType)
                            If theRasterType Is Nothing Then
                                Console.WriteLine("Error:Raster Type not found " & theRasterTypeName.Name)
                                Return Nothing
                            End If

                            ' Create a new ThumbnailBuilder object and set it's InnerRasterBuilder property to 
                            ' the RasterBuilder from the RasterType object. Then set the thumbnail builder to 
                            ' be the RasterBuilder for the RasterType object. This inserts the thumbnail builder in 
                            ' between the RasterType and it's RasterBuilder.

                            ' Create the Thumbnail Builder 
                            Dim thumbnailBuilder As IRasterBuilder = New ThumbnailBuilder()
                            '  Set the InnerRasterBuilder property with current Raster Type's Raster Builder
                            DirectCast(thumbnailBuilder, ThumbnailBuilder).InnerRasterBuilder = theRasterType.RasterBuilder
                            ' Set the Raster Builder of theRasterType to the above created thumbnail builder.
                            theRasterType.RasterBuilder = thumbnailBuilder
                            Dim theName As IName = theRasterType.FullName
                            DirectCast(theName, IRasterTypeName).Name = "Thumbnail Raster Dataset"
                            Return theRasterType
                        End If

                    Case "Thumbnail QuickBird"
                        If True Then
                            ' Create a Raster Type Name object.
                            Dim theRasterTypeName As IRasterTypeName = New RasterTypeNameClass()
                            ' Assign the name of the built-in Raster Type to the name object.
                            ' The Name field accepts a path to an .art file as well 
                            ' the name for a built-in Raster Type.
                            theRasterTypeName.Name = RasterTypeName.Replace("Thumbnail ", "")
                            ' Use the Open function from the IName interface to get the Raster Type object.
                            Dim theRasterType As IRasterType = DirectCast(DirectCast(theRasterTypeName, IName).Open(), IRasterType)
                            If theRasterType Is Nothing Then
                                Console.WriteLine("Error:Raster Type not found " & theRasterTypeName.Name)
                                Return Nothing
                            End If

                            ' Create a new TumbnailBuilder object and set it's InnerRasterBuilder property to 
                            ' the RasterBuilder from the RasterType object. Then set the thumbnail builder to 
                            ' be the RasterBuilder for the RasterType object. This inserts the thumbnail builder in 
                            ' between the RasterType and it's RasterBuilder.

                            ' Create the Thumbnail Builder 
                            Dim thumbnailBuilder As IRasterBuilder = New ThumbnailBuilder()
                            '  Set the InnerRasterBuilder property with current Raster Type's Raster Builder
                            DirectCast(thumbnailBuilder, ThumbnailBuilder).InnerRasterBuilder = theRasterType.RasterBuilder
                            ' Set the Raster Builder of theRasterType to the above created thumbnail builder.
                            theRasterType.RasterBuilder = thumbnailBuilder
                            Dim theName As IName = theRasterType.FullName
                            DirectCast(theName, IRasterTypeName).Name = "Thumbnail QuickBird"
                            Return theRasterType
                        End If
                    Case Else
                        Return Nothing
                End Select
            Catch ex As Exception
                Throw New Exception("Error: Failed to create " & RasterTypeName & ": " & ex.Message)
            End Try
        End Function

        ''' <summary>
        ''' Name of the Raster Type factory.
        ''' </summary>
        Public ReadOnly Property Name() As String Implements IRasterTypeFactory.Name
            Get
                Return "Thumbnail Raster Type Factory"
            End Get
        End Property

        ''' <summary>
        ''' List of Raster Types which the factory can create.
        ''' </summary>
        Public ReadOnly Property RasterTypeNames() As IStringArray Implements IRasterTypeFactory.RasterTypeNames
            Get
                Return myRasterTypeNames
            End Get
        End Property
#End Region

#Region "COM Registration Function(s)"
        <ComRegisterFunction()> _
        Private Shared Sub Reg(ByVal regKey As String)
            ESRI.ArcGIS.ADF.CATIDs.RasterTypeFactory.Register(regKey)
        End Sub

        <ComUnregisterFunction()> _
        Private Shared Sub Unreg(ByVal regKey As String)
            ESRI.ArcGIS.ADF.CATIDs.RasterTypeFactory.Unregister(regKey)
        End Sub
#End Region
    End Class

    '''<summary>
    ''' This class implements the interface IThumbnailBuilder, IPersistVariant.
    '''</summary>
    <Guid("CB37C3B0-5080-4bec-9065-AF036CEBA0F9")> _
    <ClassInterface(ClassInterfaceType.None)> _
    <ProgId("CustomRasterBuilder.ThumbnailBuilder")> _
    <ComVisible(True)> _
    Public Class ThumbnailBuilder
        Implements IThumbnailBuilder
        Implements IPersistVariant
        Implements IRasterBuilderInit
#Region "Private Members"
        Public m_innerRasterBuilder As IRasterBuilder
        ' Inner Raster Builder 
        Private myUID As UID
        ' UID for the Thumbnail Builder.
        ''' <summary>
        ''' The following function creates a field called "ThumbNail" and adds it to the existing
        ''' fields in the mosaic dataset catalog as a blob.
        ''' </summary>
        ''' <param name="myFields">List of fields added to the Mosaic Catalog</param>
        Private Shared Sub AddingNewField(ByVal myFields As IFields)
            Dim pField As IField = New FieldClass()
            ' Create a new field object
            Dim objectIDFieldEditor As IFieldEdit = DirectCast(pField, IFieldEdit)
            ' Set the field editor for this field
            objectIDFieldEditor.Name_2 = "ThumbNail"
            ' Set the name of the field
            objectIDFieldEditor.Type_2 = esriFieldType.esriFieldTypeBlob
            ' Set the type of the field as Blob
            Dim fieldsEditor As IFieldsEdit = DirectCast(myFields, IFieldsEdit)
            ' Add the newly created field to list of existing fields
            fieldsEditor.AddField(pField)
            Return
        End Sub

        ''' <summary>
        ''' Each BuilderItem contains a Function Raster Dataset which is used to create the thumbnail.
        ''' The thumbnail is created by Nearest Neighbor resampling of the Function Raster Dataset. 
        ''' The resampled raster is exported as a byte array and saved Into the IMemoryBlobStreamVariant. 
        ''' The blob is then inserted into the Thumbnail field.
        ''' </summary>
        ''' <param name="mybuilderItem">Item containing the Function Dataset to be added to the Mosaic Dataset</param>
        Private Sub Resampling(ByVal mybuilderItem As IBuilderItem)
            Try
                Dim pFuncRasterDataset As IFunctionRasterDataset = mybuilderItem.Dataset
                ' Get the FunctionRasterDataset from mybuilderItem
                Dim pRasterDataset As IRasterDataset
                pRasterDataset = DirectCast(pFuncRasterDataset, IRasterDataset)
                ' Cast the FunctionRasterDataset to Raster Dataset
                Dim thePropSet As IPropertySet = pFuncRasterDataset.Properties
                ' Get the properties of the raster Dataset
                Dim praster As IRaster = pRasterDataset.CreateDefaultRaster()
                ' Create default raster from the above raster dataset
                praster.ResampleMethod = rstResamplingTypes.RSP_NearestNeighbor
                ' The raster is resampled by RSP_NearestNeighbor
                Dim pRasterProps As IRasterProps = DirectCast(praster, IRasterProps)
                ' Raster properties are used to update the height, width of the raster
                pRasterProps.Height = 256
                pRasterProps.Width = 256

                Dim pConverter As IRasterExporter = New RasterExporterClass()
                ' IRasterExporter object is used to convert the raster to byte array.
                Dim pBytesArr As Byte()
                pBytesArr = pConverter.ExportToBytes(praster, "TIFF")
                ' Convert the resampled Raster to a Byte array 
                Dim memBlobStream As IMemoryBlobStream = New MemoryBlobStream()
                ' Create new IMemoryBlobStream
                Dim varBlobStream As IMemoryBlobStreamVariant = DirectCast(memBlobStream, IMemoryBlobStreamVariant)
                ' Assign to IMemoryBlobStreamVariant
                Dim anObject As Object = pBytesArr
                varBlobStream.ImportFromVariant(anObject)
                ' IMemoryBlobStreamVariant object is assigned the byte array
                ' and saved to the property "ThumbNail"
                thePropSet.SetProperty("ThumbNail", memBlobStream)
            Catch ex As Exception
                Dim myExc As New System.Exception("Error: Failed to Re-Sampled the raster.Thumbnails will not be created." & ex.Message, ex)
                Throw myExc
            End Try
            Return

        End Sub
#End Region

#Region "ThumbnailBuilder Members"
        ''' <summary>
        ''' This property gets and sets the raster type of the Thumbnail Builder.
        ''' </summary>
        Public Property InnerRasterBuilder() As IRasterBuilder Implements IThumbnailBuilder.InnerRasterBuilder
            Get
                Return m_innerRasterBuilder
            End Get
            Set(ByVal value As IRasterBuilder)
                m_innerRasterBuilder = value
            End Set
        End Property

        Public Sub New()
            m_innerRasterBuilder = Nothing

            myUID = New UIDClass()
            myUID.Value = "{" & "CB37C3B0-5080-4bec-9065-AF036CEBA0F9" & "}"
        End Sub
#End Region

#Region "IRasterBuilder Members"
        ''' <summary>
        ''' AuxiliaryFieldAlias property gets and sets the Auxiliary fields Alias
        ''' present in the raster builder
        ''' </summary>
        Public Property AuxiliaryFieldAlias() As ESRI.ArcGIS.esriSystem.IPropertySet Implements IRasterBuilder.AuxiliaryFieldAlias
            Get
                Return m_innerRasterBuilder.AuxiliaryFieldAlias
            End Get
            Set(ByVal value As ESRI.ArcGIS.esriSystem.IPropertySet)
                m_innerRasterBuilder.AuxiliaryFieldAlias = value
            End Set
        End Property

        ''' <summary>
        ''' Flag to specify whether the Raster Builder can build items in place.
        ''' </summary>
        Public ReadOnly Property CanBuildInPlace() As Boolean Implements IRasterBuilder.CanBuildInPlace
            Get
                Return m_innerRasterBuilder.CanBuildInPlace
            End Get
        End Property

        ''' <summary>
        ''' For adding Thumbnails, a new field called "Thumbnail" of type blob is created
        ''' and added to the Auxiliary Fields in raster builder.
        ''' </summary>
        Public Property AuxiliaryFields() As IFields Implements IRasterBuilder.AuxiliaryFields
            Get
                Dim pRasterBuilder As ESRI.ArcGIS.DataSourcesRaster.IRasterBuilder = m_innerRasterBuilder
                Dim myFields As IFields = pRasterBuilder.AuxiliaryFields
                ' Existing Fields present in the Raster Builder. 
                Try
                    Dim count As Integer = myFields.FindField("ThumbNail")
                    ' Check if the Field "ThumbNail" already exists.
                    If count = -1 Then
                        AddingNewField(myFields)
                        ' If not add the new field "ThumbNail"
                        ' Assign the updated Fields back to the raster builder.
                        pRasterBuilder.AuxiliaryFields = myFields
                    End If
                Catch ex As Exception
                    Dim myExc As New System.Exception("Failed to add the field ThumbNail." & ex.Message, ex)
                    Throw myExc
                End Try
                ' Return the updated fields.
                Return m_innerRasterBuilder.AuxiliaryFields
            End Get
            Set(ByVal value As IFields)
                m_innerRasterBuilder.AuxiliaryFields = value
            End Set
        End Property

        ''' <summary>
        ''' Prepare the Raster Type for generating Crawler items
        ''' </summary>
        ''' <param name="pCrawler">Crawler to use to generate the crawler items</param>
        Public Sub BeginConstruction(ByVal pCrawler As IDataSourceCrawler) Implements IRasterBuilder.BeginConstruction
            m_innerRasterBuilder.BeginConstruction(pCrawler)
        End Sub

        ''' <summary>
        ''' Call the build function of the inner Raster Type and generate a thumbnail from the Builder Item created.
        ''' </summary>
        ''' <param name="pItemURI">URI of the Item to be built</param>
        ''' <returns></returns>
        Public Function Build(ByVal pItemURI As IItemURI) As IBuilderItem Implements IRasterBuilder.Build
            Dim pbuilderItem As IBuilderItem = m_innerRasterBuilder.Build(pItemURI)
            ' Generate the IBuilderItem
            Resampling(pbuilderItem)
            ' Generate the Thumbnail from the item.
            Return pbuilderItem
        End Function

        ''' <summary>
        ''' Construct a Unique Resource Identifier (URI)
        ''' for each crawler item
        ''' </summary>
        ''' <param name="crawlerItem">Crawled Item from which the URI is generated</param>
        Public Sub ConstructURIs(ByVal crawlerItem As Object) Implements IRasterBuilder.ConstructURIs
            m_innerRasterBuilder.ConstructURIs(crawlerItem)
        End Sub

        ''' <summary>
        ''' Finish Construction of the URI's
        ''' </summary>
        ''' <returns></returns>
        Public Function EndConstruction() As IItemURIArray Implements IRasterBuilder.EndConstruction
            Return m_innerRasterBuilder.EndConstruction()
        End Function

        ''' <summary>
        ''' Generate the next URI.
        ''' </summary>
        ''' <returns>The URI generated.</returns>
        Public Function GetNextURI() As IItemURI Implements IRasterBuilder.GetNextURI
            Return m_innerRasterBuilder.GetNextURI()
        End Function

        ''' <summary>
        ''' Get the recommended data crawler for the Raster Type based on 
        ''' properties provided by the user.
        ''' </summary>
        ''' <param name="pDataSourceProperties">List of properties provided by the user</param>
        ''' <returns></returns>
        Public Function GetRecommendedCrawler(ByVal pDataSourceProperties As IPropertySet) As IDataSourceCrawler Implements IRasterBuilder.GetRecommendedCrawler
            Return m_innerRasterBuilder.GetRecommendedCrawler(pDataSourceProperties)
        End Function

        ''' <summary>
        ''' Check if the item provided is "stale" or not valid
        ''' </summary>
        ''' <param name="pItemURI">URI for the item to be checked</param>
        ''' <returns></returns>
        Public Function IsStale(ByVal pItemURI As IItemURI) As Boolean Implements IRasterBuilder.IsStale
            Return m_innerRasterBuilder.IsStale(pItemURI)
        End Function

        ''' <summary>
        ''' Properties associated with the Raster Type
        ''' </summary>
        Public Property Properties() As IPropertySet Implements IRasterBuilder.Properties
            Get
                Return m_innerRasterBuilder.Properties
            End Get
            Set(ByVal value As IPropertySet)
                m_innerRasterBuilder.Properties = value
            End Set
        End Property


#End Region

#Region "IPersistVariant Members"
        ''' <summary>
        ''' UID for the object implementing the Persist Variant
        ''' </summary>
        Private ReadOnly Property IPersistVariant_ID() As UID Implements IPersistVariant.ID
            Get
                Return myUID
            End Get
        End Property

        ''' <summary>
        ''' Load the object from the stream provided
        ''' </summary>
        ''' <param name="Stream">Stream that represents the serialized Raster Type</param>
        Private Sub IPersistVariant_Load(ByVal Stream As IVariantStream) Implements IPersistVariant.Load
            Dim name As String = DirectCast(Stream.Read(), String)
            'if (innerRasterBuilder is IPersistVariant)
            '    ((IPersistVariant)innerRasterBuilder).Load(Stream);
            m_innerRasterBuilder = DirectCast(Stream.Read(), IRasterBuilder)
            ' Load the innerRasterBuilder from the stream.
        End Sub

        ''' <summary>
        ''' Same the Raster Type to the stream provided
        ''' </summary>
        ''' <param name="Stream">Stream to serialize the Raster Type into</param>
        Private Sub IPersistVariant_Save(ByVal Stream As IVariantStream) Implements IPersistVariant.Save
            Stream.Write("ThmbnailBuilder")
            'if (innerRasterBuilder is IPersistVariant)
            '    ((IPersistVariant)innerRasterBuilder).Save(Stream);
            Stream.Write(m_innerRasterBuilder)
            ' Save the innerRasterBuilder into the stream.
        End Sub
#End Region

#Region "IRasterBuilderInit Members"
        ''' <summary>
        ''' Default spatial reference for the MD.
        ''' </summary>
        Private Property IRasterBuilderInit_DefaultSpatialReference() As ISpatialReference Implements IRasterBuilderInit.DefaultSpatialReference
            Get
                Return DirectCast(m_innerRasterBuilder, IRasterBuilderInit).DefaultSpatialReference
            End Get
            Set(ByVal value As ISpatialReference)
                DirectCast(m_innerRasterBuilder, IRasterBuilderInit).DefaultSpatialReference = value
            End Set
        End Property

        ''' <summary>
        ''' Parent mosaic dataset for the Raster Builder.
        ''' </summary>
        Private Property IRasterBuilderInit_MosaicDataset() As IMosaicDataset Implements IRasterBuilderInit.MosaicDataset
            Get
                Return DirectCast(m_innerRasterBuilder, IRasterBuilderInit).MosaicDataset
            End Get
            Set(ByVal value As IMosaicDataset)
                DirectCast(m_innerRasterBuilder, IRasterBuilderInit).MosaicDataset = value
            End Set
        End Property

        ''' <summary>
        ''' The raster type operation helper object associated with this raster type.
        ''' </summary>
        Private Property IRasterBuilderInit_RasterTypeOperation() As IRasterTypeOperation Implements IRasterBuilderInit.RasterTypeOperation
            Get
                Return DirectCast(m_innerRasterBuilder, IRasterBuilderInit).RasterTypeOperation
            End Get
            Set(ByVal value As IRasterTypeOperation)
                DirectCast(m_innerRasterBuilder, IRasterBuilderInit).RasterTypeOperation = value
            End Set
        End Property

        ''' <summary>
        ''' Tracker for when cancel is pressed.
        ''' </summary>
        Private Property IRasterBuilderInit_TrackCancel() As ITrackCancel Implements IRasterBuilderInit.TrackCancel
            Get
                Return DirectCast(m_innerRasterBuilder, IRasterBuilderInit).TrackCancel
            End Get
            Set(ByVal value As ITrackCancel)
                DirectCast(m_innerRasterBuilder, IRasterBuilderInit).TrackCancel = value
            End Set
        End Property
#End Region
    End Class
End Namespace


