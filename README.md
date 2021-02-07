# Dynamic DNS API
This is a Google Dynamic DNS client the uses the google domain API to update and ensure the domain IP remains up to date on a none-static or dynamic IP address connection.

## Features
Uses the full Google Dynamic DNS API found [here](https://support.google.com/domains/answer/6147083?hl=en).

## Usage
1. Download build.zip in [here](https://github.com/nProgrammer94/ddns/blob/main/ddns/ddns/build/build.zip) or download source code and build.
2. Install dotnet core on your server [link](https://docs.microsoft.com/en-us/dotnet/core/install/linux-ubuntu)
3. Copy build zip to server and unzip.
4. Change config in appsetting.json.
5. setup Systemd for run job automatic.

