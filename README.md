# OffensivePowerShellTasking
Run multiple PowerShell scripts concurrently in different app domains.  Solves the offensive security problem of running multiple PowerShell scripts concurrently without without spawning powershell.exe and without the scripts causing problems with each other (usually due to PInvoke'd functions). 

Note: This project continually loads/unloads .NET AppDomains.  During testing, I found that there appears to be a memory leak in somewhere in the .NET core's code when loading/unloading AppDomains (~1 MB every 30 load/unload events).
