# FFmpeg Batch AV Converter 3.1.1 for Linux Wine 9.0**

This version of FFmpeg Batch A/V Converter is designed to work in Linux by using Wine 9.x or higher. 
Instructions for installation will vary depending on Linux version

Example for Ubuntu 22: 
https://wine.htmlvalidator.com/install-wine-on-ubuntu-22.04.html
(Install mono on first run of FFBatch.exe)

For Uuntu/Debian, general installation instructions: 

* Wine info: https://gitlab.winehq.org/wine/wine/-/wikis/Debian-Ubuntu (wine64).
* Mono info: https://github.com/madewokherd/wine-mono/releases
* Mono x86_msi 9.3.1 package: https://github.com/madewokherd/wine-mono/releases/download/wine-mono-9.3.1/wine-mono-9.3.1-x86.msi
* To install, run: 'wine64 wine-mono-9.3.1-x86.msi'

Generic commands for Ubuntu/Debian:

------------------------------------------------------------------------------------------

sudo dpkg --add-architecture i386 
sudo mkdir -pm 755 /etc/apt/keyrings
sudo wget -O /etc/apt/keyrings/winehq-archive.key https://dl.winehq.org/wine-builds/winehq.key
sudo wget -NP /etc/apt/sources.list.d/ https://dl.winehq.org/wine-builds/debian/dists/bookworm/winehq-bookworm.sources #Para Debian
sudo wget -NP /etc/apt/sources.list.d/ https://dl.winehq.org/wine-builds/ubuntu/dists/mantic/winehq-mantic.sources #Para Ubuntu
sudo apt update
sudo apt install --install-recommends winehq-stable

------------------------------------------------------------------------------------------

After installation you will be able to run setup.exe or FFBatch.exe right-clicking on it and selecting "Open with wine". 

**NOTE**: When prompted,  install wine-mono or application will not work.

In order to avoid some runtime error and wrong size windows, in a terminal run 'winecfg':
* In applications tab, set application mode to 'Windows 10'.
* In desktop integration tab, Appearance, 'No theme'.

<img src="https://private-user-images.githubusercontent.com/907799/388438280-1d1489b3-c40c-41b2-9f44-b3467cc2decf.png?jwt=eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJnaXRodWIuY29tIiwiYXVkIjoicmF3LmdpdGh1YnVzZXJjb250ZW50LmNvbSIsImtleSI6ImtleTUiLCJleHAiOjE3MzIzODEwNjIsIm5iZiI6MTczMjM4MDc2MiwicGF0aCI6Ii85MDc3OTkvMzg4NDM4MjgwLTFkMTQ4OWIzLWM0MGMtNDFiMi05ZjQ0LWIzNDY3Y2MyZGVjZi5wbmc_WC1BbXotQWxnb3JpdGhtPUFXUzQtSE1BQy1TSEEyNTYmWC1BbXotQ3JlZGVudGlhbD1BS0lBVkNPRFlMU0E1M1BRSzRaQSUyRjIwMjQxMTIzJTJGdXMtZWFzdC0xJTJGczMlMkZhd3M0X3JlcXVlc3QmWC1BbXotRGF0ZT0yMDI0MTEyM1QxNjUyNDJaJlgtQW16LUV4cGlyZXM9MzAwJlgtQW16LVNpZ25hdHVyZT1jN2JhMGJhMTNiMjlmYWFlY2JhYTAzOTcwYjQzMGNiMmY4N2M2ZDRiOWZlOGY5MzFmZDNmOTFmN2U2ZGRlODdmJlgtQW16LVNpZ25lZEhlYWRlcnM9aG9zdCJ9.gCssOcXOABfIFGuew864eGbrMMzRhw0cXuKVOZyviSQ">
<img src="https://private-user-images.githubusercontent.com/907799/388438707-3f314333-decf-4dc4-98e5-4a58617a5339.png?jwt=eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJnaXRodWIuY29tIiwiYXVkIjoicmF3LmdpdGh1YnVzZXJjb250ZW50LmNvbSIsImtleSI6ImtleTUiLCJleHAiOjE3MzIzODEwNjIsIm5iZiI6MTczMjM4MDc2MiwicGF0aCI6Ii85MDc3OTkvMzg4NDM4NzA3LTNmMzE0MzMzLWRlY2YtNGRjNC05OGU1LTRhNTg2MTdhNTMzOS5wbmc_WC1BbXotQWxnb3JpdGhtPUFXUzQtSE1BQy1TSEEyNTYmWC1BbXotQ3JlZGVudGlhbD1BS0lBVkNPRFlMU0E1M1BRSzRaQSUyRjIwMjQxMTIzJTJGdXMtZWFzdC0xJTJGczMlMkZhd3M0X3JlcXVlc3QmWC1BbXotRGF0ZT0yMDI0MTEyM1QxNjUyNDJaJlgtQW16LUV4cGlyZXM9MzAwJlgtQW16LVNpZ25hdHVyZT1jMTE4MTIwMDI0YjU2YjNhZjE2MTI4YjUzZGJmZTRjMTYzNTQzZWVkZTNhYTkxNGI1YjYyYWUxNzY2NzZiM2M5JlgtQW16LVNpZ25lZEhlYWRlcnM9aG9zdCJ9.WziMyQqCpC7NDVrxkaOCsG2Yh47mgWg0L1RtXAO3qi0">

You can use the included shell script to create a shortcut to the application:
~~~
chmod +x wine-create-shourtcut.sh
./wine-create-shortcut /path/to/FFBatch.exe
~~~
