// Copyright 2011 ESRI
// 
// All rights reserved under the copyright laws of the United States
// and applicable international laws, treaties, and conventions.
// 
// You may freely redistribute and use this sample code, with or
// without modification, provided you include the original copyright
// notice and use restrictions.
// 
// See the use restrictions at <your ArcGIS install location>/DeveloperKit10.1/userestrictions.txt.
// 

#include "stdafx.h"
#include "NameConstants.h"
#include "float.h"  // for FLT_MAX, etc.
#include "math.h"   // for HUGE_VAL
#include "ConnectivitySolver.h"
#include "ConnectivitySymbolizer.h"

// ConnectivitySolver

/////////////////////////////////////////////////////////////////////
// IConnectivitySolver

STDMETHODIMP ConnectivitySolver::get_OutputLines(esriNAOutputLineType* pVal)
{
  if (!pVal)
    return E_POINTER;

  *pVal = m_outputLineType;

  return S_OK;
}

STDMETHODIMP ConnectivitySolver::put_OutputLines(esriNAOutputLineType newVal)
{
  m_outputLineType = newVal;

  m_bPersistDirty = true;

  return S_OK;
}

STDMETHODIMP ConnectivitySolver::get_OutputConnectivity(outputConnectivityType* pVal)
{
  if (!pVal)
    return E_POINTER;

  *pVal = m_outputConnectivityType;

  return S_OK;
}

STDMETHODIMP ConnectivitySolver::put_OutputConnectivity(outputConnectivityType newVal)
{
  m_outputConnectivityType = newVal;

  m_bPersistDirty = true;

  return S_OK;
}

/////////////////////////////////////////////////////////////////////
// INASolver

STDMETHODIMP ConnectivitySolver::get_Name(BSTR* pName)
{
  if (!pName)
    return E_POINTER;

  // This name is locale/language independent and should not be translated. 

  *pName = ::SysAllocString(CS_NAME);

  return S_OK;
}

STDMETHODIMP ConnectivitySolver::get_DisplayName(BSTR* pName)
{
  if (!pName)
    return E_POINTER;

  // This name should be translated and would typically come from
  // a string resource. 

  *pName = ::SysAllocString(CS_DISPLAY_NAME);

  return S_OK;
}

STDMETHODIMP ConnectivitySolver::get_ClassDefinitions(INamedSet** ppDefinitions)
{
  if (!ppDefinitions)
    return E_POINTER;

  *ppDefinitions = 0;

  // get_ClassDefinitions() will return the default NAClasses that this solver uses.
  // We will define a NAClass for "Seed Points", "Barriers", and "Lines".
  // Barriers are just like the other NA Solvers' Barriers.
  // Lines are the sets of connected/disconnected lines that this solver discovers.
  // Lines are akin to the Routes output from a Route Solver.
  // Seed Points are the origins of traversal for this solver.

  ISpatialReferencePtr ipUnkSR(CLSID_UnknownCoordinateSystem);

  HRESULT hr;
  if (FAILED(hr = BuildClassDefinitions(ipUnkSR, ppDefinitions, 0)))
    return AtlReportError(GetObjectCLSID(), _T("Failed to create class definitions."), IID_INASolver, hr);

  return S_OK;
}

STDMETHODIMP ConnectivitySolver::get_CanUseHierarchy(VARIANT_BOOL* pCanUseHierarchy)
{
  if (!pCanUseHierarchy)
    return E_POINTER;

  // This solver does not make use of hierarchies

  *pCanUseHierarchy = VARIANT_FALSE;

  return S_OK;
}

STDMETHODIMP ConnectivitySolver::get_CanAccumulateAttributes(VARIANT_BOOL* pCanAccumulateAttrs)
{
  if (!pCanAccumulateAttrs)
    return E_POINTER;

  // This solver does not support attribute accumulation

  *pCanAccumulateAttrs = VARIANT_FALSE;

  return S_OK;
}

STDMETHODIMP ConnectivitySolver::get_Properties(IPropertySet** ppPropSet)
{
  if (!ppPropSet)
    return E_POINTER;

  // The use of the property set has been deprecated at 9.2.
  // Clients should use the accessors and mutators on the solver interfaces.

  *ppPropSet = 0;

  return E_NOTIMPL;
}

STDMETHODIMP ConnectivitySolver::CreateLayer(INAContext* pContext, INALayer** ppLayer)
{
  if (!ppLayer)
    return E_POINTER;

  *ppLayer = 0;

  // This is an appropriate place to check if the user is licensed to run
  // your solver and fail with "E_NOT_LICENSED" or similar.

  // Create our custom symbolizer and use it to create the NALayer.
  // NOTE: we are assuming here that there is only one symbolizer to
  // consider.  The ArcGIS Network Analyst extension framework and ESRI solvers support the notion
  // that there can be many symbolizers in CATID_NetworkAnalystSymbolizer.
  // One can iterate the objects in this category and call the Applies()
  // method to see if the symbolizer should be used for a particular solver/context.
  // There is also a get_Priority() method that is used to determine which
  // to use if many Apply.

  	INASymbolizerPtr ipNASymbolizer(__uuidof(ConnectivitySymbolizer));

  return ipNASymbolizer->CreateLayer(pContext, ppLayer);
}

STDMETHODIMP ConnectivitySolver::UpdateLayer(INALayer* pLayer, VARIANT_BOOL* pLayerUpdated)
{
  if (!pLayer || !pLayerUpdated)
    return E_POINTER;

  // This method is called after Solve() and gives us a chance to react to the results of
  // the Solve and change the layer. For example, the Service Area solver updates
  // its layer renderers after Solve has been called to adjust for unique values.

  // We will not need to update our layer, since our renderers will remain the same throughout

  *pLayerUpdated = VARIANT_FALSE;

  return S_OK;
}

