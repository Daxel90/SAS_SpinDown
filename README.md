# SAS_SpinDown

### Description
**SAS_SpinDown** is a utility to manually or automatically spin down SAS disks.  
It can be configured to manage only selected disks.

---

### Usage (v1.00)
- **`SAS_SpinDown STATUS`** → Show disk status  
- **`SAS_SpinDown OFF`** → Put all disks in standby  
- **`SAS_SpinDown ON`** → Power on all disks  
- **`SAS_SpinDown AUTO N`** → Start automatic spin down after *N* minutes of inactivity 

---

### Commands Used by the Software
- **`sg_map`** → Map `sdX` disks to `sgX` disks  
- **`sg_start -vvv --pc=3 /dev/sgX`** → Spin down the disks  
- **`sg_start --start /dev/sgX`** → Spin up the disks  
- **`sdparm --command=sense /dev/sgX`** → Check if a disk is in standby  
- **`smartctl --all /dev/sgX`** → Read disk temperature  
- **`lsblk -J -o NAME,LABEL,MOUNTPOINT`** → Get mount points and labels  
- **`cat /proc/diskstats`** → Detect when a disk is unused and automatically spin it down  

---

### Config File Example
The config file is automatically created and populated with all system disks after the first **`SAS_SpinDown STATUS`** command.  
You can edit `config.cfg` to disable unwanted disks.

Example:
```ini
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
```

---

### Output Example (STATUS Command)
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
