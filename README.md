# Beowulf.ArmaTerrainExport
Just a quick and dirty height data grabber. Exports it to 16bit greyscale png. 1 meter per pixel.

"How hard can it be.."

Steps to use it:
- Compile the dll, e.g. by using Visual Studio
- Copy the dll to the Arma directory, dll name should be "bate.dll" or "bate_x64.dll"
- Create the folder C:/arma3/terrain/ (can be changed in AddIn.cs, before building the dll)
- Start a mission on the terrain you want. Either copy the sqf file into the mission folder and execVm it, or just spawn the code from the debug console (`[] spawn {paste the code here}`)
- make sure the generated textfile from C:/arma3/terrain/ , and the topng.py script are in the same folder
- rename the generated textfile to todo.txt
- run the script (i.e.: python topng.py). Python must be installed, along with the pillow library for it
- that should create the png. minimum and maximum elevation data and scaling are detected automatically