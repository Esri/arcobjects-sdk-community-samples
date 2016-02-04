/*

   Copyright 2016 Esri

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



#pragma once

#include <vector>
#include <stack>
//#include <hash_map>
#include <unordered_map>

using namespace std;
using namespace stdext;

struct segmentRecord
{
  segmentRecord(double from, double to)
   : fromPosition(from),
     toPosition(to)
  {
  }

  double fromPosition;
  double toPosition;
};

typedef vector<bool> connectivity_bitmap;
typedef stack<long> junction_stack;
typedef unordered_multimap<long, segmentRecord> segment_hash;
typedef pair<long, segmentRecord> segment_entry;
typedef pair<segment_hash::const_iterator, segment_hash::const_iterator> oid_bucket;

