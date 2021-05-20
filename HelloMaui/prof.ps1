./profile-android.ps1 `
    -project HelloMaui.csproj `
    -package com.microsoft.hellomaui `
    -configuration Release `
    -extra /p:DefineConstants=USE_MS `
    -dotnet dotnet `
    -iterations 20

./profile-android.ps1 `
    -project HelloMaui.csproj `
    -package com.microsoft.hellomaui `
    -configuration Release `
    -dotnet dotnet `
    -iterations 20
