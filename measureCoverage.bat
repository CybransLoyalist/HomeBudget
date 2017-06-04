packages\OpenCover.4.6.519\tools\OpenCover.Console.exe -target:runtests.bat -register:user -filter:+[HomeBudget]* -excludebyattribute:*.ExcludeFromCoverage*
packages\ReportGenerator.2.5.8\tools\ReportGenerator.exe -reports:results.xml -targetdir:coverage 
pause