STDMETHODIMP ConnectivitySolver::Solve(INAContext* pNAContext, IGPMessages* pMessages, ITrackCancel* pTrackCancel, VARIANT_BOOL* pIsPartialSolution)
{
  /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
  // Solve is the method that is called to perform the actual network analysis. The solver component
  // should be in a valid state before this method should be called. For example, within the ArcMap
  // application, ArcGIS Network Analyst extension performs certain validation checks on the solver and its associated output
  // before enabling the Solve command (on the Network Analyst toolbar) for any given solver.
  // This validation includes the following status checks:
  // 1) check that the solver has created a valid context
  // 2) check that the solver has created a valid NALayer
  // 3) check that the solver's associated NAClass feature classes currently have valid cardinalities 
  //    (this last concept is discussed in the BuildClassDefinitions method of this custom solver)

  // Once the solver component has produced a valid state for appropriate processing (such as defined above),
  // this method should be available to call
  // NOTE: for consistency within custom applications, similar validation checks should also be implemented
  // before calling the Solve method on any solver  

  HRESULT hr;

  // Check for null parameter variables (the track cancel variable is typically considered optional)
  if (!pNAContext || !pMessages)
    return E_POINTER;

  // The partialSolution variable is used to indicate to the caller of this method whether or not we were only able to 
  // find a partial solution to the problem. We initialize this variable to false and set it to true only in certain
  // conditional cases (e.g., some stops/points are unreachable, a seed point is unlocated, etc.)
  *pIsPartialSolution = VARIANT_FALSE;

  // NOTE: this is an appropriate place to check if the user is licensed to run
  // your solver and fail with "E_NOT_LICENSED" or similar.

  // Clear the GP messages
  if (FAILED(hr = pMessages->Clear()))
    return hr;

  // Validate the context (i.e., make sure that it is bound to a network dataset)
  INetworkDatasetPtr ipNetworkDataset;
  if (FAILED(hr = pNAContext->get_NetworkDataset(&ipNetworkDataset)))
    return hr;

  if (!ipNetworkDataset)
    return AtlReportError(GetObjectCLSID(), _T("Context does not have a valid network dataset."), IID_INASolver);

  // NOTE: this is also a good place to perform any additional necessary validation, such as
  // synchronizing the attribute names set on your solver with those of the context's network dataset

  // Check for a Step Progressor on the track cancel parameter variable.
  // This can be used to indicate progress and output messages to the client throughout the solve
  CancelTrackerHelper cancelTrackerHelper;
  IStepProgressorPtr ipStepProgressor;
  if (pTrackCancel)
  {
    IProgressorPtr ipProgressor;
    if (FAILED(hr = pTrackCancel->get_Progressor(&ipProgressor)))
      return hr;

    ipStepProgressor = ipProgressor;

    // We use the cancel tracker helper object to disassociate and reassociate the cancel tracker from the progressor
    // during and after the Solve, respectively. This prevents calls to ITrackCancel::Continue from stepping our progressor
    cancelTrackerHelper.ManageTrackCancel(pTrackCancel);
  }

  /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
  // Reset each NAClass to an appropriate state (as necessary) before proceeding
  // This is typically done in order to:
  // 1) remove any features that were previously created in our output NAClasses from previous solves
  // 2) check the input NAClasses for output fields and reset these field values as necessary
  // In our case, we do not have any input NAClasses with output fields, and we only have one output feature class, 
  // so we can simply clear any features currently present in the "LineData" NAClass. 
  // NOTE: if you have multiple input/output NAClasses to clean up, you can simply loop through all NAClasses,
  // get their NAClassDefinition, check whether or not each is an input/output class, and reset it accordingly
  INamedSetPtr ipNAClasses;
  if (FAILED(hr = pNAContext->get_NAClasses(&ipNAClasses)))
    return hr;

  IUnknownPtr ipUnk;
  if (FAILED(hr = ipNAClasses->get_ItemByName(CComBSTR(CS_LINES_NAME), &ipUnk)))
    return hr;

  INAClassPtr ipLinesNAClass(ipUnk);
  if (FAILED(hr = ipLinesNAClass->DeleteAllRows()))   // remove any features that might have been created on previous solves
    return hr;

  ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
  // Setup the Network Forward Star for traversal
  // Create a Forward Star object from the INetworkQuery interface of the network dataset
  INetworkQueryPtr ipNetworkQuery(ipNetworkDataset);
  INetworkForwardStarPtr ipNetworkForwardStar;
  if (FAILED(hr = ipNetworkQuery->CreateForwardStar(&ipNetworkForwardStar)))
    return hr;

  // QI the Forward Star to INetworkForwardStarEx
  // This interface can be used to setup necessary traversal constraints on the Forward Star before proceeding
  // This typically includes:
  // 1) setting the traversal direction (through INetworkForwardStarSetup::IsForwardTraversal)
  // 2) setting any restrictions on the forward star (e.g., oneway restrictions, restricted turns, etc.)
  // 3) setting the U-turn policy (through INetworkForwardStarSetup::Backtrack)
  // 4) setting the hierarchy (through INetworkForwardStarSetup::HierarchyAttribute)
  // 5) setting up traversable/non-traversable elements (in our case, we will be setting up barriers as non-traversable)
  INetworkForwardStarExPtr ipNetworkForwardStarEx(ipNetworkForwardStar);

  // Get the "Barriers" NAClass table (we need the NALocation objects from this NAClass to push barriers into the Forward Star)
  ITablePtr ipBarriersTable;
  if (FAILED(hr = GetNAClassTable(pNAContext, CComBSTR(CS_BARRIERS_NAME), &ipBarriersTable)))
    return hr;

  // Load the barriers
  if (FAILED(hr = LoadBarriers(ipBarriersTable, ipNetworkQuery, ipNetworkForwardStarEx)))
    return hr;

  // Create a Forward Star Adjacencies object (we need this object to hold traversal queries carried out on the Forward Star)
  INetworkForwardStarAdjacenciesPtr ipNetworkForwardStarAdjacencies;
  if (FAILED(hr = ipNetworkQuery->CreateForwardStarAdjacencies(&ipNetworkForwardStarAdjacencies)))
    return hr;

  /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
  // Begin traversal
  // Determine the maximum number of edge elements in the network dataset and initialize data structures
  long maxEdgeEID, totalEdgeEIDs, totalConnectedEdgeEIDs, totalDisconnectedEdgeEIDs;
  if (FAILED(hr = ipNetworkQuery->get_MaxEID(esriNETEdge, &maxEdgeEID)))
    return hr;

  if (FAILED(hr = ipNetworkQuery->get_ElementCount(esriNETEdge, &totalEdgeEIDs)))
    return hr;

  // We must divide this value in half because get_ElementCount returns two counts for each unique edge EID: one for each direction
  // Since this is an undirected search, we are not concerned with edge direction
  totalEdgeEIDs /= 2;

  // Edge connectivity bitmap
  connectivity_bitmap edgeEIDIsConnected;
  edgeEIDIsConnected.resize(maxEdgeEID + 1); // initialize bitmap size (all are automatically set to false)

  // Stack for holding junction EIDs
  junction_stack  junctionStack;

  // Hashtable for holding segmentRecords
  segment_hash  segmentRecordHashTable; 

  // Get the "Seed Points" NAClass table (we need the NALocation objects from this NAClass as the starting points for Forward Star
  // traversals)
  ITablePtr ipSeedPointsTable;
  if (FAILED(hr = GetNAClassTable(pNAContext, CComBSTR(CS_SEED_POINTS_NAME), &ipSeedPointsTable)))
    return hr;

  // Setup our progressor based on the number of seed points
  if (ipStepProgressor)
  {
    long numberOfSeedPoints;
    if (FAILED(hr = ipSeedPointsTable->RowCount(0, &numberOfSeedPoints)))
      return hr;
    
    // Step progressor range = 0 through numberOfSeedPoints
    if (FAILED(hr = ipStepProgressor->put_MinRange(0)))                       // 0 through...
      return hr;
    if (FAILED(hr = ipStepProgressor->put_MaxRange(numberOfSeedPoints)))      // numberOfSeedPoints...
      return hr;
    if (FAILED(hr = ipStepProgressor->put_StepValue(1)))                      // incremented in step intervals of 1...
      return hr;
    if (FAILED(hr = ipStepProgressor->put_Position(0)))                       // and starting at step 0
      return hr;

    // Show the progressor
    if (FAILED(hr = ipStepProgressor->Show()))
      return hr;
  }

  // Get a cursor on the seed points table to loop through each row
  ICursorPtr ipCursor;
  if (FAILED(hr = ipSeedPointsTable->Search(0, VARIANT_TRUE, &ipCursor)))
    return hr;

  // Create variables for looping through the cursor and traversing the network
  IRowPtr ipRow;
  INALocationObjectPtr ipNALocationObject;
  INALocationPtr ipNALocation(CLSID_NALocation);
  IEnumNetworkElementPtr ipEnumNetworkElement;
  
  INetworkEdgePtr ipCurrentEdge;
  INetworkJunctionPtr ipCurrentJunction;

  INetworkElementPtr ipElement;
  ipNetworkQuery->CreateNetworkElement(esriNETEdge, &ipElement);
  ipCurrentEdge = ipElement;
  ipNetworkQuery->CreateNetworkElement(esriNETJunction, &ipElement);
  ipCurrentJunction = ipElement;
  
  long sourceID, sourceOID, currentEdgeEID, currentJunctionEID, adjacentEdgeCount;
  double posAlong, fromPosition, toPosition;
  esriNetworkElementType elementType;

  // Setup a message on our step progressor indicating that we are traversing the network
  if (ipStepProgressor)
    ipStepProgressor->put_Message(CComBSTR(L"Computing connectivity from seed point(s)")); // add more specific information here if appropriate

  // This begins our main network traversal loop
  // Loop through the cursor getting the NALocation of each NALocationObject in the seed points table,  
  // and traverse the network from each seed NALocation, setting flags in our connectivity bitmap as appropriate
  totalConnectedEdgeEIDs = 0;
  VARIANT_BOOL keepGoing, isLocated, isRestricted;
  while (ipCursor->NextRow(&ipRow) == S_OK)
  {
    ipNALocationObject = ipRow;
    if (!ipNALocationObject) // we only want valid NALocationObjects
    {
      // If this seed point is an invalid NALocationObject, we will only be able to find a partial solution
      *pIsPartialSolution = VARIANT_TRUE;
      continue;
    }

    if (FAILED(hr = ipNALocationObject->QueryNALocation(ipNALocation)))
      return hr;

    // Once we have the NALocation, we need to check if it is actually located within the network dataset
    isLocated = VARIANT_FALSE;
    if (ipNALocation)
    {
      if (FAILED(hr = ipNALocation->get_IsLocated(&isLocated)))
        return hr;
    }

    // We are only concerned with located seed point NALocations
    if (!isLocated)
    {
      // If this seed point is unlocated, we will only be able to find a partial solution
      *pIsPartialSolution = VARIANT_TRUE;
    }
    else
    {
      // Get the SourceID for the NALocation
      if (FAILED(hr = ipNALocation->get_SourceID(&sourceID)))
        return hr;

      // Get the SourceOID for the NALocation
      if (FAILED(hr = ipNALocation->get_SourceOID(&sourceOID)))
        return hr;

      // Get the PosAlong for the NALocation
      if (FAILED(hr = ipNALocation->get_SourcePosition(&posAlong)))
        return hr;

      // Once we have a located NALocation, we query the network to obtain its associated network elements
      if (FAILED(hr = ipNetworkQuery->get_ElementsByOID(sourceID, sourceOID, &ipEnumNetworkElement)))
        return hr;

      // We must loop through the returned elements, looking for an appropriate starting point
      ipEnumNetworkElement->Reset();
      while (ipEnumNetworkElement->Next(&ipElement) == S_OK)
      {
        // We must first check that the network element is traversable
        // We can only begin a traversal from a traversable element
        if (FAILED(hr = ipNetworkForwardStarEx->get_IsRestricted(ipElement, &isRestricted)))
          return hr;

        if (isRestricted)
          continue;

        // We must then check the returned element type
        ipElement->get_ElementType(&elementType);

        // If the element is a junction, then it is the starting point of traversal 
        // We simply add its EID to the stack and break out of the enumerating loop
        if (elementType == esriNETJunction)
        {
          if (FAILED(hr = ipElement->get_EID(&currentJunctionEID)))
            return hr;
          
          junctionStack.push(currentJunctionEID);

          break;  // exit the containing while loop
        }

        // If the element is an edge, then we must check the fromPosition and toPosition to be certain it holds an appropriate starting point 
        if (elementType == esriNETEdge)
        {
          INetworkEdgePtr ipEdge(ipElement);
          if (FAILED(hr = ipEdge->QueryPositions(&fromPosition, &toPosition)))
            return hr;

          if (fromPosition <= posAlong && posAlong <= toPosition)
          {
            // Our NALocation lies along this edge element
            // We will start our traversal from the FromJunction of this edge
            if (FAILED(hr = ipEdge->QueryJunctions(ipCurrentJunction, 0)))
              return hr;

            // We simply add its EID to the stack and break out of the enumerating loop
            if (FAILED(hr = ipCurrentJunction->get_EID(&currentJunctionEID)))
              return hr;
            
            junctionStack.push(currentJunctionEID);

            break;  // exit the containing while loop
          }
        }
      }

      // Continue traversing the network while the stack has remaining junction EIDs in it
      while (junctionStack.size() > 0)
      {
        // Remove the next junction EID from the top of the stack
        currentJunctionEID = junctionStack.top();
        junctionStack.pop();

        // Query for this junction using its EID
        if (FAILED(hr = ipNetworkForwardStarEx->QueryJunction(currentJunctionEID, ipCurrentJunction)))
          return hr;

        // Query adjacencies from the current junction
        if (FAILED(hr = ipNetworkForwardStarEx->QueryAdjacencies(ipCurrentJunction, 0, 0, ipNetworkForwardStarAdjacencies)))
          return hr;
   
        // Get the adjacent edge count
        if (FAILED(hr = ipNetworkForwardStarAdjacencies->get_Count(&adjacentEdgeCount)))
          return hr;

        // Loop through all adjacent edges setting their connectivity to true (if they haven't already been traversed)
        for (long i = 0; i < adjacentEdgeCount; i++)
        {
          if (FAILED(hr = ipNetworkForwardStarAdjacencies->QueryEdge(i, ipCurrentEdge, &fromPosition, &toPosition)))
            return hr;

          if (FAILED(hr = ipCurrentEdge->get_EID(&currentEdgeEID)))
            return hr;

          // Check to see if we have traversed this edge before
          if (!edgeEIDIsConnected[currentEdgeEID])
          {
            // If not, set its connectivity to true
            edgeEIDIsConnected[currentEdgeEID] = true;

            // Add this edge element's ToJunction to the junctionStack for further traversal
            if (FAILED(hr = ipCurrentEdge->QueryJunctions(0, ipCurrentJunction)))
              return hr;  

            if (FAILED(hr = ipCurrentJunction->get_EID(&currentJunctionEID)))
              return hr;

            junctionStack.push(currentJunctionEID);

            // Increment the total number of connected edges
            totalConnectedEdgeEIDs++;
          }
        }
      }
    }

    // Step the progressor before continuing to the next seed point
    if (ipStepProgressor)
      ipStepProgressor->Step();

    // Check to see if the user wishes to continue or cancel the solve (i.e., check whether or not the user has hit the ESC key to stop processing)
    if (pTrackCancel)
    {
      if (FAILED(hr = pTrackCancel->Continue(&keepGoing)))
        return hr;
      if (keepGoing == VARIANT_FALSE)
      { 
        // The user wishes to cancel the solve
        // The cancel tracker helper will automatically hide the progressor and reassociate it with the track cancel object
        // (this is done implicitly from its deconstructor)
        return E_ABORT;
      }
    }
  }

  /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
  // Write output
 
  // Now that we have completed our traversal of the network from the seed points, we must output the connected/disconnected edges
  // to the "LineData" NAClass

  // We already know the total number of connected edges, so we must figure out the remaining
  // number of disconnected edges, if any
  totalDisconnectedEdgeEIDs = totalEdgeEIDs - totalConnectedEdgeEIDs;

  // Check to see what kind and how many features we will be outputting to the Lines NAClass
  long numberOfOutputSteps = (m_outputConnectivityType == outputDisconnectedLines) ? totalDisconnectedEdgeEIDs : totalConnectedEdgeEIDs;

  // Check to see if we actually have any features to output
  if(numberOfOutputSteps == 0)
  {
    // Since we have nothing to output, we report the connectivity results to the user and return
    if(m_outputConnectivityType == outputDisconnectedLines)
      pMessages->AddMessage(CComBSTR(L"The network is fully connected to the seed point(s)."));
    else
      pMessages->AddMessage(CComBSTR(L"The network is fully disconnected from the seed point(s)."));

    // The cancel tracker helper will automatically hide the progressor and reassociate it with the track cancel object
    // (this is done implicitly from its deconstructor)

    return S_OK;
  }

  // If we reach this point, we have some connected/disconnected features to output to the Lines NAClass
  // Reset the progressor based on the number of features that we must output
  if (ipStepProgressor)
  {
    // Step progressor range = 0 through numberOfOutputSteps
    if (FAILED(hr = ipStepProgressor->put_MinRange(0)))                       
      return hr;
    if (FAILED(hr = ipStepProgressor->put_MaxRange(numberOfOutputSteps)))     
      return hr;
    if (FAILED(hr = ipStepProgressor->put_StepValue(1)))                      
      return hr;
    if (FAILED(hr = ipStepProgressor->put_Position(0)))                       
      return hr;
  }

  // Create an output type check to determine which edges to output based on their connectivity value within the bitmap
  bool outputTypeCheck = (m_outputConnectivityType == outputDisconnectedLines) ? false : true;

  // Get the "LineData" NAClass feature class
  IFeatureClassPtr ipLinesFC(ipLinesNAClass);

  // Create an insert cursor and feature buffer from the "LineData" feature class to be used to write connected/disconnected edges
  IFeatureCursorPtr ipFeatureCursor;
  if (FAILED(hr = ipLinesFC->Insert(VARIANT_TRUE, &ipFeatureCursor)))
    return hr;

  IFeatureBufferPtr ipFeatureBuffer;
  if (FAILED(hr = ipLinesFC->CreateFeatureBuffer(&ipFeatureBuffer)))
    return hr;

  // Query for the appropriate field index values in the "LineData" feature class
  long sourceIDFieldIndex, sourceOIDFieldIndex, fromPositionFieldIndex, toPositionFieldIndex;
  if (FAILED(hr = ipLinesFC->FindField(CComBSTR(CS_FIELD_SOURCE_ID), &sourceIDFieldIndex)))
    return hr;
  if (FAILED(hr = ipLinesFC->FindField(CComBSTR(CS_FIELD_SOURCE_OID), &sourceOIDFieldIndex)))
    return hr;
  if (FAILED(hr = ipLinesFC->FindField(CComBSTR(CS_FIELD_FROM_POS), &fromPositionFieldIndex)))
    return hr;
  if (FAILED(hr = ipLinesFC->FindField(CComBSTR(CS_FIELD_TO_POS), &toPositionFieldIndex)))
    return hr;

  // Setup a message on our step progressor indicating that we are outputting connected/disconnected feature information
  if (ipStepProgressor)
    ipStepProgressor->put_Message(CComBSTR(L"Writing output features")); // add more specific information here if appropriate

  long sourceCount;
  if (FAILED(hr = ipNetworkDataset->get_SourceCount(&sourceCount)))
    return hr;

  // Loop through each network source checking for edge sources
  INetworkSourcePtr ipNetworkSource;
  IFeatureClassPtr ipNetworkSourceFC;
  IFeatureClassContainerPtr ipFeatureClassContainer(ipNetworkDataset);
  CComBSTR sourceName;
  CComVariant featureID(0);
  long lineFeatureCount = 0;
  for (long i = 0; i < sourceCount; i++)
  {
    // Get the network source at this index
    if (FAILED(hr = ipNetworkDataset->get_Source(i, &ipNetworkSource)))
      return hr;

    // Get the element type of this network source
    if (FAILED(hr = ipNetworkSource->get_ElementType(&elementType)))
      return hr;

    // If the network source is not an edge source, then continue
    if (elementType != esriNETEdge)
      continue;

    // Get the SourceID for this source
    if (FAILED(hr = ipNetworkSource->get_ID(&sourceID)))
      return hr;

    // Get the source feature class
    if (FAILED(hr = ipNetworkSource->get_Name(&sourceName)))
      return hr;

    if (FAILED(hr = ipFeatureClassContainer->get_ClassByName(sourceName, &ipNetworkSourceFC)))
      return hr;
    
    if (!ipNetworkSourceFC) // Some SDC sources do not represent valid feature classes. We must check for this and continue, as appropriate
      continue;

    // Query for an enumerator of all edge elements from this source
    if (FAILED(hr = ipNetworkQuery->get_ElementsForSource(sourceID, &ipEnumNetworkElement)))
      return hr;

    // Loop through all returned edge elements, checking whether to use them for connected/disconnected output
    while (ipEnumNetworkElement->Next(&ipElement) == S_OK)
    {
      if (FAILED(hr = ipElement->get_EID(&currentEdgeEID)))
        return hr;

      // Check edge element for output as connected/disconnected
      if (edgeEIDIsConnected[currentEdgeEID] == outputTypeCheck)
      {
        // We will use this edge for feature output to the "LineData" NAClass
        ipCurrentEdge = ipElement;

        // Get the SourceOID of the edge
        if (FAILED(hr = ipCurrentEdge->get_OID(&sourceOID)))
          return hr;

        // Get the from/to positions of the edge
        if (FAILED(hr = ipCurrentEdge->QueryPositions(&fromPosition, &toPosition)))
          return hr;

        // Check for whether or not we are outputting geometry here
        if (m_outputLineType == esriNAOutputLineTrueShape)
        {
          // See if we have already entered this OID in the hash table; if not, increment the line feature count
          if(segmentRecordHashTable.count(sourceOID) == 0)
            lineFeatureCount++;

          // Create a new segment record and store it as an entry in the hash table using the SourceOID as its key value
          segmentRecord currentSegmentRecord(fromPosition, toPosition);
          segmentRecordHashTable.insert(segment_entry(sourceOID, currentSegmentRecord));
          
          // Check the number of unique OID values in the hash table
          // Every c_featureRetrievalInterval OIDs, we will grab features from the source feature class and create new "LineData" features
          // This is more efficient than sequentially calling GetFeature(...) for each individual feature
          if (lineFeatureCount == c_featureRetrievalInterval)
          {
            if (FAILED(hr = OutputLineFeatures(sourceID, lineFeatureCount, segmentRecordHashTable, ipNetworkSourceFC, ipFeatureCursor, ipFeatureBuffer)))
              return hr;

            // Reset the cumulative line feature count to zero
            lineFeatureCount = 0;
          }
        }
        else
        {
          // Otherwise, we can simply store the values on the feature buffer and insert the feature into the insert cursor
          if (FAILED(hr = ipFeatureBuffer->put_Value(sourceIDFieldIndex, CComVariant(sourceID))))
            return hr;
          if (FAILED(hr = ipFeatureBuffer->put_Value(sourceOIDFieldIndex, CComVariant(sourceOID))))
            return hr;
          if (FAILED(hr = ipFeatureBuffer->put_Value(fromPositionFieldIndex, CComVariant(fromPosition))))
            return hr;
          if (FAILED(hr = ipFeatureBuffer->put_Value(toPositionFieldIndex, CComVariant(toPosition))))
            return hr;

          // Insert the feature buffer in the insert cursor
          if (FAILED(hr = ipFeatureCursor->InsertFeature(ipFeatureBuffer, &featureID)))
            return hr;
        }

        // Step the progressor before continuing
        if (ipStepProgressor)
          ipStepProgressor->Step();
      }

      // Check to see if the user wishes to continue or cancel the solve (i.e., check whether or not the user has hit the ESC key to stop processing)
      if (pTrackCancel)
      {
        if (FAILED(hr = pTrackCancel->Continue(&keepGoing)))
          return hr;
        if (keepGoing == VARIANT_FALSE)
        { 
          // The user wishes to cancel the solve
          // The cancel tracker helper will automatically hide the progressor and reassociate it with the track cancel object
          // (this is done implicitly from its deconstructor)
          return E_ABORT;
        }
      }
    }

    // Make sure there are no remaining features to be output to the NAClass
    if (m_outputLineType == esriNAOutputLineTrueShape && lineFeatureCount > 0)
    {
      if (FAILED(hr = OutputLineFeatures(sourceID, lineFeatureCount, segmentRecordHashTable, ipNetworkSourceFC, ipFeatureCursor, ipFeatureBuffer)))
        return hr;

      // Reset the cumulative line feature count to zero
      lineFeatureCount = 0;
    }
  }

  CString formatString;
  CString msgString;

  // Report the connectivity results to the user and return
  if (m_outputConnectivityType == outputDisconnectedLines)
  {
    formatString = _T("The network has %d edges disconnected from the seed point(s).");
    msgString.Format(formatString, numberOfOutputSteps);
  }
  else
  {
    formatString = _T("The network has %d edges connected to the seed point(s).");
    msgString.Format(formatString, numberOfOutputSteps);
  }  
  pMessages->AddMessage(CComBSTR(msgString));

  // The cancel tracker helper will automatically hide the progressor and reassociate it with the track cancel object
  // (this is done implicitly from its deconstructor)

  return S_OK;
}

