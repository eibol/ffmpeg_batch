# FFmpeg Batch AV Converter 3.1.1 for Linux Wine 9.0

This version of FFmpeg Batch A/V Converter is designed to work in Linux by using Wine 9.x or higher. 
Instructions for installation will vary depending on Linux version. Staging (experimental version) is supported too.

Example for Ubuntu 22: 
https://wine.htmlvalidator.com/install-wine-on-ubuntu-22.04.html
(Install mono on first run of FFBatch.exe)

For Uuntu/Debian, general installation instructions: 

* Wine info: https://gitlab.winehq.org/wine/wine/-/wikis/Debian-Ubuntu (wine64).
* Mono info: https://github.com/madewokherd/wine-mono/releases
* Mono x86_msi 9.3.1 package: https://github.com/madewokherd/wine-mono/releases/download/wine-mono-9.3.1/wine-mono-9.3.1-x86.msi
* To install, run: 'wine64 wine-mono-9.3.1-x86.msi'

Generic commands for Ubuntu/Debian:

* sudo dpkg --add-architecture i386 
* sudo mkdir -pm 755 /etc/apt/keyrings
* sudo wget -O /etc/apt/keyrings/winehq-archive.key https://dl.winehq.org/wine-builds/winehq.key
* sudo wget -NP /etc/apt/sources.list.d/ https://dl.winehq.org/wine-builds/debian/dists/bookworm/winehq-bookworm.sources # Debian
* sudo wget -NP /etc/apt/sources.list.d/ https://dl.winehq.org/wine-builds/ubuntu/dists/mantic/winehq-mantic.sources # Ubuntu
* sudo apt update
* sudo apt install --install-recommends winehq-stable

After installation you will be able to run setup.exe or FFBatch.exe right-clicking on it and selecting "Open with wine". 

**NOTE**: When prompted,  install wine-mono or application will not work, unless you installed earlier (wine-mono-9.3.1-x86.msi). 

If you don't need visual styles, you can disable them at Desktop integration tab, Appearance, 'No theme'. It can slighly improve response time.

You can use the included shell script to create a shortcut to the application:
~~~
chmod +x wine-create-shourtcut.sh
./wine-create-shortcut /path/to/FFBatch.exe
~~~
