if (!canSuspend) exitWith { "Must be spawned!" };

"bate" callExtension ["new", []];
private _worldsize = worldSize;
startLoadingScreen ["Exporting..."];
for "_x" from 0 to _worldsize do {
    
    for "_y" from 0 to _worldsize do {
        "bate" callExtension ["data", [_x, _y, getTerrainHeightASL [_x, _y]]];
    };
    progressLoadingScreen (_x / _worldsize);
};
"bate" callExtension ["end", []];
endLoadingScreen;