STDMETHODIMP ConnectivitySolver::CreateContext(IDENetworkDataset* pNetwork, BSTR contextName, INAContext** ppNAContext)
{
  if (!pNetwork || !ppNAContext)
    return E_POINTER;

  if (!contextName || !::wcslen(contextName))
    return E_INVALIDARG;

  *ppNAContext = 0;

  HRESULT hr;

  // CreateContext() is called by the command that creates the solver and
  // initializes it.  Below we'll:
  //
  // - create the class definitions
  // - set up defaults
  //
  // After this method is called, this class should be prepared to have
  // its solve method called.

  // This is an appropriate place to check if the user is licensed to run
  // your solver and fail with "E_NOT_LICENSED" or similar.

  // Get the NDS SpatialRef, that will be the same for the context as well as all spatial NAClasses
  IDEGeoDatasetPtr ipDEGeoDataset(pNetwork);
  if (!ipDEGeoDataset)
    return E_INVALIDARG;

  ISpatialReferencePtr ipNAContextSR;

  if (FAILED(hr = ipDEGeoDataset->get_SpatialReference(&ipNAContextSR)))
    return hr;

  IUnknownPtr           ipUnknown;
  INamedSetPtr          ipNAClassDefinitions;
  INAClassDefinitionPtr ipSeedPointsClassDef;
  INAClassDefinitionPtr ipBarriersClassDef;
  INAClassDefinitionPtr ipLinesClassDef;

  // Build the class definitions
  if (FAILED(hr = BuildClassDefinitions(ipNAContextSR, &ipNAClassDefinitions, pNetwork)))
    return hr;

  ipNAClassDefinitions->get_ItemByName(CComBSTR(CS_SEED_POINTS_NAME), &ipUnknown);
  ipSeedPointsClassDef = ipUnknown;

  ipNAClassDefinitions->get_ItemByName(CComBSTR(CS_BARRIERS_NAME), &ipUnknown);
  ipBarriersClassDef = ipUnknown;

  ipNAClassDefinitions->get_ItemByName(CComBSTR(CS_LINES_NAME), &ipUnknown);
  ipLinesClassDef = ipUnknown;

  // Create a context and initialize it
  INAContextPtr     ipNAContext(CLSID_NAContext);
  INAContextEditPtr ipNAContextEdit(ipNAContext);

  if (FAILED(hr = ipNAContextEdit->Initialize(contextName, pNetwork)))
    return hr;
  ipNAContextEdit->putref_Solver(static_cast<INASolver*>(this));

  // Create NAClasses for each of our class definitions
  INamedSetPtr ipNAClasses;
  INAClassPtr  ipNAClass;

  ipNAContext->get_NAClasses(&ipNAClasses);

  if (FAILED(hr = ipNAContextEdit->CreateAnalysisClass(ipSeedPointsClassDef, &ipNAClass)))
    return hr;

  ipNAClasses->Add(CComBSTR(CS_SEED_POINTS_NAME), (IUnknownPtr)ipNAClass);

  if (FAILED(hr = ipNAContextEdit->CreateAnalysisClass(ipBarriersClassDef, &ipNAClass)))
    return hr;

  ipNAClasses->Add(CComBSTR(CS_BARRIERS_NAME), (IUnknownPtr)ipNAClass);

  if (FAILED(hr = ipNAContextEdit->CreateAnalysisClass(ipLinesClassDef, &ipNAClass)))
    return hr;

  ipNAClasses->Add(CComBSTR(CS_LINES_NAME), (IUnknownPtr)ipNAClass);

  // NOTE: this is an appropriate place to set up any hierarchy defaults if your
  // solver supports using a hierarchy attribute (this solver does not). This is
  // also an appropriate place to set the default impedance attribute if you
  // had one.

  // Set up our solver defaults
  m_outputConnectivityType = outputDisconnectedLines;
  m_outputLineType = esriNAOutputLineTrueShape;

  // Agents setup
  // NOTE: this is an appropriate place to find and attach any agents used by this solver.
  // For example, the route solver would attach the directions agent.

  // Initialize the default field mappings
  // NOTE: this is an appropriate place to set up any default field mappings to be used

  // Return the context once it has been fully created and initialized
  *ppNAContext = ipNAContext;
  ipNAContext->AddRef();

  return S_OK;
}

