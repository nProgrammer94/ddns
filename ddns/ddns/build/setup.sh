# Install Docker
## Install Docker
sudo apt update

sudo apt install -y \
     apt-transport-https \
     ca-certificates \
     curl \
     gnupg2 \
     software-properties-common


curl -fsSL https://download.docker.com/linux/$(. /etc/os-release; echo "$ID")/gpg | sudo apt-key add -


echo "deb [arch=$(dpkg --print-architecture)] https://download.docker.com/linux/$(. /etc/os-release; echo "$ID") \
     $(lsb_release -cs) stable" | \
    sudo tee /etc/apt/sources.list.d/docker.list


sudo apt update
sudo apt install -y --no-install-recommends \
    docker-ce \
    cgroupfs-mount

sudo systemctl enable docker
sudo systemctl start docker

## Install Docker Compose
sudo apt update
sudo apt install -y python3-pip libffi-dev
sudo pip3 install docker-compose

# Install Dotnet Core
sudo apt-get -y update
wget https://download.visualstudio.microsoft.com/download/pr/2ea7ea69-6110-4c39-a07c-bd4df663e49b/5d60f17a167a5696e63904f7a586d072/dotnet-sdk-3.1.102-linux-arm64.tar.gz
wget https://download.visualstudio.microsoft.com/download/pr/ec985ae1-e15c-4858-b586-de5f78956573/f585f8ffc303bbca6a711ecd61417a40/aspnetcore-runtime-3.1.2-linux-arm64.tar.gz

mkdir dotnet

tar zxf dotnet-sdk-3.1.102-linux-arm64.tar.gz -C $HOME/dotnet
tar zxf aspnetcore-runtime-3.1.2-linux-arm64.tar.gz -C $HOME/dotnet

export DOTNET_ROOT=$HOME/dotnet
export PATH=$PATH:$HOME/dotnet

sudo mkdir /var/www
sudo mkdir /var/www/ddns

sudo chmod -R 777 /var/www/ddns
sudo apt-get install unzip

cd /var/www/ddns

wget https://raw.githubusercontent.com/nProgrammer94/ddns/main/ddns/ddns/build/build.zip

unzip /var/www/ddns/build.zip

cd /etc/systemd/system
wget https://raw.githubusercontent.com/nProgrammer94/ddns/main/ddns/ddns/systemd/kestrel-ddns.service
wget https://raw.githubusercontent.com/nProgrammer94/ddns/main/ddns/ddns/systemd/kestrel-ddns.timer


sudo systemctl enable kestrel-ddns.service
sudo systemctl start kestrel-ddns.service

sudo systemctl enable kestrel-ddns.timer
sudo systemctl start kestrel-ddns.timer