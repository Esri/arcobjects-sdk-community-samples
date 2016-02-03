
GlobeDigitizePointps.dll: dlldata.obj GlobeDigitizePoint_p.obj GlobeDigitizePoint_i.obj
	link /dll /out:GlobeDigitizePointps.dll /def:GlobeDigitizePointps.def /entry:DllMain dlldata.obj GlobeDigitizePoint_p.obj GlobeDigitizePoint_i.obj \
		kernel32.lib rpcndr.lib rpcns4.lib rpcrt4.lib oleaut32.lib uuid.lib \

.c.obj:
	cl /c /Ox /DWIN32 /D_WIN32_WINNT=0x0400 /DREGISTER_PROXY_DLL \
		$<

clean:
	@del GlobeDigitizePointps.dll
	@del GlobeDigitizePointps.lib
	@del GlobeDigitizePointps.exp
	@del dlldata.obj
	@del GlobeDigitizePoint_p.obj
	@del GlobeDigitizePoint_i.obj