STDMETHODIMP ConnectivitySolver::UpdateContext(INAContext* pNAContext, IDENetworkDataset* pNetwork, IGPMessages* pMessages)
{
  // UpdateContext() is a method used to update the context based on any changes that may have been made to the
  // solver settings. This typically includes changes to the set of accumulation attribute names, etc., which can
  // be set as fields in the context's NAClass schemas

  // We will not need to update our context, since we do not support any changes necessary to use this method

  return S_OK;
}

STDMETHODIMP ConnectivitySolver::Bind(INAContext* pContext, IDENetworkDataset* pNetwork, IGPMessages* pMessages)
{
  // Bind() is a method used to re-associate the solver with a given network dataset and its schema. Calling Bind()
  // on the solver re-attaches the solver to the NAContext based on the current network dataset settings.
  // This is typically used to update the solver and its context based on changes in the network dataset's available
  // restrictions, hierarchy attributes, cost attributes, etc.

  // We will not need to bind our solver, since we do not support any changes necessary to use this method

  return S_OK;  
}

/////////////////////////////////////////////////////////////////////
// INASolverSettings

STDMETHODIMP ConnectivitySolver::get_AccumulateAttributeNames(IStringArray** ppAttributeNames)
{
  return E_NOTIMPL; // This solver does not support attribute accumulation
}

STDMETHODIMP ConnectivitySolver::putref_AccumulateAttributeNames(IStringArray* pAttributeNames)
{
  return E_NOTIMPL; // This solver does not support attribute accumulation
}

STDMETHODIMP ConnectivitySolver::put_ImpedanceAttributeName(BSTR attributeName)
{
  return E_NOTIMPL; // This solver does not support impedance attributes
}

STDMETHODIMP ConnectivitySolver::get_ImpedanceAttributeName(BSTR* pAttributeName)
{
  return E_NOTIMPL;  // This solver does not support impedance attributes
}

STDMETHODIMP ConnectivitySolver::put_IgnoreInvalidLocations(VARIANT_BOOL ignoreInvalidLocations)
{
  return E_NOTIMPL;
}
STDMETHODIMP ConnectivitySolver::get_IgnoreInvalidLocations(VARIANT_BOOL* pIgnoreInvalidLocations)
{
  return E_NOTIMPL;
}

STDMETHODIMP ConnectivitySolver::get_RestrictionAttributeNames(IStringArray** ppAttributeName)
{
  return E_NOTIMPL; // This solver does not support restriction attributes
}

STDMETHODIMP ConnectivitySolver::putref_RestrictionAttributeNames(IStringArray* pAttributeName)
{
  return E_NOTIMPL; // This solver does not support restriction attributes
}

STDMETHODIMP ConnectivitySolver::put_RestrictUTurns(esriNetworkForwardStarBacktrack backtrack)
{
  return E_NOTIMPL; // This solver does not support U-turn policies
}

STDMETHODIMP ConnectivitySolver::get_RestrictUTurns(esriNetworkForwardStarBacktrack* pBacktrack)
{
  return E_NOTIMPL; // This solver does not support U-turn policies
}

STDMETHODIMP ConnectivitySolver::put_UseHierarchy(VARIANT_BOOL useHierarchy)
{
  return E_NOTIMPL; // This solver does not support hierarchy attributes
}

STDMETHODIMP ConnectivitySolver::get_UseHierarchy(VARIANT_BOOL* pUseHierarchy)
{
  if (!pUseHierarchy)
    return E_POINTER;

  // This solver does not support hierarchy attributes

  *pUseHierarchy = VARIANT_FALSE;

  return S_OK;
}

