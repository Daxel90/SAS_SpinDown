# SAS_SpinDown

### Description

**SAS_SpinDown** is an utility to Spin Down SAS disk manualy or automaticaly, can be configured to use only some disks.

### SAS_SpinDown 1.00 Usage
**"SAS_SpinDown STATUS"** to see disks status
**"SAS_SpinDown OFF"** Standby all disks
**"SAS_SpinDown ON"** PowerOn all disks
**"SAS_SpinDown AUTO N"** Start automatic SpinDown after N min

### Command used by the software
**"sg_map"** to map sdX disks to sgX disks
**"sg_start -vvv --pc=3 /dev/sgX"** to Spin Down the disks
**"sg_start --start /dev/sgX"** to Spin Up the disks
**"sdparm --command=sense /dev/sgX"** to check if disk is in Standby or not
**"smartctl --all /dev/sgX"** to read Disk temperature
**"lsblk -J -o NAME,LABEL,MOUNTPOINT"** to get mount point and lables
**"cat /proc/diskstats"** to detect when disk is non used and autimaticaly Spin Down

### Config File Examples
The config file is automaticaly created and filled with all disks in the system after the first **"SAS_SpinDown STATUS"**
You can edit the config.cfg files and disable the unwanted disks
Example:
    sda = False
    sdb = True
    sdc = True
    sdd = True
    sde = True
    sdf = True
    sdg = True
    sdh = True
    sdi = True
    sdj = True
    sdk = True
    sdl = True
    sdm = True
    sdn = True
    sdo = True



### Output Example of STATUS
    sg1    sdb    G08    /mnt/G08     ON           26  C
    sg2    sdc    G06    /mnt/G06     STANDBY      -   C
    sg3    sdd    G02    /mnt/G02     STANDBY      -   C
    sg4    sde    G04    /mnt/G04     ON           25  C
    sg5    sdf    H07    /mnt/H07     ON           26  C
    sg6    sdg    H05    /mnt/H05     STANDBY      -   C
    sg7    sdh    H03    /mnt/H03     STANDBY      -   C
    sg8    sdi    H01    /mnt/H01     ON           26  C
    sg9    sdj    G07    /mnt/G07     STANDBY      -   C
    sg10   sdk    G05    /mnt/G05     ON           27  C
    sg11   sdl    G03    /mnt/G03     STANDBY      -   C
    sg12   sdm    G01    /mnt/G01     ON           25  C
    sg13   sdn    H08    /mnt/H08     ON           24  C
    sg14   sdo    H06    /mnt/H06     STANDBY      -   C

