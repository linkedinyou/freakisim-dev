#!/bin/bash 

DevelHome="/Users/opensim/src"

# generating Metro OpenSim.ini
perl generateOpenSimIni.pl "$DevelHome/freakisim-dev/doc/metropolis/ini/OpenSim.ini" Metropolis

# generating OSgrid OpenSim.ini
perl generateOpenSimIni.pl "$DevelHome/freakisim-dev/doc/osgrid/ini/OpenSim.ini" OSgrid

echo "OpenSim.ini generated for Metropolis and OSgrid"