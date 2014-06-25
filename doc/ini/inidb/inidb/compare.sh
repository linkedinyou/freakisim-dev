#!/bin/bash 

DevelHome="/Users/opensim/develop"

echo ""
echo "Metropolis: Checking OpenSim.ini Files."
echo ""

# checking Metro inis
echo "==== check (latest) Metro AkiSim OpenSim.ini against Database ========================"
perl checkAkiOpenSimIniAgainstDatabase.pl "$DevelHome/freakisim-dev/doc/metropolis/ini/OpenSim.ini" Metropolis
echo "==== check (latest) Metro Distribution OpenSim.ini against Database ========================"
perl checkOpenSimIniAgainstDatabase.pl "$DevelHome/metrosim/bin/OpenSim.ini" Metropolis
echo "==== check Database ageinst (latest) Metro AkiSim OpenSim.ini ========================"
perl checkDatabaseAgainstAkiOpenSimIni.pl "$DevelHome/freakisim-dev/doc/metropolis/ini/OpenSim.ini" Metropolis
echo "==== check Database ageinst (latest) Metro Distribution OpenSim.ini ========================"
perl checkDatabaseAgainstOpenSimIni.pl "$DevelHome/metrosim/bin/OpenSim.ini" Metropolis

echo ""
echo "OSGrid:     Checking OpenSim.ini Files."
echo ""

# checking OSgrid inis
echo "==== check (lstest) OSgrid FreAkiSim OpenSim.ini against Database ========================"
perl checkAkiOpenSimIniAgainstDatabase.pl "$DevelHome/freakisim-dev/doc/osgrid/ini/OpenSim.ini" OSgrid
echo "==== check (latest) OSgrid Distribution OpenSim.ini against Database ========================"
perl checkOpenSimIniAgainstDatabase.pl "$DevelHome/osgridsim/bin/OpenSim.ini" OSgrid
echo "==== check Database ageinst (latest) OSgrid FreAkiSim OpenSim.ini ========================"
perl checkDatabaseAgainstAkiOpenSimIni.pl "$DevelHome/freakisim-dev/doc/osgrid/ini/OpenSim.ini" OSgrid
echo "==== check Database ageinst (latest) OSgrid Distribution OpenSim.ini ========================"
perl checkDatabaseAgainstOpenSimIni.pl "$DevelHome/osgridsim/bin/OpenSim.ini" OSgrid

echo ""
echo "Metropolis: Checking OpenSimDefaults.ini against Database"
echo ""

echo "==== check (latest) Metro Distribution OpenSimDefaults.ini against Database ========================"
perl checkDefaultIniAgainstDatabase.pl "$DevelHome/metrosim/bin/OpenSimDefaults.ini" Metropolis
echo "==== check Database ageinst (latest) Metro Distribution OpenSimDefaults.ini ========================"
perl checkDatabaseAgainstDefaultIni.pl "$DevelHome/metrosim/bin/OpenSimDefaults.ini" Metropolis

echo ""
echo "OSGrid:     Checking OpenSimDefaults.ini against Database"
echo ""
echo "==== check (latest) OSgrid Distribution OpenSimDefaults.ini against Database ========================"
perl checkDefaultIniAgainstDatabase.pl "$DevelHome/osgridsim/bin/OpenSimDefaults.ini" OSgrid
echo "==== check Database ageinst (latest) OSgrid Distribution OpenSimDefaults.ini ========================"
perl checkDatabaseAgainstDefaultIni.pl "$DevelHome/osgridsim/bin/OpenSimDefaults.ini" OSgrid


echo ""
echo "Arriba-Sim: Checking OpenSimDefaults.ini against Database"
echo ""

echo "==== check arribasim-dev (GIT) OpenSimDefaults.ini against Database ========================"
perl checkDefaultIniAgainstDatabase.pl "$DevelHome/arribasim-dev/OpenSim/Region/Application/Resources/OpenSimDefaults.ini" Repo
echo "==== check Database ageinst (latest) arribasim-dev Distribution OpenSimDefaults.ini ========================"
perl checkDatabaseAgainstDefaultIni.pl "$DevelHome/arribasim-dev/OpenSim/Region/Application/Resources/OpenSimDefaults.ini" Repo

echo ""
echo "OpenSim:   Checking OpenSimDefaults.ini against Database"
echo ""

echo "==== check opensim (GIT) OpenSimDefaults.ini against Database ========================"
perl checkDefaultIniAgainstDatabase.pl "$DevelHome/opensim/bin/OpenSimDefaults.ini" Repo
echo "==== check Database ageinst (latest) opensim Distribution OpenSimDefaults.ini ========================"
perl checkDatabaseAgainstDefaultIni.pl "$DevelHome/opensim/bin/OpenSimDefaults.ini" Repo
