rm -rf ./pub-html
dotnet publish ./src/TGGDRTFN/TGGDRTFN.csproj -o ./pub-html -c Release 
rm -f ./pub-html/*.pdb
butler push pub-html/wwwroot thegrumpygamedev/tggd-rtfn:html
