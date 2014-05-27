#!/bin/bash 

DevelHome="/Users/opensim/develop"

# checking Metro inis
# echo "==== check (latest) Metro AkiSim OpenSim.ini against Database ========================"
# perl checkAkiOpenSimIniAgainstDatabase.pl "$DevelHome/akisim/doc/metropolis/ini/OpenSim.ini" Metropolis
# echo "==== check (latest) Metro Distribution OpenSim.ini against Database ========================"
# perl checkOpenSimIniAgainstDatabase.pl "$DevelHome/metrosim/bin/OpenSim.ini" Metropolis
# echo "==== check (latest) Metro Distribution OpenSimDefaults.ini against Database ========================"
# perl checkDefaultIniAgainstDatabase.pl "$DevelHome/metrosim/bin/OpenSimDefaults.ini"
# echo "==== check Database ageinst (latest) Metro AkiSim OpenSim.ini ========================"
# perl checkDatabaseAgainstAkiOpenSimIni.pl "$DevelHome/akisim/doc/metropolis/ini/OpenSim.ini" Metropolis
# echo "==== check Database ageinst (latest) Metro Distribution OpenSim.ini ========================"
# perl checkDatabaseAgainstOpenSimIni.pl "$DevelHome/metrosim/bin/OpenSim.ini" Metropolis
# echo "==== check Database ageinst (latest) Metro Distribution OpenSimDefaults.ini ========================"
# perl checkDatabaseAgainstDefaultIni.pl "$DevelHome/metrosim/bin/OpenSimDefaults.ini" 

# echo ""
# echo ""
# echo ""

# checking OSgrid inis
echo "==== check (lstest) OSgrid FreAkiSim OpenSim.ini against Database ========================"
perl checkAkiOpenSimIniAgainstDatabase.pl "$DevelHome/freakisim-dev/doc/osgrid/ini/OpenSim.ini" OSgrid
echo "==== check (latest) OSgrid Distribution OpenSim.ini against Database ========================"
perl checkOpenSimIniAgainstDatabase.pl "$DevelHome/osgridsim/bin/OpenSim.ini" OSgrid
echo "==== check (latest) OSgrid Distribution OpenSimDefaults.ini against Database ========================"
perl checkDefaultIniAgainstDatabase.pl "$DevelHome/osgridsim/bin/OpenSimDefaults.ini"
echo "==== check Database ageinst (latest) OSgrid FreAkiSim OpenSim.ini ========================"
perl checkDatabaseAgainstAkiOpenSimIni.pl "$DevelHome/freakisim-dev/doc/osgrid/ini/OpenSim.ini" OSgrid
echo "==== check Database ageinst (latest) OSgrid Distribution OpenSim.ini ========================"
perl checkDatabaseAgainstOpenSimIni.pl "$DevelHome/osgridsim/bin/OpenSim.ini" OSgrid
echo "==== check Database ageinst (latest) OSgrid Distribution OpenSimDefaults.ini ========================"
perl checkDatabaseAgainstDefaultIni.pl "$DevelHome/osgridsim/bin/OpenSimDefaults.ini" 

echo ""
echo ""
echo ""

echo "==== check arribasim-dev (GIT) OpenSimDefaults.ini against Database ========================"
perl checkDefaultIniAgainstDatabase.pl "$DevelHome/arribasim-dev/bin/OpenSimDefaults.ini"
echo "==== check Database ageinst (latest) arribasim-dev Distribution OpenSimDefaults.ini ========================"
perl checkDatabaseAgainstDefaultIni.pl "$DevelHome/arribasim-dev/bin/OpenSimDefaults.ini" 
