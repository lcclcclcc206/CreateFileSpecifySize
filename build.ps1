$projLIBPath = "CreateFile/CreateFile.LIB.csproj"
$projCMDPath = "CreateFile.CMD/CreateFile.CMD.csproj"
$projUIPath = "CreateFile.UI/CreateFile.UI.csproj"

$publishPath = "publish/portable"


dotnet build $projLIBPath

dotnet publish $projCMDPath -o $publishPath --no-self-contained --arch x64

dotnet publish $projUIPath -o $publishPath --no-self-contained --arch x64
