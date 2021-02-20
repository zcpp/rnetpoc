# rnetpoc
POC of having dotnet core 3.1 work with R4.0 through R.NET 1.9

# Environment setup (on ubuntu 18.04)
## Install R 4.0
### set source: 
deb https://cloud.r-project.org/bin/linux/ubuntu bionic-cran40/
### secure apt:
sudo apt-key adv --keyserver keyserver.ubuntu.com --recv-keys E298A3A825C0D65DFD57CBB651716619E084DAB9
### install
sudo apt-get update
sudo apt-get install r-base
### set environment vars:
```bash
if [ "${LD_LIBRARY_PATH}" != "" ]
then
    export LD_LIBRARY_PATH=/usr/lib:/usr/lib/R/lib:/usr/local/lib:${LD_LIBRARY_PATH}
else
    export LD_LIBRARY_PATH=/usr/lib:/usr/lib/R/lib:/usr/local/lib
fi
# You may as well update the PATH environment variable, though R.NET does update it if need be.
export PATH=/usr/bin:/usr/lib/R/lib:${PATH}
export R_HOME=/usr/lib/R
```

## Install mono
sudo apt-get update

sudo apt-get install dirmngr gnupg apt-transport-https ca-certificates

sudo apt-key adv --keyserver hkp://keyserver.ubuntu.com:80 --recv-keys 3FA7E0328081BFF6A14DA29AA6A19B38D3D831EF

sudo sh -c 'echo "deb https://download.mono-project.com/repo/ubuntu stable-bionic main" > /etc/apt/sources.list.d/mono-official-stable.list'

sudo apt updatesudo apt install mono-complete

# Build and run
dotnet restore

dotnet build

dotnet run

# API test
http://localhost:5000/api/test/123