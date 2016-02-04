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
// ############################################################################
// ############################################################################
// ############################################################################
// ############################################################################
//
// Namespace:     OpenGL
// Class:         GL
// File Version:  4.0 (2003 Feb 14; Second Public Release)
//
// Description: C# Wrapper for OpenGL GL API.
//
// WARNING: This class must be compiled using the /unsafe C# compiler switch
//          since many OpenGL functions involve pointers.
//
// Development Notes:
//
//   This file was almost entirely generated from the following file:
// "C:\\Program Files\\Microsoft Visual Studio\\VC98\\Include\\GL\\GL.H"
// (69,083 bytes; Friday, April 24, 1998, 12:00:00 AM).
//
//   All aspects of GL.H were converted and emitted as C# code,
// except for typedef's (such as typedef's of function pointers
// for OpenGL extensions).  All pointer types in arguments and return
// types has been preserved.
//
//   After exposing all constants and functions as closely as possible to
// the original "C" code prototypes, we expose any function involving pointers
// again, overloading the same function name, but changing pointer types to
// C# array types (e.g., float[] instead of float *), or using the "IntPtr"
// type.  These convenient function overloads appear after all of the 
// "faithful" versions of the GL functions
//
// Planned Modifications:
//
//    Functions returning values need to be implemented.  This is simply
// something that was just a little too tricky for my automated file conversion.
//
// Future Versions of this File:
//
//   This file is the first public release of a particular C# wrapper for
// the GL portion of the OpenGL API.  Please visit the following web page
// for updates to this file:
//
//     http://www.colinfahey.com/opengl/csharp.htm
//
//   Send any bug reports or minor implementation enhancement suggestions
// to the following e-mail address:
//
//     cpfahey@earthlink.net      (Colin P. Fahey)
//
// ############################################################################
// ############################################################################
// ############################################################################
// ############################################################################
//
//
using System;
using System.Runtime.InteropServices; // Necessary for [DllImport(...)]
namespace OpenGL
{
[ComVisible(false)]
public class GL
{
public const string GL_DLL = "opengl32";
//
// ############################################################################
// ############################################################################
// ############################################################################
// ############################################################################
//
//
/*++ BUILD Version: 0004    // Increment this if a change has global effects

Copyright (c) 1985-96, Microsoft Corporation

Module Name:

    gl.h

Abstract:

    Procedure declarations, constant definitions and macros for the OpenGL
    component.

--*/

/*
** Copyright 1996 Silicon Graphics, Inc.
** All Rights Reserved.
**
** This is UNPUBLISHED PROPRIETARY SOURCE CODE of Silicon Graphics, Inc.;
** the contents of this file may not be disclosed to third parties, copied or
** duplicated in any form, in whole or in part, without the prior written
** permission of Silicon Graphics, Inc.
**
** RESTRICTED RIGHTS LEGEND:
** Use, duplication or disclosure by the Government is subject to restrictions
** as set forth in subdivision (c)(1)(ii) of the Rights in Technical Data
** and Computer Software clause at DFARS 252.227-7013, and/or in similar or
** successor clauses in the FAR, DOD or NASA FAR Supplement. Unpublished -
** rights reserved under the Copyright Laws of the United States.
*/

/*************************************************************/

/* Version */
public const uint  GL_VERSION_1_1                       =            1;

/* AccumOp */
public const uint  GL_ACCUM                             =       0x0100;
public const uint  GL_LOAD                              =       0x0101;
public const uint  GL_RETURN                            =       0x0102;
public const uint  GL_MULT                              =       0x0103;
public const uint  GL_ADD                               =       0x0104;

/* AlphaFunction */
public const uint  GL_NEVER                             =       0x0200;
public const uint  GL_LESS                              =       0x0201;
public const uint  GL_EQUAL                             =       0x0202;
public const uint  GL_LEQUAL                            =       0x0203;
public const uint  GL_GREATER                           =       0x0204;
public const uint  GL_NOTEQUAL                          =       0x0205;
public const uint  GL_GEQUAL                            =       0x0206;
public const uint  GL_ALWAYS                            =       0x0207;

/* AttribMask */
public const uint  GL_CURRENT_BIT                       =   0x00000001;
public const uint  GL_POINT_BIT                         =   0x00000002;
public const uint  GL_LINE_BIT                          =   0x00000004;
public const uint  GL_POLYGON_BIT                       =   0x00000008;
public const uint  GL_POLYGON_STIPPLE_BIT               =   0x00000010;
public const uint  GL_PIXEL_MODE_BIT                    =   0x00000020;
public const uint  GL_LIGHTING_BIT                      =   0x00000040;
public const uint  GL_FOG_BIT                           =   0x00000080;
public const uint  GL_DEPTH_BUFFER_BIT                  =   0x00000100;
public const uint  GL_ACCUM_BUFFER_BIT                  =   0x00000200;
public const uint  GL_STENCIL_BUFFER_BIT                =   0x00000400;
public const uint  GL_VIEWPORT_BIT                      =   0x00000800;
public const uint  GL_TRANSFORM_BIT                     =   0x00001000;
public const uint  GL_ENABLE_BIT                        =   0x00002000;
public const uint  GL_COLOR_BUFFER_BIT                  =   0x00004000;
public const uint  GL_HINT_BIT                          =   0x00008000;
public const uint  GL_EVAL_BIT                          =   0x00010000;
public const uint  GL_LIST_BIT                          =   0x00020000;
public const uint  GL_TEXTURE_BIT                       =   0x00040000;
public const uint  GL_SCISSOR_BIT                       =   0x00080000;
public const uint  GL_ALL_ATTRIB_BITS                   =   0x000fffff;

/* BeginMode */
public const uint  GL_POINTS                            =       0x0000;
public const uint  GL_LINES                             =       0x0001;
public const uint  GL_LINE_LOOP                         =       0x0002;
public const uint  GL_LINE_STRIP                        =       0x0003;
public const uint  GL_TRIANGLES                         =       0x0004;
public const uint  GL_TRIANGLE_STRIP                    =       0x0005;
public const uint  GL_TRIANGLE_FAN                      =       0x0006;
public const uint  GL_QUADS                             =       0x0007;
public const uint  GL_QUAD_STRIP                        =       0x0008;
public const uint  GL_POLYGON                           =       0x0009;

/* BlendingFactorDest */
public const uint  GL_ZERO                              =            0;
public const uint  GL_ONE                               =            1;
public const uint  GL_SRC_COLOR                         =       0x0300;
public const uint  GL_ONE_MINUS_SRC_COLOR               =       0x0301;
public const uint  GL_SRC_ALPHA                         =       0x0302;
public const uint  GL_ONE_MINUS_SRC_ALPHA               =       0x0303;
public const uint  GL_DST_ALPHA                         =       0x0304;
public const uint  GL_ONE_MINUS_DST_ALPHA               =       0x0305;

/* BlendingFactorSrc */
/*      GL_ZERO */
/*      GL_ONE */
public const uint  GL_DST_COLOR                         =       0x0306;
public const uint  GL_ONE_MINUS_DST_COLOR               =       0x0307;
public const uint  GL_SRC_ALPHA_SATURATE                =       0x0308;
/*      GL_SRC_ALPHA */
/*      GL_ONE_MINUS_SRC_ALPHA */
/*      GL_DST_ALPHA */
/*      GL_ONE_MINUS_DST_ALPHA */

/* Boolean */
public const uint  GL_TRUE                              =            1;
public const uint  GL_FALSE                             =            0;

/* ClearBufferMask */
/*      GL_COLOR_BUFFER_BIT */
/*      GL_ACCUM_BUFFER_BIT */
/*      GL_STENCIL_BUFFER_BIT */
/*      GL_DEPTH_BUFFER_BIT */

/* ClientArrayType */
/*      GL_VERTEX_ARRAY */
/*      GL_NORMAL_ARRAY */
/*      GL_COLOR_ARRAY */
/*      GL_INDEX_ARRAY */
/*      GL_TEXTURE_COORD_ARRAY */
/*      GL_EDGE_FLAG_ARRAY */

/* ClipPlaneName */
public const uint  GL_CLIP_PLANE0                       =       0x3000;
public const uint  GL_CLIP_PLANE1                       =       0x3001;
public const uint  GL_CLIP_PLANE2                       =       0x3002;
public const uint  GL_CLIP_PLANE3                       =       0x3003;
public const uint  GL_CLIP_PLANE4                       =       0x3004;
public const uint  GL_CLIP_PLANE5                       =       0x3005;

/* ColorMaterialFace */
/*      GL_FRONT */
/*      GL_BACK */
/*      GL_FRONT_AND_BACK */

/* ColorMaterialParameter */
/*      GL_AMBIENT */
/*      GL_DIFFUSE */
/*      GL_SPECULAR */
/*      GL_EMISSION */
/*      GL_AMBIENT_AND_DIFFUSE */

/* ColorPointerType */
/*      GL_BYTE */
/*      GL_UNSIGNED_BYTE */
/*      GL_SHORT */
/*      GL_UNSIGNED_SHORT */
/*      GL_INT */
/*      GL_UNSIGNED_INT */
/*      GL_FLOAT */
/*      GL_DOUBLE */

/* CullFaceMode */
/*      GL_FRONT */
/*      GL_BACK */
/*      GL_FRONT_AND_BACK */

/* DataType */
public const uint  GL_BYTE                              =       0x1400;
public const uint  GL_UNSIGNED_BYTE                     =       0x1401;
public const uint  GL_SHORT                             =       0x1402;
public const uint  GL_UNSIGNED_SHORT                    =       0x1403;
public const uint  GL_INT                               =       0x1404;
public const uint  GL_UNSIGNED_INT                      =       0x1405;
public const uint  GL_FLOAT                             =       0x1406;
public const uint  GL_2_BYTES                           =       0x1407;
public const uint  GL_3_BYTES                           =       0x1408;
public const uint  GL_4_BYTES                           =       0x1409;
public const uint  GL_DOUBLE                            =       0x140a;

/* DepthFunction */
/*      GL_NEVER */
/*      GL_LESS */
/*      GL_EQUAL */
/*      GL_LEQUAL */
/*      GL_GREATER */
/*      GL_NOTEQUAL */
/*      GL_GEQUAL */
/*      GL_ALWAYS */

/* DrawBufferMode */
public const uint  GL_NONE                              =            0;
public const uint  GL_FRONT_LEFT                        =       0x0400;
public const uint  GL_FRONT_RIGHT                       =       0x0401;
public const uint  GL_BACK_LEFT                         =       0x0402;
public const uint  GL_BACK_RIGHT                        =       0x0403;
public const uint  GL_FRONT                             =       0x0404;
public const uint  GL_BACK                              =       0x0405;
public const uint  GL_LEFT                              =       0x0406;
public const uint  GL_RIGHT                             =       0x0407;
public const uint  GL_FRONT_AND_BACK                    =       0x0408;
public const uint  GL_AUX0                              =       0x0409;
public const uint  GL_AUX1                              =       0x040a;
public const uint  GL_AUX2                              =       0x040b;
public const uint  GL_AUX3                              =       0x040c;

/* Enable */
/*      GL_FOG */
/*      GL_LIGHTING */
/*      GL_TEXTURE_1D */
/*      GL_TEXTURE_2D */
/*      GL_LINE_STIPPLE */
/*      GL_POLYGON_STIPPLE */
/*      GL_CULL_FACE */
/*      GL_ALPHA_TEST */
/*      GL_BLEND */
/*      GL_INDEX_LOGIC_OP */
/*      GL_COLOR_LOGIC_OP */
/*      GL_DITHER */
/*      GL_STENCIL_TEST */
/*      GL_DEPTH_TEST */
/*      GL_CLIP_PLANE0 */
/*      GL_CLIP_PLANE1 */
/*      GL_CLIP_PLANE2 */
/*      GL_CLIP_PLANE3 */
/*      GL_CLIP_PLANE4 */
/*      GL_CLIP_PLANE5 */
/*      GL_LIGHT0 */
/*      GL_LIGHT1 */
/*      GL_LIGHT2 */
/*      GL_LIGHT3 */
/*      GL_LIGHT4 */
/*      GL_LIGHT5 */
/*      GL_LIGHT6 */
/*      GL_LIGHT7 */
/*      GL_TEXTURE_GEN_S */
/*      GL_TEXTURE_GEN_T */
/*      GL_TEXTURE_GEN_R */
/*      GL_TEXTURE_GEN_Q */
/*      GL_MAP1_VERTEX_3 */
/*      GL_MAP1_VERTEX_4 */
/*      GL_MAP1_COLOR_4 */
/*      GL_MAP1_INDEX */
/*      GL_MAP1_NORMAL */
/*      GL_MAP1_TEXTURE_COORD_1 */
/*      GL_MAP1_TEXTURE_COORD_2 */
/*      GL_MAP1_TEXTURE_COORD_3 */
/*      GL_MAP1_TEXTURE_COORD_4 */
/*      GL_MAP2_VERTEX_3 */
/*      GL_MAP2_VERTEX_4 */
/*      GL_MAP2_COLOR_4 */
/*      GL_MAP2_INDEX */
/*      GL_MAP2_NORMAL */
/*      GL_MAP2_TEXTURE_COORD_1 */
/*      GL_MAP2_TEXTURE_COORD_2 */
/*      GL_MAP2_TEXTURE_COORD_3 */
/*      GL_MAP2_TEXTURE_COORD_4 */
/*      GL_POINT_SMOOTH */
/*      GL_LINE_SMOOTH */
/*      GL_POLYGON_SMOOTH */
/*      GL_SCISSOR_TEST */
/*      GL_COLOR_MATERIAL */
/*      GL_NORMALIZE */
/*      GL_AUTO_NORMAL */
/*      GL_VERTEX_ARRAY */
/*      GL_NORMAL_ARRAY */
/*      GL_COLOR_ARRAY */
/*      GL_INDEX_ARRAY */
/*      GL_TEXTURE_COORD_ARRAY */
/*      GL_EDGE_FLAG_ARRAY */
/*      GL_POLYGON_OFFSET_POINT */
/*      GL_POLYGON_OFFSET_LINE */
/*      GL_POLYGON_OFFSET_FILL */

/* ErrorCode */
public const uint  GL_NO_ERROR                          =            0;
public const uint  GL_INVALID_ENUM                      =       0x0500;
public const uint  GL_INVALID_VALUE                     =       0x0501;
public const uint  GL_INVALID_OPERATION                 =       0x0502;
public const uint  GL_STACK_OVERFLOW                    =       0x0503;
public const uint  GL_STACK_UNDERFLOW                   =       0x0504;
public const uint  GL_OUT_OF_MEMORY                     =       0x0505;

/* FeedBackMode */
public const uint  GL_2D                                =       0x0600;
public const uint  GL_3D                                =       0x0601;
public const uint  GL_3D_COLOR                          =       0x0602;
public const uint  GL_3D_COLOR_TEXTURE                  =       0x0603;
public const uint  GL_4D_COLOR_TEXTURE                  =       0x0604;

/* FeedBackToken */
public const uint  GL_PASS_THROUGH_TOKEN                =       0x0700;
public const uint  GL_POINT_TOKEN                       =       0x0701;
public const uint  GL_LINE_TOKEN                        =       0x0702;
public const uint  GL_POLYGON_TOKEN                     =       0x0703;
public const uint  GL_BITMAP_TOKEN                      =       0x0704;
public const uint  GL_DRAW_PIXEL_TOKEN                  =       0x0705;
public const uint  GL_COPY_PIXEL_TOKEN                  =       0x0706;
public const uint  GL_LINE_RESET_TOKEN                  =       0x0707;

/* FogMode */
/*      GL_LINEAR */
public const uint  GL_EXP                               =       0x0800;
public const uint  GL_EXP2                              =       0x0801;

/* FogParameter */
/*      GL_FOG_COLOR */
/*      GL_FOG_DENSITY */
/*      GL_FOG_END */
/*      GL_FOG_INDEX */
/*      GL_FOG_MODE */
/*      GL_FOG_START */

/* FrontFaceDirection */
public const uint  GL_CW                                =       0x0900;
public const uint  GL_CCW                               =       0x0901;

/* GetMapTarget */
public const uint  GL_COEFF                             =       0x0a00;
public const uint  GL_ORDER                             =       0x0a01;
public const uint  GL_DOMAIN                            =       0x0a02;

/* GetPixelMap */
/*      GL_PIXEL_MAP_I_TO_I */
/*      GL_PIXEL_MAP_S_TO_S */
/*      GL_PIXEL_MAP_I_TO_R */
/*      GL_PIXEL_MAP_I_TO_G */
/*      GL_PIXEL_MAP_I_TO_B */
/*      GL_PIXEL_MAP_I_TO_A */
/*      GL_PIXEL_MAP_R_TO_R */
/*      GL_PIXEL_MAP_G_TO_G */
/*      GL_PIXEL_MAP_B_TO_B */
/*      GL_PIXEL_MAP_A_TO_A */

/* GetPointerTarget */
/*      GL_VERTEX_ARRAY_POINTER */
/*      GL_NORMAL_ARRAY_POINTER */
/*      GL_COLOR_ARRAY_POINTER */
/*      GL_INDEX_ARRAY_POINTER */
/*      GL_TEXTURE_COORD_ARRAY_POINTER */
/*      GL_EDGE_FLAG_ARRAY_POINTER */

/* GetTarget */
public const uint  GL_CURRENT_COLOR                     =       0x0b00;
public const uint  GL_CURRENT_INDEX                     =       0x0b01;
public const uint  GL_CURRENT_NORMAL                    =       0x0b02;
public const uint  GL_CURRENT_TEXTURE_COORDS            =       0x0b03;
public const uint  GL_CURRENT_RASTER_COLOR              =       0x0b04;
public const uint  GL_CURRENT_RASTER_INDEX              =       0x0b05;
public const uint  GL_CURRENT_RASTER_TEXTURE_COORDS     =       0x0b06;
public const uint  GL_CURRENT_RASTER_POSITION           =       0x0b07;
public const uint  GL_CURRENT_RASTER_POSITION_VALID     =       0x0b08;
public const uint  GL_CURRENT_RASTER_DISTANCE           =       0x0b09;
public const uint  GL_POINT_SMOOTH                      =       0x0b10;
public const uint  GL_POINT_SIZE                        =       0x0b11;
public const uint  GL_POINT_SIZE_RANGE                  =       0x0b12;
public const uint  GL_POINT_SIZE_GRANULARITY            =       0x0b13;
public const uint  GL_LINE_SMOOTH                       =       0x0b20;
public const uint  GL_LINE_WIDTH                        =       0x0b21;
public const uint  GL_LINE_WIDTH_RANGE                  =       0x0b22;
public const uint  GL_LINE_WIDTH_GRANULARITY            =       0x0b23;
public const uint  GL_LINE_STIPPLE                      =       0x0b24;
public const uint  GL_LINE_STIPPLE_PATTERN              =       0x0b25;
public const uint  GL_LINE_STIPPLE_REPEAT               =       0x0b26;
public const uint  GL_LIST_MODE                         =       0x0b30;
public const uint  GL_MAX_LIST_NESTING                  =       0x0b31;
public const uint  GL_LIST_BASE                         =       0x0b32;
public const uint  GL_LIST_INDEX                        =       0x0b33;
public const uint  GL_POLYGON_MODE                      =       0x0b40;
public const uint  GL_POLYGON_SMOOTH                    =       0x0b41;
public const uint  GL_POLYGON_STIPPLE                   =       0x0b42;
public const uint  GL_EDGE_FLAG                         =       0x0b43;
public const uint  GL_CULL_FACE                         =       0x0b44;
public const uint  GL_CULL_FACE_MODE                    =       0x0b45;
public const uint  GL_FRONT_FACE                        =       0x0b46;
public const uint  GL_LIGHTING                          =       0x0b50;
public const uint  GL_LIGHT_MODEL_LOCAL_VIEWER          =       0x0b51;
public const uint  GL_LIGHT_MODEL_TWO_SIDE              =       0x0b52;
public const uint  GL_LIGHT_MODEL_AMBIENT               =       0x0b53;
public const uint  GL_SHADE_MODEL                       =       0x0b54;
public const uint  GL_COLOR_MATERIAL_FACE               =       0x0b55;
public const uint  GL_COLOR_MATERIAL_PARAMETER          =       0x0b56;
public const uint  GL_COLOR_MATERIAL                    =       0x0b57;
public const uint  GL_FOG                               =       0x0b60;
public const uint  GL_FOG_INDEX                         =       0x0b61;
public const uint  GL_FOG_DENSITY                       =       0x0b62;
public const uint  GL_FOG_START                         =       0x0b63;
public const uint  GL_FOG_END                           =       0x0b64;
public const uint  GL_FOG_MODE                          =       0x0b65;
public const uint  GL_FOG_COLOR                         =       0x0b66;
public const uint  GL_DEPTH_RANGE                       =       0x0b70;
public const uint  GL_DEPTH_TEST                        =       0x0b71;
public const uint  GL_DEPTH_WRITEMASK                   =       0x0b72;
public const uint  GL_DEPTH_CLEAR_VALUE                 =       0x0b73;
public const uint  GL_DEPTH_FUNC                        =       0x0b74;
public const uint  GL_ACCUM_CLEAR_VALUE                 =       0x0b80;
public const uint  GL_STENCIL_TEST                      =       0x0b90;
public const uint  GL_STENCIL_CLEAR_VALUE               =       0x0b91;
public const uint  GL_STENCIL_FUNC                      =       0x0b92;
public const uint  GL_STENCIL_VALUE_MASK                =       0x0b93;
public const uint  GL_STENCIL_FAIL                      =       0x0b94;
public const uint  GL_STENCIL_PASS_DEPTH_FAIL           =       0x0b95;
public const uint  GL_STENCIL_PASS_DEPTH_PASS           =       0x0b96;
public const uint  GL_STENCIL_REF                       =       0x0b97;
public const uint  GL_STENCIL_WRITEMASK                 =       0x0b98;
public const uint  GL_MATRIX_MODE                       =       0x0ba0;
public const uint  GL_NORMALIZE                         =       0x0ba1;
public const uint  GL_VIEWPORT                          =       0x0ba2;
public const uint  GL_MODELVIEW_STACK_DEPTH             =       0x0ba3;
public const uint  GL_PROJECTION_STACK_DEPTH            =       0x0ba4;
public const uint  GL_TEXTURE_STACK_DEPTH               =       0x0ba5;
public const uint  GL_MODELVIEW_MATRIX                  =       0x0ba6;
public const uint  GL_PROJECTION_MATRIX                 =       0x0ba7;
public const uint  GL_TEXTURE_MATRIX                    =       0x0ba8;
public const uint  GL_ATTRIB_STACK_DEPTH                =       0x0bb0;
public const uint  GL_CLIENT_ATTRIB_STACK_DEPTH         =       0x0bb1;
public const uint  GL_ALPHA_TEST                        =       0x0bc0;
public const uint  GL_ALPHA_TEST_FUNC                   =       0x0bc1;
public const uint  GL_ALPHA_TEST_REF                    =       0x0bc2;
public const uint  GL_DITHER                            =       0x0bd0;
public const uint  GL_BLEND_DST                         =       0x0be0;
public const uint  GL_BLEND_SRC                         =       0x0be1;
public const uint  GL_BLEND                             =       0x0be2;
public const uint  GL_LOGIC_OP_MODE                     =       0x0bf0;
public const uint  GL_INDEX_LOGIC_OP                    =       0x0bf1;
public const uint  GL_COLOR_LOGIC_OP                    =       0x0bf2;
public const uint  GL_AUX_BUFFERS                       =       0x0c00;
public const uint  GL_DRAW_BUFFER                       =       0x0c01;
public const uint  GL_READ_BUFFER                       =       0x0c02;
public const uint  GL_SCISSOR_BOX                       =       0x0c10;
public const uint  GL_SCISSOR_TEST                      =       0x0c11;
public const uint  GL_INDEX_CLEAR_VALUE                 =       0x0c20;
public const uint  GL_INDEX_WRITEMASK                   =       0x0c21;
public const uint  GL_COLOR_CLEAR_VALUE                 =       0x0c22;
public const uint  GL_COLOR_WRITEMASK                   =       0x0c23;
public const uint  GL_INDEX_MODE                        =       0x0c30;
public const uint  GL_RGBA_MODE                         =       0x0c31;
public const uint  GL_DOUBLEBUFFER                      =       0x0c32;
public const uint  GL_STEREO                            =       0x0c33;
public const uint  GL_RENDER_MODE                       =       0x0c40;
public const uint  GL_PERSPECTIVE_CORRECTION_HINT       =       0x0c50;
public const uint  GL_POINT_SMOOTH_HINT                 =       0x0c51;
public const uint  GL_LINE_SMOOTH_HINT                  =       0x0c52;
public const uint  GL_POLYGON_SMOOTH_HINT               =       0x0c53;
public const uint  GL_FOG_HINT                          =       0x0c54;
public const uint  GL_TEXTURE_GEN_S                     =       0x0c60;
public const uint  GL_TEXTURE_GEN_T                     =       0x0c61;
public const uint  GL_TEXTURE_GEN_R                     =       0x0c62;
public const uint  GL_TEXTURE_GEN_Q                     =       0x0c63;
public const uint  GL_PIXEL_MAP_I_TO_I                  =       0x0c70;
public const uint  GL_PIXEL_MAP_S_TO_S                  =       0x0c71;
public const uint  GL_PIXEL_MAP_I_TO_R                  =       0x0c72;
public const uint  GL_PIXEL_MAP_I_TO_G                  =       0x0c73;
public const uint  GL_PIXEL_MAP_I_TO_B                  =       0x0c74;
public const uint  GL_PIXEL_MAP_I_TO_A                  =       0x0c75;
public const uint  GL_PIXEL_MAP_R_TO_R                  =       0x0c76;
public const uint  GL_PIXEL_MAP_G_TO_G                  =       0x0c77;
public const uint  GL_PIXEL_MAP_B_TO_B                  =       0x0c78;
public const uint  GL_PIXEL_MAP_A_TO_A                  =       0x0c79;
public const uint  GL_PIXEL_MAP_I_TO_I_SIZE             =       0x0cb0;
public const uint  GL_PIXEL_MAP_S_TO_S_SIZE             =       0x0cb1;
public const uint  GL_PIXEL_MAP_I_TO_R_SIZE             =       0x0cb2;
public const uint  GL_PIXEL_MAP_I_TO_G_SIZE             =       0x0cb3;
public const uint  GL_PIXEL_MAP_I_TO_B_SIZE             =       0x0cb4;
public const uint  GL_PIXEL_MAP_I_TO_A_SIZE             =       0x0cb5;
public const uint  GL_PIXEL_MAP_R_TO_R_SIZE             =       0x0cb6;
public const uint  GL_PIXEL_MAP_G_TO_G_SIZE             =       0x0cb7;
public const uint  GL_PIXEL_MAP_B_TO_B_SIZE             =       0x0cb8;
public const uint  GL_PIXEL_MAP_A_TO_A_SIZE             =       0x0cb9;
public const uint  GL_UNPACK_SWAP_BYTES                 =       0x0cf0;
public const uint  GL_UNPACK_LSB_FIRST                  =       0x0cf1;
public const uint  GL_UNPACK_ROW_LENGTH                 =       0x0cf2;
public const uint  GL_UNPACK_SKIP_ROWS                  =       0x0cf3;
public const uint  GL_UNPACK_SKIP_PIXELS                =       0x0cf4;
public const uint  GL_UNPACK_ALIGNMENT                  =       0x0cf5;
public const uint  GL_PACK_SWAP_BYTES                   =       0x0d00;
public const uint  GL_PACK_LSB_FIRST                    =       0x0d01;
public const uint  GL_PACK_ROW_LENGTH                   =       0x0d02;
public const uint  GL_PACK_SKIP_ROWS                    =       0x0d03;
public const uint  GL_PACK_SKIP_PIXELS                  =       0x0d04;
public const uint  GL_PACK_ALIGNMENT                    =       0x0d05;
public const uint  GL_MAP_COLOR                         =       0x0d10;
public const uint  GL_MAP_STENCIL                       =       0x0d11;
public const uint  GL_INDEX_SHIFT                       =       0x0d12;
public const uint  GL_INDEX_OFFSET                      =       0x0d13;
public const uint  GL_RED_SCALE                         =       0x0d14;
public const uint  GL_RED_BIAS                          =       0x0d15;
public const uint  GL_ZOOM_X                            =       0x0d16;
public const uint  GL_ZOOM_Y                            =       0x0d17;
public const uint  GL_GREEN_SCALE                       =       0x0d18;
public const uint  GL_GREEN_BIAS                        =       0x0d19;
public const uint  GL_BLUE_SCALE                        =       0x0d1a;
public const uint  GL_BLUE_BIAS                         =       0x0d1b;
public const uint  GL_ALPHA_SCALE                       =       0x0d1c;
public const uint  GL_ALPHA_BIAS                        =       0x0d1d;
public const uint  GL_DEPTH_SCALE                       =       0x0d1e;
public const uint  GL_DEPTH_BIAS                        =       0x0d1f;
public const uint  GL_MAX_EVAL_ORDER                    =       0x0d30;
public const uint  GL_MAX_LIGHTS                        =       0x0d31;
public const uint  GL_MAX_CLIP_PLANES                   =       0x0d32;
public const uint  GL_MAX_TEXTURE_SIZE                  =       0x0d33;
public const uint  GL_MAX_PIXEL_MAP_TABLE               =       0x0d34;
public const uint  GL_MAX_ATTRIB_STACK_DEPTH            =       0x0d35;
public const uint  GL_MAX_MODELVIEW_STACK_DEPTH         =       0x0d36;
public const uint  GL_MAX_NAME_STACK_DEPTH              =       0x0d37;
public const uint  GL_MAX_PROJECTION_STACK_DEPTH        =       0x0d38;
public const uint  GL_MAX_TEXTURE_STACK_DEPTH           =       0x0d39;
public const uint  GL_MAX_VIEWPORT_DIMS                 =       0x0d3a;
public const uint  GL_MAX_CLIENT_ATTRIB_STACK_DEPTH     =       0x0d3b;
public const uint  GL_SUBPIXEL_BITS                     =       0x0d50;
public const uint  GL_INDEX_BITS                        =       0x0d51;
public const uint  GL_RED_BITS                          =       0x0d52;
public const uint  GL_GREEN_BITS                        =       0x0d53;
public const uint  GL_BLUE_BITS                         =       0x0d54;
public const uint  GL_ALPHA_BITS                        =       0x0d55;
public const uint  GL_DEPTH_BITS                        =       0x0d56;
public const uint  GL_STENCIL_BITS                      =       0x0d57;
public const uint  GL_ACCUM_RED_BITS                    =       0x0d58;
public const uint  GL_ACCUM_GREEN_BITS                  =       0x0d59;
public const uint  GL_ACCUM_BLUE_BITS                   =       0x0d5a;
public const uint  GL_ACCUM_ALPHA_BITS                  =       0x0d5b;
public const uint  GL_NAME_STACK_DEPTH                  =       0x0d70;
public const uint  GL_AUTO_NORMAL                       =       0x0d80;
public const uint  GL_MAP1_COLOR_4                      =       0x0d90;
public const uint  GL_MAP1_INDEX                        =       0x0d91;
public const uint  GL_MAP1_NORMAL                       =       0x0d92;
public const uint  GL_MAP1_TEXTURE_COORD_1              =       0x0d93;
public const uint  GL_MAP1_TEXTURE_COORD_2              =       0x0d94;
public const uint  GL_MAP1_TEXTURE_COORD_3              =       0x0d95;
public const uint  GL_MAP1_TEXTURE_COORD_4              =       0x0d96;
public const uint  GL_MAP1_VERTEX_3                     =       0x0d97;
public const uint  GL_MAP1_VERTEX_4                     =       0x0d98;
public const uint  GL_MAP2_COLOR_4                      =       0x0db0;
public const uint  GL_MAP2_INDEX                        =       0x0db1;
public const uint  GL_MAP2_NORMAL                       =       0x0db2;
public const uint  GL_MAP2_TEXTURE_COORD_1              =       0x0db3;
public const uint  GL_MAP2_TEXTURE_COORD_2              =       0x0db4;
public const uint  GL_MAP2_TEXTURE_COORD_3              =       0x0db5;
public const uint  GL_MAP2_TEXTURE_COORD_4              =       0x0db6;
public const uint  GL_MAP2_VERTEX_3                     =       0x0db7;
public const uint  GL_MAP2_VERTEX_4                     =       0x0db8;
public const uint  GL_MAP1_GRID_DOMAIN                  =       0x0dd0;
public const uint  GL_MAP1_GRID_SEGMENTS                =       0x0dd1;
public const uint  GL_MAP2_GRID_DOMAIN                  =       0x0dd2;
public const uint  GL_MAP2_GRID_SEGMENTS                =       0x0dd3;
public const uint  GL_TEXTURE_1D                        =       0x0de0;
public const uint  GL_TEXTURE_2D                        =       0x0de1;
public const uint  GL_FEEDBACK_BUFFER_POINTER           =       0x0df0;
public const uint  GL_FEEDBACK_BUFFER_SIZE              =       0x0df1;
public const uint  GL_FEEDBACK_BUFFER_TYPE              =       0x0df2;
public const uint  GL_SELECTION_BUFFER_POINTER          =       0x0df3;
public const uint  GL_SELECTION_BUFFER_SIZE             =       0x0df4;
/*      GL_TEXTURE_BINDING_1D */
/*      GL_TEXTURE_BINDING_2D */
/*      GL_VERTEX_ARRAY */
/*      GL_NORMAL_ARRAY */
/*      GL_COLOR_ARRAY */
/*      GL_INDEX_ARRAY */
/*      GL_TEXTURE_COORD_ARRAY */
/*      GL_EDGE_FLAG_ARRAY */
/*      GL_VERTEX_ARRAY_SIZE */
/*      GL_VERTEX_ARRAY_TYPE */
/*      GL_VERTEX_ARRAY_STRIDE */
/*      GL_NORMAL_ARRAY_TYPE */
/*      GL_NORMAL_ARRAY_STRIDE */
/*      GL_COLOR_ARRAY_SIZE */
/*      GL_COLOR_ARRAY_TYPE */
/*      GL_COLOR_ARRAY_STRIDE */
/*      GL_INDEX_ARRAY_TYPE */
/*      GL_INDEX_ARRAY_STRIDE */
/*      GL_TEXTURE_COORD_ARRAY_SIZE */
/*      GL_TEXTURE_COORD_ARRAY_TYPE */
/*      GL_TEXTURE_COORD_ARRAY_STRIDE */
/*      GL_EDGE_FLAG_ARRAY_STRIDE */
/*      GL_POLYGON_OFFSET_FACTOR */
/*      GL_POLYGON_OFFSET_UNITS */

/* GetTextureParameter */
/*      GL_TEXTURE_MAG_FILTER */
/*      GL_TEXTURE_MIN_FILTER */
/*      GL_TEXTURE_WRAP_S */
/*      GL_TEXTURE_WRAP_T */
public const uint  GL_TEXTURE_WIDTH                     =       0x1000;
public const uint  GL_TEXTURE_HEIGHT                    =       0x1001;
public const uint  GL_TEXTURE_INTERNAL_FORMAT           =       0x1003;
public const uint  GL_TEXTURE_BORDER_COLOR              =       0x1004;
public const uint  GL_TEXTURE_BORDER                    =       0x1005;
/*      GL_TEXTURE_RED_SIZE */
/*      GL_TEXTURE_GREEN_SIZE */
/*      GL_TEXTURE_BLUE_SIZE */
/*      GL_TEXTURE_ALPHA_SIZE */
/*      GL_TEXTURE_LUMINANCE_SIZE */
/*      GL_TEXTURE_INTENSITY_SIZE */
/*      GL_TEXTURE_PRIORITY */
/*      GL_TEXTURE_RESIDENT */

/* HintMode */
public const uint  GL_DONT_CARE                         =       0x1100;
public const uint  GL_FASTEST                           =       0x1101;
public const uint  GL_NICEST                            =       0x1102;

/* HintTarget */
/*      GL_PERSPECTIVE_CORRECTION_HINT */
/*      GL_POINT_SMOOTH_HINT */
/*      GL_LINE_SMOOTH_HINT */
/*      GL_POLYGON_SMOOTH_HINT */
/*      GL_FOG_HINT */
/*      GL_PHONG_HINT */

/* IndexPointerType */
/*      GL_SHORT */
/*      GL_INT */
/*      GL_FLOAT */
/*      GL_DOUBLE */

/* LightModelParameter */
/*      GL_LIGHT_MODEL_AMBIENT */
/*      GL_LIGHT_MODEL_LOCAL_VIEWER */
/*      GL_LIGHT_MODEL_TWO_SIDE */

/* LightName */
public const uint  GL_LIGHT0                            =       0x4000;
public const uint  GL_LIGHT1                            =       0x4001;
public const uint  GL_LIGHT2                            =       0x4002;
public const uint  GL_LIGHT3                            =       0x4003;
public const uint  GL_LIGHT4                            =       0x4004;
public const uint  GL_LIGHT5                            =       0x4005;
public const uint  GL_LIGHT6                            =       0x4006;
public const uint  GL_LIGHT7                            =       0x4007;

/* LightParameter */
public const uint  GL_AMBIENT                           =       0x1200;
public const uint  GL_DIFFUSE                           =       0x1201;
public const uint  GL_SPECULAR                          =       0x1202;
public const uint  GL_POSITION                          =       0x1203;
public const uint  GL_SPOT_DIRECTION                    =       0x1204;
public const uint  GL_SPOT_EXPONENT                     =       0x1205;
public const uint  GL_SPOT_CUTOFF                       =       0x1206;
public const uint  GL_CONSTANT_ATTENUATION              =       0x1207;
public const uint  GL_LINEAR_ATTENUATION                =       0x1208;
public const uint  GL_QUADRATIC_ATTENUATION             =       0x1209;

/* InterleavedArrays */
/*      GL_V2F */
/*      GL_V3F */
/*      GL_C4UB_V2F */
/*      GL_C4UB_V3F */
/*      GL_C3F_V3F */
/*      GL_N3F_V3F */
/*      GL_C4F_N3F_V3F */
/*      GL_T2F_V3F */
/*      GL_T4F_V4F */
/*      GL_T2F_C4UB_V3F */
/*      GL_T2F_C3F_V3F */
/*      GL_T2F_N3F_V3F */
/*      GL_T2F_C4F_N3F_V3F */
/*      GL_T4F_C4F_N3F_V4F */

/* ListMode */
public const uint  GL_COMPILE                           =       0x1300;
public const uint  GL_COMPILE_AND_EXECUTE               =       0x1301;

/* ListNameType */
/*      GL_BYTE */
/*      GL_UNSIGNED_BYTE */
/*      GL_SHORT */
/*      GL_UNSIGNED_SHORT */
/*      GL_INT */
/*      GL_UNSIGNED_INT */
/*      GL_FLOAT */
/*      GL_2_BYTES */
/*      GL_3_BYTES */
/*      GL_4_BYTES */

/* LogicOp */
public const uint  GL_CLEAR                             =       0x1500;
public const uint  GL_AND                               =       0x1501;
public const uint  GL_AND_REVERSE                       =       0x1502;
public const uint  GL_COPY                              =       0x1503;
public const uint  GL_AND_INVERTED                      =       0x1504;
public const uint  GL_NOOP                              =       0x1505;
public const uint  GL_XOR                               =       0x1506;
public const uint  GL_OR                                =       0x1507;
public const uint  GL_NOR                               =       0x1508;
public const uint  GL_EQUIV                             =       0x1509;
public const uint  GL_INVERT                            =       0x150a;
public const uint  GL_OR_REVERSE                        =       0x150b;
public const uint  GL_COPY_INVERTED                     =       0x150c;
public const uint  GL_OR_INVERTED                       =       0x150d;
public const uint  GL_NAND                              =       0x150e;
public const uint  GL_SET                               =       0x150f;

/* MapTarget */
/*      GL_MAP1_COLOR_4 */
/*      GL_MAP1_INDEX */
/*      GL_MAP1_NORMAL */
/*      GL_MAP1_TEXTURE_COORD_1 */
/*      GL_MAP1_TEXTURE_COORD_2 */
/*      GL_MAP1_TEXTURE_COORD_3 */
/*      GL_MAP1_TEXTURE_COORD_4 */
/*      GL_MAP1_VERTEX_3 */
/*      GL_MAP1_VERTEX_4 */
/*      GL_MAP2_COLOR_4 */
/*      GL_MAP2_INDEX */
/*      GL_MAP2_NORMAL */
/*      GL_MAP2_TEXTURE_COORD_1 */
/*      GL_MAP2_TEXTURE_COORD_2 */
/*      GL_MAP2_TEXTURE_COORD_3 */
/*      GL_MAP2_TEXTURE_COORD_4 */
/*      GL_MAP2_VERTEX_3 */
/*      GL_MAP2_VERTEX_4 */

/* MaterialFace */
/*      GL_FRONT */
/*      GL_BACK */
/*      GL_FRONT_AND_BACK */

/* MaterialParameter */
public const uint  GL_EMISSION                          =       0x1600;
public const uint  GL_SHININESS                         =       0x1601;
public const uint  GL_AMBIENT_AND_DIFFUSE               =       0x1602;
public const uint  GL_COLOR_INDEXES                     =       0x1603;
/*      GL_AMBIENT */
/*      GL_DIFFUSE */
/*      GL_SPECULAR */

/* MatrixMode */
public const uint  GL_MODELVIEW                         =       0x1700;
public const uint  GL_PROJECTION                        =       0x1701;
public const uint  GL_TEXTURE                           =       0x1702;

/* MeshMode1 */
/*      GL_POINT */
/*      GL_LINE */

/* MeshMode2 */
/*      GL_POINT */
/*      GL_LINE */
/*      GL_FILL */

/* NormalPointerType */
/*      GL_BYTE */
/*      GL_SHORT */
/*      GL_INT */
/*      GL_FLOAT */
/*      GL_DOUBLE */

/* PixelCopyType */
public const uint  GL_COLOR                             =       0x1800;
public const uint  GL_DEPTH                             =       0x1801;
public const uint  GL_STENCIL                           =       0x1802;

/* PixelFormat */
public const uint  GL_COLOR_INDEX                       =       0x1900;
public const uint  GL_STENCIL_INDEX                     =       0x1901;
public const uint  GL_DEPTH_COMPONENT                   =       0x1902;
public const uint  GL_RED                               =       0x1903;
public const uint  GL_GREEN                             =       0x1904;
public const uint  GL_BLUE                              =       0x1905;
public const uint  GL_ALPHA                             =       0x1906;
public const uint  GL_RGB                               =       0x1907;
public const uint  GL_RGBA                              =       0x1908;
public const uint  GL_LUMINANCE                         =       0x1909;
public const uint  GL_LUMINANCE_ALPHA                   =       0x190a;

/* PixelMap */
/*      GL_PIXEL_MAP_I_TO_I */
/*      GL_PIXEL_MAP_S_TO_S */
/*      GL_PIXEL_MAP_I_TO_R */
/*      GL_PIXEL_MAP_I_TO_G */
/*      GL_PIXEL_MAP_I_TO_B */
/*      GL_PIXEL_MAP_I_TO_A */
/*      GL_PIXEL_MAP_R_TO_R */
/*      GL_PIXEL_MAP_G_TO_G */
/*      GL_PIXEL_MAP_B_TO_B */
/*      GL_PIXEL_MAP_A_TO_A */

/* PixelStore */
/*      GL_UNPACK_SWAP_BYTES */
/*      GL_UNPACK_LSB_FIRST */
/*      GL_UNPACK_ROW_LENGTH */
/*      GL_UNPACK_SKIP_ROWS */
/*      GL_UNPACK_SKIP_PIXELS */
/*      GL_UNPACK_ALIGNMENT */
/*      GL_PACK_SWAP_BYTES */
/*      GL_PACK_LSB_FIRST */
/*      GL_PACK_ROW_LENGTH */
/*      GL_PACK_SKIP_ROWS */
/*      GL_PACK_SKIP_PIXELS */
/*      GL_PACK_ALIGNMENT */

/* PixelTransfer */
/*      GL_MAP_COLOR */
/*      GL_MAP_STENCIL */
/*      GL_INDEX_SHIFT */
/*      GL_INDEX_OFFSET */
/*      GL_RED_SCALE */
/*      GL_RED_BIAS */
/*      GL_GREEN_SCALE */
/*      GL_GREEN_BIAS */
/*      GL_BLUE_SCALE */
/*      GL_BLUE_BIAS */
/*      GL_ALPHA_SCALE */
/*      GL_ALPHA_BIAS */
/*      GL_DEPTH_SCALE */
/*      GL_DEPTH_BIAS */

/* PixelType */
public const uint  GL_BITMAP                            =       0x1a00;
/*      GL_BYTE */
/*      GL_UNSIGNED_BYTE */
/*      GL_SHORT */
/*      GL_UNSIGNED_SHORT */
/*      GL_INT */
/*      GL_UNSIGNED_INT */
/*      GL_FLOAT */

/* PolygonMode */
public const uint  GL_POINT                             =       0x1b00;
public const uint  GL_LINE                              =       0x1b01;
public const uint  GL_FILL                              =       0x1b02;

/* ReadBufferMode */
/*      GL_FRONT_LEFT */
/*      GL_FRONT_RIGHT */
/*      GL_BACK_LEFT */
/*      GL_BACK_RIGHT */
/*      GL_FRONT */
/*      GL_BACK */
/*      GL_LEFT */
/*      GL_RIGHT */
/*      GL_AUX0 */
/*      GL_AUX1 */
/*      GL_AUX2 */
/*      GL_AUX3 */

/* RenderingMode */
public const uint  GL_RENDER                            =       0x1c00;
public const uint  GL_FEEDBACK                          =       0x1c01;
public const uint  GL_SELECT                            =       0x1c02;

/* ShadingModel */
public const uint  GL_FLAT                              =       0x1d00;
public const uint  GL_SMOOTH                            =       0x1d01;

/* StencilFunction */
/*      GL_NEVER */
/*      GL_LESS */
/*      GL_EQUAL */
/*      GL_LEQUAL */
/*      GL_GREATER */
/*      GL_NOTEQUAL */
/*      GL_GEQUAL */
/*      GL_ALWAYS */

/* StencilOp */
/*      GL_ZERO */
public const uint  GL_KEEP                              =       0x1e00;
public const uint  GL_REPLACE                           =       0x1e01;
public const uint  GL_INCR                              =       0x1e02;
public const uint  GL_DECR                              =       0x1e03;
/*      GL_INVERT */

/* StringName */
public const uint  GL_VENDOR                            =       0x1f00;
public const uint  GL_RENDERER                          =       0x1f01;
public const uint  GL_VERSION                           =       0x1f02;
public const uint  GL_EXTENSIONS                        =       0x1f03;

/* TextureCoordName */
public const uint  GL_S                                 =       0x2000;
public const uint  GL_T                                 =       0x2001;
public const uint  GL_R                                 =       0x2002;
public const uint  GL_Q                                 =       0x2003;

/* TexCoordPointerType */
/*      GL_SHORT */
/*      GL_INT */
/*      GL_FLOAT */
/*      GL_DOUBLE */

/* TextureEnvMode */
public const uint  GL_MODULATE                          =       0x2100;
public const uint  GL_DECAL                             =       0x2101;
/*      GL_BLEND */
/*      GL_REPLACE */

/* TextureEnvParameter */
public const uint  GL_TEXTURE_ENV_MODE                  =       0x2200;
public const uint  GL_TEXTURE_ENV_COLOR                 =       0x2201;

/* TextureEnvTarget */
public const uint  GL_TEXTURE_ENV                       =       0x2300;

/* TextureGenMode */
public const uint  GL_EYE_LINEAR                        =       0x2400;
public const uint  GL_OBJECT_LINEAR                     =       0x2401;
public const uint  GL_SPHERE_MAP                        =       0x2402;

/* TextureGenParameter */
public const uint  GL_TEXTURE_GEN_MODE                  =       0x2500;
public const uint  GL_OBJECT_PLANE                      =       0x2501;
public const uint  GL_EYE_PLANE                         =       0x2502;

/* TextureMagFilter */
public const uint  GL_NEAREST                           =       0x2600;
public const uint  GL_LINEAR                            =       0x2601;

/* TextureMinFilter */
/*      GL_NEAREST */
/*      GL_LINEAR */
public const uint  GL_NEAREST_MIPMAP_NEAREST            =       0x2700;
public const uint  GL_LINEAR_MIPMAP_NEAREST             =       0x2701;
public const uint  GL_NEAREST_MIPMAP_LINEAR             =       0x2702;
public const uint  GL_LINEAR_MIPMAP_LINEAR              =       0x2703;

/* TextureParameterName */
public const uint  GL_TEXTURE_MAG_FILTER                =       0x2800;
public const uint  GL_TEXTURE_MIN_FILTER                =       0x2801;
public const uint  GL_TEXTURE_WRAP_S                    =       0x2802;
public const uint  GL_TEXTURE_WRAP_T                    =       0x2803;
/*      GL_TEXTURE_BORDER_COLOR */
/*      GL_TEXTURE_PRIORITY */

/* TextureTarget */
/*      GL_TEXTURE_1D */
/*      GL_TEXTURE_2D */
/*      GL_PROXY_TEXTURE_1D */
/*      GL_PROXY_TEXTURE_2D */

/* TextureWrapMode */
public const uint  GL_CLAMP                             =       0x2900;
public const uint  GL_REPEAT                            =       0x2901;

/* VertexPointerType */
/*      GL_SHORT */
/*      GL_INT */
/*      GL_FLOAT */
/*      GL_DOUBLE */

/* ClientAttribMask */
public const uint  GL_CLIENT_PIXEL_STORE_BIT            =   0x00000001;
public const uint  GL_CLIENT_VERTEX_ARRAY_BIT           =   0x00000002;
public const uint  GL_CLIENT_ALL_ATTRIB_BITS            =   0xffffffff;

/* polygon_offset */
public const uint  GL_POLYGON_OFFSET_FACTOR             =       0x8038;
public const uint  GL_POLYGON_OFFSET_UNITS              =       0x2a00;
public const uint  GL_POLYGON_OFFSET_POINT              =       0x2a01;
public const uint  GL_POLYGON_OFFSET_LINE               =       0x2a02;
public const uint  GL_POLYGON_OFFSET_FILL               =       0x8037;

/* texture */
public const uint  GL_ALPHA4                            =       0x803b;
public const uint  GL_ALPHA8                            =       0x803c;
public const uint  GL_ALPHA12                           =       0x803d;
public const uint  GL_ALPHA16                           =       0x803e;
public const uint  GL_LUMINANCE4                        =       0x803f;
public const uint  GL_LUMINANCE8                        =       0x8040;
public const uint  GL_LUMINANCE12                       =       0x8041;
public const uint  GL_LUMINANCE16                       =       0x8042;
public const uint  GL_LUMINANCE4_ALPHA4                 =       0x8043;
public const uint  GL_LUMINANCE6_ALPHA2                 =       0x8044;
public const uint  GL_LUMINANCE8_ALPHA8                 =       0x8045;
public const uint  GL_LUMINANCE12_ALPHA4                =       0x8046;
public const uint  GL_LUMINANCE12_ALPHA12               =       0x8047;
public const uint  GL_LUMINANCE16_ALPHA16               =       0x8048;
public const uint  GL_INTENSITY                         =       0x8049;
public const uint  GL_INTENSITY4                        =       0x804a;
public const uint  GL_INTENSITY8                        =       0x804b;
public const uint  GL_INTENSITY12                       =       0x804c;
public const uint  GL_INTENSITY16                       =       0x804d;
public const uint  GL_R3_G3_B2                          =       0x2a10;
public const uint  GL_RGB4                              =       0x804f;
public const uint  GL_RGB5                              =       0x8050;
public const uint  GL_RGB8                              =       0x8051;
public const uint  GL_RGB10                             =       0x8052;
public const uint  GL_RGB12                             =       0x8053;
public const uint  GL_RGB16                             =       0x8054;
public const uint  GL_RGBA2                             =       0x8055;
public const uint  GL_RGBA4                             =       0x8056;
public const uint  GL_RGB5_A1                           =       0x8057;
public const uint  GL_RGBA8                             =       0x8058;
public const uint  GL_RGB10_A2                          =       0x8059;
public const uint  GL_RGBA12                            =       0x805a;
public const uint  GL_RGBA16                            =       0x805b;
public const uint  GL_TEXTURE_RED_SIZE                  =       0x805c;
public const uint  GL_TEXTURE_GREEN_SIZE                =       0x805d;
public const uint  GL_TEXTURE_BLUE_SIZE                 =       0x805e;
public const uint  GL_TEXTURE_ALPHA_SIZE                =       0x805f;
public const uint  GL_TEXTURE_LUMINANCE_SIZE            =       0x8060;
public const uint  GL_TEXTURE_INTENSITY_SIZE            =       0x8061;
public const uint  GL_PROXY_TEXTURE_1D                  =       0x8063;
public const uint  GL_PROXY_TEXTURE_2D                  =       0x8064;

/* texture_object */
public const uint  GL_TEXTURE_PRIORITY                  =       0x8066;
public const uint  GL_TEXTURE_RESIDENT                  =       0x8067;
public const uint  GL_TEXTURE_BINDING_1D                =       0x8068;
public const uint  GL_TEXTURE_BINDING_2D                =       0x8069;

/* vertex_array */
public const uint  GL_VERTEX_ARRAY                      =       0x8074;
public const uint  GL_NORMAL_ARRAY                      =       0x8075;
public const uint  GL_COLOR_ARRAY                       =       0x8076;
public const uint  GL_INDEX_ARRAY                       =       0x8077;
public const uint  GL_TEXTURE_COORD_ARRAY               =       0x8078;
public const uint  GL_EDGE_FLAG_ARRAY                   =       0x8079;
public const uint  GL_VERTEX_ARRAY_SIZE                 =       0x807a;
public const uint  GL_VERTEX_ARRAY_TYPE                 =       0x807b;
public const uint  GL_VERTEX_ARRAY_STRIDE               =       0x807c;
public const uint  GL_NORMAL_ARRAY_TYPE                 =       0x807e;
public const uint  GL_NORMAL_ARRAY_STRIDE               =       0x807f;
public const uint  GL_COLOR_ARRAY_SIZE                  =       0x8081;
public const uint  GL_COLOR_ARRAY_TYPE                  =       0x8082;
public const uint  GL_COLOR_ARRAY_STRIDE                =       0x8083;
public const uint  GL_INDEX_ARRAY_TYPE                  =       0x8085;
public const uint  GL_INDEX_ARRAY_STRIDE                =       0x8086;
public const uint  GL_TEXTURE_COORD_ARRAY_SIZE          =       0x8088;
public const uint  GL_TEXTURE_COORD_ARRAY_TYPE          =       0x8089;
public const uint  GL_TEXTURE_COORD_ARRAY_STRIDE        =       0x808a;
public const uint  GL_EDGE_FLAG_ARRAY_STRIDE            =       0x808c;
public const uint  GL_VERTEX_ARRAY_POINTER              =       0x808e;
public const uint  GL_NORMAL_ARRAY_POINTER              =       0x808f;
public const uint  GL_COLOR_ARRAY_POINTER               =       0x8090;
public const uint  GL_INDEX_ARRAY_POINTER               =       0x8091;
public const uint  GL_TEXTURE_COORD_ARRAY_POINTER       =       0x8092;
public const uint  GL_EDGE_FLAG_ARRAY_POINTER           =       0x8093;
public const uint  GL_V2F                               =       0x2a20;
public const uint  GL_V3F                               =       0x2a21;
public const uint  GL_C4UB_V2F                          =       0x2a22;
public const uint  GL_C4UB_V3F                          =       0x2a23;
public const uint  GL_C3F_V3F                           =       0x2a24;
public const uint  GL_N3F_V3F                           =       0x2a25;
public const uint  GL_C4F_N3F_V3F                       =       0x2a26;
public const uint  GL_T2F_V3F                           =       0x2a27;
public const uint  GL_T4F_V4F                           =       0x2a28;
public const uint  GL_T2F_C4UB_V3F                      =       0x2a29;
public const uint  GL_T2F_C3F_V3F                       =       0x2a2a;
public const uint  GL_T2F_N3F_V3F                       =       0x2a2b;
public const uint  GL_T2F_C4F_N3F_V3F                   =       0x2a2c;
public const uint  GL_T4F_C4F_N3F_V4F                   =       0x2a2d;

/* Extensions */
public const uint  GL_EXT_vertex_array                  =            1;
public const uint  GL_EXT_bgra                          =            1;
public const uint  GL_EXT_paletted_texture              =            1;
public const uint  GL_WIN_swap_hint                     =            1;
public const uint  GL_WIN_draw_range_elements           =            1;
// #define GL_WIN_phong_shading              1
// #define GL_WIN_specular_fog               1

/* EXT_vertex_array */
public const uint  GL_VERTEX_ARRAY_EXT                  =       0x8074;
public const uint  GL_NORMAL_ARRAY_EXT                  =       0x8075;
public const uint  GL_COLOR_ARRAY_EXT                   =       0x8076;
public const uint  GL_INDEX_ARRAY_EXT                   =       0x8077;
public const uint  GL_TEXTURE_COORD_ARRAY_EXT           =       0x8078;
public const uint  GL_EDGE_FLAG_ARRAY_EXT               =       0x8079;
public const uint  GL_VERTEX_ARRAY_SIZE_EXT             =       0x807a;
public const uint  GL_VERTEX_ARRAY_TYPE_EXT             =       0x807b;
public const uint  GL_VERTEX_ARRAY_STRIDE_EXT           =       0x807c;
public const uint  GL_VERTEX_ARRAY_COUNT_EXT            =       0x807d;
public const uint  GL_NORMAL_ARRAY_TYPE_EXT             =       0x807e;
public const uint  GL_NORMAL_ARRAY_STRIDE_EXT           =       0x807f;
public const uint  GL_NORMAL_ARRAY_COUNT_EXT            =       0x8080;
public const uint  GL_COLOR_ARRAY_SIZE_EXT              =       0x8081;
public const uint  GL_COLOR_ARRAY_TYPE_EXT              =       0x8082;
public const uint  GL_COLOR_ARRAY_STRIDE_EXT            =       0x8083;
public const uint  GL_COLOR_ARRAY_COUNT_EXT             =       0x8084;
public const uint  GL_INDEX_ARRAY_TYPE_EXT              =       0x8085;
public const uint  GL_INDEX_ARRAY_STRIDE_EXT            =       0x8086;
public const uint  GL_INDEX_ARRAY_COUNT_EXT             =       0x8087;
public const uint  GL_TEXTURE_COORD_ARRAY_SIZE_EXT      =       0x8088;
public const uint  GL_TEXTURE_COORD_ARRAY_TYPE_EXT      =       0x8089;
public const uint  GL_TEXTURE_COORD_ARRAY_STRIDE_EXT    =       0x808a;
public const uint  GL_TEXTURE_COORD_ARRAY_COUNT_EXT     =       0x808b;
public const uint  GL_EDGE_FLAG_ARRAY_STRIDE_EXT        =       0x808c;
public const uint  GL_EDGE_FLAG_ARRAY_COUNT_EXT         =       0x808d;
public const uint  GL_VERTEX_ARRAY_POINTER_EXT          =       0x808e;
public const uint  GL_NORMAL_ARRAY_POINTER_EXT          =       0x808f;
public const uint  GL_COLOR_ARRAY_POINTER_EXT           =       0x8090;
public const uint  GL_INDEX_ARRAY_POINTER_EXT           =       0x8091;
public const uint  GL_TEXTURE_COORD_ARRAY_POINTER_EXT   =       0x8092;
public const uint  GL_EDGE_FLAG_ARRAY_POINTER_EXT       =       0x8093;
public const uint  GL_DOUBLE_EXT                        =    GL_DOUBLE;

/* EXT_bgra */
public const uint  GL_BGR_EXT                           =       0x80e0;
public const uint  GL_BGRA_EXT                          =       0x80e1;

/* EXT_paletted_texture */

/* These must match the GL_COLOR_TABLE_*_SGI enumerants */
public const uint  GL_COLOR_TABLE_FORMAT_EXT            =       0x80d8;
public const uint  GL_COLOR_TABLE_WIDTH_EXT             =       0x80d9;
public const uint  GL_COLOR_TABLE_RED_SIZE_EXT          =       0x80da;
public const uint  GL_COLOR_TABLE_GREEN_SIZE_EXT        =       0x80db;
public const uint  GL_COLOR_TABLE_BLUE_SIZE_EXT         =       0x80dc;
public const uint  GL_COLOR_TABLE_ALPHA_SIZE_EXT        =       0x80dd;
public const uint  GL_COLOR_TABLE_LUMINANCE_SIZE_EXT    =       0x80de;
public const uint  GL_COLOR_TABLE_INTENSITY_SIZE_EXT    =       0x80df;

public const uint  GL_COLOR_INDEX1_EXT                  =       0x80e2;
public const uint  GL_COLOR_INDEX2_EXT                  =       0x80e3;
public const uint  GL_COLOR_INDEX4_EXT                  =       0x80e4;
public const uint  GL_COLOR_INDEX8_EXT                  =       0x80e5;
public const uint  GL_COLOR_INDEX12_EXT                 =       0x80e6;
public const uint  GL_COLOR_INDEX16_EXT                 =       0x80e7;

/* WIN_draw_range_elements */
public const uint  GL_MAX_ELEMENTS_VERTICES_WIN         =       0x80e8;
public const uint  GL_MAX_ELEMENTS_INDICES_WIN          =       0x80e9;

/* WIN_phong_shading */
public const uint  GL_PHONG_WIN                         =       0x80ea;
public const uint  GL_PHONG_HINT_WIN                    =       0x80eb;

/* WIN_specular_fog */
public const uint  GL_FOG_SPECULAR_TEXTURE_WIN          =       0x80ec;

/* For compatibility with OpenGL v1.0 */
public const uint  GL_LOGIC_OP                          = GL_INDEX_LOGIC_OP;
public const uint  GL_TEXTURE_COMPONENTS                = GL_TEXTURE_INTERNAL_FORMAT;

/*************************************************************/

/* EXT_vertex_array */

/* WIN_draw_range_elements */

/* WIN_swap_hint */

/* EXT_paletted_texture */

/* __GL_H__ */
/* __gl_h_ */
//
//
// ############################################################################
// ############################################################################
// ############################################################################
// ############################################################################
//
//
// OpenGL functions from GL.H with pointer types intact.
//
//
[DllImport(GL_DLL)] public unsafe static extern void   glAccum                  ( uint op, float valuex );
[DllImport(GL_DLL)] public unsafe static extern void   glAlphaFunc              ( uint func, float refx );
[DllImport(GL_DLL)] public unsafe static extern byte   glAreTexturesResident    ( int n, uint * textures, byte * residences );
[DllImport(GL_DLL)] public unsafe static extern void   glArrayElement           ( int i );
[DllImport(GL_DLL)] public unsafe static extern void   glBegin                  ( uint mode );
[DllImport(GL_DLL)] public unsafe static extern void   glBindTexture            ( uint target, uint texture );
[DllImport(GL_DLL)] public unsafe static extern void   glBitmap                 ( int width, int height, float xorig, float yorig, float xmove, float ymove, byte * bitmap );
[DllImport(GL_DLL)] public unsafe static extern void   glBlendFunc              ( uint sfactor, uint dfactor );
[DllImport(GL_DLL)] public unsafe static extern void   glCallList               ( uint list );
[DllImport(GL_DLL)] public unsafe static extern void   glCallLists              ( int n, uint type, void * lists );
[DllImport(GL_DLL)] public unsafe static extern void   glClear                  ( uint mask );
[DllImport(GL_DLL)] public unsafe static extern void   glClearAccum             ( float red, float green, float blue, float alpha );
[DllImport(GL_DLL)] public unsafe static extern void   glClearColor             ( float red, float green, float blue, float alpha );
[DllImport(GL_DLL)] public unsafe static extern void   glClearDepth             ( double depth );
[DllImport(GL_DLL)] public unsafe static extern void   glClearIndex             ( float c );
[DllImport(GL_DLL)] public unsafe static extern void   glClearStencil           ( int s );
[DllImport(GL_DLL)] public unsafe static extern void   glClipPlane              ( uint plane, double * equation );
[DllImport(GL_DLL)] public unsafe static extern void   glColor3b                ( sbyte red, sbyte green, sbyte blue );
[DllImport(GL_DLL)] public unsafe static extern void   glColor3bv               ( sbyte * v );
[DllImport(GL_DLL)] public unsafe static extern void   glColor3d                ( double red, double green, double blue );
[DllImport(GL_DLL)] public unsafe static extern void   glColor3dv               ( double * v );
[DllImport(GL_DLL)] public unsafe static extern void   glColor3f                ( float red, float green, float blue );
[DllImport(GL_DLL)] public unsafe static extern void   glColor3fv               ( float * v );
[DllImport(GL_DLL)] public unsafe static extern void   glColor3i                ( int red, int green, int blue );
[DllImport(GL_DLL)] public unsafe static extern void   glColor3iv               ( int * v );
[DllImport(GL_DLL)] public unsafe static extern void   glColor3s                ( short red, short green, short blue );
[DllImport(GL_DLL)] public unsafe static extern void   glColor3sv               ( short * v );
[DllImport(GL_DLL)] public unsafe static extern void   glColor3ub               ( byte red, byte green, byte blue );
[DllImport(GL_DLL)] public unsafe static extern void   glColor3ubv              ( byte * v );
[DllImport(GL_DLL)] public unsafe static extern void   glColor3ui               ( uint red, uint green, uint blue );
[DllImport(GL_DLL)] public unsafe static extern void   glColor3uiv              ( uint * v );
[DllImport(GL_DLL)] public unsafe static extern void   glColor3us               ( ushort red, ushort green, ushort blue );
[DllImport(GL_DLL)] public unsafe static extern void   glColor3usv              ( ushort * v );
[DllImport(GL_DLL)] public unsafe static extern void   glColor4b                ( sbyte red, sbyte green, sbyte blue, sbyte alpha );
[DllImport(GL_DLL)] public unsafe static extern void   glColor4bv               ( sbyte * v );
[DllImport(GL_DLL)] public unsafe static extern void   glColor4d                ( double red, double green, double blue, double alpha );
[DllImport(GL_DLL)] public unsafe static extern void   glColor4dv               ( double * v );
[DllImport(GL_DLL)] public unsafe static extern void   glColor4f                ( float red, float green, float blue, float alpha );
[DllImport(GL_DLL)] public unsafe static extern void   glColor4fv               ( float * v );
[DllImport(GL_DLL)] public unsafe static extern void   glColor4i                ( int red, int green, int blue, int alpha );
[DllImport(GL_DLL)] public unsafe static extern void   glColor4iv               ( int * v );
[DllImport(GL_DLL)] public unsafe static extern void   glColor4s                ( short red, short green, short blue, short alpha );
[DllImport(GL_DLL)] public unsafe static extern void   glColor4sv               ( short * v );
[DllImport(GL_DLL)] public unsafe static extern void   glColor4ub               ( byte red, byte green, byte blue, byte alpha );
[DllImport(GL_DLL)] public unsafe static extern void   glColor4ubv              ( byte * v );
[DllImport(GL_DLL)] public unsafe static extern void   glColor4ui               ( uint red, uint green, uint blue, uint alpha );
[DllImport(GL_DLL)] public unsafe static extern void   glColor4uiv              ( uint * v );
[DllImport(GL_DLL)] public unsafe static extern void   glColor4us               ( ushort red, ushort green, ushort blue, ushort alpha );
[DllImport(GL_DLL)] public unsafe static extern void   glColor4usv              ( ushort * v );
[DllImport(GL_DLL)] public unsafe static extern void   glColorMask              ( byte red, byte green, byte blue, byte alpha );
[DllImport(GL_DLL)] public unsafe static extern void   glColorMaterial          ( uint face, uint mode );
[DllImport(GL_DLL)] public unsafe static extern void   glColorPointer           ( int size, uint type, int stride, void * pointer );
[DllImport(GL_DLL)] public unsafe static extern void   glCopyPixels             ( int x, int y, int width, int height, uint type );
[DllImport(GL_DLL)] public unsafe static extern void   glCopyTexImage1D         ( uint target, int level, uint internalFormat, int x, int y, int width, int border );
[DllImport(GL_DLL)] public unsafe static extern void   glCopyTexImage2D         ( uint target, int level, uint internalFormat, int x, int y, int width, int height, int border );
[DllImport(GL_DLL)] public unsafe static extern void   glCopyTexSubImage1D      ( uint target, int level, int xoffset, int x, int y, int width );
[DllImport(GL_DLL)] public unsafe static extern void   glCopyTexSubImage2D      ( uint target, int level, int xoffset, int yoffset, int x, int y, int width, int height );
[DllImport(GL_DLL)] public unsafe static extern void   glCullFace               ( uint mode );
[DllImport(GL_DLL)] public unsafe static extern void   glDeleteLists            ( uint list, int range );
[DllImport(GL_DLL)] public unsafe static extern void   glDeleteTextures         ( int n, uint * textures );
[DllImport(GL_DLL)] public unsafe static extern void   glDepthFunc              ( uint func );
[DllImport(GL_DLL)] public unsafe static extern void   glDepthMask              ( byte flag );
[DllImport(GL_DLL)] public unsafe static extern void   glDepthRange             ( double zNear, double zFar );
[DllImport(GL_DLL)] public unsafe static extern void   glDisable                ( uint cap );
[DllImport(GL_DLL)] public unsafe static extern void   glDisableClientState     ( uint array );
[DllImport(GL_DLL)] public unsafe static extern void   glDrawArrays             ( uint mode, int first, int count );
[DllImport(GL_DLL)] public unsafe static extern void   glDrawBuffer             ( uint mode );
[DllImport(GL_DLL)] public unsafe static extern void   glDrawElements           ( uint mode, int count, uint type, void * indices );
[DllImport(GL_DLL)] public unsafe static extern void   glDrawPixels             ( int width, int height, uint format, uint type, void * pixels );
[DllImport(GL_DLL)] public unsafe static extern void   glEdgeFlag               ( byte flag );
[DllImport(GL_DLL)] public unsafe static extern void   glEdgeFlagPointer        ( int stride, void * pointer );
[DllImport(GL_DLL)] public unsafe static extern void   glEdgeFlagv              ( byte * flag );
[DllImport(GL_DLL)] public unsafe static extern void   glEnable                 ( uint cap );
[DllImport(GL_DLL)] public unsafe static extern void   glEnableClientState      ( uint array );
[DllImport(GL_DLL)] public unsafe static extern void   glEnd                    ( );
[DllImport(GL_DLL)] public unsafe static extern void   glEndList                ( );
[DllImport(GL_DLL)] public unsafe static extern void   glEvalCoord1d            ( double u );
[DllImport(GL_DLL)] public unsafe static extern void   glEvalCoord1dv           ( double * u );
[DllImport(GL_DLL)] public unsafe static extern void   glEvalCoord1f            ( float u );
[DllImport(GL_DLL)] public unsafe static extern void   glEvalCoord1fv           ( float * u );
[DllImport(GL_DLL)] public unsafe static extern void   glEvalCoord2d            ( double u, double v );
[DllImport(GL_DLL)] public unsafe static extern void   glEvalCoord2dv           ( double * u );
[DllImport(GL_DLL)] public unsafe static extern void   glEvalCoord2f            ( float u, float v );
[DllImport(GL_DLL)] public unsafe static extern void   glEvalCoord2fv           ( float * u );
[DllImport(GL_DLL)] public unsafe static extern void   glEvalMesh1              ( uint mode, int i1, int i2 );
[DllImport(GL_DLL)] public unsafe static extern void   glEvalMesh2              ( uint mode, int i1, int i2, int j1, int j2 );
[DllImport(GL_DLL)] public unsafe static extern void   glEvalPoint1             ( int i );
[DllImport(GL_DLL)] public unsafe static extern void   glEvalPoint2             ( int i, int j );
[DllImport(GL_DLL)] public unsafe static extern void   glFeedbackBuffer         ( int size, uint type, float * buffer );
[DllImport(GL_DLL)] public unsafe static extern void   glFinish                 ( );
[DllImport(GL_DLL)] public unsafe static extern void   glFlush                  ( );
[DllImport(GL_DLL)] public unsafe static extern void   glFogf                   ( uint pname, float param );
[DllImport(GL_DLL)] public unsafe static extern void   glFogfv                  ( uint pname, float * paramsx );
[DllImport(GL_DLL)] public unsafe static extern void   glFogi                   ( uint pname, int param );
[DllImport(GL_DLL)] public unsafe static extern void   glFogiv                  ( uint pname, int * paramsx );
[DllImport(GL_DLL)] public unsafe static extern void   glFrontFace              ( uint mode );
[DllImport(GL_DLL)] public unsafe static extern void   glFrustum                ( double left, double right, double bottom, double top, double zNear, double zFar );
[DllImport(GL_DLL)] public unsafe static extern uint   glGenLists               ( int range );
[DllImport(GL_DLL)] public unsafe static extern void   glGenTextures            ( int n, uint * textures );
[DllImport(GL_DLL)] public unsafe static extern void   glGetBooleanv            ( uint pname, byte * paramsx );
[DllImport(GL_DLL)] public unsafe static extern void   glGetClipPlane           ( uint plane, double * equation );
[DllImport(GL_DLL)] public unsafe static extern void   glGetDoublev             ( uint pname, double * paramsx );
[DllImport(GL_DLL)] public unsafe static extern uint   glGetError               ( );
[DllImport(GL_DLL)] public unsafe static extern void   glGetFloatv              ( uint pname, float * paramsx );
[DllImport(GL_DLL)] public unsafe static extern void   glGetIntegerv            ( uint pname, int * paramsx );
[DllImport(GL_DLL)] public unsafe static extern void   glGetLightfv             ( uint light, uint pname, float * paramsx );
[DllImport(GL_DLL)] public unsafe static extern void   glGetLightiv             ( uint light, uint pname, int * paramsx );
[DllImport(GL_DLL)] public unsafe static extern void   glGetMapdv               ( uint target, uint query, double * v );
[DllImport(GL_DLL)] public unsafe static extern void   glGetMapfv               ( uint target, uint query, float * v );
[DllImport(GL_DLL)] public unsafe static extern void   glGetMapiv               ( uint target, uint query, int * v );
[DllImport(GL_DLL)] public unsafe static extern void   glGetMaterialfv          ( uint face, uint pname, float * paramsx );
[DllImport(GL_DLL)] public unsafe static extern void   glGetMaterialiv          ( uint face, uint pname, int * paramsx );
[DllImport(GL_DLL)] public unsafe static extern void   glGetPixelMapfv          ( uint map, float * values );
[DllImport(GL_DLL)] public unsafe static extern void   glGetPixelMapuiv         ( uint map, uint * values );
[DllImport(GL_DLL)] public unsafe static extern void   glGetPixelMapusv         ( uint map, ushort * values );
[DllImport(GL_DLL)] public unsafe static extern void   glGetPointerv            ( uint pname, void * paramsx );
[DllImport(GL_DLL)] public unsafe static extern void   glGetPolygonStipple      ( byte * mask );
[DllImport(GL_DLL)] public unsafe static extern byte * glGetString              ( uint name );
[DllImport(GL_DLL)] public unsafe static extern void   glGetTexEnvfv            ( uint target, uint pname, float * paramsx );
[DllImport(GL_DLL)] public unsafe static extern void   glGetTexEnviv            ( uint target, uint pname, int * paramsx );
[DllImport(GL_DLL)] public unsafe static extern void   glGetTexGendv            ( uint coord, uint pname, double * paramsx );
[DllImport(GL_DLL)] public unsafe static extern void   glGetTexGenfv            ( uint coord, uint pname, float * paramsx );
[DllImport(GL_DLL)] public unsafe static extern void   glGetTexGeniv            ( uint coord, uint pname, int * paramsx );
[DllImport(GL_DLL)] public unsafe static extern void   glGetTexImage            ( uint target, int level, uint format, uint type, void * pixels );
[DllImport(GL_DLL)] public unsafe static extern void   glGetTexLevelParameterfv ( uint target, int level, uint pname, float * paramsx );
[DllImport(GL_DLL)] public unsafe static extern void   glGetTexLevelParameteriv ( uint target, int level, uint pname, int * paramsx );
[DllImport(GL_DLL)] public unsafe static extern void   glGetTexParameterfv      ( uint target, uint pname, float * paramsx );
[DllImport(GL_DLL)] public unsafe static extern void   glGetTexParameteriv      ( uint target, uint pname, int * paramsx );
[DllImport(GL_DLL)] public unsafe static extern void   glHint                   ( uint target, uint mode );
[DllImport(GL_DLL)] public unsafe static extern void   glIndexMask              ( uint mask );
[DllImport(GL_DLL)] public unsafe static extern void   glIndexPointer           ( uint type, int stride, void * pointer );
[DllImport(GL_DLL)] public unsafe static extern void   glIndexd                 ( double c );
[DllImport(GL_DLL)] public unsafe static extern void   glIndexdv                ( double * c );
[DllImport(GL_DLL)] public unsafe static extern void   glIndexf                 ( float c );
[DllImport(GL_DLL)] public unsafe static extern void   glIndexfv                ( float * c );
[DllImport(GL_DLL)] public unsafe static extern void   glIndexi                 ( int c );
[DllImport(GL_DLL)] public unsafe static extern void   glIndexiv                ( int * c );
[DllImport(GL_DLL)] public unsafe static extern void   glIndexs                 ( short c );
[DllImport(GL_DLL)] public unsafe static extern void   glIndexsv                ( short * c );
[DllImport(GL_DLL)] public unsafe static extern void   glIndexub                ( byte c );
[DllImport(GL_DLL)] public unsafe static extern void   glIndexubv               ( byte * c );
[DllImport(GL_DLL)] public unsafe static extern void   glInitNames              ( );
[DllImport(GL_DLL)] public unsafe static extern void   glInterleavedArrays      ( uint format, int stride, void * pointer );
[DllImport(GL_DLL)] public unsafe static extern byte   glIsEnabled              ( uint cap );
[DllImport(GL_DLL)] public unsafe static extern byte   glIsList                 ( uint list );
[DllImport(GL_DLL)] public unsafe static extern byte   glIsTexture              ( uint texture );
[DllImport(GL_DLL)] public unsafe static extern void   glLightModelf            ( uint pname, float param );
[DllImport(GL_DLL)] public unsafe static extern void   glLightModelfv           ( uint pname, float * paramsx );
[DllImport(GL_DLL)] public unsafe static extern void   glLightModeli            ( uint pname, int param );
[DllImport(GL_DLL)] public unsafe static extern void   glLightModeliv           ( uint pname, int * paramsx );
[DllImport(GL_DLL)] public unsafe static extern void   glLightf                 ( uint light, uint pname, float param );
[DllImport(GL_DLL)] public unsafe static extern void   glLightfv                ( uint light, uint pname, float * paramsx );
[DllImport(GL_DLL)] public unsafe static extern void   glLighti                 ( uint light, uint pname, int param );
[DllImport(GL_DLL)] public unsafe static extern void   glLightiv                ( uint light, uint pname, int * paramsx );
[DllImport(GL_DLL)] public unsafe static extern void   glLineStipple            ( int factor, ushort pattern );
[DllImport(GL_DLL)] public unsafe static extern void   glLineWidth              ( float width );
[DllImport(GL_DLL)] public unsafe static extern void   glListBase               ( uint basex );
[DllImport(GL_DLL)] public unsafe static extern void   glLoadIdentity           ( );
[DllImport(GL_DLL)] public unsafe static extern void   glLoadMatrixd            ( double * m );
[DllImport(GL_DLL)] public unsafe static extern void   glLoadMatrixf            ( float * m );
[DllImport(GL_DLL)] public unsafe static extern void   glLoadName               ( uint name );
[DllImport(GL_DLL)] public unsafe static extern void   glLogicOp                ( uint opcode );
[DllImport(GL_DLL)] public unsafe static extern void   glMap1d                  ( uint target, double u1, double u2, int stride, int order, double * points );
[DllImport(GL_DLL)] public unsafe static extern void   glMap1f                  ( uint target, float u1, float u2, int stride, int order, float * points );
[DllImport(GL_DLL)] public unsafe static extern void   glMap2d                  ( uint target, double u1, double u2, int ustride, int uorder, double v1, double v2, int vstride, int vorder, double * points );
[DllImport(GL_DLL)] public unsafe static extern void   glMap2f                  ( uint target, float u1, float u2, int ustride, int uorder, float v1, float v2, int vstride, int vorder, float * points );
[DllImport(GL_DLL)] public unsafe static extern void   glMapGrid1d              ( int un, double u1, double u2 );
[DllImport(GL_DLL)] public unsafe static extern void   glMapGrid1f              ( int un, float u1, float u2 );
[DllImport(GL_DLL)] public unsafe static extern void   glMapGrid2d              ( int un, double u1, double u2, int vn, double v1, double v2 );
[DllImport(GL_DLL)] public unsafe static extern void   glMapGrid2f              ( int un, float u1, float u2, int vn, float v1, float v2 );
[DllImport(GL_DLL)] public unsafe static extern void   glMaterialf              ( uint face, uint pname, float param );
[DllImport(GL_DLL)] public unsafe static extern void   glMaterialfv             ( uint face, uint pname, float * paramsx );
[DllImport(GL_DLL)] public unsafe static extern void   glMateriali              ( uint face, uint pname, int param );
[DllImport(GL_DLL)] public unsafe static extern void   glMaterialiv             ( uint face, uint pname, int * paramsx );
[DllImport(GL_DLL)] public unsafe static extern void   glMatrixMode             ( uint mode );
[DllImport(GL_DLL)] public unsafe static extern void   glMultMatrixd            ( double * m );
[DllImport(GL_DLL)] public unsafe static extern void   glMultMatrixf            ( float * m );
[DllImport(GL_DLL)] public unsafe static extern void   glNewList                ( uint list, uint mode );
[DllImport(GL_DLL)] public unsafe static extern void   glNormal3b               ( sbyte nx, sbyte ny, sbyte nz );
[DllImport(GL_DLL)] public unsafe static extern void   glNormal3bv              ( sbyte * v );
[DllImport(GL_DLL)] public unsafe static extern void   glNormal3d               ( double nx, double ny, double nz );
[DllImport(GL_DLL)] public unsafe static extern void   glNormal3dv              ( double * v );
[DllImport(GL_DLL)] public unsafe static extern void   glNormal3f               ( float nx, float ny, float nz );
[DllImport(GL_DLL)] public unsafe static extern void   glNormal3fv              ( float * v );
[DllImport(GL_DLL)] public unsafe static extern void   glNormal3i               ( int nx, int ny, int nz );
[DllImport(GL_DLL)] public unsafe static extern void   glNormal3iv              ( int * v );
[DllImport(GL_DLL)] public unsafe static extern void   glNormal3s               ( short nx, short ny, short nz );
[DllImport(GL_DLL)] public unsafe static extern void   glNormal3sv              ( short * v );
[DllImport(GL_DLL)] public unsafe static extern void   glNormalPointer          ( uint type, int stride, void * pointer );
[DllImport(GL_DLL)] public unsafe static extern void   glOrtho                  ( double left, double right, double bottom, double top, double zNear, double zFar );
[DllImport(GL_DLL)] public unsafe static extern void   glPassThrough            ( float token );
[DllImport(GL_DLL)] public unsafe static extern void   glPixelMapfv             ( uint map, int mapsize, float * values );
[DllImport(GL_DLL)] public unsafe static extern void   glPixelMapuiv            ( uint map, int mapsize, uint * values );
[DllImport(GL_DLL)] public unsafe static extern void   glPixelMapusv            ( uint map, int mapsize, ushort * values );
[DllImport(GL_DLL)] public unsafe static extern void   glPixelStoref            ( uint pname, float param );
[DllImport(GL_DLL)] public unsafe static extern void   glPixelStorei            ( uint pname, int param );
[DllImport(GL_DLL)] public unsafe static extern void   glPixelTransferf         ( uint pname, float param );
[DllImport(GL_DLL)] public unsafe static extern void   glPixelTransferi         ( uint pname, int param );
[DllImport(GL_DLL)] public unsafe static extern void   glPixelZoom              ( float xfactor, float yfactor );
[DllImport(GL_DLL)] public unsafe static extern void   glPointSize              ( float size );
[DllImport(GL_DLL)] public unsafe static extern void   glPolygonMode            ( uint face, uint mode );
[DllImport(GL_DLL)] public unsafe static extern void   glPolygonOffset          ( float factor, float units );
[DllImport(GL_DLL)] public unsafe static extern void   glPolygonStipple         ( byte * mask );
[DllImport(GL_DLL)] public unsafe static extern void   glPopAttrib              ( );
[DllImport(GL_DLL)] public unsafe static extern void   glPopClientAttrib        ( );
[DllImport(GL_DLL)] public unsafe static extern void   glPopMatrix              ( );
[DllImport(GL_DLL)] public unsafe static extern void   glPopName                ( );
[DllImport(GL_DLL)] public unsafe static extern void   glPrioritizeTextures     ( int n, uint * textures, float * priorities );
[DllImport(GL_DLL)] public unsafe static extern void   glPushAttrib             ( uint mask );
[DllImport(GL_DLL)] public unsafe static extern void   glPushClientAttrib       ( uint mask );
[DllImport(GL_DLL)] public unsafe static extern void   glPushMatrix             ( );
[DllImport(GL_DLL)] public unsafe static extern void   glPushName               ( uint name );
[DllImport(GL_DLL)] public unsafe static extern void   glRasterPos2d            ( double x, double y );
[DllImport(GL_DLL)] public unsafe static extern void   glRasterPos2dv           ( double * v );
[DllImport(GL_DLL)] public unsafe static extern void   glRasterPos2f            ( float x, float y );
[DllImport(GL_DLL)] public unsafe static extern void   glRasterPos2fv           ( float * v );
[DllImport(GL_DLL)] public unsafe static extern void   glRasterPos2i            ( int x, int y );
[DllImport(GL_DLL)] public unsafe static extern void   glRasterPos2iv           ( int * v );
[DllImport(GL_DLL)] public unsafe static extern void   glRasterPos2s            ( short x, short y );
[DllImport(GL_DLL)] public unsafe static extern void   glRasterPos2sv           ( short * v );
[DllImport(GL_DLL)] public unsafe static extern void   glRasterPos3d            ( double x, double y, double z );
[DllImport(GL_DLL)] public unsafe static extern void   glRasterPos3dv           ( double * v );
[DllImport(GL_DLL)] public unsafe static extern void   glRasterPos3f            ( float x, float y, float z );
[DllImport(GL_DLL)] public unsafe static extern void   glRasterPos3fv           ( float * v );
[DllImport(GL_DLL)] public unsafe static extern void   glRasterPos3i            ( int x, int y, int z );
[DllImport(GL_DLL)] public unsafe static extern void   glRasterPos3iv           ( int * v );
[DllImport(GL_DLL)] public unsafe static extern void   glRasterPos3s            ( short x, short y, short z );
[DllImport(GL_DLL)] public unsafe static extern void   glRasterPos3sv           ( short * v );
[DllImport(GL_DLL)] public unsafe static extern void   glRasterPos4d            ( double x, double y, double z, double w );
[DllImport(GL_DLL)] public unsafe static extern void   glRasterPos4dv           ( double * v );
[DllImport(GL_DLL)] public unsafe static extern void   glRasterPos4f            ( float x, float y, float z, float w );
[DllImport(GL_DLL)] public unsafe static extern void   glRasterPos4fv           ( float * v );
[DllImport(GL_DLL)] public unsafe static extern void   glRasterPos4i            ( int x, int y, int z, int w );
[DllImport(GL_DLL)] public unsafe static extern void   glRasterPos4iv           ( int * v );
[DllImport(GL_DLL)] public unsafe static extern void   glRasterPos4s            ( short x, short y, short z, short w );
[DllImport(GL_DLL)] public unsafe static extern void   glRasterPos4sv           ( short * v );
[DllImport(GL_DLL)] public unsafe static extern void   glReadBuffer             ( uint mode );
[DllImport(GL_DLL)] public unsafe static extern void   glReadPixels             ( int x, int y, int width, int height, uint format, uint type, void * pixels );
[DllImport(GL_DLL)] public unsafe static extern void   glRectd                  ( double x1, double y1, double x2, double y2 );
[DllImport(GL_DLL)] public unsafe static extern void   glRectdv                 ( double * v1, double * v2 );
[DllImport(GL_DLL)] public unsafe static extern void   glRectf                  ( float x1, float y1, float x2, float y2 );
[DllImport(GL_DLL)] public unsafe static extern void   glRectfv                 ( float * v1, float * v2 );
[DllImport(GL_DLL)] public unsafe static extern void   glRecti                  ( int x1, int y1, int x2, int y2 );
[DllImport(GL_DLL)] public unsafe static extern void   glRectiv                 ( int * v1, int * v2 );
[DllImport(GL_DLL)] public unsafe static extern void   glRects                  ( short x1, short y1, short x2, short y2 );
[DllImport(GL_DLL)] public unsafe static extern void   glRectsv                 ( short * v1, short * v2 );
[DllImport(GL_DLL)] public unsafe static extern int    glRenderMode             ( uint mode );
[DllImport(GL_DLL)] public unsafe static extern void   glRotated                ( double angle, double x, double y, double z );
[DllImport(GL_DLL)] public unsafe static extern void   glRotatef                ( float angle, float x, float y, float z );
[DllImport(GL_DLL)] public unsafe static extern void   glScaled                 ( double x, double y, double z );
[DllImport(GL_DLL)] public unsafe static extern void   glScalef                 ( float x, float y, float z );
[DllImport(GL_DLL)] public unsafe static extern void   glScissor                ( int x, int y, int width, int height );
[DllImport(GL_DLL)] public unsafe static extern void   glSelectBuffer           ( int size, uint * buffer );
[DllImport(GL_DLL)] public unsafe static extern void   glShadeModel             ( uint mode );
[DllImport(GL_DLL)] public unsafe static extern void   glStencilFunc            ( uint func, int refx, uint mask );
[DllImport(GL_DLL)] public unsafe static extern void   glStencilMask            ( uint mask );
[DllImport(GL_DLL)] public unsafe static extern void   glStencilOp              ( uint fail, uint zfail, uint zpass );
[DllImport(GL_DLL)] public unsafe static extern void   glTexCoord1d             ( double s );
[DllImport(GL_DLL)] public unsafe static extern void   glTexCoord1dv            ( double * v );
[DllImport(GL_DLL)] public unsafe static extern void   glTexCoord1f             ( float s );
[DllImport(GL_DLL)] public unsafe static extern void   glTexCoord1fv            ( float * v );
[DllImport(GL_DLL)] public unsafe static extern void   glTexCoord1i             ( int s );
[DllImport(GL_DLL)] public unsafe static extern void   glTexCoord1iv            ( int * v );
[DllImport(GL_DLL)] public unsafe static extern void   glTexCoord1s             ( short s );
[DllImport(GL_DLL)] public unsafe static extern void   glTexCoord1sv            ( short * v );
[DllImport(GL_DLL)] public unsafe static extern void   glTexCoord2d             ( double s, double t );
[DllImport(GL_DLL)] public unsafe static extern void   glTexCoord2dv            ( double * v );
[DllImport(GL_DLL)] public unsafe static extern void   glTexCoord2f             ( float s, float t );
[DllImport(GL_DLL)] public unsafe static extern void   glTexCoord2fv            ( float * v );
[DllImport(GL_DLL)] public unsafe static extern void   glTexCoord2i             ( int s, int t );
[DllImport(GL_DLL)] public unsafe static extern void   glTexCoord2iv            ( int * v );
[DllImport(GL_DLL)] public unsafe static extern void   glTexCoord2s             ( short s, short t );
[DllImport(GL_DLL)] public unsafe static extern void   glTexCoord2sv            ( short * v );
[DllImport(GL_DLL)] public unsafe static extern void   glTexCoord3d             ( double s, double t, double r );
[DllImport(GL_DLL)] public unsafe static extern void   glTexCoord3dv            ( double * v );
[DllImport(GL_DLL)] public unsafe static extern void   glTexCoord3f             ( float s, float t, float r );
[DllImport(GL_DLL)] public unsafe static extern void   glTexCoord3fv            ( float * v );
[DllImport(GL_DLL)] public unsafe static extern void   glTexCoord3i             ( int s, int t, int r );
[DllImport(GL_DLL)] public unsafe static extern void   glTexCoord3iv            ( int * v );
[DllImport(GL_DLL)] public unsafe static extern void   glTexCoord3s             ( short s, short t, short r );
[DllImport(GL_DLL)] public unsafe static extern void   glTexCoord3sv            ( short * v );
[DllImport(GL_DLL)] public unsafe static extern void   glTexCoord4d             ( double s, double t, double r, double q );
[DllImport(GL_DLL)] public unsafe static extern void   glTexCoord4dv            ( double * v );
[DllImport(GL_DLL)] public unsafe static extern void   glTexCoord4f             ( float s, float t, float r, float q );
[DllImport(GL_DLL)] public unsafe static extern void   glTexCoord4fv            ( float * v );
[DllImport(GL_DLL)] public unsafe static extern void   glTexCoord4i             ( int s, int t, int r, int q );
[DllImport(GL_DLL)] public unsafe static extern void   glTexCoord4iv            ( int * v );
[DllImport(GL_DLL)] public unsafe static extern void   glTexCoord4s             ( short s, short t, short r, short q );
[DllImport(GL_DLL)] public unsafe static extern void   glTexCoord4sv            ( short * v );
[DllImport(GL_DLL)] public unsafe static extern void   glTexCoordPointer        ( int size, uint type, int stride, void * pointer );
[DllImport(GL_DLL)] public unsafe static extern void   glTexEnvf                ( uint target, uint pname, float param );
[DllImport(GL_DLL)] public unsafe static extern void   glTexEnvfv               ( uint target, uint pname, float * paramsx );
[DllImport(GL_DLL)] public unsafe static extern void   glTexEnvi                ( uint target, uint pname, int param );
[DllImport(GL_DLL)] public unsafe static extern void   glTexEnviv               ( uint target, uint pname, int * paramsx );
[DllImport(GL_DLL)] public unsafe static extern void   glTexGend                ( uint coord, uint pname, double param );
[DllImport(GL_DLL)] public unsafe static extern void   glTexGendv               ( uint coord, uint pname, double * paramsx );
[DllImport(GL_DLL)] public unsafe static extern void   glTexGenf                ( uint coord, uint pname, float param );
[DllImport(GL_DLL)] public unsafe static extern void   glTexGenfv               ( uint coord, uint pname, float * paramsx );
[DllImport(GL_DLL)] public unsafe static extern void   glTexGeni                ( uint coord, uint pname, int param );
[DllImport(GL_DLL)] public unsafe static extern void   glTexGeniv               ( uint coord, uint pname, int * paramsx );
[DllImport(GL_DLL)] public unsafe static extern void   glTexImage1D             ( uint target, int level, int internalformat, int width, int border, uint format, uint type, void * pixels );
[DllImport(GL_DLL)] public unsafe static extern void   glTexImage2D             ( uint target, int level, int internalformat, int width, int height, int border, uint format, uint type, void * pixels );
[DllImport(GL_DLL)] public unsafe static extern void   glTexParameterf          ( uint target, uint pname, float param );
[DllImport(GL_DLL)] public unsafe static extern void   glTexParameterfv         ( uint target, uint pname, float * paramsx );
[DllImport(GL_DLL)] public unsafe static extern void   glTexParameteri          ( uint target, uint pname, int param );
[DllImport(GL_DLL)] public unsafe static extern void   glTexParameteriv         ( uint target, uint pname, int * paramsx );
[DllImport(GL_DLL)] public unsafe static extern void   glTexSubImage1D          ( uint target, int level, int xoffset, int width, uint format, uint type, void * pixels );
[DllImport(GL_DLL)] public unsafe static extern void   glTexSubImage2D          ( uint target, int level, int xoffset, int yoffset, int width, int height, uint format, uint type, void * pixels );
[DllImport(GL_DLL)] public unsafe static extern void   glTranslated             ( double x, double y, double z );
[DllImport(GL_DLL)] public unsafe static extern void   glTranslatef             ( float x, float y, float z );
[DllImport(GL_DLL)] public unsafe static extern void   glVertex2d               ( double x, double y );
[DllImport(GL_DLL)] public unsafe static extern void   glVertex2dv              ( double * v );
[DllImport(GL_DLL)] public unsafe static extern void   glVertex2f               ( float x, float y );
[DllImport(GL_DLL)] public unsafe static extern void   glVertex2fv              ( float * v );
[DllImport(GL_DLL)] public unsafe static extern void   glVertex2i               ( int x, int y );
[DllImport(GL_DLL)] public unsafe static extern void   glVertex2iv              ( int * v );
[DllImport(GL_DLL)] public unsafe static extern void   glVertex2s               ( short x, short y );
[DllImport(GL_DLL)] public unsafe static extern void   glVertex2sv              ( short * v );
[DllImport(GL_DLL)] public unsafe static extern void   glVertex3d               ( double x, double y, double z );
[DllImport(GL_DLL)] public unsafe static extern void   glVertex3dv              ( double * v );
[DllImport(GL_DLL)] public unsafe static extern void   glVertex3f               ( float x, float y, float z );
[DllImport(GL_DLL)] public unsafe static extern void   glVertex3fv              ( float * v );
[DllImport(GL_DLL)] public unsafe static extern void   glVertex3i               ( int x, int y, int z );
[DllImport(GL_DLL)] public unsafe static extern void   glVertex3iv              ( int * v );
[DllImport(GL_DLL)] public unsafe static extern void   glVertex3s               ( short x, short y, short z );
[DllImport(GL_DLL)] public unsafe static extern void   glVertex3sv              ( short * v );
[DllImport(GL_DLL)] public unsafe static extern void   glVertex4d               ( double x, double y, double z, double w );
[DllImport(GL_DLL)] public unsafe static extern void   glVertex4dv              ( double * v );
[DllImport(GL_DLL)] public unsafe static extern void   glVertex4f               ( float x, float y, float z, float w );
[DllImport(GL_DLL)] public unsafe static extern void   glVertex4fv              ( float * v );
[DllImport(GL_DLL)] public unsafe static extern void   glVertex4i               ( int x, int y, int z, int w );
[DllImport(GL_DLL)] public unsafe static extern void   glVertex4iv              ( int * v );
[DllImport(GL_DLL)] public unsafe static extern void   glVertex4s               ( short x, short y, short z, short w );
[DllImport(GL_DLL)] public unsafe static extern void   glVertex4sv              ( short * v );
[DllImport(GL_DLL)] public unsafe static extern void   glVertexPointer          ( int size, uint type, int stride, void * pointer );
[DllImport(GL_DLL)] public unsafe static extern void   glViewport               ( int x, int y, int width, int height );
//
//
// ############################################################################
// ############################################################################
// ############################################################################
// ############################################################################
//
//
// C# Alternative Overrides for OpenGL functions involving pointers.
//
//

// ************************* FIX ME ***********************
// ************************* FIX ME ***********************
// ************************* FIX ME ***********************
/* 
public unsafe static byte   glAreTexturesResident    ( int n, uint[] textures, byte[] residences )
{
fixed ( uint * p_textures = textures ) { 
fixed ( byte * p_residences = residences ) { 
  glAreTexturesResident( n, p_textures, p_residences );
  } } 
}
*/ 
// ************************* FIX ME ***********************
// ************************* FIX ME ***********************
// ************************* FIX ME ***********************


public unsafe static void   glBitmap                 ( int width, int height, float xorig, float yorig, float xmove, float ymove, byte[] bitmap )
{
fixed ( byte * p_bitmap = bitmap ) { 
  glBitmap( width, height, xorig, yorig, xmove, ymove, p_bitmap );
  } 
}

public unsafe static void   glCallLists              ( int n, uint type, IntPtr lists )
{
  glCallLists( n, type, lists );
}

public unsafe static void   glClipPlane              ( uint plane, double[] equation )
{
fixed ( double * p_equation = equation ) { 
  glClipPlane( plane, p_equation );
  } 
}

public unsafe static void   glColor3bv               ( sbyte[] v )
{
fixed ( sbyte * p_v = v ) { 
  glColor3bv( p_v );
  } 
}

public unsafe static void   glColor3dv               ( double[] v )
{
fixed ( double * p_v = v ) { 
  glColor3dv( p_v );
  } 
}

public unsafe static void   glColor3fv               ( float[] v )
{
fixed ( float * p_v = v ) { 
  glColor3fv( p_v );
  } 
}

public unsafe static void   glColor3iv               ( int[] v )
{
fixed ( int * p_v = v ) { 
  glColor3iv( p_v );
  } 
}

public unsafe static void   glColor3sv               ( short[] v )
{
fixed ( short * p_v = v ) { 
  glColor3sv( p_v );
  } 
}

public unsafe static void   glColor3ubv              ( byte[] v )
{
fixed ( byte * p_v = v ) { 
  glColor3ubv( p_v );
  } 
}

public unsafe static void   glColor3uiv              ( uint[] v )
{
fixed ( uint * p_v = v ) { 
  glColor3uiv( p_v );
  } 
}

public unsafe static void   glColor3usv              ( ushort[] v )
{
fixed ( ushort * p_v = v ) { 
  glColor3usv( p_v );
  } 
}

public unsafe static void   glColor4bv               ( sbyte[] v )
{
fixed ( sbyte * p_v = v ) { 
  glColor4bv( p_v );
  } 
}

public unsafe static void   glColor4dv               ( double[] v )
{
fixed ( double * p_v = v ) { 
  glColor4dv( p_v );
  } 
}

public unsafe static void   glColor4fv               ( float[] v )
{
fixed ( float * p_v = v ) { 
  glColor4fv( p_v );
  } 
}

public unsafe static void   glColor4iv               ( int[] v )
{
fixed ( int * p_v = v ) { 
  glColor4iv( p_v );
  } 
}

public unsafe static void   glColor4sv               ( short[] v )
{
fixed ( short * p_v = v ) { 
  glColor4sv( p_v );
  } 
}

public unsafe static void   glColor4ubv              ( byte[] v )
{
fixed ( byte * p_v = v ) { 
  glColor4ubv( p_v );
  } 
}

public unsafe static void   glColor4uiv              ( uint[] v )
{
fixed ( uint * p_v = v ) { 
  glColor4uiv( p_v );
  } 
}

public unsafe static void   glColor4usv              ( ushort[] v )
{
fixed ( ushort * p_v = v ) { 
  glColor4usv( p_v );
  } 
}

public unsafe static void   glColorPointer           ( int size, uint type, int stride, IntPtr pointer )
{
  glColorPointer( size, type, stride, pointer );
}

public unsafe static void   glDeleteTextures         ( int n, uint[] textures )
{
fixed ( uint * p_textures = textures ) { 
  glDeleteTextures( n, p_textures );
  } 
}

public unsafe static void   glDrawElements           ( uint mode, int count, uint type, IntPtr indices )
{
  glDrawElements( mode, count, type, indices );
}

public unsafe static void   glDrawPixels             ( int width, int height, uint format, uint type, IntPtr pixels )
{
  glDrawPixels( width, height, format, type, pixels );
}

public unsafe static void   glEdgeFlagPointer        ( int stride, IntPtr pointer )
{
  glEdgeFlagPointer( stride, pointer );
}

public unsafe static void   glEdgeFlagv              ( byte[] flag )
{
fixed ( byte * p_flag = flag ) { 
  glEdgeFlagv( p_flag );
  } 
}

public unsafe static void   glEvalCoord1dv           ( double[] u )
{
fixed ( double * p_u = u ) { 
  glEvalCoord1dv( p_u );
  } 
}

public unsafe static void   glEvalCoord1fv           ( float[] u )
{
fixed ( float * p_u = u ) { 
  glEvalCoord1fv( p_u );
  } 
}

public unsafe static void   glEvalCoord2dv           ( double[] u )
{
fixed ( double * p_u = u ) { 
  glEvalCoord2dv( p_u );
  } 
}

public unsafe static void   glEvalCoord2fv           ( float[] u )
{
fixed ( float * p_u = u ) { 
  glEvalCoord2fv( p_u );
  } 
}

public unsafe static void   glFeedbackBuffer         ( int size, uint type, float[] buffer )
{
fixed ( float * p_buffer = buffer ) { 
  glFeedbackBuffer( size, type, p_buffer );
  } 
}

public unsafe static void   glFogfv                  ( uint pname, float[] paramsx )
{
fixed ( float * p_paramsx = paramsx ) { 
  glFogfv( pname, p_paramsx );
  } 
}

public unsafe static void   glFogiv                  ( uint pname, int[] paramsx )
{
fixed ( int * p_paramsx = paramsx ) { 
  glFogiv( pname, p_paramsx );
  } 
}

public unsafe static void   glGenTextures            ( int n, uint[] textures )
{
fixed ( uint * p_textures = textures ) { 
  glGenTextures( n, p_textures );
  } 
}

public unsafe static void   glGetBooleanv            ( uint pname, byte[] paramsx )
{
fixed ( byte * p_paramsx = paramsx ) { 
  glGetBooleanv( pname, p_paramsx );
  } 
}

public unsafe static void   glGetClipPlane           ( uint plane, double[] equation )
{
fixed ( double * p_equation = equation ) { 
  glGetClipPlane( plane, p_equation );
  } 
}

public unsafe static void   glGetDoublev             ( uint pname, double[] paramsx )
{
fixed ( double * p_paramsx = paramsx ) { 
  glGetDoublev( pname, p_paramsx );
  } 
}

public unsafe static void   glGetFloatv              ( uint pname, float[] paramsx )
{
fixed ( float * p_paramsx = paramsx ) { 
  glGetFloatv( pname, p_paramsx );
  } 
}

public unsafe static void   glGetIntegerv            ( uint pname, int[] paramsx )
{
fixed ( int * p_paramsx = paramsx ) { 
  glGetIntegerv( pname, p_paramsx );
  } 
}

public unsafe static void   glGetLightfv             ( uint light, uint pname, float[] paramsx )
{
fixed ( float * p_paramsx = paramsx ) { 
  glGetLightfv( light, pname, p_paramsx );
  } 
}

public unsafe static void   glGetLightiv             ( uint light, uint pname, int[] paramsx )
{
fixed ( int * p_paramsx = paramsx ) { 
  glGetLightiv( light, pname, p_paramsx );
  } 
}

public unsafe static void   glGetMapdv               ( uint target, uint query, double[] v )
{
fixed ( double * p_v = v ) { 
  glGetMapdv( target, query, p_v );
  } 
}

public unsafe static void   glGetMapfv               ( uint target, uint query, float[] v )
{
fixed ( float * p_v = v ) { 
  glGetMapfv( target, query, p_v );
  } 
}

public unsafe static void   glGetMapiv               ( uint target, uint query, int[] v )
{
fixed ( int * p_v = v ) { 
  glGetMapiv( target, query, p_v );
  } 
}

public unsafe static void   glGetMaterialfv          ( uint face, uint pname, float[] paramsx )
{
fixed ( float * p_paramsx = paramsx ) { 
  glGetMaterialfv( face, pname, p_paramsx );
  } 
}

public unsafe static void   glGetMaterialiv          ( uint face, uint pname, int[] paramsx )
{
fixed ( int * p_paramsx = paramsx ) { 
  glGetMaterialiv( face, pname, p_paramsx );
  } 
}

public unsafe static void   glGetPixelMapfv          ( uint map, float[] values )
{
fixed ( float * p_values = values ) { 
  glGetPixelMapfv( map, p_values );
  } 
}

public unsafe static void   glGetPixelMapuiv         ( uint map, uint[] values )
{
fixed ( uint * p_values = values ) { 
  glGetPixelMapuiv( map, p_values );
  } 
}

public unsafe static void   glGetPixelMapusv         ( uint map, ushort[] values )
{
fixed ( ushort * p_values = values ) { 
  glGetPixelMapusv( map, p_values );
  } 
}

public unsafe static void   glGetPointerv            ( uint pname, IntPtr paramsx )
{
  glGetPointerv( pname, paramsx );
}

public unsafe static void   glGetPolygonStipple      ( byte[] mask )
{
fixed ( byte * p_mask = mask ) { 
  glGetPolygonStipple( p_mask );
  } 
}


// ************************* FIX ME ***********************
// ************************* FIX ME ***********************
// ************************* FIX ME ***********************
/* 
public unsafe static byte[] glGetString              ( uint name )
{
  glGetString( name );
}
*/ 
// ************************* FIX ME ***********************
// ************************* FIX ME ***********************
// ************************* FIX ME ***********************


public unsafe static void   glGetTexEnvfv            ( uint target, uint pname, float[] paramsx )
{
fixed ( float * p_paramsx = paramsx ) { 
  glGetTexEnvfv( target, pname, p_paramsx );
  } 
}

public unsafe static void   glGetTexEnviv            ( uint target, uint pname, int[] paramsx )
{
fixed ( int * p_paramsx = paramsx ) { 
  glGetTexEnviv( target, pname, p_paramsx );
  } 
}

public unsafe static void   glGetTexGendv            ( uint coord, uint pname, double[] paramsx )
{
fixed ( double * p_paramsx = paramsx ) { 
  glGetTexGendv( coord, pname, p_paramsx );
  } 
}

public unsafe static void   glGetTexGenfv            ( uint coord, uint pname, float[] paramsx )
{
fixed ( float * p_paramsx = paramsx ) { 
  glGetTexGenfv( coord, pname, p_paramsx );
  } 
}

public unsafe static void   glGetTexGeniv            ( uint coord, uint pname, int[] paramsx )
{
fixed ( int * p_paramsx = paramsx ) { 
  glGetTexGeniv( coord, pname, p_paramsx );
  } 
}

public unsafe static void   glGetTexImage            ( uint target, int level, uint format, uint type, IntPtr pixels )
{
  glGetTexImage( target, level, format, type, pixels );
}

public unsafe static void   glGetTexLevelParameterfv ( uint target, int level, uint pname, float[] paramsx )
{
fixed ( float * p_paramsx = paramsx ) { 
  glGetTexLevelParameterfv( target, level, pname, p_paramsx );
  } 
}

public unsafe static void   glGetTexLevelParameteriv ( uint target, int level, uint pname, int[] paramsx )
{
fixed ( int * p_paramsx = paramsx ) { 
  glGetTexLevelParameteriv( target, level, pname, p_paramsx );
  } 
}

public unsafe static void   glGetTexParameterfv      ( uint target, uint pname, float[] paramsx )
{
fixed ( float * p_paramsx = paramsx ) { 
  glGetTexParameterfv( target, pname, p_paramsx );
  } 
}

public unsafe static void   glGetTexParameteriv      ( uint target, uint pname, int[] paramsx )
{
fixed ( int * p_paramsx = paramsx ) { 
  glGetTexParameteriv( target, pname, p_paramsx );
  } 
}

public unsafe static void   glIndexPointer           ( uint type, int stride, IntPtr pointer )
{
  glIndexPointer( type, stride, pointer );
}

public unsafe static void   glIndexdv                ( double[] c )
{
fixed ( double * p_c = c ) { 
  glIndexdv( p_c );
  } 
}

public unsafe static void   glIndexfv                ( float[] c )
{
fixed ( float * p_c = c ) { 
  glIndexfv( p_c );
  } 
}

public unsafe static void   glIndexiv                ( int[] c )
{
fixed ( int * p_c = c ) { 
  glIndexiv( p_c );
  } 
}

public unsafe static void   glIndexsv                ( short[] c )
{
fixed ( short * p_c = c ) { 
  glIndexsv( p_c );
  } 
}

public unsafe static void   glIndexubv               ( byte[] c )
{
fixed ( byte * p_c = c ) { 
  glIndexubv( p_c );
  } 
}

public unsafe static void   glInterleavedArrays      ( uint format, int stride, IntPtr pointer )
{
  glInterleavedArrays( format, stride, pointer );
}

public unsafe static void   glLightModelfv           ( uint pname, float[] paramsx )
{
fixed ( float * p_paramsx = paramsx ) { 
  glLightModelfv( pname, p_paramsx );
  } 
}

public unsafe static void   glLightModeliv           ( uint pname, int[] paramsx )
{
fixed ( int * p_paramsx = paramsx ) { 
  glLightModeliv( pname, p_paramsx );
  } 
}

public unsafe static void   glLightfv                ( uint light, uint pname, float[] paramsx )
{
fixed ( float * p_paramsx = paramsx ) { 
  glLightfv( light, pname, p_paramsx );
  } 
}

public unsafe static void   glLightiv                ( uint light, uint pname, int[] paramsx )
{
fixed ( int * p_paramsx = paramsx ) { 
  glLightiv( light, pname, p_paramsx );
  } 
}

public unsafe static void   glLoadMatrixd            ( double[] m )
{
fixed ( double * p_m = m ) { 
  glLoadMatrixd( p_m );
  } 
}

public unsafe static void   glLoadMatrixf            ( float[] m )
{
fixed ( float * p_m = m ) { 
  glLoadMatrixf( p_m );
  } 
}

public unsafe static void   glMap1d                  ( uint target, double u1, double u2, int stride, int order, double[] points )
{
fixed ( double * p_points = points ) { 
  glMap1d( target, u1, u2, stride, order, p_points );
  } 
}

public unsafe static void   glMap1f                  ( uint target, float u1, float u2, int stride, int order, float[] points )
{
fixed ( float * p_points = points ) { 
  glMap1f( target, u1, u2, stride, order, p_points );
  } 
}

public unsafe static void   glMap2d                  ( uint target, double u1, double u2, int ustride, int uorder, double v1, double v2, int vstride, int vorder, double[] points )
{
fixed ( double * p_points = points ) { 
  glMap2d( target, u1, u2, ustride, uorder, v1, v2, vstride, vorder, p_points );
  } 
}

public unsafe static void   glMap2f                  ( uint target, float u1, float u2, int ustride, int uorder, float v1, float v2, int vstride, int vorder, float[] points )
{
fixed ( float * p_points = points ) { 
  glMap2f( target, u1, u2, ustride, uorder, v1, v2, vstride, vorder, p_points );
  } 
}

public unsafe static void   glMaterialfv             ( uint face, uint pname, float[] paramsx )
{
fixed ( float * p_paramsx = paramsx ) { 
  glMaterialfv( face, pname, p_paramsx );
  } 
}

public unsafe static void   glMaterialiv             ( uint face, uint pname, int[] paramsx )
{
fixed ( int * p_paramsx = paramsx ) { 
  glMaterialiv( face, pname, p_paramsx );
  } 
}

public unsafe static void   glMultMatrixd            ( double[] m )
{
fixed ( double * p_m = m ) { 
  glMultMatrixd( p_m );
  } 
}

public unsafe static void   glMultMatrixf            ( float[] m )
{
fixed ( float * p_m = m ) { 
  glMultMatrixf( p_m );
  } 
}

public unsafe static void   glNormal3bv              ( sbyte[] v )
{
fixed ( sbyte * p_v = v ) { 
  glNormal3bv( p_v );
  } 
}

public unsafe static void   glNormal3dv              ( double[] v )
{
fixed ( double * p_v = v ) { 
  glNormal3dv( p_v );
  } 
}

public unsafe static void   glNormal3fv              ( float[] v )
{
fixed ( float * p_v = v ) { 
  glNormal3fv( p_v );
  } 
}

public unsafe static void   glNormal3iv              ( int[] v )
{
fixed ( int * p_v = v ) { 
  glNormal3iv( p_v );
  } 
}

public unsafe static void   glNormal3sv              ( short[] v )
{
fixed ( short * p_v = v ) { 
  glNormal3sv( p_v );
  } 
}

public unsafe static void   glNormalPointer          ( uint type, int stride, IntPtr pointer )
{
  glNormalPointer( type, stride, pointer );
}

public unsafe static void   glPixelMapfv             ( uint map, int mapsize, float[] values )
{
fixed ( float * p_values = values ) { 
  glPixelMapfv( map, mapsize, p_values );
  } 
}

public unsafe static void   glPixelMapuiv            ( uint map, int mapsize, uint[] values )
{
fixed ( uint * p_values = values ) { 
  glPixelMapuiv( map, mapsize, p_values );
  } 
}

public unsafe static void   glPixelMapusv            ( uint map, int mapsize, ushort[] values )
{
fixed ( ushort * p_values = values ) { 
  glPixelMapusv( map, mapsize, p_values );
  } 
}

public unsafe static void   glPolygonStipple         ( byte[] mask )
{
fixed ( byte * p_mask = mask ) { 
  glPolygonStipple( p_mask );
  } 
}

public unsafe static void   glPrioritizeTextures     ( int n, uint[] textures, float[] priorities )
{
fixed ( uint * p_textures = textures ) { 
fixed ( float * p_priorities = priorities ) { 
  glPrioritizeTextures( n, p_textures, p_priorities );
  } } 
}

public unsafe static void   glRasterPos2dv           ( double[] v )
{
fixed ( double * p_v = v ) { 
  glRasterPos2dv( p_v );
  } 
}

public unsafe static void   glRasterPos2fv           ( float[] v )
{
fixed ( float * p_v = v ) { 
  glRasterPos2fv( p_v );
  } 
}

public unsafe static void   glRasterPos2iv           ( int[] v )
{
fixed ( int * p_v = v ) { 
  glRasterPos2iv( p_v );
  } 
}

public unsafe static void   glRasterPos2sv           ( short[] v )
{
fixed ( short * p_v = v ) { 
  glRasterPos2sv( p_v );
  } 
}

public unsafe static void   glRasterPos3dv           ( double[] v )
{
fixed ( double * p_v = v ) { 
  glRasterPos3dv( p_v );
  } 
}

public unsafe static void   glRasterPos3fv           ( float[] v )
{
fixed ( float * p_v = v ) { 
  glRasterPos3fv( p_v );
  } 
}

public unsafe static void   glRasterPos3iv           ( int[] v )
{
fixed ( int * p_v = v ) { 
  glRasterPos3iv( p_v );
  } 
}

public unsafe static void   glRasterPos3sv           ( short[] v )
{
fixed ( short * p_v = v ) { 
  glRasterPos3sv( p_v );
  } 
}

public unsafe static void   glRasterPos4dv           ( double[] v )
{
fixed ( double * p_v = v ) { 
  glRasterPos4dv( p_v );
  } 
}

public unsafe static void   glRasterPos4fv           ( float[] v )
{
fixed ( float * p_v = v ) { 
  glRasterPos4fv( p_v );
  } 
}

public unsafe static void   glRasterPos4iv           ( int[] v )
{
fixed ( int * p_v = v ) { 
  glRasterPos4iv( p_v );
  } 
}

public unsafe static void   glRasterPos4sv           ( short[] v )
{
fixed ( short * p_v = v ) { 
  glRasterPos4sv( p_v );
  } 
}

public unsafe static void   glReadPixels             ( int x, int y, int width, int height, uint format, uint type, IntPtr pixels )
{
  glReadPixels( x, y, width, height, format, type, pixels );
}

public unsafe static void   glRectdv                 ( double[] v1, double[] v2 )
{
fixed ( double * p_v1 = v1 ) { 
fixed ( double * p_v2 = v2 ) { 
  glRectdv( p_v1, p_v2 );
  } } 
}

public unsafe static void   glRectfv                 ( float[] v1, float[] v2 )
{
fixed ( float * p_v1 = v1 ) { 
fixed ( float * p_v2 = v2 ) { 
  glRectfv( p_v1, p_v2 );
  } } 
}

public unsafe static void   glRectiv                 ( int[] v1, int[] v2 )
{
fixed ( int * p_v1 = v1 ) { 
fixed ( int * p_v2 = v2 ) { 
  glRectiv( p_v1, p_v2 );
  } } 
}

public unsafe static void   glRectsv                 ( short[] v1, short[] v2 )
{
fixed ( short * p_v1 = v1 ) { 
fixed ( short * p_v2 = v2 ) { 
  glRectsv( p_v1, p_v2 );
  } } 
}

public unsafe static void   glSelectBuffer           ( int size, uint[] buffer )
{
fixed ( uint * p_buffer = buffer ) { 
  glSelectBuffer( size, p_buffer );
  } 
}

public unsafe static void   glTexCoord1dv            ( double[] v )
{
fixed ( double * p_v = v ) { 
  glTexCoord1dv( p_v );
  } 
}

public unsafe static void   glTexCoord1fv            ( float[] v )
{
fixed ( float * p_v = v ) { 
  glTexCoord1fv( p_v );
  } 
}

public unsafe static void   glTexCoord1iv            ( int[] v )
{
fixed ( int * p_v = v ) { 
  glTexCoord1iv( p_v );
  } 
}

public unsafe static void   glTexCoord1sv            ( short[] v )
{
fixed ( short * p_v = v ) { 
  glTexCoord1sv( p_v );
  } 
}

public unsafe static void   glTexCoord2dv            ( double[] v )
{
fixed ( double * p_v = v ) { 
  glTexCoord2dv( p_v );
  } 
}

public unsafe static void   glTexCoord2fv            ( float[] v )
{
fixed ( float * p_v = v ) { 
  glTexCoord2fv( p_v );
  } 
}

public unsafe static void   glTexCoord2iv            ( int[] v )
{
fixed ( int * p_v = v ) { 
  glTexCoord2iv( p_v );
  } 
}

public unsafe static void   glTexCoord2sv            ( short[] v )
{
fixed ( short * p_v = v ) { 
  glTexCoord2sv( p_v );
  } 
}

public unsafe static void   glTexCoord3dv            ( double[] v )
{
fixed ( double * p_v = v ) { 
  glTexCoord3dv( p_v );
  } 
}

public unsafe static void   glTexCoord3fv            ( float[] v )
{
fixed ( float * p_v = v ) { 
  glTexCoord3fv( p_v );
  } 
}

public unsafe static void   glTexCoord3iv            ( int[] v )
{
fixed ( int * p_v = v ) { 
  glTexCoord3iv( p_v );
  } 
}

public unsafe static void   glTexCoord3sv            ( short[] v )
{
fixed ( short * p_v = v ) { 
  glTexCoord3sv( p_v );
  } 
}

public unsafe static void   glTexCoord4dv            ( double[] v )
{
fixed ( double * p_v = v ) { 
  glTexCoord4dv( p_v );
  } 
}

public unsafe static void   glTexCoord4fv            ( float[] v )
{
fixed ( float * p_v = v ) { 
  glTexCoord4fv( p_v );
  } 
}

public unsafe static void   glTexCoord4iv            ( int[] v )
{
fixed ( int * p_v = v ) { 
  glTexCoord4iv( p_v );
  } 
}

public unsafe static void   glTexCoord4sv            ( short[] v )
{
fixed ( short * p_v = v ) { 
  glTexCoord4sv( p_v );
  } 
}

public unsafe static void   glTexCoordPointer        ( int size, uint type, int stride, IntPtr pointer )
{
  glTexCoordPointer( size, type, stride, pointer );
}

public unsafe static void   glTexEnvfv               ( uint target, uint pname, float[] paramsx )
{
fixed ( float * p_paramsx = paramsx ) { 
  glTexEnvfv( target, pname, p_paramsx );
  } 
}

public unsafe static void   glTexEnviv               ( uint target, uint pname, int[] paramsx )
{
fixed ( int * p_paramsx = paramsx ) { 
  glTexEnviv( target, pname, p_paramsx );
  } 
}

public unsafe static void   glTexGendv               ( uint coord, uint pname, double[] paramsx )
{
fixed ( double * p_paramsx = paramsx ) { 
  glTexGendv( coord, pname, p_paramsx );
  } 
}

public unsafe static void   glTexGenfv               ( uint coord, uint pname, float[] paramsx )
{
fixed ( float * p_paramsx = paramsx ) { 
  glTexGenfv( coord, pname, p_paramsx );
  } 
}

public unsafe static void   glTexGeniv               ( uint coord, uint pname, int[] paramsx )
{
fixed ( int * p_paramsx = paramsx ) { 
  glTexGeniv( coord, pname, p_paramsx );
  } 
}

public unsafe static void   glTexImage1D             ( uint target, int level, int internalformat, int width, int border, uint format, uint type, IntPtr pixels )
{
  glTexImage1D( target, level, internalformat, width, border, format, type, pixels );
}

public unsafe static void   glTexImage2D             ( uint target, int level, int internalformat, int width, int height, int border, uint format, uint type, IntPtr pixels )
{
  glTexImage2D( target, level, internalformat, width, height, border, format, type, pixels );
}

public unsafe static void   glTexParameterfv         ( uint target, uint pname, float[] paramsx )
{
fixed ( float * p_paramsx = paramsx ) { 
  glTexParameterfv( target, pname, p_paramsx );
  } 
}

public unsafe static void   glTexParameteriv         ( uint target, uint pname, int[] paramsx )
{
fixed ( int * p_paramsx = paramsx ) { 
  glTexParameteriv( target, pname, p_paramsx );
  } 
}

public unsafe static void   glTexSubImage1D          ( uint target, int level, int xoffset, int width, uint format, uint type, IntPtr pixels )
{
  glTexSubImage1D( target, level, xoffset, width, format, type, pixels );
}

public unsafe static void   glTexSubImage2D          ( uint target, int level, int xoffset, int yoffset, int width, int height, uint format, uint type, IntPtr pixels )
{
  glTexSubImage2D( target, level, xoffset, yoffset, width, height, format, type, pixels );
}

public unsafe static void   glVertex2dv              ( double[] v )
{
fixed ( double * p_v = v ) { 
  glVertex2dv( p_v );
  } 
}

public unsafe static void   glVertex2fv              ( float[] v )
{
fixed ( float * p_v = v ) { 
  glVertex2fv( p_v );
  } 
}

public unsafe static void   glVertex2iv              ( int[] v )
{
fixed ( int * p_v = v ) { 
  glVertex2iv( p_v );
  } 
}

public unsafe static void   glVertex2sv              ( short[] v )
{
fixed ( short * p_v = v ) { 
  glVertex2sv( p_v );
  } 
}

public unsafe static void   glVertex3dv              ( double[] v )
{
fixed ( double * p_v = v ) { 
  glVertex3dv( p_v );
  } 
}

public unsafe static void   glVertex3fv              ( float[] v )
{
fixed ( float * p_v = v ) { 
  glVertex3fv( p_v );
  } 
}

public unsafe static void   glVertex3iv              ( int[] v )
{
fixed ( int * p_v = v ) { 
  glVertex3iv( p_v );
  } 
}

public unsafe static void   glVertex3sv              ( short[] v )
{
fixed ( short * p_v = v ) { 
  glVertex3sv( p_v );
  } 
}

public unsafe static void   glVertex4dv              ( double[] v )
{
fixed ( double * p_v = v ) { 
  glVertex4dv( p_v );
  } 
}

public unsafe static void   glVertex4fv              ( float[] v )
{
fixed ( float * p_v = v ) { 
  glVertex4fv( p_v );
  } 
}

public unsafe static void   glVertex4iv              ( int[] v )
{
fixed ( int * p_v = v ) { 
  glVertex4iv( p_v );
  } 
}

public unsafe static void   glVertex4sv              ( short[] v )
{
fixed ( short * p_v = v ) { 
  glVertex4sv( p_v );
  } 
}

public unsafe static void   glVertexPointer          ( int size, uint type, int stride, IntPtr pointer )
{
  glVertexPointer( size, type, stride, pointer );
}

//
//
// ############################################################################
// ############################################################################
// ############################################################################
// ############################################################################
//
//
} // public class GL
} // namespace OpenGL
//
//
// ############################################################################
// ############################################################################
// ############################################################################
// ############################################################################
