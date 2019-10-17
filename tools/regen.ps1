Set-Alias java "S:\Program Files (x86)\Minecraft\runtime\jre-x64\bin\java.exe"
java -jar $PSScriptRoot\swagger-codegen-cli.jar help generate -i https://todoserver1.azurewebsites.net/swagger/v1/swagger.json -l csharp-dotnet2 -o $PSScriptRoot\generated
