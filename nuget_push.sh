#!/usr/bin/env bash

apiKey="fe68f856-1971-3bf7-841d-adfe9f0ffd5d";
source="http://119.23.53.36:8081/";

Usage()
{
	echo "Usage:  nuget_push <push_file>"
	exit 1
}

[ "$1" = "" ] && Usage

mono ./nuget.exe push -Source $source -ApiKey $apiKey "$1"
