@ECHO OFF

ECHO �
ECHO ��������������������������������������������ͻ
ECHO �            Updating Build Log...           �
ECHO ��������������������������������������������ͼ
ECHO �

COPY /Y %DEPLOYPATH%BuildScripts\Build.log Build.log

ECHO Build %BUILDVER% compiled at %DATE% %TIME% by %USERDOMAIN%\%USERNAME% from %COMPUTERNAME% >>Build.log

COPY /Y Build.log %DEPLOYPATH%BuildScripts\Build.log
