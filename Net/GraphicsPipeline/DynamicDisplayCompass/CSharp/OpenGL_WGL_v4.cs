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
// ############################################################################
// ############################################################################
// ############################################################################
// ############################################################################
//
// Namespace:     OpenGL
// Class:         WGL
// File Version:  4.0 (2003 Feb 14; Second Public Release)
//
// Description: C# Wrapper for miscellaneous Windows code to support OpenGL.
//
// WARNING: This class must be compiled using the /unsafe C# compiler switch
//          since many OpenGL functions involve pointers.
//
// Development Notes:
//
//   This file does not correspond to any particular C/C++ header file.
// I hand-picked a small set of functions necessary to support OpenGL 
// C# application development for Windows.
//
// Future Versions of this File:
//
//   This file is the first public release of a particular C# wrapper for
// Windows support for OpenGL applications.  Please visit the following web
// page for updates to this file:
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

using System;
using System.Runtime.InteropServices; // Necessary for [DllImport(...)]
namespace OpenGL
{
[ComVisible(false)]
public class WGL
{

// ############################################################################
// ############################################################################
// ############################################################################
// ############################################################################

// ============================================================================
//  int MessageBox
//    (
//    HWND hWnd,          // handle to owner window
//    LPCTSTR lpText,     // text in message box
//    LPCTSTR lpCaption,  // message box title
//    UINT uType          // message box style
//    );
// ============================================================================
[DllImport("user32")]
public static extern unsafe
int MessageBox( uint hwnd, string text, string caption, uint type );

public const uint MB_OK = 0;

// ############################################################################
// ############################################################################
// ############################################################################
// ############################################################################

// ============================================================================
//  HDC GetDC
//    (
//    HWND hWnd   // handle to window
//    );
// ============================================================================
[DllImport("user32")]
public static extern unsafe
uint GetDC( uint hwnd );

// ############################################################################
// ############################################################################
// ############################################################################
// ############################################################################

// ============================================================================
//
//  typedef struct tagPIXELFORMATDESCRIPTOR { // pfd   
//    WORD  nSize; 
//    WORD  nVersion; 
//    DWORD dwFlags; 
//    BYTE  iPixelType; 
//    BYTE  cColorBits; 
//    BYTE  cRedBits; 
//    BYTE  cRedShift; 
//    BYTE  cGreenBits; 
//    BYTE  cGreenShift; 
//    BYTE  cBlueBits; 
//    BYTE  cBlueShift; 
//    BYTE  cAlphaBits; 
//    BYTE  cAlphaShift; 
//    BYTE  cAccumBits; 
//    BYTE  cAccumRedBits; 
//    BYTE  cAccumGreenBits; 
//    BYTE  cAccumBlueBits; 
//    BYTE  cAccumAlphaBits; 
//    BYTE  cDepthBits; 
//    BYTE  cStencilBits; 
//    BYTE  cAuxBuffers; 
//    BYTE  iLayerType; 
//    BYTE  bReserved; 
//    DWORD dwLayerMask; 
//    DWORD dwVisibleMask; 
//    DWORD dwDamageMask; 
//  } PIXELFORMATDESCRIPTOR; 
//  
// ============================================================================
[StructLayout(LayoutKind.Sequential)] 
public struct PIXELFORMATDESCRIPTOR 
{
public ushort  nSize; 
public ushort  nVersion; 
public uint    dwFlags; 
public byte    iPixelType; 
public byte    cColorBits; 
public byte    cRedBits; 
public byte    cRedShift; 
public byte    cGreenBits; 
public byte    cGreenShift; 
public byte    cBlueBits; 
public byte    cBlueShift; 
public byte    cAlphaBits; 
public byte    cAlphaShift; 
public byte    cAccumBits; 
public byte    cAccumRedBits; 
public byte    cAccumGreenBits; 
public byte    cAccumBlueBits; 
public byte    cAccumAlphaBits; 
public byte    cDepthBits; 
public byte    cStencilBits; 
public byte    cAuxBuffers; 
public byte    iLayerType; 
public byte    bReserved; 
public uint    dwLayerMask; 
public uint    dwVisibleMask; 
public uint    dwDamageMask; 
// 40 bytes total
}

/* pixel types */
public const uint  PFD_TYPE_RGBA        = 0;
public const uint  PFD_TYPE_COLORINDEX  = 1;

/* layer types */
public const uint  PFD_MAIN_PLANE       = 0;
public const uint  PFD_OVERLAY_PLANE    = 1;
public const uint  PFD_UNDERLAY_PLANE   = 0xff; // (-1)

/* PIXELFORMATDESCRIPTOR flags */
public const uint  PFD_DOUBLEBUFFER            = 0x00000001;
public const uint  PFD_STEREO                  = 0x00000002;
public const uint  PFD_DRAW_TO_WINDOW          = 0x00000004;
public const uint  PFD_DRAW_TO_BITMAP          = 0x00000008;
public const uint  PFD_SUPPORT_GDI             = 0x00000010;
public const uint  PFD_SUPPORT_OPENGL          = 0x00000020;
public const uint  PFD_GENERIC_FORMAT          = 0x00000040;
public const uint  PFD_NEED_PALETTE            = 0x00000080;
public const uint  PFD_NEED_SYSTEM_PALETTE     = 0x00000100;
public const uint  PFD_SWAP_EXCHANGE           = 0x00000200;
public const uint  PFD_SWAP_COPY               = 0x00000400;
public const uint  PFD_SWAP_LAYER_BUFFERS      = 0x00000800;
public const uint  PFD_GENERIC_ACCELERATED     = 0x00001000;
public const uint  PFD_SUPPORT_DIRECTDRAW      = 0x00002000;

/* PIXELFORMATDESCRIPTOR flags for use in ChoosePixelFormat only */
public const uint  PFD_DEPTH_DONTCARE          = 0x20000000;
public const uint  PFD_DOUBLEBUFFER_DONTCARE   = 0x40000000;
public const uint  PFD_STEREO_DONTCARE         = 0x80000000;

// ############################################################################
// ############################################################################
// ############################################################################
// ############################################################################

// ============================================================================
// int ChoosePixelFormat
//   (
//   HDC  hdc,  // device context to search for a best pixel format 
//              // match
//   CONST PIXELFORMATDESCRIPTOR *  ppfd 
//              // pixel format for which a best match is sought
//   );
// ============================================================================
[DllImport("gdi32")]
public static extern unsafe
int ChoosePixelFormat( uint hdc, PIXELFORMATDESCRIPTOR * p_pfd );

// ############################################################################
// ############################################################################
// ############################################################################
// ############################################################################

// ============================================================================
// BOOL SetPixelFormat
//   (
//   HDC  hdc,  // device context whose pixel format the function 
//              // attempts to set
//   int  iPixelFormat,
//              // pixel format index (one-based)
//   CONST PIXELFORMATDESCRIPTOR *  ppfd 
//              // pointer to logical pixel format specification
//   );
// ============================================================================
[DllImport("gdi32")]
public static extern unsafe
int SetPixelFormat( uint hdc, int iPixelFormat, PIXELFORMATDESCRIPTOR * p_pfd );

// ############################################################################
// ############################################################################
// ############################################################################
// ############################################################################

// ============================================================================
// HGLRC wglCreateContext
//   (
//   HDC  hdc   // device context of device that the rendering context 
//              // will be suitable for
//   );
// ============================================================================
[DllImport("opengl32")]
public static extern unsafe
uint wglCreateContext( uint hdc );


// ############################################################################
// ############################################################################
// ############################################################################
// ############################################################################

// ============================================================================
// BOOL wglMakeCurrent
//   (
//   HDC  hdc,      // device context of device that OpenGL calls are 
//                  // to be drawn on
//   HGLRC  hglrc   // OpenGL rendering context to be made the calling 
//                  // thread's current rendering context
//   );
// ============================================================================
[DllImport("opengl32")]
public static extern unsafe
int wglMakeCurrent( uint hdc, uint hglrc );

// ############################################################################
// ############################################################################
// ############################################################################
// ############################################################################

// ============================================================================
// BOOL wglDeleteContext
//   (
//   HGLRC  hglrc   // handle to the OpenGL rendering context to delete
//   );
// ============================================================================
[DllImport("opengl32")]
public static extern unsafe
int wglDeleteContext( uint hglrc );

// ############################################################################
// ############################################################################
// ############################################################################
// ############################################################################

// ============================================================================
// BOOL wglSwapBuffers
//   (
//   HDC  hdc  // device context whose buffers get swapped
//   );
// ============================================================================
[DllImport("opengl32")]
public static extern unsafe
uint wglSwapBuffers( uint hdc );

// ############################################################################
// ############################################################################
// ############################################################################
// ############################################################################

// ============================================================================
// DWORD GetLastError(VOID);
// ============================================================================
[DllImport("kernel32")]
public static extern unsafe
uint GetLastError( );
// DWORD GetLastError(VOID);

// ############################################################################
// ############################################################################
// ############################################################################
// ############################################################################

// ============================================================================
// The following function, DemoCreateRenderingContext(ref_uint_DC,ref_uint_RC),
// can be used as a simple way to create an OpenGL "Rendering Context" (RC).
// **** DO NOT CALL DemoCreateRenderingContext() DIRECTLY IF YOU CHOOSE TO
// CALL DemoInitOpenGL() (below) TO ESTABLISH OPENGL. ****
// ============================================================================
public static unsafe void DemoCreateRenderingContext
  (
  ref uint  ref_uint_DC,
  ref uint  ref_uint_RC
  ) 
{
  ref_uint_RC = 0;

  PIXELFORMATDESCRIPTOR pfd = new PIXELFORMATDESCRIPTOR();

  // --------------------------------------------------------------------------
  pfd.nSize           = 40; // sizeof(PIXELFORMATDESCRIPTOR); 
  pfd.nVersion        = 1; 
  pfd.dwFlags         = (PFD_DRAW_TO_WINDOW |  PFD_SUPPORT_OPENGL |  PFD_DOUBLEBUFFER); 
  pfd.iPixelType      = (byte)(PFD_TYPE_RGBA);
  pfd.cColorBits      = 32; 
  pfd.cRedBits        = 0; 
  pfd.cRedShift       = 0; 
  pfd.cGreenBits      = 0; 
  pfd.cGreenShift     = 0; 
  pfd.cBlueBits       = 0; 
  pfd.cBlueShift      = 0; 
  pfd.cAlphaBits      = 0; 
  pfd.cAlphaShift     = 0; 
  pfd.cAccumBits      = 0; 
  pfd.cAccumRedBits   = 0; 
  pfd.cAccumGreenBits = 0;
  pfd.cAccumBlueBits  = 0; 
  pfd.cAccumAlphaBits = 0;
  pfd.cDepthBits      = 32; 
  pfd.cStencilBits    = 0; 
  pfd.cAuxBuffers     = 0; 
  pfd.iLayerType      = (byte)(PFD_MAIN_PLANE);
  pfd.bReserved       = 0; 
  pfd.dwLayerMask     = 0; 
  pfd.dwVisibleMask   = 0; 
  pfd.dwDamageMask    = 0; 
  // --------------------------------------------------------------------------



  // --------------------------------------------------------------------------
  // Choose Pixel Format
  // --------------------------------------------------------------------------
  int  iPixelFormat = 0;

  PIXELFORMATDESCRIPTOR* _pfd = &pfd;
  iPixelFormat = WGL.ChoosePixelFormat(ref_uint_DC, _pfd);

  if (0 == iPixelFormat)
    {
    uint   uint_LastError = WGL.GetLastError();
    string string_Message = "ChoosePixelFormat() FAILED:  Error: " + uint_LastError;
    WGL.MessageBox( 0, string_Message, "WGL.DemoGetRenderingContext() : ERROR", MB_OK );
    return;
    }
  // --------------------------------------------------------------------------


  // --------------------------------------------------------------------------
  // Set Pixel Format
  // --------------------------------------------------------------------------
  int int_Result_SPF = 0;

  int_Result_SPF = WGL.SetPixelFormat(ref_uint_DC, iPixelFormat, _pfd);

  if (0 == int_Result_SPF)
    {
    uint   uint_LastError = WGL.GetLastError();
    string string_Message = "SetPixelFormat() FAILED.  Error: " + uint_LastError;
    WGL.MessageBox( 0, string_Message, "WGL.DemoGetRenderingContext() : ERROR", MB_OK );
    return;
    }
  // --------------------------------------------------------------------------



  // --------------------------------------------------------------------------
  // Create Rendering Context (RC)
  // NOTE: You will get the following error:
  //             126 : ERROR_MOD_NOT_FOUND
  // if you attempt to create a render context too soon after creating a
  // window and getting its Device Context (DC).
  // See the comments for WGL.DemoInitOpenGL() on how to use a call to
  // WGL.wglSwapBuffers( ref_uint_DC ) before attempting to create the RC.
  // --------------------------------------------------------------------------
  ref_uint_RC = WGL.wglCreateContext( ref_uint_DC );

  if (0 == ref_uint_RC)
    {    
    uint   uint_LastError = WGL.GetLastError();
    string string_Message = "wglCreateContext() FAILED.  Error: " + uint_LastError;
    WGL.MessageBox( 0, string_Message, "WGL.DemoGetRenderingContext() : ERROR", MB_OK );
    return;
    }
  // --------------------------------------------------------------------------


  // --------------------------------------------------------------------------
  // Make the new Render Context (RC) the current Render Context (RC)
  // --------------------------------------------------------------------------
  int int_Result_MC = 0;

  int_Result_MC = WGL.wglMakeCurrent( ref_uint_DC, ref_uint_RC );

  if (0 == int_Result_MC)
    {
    uint   uint_LastError = WGL.GetLastError();
    string string_Message = "wglMakeCurrent() FAILED.  Error: " + uint_LastError;
    WGL.MessageBox( 0, string_Message, "WGL.DemoGetRenderingContext() : ERROR", MB_OK );
    // ***************************
    WGL.wglDeleteContext( ref_uint_RC );
    ref_uint_RC = 0;
    // ***************************
    return;
    }
  // --------------------------------------------------------------------------        
}

// ############################################################################
// ############################################################################
// ############################################################################
// ############################################################################

// ============================================================================
// The following can be used as an example of how to initialize OpenGL 
// rendering.  A System.Windows.Forms object can use a window handle acquired
// from (uint)((this.Handle).ToInt32()) as the "HWND" parameter.
//
// Here is a crude illustration of the use of WGL.DemoInitOpenGL() by a Form:
//
//    // ----------------------------------------------------------------------
//    public static uint            m_uint_HWND = 0;
//    public static uint            m_uint_DC   = 0;
//    public static uint            m_uint_RC   = 0;
//    
//    protected override void OnPaintBackground( PaintEventArgs e )
//    { 
//    // This overrides the System.Windows.Forms.Control protected method
//    // "OnPaintBackground()" so that we don't clear the client area of
//    // this form window -- so the OpenGL doesn't flicker on each frame.
//    }
//    
//    protected override void OnPaint( System.Windows.Forms.PaintEventArgs e )
//    {
//    if (0 == m_uint_RC)
//      {
//      m_uint_HWND = (uint)((this.Handle).ToInt32());
//      WGL.DemoInitOpenGL( m_uint_HWND, ref m_uint_DC, ref m_uint_RC );
//      }
//    if (0 != m_uint_RC)
//      {
//      WGL.DemoOpenGLDraw( this.Size.Width, this.Size.Height,  m_uint_DC );
//      }
//    System.Threading.Thread.Sleep( 10 ); // 10 msec --> 100 frames per second, max.
//    Invalidate(false); // Force OnPaint() to get called again.
//    }
//    // ----------------------------------------------------------------------
//
// ============================================================================
public static void DemoInitOpenGL
  (
  uint         uint_HWND,  // in
  ref uint ref_uint_DC,    // out
  ref uint ref_uint_RC     // out
  )
{
  ref_uint_DC   = WGL.GetDC( uint_HWND );

  // CAUTION: Not doing the following WGL.wglSwapBuffers() on the DC will
  // result in a failure to subsequently create the RC.
  WGL.wglSwapBuffers( ref_uint_DC );

  WGL.DemoCreateRenderingContext( ref ref_uint_DC, ref ref_uint_RC );
	
	if (0 == ref_uint_RC) 
	  {
    WGL.MessageBox( 0, "Failed to create OpenGL Rendering Context (RC)", 
                "WGL.DemoInitOpenGL() : ERROR", MB_OK );
    return;
	  }
}

// ############################################################################
// ############################################################################
// ############################################################################
// ############################################################################


// ============================================================================
// The following is an example of OpenGL rendering code, complete with
// buffer swapping.  This function can be called by a Form's "OnPaint()"
// method if a previous WGL.DemoInitOpenGL() call (for example) has
// already successfully established a valid Render Context (RC).
// ============================================================================

public static void DemoOpenGLDraw
  (
  int      int_WindowWidth,
  int      int_WindowHeight,
  uint     uint_DC
  )
{
  int int_Phase = (int)(System.Environment.TickCount % 120000);
  float float_Phase = (float)(0.3f * (int_Phase));

  if (int_WindowWidth  <= 0)  int_WindowWidth  = 1;
  if (int_WindowHeight <= 0)  int_WindowHeight = 1;

  GL.glViewport( 0, 0, int_WindowWidth, int_WindowHeight );

  GL.glEnable     ( GL.GL_DEPTH_TEST );
  GL.glDepthFunc  ( GL.GL_LEQUAL     );
  GL.glEnable     ( GL.GL_CULL_FACE  );
  GL.glCullFace   ( GL.GL_BACK       );
  GL.glClearColor ( 0.0f, 0.0f, 0.0f, 0.0f );
  GL.glClear      ( GL.GL_COLOR_BUFFER_BIT | GL.GL_DEPTH_BUFFER_BIT );
  GL.glMatrixMode ( GL.GL_PROJECTION );
  GL.glLoadIdentity();

  //GL.glOrtho( 0.0f, (float)(int_WindowWidth), 0.0f, (float)(int_WindowHeight), -1.0f, 1.0f );
  GLU.gluPerspective
      ( 
      60.0,    // Field of view angle (Y angle; degrees)
      ((double)(int_WindowWidth) / (double)(int_WindowHeight)), 
      1.0,     // Near plane
      1000.0   // Far  plane
      );

  GL.glMatrixMode ( GL.GL_MODELVIEW );
  GL.glLoadIdentity();

  // Translating the camera to +600.0f Z is essentially
  // adding -600.0f to all drawing commands.  
  GL.glTranslatef( 0.0f, 0.0f, -600.0f );

  GL.glRotatef( (0.11f * float_Phase), 1.0f, 0.0f, 0.0f );
  GL.glRotatef( (0.31f * float_Phase), 0.0f, 1.0f, 0.0f );
  GL.glRotatef( (0.19f * float_Phase), 0.0f, 0.0f, 1.0f );

  float[][] vert_xyz = new float[8][]
  {
  new float[] { -100.0f, -100.0f, -100.0f },  // 0
  new float[] { -100.0f, -100.0f,  100.0f },  // 1
  new float[] { -100.0f,  100.0f, -100.0f },  // 2
  new float[] { -100.0f,  100.0f,  100.0f },  // 3
  new float[] {  100.0f, -100.0f, -100.0f },  // 4
  new float[] {  100.0f, -100.0f,  100.0f },  // 5
  new float[] {  100.0f,  100.0f, -100.0f },  // 6
  new float[] {  100.0f,  100.0f,  100.0f }   // 7
  };
  int [][] tri_abc = new int [12][]
  {
  new int[] {0,2,4}, new int[] {4,2,6}, // Back
  new int[] {0,4,1}, new int[] {1,4,5}, // Bottom
  new int[] {0,1,2}, new int[] {2,1,3}, // Left
  new int[] {4,6,5}, new int[] {5,6,7}, // Right
  new int[] {2,3,6}, new int[] {6,3,7}, // Top
  new int[] {1,5,3}, new int[] {3,5,7}  // Front
  };
  float[][] colors_rgb = new float[12][]
  {
  new float[] {0.5f,0.1f,0.1f }, new float[] {1.0f,0.1f,0.1f }, // Red
  new float[] {0.5f,0.5f,0.1f }, new float[] {1.0f,1.0f,0.1f }, // Yellow
  new float[] {0.1f,0.5f,0.1f }, new float[] {0.1f,1.0f,0.1f }, // Green
  new float[] {0.1f,0.5f,0.5f }, new float[] {0.1f,1.0f,1.0f }, // Cyan
  new float[] {0.1f,0.1f,0.5f }, new float[] {0.1f,0.1f,1.0f }, // Blue
  new float[] {0.5f,0.1f,0.5f }, new float[] {1.0f,0.1f,1.0f }  // Magenta
  };

  int iTriTotal = 12;
  int iTriIndex = 0;
  GL.glBegin( GL.GL_TRIANGLES );
  for ( iTriIndex = 0; iTriIndex < iTriTotal; iTriIndex++ )
    {
    GL.glColor3fv( colors_rgb[iTriIndex] );
    GL.glVertex3fv( vert_xyz[tri_abc[iTriIndex][0]] );

    GL.glColor3fv( colors_rgb[iTriIndex] );
    GL.glVertex3fv( vert_xyz[tri_abc[iTriIndex][1]] );

    GL.glColor3fv( colors_rgb[iTriIndex] );
    GL.glVertex3fv( vert_xyz[tri_abc[iTriIndex][2]] );
    }
  GL.glEnd();

  WGL.wglSwapBuffers( uint_DC );
}


// ############################################################################
// ############################################################################
// ############################################################################
// ############################################################################
//
//
} // public class WGL
} // namespace OpenGL
//
//
// ############################################################################
// ############################################################################
// ############################################################################
// ############################################################################
