@ECHO OFF
SET VEDEBUG=
IF "%2"=="Debug" SET VEDEBUG=/debug
IF "%1"=="" GOTO INITVARS
IF "%1"=="InitVars" GOTO INITVARS
GOTO COMPILE
:INITVARS
CALL "c:\Program Files\Microsoft Visual Studio .NET\Common7\Tools\vsvars32.bat" >NUL
:COMPILE
ECHO �
ECHO ��������������������������������������������ͻ
IF "%VEDEBUG%"=="" ECHO �       Compiling VariableEvaluator.js...    �
IF NOT "%VEDEBUG%"=="" ECHO �   Compiling DEBUG VariableEvaluator.js...  �
ECHO ��������������������������������������������ͼ
ECHO �
IF "%SOURCE%"=="" SET SOURCE=..\
jsc %VEDEBUG% /out:%SOURCE%Config\ref\TVA.VarEval.dll /t:library %SOURCE%Config\VariableEvaluator.js
IF "%1"=="" GOTO DOPAUSE
IF "%1"=="InitVars" GOTO DOPAUSE
GOTO CONTINUE
:DOPAUSE
ECHO �
ECHO If no errors appear, then compile was successful.
ECHO Check ref directory for updated TVA.VarEval.dll.
ECHO �
PAUSE
:CONTINUE
