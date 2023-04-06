# Windows Update Command Line Interface
**wucli** is a command line interface for using Windows Update. 

**wucli.exe --help**
```
Description:
  Command line interface for working with Windows Updates.

Usage:
  wucli [command] [options]

Options:
  --version       Show version information
  -?, -h, --help  Show help and usage information

Commands:
  install         Install all available updates on this machine.
  list-available  Get the updates that are available for this machine.
  list-installed  Get the updates that are installed on this machine.
```

**wucli.exe install --help**
```
Description:
  Install all available updates on this machine.

Usage:
  wucli install [options]

Options:
  -c, --criteria <criteria>  The criteria for updates (Default: "IsInstalled=0 AND IsHidden=0")
  -r, --reboot               Reboot the machine if any updates require a reboot.
  -?, -h, --help             Show help and usage information
  ```