STDMETHODIMP ConnectivitySolver::put_HierarchyAttributeName(BSTR attributeName)
{
  return E_NOTIMPL; // This solver does not support hierarchy attributes
}

STDMETHODIMP ConnectivitySolver::get_HierarchyAttributeName(BSTR* pattributeName)
{
  return E_NOTIMPL; // This solver does not support hierarchy attributes
}

STDMETHODIMP ConnectivitySolver::put_HierarchyLevelCount(long Count)
{
  return E_NOTIMPL; // This solver does not support hierarchy attributes
}

STDMETHODIMP ConnectivitySolver::get_HierarchyLevelCount(long* pCount)
{
  return E_NOTIMPL; // This solver does not support hierarchy attributes
}

STDMETHODIMP ConnectivitySolver::put_MaxValueForHierarchy(long level, long value)
{
  return E_NOTIMPL; // This solver does not support hierarchy attributes
}
STDMETHODIMP ConnectivitySolver::get_MaxValueForHierarchy(long level, long* pValue)
{
  return E_NOTIMPL; // This solver does not support hierarchy attributes
}

STDMETHODIMP ConnectivitySolver::put_NumTransitionToHierarchy(long toLevel, long value)
{
  return E_NOTIMPL; // This solver does not support hierarchy attributes
}

STDMETHODIMP ConnectivitySolver::get_NumTransitionToHierarchy(long toLevel, long* pValue)
{
  return E_NOTIMPL; // This solver does not support hierarchy attributes
}

/////////////////////////////////////////////////////////////////////
// IPersistStream

STDMETHODIMP ConnectivitySolver::IsDirty()
{
  return (m_bPersistDirty ? S_OK : S_FALSE);
}

STDMETHODIMP ConnectivitySolver::Load(IStream* pStm)
{
  if (!pStm)
    return E_POINTER;
  
  ULONG     numBytes;
  HRESULT   hr;

  // We need to check the saved version number
  long savedVersion;
  if (FAILED(hr = pStm->Read(&savedVersion, sizeof(savedVersion), &numBytes)))
    return hr;

  // We only support versions less than or equal to the current c_version
  if (savedVersion > c_version || savedVersion <= 0)
    return E_FAIL;

  // We need to read our persisted solver settings
  if (FAILED(hr = pStm->Read(&m_outputConnectivityType, sizeof(m_outputConnectivityType), &numBytes)))
    return hr;

  if (FAILED(hr = pStm->Read(&m_outputLineType, sizeof(m_outputLineType), &numBytes)))
    return hr;

  m_bPersistDirty = false;

  return S_OK;
}

STDMETHODIMP ConnectivitySolver::Save(IStream* pStm, BOOL fClearDirty)
{
  if (fClearDirty)
    m_bPersistDirty = false;

  ULONG     numBytes;
  HRESULT   hr;

  // We need to persist the c_version number
  if (FAILED(hr = pStm->Write(&c_version, sizeof(c_version), &numBytes)))
    return hr;

  // We need to persist our solver settings
  if (FAILED(hr = pStm->Write(&m_outputConnectivityType, sizeof(m_outputConnectivityType), &numBytes)))
    return hr;

  if (FAILED(hr = pStm->Write(&m_outputLineType, sizeof(m_outputLineType), &numBytes)))
    return hr;

  return S_OK;
}

STDMETHODIMP ConnectivitySolver::GetSizeMax(_ULARGE_INTEGER* pCbSize)
{
  if (!pCbSize)
    return E_POINTER;

  pCbSize->HighPart = 0;
  pCbSize->LowPart = sizeof(short);

  return S_OK;
}

STDMETHODIMP ConnectivitySolver::GetClassID(CLSID *pClassID)
{
  if (!pClassID)
    return E_POINTER;

  *pClassID = __uuidof(ConnectivitySolver); 

  return S_OK;
}

//////////////////////////////////////////
// private methods

