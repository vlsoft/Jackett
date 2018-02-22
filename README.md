﻿# Jackett

[![GitHub issues](https://img.shields.io/github/issues/Jackett/Jackett.svg?maxAge=60&style=flat-square)](https://github.com/Jackett/Jackett/issues)
[![GitHub pull requests](https://img.shields.io/github/issues-pr/Jackett/Jackett.svg?maxAge=60&style=flat-square)](https://github.com/Jackett/Jackett/pulls)
[![Github Releases](https://img.shields.io/github/downloads/Jackett/Jackett/total.svg?maxAge=60&style=flat-square)](https://github.com/Jackett/Jackett/releases/latest)
[![Docker Pulls](https://img.shields.io/docker/pulls/linuxserver/jackett.svg?maxAge=60&style=flat-square)](https://hub.docker.com/r/linuxserver/jackett/)

This project is a new fork and is recruiting development help.  If you are able to help out please contact us.

Jackett works as a proxy server: it translates queries from apps ([Sonarr](https://github.com/Sonarr/Sonarr), [Radarr](https://github.com/Radarr/Radarr), [SickRage](https://sickrage.github.io/), [CouchPotato](https://couchpota.to/), [Mylar](https://github.com/evilhero/mylar), etc) into tracker-site-specific http queries, parses the html response, then sends results back to the requesting software. This allows for getting recent uploads (like RSS) and performing searches. Jackett is a single repository of maintained indexer scraping & translation logic - removing the burden from other apps.

Developer note: The software implements the [Torznab](https://github.com/Sonarr/Sonarr/wiki/Implementing-a-Torznab-indexer) (with [nZEDb](https://github.com/nZEDb/nZEDb/blob/dev/docs/newznab_api_specification.txt) category numbering) and [TorrentPotato](https://github.com/RuudBurger/CouchPotatoServer/wiki/Couchpotato-torrent-provider) APIs.



#### Supported Systems
* Windows using .NET 4.5
* Linux and OSX using Mono 4 (mono 3 is no longer supported).

### Supported Public Trackers
 * EZTV
 * Isohunt
 * KickAssTorrent
 * KickAssTorrent (kat.how clone)
 * LimeTorrents
 * RARBG
 * ShowRSS
 * Sky torrents
 * The Pirate Bay
 * TorrentProject
 * Torrentz2
 
### Supported Private Trackers
 * 2 Fast 4 You
 * 7tor
 * Abnormal
 * Acid-Lounge
 * AlphaRatio
 * AlphaReign
 * Andraste
 * AnimeBytes
 * AnimeTorrents
 * AOX
 * Apollo (XANAX)
 * ArabaFenice
 * AsianDVDClub
 * Avistaz
 * BakaBT  [![(invite needed)][inviteneeded]](#)
 * bB
 * Best Friends
 * BeyondHD
 * Bit-City Reloaded
 * BIT-HDTV
 * BitHQ
 * BitHUmen
 * BitMeTV
 * BitSoup  [![(invite needed)][inviteneeded]](#)
 * Bitspyder
 * Blu-bits
 * BlueBird
 * BroadcastTheNet  [![(invite needed)][inviteneeded]](#)
 * BTN
 * BTNext
 * Carpathians
 * CHDBits
 * CinemaZ
 * CZTeam
 * DanishBits
 * DataScene
 * Demonoid
 * Diablo Torrent
 * DigitalHive
 * Dream Team
 * EoT-Forum
 * eStone
 * Ethor.net (Thor's Land)
 * FANO.IN
 * FileList
 * Freedom-HD
 * Freshon
 * FullMixMusic
 * FunFile
 * FunkyTorrents
 * Fuzer
 * GFXPeers
 * Ghost City
 * GimmePeers <!-- maintained by jamesb2147 -->
 * GODS  [![(invite needed)][inviteneeded]](#)
 * Gormogon
 * Hardbay
 * HD4Free
 * HD-Space
 * HD-Torrents
 * HDClub
 * HDHome
 * HDPter
 * HDSky
 * Hebits
 * Hon3y HD
 * Hounddawgs
 * House-of-Torrents
 * Hyperay
 * ICE Torrent
 * Immortalseed
 * Infinity-T
 * inPeril
 * Insane Tracker
 * IPTorrents
 * JPopsuki
 * Kapaki
 * Le Paradis Du Net
 * LinkoManija
 * LosslessClub
 * M-Team - TP
 * Magico
 * Majomparádé 
 * Mononoké-BT
 * MoreThanTV
 * MyAnonamouse
 * myAmity
 * MySpleen
 * Nachtwerk
 * NCore
 * NetHD
 * New Real World
 * NextGen
 * Norbits  [![(invite needed)][inviteneeded]](#) <!-- added by DiseaseNO but no longer maintained? -->
 * nostream
 * notwhat.cd
 * PassThePopcorn  [![(invite needed)][inviteneeded]](#)
 * PirateTheNet
 * PiXELHD
 * PolishSource
 * Pretome
 * PrivateHD
 * PTFiles
 * QcTorrent
 * RapideTracker
 * Redacted (PassTheHeadphones)
 * RevolutionTT
 * Rockhard Lossless
 * RoDVD
 * RuTracker
 * SceneAccess
 * SceneFZ
 * SceneTime
 * SDBits
 * Secret Cinema
 * Shareisland
 * ShareSpaceDB
 * Shazbat  [![(invite needed)][inviteneeded]](#)
 * Shellife
 * SpeedCD
 * Superbits
 * The Horror Charnel
 * The New Retro
 * The Shinning
 * The-Torrents
 * TehConnection
 * TenYardTracker
 * Torrent Network
 * Torrent Sector Crew
 * Torrent411
 * TorrentBD
 * TorrentBytes
 * TorrentDay
 * TorrentHeaven
 * TorrentHR
 * Torrenting
 * TorrentLeech
 * Torrents.Md
 * TorrentShack
 * Torrent-Syndikat
 * ToTheGlory
 * TranceTraffic
 * TransmitheNet
 * TV Chaos UK
 * TV-Vault
 * u-Torrent
 * UHDBits
 * ULTRAHDCLUB
 * World-In-HD  [![(invite needed)][inviteneeded]](#)
 * WorldOfP2P
 * x264
 * XSpeeds
 * Xthor
 * Xtreme Zone 

Trackers marked with  [![(invite needed)][inviteneeded]](#) have no active maintainer and are missing features or are broken. If you have an invite for them please send it to kaso1717 -at- gmail.com to get them fixed/improved.
 
## Installation on Windows

We recommend you install Jackett as a Windows service using the supplied installer. You may also download the zipped version if you would like to configure everything manually.

To get started with using the installer for Jackett, follow the steps below:

1. Download the latest version of the Windows installer, "Jackett.Installer.Windows.exe" from the [releases](https://github.com/Jackett/Jackett/releases/latest) page.
2. When prompted if you would like this app to make changes to your computer, select "yes".
3. If you would like to install Jackett as a Windows Service, make sure the "Install as Windows Service" checkbox is filled.
4. Once the installation has finished, check the "Launch Jackett" box to get started.
5. Navigate your web browser to: http://127.0.0.1:9117
6. You're now ready to begin adding your trackers and using Jackett.

When installed as a service the tray icon acts as a way to open/start/stop Jackett. If you opted to not install it as a service then Jackett will run its web server from the tray tool.

Jackett can also be run from the command line if you would like to see log messages (Ensure the server isn't already running from the tray/service). This can be done by using "JackettConsole.exe" (for Command Prompt), found in the Jackett data folder: "%ProgramData%\Jackett". 

## Installation on Linux/OSX
 1. Install [Mono 4](http://www.mono-project.com/download/#download-lin) or better (version 4.8 is recommended)
       * Follow the instructions on the mono website and install the `mono-devel` and the `ca-certificates-mono` packages.
       * On Red Hat/CentOS/openSUSE/Fedora the `mono-locale-extras` package is also required.
 2. Install  libcurl:
       * Debian/Ubunutu: `apt-get install libcurl4-openssl-dev`
       * Redhat/Fedora: `yum install libcurl-devel`
       * For other distros see the  [Curl docs](http://curl.haxx.se/dlwiz/?type=devel).
 3. Download and extract the latest `Jackett.Binaries.Mono.tar.gz` release from the [releases page](https://github.com/Jackett/Jackett/releases) and run Jackett using mono with the command `mono --debug JackettConsole.exe`.

Detailed instructions for [Ubuntu 14.x](http://www.htpcguides.com/install-jackett-on-ubuntu-14-x-for-custom-torrents-in-sonarr/) and [Ubuntu 15.x](http://www.htpcguides.com/install-jackett-ubuntu-15-x-for-custom-torrents-in-sonarr/)

## Installation using Docker
Detailed instructions are available at [LinuxServer.io Jackett Docker](https://hub.docker.com/r/linuxserver/jackett/). The Jackett Docker is highly recommended, especially if you are having Mono stability issues or having issues running Mono on your system eg. QNAP, Synology. Thanks to [LinuxServer.io](https://linuxserver.io)

## Installation on Synology
Jackett is available as beta package from [SynoCommuniy](https://synocommunity.com/)

## Troubleshooting

* __Command line switches__

  You can pass various options when running via the command line, see --help for details.

* __Unable to  connect to trackers with invalid SSL certificates__

  You can disable certificate validation using the `--IgnoreSslErrors true` option but it's not recommended to use it as it enables Man-in-the-middle attacks on your connections.

*  __Enable logging__

  You can get additional logging with the command line switches `-t -l` or by enabeling `Enhanced logging` via the web interface.
  Please post logs if you are unable to resolve your issue with these switches ensuring to remove your username/password/cookies.
  The logfiles (log.txt/updater.txt) are stored in `%ProgramData%\Jackett` on Windows and `~/.config/Jackett/` on Linux/OSX.

## Creating an issue
Please supply as much information about the problem you are experiencing as possible. Your issue has a much greater chance of being resolved if logs are supplied so that we can see what is going on. Creating an issue with '### isn't working' doesn't help anyone to fix the problem.

## Contributing
All contributions are welcome just send a pull request.  Jackett's framework allows our team (and any other volunteering dev) to implement new trackers in an hour or two. If you'd like support for a new tracker but are not a developer then feel free to leave a request on the [issues page](https://github.com/Jackett/Jackett/issues).  It is recommended to use Visual studio 2015 when making code changes in this project.


## Screenshots

![screenshot](https://i.imgur.com/0d1nl7g.png "screenshot")

[inviteneeded]: https://raw.githubusercontent.com/Jackett/Jackett/master/.github/label-inviteneeded.png
