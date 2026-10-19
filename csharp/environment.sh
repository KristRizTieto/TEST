# Source this file to activate the SDK and Chromium in this cloud environment.
export DOTNET_ROOT=/workspace/.dotnet
export DOTNET_CLI_HOME=/workspace/.dotnet-home
export NUGET_PACKAGES=/workspace/.nuget/packages
export XDG_RUNTIME_DIR=/workspace/.runtime
export XDG_DATA_HOME=/workspace/.local/share
export DOTNET_CLI_TELEMETRY_OPTOUT=1
export PLAYWRIGHT_CHROMIUM_EXECUTABLE_PATH=/usr/bin/chromium
export PATH="$DOTNET_ROOT:$PATH"
mkdir -p "$XDG_RUNTIME_DIR" "$XDG_DATA_HOME"
chmod 700 "$XDG_RUNTIME_DIR"