HRESULT ConnectivitySolver::BuildClassDefinitions(ISpatialReference* pSpatialRef, INamedSet** ppDefinitions, IDENetworkDataset* pDENDS)
{
  if (!pSpatialRef || !ppDefinitions)
    return E_POINTER;

  // This function creates the class definitions for the connectivity solver.
  // Recall, class definitions are in-memory feature classes that store the
  // inputs and outputs for a solver.  This solver's class definitions are:

  // Seed Points (input)            Seed points determine the start locations
  // - OID                          of the search
  // - Shape
  // - Name 
  // - (NALocation fields)

  // Barriers (input)               Barriers restrict network edges from being
  // - OID                          traversable
  // - Shape
  // - Name
  // - (NALocation fields)

  // Lines (output)                 Connected or Disconnected lines depending
  // - OID                          on the OutputConnectivity solver option
  // - Shape                        
  // - SourceID
  // - SourceOID
  // - FromPos
  // - ToPos

  HRESULT hr;

  // Create the class definitions named set and the variables needed to properly instantiate them
  INamedSetPtr              ipClassDefinitions(CLSID_NamedSet);
  INAClassDefinitionPtr     ipClassDef;
  INAClassDefinitionEditPtr ipClassDefEdit;
  IUIDPtr                   ipIUID;
  IFieldsPtr                ipFields;
  IFieldsEditPtr            ipFieldsEdit;
  IFieldPtr                 ipField;
  IFieldEditPtr             ipFieldEdit;

  //////////////////////////////////////////////////////////
  // Seed Points class definition

  ipClassDef.CreateInstance(CLSID_NAClassDefinition);
  ipClassDefEdit = ipClassDef;
  ipIUID.CreateInstance(CLSID_UID);
  if (FAILED(hr = ipIUID->put_Value(CComVariant(L"esriNetworkAnalyst.NALocationFeature"))))
    return hr;
  ipClassDefEdit->putref_ClassCLSID(ipIUID);

  // Create the fields for the class definition
  ipFields.CreateInstance(CLSID_Fields);
  ipFieldsEdit = ipFields;

  // Create and add an OID field
  ipField.CreateInstance(CLSID_Field);
  ipFieldEdit = ipField;
  ipFieldEdit->put_Name(CComBSTR(CS_FIELD_OID));
  ipFieldEdit->put_Type(esriFieldTypeOID);
  ipFieldsEdit->AddField(ipFieldEdit);

  // Create and add a Shape field
  ipField.CreateInstance(CLSID_Field);
  ipFieldEdit = ipField;
  {
    IGeometryDefEditPtr  ipGeoDef(CLSID_GeometryDef);

    ipGeoDef->put_GeometryType(esriGeometryPoint);
    ipGeoDef->put_HasM(VARIANT_FALSE);
    ipGeoDef->put_HasZ(VARIANT_FALSE);
    ipGeoDef->putref_SpatialReference(pSpatialRef);
    ipFieldEdit->put_Name(CComBSTR(CS_FIELD_SHAPE));
    ipFieldEdit->put_IsNullable(VARIANT_TRUE);
    ipFieldEdit->put_Type(esriFieldTypeGeometry);
    ipFieldEdit->putref_GeometryDef(ipGeoDef);
  }
  ipFieldsEdit->AddField(ipFieldEdit);

  // Create and add a Name field
  ipField.CreateInstance(CLSID_Field);
  ipFieldEdit = ipField;
  ipFieldEdit->put_Name(CComBSTR(CS_FIELD_NAME));
  ipFieldEdit->put_Type(esriFieldTypeString);
  ipFieldEdit->put_Length(128);
  ipFieldsEdit->AddField(ipFieldEdit);

  // Add the NALocation fields
  AddLocationFields(ipFieldsEdit, pDENDS);

  // Add the new fields to the class definition (these must be set before setting field types)
  ipClassDefEdit->putref_Fields(ipFields);

  // Setup the field types (i.e., input/output fields, editable/non-editable fields, visible/non-visible fields, or a combination of these)
  AddLocationFieldTypes(ipFields, ipClassDefEdit);

  ipClassDefEdit->put_FieldType(CComBSTR(CS_FIELD_OID), esriNAFieldTypeInput | esriNAFieldTypeNotEditable);
  ipClassDefEdit->put_FieldType(CComBSTR(CS_FIELD_SHAPE), esriNAFieldTypeInput | esriNAFieldTypeNotEditable | esriNAFieldTypeNotVisible);
  ipClassDefEdit->put_FieldType(CComBSTR(CS_FIELD_NAME), esriNAFieldTypeInput);

  // Setup whether the NAClass is considered as input/output
  ipClassDefEdit->put_IsInput(VARIANT_TRUE);
  ipClassDefEdit->put_IsOutput(VARIANT_FALSE);

  // Setup necessary cardinality for allowing a Solve to be run 
  // NOTE: the LowerBound property is used to define the minimum number of required NALocationObjects that are required by the solver
  // to perform analysis. The UpperBound property is used to define the maximum number of NALocationObjects that are allowed by the solver
  // to perform analysis.
  // In our case, we must have at least one seed point stored in the class before allowing Solve to enabled in ArcMap, and we have no UpperBound
  ipClassDefEdit->put_LowerBound(1);

  // Give the class definition a name...
  ipClassDefEdit->put_Name(CComBSTR(CS_SEED_POINTS_NAME));

  // ...and add it to the named set
  ipClassDefinitions->Add(CComBSTR(CS_SEED_POINTS_NAME), (IUnknownPtr)ipClassDef);

  //////////////////////////////////////////////////////////
  // Barriers class definition

  ipClassDef.CreateInstance(CLSID_NAClassDefinition);
  ipClassDefEdit = ipClassDef;
  ipIUID.CreateInstance(CLSID_UID);
  if (FAILED(hr = ipIUID->put_Value(CComVariant(L"esriNetworkAnalyst.NALocationFeature"))))
    return hr;
  ipClassDefEdit->putref_ClassCLSID(ipIUID);

  ipFields.CreateInstance(CLSID_Fields);
  ipFieldsEdit = ipFields;

  ipField.CreateInstance(CLSID_Field);
  ipFieldEdit = ipField;
  ipFieldEdit->put_Name(CComBSTR(CS_FIELD_OID));
  ipFieldEdit->put_Type(esriFieldTypeOID);
  ipFieldsEdit->AddField(ipFieldEdit);

  ipField.CreateInstance(CLSID_Field);
  ipFieldEdit = ipField;
  {
    IGeometryDefEditPtr  ipGeoDef(CLSID_GeometryDef);

    ipGeoDef->put_GeometryType(esriGeometryPoint);
    ipGeoDef->put_HasM(VARIANT_FALSE);
    ipGeoDef->put_HasZ(VARIANT_FALSE);
    ipGeoDef->putref_SpatialReference(pSpatialRef);
    ipFieldEdit->put_Name(CComBSTR(CS_FIELD_SHAPE));
    ipFieldEdit->put_IsNullable(VARIANT_TRUE);
    ipFieldEdit->put_Type(esriFieldTypeGeometry);
    ipFieldEdit->putref_GeometryDef(ipGeoDef);
  }
  ipFieldsEdit->AddField(ipFieldEdit);

  ipField.CreateInstance(CLSID_Field);
  ipFieldEdit = ipField;
  ipFieldEdit->put_Name(CComBSTR(CS_FIELD_NAME));
  ipFieldEdit->put_Type(esriFieldTypeString);
  ipFieldEdit->put_Length(128);
  ipFieldsEdit->AddField(ipFieldEdit);

  AddLocationFields(ipFieldsEdit, pDENDS);

  ipClassDefEdit->putref_Fields(ipFields);  

  AddLocationFieldTypes(ipFields, ipClassDefEdit);

  ipClassDefEdit->put_FieldType(CComBSTR(CS_FIELD_OID), esriNAFieldTypeInput | esriNAFieldTypeNotEditable);
  ipClassDefEdit->put_FieldType(CComBSTR(CS_FIELD_SHAPE), esriNAFieldTypeInput | esriNAFieldTypeNotEditable | esriNAFieldTypeNotVisible);
  ipClassDefEdit->put_FieldType(CComBSTR(CS_FIELD_NAME), esriNAFieldTypeInput);

  ipClassDefEdit->put_IsInput(VARIANT_TRUE);
  ipClassDefEdit->put_IsOutput(VARIANT_FALSE);

  ipClassDefEdit->put_Name(CComBSTR(CS_BARRIERS_NAME));

  ipClassDefinitions->Add(CComBSTR(CS_BARRIERS_NAME), (IUnknownPtr)ipClassDef);

  //////////////////////////////////////////////////////////
  // Lines class definition 

  ipClassDef.CreateInstance(CLSID_NAClassDefinition);
  ipClassDefEdit = ipClassDef;
  ipIUID.CreateInstance(CLSID_UID);
  if (FAILED(hr = ipIUID->put_Value(CComVariant(L"esriGeoDatabase.Feature"))))
    return hr;
  ipClassDefEdit->putref_ClassCLSID(ipIUID);

  ipFields.CreateInstance(CLSID_Fields);
  ipFieldsEdit = ipFields;

  ipField.CreateInstance(CLSID_Field);
  ipFieldEdit = ipField;
  ipFieldEdit->put_Name(CComBSTR(CS_FIELD_OID));
  ipFieldEdit->put_Type(esriFieldTypeOID);
  ipFieldsEdit->AddField(ipFieldEdit);

  ipField.CreateInstance(CLSID_Field);
  ipFieldEdit = ipField;
  {
    IGeometryDefEditPtr  ipGeoDef(CLSID_GeometryDef);

    ipGeoDef->put_GeometryType(esriGeometryPolyline);
    ipGeoDef->put_HasM(VARIANT_FALSE);
    ipGeoDef->put_HasZ(VARIANT_FALSE);
    ipGeoDef->putref_SpatialReference(pSpatialRef);
    ipFieldEdit->put_Name(CComBSTR(CS_FIELD_SHAPE));
    ipFieldEdit->put_IsNullable(VARIANT_TRUE);
    ipFieldEdit->put_Type(esriFieldTypeGeometry);
    ipFieldEdit->putref_GeometryDef(ipGeoDef);
  }
  ipFieldsEdit->AddField(ipFieldEdit);

  ipField.CreateInstance(CLSID_Field);
  ipFieldEdit = ipField;
  ipFieldEdit->put_Name(CComBSTR(CS_FIELD_SOURCE_ID));
  ipFieldEdit->put_Type(esriFieldTypeInteger);
  ipFieldsEdit->AddField(ipFieldEdit);

  ipField.CreateInstance(CLSID_Field);
  ipFieldEdit = ipField;
  ipFieldEdit->put_Name(CComBSTR(CS_FIELD_SOURCE_OID));
  ipFieldEdit->put_Type(esriFieldTypeInteger);
  ipFieldsEdit->AddField(ipFieldEdit);

  ipField.CreateInstance(CLSID_Field);
  ipFieldEdit = ipField;
  ipFieldEdit->put_Name(CComBSTR(CS_FIELD_FROM_POS));
  ipFieldEdit->put_Type(esriFieldTypeDouble);
  ipFieldsEdit->AddField(ipFieldEdit);

  ipField.CreateInstance(CLSID_Field);
  ipFieldEdit = ipField;
  ipFieldEdit->put_Name(CComBSTR(CS_FIELD_TO_POS));
  ipFieldEdit->put_Type(esriFieldTypeDouble);
  ipFieldsEdit->AddField(ipFieldEdit);

  ipClassDefEdit->putref_Fields(ipFields);

  ipClassDefEdit->put_FieldType(CComBSTR(CS_FIELD_OID), esriNAFieldTypeOutput | esriNAFieldTypeNotEditable);
  ipClassDefEdit->put_FieldType(CComBSTR(CS_FIELD_SHAPE), esriNAFieldTypeOutput | esriNAFieldTypeNotEditable | esriNAFieldTypeNotVisible);
  ipClassDefEdit->put_FieldType(CComBSTR(CS_FIELD_SOURCE_ID), esriNAFieldTypeOutput);
  ipClassDefEdit->put_FieldType(CComBSTR(CS_FIELD_SOURCE_OID), esriNAFieldTypeOutput);
  ipClassDefEdit->put_FieldType(CComBSTR(CS_FIELD_FROM_POS), esriNAFieldTypeOutput);
  ipClassDefEdit->put_FieldType(CComBSTR(CS_FIELD_TO_POS), esriNAFieldTypeOutput);

  ipClassDefEdit->put_IsInput(VARIANT_FALSE);
  ipClassDefEdit->put_IsOutput(VARIANT_TRUE);

  ipClassDefEdit->put_Name(CComBSTR(CS_LINES_NAME));

  ipClassDefinitions->Add(CComBSTR(CS_LINES_NAME), (IUnknownPtr)ipClassDef);

  // Return the class definitions once we have finished
  ipClassDefinitions->AddRef();
  *ppDefinitions = ipClassDefinitions;

  return S_OK;
}

HRESULT ConnectivitySolver::GetNAClassTable(INAContext* pContext, BSTR className, ITable** ppTable)
{
  if (!pContext || !ppTable)
    return E_POINTER;

  HRESULT hr;
  INamedSetPtr ipNamedSet;

  if (FAILED(hr = pContext->get_NAClasses(&ipNamedSet)))
    return hr;

  IUnknownPtr ipUnk;
  if (FAILED(hr = ipNamedSet->get_ItemByName(className, &ipUnk)))
    return hr;

  ITablePtr ipTable(ipUnk);

  if (!ipTable)
    return AtlReportError(GetObjectCLSID(), _T("Context has an invalid NAClass."), IID_INASolver);

  ipTable->AddRef();
  *ppTable = ipTable;

  return S_OK;
}

