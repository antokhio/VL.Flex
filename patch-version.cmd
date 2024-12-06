setlocal enableextensions disabledelayedexpansion

set "search={{ VERSION }}"
set "replace=%1"

set "textFile=test.nuspec"

for /f "delims=" %%i in ('type "%textFile%" ^& break ^> "%textFile%" ') do (
    set "line=%%i"
    setlocal enabledelayedexpansion
    >>"%textFile%" echo(!line:%search%=%replace%!
    endlocal
)
