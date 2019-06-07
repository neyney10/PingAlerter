# PingAlerter
## Introduction (and a bit of storytelling)
I was playing some multiplayer FPS games (such as Call of Duty and Counter Strike), as I was traveling between places in the city I am connecting to different Wi-Fi routers and each time in a different distance from them, so as I was playing the FPS game I get sometimes packet drops or lags in crucial moments and I've got annoyed by those lags always happening at crucial moments, I wish if I can get a tool which monitors the latency and checks if at the exact moment the latency is ok, if it is not, then I can back off and wait for the lag to end, most lag spikes are no more than 1-2 seconds.

For this I've developed this tool in C# .Net 4.0 (Targeting 4.5.2) using WPF and the MV-VM Pattern for the UI.
This tool beeps (for now, I hope to add more features to it) when a lag spike is detected.

In order to determine latency stability and lag spikes I'm using statistics from the last few seconds and using some Probability formulas over them, such as Variance/Standard Deviation (High Std Deviation means less stability).
in general the user can config the threshold detection values for lag spikes.

the tool also differentiate Latency to router (Wi-Fi) and Latency to remote server address, as most lag spikes occur because of bad Wi-Fi signal or noise.

## Screenshots
![](/Media/screenshot1_settings.png)
![](/Media/screenshot2_logs.png)