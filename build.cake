var target = Argument("target", "Test");
var configuration = Argument("configuration", "Release");
var solution = "./HybridRequests.sln";

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("Clean")
    .Does(() =>
{
    CleanDirectory($"./src/HybridRequests/bin/{configuration}");
});

Task("Restore")
    .IsDependentOn("Clean")
    .Does(() => 
    {
        DotNetCoreRestore(solution, new DotNetCoreRestoreSettings
        {
            NoCache = true,
        });
    });

Task("Build")
    .IsDependentOn("Restore")
    .Does(() =>
{
    DotNetCoreBuild(solution, new DotNetCoreBuildSettings
    {
        Configuration = configuration,
    });
});

Task("Test")
    .IsDependentOn("Build")
    .Does(() =>
{
    DotNetCoreTest(solution, new DotNetCoreTestSettings
    {
        Configuration = configuration,
        NoBuild = true,
    });
});

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);