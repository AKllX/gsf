@ECHO OFF

ECHO �
ECHO ��������������������������������������������ͻ
ECHO �      TVA Code Library Source Retrieve      �
ECHO ��������������������������������������������ͼ
ECHO �
ECHO �
ECHO This build script will download and overwrite your local source code archive.
ECHO If you don't want to do this cancel this script now, otherwise
PAUSE

CALL SetPaths.bat

REM Make a local copy of the remote source code...
XCOPY %DEPLOYPATH%Source\*.* %SOURCE%*.* /E /V /C /H /R /K /Y

ECHO �
ECHO ��������������������������������������������ͻ
ECHO � TVA Code Library Source Retrieve Complete  �
ECHO ��������������������������������������������ͼ
ECHO �