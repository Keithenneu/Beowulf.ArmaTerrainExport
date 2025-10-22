# Beowulf.ArmaTerrainExport
Just a quick and dirty height data grabber. Exports it to 16bit greyscale png. 1 meter per pixel.

"How hard can it be.."

Steps to use it:
- Compile the dll, e.g. by using Visual Studio (or download it from release [here](https://github.com/Keithenneu/Beowulf.ArmaTerrainExport/releases/download/0/bate_x64.dll))
- Copy the dll to the Arma directory, dll name should be "bate.dll" or "bate_x64.dll"
- Create the folder C:/arma3/terrain/ (can be changed in AddIn.cs, before building the dll)
- Start a mission on the terrain you want. Either copy the sqf file into the mission folder and execVm it, or just spawn the code from the debug console (`[] spawn {paste the code here}`)
- make sure the generated textfile from C:/arma3/terrain/ , and the topng.py script are in the same folder
- change the name of the text file in the python script, as well as the island size (currently 15360 for tanoa)
- run the script (i.e.: python topng.py). Python must be installed, along with the pillow library for it
- that should create the png. stuff like water level and scaling can be set in the python script.