HRESULT ConnectivitySolver::LoadBarriers(ITable* pTable, INetworkQuery* pNetworkQuery, INetworkForwardStarEx* pNetworkForwardStarEx)
{
  if (!pTable || !pNetworkForwardStarEx)
    return E_POINTER;

  HRESULT hr;

  // Initialize all network elements as traversable
  if (FAILED(hr = pNetworkForwardStarEx->RemoveElementRestrictions()))
    return hr;

  // Get a cursor on the table to loop through each row
  ICursorPtr ipCursor;
  if (FAILED(hr = pTable->Search(0, VARIANT_TRUE, &ipCursor)))
    return hr;

  // Create variables for looping through the cursor and setting up barriers
  IRowPtr ipRow;
  INALocationObjectPtr ipNALocationObject;
  INALocationPtr ipNALocation(CLSID_NALocation);
  IEnumNetworkElementPtr ipEnumNetworkElement;
  long sourceID = -1, sourceOID = -1;
  double posAlong, fromPosition, toPosition;
  
  INetworkEdgePtr ipReverseEdge;  // we need one edge element for querying the opposing direction
  INetworkElementPtr ipElement;
  pNetworkQuery->CreateNetworkElement(esriNETEdge, &ipElement);
  ipReverseEdge = ipElement;
  esriNetworkElementType elementType;

  // Loop through the cursor getting the NALocation of each NALocationObject,  
  // then query the network elements associated with each NALocation,
  // and set their traversability to false
  while (ipCursor->NextRow(&ipRow) == S_OK)
  {
    ipNALocationObject = ipRow;
    if (!ipNALocationObject) // we only want valid NALocationObjects
      continue;

    if (FAILED(hr = ipNALocationObject->QueryNALocation(ipNALocation)))
      return hr;

    // Once we have the NALocation, we need to check if it is actually located within the network dataset
    VARIANT_BOOL isLocated = VARIANT_FALSE;
    if (ipNALocation)
    {
      if (FAILED(hr = ipNALocation->get_IsLocated(&isLocated)))
        return hr;
    }
    
    // We are only concerned with located NALocations
    if (isLocated)
    {
      // Get the SourceID for the NALocation
      if (FAILED(hr = ipNALocation->get_SourceID(&sourceID)))
        return hr;

      // Get the SourceOID for the NALocation
      if (FAILED(hr = ipNALocation->get_SourceOID(&sourceOID)))
        return hr;

      // Get the PosAlong for the NALocation
      if (FAILED(hr = ipNALocation->get_SourcePosition(&posAlong)))
        return hr;

      // Once we have a located NALocation, we query the network to obtain its associated network elements
      if (FAILED(hr = pNetworkQuery->get_ElementsByOID(sourceID, sourceOID, &ipEnumNetworkElement)))
        return hr;

      // Set the traversability for all appropriate network elements to false
      ipEnumNetworkElement->Reset();
      while (ipEnumNetworkElement->Next(&ipElement) == S_OK)
      {
        if (FAILED(hr = ipElement->get_ElementType(&elementType)))
          return hr;

        switch (elementType)
        {
          case esriNETJunction:
          {
		    INetworkJunctionPtr ipJunction(ipElement);
            // If it is a junction, we can just set the element to non-traversable
            if (FAILED(hr = pNetworkForwardStarEx->AddJunctionRestriction(ipJunction)))
              return hr;
            
            break;
          }
          case esriNETEdge:
          {
            // If it is an edge, we must make sure it is an appropriate edge to set as non-traversable (by checking its fromPosition and toPosition)
            INetworkEdgePtr ipEdge(ipElement);
            if (FAILED(hr = ipEdge->QueryPositions(&fromPosition, &toPosition)))
              return hr;

            if (fromPosition <= posAlong && posAlong <= toPosition)
            {
              // Our NALocation lies along this edge element
              // Set it as non-traversable in both directions  

              // First, set this edge element as non-traversable in the AlongDigitized direction (this is the default direction of edge elements returned from the get_ElementsByOID method above)
		      INetworkEdgePtr ipEdge(ipElement);
              if (FAILED(hr = pNetworkForwardStarEx->AddEdgeRestriction(ipEdge, 0.0, 1.0)))
                return hr;
              
              // Then, query the edge element to get its opposite direction
              if (FAILED(hr = ipEdge->QueryEdgeInOtherDirection(ipReverseEdge)))
                return hr;

              // Finally, set this edge as non-traversable in the AgainstDigitized direction
              if (FAILED(hr = pNetworkForwardStarEx->AddEdgeRestriction(ipReverseEdge, 0.0, 1.0)))
                return hr;

              break;
            }
          }
        }
      }
    }
  }

  return S_OK;
}

HRESULT ConnectivitySolver::AddLocationFields(IFieldsEdit* pFieldsEdit, IDENetworkDataset* pDENDS)
{
  if (!pFieldsEdit)
    return E_POINTER;

  HRESULT                   hr;
  IFieldPtr                 ipField;
  IFieldEditPtr             ipFieldEdit;
  IRangeDomainPtr           ipRangeDomain;
  CComVariant                longDefaultValue(static_cast<long>(-1));
  CComVariant                doubleDefaultValue(static_cast<double>(-1));

  ipField.CreateInstance(CLSID_Field);
  ipFieldEdit = ipField;
  ipFieldEdit->put_Name(CComBSTR(CS_FIELD_SOURCE_ID));
  ipFieldEdit->put_Type(esriFieldTypeInteger);
  ipFieldEdit->put_DefaultValue(longDefaultValue);
  ipFieldEdit->put_Precision(0);
  ipFieldEdit->put_Required(VARIANT_TRUE);
  ipFieldEdit->put_Length(4);

  // if we can get source names, then add them to a coded value domain
  if (pDENDS)
  {
    IArrayPtr               ipSourceArray;
    if (FAILED(hr = pDENDS->get_Sources(&ipSourceArray)))
      return hr;

    ICodedValueDomainPtr    ipCodedValueDomain(CLSID_CodedValueDomain);
    IDomainPtr(ipCodedValueDomain)->put_Name(CS_FIELD_SOURCE_ID);
    IDomainPtr(ipCodedValueDomain)->put_FieldType(esriFieldTypeInteger);

    IUnknownPtr             ipUnk;
    INetworkSourcePtr       ipSource;
    long                    sourceCount;
    ipSourceArray->get_Count(&sourceCount);
    for (long i = 0; i < sourceCount; i++)
    {
      ipSourceArray->get_Element(i, &ipUnk);
      ipSource = ipUnk;

      long        sourceID;
      ipSource->get_ID(&sourceID);
      CComBSTR     name;
      ipSource->get_Name(&name);
      CComVariant     value(sourceID);

      ipCodedValueDomain->AddCode(value, name);
    }
    ipFieldEdit->putref_Domain((IDomainPtr)ipCodedValueDomain);
  }

  pFieldsEdit->AddField(ipFieldEdit);

  // Add the source OID field
  ipField.CreateInstance(CLSID_Field);
  ipRangeDomain.CreateInstance(CLSID_RangeDomain);
  IDomainPtr(ipRangeDomain)->put_Name(CS_FIELD_SOURCE_OID);
  IDomainPtr(ipRangeDomain)->put_FieldType(esriFieldTypeInteger);
  ipRangeDomain->put_MinValue(CComVariant((long)-1));
  ipRangeDomain->put_MaxValue(CComVariant((long)LONG_MAX));

  ipFieldEdit = ipField;
  ipFieldEdit->put_Name(CComBSTR(CS_FIELD_SOURCE_OID));
  ipFieldEdit->put_Type(esriFieldTypeInteger);
  ipFieldEdit->put_DefaultValue(longDefaultValue);
  ipFieldEdit->put_Precision(0);
  ipFieldEdit->put_Length(4);
  ipFieldEdit->put_Required(VARIANT_TRUE);
  ipFieldEdit->putref_Domain((IDomainPtr)ipRangeDomain);
  pFieldsEdit->AddField(ipFieldEdit);

  // Add the position field
  ipField.CreateInstance(CLSID_Field);
  ipRangeDomain.CreateInstance(CLSID_RangeDomain);
  IDomainPtr(ipRangeDomain)->put_Name(CS_FIELD_POSITION);
  IDomainPtr(ipRangeDomain)->put_FieldType(esriFieldTypeDouble);
  ipRangeDomain->put_MinValue(CComVariant((double)0.0));
  ipRangeDomain->put_MaxValue(CComVariant((double)1.0));
  ipFieldEdit = ipField;
  ipFieldEdit->put_Name(CComBSTR(CS_FIELD_POSITION));
  ipFieldEdit->put_Type(esriFieldTypeDouble);
  ipFieldEdit->put_DefaultValue(doubleDefaultValue);
  ipFieldEdit->put_Precision(0);
  ipFieldEdit->put_Length(8);
  ipFieldEdit->put_Required(VARIANT_TRUE);
  ipFieldEdit->putref_Domain((IDomainPtr)ipRangeDomain);
  pFieldsEdit->AddField(ipFieldEdit);

  // Add the side of Edge domain
  ipField.CreateInstance(CLSID_Field);
  ipFieldEdit = ipField;
  ipFieldEdit->put_Name(CComBSTR(CS_FIELD_SIDE_OF_EDGE));
  ipFieldEdit->put_Type(esriFieldTypeInteger);
  ipFieldEdit->put_DefaultValue(longDefaultValue);
  ipFieldEdit->put_Precision(0);
  ipFieldEdit->put_Length(4);
  ipFieldEdit->put_Required(VARIANT_TRUE);

  IDomainPtr    ipSideDomain;
  CreateSideOfEdgeDomain(&ipSideDomain);
  ipFieldEdit->putref_Domain(ipSideDomain);
  pFieldsEdit->AddField(ipFieldEdit);

  // Add the curb approach domain
  ipField.CreateInstance(CLSID_Field);
  ipFieldEdit = ipField;
  ipFieldEdit->put_Name(CComBSTR(CS_FIELD_CURBAPPROACH));
  ipFieldEdit->put_Type(esriFieldTypeInteger);
  ipFieldEdit->put_DefaultValue(CComVariant((long)esriNAEitherSideOfVehicle));
  ipFieldEdit->put_Precision(0);
  ipFieldEdit->put_Length(4);
  ipFieldEdit->put_Required(VARIANT_TRUE);

  IDomainPtr    ipCurbDomain;
  CreateCurbApproachDomain(&ipCurbDomain);
  ipFieldEdit->putref_Domain(ipCurbDomain);
  pFieldsEdit->AddField(ipFieldEdit);

  // Add the status field
  ipField.CreateInstance(CLSID_Field);
  ipFieldEdit = ipField;
  ipFieldEdit->put_Name(CComBSTR(CS_FIELD_STATUS));
  ipFieldEdit->put_Type(esriFieldTypeInteger);
  ipFieldEdit->put_DefaultValue(CComVariant((long)0));
  ipFieldEdit->put_Precision(0);
  ipFieldEdit->put_Length(4);
  ipFieldEdit->put_Required(VARIANT_TRUE);

  // set up coded value domains for the the status values
  ICodedValueDomainPtr    ipCodedValueDomain(CLSID_CodedValueDomain);
  CreateStatusCodedValueDomain(ipCodedValueDomain);
  ipFieldEdit->putref_Domain((IDomainPtr)ipCodedValueDomain);
  pFieldsEdit->AddField(ipFieldEdit);

  return S_OK;
}

