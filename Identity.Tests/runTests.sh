clear
rm -rf TestResults
dotnet test --settings coverlet.runsettings --collect:"XPlat Code Coverage"
dotnet reportgenerator -reports:"TestResults/*/coverage.cobertura.xml" -targetdir:"TestResults/CoverageReport" -reporttypes:Html
# google-chrome TestResults/CoverageReport/index.html &
# /opt/microsoft/msedge/msedge TestResults/CoverageReport/index.html &
