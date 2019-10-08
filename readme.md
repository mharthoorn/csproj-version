# Vsn
Vsn is a lightweight command line tool to manage the file version in a .csproj file.

## Background
.NET core projects now work with a central version property in the .csproj file to control the version number of the app.
This version also goes into the final Assembly or executable.

## Setting the version
You can set the version in your csproj file explicitly by using 
```
	vsn set <version>
	
```
For example:
```
	vsn set 1.2.0-beta-3
```

## Bumping the version
```
	vsn bump <component>
		- major
		- minor
		- patch
		- pre (can be alpha, beta, prerelease, etc)
```

For example:
```
	> vsn
	0.3.0
	> vsn bump patch
	Version set to 0.3.1
```
With a prerelease, the number at the end will be increased. If there is no number, '-1' will be added:
```
	1.3.0-beta-4	wil be come 1.3.0-beta-5
	1.3.0-alpha		will become 1.3.0-alpha-1
```

## Getting the version
If you just type vsn, the current version of the project file will be printed. It will be only that, so that you can use it in your scripts.
This way you can use vsn to set variables. 
See these two Powershell examples:


```
	$version = vsn
```
You can even use it as a reference in a longer command:
```
	git tag $(vsn)
```

## Limitations
- Vsn currently only works if there is just one .csproj file in the current directory. 
- Vsn does not yet work with VersionPrefix or VersionSuffix