HRESULT ConnectivitySolver::CreateSideOfEdgeDomain(IDomain** ppDomain)
{
  if (!ppDomain)
    return E_POINTER;

  HRESULT hr;
  ICodedValueDomainPtr ipCodedValueDomain(CLSID_CodedValueDomain);

  // Domains need names in order to serialize them
  //
  IDomainPtr(ipCodedValueDomain)->put_Name(CComBSTR(CS_SIDEOFEDGE_DOMAINNAME));
  IDomainPtr(ipCodedValueDomain)->put_FieldType(esriFieldTypeInteger);

  CComVariant value(static_cast<long>(esriNAEdgeSideLeft));
  if (FAILED(hr = ipCodedValueDomain->AddCode(value, CComBSTR(L"Left Side"))))
    return hr;

  value.lVal = esriNAEdgeSideRight;
  if (FAILED(hr = ipCodedValueDomain->AddCode(value, CComBSTR(L"Right Side"))))
    return hr;

  *ppDomain = (IDomainPtr)ipCodedValueDomain;
  ipCodedValueDomain->AddRef();
  return S_OK;
}

HRESULT ConnectivitySolver::CreateCurbApproachDomain(IDomain** ppDomain)
{
  if (!ppDomain)
    return E_POINTER;

  HRESULT       hr;
  ICodedValueDomainPtr    ipCodedValueDomain(CLSID_CodedValueDomain);

  IDomainPtr(ipCodedValueDomain)->put_Name(CComBSTR(CS_CURBAPPROACH_DOMAINNAME));
  IDomainPtr(ipCodedValueDomain)->put_FieldType(esriFieldTypeInteger);

  CComVariant value(static_cast<long>(esriNAEitherSideOfVehicle));
  if (FAILED(hr = ipCodedValueDomain->AddCode(value, CComBSTR(L"Either side of vehicle"))))
    return hr;

  value.lVal = esriNARightSideOfVehicle;
  if (FAILED(hr = ipCodedValueDomain->AddCode(value, CComBSTR(L"Right side of vehicle"))))
    return hr;

  value.lVal = esriNALeftSideOfVehicle;
  if (FAILED(hr = ipCodedValueDomain->AddCode(value, CComBSTR(L"Left side of vehicle"))))
    return hr;

  *ppDomain = (IDomainPtr)ipCodedValueDomain;
  ipCodedValueDomain->AddRef();
  return S_OK;
}

HRESULT ConnectivitySolver::CreateStatusCodedValueDomain(ICodedValueDomain* pCodedValueDomain)
{
  if (!pCodedValueDomain)
    return E_POINTER;

  IDomainPtr(pCodedValueDomain)->put_Name(CComBSTR(CS_LOCATIONSTATUS_DOMAINNAME));
  IDomainPtr(pCodedValueDomain)->put_FieldType(esriFieldTypeInteger);

  CComVariant value(static_cast<long>(esriNAObjectStatusOK)); // this code is exclusive with the other codes
  pCodedValueDomain->AddCode(value, CComBSTR(L"OK"));

  value.lVal = esriNAObjectStatusNotLocated;
  pCodedValueDomain->AddCode(value, CComBSTR(L"Not located"));

  value.lVal = esriNAObjectStatusElementNotLocated;
  pCodedValueDomain->AddCode(value, CComBSTR(L"Network element not located"));

  value.lVal = esriNAObjectStatusElementNotTraversable;
  pCodedValueDomain->AddCode(value, CComBSTR(L"Element not traversible"));

  value.lVal = esriNAObjectStatusInvalidFieldValues;
  pCodedValueDomain->AddCode(value, CComBSTR(L"Invalid field values"));

  value.lVal = esriNAObjectStatusNotReached;
  pCodedValueDomain->AddCode(value, CComBSTR(L"Not reached"));

  value.lVal = esriNAObjectStatusTimeWindowViolation;
  pCodedValueDomain->AddCode(value, CComBSTR(L"Time window violation"));

  return S_OK;
}

HRESULT ConnectivitySolver::AddLocationFieldTypes(IFields* pFields, INAClassDefinitionEdit* pClassDef)
{
  if (!pClassDef)
    return E_INVALIDARG;

  pClassDef->put_FieldType(CComBSTR(CS_FIELD_SOURCE_ID), esriNAFieldTypeInput);
  pClassDef->put_FieldType(CComBSTR(CS_FIELD_SOURCE_OID), esriNAFieldTypeInput);
  pClassDef->put_FieldType(CComBSTR(CS_FIELD_POSITION), esriNAFieldTypeInput);
  pClassDef->put_FieldType(CComBSTR(CS_FIELD_SIDE_OF_EDGE), esriNAFieldTypeInput);
  pClassDef->put_FieldType(CComBSTR(CS_FIELD_CURBAPPROACH), esriNAFieldTypeInput);
  pClassDef->put_FieldType(CComBSTR(CS_FIELD_STATUS), esriNAFieldTypeInput | esriNAFieldTypeNotEditable);

  return S_OK;
}

HRESULT ConnectivitySolver::OutputLineFeatures(long sourceID, long featureCount, segment_hash& segmentRecordHashTable, IFeatureClass* pSourceFC, IFeatureCursor* pInsertFeatureCursor, IFeatureBuffer* pFeatureBuffer)
{
  if (!pSourceFC || !pInsertFeatureCursor || !pFeatureBuffer)
    return E_POINTER;

  if(sourceID < 0 || featureCount <= 0)
    return E_INVALIDARG;

  HRESULT hr;

  // Query for the appropriate field index values in the "LineData" feature class
  IFieldsPtr ipFields;
  if (FAILED(hr = pFeatureBuffer->get_Fields(&ipFields)))
    return hr;

  long sourceIDFieldIndex, sourceOIDFieldIndex, fromPositionFieldIndex, toPositionFieldIndex;
  if (FAILED(hr = ipFields->FindField(CComBSTR(CS_FIELD_SOURCE_ID), &sourceIDFieldIndex)))
    return hr;
  if (FAILED(hr = ipFields->FindField(CComBSTR(CS_FIELD_SOURCE_OID), &sourceOIDFieldIndex)))
    return hr;
  if (FAILED(hr = ipFields->FindField(CComBSTR(CS_FIELD_FROM_POS), &fromPositionFieldIndex)))
    return hr;
  if (FAILED(hr = ipFields->FindField(CComBSTR(CS_FIELD_TO_POS), &toPositionFieldIndex)))
    return hr;

  long* oidArray = new long[featureCount];
  segment_hash::const_iterator segmentIterator;
  long i = 0;
  for (segmentIterator = segmentRecordHashTable.begin(); segmentIterator != segmentRecordHashTable.end(); segmentIterator = segmentRecordHashTable.upper_bound(segmentIterator->first))
    oidArray[i++] = segmentIterator->first; // pass all unique OID keys into the oidArray

  // Create a VARIANT array of the OID values
  CComVariant oids;
  oids.vt = VT_I4 | VT_ARRAY;
  oids.parray = ::SafeArrayCreateVector(VT_I4, 0, featureCount);
  void* p;
  ::SafeArrayAccessData(oids.parray, &p);
  ::CopyMemory(p, (void*)oidArray, oids.parray->cbElements * featureCount);
  ::SafeArrayUnaccessData(oids.parray);

  // Get the features from the network source feature class that correspond to this list of OIDs
  IFeatureCursorPtr ipSourceFeatureCursor;
  if (FAILED(hr = pSourceFC->GetFeatures(oids, VARIANT_TRUE, &ipSourceFeatureCursor)))
    return hr;
  
  // Clean up dynamic memory allocation
  delete[] oidArray;

  // Loop through the source feature cursor, get the oid_bucket for each feature, and insert new features into the insert cursor
  IFeaturePtr ipSourceFeature;
  IGeometryPtr ipGeometry;
  oid_bucket edgeSegments;
  double fromPosition, toPosition;
  long sourceOID;
  CComVariant featureID(0);
  while (ipSourceFeatureCursor->NextFeature(&ipSourceFeature) == S_OK)
  {
    // Get the OID for this source
    if(FAILED(hr = ipSourceFeature->get_OID(&sourceOID)))
      return hr;

    // Get an iterator over all segmentRecords associated with this OID key
    edgeSegments = segmentRecordHashTable.equal_range(sourceOID);
    segmentIterator = edgeSegments.first;
    while (segmentIterator != edgeSegments.second)
    {
      // Get the segment record associated with this particular edge element
      segmentRecord currentSegmentRecord = segmentIterator->second;
      fromPosition = currentSegmentRecord.fromPosition;
      toPosition = currentSegmentRecord.toPosition;

      if (FAILED(hr = ipSourceFeature->get_Shape(&ipGeometry)))
        return hr;

      // Check to see how much of the line geometry we can copy over
      if (fromPosition != 0.0 || toPosition != 1.0)
      {
        // We must use only a subcurve of the line geometry
        ICurve3Ptr ipCurve(ipGeometry);
        ICurvePtr  ipSubCurve;

        if (FAILED(hr = ipCurve->GetSubcurve(fromPosition, toPosition, VARIANT_TRUE, &ipSubCurve)))
          return hr;

        ipGeometry = ipSubCurve;
      }
      
      // Store the feature values on the feature buffer
      if (FAILED(hr = pFeatureBuffer->putref_Shape(ipGeometry)))
        return hr;
      if (FAILED(hr = pFeatureBuffer->put_Value(sourceIDFieldIndex, CComVariant(sourceID))))
        return hr;
      if (FAILED(hr = pFeatureBuffer->put_Value(sourceOIDFieldIndex, CComVariant(sourceOID))))
        return hr;
      if (FAILED(hr = pFeatureBuffer->put_Value(fromPositionFieldIndex, CComVariant(fromPosition))))
        return hr;
      if (FAILED(hr = pFeatureBuffer->put_Value(toPositionFieldIndex, CComVariant(toPosition))))
        return hr;

      // Insert the feature buffer in the insert cursor
      if (FAILED(hr = pInsertFeatureCursor->InsertFeature(pFeatureBuffer, &featureID)))
        return hr;

      segmentIterator++;
    }
  }

  // Empty segmentRecordHashTable
  segmentRecordHashTable.clear();

  return S_OK;
}

