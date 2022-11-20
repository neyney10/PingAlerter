# PingAlerter
## Introduction (and a bit of storytelling)
I was playing some multiplayer FPS games (such as Call of Duty and Counter Strike), as I was traveling between places in the city I am connecting to different Wi-Fi routers and each time in a different distance from them, so as I was playing the FPS game I get sometimes packet drops or lags in crucial moments and I've got annoyed by those lags always happening at crucial moments, I wish if I can get a tool which monitors the latency and checks if at the exact moment the latency is ok, if it is not, then I can back off and wait for the lag to end, most lag spikes are no more than 1-2 seconds.

For this I've developed this tool in C# .Net 4.0 (Targeting 4.5.2 to 4.72, depending on version) using WPF and the MV-VM Pattern for the UI.
This tool beeps (for now, I hope to add more features to it) when a lag spike is detected.

In order to determine latency stability and lag spikes I'm using statistics from the last few seconds and using some Probability formulas over them, such as Variance/Standard Deviation (High Std Deviation means less stability).
in general the user can config the threshold detection values for lag spikes.

the tool also differentiate Latency to router (Wi-Fi) and Latency to remote server address, as most lag spikes occur because of bad Wi-Fi signal or noise.

## Features
- Emphesize on perfomance: does not hit your system resources much, as tested with my laptop with 7200RPM HDD, and i7-6500u intel CPU, the latest version (0.16) currently consume 0.1 disk usage, around 30-50mb of RAM and around 0.4%-0.8% CPU utilizations.
- Log scans to file.
- Possible to log scans to MySql DB as well.
- Alert a sound on detection of spike lag.
- An partially transparent overlay window that changes alerts on lag spikes by changing color. Provides a visual indication of spikes while in full-screen applications such as games.
- Configure sensitivity of alerts.

### What's next? (In later versions)
- More Monitor Configuration options.
- More types of monitors such as LAN Monitor, Port Monitor and such.
- Support minimize to System tray.

## How to use
### Download
1. Head over to the [Releases](https://github.com/neyney10/PingAlerter/releases) tab to download the application.
2. Extract the files into a directory.
3. Launch `PingAlerter.exe.`

### Compile from Source
1. Clone this repository.
2. Install the required dependancies and packages via nuget as defined in packages.config via the command `nuget restore PingAlerter.sln`. (for other methods, please see the [official documentation](https://learn.microsoft.com/en-us/nuget/consume-packages/package-restore))
3. Open Visual Studio 2019 (or later) and select from the top toolbar menu `Build -> Build Solution`.
4. The build with all required files will be saved under `PingAlerter\bin`.
5. Launch `PingAlerter.exe.`

## Settings
![](/Media/1_6screenshot1.png)
- General Addresses: Applied for all addresses beside the default gateway
    - Latency Threshold: Alert where many adderesses were reporting a higher ping by this value than when sampled in the PreCheck. Example: where the sample gave an average RTT in milliseconds of 15, and your Latency Thereshold is around 50, so if on a certain scan some of the addresses were reporting higher than 15+50 (i.e 65 and above of RTT) then it would give an alert.
    - Stability Threshold: The variance of last scans, if the RTT of pings are scattering, it would give an alert.
- Default Gateway: Applied for the first router in your route as in Wi-Fi is what matters the most.
    - Latency Threshold: same.
    - Stability Threshold: same.
- PreCheck: before the monitor starts it first take few samples to get the base data to relate all next scans to it.
    - Sample Amount: amount of samples to take (amount of iterations/cycles), higher amount will give higher precision but slower start.
    - Ping Amount: each sample pings multiple times all hosts in its list, this value indicated how many times each sample pings.
- Alert Options.
    - Make Sound: pretty self explantory.
    - Alert Sound (.wav): a path to a sound (.wav) file to play when an alert is raised.
- Log Options: logging scans to file or database (currently only supports file)
    - Log Filepath: a txt filepath to save/append logs, does not overwrite on new monitoring sessions. Empty filepath disables this option.
    - connString: a MySQL Connection string.  Empty string disables this option. Please note that the app must have permissions to use user variables, by appending `Allow User Variables=True` to the end of the connection string.
## Dependencies and Requirements

 - Windows Vista and up (Does not support linux for now)
 - .Net framework 4.0 and above (Recommended version 4.7.2 and above)


## Screenshots
### Version 0.20 (Release build)
![](/Media/2_0screenshot1.png)
![](/Media/2_0screenshot2.png)
### Version 0.16 (Development build)
![](/Media/1_6screenshot1.png)
### Version 0.15 (Development build)
![](/Media/1_5screenshot1.png)
![](/Media/1_5screenshot2.png)
![](/Media/1_5screenshot3.png)
### Version 0.1 (Development build)
![](/Media/screenshot1_settings.png)
![](/Media/screenshot2_logs.png)
